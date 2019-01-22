using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System.Data.Entity.Validation;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial
{
    public class RatAtuDepJudicialDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public class PRE_TBL_DEPOSITO_JUDIC_view
        {
            public string PASTA { get; set; }
            public decimal? VALOR_BSPS { get; set; }
            public decimal? VALOR_BD { get; set; }
            public decimal? VALOR_CV { get; set; }
            public DateTime? DTH_PAGAMENTO { get; set; }
            public string PLANO { get; set; }
        }


        //public PRE_TBL_DEPOSITO_JUDIC_view GetValorRateio(string pasta, DateTime dataPagamento)
        //{
        //    IQueryable<PRE_TBL_DEPOSITO_JUDIC_view> query;

        //    query = from j in m_DbContext.PRE_TBL_DEPOSITO_JUDIC
        //            from pg in m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO
        //            where j.COD_DEPOSITO_JUDIC == pg.COD_DEPOSITO_JUDIC
        //            && (j.NRO_PASTA == pasta || pasta == null)
        //            && (pg.DTH_PAGAMENTO == dataPagamento || dataPagamento == null)
        //            select new PRE_TBL_DEPOSITO_JUDIC_view()
        //            {
        //                PASTA = j.NRO_PASTA,
        //                VALOR_BSPS = pg.VLR_BSPS,
        //                VALOR_BD = pg.VLR_BD,
        //                VALOR_CV = pg.VLR_CV,
        //                DTH_PAGAMENTO = pg.DTH_PAGAMENTO,
        //                PLANO = j.PLANO
        //            };

        //    return query.FirstOrDefault();
        //}


        public Resultado Inserir(PRE_TBL_JUR_DEP_JUDICIAL obj)
        {
            Resultado res = new Resultado();

            obj.ID_REG = GetMaxPk();

            try
            {

                m_DbContext.PRE_TBL_JUR_DEP_JUDICIAL.Add(obj);
                int row = m_DbContext.SaveChanges();

                if (row > 0)
                {
                    res.Sucesso("Registro inserido com sucesso.");
                }

            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                         .SelectMany(x => x.ValidationErrors)
                         .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return res;
        }

        public IQueryable<PRE_TBL_JUR_DEP_JUDICIAL> exportaRel(DateTime dataGeracao)
        {
            IQueryable<PRE_TBL_JUR_DEP_JUDICIAL> query;

            query = from u in m_DbContext.PRE_TBL_JUR_DEP_JUDICIAL
                    where (u.DAT_GERACAO == dataGeracao)
                    select u;

            return query;
        }

        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_JUR_DEP_JUDICIAL.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public Resultado Deleta(DateTime data)
        {
            Resultado res = new Resultado();
            
            try
            {
               
                var deleta = from a in m_DbContext.PRE_TBL_JUR_DEP_JUDICIAL
                             where a.DAT_GERACAO == data
                             select a;

                foreach (var b in deleta)
                {
                    m_DbContext.PRE_TBL_JUR_DEP_JUDICIAL.Remove(b);
                }

                m_DbContext.SaveChanges();

              
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public Resultado GeraRateio(DateTime datGeracao)
        {
            Resultado res = new Resultado();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                objConexao.AdicionarParametro("p_DtMov", datGeracao);
                bool result = objConexao.ExecutarNonQuery("OWN_FUNCESP.FUN_PRC_JUR_RAT_DEP_JUDICIAL");

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

    }
}
