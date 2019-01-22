using IntegWeb.Framework;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    internal class  SimulaBDDAL
    {
        public bool ImportaDados(DataTable dt)
        {
            bool ret = false;

         
                using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
                {
                    bulkCopy.DestinationTableName = "OWN_FUNCESP.PRE_TBL_SIM_BD";
                    bulkCopy.ColumnMappings.Add("EMPRESA", "EMPRESA");
                    bulkCopy.ColumnMappings.Add("REGISTRO", "REGISTRO");
                    bulkCopy.ColumnMappings.Add("PLANO", "PLANO");
                    bulkCopy.ColumnMappings.Add("MATRICULA", "MATRICULA");
                    bulkCopy.ColumnMappings.Add("DT_INCLUSAO", "DT_INCLUSAO");
                    bulkCopy.ColumnMappings.Add("RESPONSAVEL", "RESPONSAVEL");

                    try
                    {
                      bulkCopy.WriteToServer(dt);
                        ret = true;
                    }
                    catch (Exception ex)
                    {
                        ret = false;
                        throw new Exception(ex.Message + "\\n\\nVerique se a planinha contém as colunas (EMPRESA,REGISTRO,PLANO,MATRICULA)");
                    }
                    finally
                    {
                        bulkCopy.Close();

                    }
                   
                
            }
            return ret;
        }

        public bool Deletar()
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                return objConexao.ExecutarNonQuery("PRE_PKG_SIMULABD.DELETAR"); ;
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

        public DataTable SelectAll()
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("DADOS");
                System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_SIMULABD.LISTAR");

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
