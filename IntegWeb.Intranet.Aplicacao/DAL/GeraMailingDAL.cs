using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    class GeraMailingDAL
    {

        /// <summary>
        /// Retorna uma consulta com os plano de saude ativo
        /// </summary>
        /// <returns></returns>
        public DataTable ExtRelSau()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("srcReturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.proc_sau_consulta_saude");




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

        /// <summary>
        /// Retorna uma consulta com os dados de previdencia dos complementado e suplementados
        /// </summary>
        /// <returns></returns>
        public DataTable ExtRelPrev()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("srcReturn");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.proc_prev_consulta_previdencia");

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
    }
}
