using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.SqlClient;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL.Tesouraria
{
    public class FIN_TBL_RES_ABERT_FINANCEIRA_view
    {
        public decimal ID_REG { get; set; }
        public Nullable<decimal> COD_EMPRS { get; set; }
        public string NOM_EMPRS { get; set; }
        public Nullable<int> ANO_REF { get; set; }
        public Nullable<int> MES_REF { get; set; }
        public string NATUREZA { get; set; }
        public Nullable<decimal> VALOR_PART { get; set; }
        public Nullable<decimal> VALOR_CRED_BRUTO { get; set; }
        public Nullable<decimal> IMPOSTO { get; set; }
        public Nullable<decimal> VALOR_CRED_LIQ { get; set; }
        public string APROVACAO { get; set; }
        public string USU_APROVACAO { get; set; }
        public Nullable<System.DateTime> DAT_APROVACAO { get; set; }
    
    }

    public class FIN_TBL_RES_ABERT_FINANCEIRA_view2
    {

        public string NOM_EMPRS { get; set; }
        public string NATUREZA { get; set; }
        public Nullable<decimal> VALOR_PART { get; set; }
        public Nullable<decimal> VALOR_CRED_BRUTO { get; set; }
        public Nullable<decimal> IMPOSTO { get; set; }
        public Nullable<decimal> VALOR_CRED_LIQ { get; set; }
    }
    public class FIN_TBL_RES_ABERT_FINANCEIRA_view3
    {
        public string CODIGO { get; set; }
        public string EMPRESA { get; set; }
        public string NATUREZA { get; set; }
        public double VALOR { get; set; }
        public double BRUTO { get; set; }
        public double IMPOSTO { get; set; }
        public double LIQUIDO { get; set; }
    }

    public class AberturaFinanceiraDAL
    {

        public EntitiesConn m_DbContext = new EntitiesConn();

        public Resultado InsereAberturaFinanceira(string codEmp, string codPlano, int mes, int ano)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            Resultado res = new Resultado();
            try
            {
                objConexao.AdicionarParametro("codEmp", codEmp);
                objConexao.AdicionarParametro("codPlan", codPlano);
                objConexao.AdicionarParametro("mes", mes);
                objConexao.AdicionarParametro("ano", ano);
                bool result = objConexao.ExecutarNonQuery("own_funcesp.FUN_PKG_ABERTURA_FINANCEIRA.prc_insere_abert_fin_emp_plano");

                if (result == true)
                {
                    res.Sucesso("Processamento Feito com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            finally
            {
                objConexao.Dispose();
            }

            return res;
        }

        public Resultado InsereAberturaFinanceiraCPFL(int mes, int ano)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            Resultado res = new Resultado();
            try
            {
                objConexao.AdicionarParametro("mes", mes);
                objConexao.AdicionarParametro("ano", ano);
                bool result = objConexao.ExecutarNonQuery("own_funcesp.FUN_PKG_ABERTURA_FINANCEIRA.prc_insere_abert_fin_cpfl");

                if (result == true)
                {
                    res.Sucesso("Processamento Feito com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            finally
            {
                objConexao.Dispose();
            }

            return res;
        }

        public Resultado InsereResumoAberturaFinanceira(string mes, string ano)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            Resultado res = new Resultado();

            try
            {
                objConexao.AdicionarParametro("vMes", mes);
                objConexao.AdicionarParametro("vAno", ano);

                bool result = objConexao.ExecutarNonQuery("own_funcesp.FUN_PKG_ABERTURA_FINANCEIRA.prc_gera_res_abert_fin");

                if (result == true)
                {
                    res.Sucesso("Processamento Feito com Sucesso");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public List<FIN_TBL_RES_ABERT_FINANCEIRA_view> GetData(int startRowIndex, int maximumRows, int? mes, int? ano, string sortParameter)
        {
            return GetWhere(mes, ano).GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<FIN_TBL_RES_ABERT_FINANCEIRA_view> GetWhere(int? mes, int? ano)
        {
            IQueryable<FIN_TBL_RES_ABERT_FINANCEIRA_view> query = from abert in m_DbContext.FIN_TBL_RES_ABERT_FINANCEIRA
                                                                  from emp in m_DbContext.EMPRESA
                                                                  where (abert.COD_EMPRS == emp.COD_EMPRS)
                                                                   && (abert.MES_REF == mes || mes == null)
                                                                   && (abert.ANO_REF == ano || ano == null)
                                                                  select new FIN_TBL_RES_ABERT_FINANCEIRA_view()
                                                                  {
                                                                      ID_REG = abert.ID_REG,
                                                                      COD_EMPRS = abert.COD_EMPRS,
                                                                      NOM_EMPRS = emp.NOM_ABRVO_EMPRS,
                                                                      ANO_REF = abert.ANO_REF, 
                                                                      MES_REF = abert.MES_REF,
                                                                      NATUREZA = abert.NATUREZA,
                                                                      VALOR_PART = abert.VALOR_PART,
                                                                      VALOR_CRED_BRUTO = abert.VALOR_CRED_BRUTO,
                                                                      IMPOSTO = abert.IMPOSTO,
                                                                      VALOR_CRED_LIQ = abert.VALOR_CRED_LIQ, 
                                                                      APROVACAO = abert.APROVACAO,
                                                                      USU_APROVACAO = abert.USU_APROVACAO,
                                                                      DAT_APROVACAO = abert.DAT_APROVACAO
                                                                  };

            return query;
        }

        public int GetDataCount(int? mes, int? ano)
        {
            return GetWhere(mes, ano).Count();
        }

        public Resultado AprovacaoAberturaFinanceira(int idReg, string usuario)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.FIN_TBL_RES_ABERT_FINANCEIRA.FirstOrDefault(abert => abert.ID_REG == idReg);

                atualiza.APROVACAO = "S";
                atualiza.USU_APROVACAO = usuario;
                atualiza.DAT_APROVACAO = DateTime.Now;

                int rows_update = m_DbContext.SaveChanges();

                if (rows_update > 0)
                {
                    res.Sucesso("Aprovado");
                }

            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public int GetDataCountAprovados(int empresa, int? mes, int? ano)
        {
            IQueryable<FIN_TBL_RES_ABERT_FINANCEIRA> query = from abert in m_DbContext.FIN_TBL_RES_ABERT_FINANCEIRA
                                                             where (abert.MES_REF == mes || mes == null)
                                                              && (abert.ANO_REF == ano || ano == null)
                                                              && (abert.COD_EMPRS == empresa)
                                                              && (abert.APROVACAO == "S")
                                                             select abert;

            return query.Count();

        }

        public int GetDataCountEmpresa(int empresa, int? mes, int? ano)
        {
            IQueryable<FIN_TBL_RES_ABERT_FINANCEIRA> query = from abert in m_DbContext.FIN_TBL_RES_ABERT_FINANCEIRA
                                                             where (abert.MES_REF == mes || mes == null)
                                                              && (abert.ANO_REF == ano || ano == null)
                                                              && (abert.COD_EMPRS == empresa)
                                                             select abert;

            return query.Count();
        }

        public List<FIN_TBL_RES_ABERT_FINANCEIRA_view2> GetDataExportar(int? mes, int? ano)
        {
            IQueryable<FIN_TBL_RES_ABERT_FINANCEIRA_view2> query = from abert in m_DbContext.FIN_TBL_RES_ABERT_FINANCEIRA
                                                                  from emp in m_DbContext.EMPRESA
                                                                  where (abert.COD_EMPRS == emp.COD_EMPRS)
                                                                   && (abert.MES_REF == mes || mes == null)
                                                                   && (abert.ANO_REF == ano || ano == null)
                                                                   select new FIN_TBL_RES_ABERT_FINANCEIRA_view2()
                                                                  {

                                                                      NOM_EMPRS = emp.NOM_ABRVO_EMPRS,
                                                                      NATUREZA = abert.NATUREZA,
                                                                      VALOR_PART = abert.VALOR_PART,
                                                                      VALOR_CRED_BRUTO = abert.VALOR_CRED_BRUTO,
                                                                      IMPOSTO = abert.IMPOSTO,
                                                                      VALOR_CRED_LIQ = abert.VALOR_CRED_LIQ
                                                                  
                                                                  };

            return query.ToList();

        
        }
        public List<FIN_TBL_RES_ABERT_FINANCEIRA_view3> GetDataConsolidado(int? pMes, int? pAno)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            FIN_TBL_RES_ABERT_FINANCEIRA_view3 reg = new FIN_TBL_RES_ABERT_FINANCEIRA_view3();
            List<FIN_TBL_RES_ABERT_FINANCEIRA_view3> result = new List<FIN_TBL_RES_ABERT_FINANCEIRA_view3>();
            DataTable dt = new DataTable();
            try
            {
                StringBuilder querysql = new StringBuilder();
                querysql.Append(" SELECT emp.COD_EMPRS,emp.NOM_ABRVO_EMPRS,abert.NATUREZA,abert.VALOR_PART,abert.VALOR_CRED_BRUTO,abert.IMPOSTO,abert.VALOR_CRED_LIQ ");
                querysql.Append(" FROM FIN_TBL_RES_ABERT_FINANCEIRA abert inner join EMPRESA emp on abert.COD_EMPRS = emp.COD_EMPRS ");
                querysql.Append(" WHERE ");
                querysql.Append(" MES_REF = " + pMes + " and ANO_REF = " + pAno);

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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    reg = new FIN_TBL_RES_ABERT_FINANCEIRA_view3();
                    reg.CODIGO = dt.Rows[i]["COD_EMPRS"].ToString();
                    reg.EMPRESA = dt.Rows[i]["NOM_ABRVO_EMPRS"].ToString();
                    reg.NATUREZA = dt.Rows[i]["NATUREZA"].ToString();
                    reg.VALOR = Convert.ToDouble(dt.Rows[i]["VALOR_PART"].ToString());
                    reg.BRUTO = Convert.ToDouble(dt.Rows[i]["VALOR_CRED_BRUTO"].ToString());
                    reg.IMPOSTO = Convert.ToDouble(dt.Rows[i]["IMPOSTO"].ToString());
                    reg.LIQUIDO = Convert.ToDouble(dt.Rows[i]["VALOR_CRED_LIQ"].ToString());
                    result.Add(reg);
                }
            }
            return result;
        }
    }
}
