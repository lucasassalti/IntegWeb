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
    public class ControleUnimedCrmDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public List<CAD_TBL_CONTROLEUNIMED> GetData(int startRowIndex, int maximumRows, short? nempr, int? nreg, string sub, string pessDsCpf, string sortParameter)
        {
            return GetWhere(nempr, nreg, sub, pessDsCpf)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int GetDataCount(short? nempr, int? nreg, string sub, string pessDsCpf)
        {
            return GetWhere(nempr, nreg, sub, pessDsCpf).Count();
        }

        public IQueryable<CAD_TBL_CONTROLEUNIMED> GetWhere(short? nempr, int? nreg, string sub, string pessDsCpf)
        {
            IQueryable<CAD_TBL_CONTROLEUNIMED> query;

            long lcpf = long.Parse(pessDsCpf.Replace(".", "").Replace("-", ""));

            query = from u in m_DbContext.CAD_TBL_CONTROLEUNIMED
                    where (u.COD_EMPRS == nempr || nempr == null)
                    && (u.NUM_MATRICULA == nreg || nreg == null)
                    && (u.SUB_MATRICULA == sub || sub == null)
                    && (u.CPF == lcpf || pessDsCpf == null)

                    select u;

            return query;
        }

        public DataTable selectUnimedPlanoSaudeTitular(string emp, string num_Matricula)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select * FROM OWN_FUNCESP.ATE_VW_UnimedPlanoSaudeTITULAR ");
                querysql.Append(" WHERE ");
                querysql.Append(" cod_emprs = " + emp.ToString());
                querysql.Append(" AND num_matricula = " + num_Matricula.ToString());

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

        public DataTable selectUnimedPlanoSaudeRepres(string emp, string num_Matricula, string nrepr)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select * FROM OWN_FUNCESP.ATE_VW_UnimedPlanoSaudeRepres ");
                querysql.Append(" WHERE ");
                querysql.Append(" cod_emprs = " + emp.ToString());
                querysql.Append(" AND num_matricula = " + num_Matricula.ToString());
                querysql.Append(" AND NUM_IDNTF_RPTANT = " + nrepr.ToString());


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
