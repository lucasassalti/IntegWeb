using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Framework;
using IntegWeb.Entidades;
using System.Data;
using System.Data.OracleClient;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    public class EmailCancelamentoPlanoDAL
    {
        public DataTable selectCancelarPlanoTitular(string emp, string num_Matricula)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select NOM_PARTICIP , DES_PLANO, NUM_SUB_MATRIC, CASE WHEN DES_PLANO LIKE '%DIGNA%' THEN 1 ELSE 0 END PLANO_DIGNA ");
                querysql.Append("   FROM OWN_FUNCESP.ATE_VW_CANCELPLANOTITULAR ");
                querysql.Append(" WHERE ");
                querysql.Append("   cod_emprs = " + emp.ToString());
                querysql.Append("   AND num_matricula = " + num_Matricula.ToString());
                querysql.Append(" ORDER BY PLANO_DIGNA, DES_PLANO, NOM_PARTICIP ");

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString());
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

        public DataTable selectCancelarPlanoRepress(string emp, string num_Matricula, string repres)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select NOM_PARTICIP, DES_PLANO, NUM_SUB_MATRIC, CASE WHEN DES_PLANO LIKE '%DIGNA%' THEN 1 ELSE 0 END PLANO_DIGNA ");
                querysql.Append("   FROM OWN_FUNCESP.ATE_VW_CANCELPLANOREPRES ");
                querysql.Append(" WHERE ");
                querysql.Append("   cod_emprs = " + emp.ToString());
                querysql.Append("   AND num_matricula = " + num_Matricula.ToString());
                querysql.Append("   AND NUM_IDNTF_RPTANT = " + repres.ToString());
                querysql.Append(" ORDER BY PLANO_DIGNA, DES_PLANO, NOM_PARTICIP ");

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString());

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

        public DataTable selectRetornaProtocolo(string numChamado)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select D.id_cham_cd_chamado, D.cham_ds_protocolo FROM OWN_PLUSOFTCRM.CS_NGTB_CHAMADO_CHAM D ");
                querysql.Append(" WHERE ");
                querysql.Append(" D.id_cham_cd_chamado =" + numChamado.ToString());

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString());

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
