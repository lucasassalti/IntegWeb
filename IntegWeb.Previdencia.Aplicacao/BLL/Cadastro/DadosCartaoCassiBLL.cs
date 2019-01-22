using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class DadosCartaoCassiBLL : DadosCartaoCassiDAL
    {

        //Ajusta os dados do datatable.
        private DataTable PreparaBaseDados(DataTable dtExcelBaseDados)
        {
            try
            {
                DataTable dtBaseDados = dtExcelBaseDados.Clone();
                //dtBaseDados.Columns["Data Adesão"].DataType = typeof(DateTime);
                //dtBaseDados.Columns["Data Exclusão"].DataType = typeof(DateTime);
                ////dtBaseDados.Columns["Data Inicial"].DataType = typeof(DateTime);
                //dtBaseDados.Columns["Data Final"].DataType = typeof(DateTime);
                //dtBaseDados.Columns["Dt. Nascimento"].DataType = typeof(DateTime);

                //Remove a Row que contem a palavra "Linha Total:" do Excel
                dtExcelBaseDados.Rows.Remove(dtExcelBaseDados.Rows[dtExcelBaseDados.Rows.Count - 1]);

                //Verifica as linhas que não tem "Mat. Funcional" preenchido
                for (int i = dtExcelBaseDados.Rows.Count - 1; i <= dtExcelBaseDados.Rows.Count && i > -1; i--)
                {
                    if (string.IsNullOrEmpty(dtExcelBaseDados.Rows[i]["Mat. Funcional"].ToString()))
                    {
                        dtExcelBaseDados.Rows.Remove(dtExcelBaseDados.Rows[i]);
                    }
                }

                //Ajustar campos CPF e MatFuncional
                for (int i = 0; i < dtExcelBaseDados.Rows.Count; i++)
                {
                    DateTime resultado = DateTime.MinValue;

                    //ajusta a matricula para 14 digitos
                    dtExcelBaseDados.Rows[i]["Mat. Funcional"] = dtExcelBaseDados.Rows[i]["Mat. Funcional"].ToString().PadLeft(14, '0');
                    //ajusta o CPF para 11 digitos
                    dtExcelBaseDados.Rows[i]["CPF"] = dtExcelBaseDados.Rows[i]["CPF"].ToString().PadLeft(11, '0');

                    //Resolvendo problema da data do excell que vem como numeros.

                    var valor = convertDateIntToDatetime(dtExcelBaseDados.Rows[i]["Data Inicial"].ToString());

                    dtExcelBaseDados.Rows[i]["Data Inicial"] = valor.ToString().Substring(0, 10);
                    //dtExcelBaseDados.Rows[i]["Data Final"] = convertDateIntToDatetime(dtExcelBaseDados.Rows[i]["Data Final"].ToString());
                    //dtExcelBaseDados.Rows[i]["Dt. Nascimento"] = convertDateIntToDatetime(dtExcelBaseDados.Rows[i]["Dt. Nascimento"].ToString());
                    //dtExcelBaseDados.Rows[i]["Data Adesão"] = convertDateIntToDatetime(dtExcelBaseDados.Rows[i]["Data Adesão"].ToString());

                }

                //Verifica se o valor Mat. Funcional já existe na tabela SAU_TBL_DADOS_CARTAO_CASSI e removi a linha.
                for (int i = dtExcelBaseDados.Rows.Count - 1; i <= dtExcelBaseDados.Rows.Count && i > -1; i--)
                {
                    if (PesquisaMatricula(dtExcelBaseDados.Rows[i]["Mat. Funcional"].ToString()))
                    {
                        dtExcelBaseDados.Rows.Remove(dtExcelBaseDados.Rows[i]);
                    }
                }

                //remove itens repetidos do DataTable
                DataTable semDuplicidade = RemovelinhasDuplicadas(dtExcelBaseDados, "Mat. Funcional");

                //Merge dos dados
                dtBaseDados.Merge(semDuplicidade, true, MissingSchemaAction.Ignore);

                return dtBaseDados;

            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message + "\\n\\nVerique se as linhas planinha estão corretas");
            }
        }

        //Removi linhas duplicadas
        public DataTable RemovelinhasDuplicadas(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            return dTable;
        }

        public bool Importar(DataTable dt, out DataTable valorDtAjustado)
        {

            bool ret = false;
            try
            {
                valorDtAjustado = PreparaBaseDados(dt);

                if (valorDtAjustado.Rows.Count > 0)
                {
                    ret = ImportaDados(valorDtAjustado);
                }

                return ret;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

                throw;
            }
        }

        public DateTime? convertDateIntToDatetime(string valor)
        {
            DateTime resultado = DateTime.MinValue;
            try
            {
                //Resolvendo problema da data do excell que vem como numeros.
                if (!DateTime.TryParse(valor.ToString().Trim(), out resultado))
                {
                    double val = double.Parse(valor.ToString());
                    DateTime requiredDate = DateTime.FromOADate(val);

                    return requiredDate;
                }
                else
                {
                    return Util.String2Date(valor);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + "\\n\\nVerique o campo de data");
            }

        }


        #region "Tab de Controle Cassi"
        
        public DataTable selectCassiControle()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select * FROM OWN_FUNCESP.CAD_TBL_CONTROLECASSI ");

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString());
                adpt.Fill(dt);
                adpt.Dispose();
            }

            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }


        public DataTable selectFiltroCassiControle(string empresa, string matricula, string sub, string nomeParticipante, string tipomovimentacao)
        {
            DataTable dt = new DataTable();
            List<CAD_TBL_CONTROLECASSI> list = new List<CAD_TBL_CONTROLECASSI>();
            list = filtroControleCassi(empresa, matricula, sub,nomeParticipante.Trim(), tipomovimentacao).ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;

            

        }


        #endregion
    }
}
