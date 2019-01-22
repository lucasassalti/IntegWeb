using IntegWeb.Entidades.Saude.Auditoria;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Auditoria
{
    internal class RegraCRCDAL
    {
        public DataTable SelectAll(RegraCRC objM)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_DESCRICAO", objM.des_regra);

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_REGRACRC.LISTAR");

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

        public bool Insert(out string mensagem, RegraCRC objM)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_DESCRICAO", objM.des_regra);
                objConexao.AdicionarParametro("P_VALOR", objM.valor);

                mensagem = "Registro Inserido com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_REGRACRC.INSERIR");

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

        public bool Update(out string mensagem, RegraCRC objM)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_DESCRICAO", objM.des_regra);
                objConexao.AdicionarParametro("P_VALOR", objM.valor);
                objConexao.AdicionarParametro("P_ID_REGRA", objM.id_regra);


                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_REGRACRC.ALTERAR");

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

        public bool Delete(out string mensagem, RegraCRC obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_REGRA", obj.id_regra);
                mensagem = "Registro Deletado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_REGRACRC.DELETAR");
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
