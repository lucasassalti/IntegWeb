using IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial
{
    public class CoeficientePensaoDAL: CoeficientePensao
    {
        protected bool Inserir(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("P_NUM_IDNTF_RPTANT", num_idntf_rptant);
                objConexao.AdicionarParametro("P_ANO_DE", ano_de);
                objConexao.AdicionarParametro("P_MES_DE", mes_de);
                objConexao.AdicionarParametro("P_ANO_ATE", ano_ate);
                objConexao.AdicionarParametro("P_MES_ATE", mes_ate);
                objConexao.AdicionarParametro("P_COEF_PENS_MES", coef_pens_mes);

                mensagem = "Inserido com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_coef_pensao.Inserir");



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }



        }

        protected bool Deletar(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("P_NUM_IDNTF_RPTANT", num_idntf_rptant);
                objConexao.AdicionarParametro("P_ANO_DE", ano_de);


                mensagem = "Deletado com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_coef_pensao.Deletar");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }



        }

        protected DataTable Listar()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametroCursor("dados");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_coef_pensao.Listar");

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
