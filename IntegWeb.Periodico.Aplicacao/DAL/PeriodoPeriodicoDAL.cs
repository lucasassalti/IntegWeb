
using  IntegWeb.Entidades;
using  IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Periodico.Aplicacao.DAL
{



    internal class PeriodoPeriodicoDAO 
    {

        public DataTable SelectAll( PeriodoPeriodico obj)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PERIODO", obj.cod_periodico);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_PERIODO.LISTAR");

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

        public bool Insert(out string mensagem, PeriodoPeriodico obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("P_DESCRICAO", obj.desc_periodo);

                mensagem = "Registro inserido com sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_PERIODO.INSERIR");

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

        public bool Update(out string mensagem, PeriodoPeriodico obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PERIODO", obj.cod_periodico);
                objConexao.AdicionarParametro("P_DESCRICAO", obj.desc_periodo);

                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_PERIODO.ALTERAR");

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

        public bool Delete(out string mensagem, PeriodoPeriodico obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_ID_PERIODO", obj.cod_periodico);
                objConexao.AdicionarParametroOut("P_RETURN");

                objConexao.ExecutarNonQuery("SAU_PKG_PERIODO.DELETAR");
                bool ret =int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;
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
