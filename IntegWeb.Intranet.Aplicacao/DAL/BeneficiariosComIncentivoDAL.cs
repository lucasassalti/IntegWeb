using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using IntegWeb.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    public class BeneficiariosComIncentivoDAL
    {
        protected DataTable DependentesIncentivo(int codEmp, int matricula)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("EMP", codEmp);
                objConexao.AdicionarParametro("MATR", matricula);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.PROC_BENEF_DEP_INCENTIVO");

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

        protected DataTable ParticipantesIncentivo(int codEmp, int matricula)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("MATR", matricula);
                objConexao.AdicionarParametro("EMP", codEmp);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OWN_FUNCESP.PROC_BENEF_PARTICIP_INCENTIVO");

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
