using IntegWeb.Entidades.Cartas;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public  class TipoServicoDAL : TipoServico
    {
        protected DataTable Select(string sWhere)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_TPSERVICO", id_tpservico);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_TPSERVICO.LISTAR");

                adpt.Fill(dt);
                adpt.Dispose();

                if (!String.IsNullOrEmpty(sWhere))
                {
                    dt = dt.Select(sWhere, "").CopyToDataTable();
                }

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

        protected bool Insert(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_DESCRICAO", descricao);

                mensagem = "Registro Inserido com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_TPSERVICO.INSERIR");

            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }



        }

        protected bool Update(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_TPSERVICO", id_tpservico);
                objConexao.AdicionarParametro("P_DESCRICAO", descricao);

                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_TPSERVICO.ALTERAR");

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

        protected bool Delete(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_TPSERVICO", id_tpservico);
                objConexao.AdicionarParametroOut("P_RETURN");

                objConexao.ExecutarNonQuery("PRE_PKG_TPSERVICO.DELETAR");
                bool ret = int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;
                if (ret)
                    mensagem = "Registro deletado com sucesso";
                else
                    mensagem = "Não é possível deletar um registro que possui vínculo.";
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
