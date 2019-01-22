using IntegWeb.Entidades.Cartas;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class BenefRecusadoDAL : BenefRecusado
    {
            
        protected DataTable SelectBenef()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_ID_BENEFRECUSADO", id_benefrecusado);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_BENEFRECUSADO.LISTAR");

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

        protected bool InsertBenef(out string mensagem, out int id)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_NOME", nome);
                objConexao.AdicionarParametro("P_CPF", cpf);
                objConexao.AdicionarParametro("P_GRAU", grau);
                objConexao.AdicionarParametro("P_RG", rg);
                objConexao.AdicionarParametro("P_MAE", mae);
                objConexao.AdicionarParametro("P_PAI", pai);
                objConexao.AdicionarParametro("P_DTNASCIMENTO", dtNascimento);
                objConexao.AdicionarParametroOut("P_RETORNO");

                mensagem = "Registro Inserido com Sucesso";
                objConexao.ExecutarNonQuery("PRE_PKG_BENEFRECUSADO.INSERIR");
                id = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                mensagem = "Registro inserido com sucesso";
                return id > 0;

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

        protected bool UpdateBenef(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_BENEFRECUSADO", id_benefrecusado);
                objConexao.AdicionarParametro("P_NOME", nome);
                objConexao.AdicionarParametro("P_CPF", cpf);
                objConexao.AdicionarParametro("P_GRAU", grau);
                objConexao.AdicionarParametro("P_RG", rg);
                objConexao.AdicionarParametro("P_MAE", mae);
                objConexao.AdicionarParametro("P_PAI", pai);
                objConexao.AdicionarParametro("P_DTNASCIMENTO", dtNascimento);


                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_BENEFRECUSADO.ALTERAR");

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

        protected bool DeleteBenef(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_BENEFRECUSADO", id_benefrecusado);
                mensagem = "Registro Deletado com Sucesso";

                return objConexao.ExecutarNonQuery("PRE_PKG_BENEFRECUSADO.DELETAR");
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
    }
}
