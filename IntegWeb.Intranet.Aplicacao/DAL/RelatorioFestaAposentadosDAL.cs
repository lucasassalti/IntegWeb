using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using IntegWeb.Framework;
using IntegWeb.Entidades;
using IntegWeb.Intranet.Aplicacao.ENTITY;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    public class RelatorioFestaAposentadosDAL
    {
        INTRA_Entity_Conn m_DbContext = new INTRA_Entity_Conn();

        ConexaoOracle objConexao = new ConexaoOracle();

        protected DataTable geraRelatorioIntervalo(DateTime dtInicio, DateTime dtFim, string forma)
        {
            DataTable dt = new DataTable();
            try
            {
                string inicio = Convert.ToDateTime(dtInicio).ToShortDateString();
                string fim = Convert.ToDateTime(dtFim).ToShortDateString();
                if (forma == "todos")
                {
                    OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("SELECT COD_EMPRS,NUM_RGTRO_EMPRG,NOM_EMPRG,COD_EMAIL_EMPRG,DATA_CADASTRO,VALOR_INGRESSO,QTDE_INGRESSO,QTDE_PARCELAS,FORM_PAGAMENTO,DATA_PAGAMENTO FROM OWN_FUNCESP.CRM_TBL_FESTAAPOSENTADOS WHERE DATA_CANCELAMENTO IS NULL AND to_date(DATA_CADASTRO,'dd/MM/yyyy') BETWEEN to_date('" + inicio + "','dd/MM/yyyy') AND to_date('" + fim + "','dd/MM/yyyy')");
                    adpt.Fill(dt);
                    adpt.Dispose();
                }
                else
                {
                    OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("SELECT COD_EMPRS,NUM_RGTRO_EMPRG,NOM_EMPRG,COD_EMAIL_EMPRG,DATA_CADASTRO,VALOR_INGRESSO,QTDE_INGRESSO,QTDE_PARCELAS,FORM_PAGAMENTO,DATA_PAGAMENTO FROM OWN_FUNCESP.CRM_TBL_FESTAAPOSENTADOS WHERE DATA_CANCELAMENTO IS NULL AND FORM_PAGAMENTO = '" + forma + "' AND to_date(DATA_CADASTRO,'dd/MM/yyyy') BETWEEN to_date('" + inicio + "','dd/MM/yyyy') AND to_date('" + fim + "','dd/MM/yyyy')");
                    adpt.Fill(dt);
                    adpt.Dispose();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            return dt;
        }
        protected DataTable geraRelatorioTudo(string forma)
        {
            DataTable dt = new DataTable();
            try
            {
                if (forma == "todos")
                {
                    OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("SELECT COD_EMPRS,NUM_RGTRO_EMPRG,NOM_EMPRG,COD_EMAIL_EMPRG,DATA_CADASTRO,VALOR_INGRESSO,QTDE_INGRESSO,QTDE_PARCELAS,FORM_PAGAMENTO,DATA_PAGAMENTO FROM OWN_FUNCESP.CRM_TBL_FESTAAPOSENTADOS WHERE DATA_CANCELAMENTO IS NULL");
                    adpt.Fill(dt);
                    adpt.Dispose();
                }
                else
                {
                    OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("SELECT COD_EMPRS,NUM_RGTRO_EMPRG,NOM_EMPRG,COD_EMAIL_EMPRG,DATA_CADASTRO,VALOR_INGRESSO,QTDE_INGRESSO,QTDE_PARCELAS,FORM_PAGAMENTO,DATA_PAGAMENTO FROM OWN_FUNCESP.CRM_TBL_FESTAAPOSENTADOS WHERE DATA_CANCELAMENTO IS NULL AND FORM_PAGAMENTO = '" + forma + "'");
                    adpt.Fill(dt);
                    adpt.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            return dt;
        }
    }
}
