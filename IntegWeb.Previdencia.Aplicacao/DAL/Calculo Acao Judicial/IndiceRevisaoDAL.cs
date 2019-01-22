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
    public class IndiceRevisaoDAL : IndiceRevisao
    {
        protected bool Inserir(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("P_NUM_LEI", num_lei);
                objConexao.AdicionarParametro("P_ANO_LEI", ano_lei);
                objConexao.AdicionarParametro("P_MES_LEI", mes_lei);
                objConexao.AdicionarParametro("P_IND_LEI", ind_lei);

                mensagem = "Inserido com Sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_ind_rev_lei.Inserir");



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
                objConexao.AdicionarParametro("P_NUM_LEI", num_lei);


                mensagem = "Deletado com Sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_ind_rev_lei.Deletar");

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

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_ind_rev_lei.Listar");

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
