using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class DadosCartaoCassiDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<SAU_TBL_DADOS_CARTAO_CASSI> GetData(int startRowIndex, int maximumRows, string pNome, string pMatricula, string sortParameter)
        {
            return GetWhere(pNome, pMatricula)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int GetDataCount(string pNome, string pMatricula)
        {
            return GetWhere(pNome, pMatricula).SelectCount();
        }

        public IQueryable<SAU_TBL_DADOS_CARTAO_CASSI> GetWhere(string pNome, string pMatricula)
        {
            IQueryable<SAU_TBL_DADOS_CARTAO_CASSI> query =
                    from dcc in m_DbContext.SAU_TBL_DADOS_CARTAO_CASSI
                    where (dcc.MATFUNCIONAL == pMatricula || pMatricula == null)
                       && (dcc.NOME.Contains(pNome.ToUpper()) || pNome == null)
                    select dcc;

            return query;
        }

        //Busca a Carterinha da Cassi pela MATFUNCIONAL
        public SAU_TBL_DADOS_CARTAO_CASSI GetCarterinha(string MATFUNCIONAL)
        {
            IQueryable<SAU_TBL_DADOS_CARTAO_CASSI> query;
            query = from a in m_DbContext.SAU_TBL_DADOS_CARTAO_CASSI.AsNoTracking()
                    where (a.MATFUNCIONAL == MATFUNCIONAL)
                    select a;

            return query.FirstOrDefault();
        }

        //Verifica se existe a Matriculafunciona
        public bool PesquisaMatricula(string MATFUNCIONAL)
        {
            IQueryable<SAU_TBL_DADOS_CARTAO_CASSI> query;
            query = from a in m_DbContext.SAU_TBL_DADOS_CARTAO_CASSI.AsNoTracking()
                    where (a.MATFUNCIONAL == MATFUNCIONAL)
                    select a;

            if (query.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        public bool DeleteDados()
        {
            bool ret = false;

            try
            {
                int delete = m_DbContext.Database.ExecuteSqlCommand("DELETE OWN_FUNCESP.SAU_TBL_DADOS_CARTAO_CASSI");

                if (delete != 0)
                {
                    ret = true;
                }
            }

            catch (Exception ex)
            {
                Util.GetInnerException(ex);
            }
            return ret;
        }

        public bool ImportaDados(DataTable dt)
        {
            bool ret = false;

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "own_funcesp.SAU_TBL_DADOS_CARTAO_CASSI";
                bulkCopy.ColumnMappings.Add("Nome", "NOME");
                bulkCopy.ColumnMappings.Add("Matrícula", "MATRICULA");
                bulkCopy.ColumnMappings.Add("Status", "STATUS");
                bulkCopy.ColumnMappings.Add("Data Adesão", "DATAADESAO");
                bulkCopy.ColumnMappings.Add("Data Exclusão", "DATAEXCLUSAO");
                bulkCopy.ColumnMappings.Add("Cartão", "CARTAO");
                bulkCopy.ColumnMappings.Add("Data Inicial", "DATAINICIAL");
                bulkCopy.ColumnMappings.Add("Data Final", "DATAFINAL");
                bulkCopy.ColumnMappings.Add("U.F.", "UF");
                bulkCopy.ColumnMappings.Add("Dt. Nascimento", "DTNASCIMENTO");
                bulkCopy.ColumnMappings.Add("CPF", "CPF");
                bulkCopy.ColumnMappings.Add("Mat. Funcional", "MATFUNCIONAL");
                bulkCopy.ColumnMappings.Add("Lotação", "LOTACAO");

                try
                {
                    bulkCopy.WriteToServer(dt);
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    throw new Exception(ex.Message + "\\n\\nVerique se a planinha contém as colunas");
                }
                finally
                {
                    bulkCopy.Close();
                }
            }
            return ret;
        }

        public Resultado atualizaTabelaControleCassi(CAD_TBL_CONTROLECASSI obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.CAD_TBL_CONTROLECASSI.FirstOrDefault(p => p.ID_REG == obj.ID_REG);

                if (atualiza != null)
                {
                    atualiza.DT_ENVIO = obj.DT_ENVIO;

                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro Atualizado com Sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }


        public IQueryable<CAD_TBL_CONTROLECASSI> filtroControleCassi(string pEmpresa, string pMatricula, string pSub, string pnomeParticipante ,string pTipoMovimentacao)
        {
            string tipoMovimentacao = "";
            if (pTipoMovimentacao == "1")
            {
                tipoMovimentacao = "INCLUSÃO";
            }

            if (pTipoMovimentacao == "2")
            {
                tipoMovimentacao = "SEGUNDA_VIA";
            }

            IQueryable<CAD_TBL_CONTROLECASSI> query =
                    from dcc in m_DbContext.CAD_TBL_CONTROLECASSI
                    where (dcc.EMPRESA == pEmpresa || pEmpresa == null)
                       && (dcc.MATRICULA == pMatricula || pMatricula == null)
                       && (dcc.SUB_MATRICULA == pSub || pSub == null)
                       && (dcc.NOM_PARTICIP.ToUpper().Trim().Contains(pnomeParticipante.ToUpper()) || pnomeParticipante.Trim() == null)
                       && (dcc.TIPOMOVIMETACAO.Contains(tipoMovimentacao) || tipoMovimentacao == null)
                    select dcc;

            return query;
        }



    }
}
