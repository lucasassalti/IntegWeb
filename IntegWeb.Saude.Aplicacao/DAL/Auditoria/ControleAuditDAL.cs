using IntegWeb.Entidades.Saude.Auditoria;
using IntegWeb.Framework;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Auditoria
{
    internal class ControleAuditDAL
    {
        public bool ImportaDados(DataTable dt)
        {
            bool ret = false;


            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.sau_tbl_histaudit";
                bulkCopy.ColumnMappings.Add("TipoServico", "TIPSERV");
                bulkCopy.ColumnMappings.Add("Nome", "NOME");
                bulkCopy.ColumnMappings.Add("Matricula", "MATRICULA");
                bulkCopy.ColumnMappings.Add("Hospital", "HOSPITAL");
                bulkCopy.ColumnMappings.Add("DataInternacao", "DT_INTER");
                bulkCopy.ColumnMappings.Add("DataAlta", "DT_ALTA");
                bulkCopy.ColumnMappings.Add("Custo", "CUSTO");
                bulkCopy.ColumnMappings.Add("Glosa", "GLOSA");
                bulkCopy.ColumnMappings.Add("Cobrado", "VALOR_COBRADO");
                bulkCopy.ColumnMappings.Add("PagoFuncesp", "VALOR_PAGO");
                bulkCopy.ColumnMappings.Add("DT_INCLUSAO", "DT_INCLUSAO");
                bulkCopy.ColumnMappings.Add("RESPONSAVEL", "RESPONSAVEL");
                bulkCopy.ColumnMappings.Add("ID_EMPAUDIT", "ID_EMPAUDIT");
                bulkCopy.ColumnMappings.Add("MESANO", "MESANO");

                try
                {
                    bulkCopy.WriteToServer(dt);
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    throw new Exception(ex.Message);
                }
                finally
                {
                    bulkCopy.Close();

                }


            }
            return ret;
        }

        public bool Deletar(AuditControle OBJ)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_MESANO", OBJ.mesano);
                objConexao.AdicionarParametro("P_ID_EMPAUDIT", OBJ.id_empaudit);
                return objConexao.ExecutarNonQuery("SAU_PKG_HISTAUDIT.DELETAR"); 
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


        }

        public DataTable ListaProcesso(AuditControle obj)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_MESANO", obj.mesano);
                objConexao.AdicionarParametro("P_ID_EMPAUDIT", obj.id_empaudit);
                System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_HISTAUDIT.LISTAR_PROCESSO");
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
    }
}
