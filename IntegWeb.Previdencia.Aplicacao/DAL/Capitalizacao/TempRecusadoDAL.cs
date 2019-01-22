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
    public class TempoRecusadoDAL : TempoRecusado
    {
        protected DataTable SelectTemp()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_ID_TPRECUSADO", id_temprecusado);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_TEMPPRECUSADO.LISTAR");

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

        protected bool InsertTemp(out string mensagem, out int id)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametro("P_EMPRESA", empresa);
                objConexao.AdicionarParametro("P_DTADMISSAO", dtadmissao);
                objConexao.AdicionarParametro("P_DTDEMISSAO", dtdemissao);
                objConexao.AdicionarParametroOut("P_RETORNO");

                mensagem = "Registro Inserido com Sucesso";
                objConexao.ExecutarNonQuery("PRE_PKG_TEMPPRECUSADO.INSERIR");
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

        protected bool UpdateTemp(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_TPRECUSADO", id_temprecusado);
                objConexao.AdicionarParametro("P_EMPRESA", empresa);
                objConexao.AdicionarParametro("P_DTADMISSAO", dtadmissao);
                objConexao.AdicionarParametro("P_DTDEMISSAO", dtdemissao);

                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_TEMPPRECUSADO.ALTERAR");

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

        protected bool DeleteTemp(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_TPRECUSADO", id_temprecusado);

                mensagem = "Registro Deletado com Sucesso";

                return objConexao.ExecutarNonQuery("PRE_PKG_TEMPPRECUSADO.DELETAR");
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
