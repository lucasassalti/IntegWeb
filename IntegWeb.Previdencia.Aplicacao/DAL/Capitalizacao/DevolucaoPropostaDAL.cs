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
    public class DevolucaoPropostaDAL : PropostaAdesao
    {
   
        protected DataTable SelecionarDevolucao()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_Id_Pradprev", id_pradprev);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PRADPREV.LISTA_DEV");

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

        protected bool Inserir(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_NOME", nome);
                objConexao.AdicionarParametro("P_DT_DEVOLUCAO", dt_devolucao);
                objConexao.AdicionarParametro("P_DESTINATARIO", destinatario);
                objConexao.AdicionarParametro("P_DESC_MOTIVO_DEV", desc_motivo_dev);
                objConexao.AdicionarParametro("P_MATRICULA", matricula);
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("P_TIPO_DOC", tipo_doc);
                objConexao.AdicionarParametroOut("P_RETORNO");

                mensagem = "Registro Inserido com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.INSERIR");

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

        protected bool Alterar(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
        

                objConexao.AdicionarParametro("P_REGISTRO", registro);
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("P_NOME", nome);
                objConexao.AdicionarParametro("P_DT_DEVOLUCAO", dt_devolucao);
                objConexao.AdicionarParametro("P_DESTINATARIO", destinatario);
                objConexao.AdicionarParametro("P_DESC_MOTIVO_DEV", desc_motivo_dev);

                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.ALTERAR_DEVOLUCAO");

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
                objConexao.AdicionarParametro("P_ID_PRADPREV", id_pradprev);
                objConexao.AdicionarParametroOut("P_RETURN");

                objConexao.ExecutarNonQuery("PRE_PKG_PRADPREV.DELETAR");
                bool ret = int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;
                if (ret)
                    mensagem = "Registro Deletado com Sucesso";
                else
                    mensagem = "Não é possível deletar um registro:\\nENVIADO/DEFERIDO/INDEFERIDO!";
                return ret;
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
