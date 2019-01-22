using IntegWeb.Entidades.Administracao;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;


namespace IntegWeb.Saude.Aplicacao.DAL.Financeiro
{
    public class GerarRelatorioAtuarialDAL
    {
        public DataTable ListaDatas(String tipoRelatorio, String dataInicial, String dataFinal)
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");

                if (tipoRelatorio == "REL_CO_PARTICIPACAO")
                {
                    objConexao.AdicionarParametro("P_DATA_INI", dataInicial);
                    objConexao.AdicionarParametro("P_DATA_FIM", dataFinal);
                }
                else if (tipoRelatorio == "REL_PROCEDIMENTOS")
                {
                    objConexao.AdicionarParametro("P_DATA_INI", dataInicial);
                    objConexao.AdicionarParametro("P_DATA_FIM", dataFinal);
                }
                else if (tipoRelatorio == "REL_MENSALIDADES")
                {
                    objConexao.AdicionarParametro("P_DATA_INI", dataInicial);
                    objConexao.AdicionarParametro("P_DATA_FIM", dataFinal);
                }


                OracleDataAdapter adpt = objConexao.ExecutarAdapter("own_funcesp.sau_pkg_relatorio_atuarial." + tipoRelatorio);

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

        public DataTable ListaSinistro(string mes, string ano)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");

                objConexao.AdicionarParametro("P_DATA_MES", mes);
                objConexao.AdicionarParametro("P_DATA_ANO", ano);

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.SAU_PKG_RELATORIO_ATUARIAL.REL_SINISTRO");

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
