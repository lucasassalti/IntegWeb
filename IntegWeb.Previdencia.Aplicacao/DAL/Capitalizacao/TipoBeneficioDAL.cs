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
    public class TipoBeneficioDAL:TipoBeneficio
    {
        protected DataTable Select()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_BENEFICIO", id_tpbeneficio);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_TPBENEFICIO.LISTAR");

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

        protected bool Insert(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_DESCRICAO", descricao);

                mensagem = "Registro Inserido com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_TPBENEFICIO.INSERIR");

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

        protected bool Update(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_BENEFICIO", id_tpbeneficio);
                objConexao.AdicionarParametro("P_DESCRICAO", descricao);

                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_TPBENEFICIO.ALTERAR");

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
                objConexao.AdicionarParametro("P_ID_BENEFICIO", id_tpbeneficio);
                objConexao.AdicionarParametroOut("P_RETURN");

                objConexao.ExecutarNonQuery("PRE_PKG_TPBENEFICIO.DELETAR");
                bool ret = int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;
                if (ret)
                    mensagem = "Registro Deletado com Sucesso";
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
