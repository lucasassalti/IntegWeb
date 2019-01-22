using IntegWeb.Entidades.Carga;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class ProcPortalDAL
    {
        public DataTable JOB_SAU_CLASSE_CONV()
        {
            //OracleDataAdapter retorno;
            ConexaoOracle objConexao = new ConexaoOracle();
            DataTable dt = new DataTable();
            try
            {

                objConexao.AdicionarParametroCursor("CurPrinc");
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_CARGA_SAUDE_MOBILE.JOB_SAU_CLASSE_CONV");

                adpt.Fill(dt);
                adpt.Dispose();

                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public DataTable JOB_SAU_CONV_PLANO()
        {
            //OracleDataAdapter retorno;
            ConexaoOracle objConexao1 = new ConexaoOracle();
            DataTable dt1 = new DataTable();
            try
            {

                objConexao1.AdicionarParametroCursor("CurPrinc1");
                OracleDataAdapter adpt1 = objConexao1.ExecutarAdapter("PRE_PKG_CARGA_SAUDE_MOBILE.JOB_SAU_CONV_PLANO");

                adpt1.Fill(dt1);
                adpt1.Dispose();

                return dt1;
            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message);
            }
            finally
            {
                objConexao1.Dispose();
            }
        }

        public DataTable JOB_SAU_ESPC_RED_CRED()
        {
            //OracleDataAdapter retorno;
            ConexaoOracle objConexao2 = new ConexaoOracle();
            DataTable dt2 = new DataTable();
            try
            {

                objConexao2.AdicionarParametroCursor("CurPrinc2");
                OracleDataAdapter adpt2 = objConexao2.ExecutarAdapter("PRE_PKG_CARGA_SAUDE_MOBILE.JOB_SAU_ESPC_RED_CRED");

                adpt2.Fill(dt2);
                adpt2.Dispose();

                return dt2;
            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message);
            }
            finally
            {
                objConexao2.Dispose();
            }
        }

        public DataTable JOB_SAU_QUALIF_REDCRED()
        {
            //OracleDataAdapter retorno;
            ConexaoOracle objConexao3 = new ConexaoOracle();
            DataTable dt3 = new DataTable();
            try
            {

                objConexao3.AdicionarParametroCursor("CurPrinc3");
                OracleDataAdapter adpt3 = objConexao3.ExecutarAdapter("PRE_PKG_CARGA_SAUDE_MOBILE.JOB_SAU_QUALIF_REDCRED");

                adpt3.Fill(dt3);
                adpt3.Dispose();

                return dt3;
            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message);
            }
            finally
            {
                objConexao3.Dispose();
            }
        }

        public DataTable JOB_SAU_REDE_CREDENCIADA()
        {
            //OracleDataAdapter retorno;
            ConexaoOracle objConexao4 = new ConexaoOracle();
            DataTable dt4 = new DataTable();
            try
            {

                objConexao4.AdicionarParametroCursor("CurPrinc4");
                OracleDataAdapter adpt4 = objConexao4.ExecutarAdapter("PRE_PKG_CARGA_SAUDE_MOBILE.JOB_SAU_REDE_CREDENCIADA");

                adpt4.Fill(dt4);
                adpt4.Dispose();

                return dt4;
            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message);
            }
            finally
            {
                objConexao4.Dispose();
            }
        }
    }
}
