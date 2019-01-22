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
    public class IncideJurosDAL : IncideJuros
    {
        protected bool Inserir(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("P_NUM_IDNTF_RPTANT", num_idntf_rptant);
                objConexao.AdicionarParametro("P_TX_JUROS", tx_juros);
                objConexao.AdicionarParametro("P_DT_INIC_VIG", dt_inic_vig);
                objConexao.AdicionarParametro("P_DT_FIM_VIG", dt_fim_vig);

                mensagem = "Inserido com Sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_incid_juros.Inserir");



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
                objConexao.AdicionarParametro("P_DT_INIC_VIG", dt_inic_vig);


                mensagem = "Deletado com Sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_incid_juros.Deletar");

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

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_incid_juros.Listar");

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
