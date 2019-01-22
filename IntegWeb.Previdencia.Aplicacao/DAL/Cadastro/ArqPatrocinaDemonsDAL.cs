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
    public class ArqPatrocinaDemonstrativoDAL
    {

        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<PRE_VW_ARQ_PAT_DEMONSTRATIVO> GetData(int startRowIndex, int maximumRows, string pGRUPO_PORTAL, string pCOD_EMPRS, short pMES_REF, short pANO_REF, string sortParameter)
        {
            return GetWhere(pGRUPO_PORTAL, pCOD_EMPRS, pMES_REF, pANO_REF)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_VW_ARQ_PAT_DEMONSTRATIVO> GetWhere(string pGRUPO_PORTAL, string pCOD_EMPRS, short pMES_REF, short pANO_REF)
        {
            //m_DbContext.Configuration.ValidateOnSaveEnabled

            //short[] aCOD_EMPRS = pCOD_EMPRS.To;
            string[] sCOD_EMPRS = pCOD_EMPRS.Split(',');

            //short?[] shortValue = new short?[] { pCOD_EMPRS };
            short?[] shortValue = Array.ConvertAll(sCOD_EMPRS, Util.TryParseShort);

            IQueryable<PRE_VW_ARQ_PAT_DEMONSTRATIVO> query;
            query = from d in m_DbContext.PRE_VW_ARQ_PAT_DEMONSTRATIVO
                    where (d.GRUPO_PORTAL == pGRUPO_PORTAL)
                       && (shortValue.Contains(d.COD_EMPRS))
                       && (d.MES_REF == pMES_REF)
                       && (d.ANO_REF == pANO_REF)
                       //&& (d.COD_ARQ_PAT_LINHA == null)
                       && (d.TIP_LANCAMENTO == "M")
                    select d;
            return query;
        }

        public int GetDataCount(string pGRUPO_PORTAL, string pCOD_EMPRS, short pMES_REF, short pANO_REF)
        {
            return GetWhere(pGRUPO_PORTAL, pCOD_EMPRS, pMES_REF, pANO_REF).SelectCount();
        }

        //public List<PRE_VW_ARQ_PAT_DEMONSTRATIVO> GetData2(int startRowIndex, int maximumRows, string pCOD_ARQ_PATS, string sortParameter)
        //{
        //    return GetWhere2(pCOD_ARQ_PATS)
        //           .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        //}

        //public IQueryable<PRE_VW_ARQ_PAT_DEMONSTRATIVO> GetWhere2(string pCOD_ARQ_PATS)
        //{

        //    string[] sArquivos = { };
        //    Nullable<long>[] aArquivos = { };
        //    if (pCOD_ARQ_PATS != null)
        //    {
        //        sArquivos = pCOD_ARQ_PATS.Split(',');
        //        //aArquivos = Array.ConvertAll(sArquivos, s => long.TryParse(s,));
        //        aArquivos = Array.ConvertAll(sArquivos, Util.TryParseLong);
        //    }

        //    IQueryable<PRE_VW_ARQ_PAT_DEMONSTRATIVO> query;
        //    query = from d in m_DbContext.PRE_VW_ARQ_PAT_DEMONSTRATIVO
        //            where (aArquivos.Contains(d.COD_ARQ_PAT))
        //               && (d.TIP_LANCAMENTO == "M")
        //            select d;
        //    return query;
        //}

        //public int GetDataCount2(string pCOD_ARQ_PATS)
        //{
        //    return GetWhere2(pCOD_ARQ_PATS).SelectCount();
        //}

        public PRE_TBL_ARQ_PAT_DEMONSTRA_DET GetLancamento(long pCOD_DEMO_DET, string pTIP_LANCAMENTO)
        {
            IQueryable<PRE_TBL_ARQ_PAT_DEMONSTRA_DET> query;
            query = from d in m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET
                    where (d.COD_DEMO_DET == pCOD_DEMO_DET)
                       && (d.TIP_LANCAMENTO == pTIP_LANCAMENTO)
                    select d;
            return query.FirstOrDefault();
        }

        public PRE_VW_ARQ_PAT_DEMONSTRATIVO GetLancamento(string pGRUPO_PORTAL, short pMES_REF, short pANO_REF, short? pCOD_EMPRS = null, int? pNUM_RGTRO_EMPRG = null, int? pCOD_VERBA = null, string pTIP_LANCAMENTO = null)
        {
            //m_DbContext.Configuration.ValidateOnSaveEnabled

            IQueryable<PRE_VW_ARQ_PAT_DEMONSTRATIVO> query;
            query = from d in m_DbContext.PRE_VW_ARQ_PAT_DEMONSTRATIVO
                    where (d.GRUPO_PORTAL == pGRUPO_PORTAL)
                       && (d.MES_REF == pMES_REF)
                       && (d.ANO_REF == pANO_REF)
                       && (d.COD_EMPRS == pCOD_EMPRS || (pCOD_EMPRS == null))
                       && (d.NUM_RGTRO_EMPRG == pNUM_RGTRO_EMPRG || (pNUM_RGTRO_EMPRG == null))
                       && (d.COD_VERBA == pCOD_VERBA || (pCOD_VERBA == null))
                       && (d.TIP_LANCAMENTO == pTIP_LANCAMENTO || pTIP_LANCAMENTO == null)
                        //&& (d.COD_ARQ_PAT_LINHA == null)
                       && (d.DTH_IMPORTADO == null)
                    select d;
            return query.FirstOrDefault();
        }

        public List<PRE_VW_ARQ_PAT_DEMONSTRATIVO> GetLancamentos(string pGRUPO_PORTAL, short pMES_REF, short pANO_REF, short pCOD_EMPRS)
        {
            //m_DbContext.Configuration.ValidateOnSaveEnabled

            IQueryable<PRE_VW_ARQ_PAT_DEMONSTRATIVO> query;
            query = from d in m_DbContext.PRE_VW_ARQ_PAT_DEMONSTRATIVO
                    where (d.GRUPO_PORTAL == pGRUPO_PORTAL)
                       && (d.COD_EMPRS == pCOD_EMPRS)
                       && (d.MES_REF == pMES_REF)
                       && (d.ANO_REF == pANO_REF)
                        //&& (d.COD_ARQ_PAT_LINHA == null)
                       && (d.TIP_LANCAMENTO == "M")
                       && (d.DTH_IMPORTADO == null)
                    select d;
            return query.ToList();
        }

        //public Resultado InsertData(PRE_TBL_ARQ_PAT_DEMONSTRA newLancamento)
        //{
        //    Resultado res = new Entidades.Resultado();
        //    try
        //    {
        //        //Novo registro
        //        if (newLancamento.COD_ARQ_PAT_DEMO == 0)
        //        {
        //            int iMaxPk_DEMO = GetMaxPk_DEMO();
        //            newLancamento.COD_ARQ_PAT_DEMO = iMaxPk_DEMO + 1;
        //            if (newLancamento.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.FirstOrDefault() != null)
        //            {
        //                long iMaxPk_DEMO_DETALHE = GetMaxPk_DEMO_DETALHE();
        //                newLancamento.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.FirstOrDefault().COD_DEMO_DET = iMaxPk_DEMO_DETALHE + 1;
        //            }
        //        }

        //        m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA.Add(newLancamento);
        //        res.Sucesso("INSERT", newLancamento.COD_ARQ_PAT_DEMO);

        //        SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Erro(Util.GetInnerException(ex));
        //    }
        //    return res;
        //}

        public Resultado SaveData(PRE_TBL_ARQ_PAT_DEMONSTRA newLancamento)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                PRE_TBL_ARQ_PAT_DEMONSTRA atualiza = null;

                if (newLancamento.COD_ARQ_PAT_DEMO == 0)
                {
                    atualiza = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA.FirstOrDefault(p => p.ANO_REF == newLancamento.ANO_REF &&
                                                                                         p.MES_REF == newLancamento.MES_REF &&
                                                                                         (p.GRUPO_PORTAL == newLancamento.GRUPO_PORTAL || newLancamento.GRUPO_PORTAL == null));
                }
                else
                {
                    atualiza = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA.Find(newLancamento.COD_ARQ_PAT_DEMO);
                }

                if (atualiza == null)  //Novo registro
                {

                    if (newLancamento.COD_ARQ_PAT_DEMO == 0)
                    {
                        int iMaxPk_DEMO = GetMaxPk_DEMO();
                        newLancamento.COD_ARQ_PAT_DEMO = iMaxPk_DEMO + 1;
                        if (newLancamento.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.FirstOrDefault() != null)
                        {
                            long iMaxPk_DEMO_DETALHE = GetMaxPk_DEMO_DETALHE();
                            newLancamento.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.FirstOrDefault().COD_DEMO_DET = iMaxPk_DEMO_DETALHE + 1;
                        }
                    }
                    m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA.Add(newLancamento);
                    res.Sucesso("INSERT", newLancamento.COD_ARQ_PAT_DEMO);
                }
                else   //Atualiza registro
                {
                    newLancamento.COD_ARQ_PAT_DEMO = atualiza.COD_ARQ_PAT_DEMO;
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newLancamento);
                    res.Sucesso("UPDATE", newLancamento.COD_ARQ_PAT_DEMO);
                }
                SaveChanges();
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado InsertData(PRE_TBL_ARQ_PAT_DEMONSTRA_DET newLancamento)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                //Novo registro
                if (newLancamento.TIP_LANCAMENTO == "A" && newLancamento.VLR_LANCAMENTO == 0)
                {
                    res.Sucesso("ACERTO ZERADO", 0); // Não inserir Acerto zerado.
                    return res;
                }

                if (newLancamento.COD_DEMO_DET == 0)
                {
                    long iMaxPk_DEMO = GetMaxPk_DEMO_DETALHE();
                    newLancamento.COD_DEMO_DET = iMaxPk_DEMO + 1;
                }
                m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.Add(newLancamento);
                res.Sucesso("INSERT", newLancamento.COD_ARQ_PAT_DEMO);
                
                SaveChanges();
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado SaveData(PRE_TBL_ARQ_PAT_DEMONSTRA_DET newLancamento)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                PRE_TBL_ARQ_PAT_DEMONSTRA_DET atualiza = null;

                
                if (newLancamento.COD_DEMO_DET == 0)
                {
                    atualiza = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.FirstOrDefault(p => p.COD_EMPRS == newLancamento.COD_EMPRS &&
                                                                                             p.NUM_RGTRO_EMPRG == newLancamento.NUM_RGTRO_EMPRG &&
                                                                                             p.COD_VERBA == newLancamento.COD_VERBA &&
                                                                                             p.TIP_LANCAMENTO == newLancamento.TIP_LANCAMENTO &&
                                                                                             (p.COD_ARQ_PAT_DEMO == newLancamento.COD_ARQ_PAT_DEMO || newLancamento.COD_ARQ_PAT_DEMO == 0) &&
                                                                                             (p.COD_ARQ_PAT_LINHA == newLancamento.COD_ARQ_PAT_LINHA || newLancamento.COD_ARQ_PAT_LINHA == null));
                }
                else
                {
                    atualiza = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.Find(newLancamento.COD_DEMO_DET, newLancamento.TIP_LANCAMENTO);
                }

                if (atualiza == null)  //Novo registro
                {

                    if (newLancamento.TIP_LANCAMENTO == "A" && newLancamento.VLR_LANCAMENTO == 0)
                    {
                        res.Sucesso("ACERTO ZERADO", 0); // Não inserir Acerto zerado.
                        return res;
                    }

                    if (newLancamento.COD_DEMO_DET == 0)
                    {
                        long iMaxPk_DEMO = GetMaxPk_DEMO_DETALHE();
                        newLancamento.COD_DEMO_DET = iMaxPk_DEMO + 1;
                    }
                    m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.Add(newLancamento);
                    res.Sucesso("INSERT", newLancamento.COD_ARQ_PAT_DEMO);
                }
                else   //Atualiza registro
                {
                    newLancamento.COD_ARQ_PAT_DEMO = atualiza.COD_ARQ_PAT_DEMO;
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newLancamento);
                    res.Sucesso("UPDATE", newLancamento.COD_ARQ_PAT_DEMO);
                }
                SaveChanges();
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        //public Resultado UpdateVLR_ACERTO(long pCOD_ARQ_PAT_DEMO, decimal pVLR_ACERTO, string pLOG_INCLUSAO)
        //{
        //    Resultado res = new Entidades.Resultado();
        //    try
        //    {
        //        var atualiza = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRATIVO.FirstOrDefault(p => p.COD_ARQ_PAT_DEMO == pCOD_ARQ_PAT_DEMO &&
        //                                                                                     p.TIP_LANCAMENTO == "A");


        //        if (atualiza == null)
        //        {

        //            var lancamento_ref = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRATIVO.FirstOrDefault(p => p.COD_ARQ_PAT_DEMO == pCOD_ARQ_PAT_DEMO);

        //            PRE_TBL_ARQ_PAT_DEMONSTRATIVO newLancamento = new PRE_TBL_ARQ_PAT_DEMONSTRATIVO();

        //            long iMaxPk_DEMO = GetMaxPk_DEMO();

        //            DateTime dtNow = DateTime.Now;

        //            newLancamento.COD_ARQ_PAT_DEMO = lancamento_ref.COD_ARQ_PAT_DEMO;
        //            newLancamento.COD_ARQ_PAT_LINHA = lancamento_ref.COD_ARQ_PAT_LINHA;
        //            newLancamento.TIP_LANCAMENTO = "A";
        //            newLancamento.TIP_CRED_DEB = lancamento_ref.TIP_CRED_DEB;
        //            newLancamento.COD_EMPRS = lancamento_ref.COD_EMPRS;
        //            newLancamento.NUM_RGTRO_EMPRG = lancamento_ref.NUM_RGTRO_EMPRG;
        //            newLancamento.ANO_REF = lancamento_ref.ANO_REF;
        //            newLancamento.MES_REF = lancamento_ref.MES_REF;
        //            newLancamento.COD_VERBA = lancamento_ref.COD_VERBA;
        //            newLancamento.VLR_LANCAMENTO = pVLR_ACERTO;
        //            newLancamento.LOG_INCLUSAO = Util.String2Limit(pLOG_INCLUSAO, 0, 30);
        //            newLancamento.DTH_INCLUSAO = dtNow;

        //            m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRATIVO.Add(newLancamento);
        //            SaveChanges();
        //            res.Sucesso("INSERT", 1);
        //        }
        //        else
        //        {
        //            //newLancamento.COD_ARQ_PAT_DEMO = atualiza.COD_ARQ_PAT_DEMO;
        //            atualiza.VLR_LANCAMENTO = pVLR_ACERTO;
        //            m_DbContext.Entry(atualiza).CurrentValues.SetValues(atualiza);
        //            SaveChanges();
        //            res.Sucesso("UPDATE", 2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Erro(Util.GetInnerException(ex));
        //    }

        //    return res;
        //}

        internal Resultado SaveChanges()
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                int rows_updated = m_DbContext.SaveChanges();
                //if (rows_updated > 0)
                //{
                res.Sucesso("Carga realizada com sucesso.");
                //}
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

        internal int GetMaxPk_DEMO()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_ARQ_PAT_DEMO);
            return maxPK;
        }

        internal long GetMaxPk_DEMO_DETALHE()
        {
            long maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_DEMO_DET);
            return maxPK;
        }

        public Resultado DeleteData(long pCOD_DEMO_DET)
        {
            Resultado res = new Resultado();
            var deleta = m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.Where(d => d.COD_DEMO_DET == pCOD_DEMO_DET);

            if (deleta != null)
            {
                deleta.ToList().ForEach(d => { m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.Remove(d); });

                int rows_deleted = m_DbContext.SaveChanges();
                if (rows_deleted > 0)
                {
                    res.Sucesso("Registros excluídos com sucesso.");
                }
            }
            return res;
        }

        internal Resultado DeleteDemonstra(long pCOD_ARQ_PAT_DEMO)
        {
            Resultado res = new Entidades.Resultado();
            try
            {

                //Exclui lançamentos a partir das linhas do arquivo :
                m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PAT_DEMONSTRA_DET   " +
                                                       " WHERE COD_ARQ_PAT_LINHA IN (SELECT L.COD_ARQ_PAT_LINHA " +
                                                       "                               FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_LINHA L " +
                                                       "                              WHERE L.COD_ARQ_PAT = " + pCOD_ARQ_PAT_DEMO.ToString() + ")", 0);
                

                //m_DbContext.SaveChanges();
                //res.Sucesso("Registro não localizado ou recadastro já efetuado.");
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

        public PRE_TBL_ARQ_PAT_VERBA GetVerbaPatrocinadora(short? pCOD_EMPRS, int? pCOD_VERBA, string pCOD_VERBA_PATROCINA)
        {
            short?[] shortValue = new short?[] { pCOD_EMPRS };

            IQueryable<PRE_TBL_ARQ_PAT_VERBA> query;
            query = from e in m_DbContext.PRE_TBL_ARQ_PAT_VERBA.AsNoTracking()
                    //where (e.COD_EMPRS == pCOD_EMPRS)
                    where (shortValue.Contains(e.COD_EMPRS))
                    && (e.COD_VERBA == pCOD_VERBA || pCOD_VERBA == null)
                    && (e.COD_VERBA_PATROCINA == pCOD_VERBA_PATROCINA || pCOD_VERBA_PATROCINA == null)
                    //&& (e.COD_VERBA_PRODUTO == pCOD_VERBA_PRODUTO || pCOD_VERBA_PRODUTO == null)
                    select e;

            return query.FirstOrDefault();
        }

        public TB_SCR_SUBGRUPO_FINANC_VERBA GetTipoLancamento(int? pCOD_VERBA)
        {
            IQueryable<TB_SCR_SUBGRUPO_FINANC_VERBA> query;
            query = from s in m_DbContext.TB_SCR_SUBGRUPO_FINANC_VERBA
                    where (s.FL_AUTOPATROC == 0)
                       && (s.COD_VERBA == pCOD_VERBA)
                    select s;

            return query.ToList().FirstOrDefault();
        }



    }

}
