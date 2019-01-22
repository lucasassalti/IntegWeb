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
    internal class ControleCRCDAL
    {
        public DataTable ListaVidas(PagamentoCRC obj)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_dt_inclusao", obj.dt_inclusao);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_CRC.LISTAR_REL_VIDAS");


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

        public DataTable ListaUsuario(PagamentoCRC obj)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_MESANO", obj.mesano);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_CRC.LISTAR_USUARIO");


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

        public bool InserAreaAssinatura(PagamentoCRC objM)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_MATRICULA", objM.matricula);
                return objConexao.ExecutarNonQuery("SAU_PKG_CRC.INSERIR_REL_VIDAS");

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
