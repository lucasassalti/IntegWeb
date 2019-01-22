using IntegWeb.Entidades.Saude.Faturamento;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Faturamento
{
    public  class AdinResumoDAL :AdinResumo
    {
        protected DataTable SelectReport()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_MESANO", mesano);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ADINRESUMO.LIST_REPORT");


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

        protected bool CreateReport(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_MATRICULA", matricula);
                mensagem = "Processado com sucesso!\\n\\nPara consultar o resultado utilize mês/ano.";
                return objConexao.ExecutarNonQuery("SAU_PKG_ADINRESUMO.CREATE_REPORT");
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
