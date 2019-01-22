using IntegWeb.Entidades;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class ArqPatrocinaNotaDebitoDAL
    {

        //public partial class PRE_TBL_ARQ_PAT_DEMONSTRA_DET_view
        //{
        //    public long COD_DEMO_DET { get; set; }
        //    public int COD_ARQ_PAT_DEMO { get; set; }
        //    public string TIP_LANCAMENTO { get; set; }
        //    public string TIP_CRED_DEB { get; set; }
        //    public Nullable<short> COD_EMPRS { get; set; }
        //    public Nullable<long> NUM_RGTRO_EMPRG { get; set; }
        //    public Nullable<int> COD_VERBA { get; set; }
        //    public string DCR_LANCAMENTO { get; set; }
        //    public Nullable<decimal> VLR_LANCAMENTO { get; set; }
        //    public Nullable<long> COD_ARQ_PAT_LINHA { get; set; }
        //    public Nullable<short> IND_DEDUZ_SALDO_FUNDO { get; set; }
        //    //public System.DateTime DTH_INCLUSAO { get; set; }
        //    //public string LOG_INCLUSAO { get; set; }
        //    //public string COD_VERBA_PATROCINA { get; set; }
        //    //public Nullable<System.DateTime> DTH_AUTORIZADO { get; set; }
        //    //public string LOG_AUTORIZADO { get; set; }
        //    //public Nullable<System.DateTime> DTH_IMPORTADO { get; set; }
        //}

        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public PRE_TBL_ARQ_NOTA_DEBITO GetNotaDebito(short sANO_REF, short sMES_REF, short? pCOD_GRUPO_EMPRS)
        {
            IQueryable<PRE_TBL_ARQ_NOTA_DEBITO> query;
            query = from nd in m_DbContext.PRE_TBL_ARQ_NOTA_DEBITO
                    where nd.ANO_REF == sANO_REF &&
                          nd.MES_REF == sMES_REF &&
                          (nd.COD_GRUPO_EMPRS == pCOD_GRUPO_EMPRS || pCOD_GRUPO_EMPRS == null)
                    select nd;

            return query.ToList().FirstOrDefault();

        }

        public PRE_TBL_ARQ_SALDO_FUNDO GetSaldoFundo(short? pCOD_SALDO_FUNDO, short? pCOD_GRUPO_EMPRS)
        {
            IQueryable<PRE_TBL_ARQ_SALDO_FUNDO> query;

            query = from sf in m_DbContext.PRE_TBL_ARQ_SALDO_FUNDO
                    where (sf.COD_SALDO_FUNDO == pCOD_SALDO_FUNDO || pCOD_SALDO_FUNDO == null)
                       && (sf.COD_GRUPO_EMPRS == pCOD_GRUPO_EMPRS || pCOD_GRUPO_EMPRS == null)
                    select sf;

            return query.FirstOrDefault();
        }

        public PRE_TBL_GRUPO_EMPRS GetCodigoGrupoEmprs(short? COD_GRUPO_EMPRS, string GRUPO_PORTAL)
        {
            IQueryable<PRE_TBL_GRUPO_EMPRS> query;

            query = from ge in m_DbContext.PRE_TBL_GRUPO_EMPRS
                    where (ge.COD_GRUPO_EMPRS == COD_GRUPO_EMPRS)
                       || (ge.GRUPO_PORTAL == GRUPO_PORTAL)
                    select ge;

            return query.FirstOrDefault();
        }

        public Resultado DeleteData(int pCOD_NOTA_DEBITO, short pCOD_GRUPO_EMPRS)
        {
            Resultado res = new Resultado();
            var deleta = m_DbContext.PRE_TBL_ARQ_NOTA_DEBITO.Find(pCOD_NOTA_DEBITO, pCOD_GRUPO_EMPRS);
            
            if (deleta != null)
            {

                // Exclui os filhos:
                //deleta.PRE_TBL_ARQ_NOTA_DEBITO.ToList().ForEach(p => m_DbContext.PRE_TBL_ARQ_NOTA_DEBITO.Remove(p));
                //m_DbContext.PRE_TBL_ARQ_NOTA_DEBITO.Remove(deleta);

                m_DbContext.PRE_TBL_ARQ_NOTA_DEBITO.Remove(deleta);
                int rows_deleted = m_DbContext.SaveChanges();
                if (rows_deleted > 0)
                {
                    res.Sucesso("Registro excluído com sucesso.");
                }
            }
            return res;
        }

        public IQueryable<PRE_VW_ARQ_PAT_DEMONSTRATIVO> GetLancamentosDemonstrativo(short? pANO_REF, short? pMES_REF, string pGRUPO_PORTAL)
        {
            IQueryable<PRE_VW_ARQ_PAT_DEMONSTRATIVO> query;
            query = from d in m_DbContext.PRE_VW_ARQ_PAT_DEMONSTRATIVO
                    where (d.GRUPO_PORTAL == pGRUPO_PORTAL)
                       && (d.MES_REF == pMES_REF)
                       && (d.ANO_REF == pANO_REF)
                       // && (d.COD_EMPRS == pCOD_EMPRS || (pCOD_EMPRS == null))
                       // && (d.NUM_RGTRO_EMPRG == pNUM_RGTRO_EMPRG || (pNUM_RGTRO_EMPRG == null))
                       // && (d.COD_VERBA == pCOD_VERBA || (pCOD_VERBA == null))
                       // && (d.TIP_LANCAMENTO == pTIP_LANCAMENTO || pTIP_LANCAMENTO == null)
                       // && (d.COD_ARQ_PAT_LINHA == null)
                       // && (d.DTH_IMPORTADO == null)
                    select d;

            //query = from d in m_DbContext.PRE_VW_ARQ_PAT_DEMONSTRATIVO
            //        join dd in m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET on d.COD_ARQ_PAT_DEMO equals dd.COD_ARQ_PAT_DEMO into leftjoin2
            //        from d2 in leftjoin2.DefaultIfEmpty()
            //        join v in m_DbContext.PRE_TBL_ARQ_PAT_VERBA on d2.COD_VERBA equals v.COD_VERBA into leftjoin
            //        from v2 in leftjoin.DefaultIfEmpty()
            //        where (d.ANO_REF == pANO_REF || pANO_REF == null)
            //           && (d.MES_REF == pMES_REF || pMES_REF == null)
            //           && (d.GRUPO_PORTAL == pGRUPO_PORTAL || pGRUPO_PORTAL == null)
            //           && (v2.COD_EMPRS == d2.COD_EMPRS)
            //        //&& (tb.COD_EMPRS == empresa || empresa == null)
            //        //&& (tb.COD_VERBA == verba || verba == null)
            //        //&& (tb.NUM_RGTRO_EMPRG == matricula || matricula == null)
            //        //&& (sgj.CRED_DEB == tipo || tipo == "0")
            //        select new PRE_TBL_ARQ_PAT_DEMONSTRA_DET_view()
            //        {
            //            COD_DEMO_DET = d2.COD_DEMO_DET,
            //            COD_ARQ_PAT_DEMO = d2.COD_ARQ_PAT_DEMO,
            //            TIP_LANCAMENTO = d2.TIP_LANCAMENTO,
            //            TIP_CRED_DEB = d2.TIP_CRED_DEB,
            //            COD_EMPRS = d2.COD_EMPRS,
            //            NUM_RGTRO_EMPRG = d2.NUM_RGTRO_EMPRG,
            //            COD_VERBA = d2.COD_VERBA,
            //            DCR_LANCAMENTO = d2.DCR_LANCAMENTO,
            //            VLR_LANCAMENTO = d2.VLR_LANCAMENTO,
            //            COD_ARQ_PAT_LINHA = d2.COD_ARQ_PAT_LINHA,
            //            IND_DEDUZ_SALDO_FUNDO = v2.IND_DEDUZ_SALDO_FUNDO
            //            //DTH_INCLUSAO = d2.DTH_INCLUSAO,
            //            //LOG_INCLUSAO = d2.LOG_INCLUSAO,
            //            //COD_VERBA_PATROCINA = d2.COD_VERBA_PATROCINA,
            //            //DTH_AUTORIZADO = d2.DTH_AUTORIZADO,
            //            //LOG_AUTORIZADO = d2.LOG_AUTORIZADO,
            //            //DTH_IMPORTADO = d2.DTH_IMPORTADO
            //        };

            return query;
        }

        internal int GetMaxPk()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_NOTA_DEBITO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_NOTA_DEBITO);
            return maxPK;
        }

        public Resultado SaveData(PRE_TBL_ARQ_NOTA_DEBITO newDebitoNota)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_NOTA_DEBITO.Find(newDebitoNota.COD_NOTA_DEBITO, newDebitoNota.COD_GRUPO_EMPRS);                

                if (atualiza == null)
                {
                    int iMaxPk = GetMaxPk();
                    newDebitoNota.COD_NOTA_DEBITO = iMaxPk + 1;
                    m_DbContext.PRE_TBL_ARQ_NOTA_DEBITO.Add(newDebitoNota);
                }
                //else
                //{
                //    objTb.VLR_DESCONTO = objFilho.VLR_DESCONTO;
                //    objTb.DTH_INCLUSAO = objFilho.DTH_INCLUSAO;
                //    objTb.LOG_INCLUSAO = objFilho.LOG_INCLUSAO;

                //    m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.Add(objTb);
                //}

                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    res.Sucesso("Registro atualizado com sucesso.", newDebitoNota.COD_NOTA_DEBITO);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

        public Resultado SaveData(PRE_TBL_ARQ_SALDO_FUNDO newSaldoFundo)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_SALDO_FUNDO.Find(newSaldoFundo.COD_SALDO_FUNDO, newSaldoFundo.DTH_INCLUSAO);
                if (atualiza != null)
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newSaldoFundo);
                    //m_DbContext.PRE_TBL_ARQ_SALDO_FUNDO.Add(newDebitoNota);
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Saldo atualizado com sucesso.");
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

    }

}
