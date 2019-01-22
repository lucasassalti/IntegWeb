using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;
using IntegWeb.Framework;
using IntegWeb.Entidades;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    public class OuvRelEstourosDAL
    {
        protected int RelEstouro(DateTime dtIni, DateTime dtFim)
        {
            int previsao = 0;

            ConexaoOracle objConexao = new ConexaoOracle();

            try
            { 
                objConexao.AdicionarParametro("DT_INICIO", dtIni);
                objConexao.AdicionarParametro("DT_FINAL", dtFim);
                objConexao.AdicionarParametroOut("DADOS");

                objConexao.ExecutarNonQuery("OUV_PKG_REL_GERENCIAL_ESTOUROS.REL_MENSAL_BLOCO1");

                previsao = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return previsao;
        }


        protected DataTable QtdEstouro(DateTime? dtIni, DateTime? dtFim)
        {
            DataTable dt = new DataTable();

            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                objConexao.AdicionarParametro("DT_INICIO", dtIni);
                objConexao.AdicionarParametro("DT_FINAL", dtFim);

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OUV_PKG_REL_GERENCIAL_ESTOUROS.QTD_ESTOUROS_BLOCO2");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }


        protected DataTable RespPeriodo(DateTime? dtIni, DateTime? dtFim)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                objConexao.AdicionarParametro("DT_INICIO", dtIni);
                objConexao.AdicionarParametro("DT_FINAL", dtFim);

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OUV_PKG_REL_GERENCIAL_ESTOUROS.RESP_PERIODO_BLOCO3");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt; 
        }


        protected DataTable EstouroMensal(DateTime? dtIni, DateTime? dtFim)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                objConexao.AdicionarParametro("DT_INICIO", dtIni);
                objConexao.AdicionarParametro("DT_FINAL", dtFim);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OUV_PKG_REL_GERENCIAL_ESTOUROS.ESTOURO_MENSAL_BLOCO4");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt; 
        }


        protected DataTable RespostaMensal(DateTime? dtIni, DateTime? dtFim)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                objConexao.AdicionarParametro("DT_INICIO", dtIni);
                objConexao.AdicionarParametro("DT_FINAL", dtFim);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OUV_PKG_REL_GERENCIAL_ESTOUROS.RESPOSTAS_MENSAL_BLOCO5");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt; 
        }

        protected DataTable AreasRelatorio()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OUV_PKG_REL_GERENCIAL_ESTOUROS.AREAS_REL_ESTOUROS");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }
    }
}
