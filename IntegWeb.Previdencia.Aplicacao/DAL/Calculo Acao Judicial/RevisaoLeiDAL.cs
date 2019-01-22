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
    public class RevisaoLeiDAL : RevisaoLei
    {
        protected bool Inserir(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_NUM_LEI", num_lei);
                objConexao.AdicionarParametro("P_DSC_LEI", dsc_lei);
                objConexao.AdicionarParametro("P_DATA_INIC_VIG", data_inic_vig);
                objConexao.AdicionarParametro("P_DATA_FIM_VIG", data_fim_vig);

                mensagem = "Inserido com Sucesso";

                return objConexao.ExecutarNonQuery("PRE_PKG_ACAO_REV_LEI.Inserir");



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

                objConexao.AdicionarParametro("P_NUM_LEI", num_lei);


                mensagem = "Deletado com Sucesso";

                return objConexao.ExecutarNonQuery("PRE_PKG_ACAO_REV_LEI.Deletar");

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
                objConexao.AdicionarParametro("P_NUM_LEI", num_lei);

                objConexao.AdicionarParametroCursor("dados");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_ACAO_REV_LEI.Listar");

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
