using  IntegWeb.Entidades;
using  IntegWeb.Entidades.Periodico;
using  IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    internal class AreaDAL
    {
         
        public DataTable SelectAll(Area obj)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_AREA", obj.id_area);
                objConexao.AdicionarParametro("P_SIGLA", obj.sigla);
                objConexao.AdicionarParametro("P_DESCRICAO", obj.descricao);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_AREA.LISTAR");

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

        public bool Insert(out string mensagem, Area objM)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_SIGLA", objM.sigla);
                objConexao.AdicionarParametro("P_DESCRICAO", objM.descricao);
                objConexao.AdicionarParametro("P_EDIFICIO", objM.edificio);
                objConexao.AdicionarParametro("P_ANDAR", objM.andar);
                objConexao.AdicionarParametro("P_CODIGO", objM.codigo);
                objConexao.AdicionarParametro("P_RESPONSAVEL", objM.responsavel);

                mensagem = "Registro Inserido com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_AREA.INSERIR");

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

        public bool Update(out string mensagem, Area objM)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_ID_AREA", objM.id_area);
                objConexao.AdicionarParametro("P_SIGLA", objM.sigla);
                objConexao.AdicionarParametro("P_DESCRICAO", objM.descricao);
                objConexao.AdicionarParametro("P_EDIFICIO", objM.edificio);
                objConexao.AdicionarParametro("P_ANDAR", objM.andar);
                objConexao.AdicionarParametro("P_CODIGO", objM.codigo);
                objConexao.AdicionarParametro("P_RESPONSAVEL", objM.responsavel);
                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_AREA.ALTERAR");

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

        public bool Delete(out string mensagem, Area obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_AREA", obj.id_area);
                objConexao.AdicionarParametroOut("P_RETURN");

                objConexao.ExecutarNonQuery("SAU_PKG_AREA.DELETAR");
                mensagem = "Registro Deletado com Sucesso";
                return int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;
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
