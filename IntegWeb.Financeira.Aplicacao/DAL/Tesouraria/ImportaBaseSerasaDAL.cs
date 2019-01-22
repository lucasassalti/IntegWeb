using IntegWeb.Framework;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL
{
    internal class ImportaBaseSerasaDAL
    {
        public bool ImportaDados(DataTable dt)
        {
            bool ret = false;

         
                using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
                {
                    bulkCopy.DestinationTableName = "OWN_FUNCESP.AAT_TBL_SERASA_PEFIN";
                    
                    bulkCopy.ColumnMappings.Add("EMPRESA", "COD_EMPRS");
                    bulkCopy.ColumnMappings.Add("MATRICULA", "NUM_RGTRO_EMPRG");
                    bulkCopy.ColumnMappings.Add("CONTRATO", "NUM_CONTRATO");
                    bulkCopy.ColumnMappings.Add("NOME", "NOM_NOME");
                    bulkCopy.ColumnMappings.Add("CPF", "NUM_CPF");
                    bulkCopy.ColumnMappings.Add("VALOR", "VLR_VALOR");
                    bulkCopy.ColumnMappings.Add("DT_COMPROMIS_DEV", "DAT_COMPROMIS_DEV");
                    bulkCopy.ColumnMappings.Add("DAT_IMPORTACAO", "DAT_IMPORTACAO");
                    bulkCopy.ColumnMappings.Add("TIPO", "COD_OPERACAO");
                    bulkCopy.ColumnMappings.Add("DESC_USER", "DESC_USER");
                    //bulkCopy.ColumnMappings.Add("COD_REMESSA_SERASA_PEFIN", "COD_REMESSA_SERASA_PEFIN");

                    try
                    {
                      bulkCopy.WriteToServer(dt);
                        ret = true;
                    }
                    catch (Exception ex)
                    {
                        ret = false;
                        throw new Exception(ex.Message + "\\n\\nVerique se a planinha contém as colunas (EMPRESA, MATRICULA, NOME, CPF, CONTRATO, DT_COMPROMIS_DEV, VALOR, TIPO)");
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
                return objConexao.ExecutarNonQuery("AAT_PKG_SERASA_PEFIN.DELETAR");
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
                System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("AAT_PKG_SERASA_PEFIN.LISTAR");

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

        public DataTable SelectRemessas()
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("DADOS");
                System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("AAT_PKG_SERASA_PEFIN.LISTAR_REMESSAS");

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


        public int GeraRemessa()
        {
            int vReturn = 0;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroOut("COD_REMESSA_SERASA_PEFIN");
                //System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("AAT_PKG_SERASA_PEFIN.GERA_REMESSA");
                objConexao.ExecutarNonQuery("AAT_PKG_SERASA_PEFIN.GERA_REMESSA");
                if (objConexao.ReturnParemeterOut().Value!=null)
                {
                    vReturn = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return vReturn;


        }



        internal bool Atualizar(int NumRemessa, int NumRemessaNova)
        {
            bool vReturn = false;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_COD_REMESSA", NumRemessa);
                objConexao.AdicionarParametro("V_COD_REMESSA_NOVO", NumRemessaNova);
                //System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("AAT_PKG_SERASA_PEFIN.GERA_REMESSA");
                objConexao.ExecutarNonQuery("AAT_PKG_SERASA_PEFIN.ATUALIZA_REMESSA");
                vReturn = true;

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return vReturn;
        }

        internal bool Deletar_Remessa(int NumRemessa)
        {
            bool vReturn = false;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_COD_REMESSA", NumRemessa);
                objConexao.ExecutarNonQuery("AAT_PKG_SERASA_PEFIN.DELETA_REMESSA");
                vReturn = true;

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return vReturn;
        }
    }
}
