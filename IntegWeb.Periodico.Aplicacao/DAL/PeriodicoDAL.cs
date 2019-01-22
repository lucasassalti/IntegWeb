using IntegWeb.Entidades;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    internal class PeriodicoDAL  
    {

        public bool Insert(out string mensagem, PeriodicoObj objM)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_EDITORA", objM.id_editora);
                objConexao.AdicionarParametro("P_NOME", objM.nome_periodico);
                objConexao.AdicionarParametro("P_CODIGO", objM.codigo);

                mensagem = "Registro Inserido com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_PERIODICO.INSERIR");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public DataTable SelectAll(PeriodicoObj objM)
        {
             DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_ID_PERIODICO", objM.id_periodico);
                objConexao.AdicionarParametro("P_ID_EDITORA", objM.id_editora);
                objConexao.AdicionarParametro("P_NOME", objM.nome_periodico);

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_PERIODICO.LISTAR");

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

        public bool Update(out string mensagem, PeriodicoObj objM)
        {


            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PERIODICO", objM.id_periodico);
                objConexao.AdicionarParametro("P_NOME", objM.nome_periodico);
                objConexao.AdicionarParametro("P_CODIGO", objM.codigo);
        
                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_PERIODICO.ALTERAR");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
        }

        public bool Delete(out string mensagem, PeriodicoObj obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PERIODICO", obj.id_periodico);
                objConexao.AdicionarParametroOut("P_RETURN");

                objConexao.ExecutarNonQuery("SAU_PKG_PERIODICO.DELETAR");
                bool ret = int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;
                if (ret)
                    mensagem = "Registro deletado com sucesso";
                else
                    mensagem = "Não é possível deletar um registro que possui vínculo.";
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


        }
    }
}
