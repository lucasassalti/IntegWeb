using IntegWeb.Entidades;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.OracleClient;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Cadastro
{
    public class ArqPatrocinadoraRepasseDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        #region .: Views :.

        public class PRE_TBL_ARQ_ENV_REPASSE_View
        {
            public int COD_ARQ_ENV_REPASSE { get; set; }
            public string DCR_ARQ_ENV_REPASSE { get; set; }
            public Nullable<short> ANO_REF { get; set; }
            public Nullable<short> MES_REF { get; set; }
            public Nullable<short> COD_GRUPO_EMPRS { get; set; }
            public Nullable<short> COD_ARQ_AREA { get; set; }
            public Nullable<int> COD_ARQ_ENVIO { get; set; }
            public DateTime DTH_INCLUSAO { get; set; }
            public string LOG_INCLUSAO { get; set; }
            public string DCR_ARQ_STATUS { get; set; }
            public short? COD_ARQ_STATUS { get; set; }
            public bool READ_ONLY
            {
                get
                {
                    switch (COD_ARQ_STATUS ?? 1)
                    {
                        case 1:
                        case 4:
                            return false;
                        default:
                            return true;
                    }                    
                }
            }            
        }

        public class PRE_TBL_ARQ_ENVIO_HIST_View
        {
            //private short? cod_arq_status;
            public long? COD_ARQ_ENVIO { get; set; }
            public short? COD_ARQ_STATUS { get; set; }
            public string DCR_ARQ_STATUS { get; set; }
            public DateTime DTH_INCLUSAO { get; set; }
        }

        public class PRE_TBL_ARQ_ENV_REPASSE_LINHA_View
        {
            public long COD_ARQ_ENV_REP_LINHA { get; set; }
            public int COD_ARQ_ENV_REPASSE { get; set; }
            public Nullable<short> COD_EMPRS { get; set; }
            public Nullable<int> NUM_RGTRO_EMPRG { get; set; }
            public Nullable<int> COD_VERBA { get; set; }
            public string COD_VERBA_PATROCINA { get; set; }
            public Nullable<decimal> VLR_PERCENTUAL { get; set; }
            public Nullable<decimal> VLR_DESCONTO { get; set; }
            public DateTime DTH_INCLUSAO { get; set; }
            public string LOG_INCLUSAO { get; set; }
            public Nullable<DateTime> DTH_EXCLUSAO { get; set; }
            public string LOG_EXCLUSAO { get; set; }
            public string DCR_VERBA { get; set; }
            public string TIPO { get; set; }
        }

        public class ATT_VIEW_VERBA
        {
            public int COD_VERBA { get; set; }
            public string DCR_VERBA { get; set; }
        }

        public class FCESP_GRUPO_EMP_View
        {
            public string GRUPO { get; set; }
            public int? EMPRESA { get; set; }
        }

        //public class PRE_TBL_ARQ_AREA_View
        //{
        //    public short COD_ARQ_AREA { get; set; }
        //    public string DCR_ARQ_AREA { get; set; }
        //    public string DCR_ARQ_SUB_AREA { get; set; }
        //    public string DCR_ARQ_C_AREA_SUB
        //    {
        //        get
        //        {
        //            return String.Format("{0}    {1}", DCR_ARQ_AREA, DCR_ARQ_SUB_AREA);
        //        }
        //    }
        //}

        #endregion

        #region .: Pesquisa :.
        public List<PRE_TBL_ARQ_ENV_REPASSE_View> GetData(int startRowIndex, int maximumRows, short? mes, short? ano, short? grupo, int? status, string referencia, short? area, string sortParameter)
        {
            return GetWhere(mes, ano, grupo, status, referencia, area)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int GetDataCount(short? mes, short? ano,  short? grupo, int? status, string referencia, short? area)
        {
            return GetWhere(mes, ano, grupo, status, referencia, area).SelectCount();
        }

        public IQueryable<PRE_TBL_ARQ_ENV_REPASSE_View> GetWhere(short? mes, short? ano, short? grupo, int? status, string referencia, short? area)
         {
            IQueryable<PRE_TBL_ARQ_ENV_REPASSE_View> query;

                    //join d in
                    //    (from dd in m_DbContext.PRE_TBL_ARQ_ENVIO_HIST
                    //     join s in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS on dd.COD_ARQ_STATUS equals s.COD_ARQ_STATUS
                    //     group dd by new { dd.COD_ARQ_ENVIO, dd.COD_ARQ_STATUS, s.DCR_ARQ_STATUS } into g
                    //     select new PRE_TBL_ARQ_ENVIO_HIST_View()
                    //     {
                    //         COD_ARQ_ENVIO = g.Key.COD_ARQ_ENVIO,
                    //         COD_ARQ_STATUS = g.Key.COD_ARQ_STATUS,
                    //         DCR_ARQ_STATUS = g.Key.DCR_ARQ_STATUS,
                    //         DTH_INCLUSAO = g.Max(x => x.DTH_INCLUSAO)
                    //     }
                    //     ) on tb.COD_ARQ_ENVIO equals d.COD_ARQ_ENVIO
                    // into leftjoin

            query = from tb in m_DbContext.PRE_TBL_ARQ_ENV_REPASSE
                    join d in 
                        (from hst in m_DbContext.PRE_TBL_ARQ_ENVIO_HIST
                         join s in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS on hst.COD_ARQ_STATUS equals s.COD_ARQ_STATUS
                        select new PRE_TBL_ARQ_ENVIO_HIST_View()
                        {
                            COD_ARQ_ENVIO = hst.COD_ARQ_ENVIO,
                            COD_ARQ_STATUS = hst.COD_ARQ_STATUS,
                            DCR_ARQ_STATUS = s.DCR_ARQ_STATUS,
                            DTH_INCLUSAO = hst.DTH_INCLUSAO
                        }) on tb.COD_ARQ_ENVIO equals d.COD_ARQ_ENVIO
                    into leftjoin from dd in leftjoin.DefaultIfEmpty()
                    where (dd.DTH_INCLUSAO == m_DbContext.PRE_TBL_ARQ_ENVIO_HIST.Where(h => h.COD_ARQ_ENVIO == tb.COD_ARQ_ENVIO).Max(h => h.DTH_INCLUSAO) || dd.COD_ARQ_ENVIO == null)
                     && (tb.COD_ARQ_AREA == area || area == null)
                     && (tb.MES_REF == mes || mes == null)
                     && (tb.ANO_REF == ano || ano == null)
                     && (tb.COD_GRUPO_EMPRS == grupo || grupo == null)
                     && (dd.COD_ARQ_STATUS == status || status == null || (dd.COD_ARQ_STATUS == null && status == 1))
                     && (tb.DCR_ARQ_ENV_REPASSE.Equals(referencia) || referencia == null || referencia == "" || tb.DCR_ARQ_ENV_REPASSE.StartsWith(referencia) || tb.DCR_ARQ_ENV_REPASSE.Contains(referencia))
                    select new PRE_TBL_ARQ_ENV_REPASSE_View()
                    {
                        COD_ARQ_ENV_REPASSE = tb.COD_ARQ_ENV_REPASSE,
                        DCR_ARQ_ENV_REPASSE = tb.DCR_ARQ_ENV_REPASSE,
                        ANO_REF = tb.ANO_REF,
                        MES_REF = tb.MES_REF,
                        COD_GRUPO_EMPRS = tb.COD_GRUPO_EMPRS,
                        COD_ARQ_AREA = tb.COD_ARQ_AREA,
                        COD_ARQ_ENVIO = tb.COD_ARQ_ENVIO,
                        DTH_INCLUSAO = tb.DTH_INCLUSAO,
                        LOG_INCLUSAO = tb.LOG_INCLUSAO,
                        COD_ARQ_STATUS = dd.COD_ARQ_STATUS ?? 1,
                        DCR_ARQ_STATUS = dd.DCR_ARQ_STATUS ?? "Novo"
                    };
            return query;
        }
        #endregion

        #region .: Detalhes :.
        public List<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> GetDataDetalhes(int startRowIndex, int maximumRows, int codigo, short? empresa, int? verba, long? matricula, string tipo, string sortParameter)
        {
            List<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> lstRepasseLinha = new List<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View>();
            lstRepasseLinha = GetWhereDetalhes(codigo, empresa, verba, matricula, tipo).GetData(startRowIndex, maximumRows, sortParameter).ToList();

            IEnumerable<ATT_VIEW_VERBA> IEnum = m_DbContext.Database.SqlQuery<ATT_VIEW_VERBA>("SELECT COD_VERBA, DCR_VERBA FROM ATT.VERBA", 0);

            //lstRepasseLinha.ForEach(x => { x.DCR_VERBA = IEnum.FirstOrDefault(v => v.COD_VERBA == x.COD_VERBA).DCR_VERBA; });

            foreach (PRE_TBL_ARQ_ENV_REPASSE_LINHA_View linha_repasse in lstRepasseLinha)
            {
                if (linha_repasse.COD_VERBA != null)
                {
                    ATT_VIEW_VERBA VIEW_VERBA = IEnum.FirstOrDefault(v => v.COD_VERBA == linha_repasse.COD_VERBA);
                    linha_repasse.DCR_VERBA = (VIEW_VERBA != null) ? VIEW_VERBA.DCR_VERBA : "";
                }
            }            

            return lstRepasseLinha;
        }

        public List<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> GetDataRepasseLinha(short? pCOD_GRUPO_EMPRS, short? pCOD_EMPRS, short? pANO_REF, short? pMES_REF, short? pCOD_ARQ_AREA)
        {
            
            IQueryable<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> query;

            query = from rl in m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA
                    join r in m_DbContext.PRE_TBL_ARQ_ENV_REPASSE on rl.COD_ARQ_ENV_REPASSE equals r.COD_ARQ_ENV_REPASSE 
                    //into l
                    //join sg in m_DbContext.TB_SCR_SUBGRUPO_FINANC_VERBA on tb.COD_VERBA equals sg.COD_VERBA into leftjoin
                    //from sgj in leftjoin.DefaultIfEmpty()
                    where (rl.DTH_EXCLUSAO == null)
                    && (r.COD_ARQ_ENVIO != null) //Apenas Repasses com Arq. Envio gerado (Liberados)
                    && (r.COD_GRUPO_EMPRS == pCOD_GRUPO_EMPRS || pCOD_GRUPO_EMPRS == null)
                    && (rl.COD_EMPRS == pCOD_EMPRS || pCOD_EMPRS == null)
                    && (r.ANO_REF == pANO_REF || pANO_REF == null)
                    && (r.MES_REF == pMES_REF || pMES_REF == null)
                    && (r.COD_ARQ_AREA == pCOD_ARQ_AREA || pCOD_ARQ_AREA == null)
                    //&& (rl.COD_VERBA == verba || verba == null)
                    //&& (rl.NUM_RGTRO_EMPRG == matricula || matricula == null)
                    //&& (sgj.CRED_DEB == tipo || tipo == "0")
                    orderby rl.COD_EMPRS, rl.NUM_RGTRO_EMPRG 
                    select new PRE_TBL_ARQ_ENV_REPASSE_LINHA_View()
                    {
                        COD_ARQ_ENV_REP_LINHA = rl.COD_ARQ_ENV_REP_LINHA,
                        COD_ARQ_ENV_REPASSE = rl.COD_ARQ_ENV_REPASSE,
                        COD_EMPRS = rl.COD_EMPRS,
                        NUM_RGTRO_EMPRG = rl.NUM_RGTRO_EMPRG,
                        COD_VERBA = rl.COD_VERBA,
                        COD_VERBA_PATROCINA = rl.COD_VERBA_PATROCINA,
                        VLR_PERCENTUAL = rl.VLR_PERCENTUAL,
                        VLR_DESCONTO = rl.VLR_DESCONTO,
                        DTH_INCLUSAO = rl.DTH_INCLUSAO,
                        LOG_INCLUSAO = rl.LOG_INCLUSAO,
                        DTH_EXCLUSAO = rl.DTH_EXCLUSAO,
                        LOG_EXCLUSAO = rl.LOG_EXCLUSAO
                        //TIPO = sgj.CRED_DEB
                    };

            //return query;

            return query.ToList();
        }

        public int GetDataCountDetalhes(int codigo, short? empresa, int? verba, long? matricula, string tipo)
        {
            return GetWhereDetalhes(codigo, empresa, verba, matricula, tipo).SelectCount();
        }

        public IQueryable<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> GetWhereDetalhes(int codigo, short? empresa, int? verba, long? matricula, string tipo)
        {
            IQueryable<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> query;

            query = from tb in m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA
                    join sg in m_DbContext.TB_SCR_SUBGRUPO_FINANC_VERBA on tb.COD_VERBA equals sg.COD_VERBA into leftjoin
                    from sgj in leftjoin.DefaultIfEmpty()
                    where (tb.COD_ARQ_ENV_REPASSE == codigo)
                    && (tb.DTH_EXCLUSAO == null)
                    && (tb.COD_EMPRS == empresa || empresa == null)
                    && (tb.COD_VERBA == verba || verba == null)
                    && (tb.NUM_RGTRO_EMPRG == matricula || matricula == null)
                    && (sgj.CRED_DEB == tipo || tipo == "0")
                    select new PRE_TBL_ARQ_ENV_REPASSE_LINHA_View()
                    {
                        COD_ARQ_ENV_REP_LINHA = tb.COD_ARQ_ENV_REP_LINHA,
                        COD_ARQ_ENV_REPASSE = tb.COD_ARQ_ENV_REPASSE,
                        COD_EMPRS = tb.COD_EMPRS,
                        NUM_RGTRO_EMPRG = tb.NUM_RGTRO_EMPRG,
                        COD_VERBA = tb.COD_VERBA,
                        COD_VERBA_PATROCINA = tb.COD_VERBA_PATROCINA,
                        VLR_PERCENTUAL = tb.VLR_PERCENTUAL,
                        VLR_DESCONTO = tb.VLR_DESCONTO,
                        DTH_INCLUSAO = tb.DTH_INCLUSAO,
                        LOG_INCLUSAO = tb.LOG_INCLUSAO,
                        DTH_EXCLUSAO = tb.DTH_EXCLUSAO,
                        LOG_EXCLUSAO = tb.LOG_EXCLUSAO,
                        TIPO = sgj.CRED_DEB
                    };

            return query;
        }

        public PRE_TBL_ARQ_ENV_REPASSE_View GetLinha(int? codigo)
        {
            IQueryable<PRE_TBL_ARQ_ENV_REPASSE_View> query;

            query = from tb in m_DbContext.PRE_TBL_ARQ_ENV_REPASSE
                    join d in
                        (from hst in m_DbContext.PRE_TBL_ARQ_ENVIO_HIST
                         join s in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS on hst.COD_ARQ_STATUS equals s.COD_ARQ_STATUS
                         select new PRE_TBL_ARQ_ENVIO_HIST_View()
                         {
                             COD_ARQ_ENVIO = hst.COD_ARQ_ENVIO,
                             COD_ARQ_STATUS = hst.COD_ARQ_STATUS,
                             DCR_ARQ_STATUS = s.DCR_ARQ_STATUS,
                             DTH_INCLUSAO = hst.DTH_INCLUSAO
                         }) on tb.COD_ARQ_ENVIO equals d.COD_ARQ_ENVIO
                    into leftjoin
                    from dd in leftjoin.DefaultIfEmpty()
                    where (dd.DTH_INCLUSAO == m_DbContext.PRE_TBL_ARQ_ENVIO_HIST.Where(h => h.COD_ARQ_ENVIO == tb.COD_ARQ_ENVIO).Max(h => h.DTH_INCLUSAO) || dd.COD_ARQ_ENVIO == null)
                       && (tb.COD_ARQ_ENV_REPASSE == codigo)
                    select new PRE_TBL_ARQ_ENV_REPASSE_View()
                    {
                        COD_ARQ_ENV_REPASSE = tb.COD_ARQ_ENV_REPASSE,
                        DCR_ARQ_ENV_REPASSE = tb.DCR_ARQ_ENV_REPASSE,
                        ANO_REF = tb.ANO_REF,
                        MES_REF = tb.MES_REF,
                        COD_GRUPO_EMPRS = tb.COD_GRUPO_EMPRS,
                        COD_ARQ_AREA = tb.COD_ARQ_AREA,
                        COD_ARQ_ENVIO = tb.COD_ARQ_ENVIO,
                        DTH_INCLUSAO = tb.DTH_INCLUSAO,
                        LOG_INCLUSAO = tb.LOG_INCLUSAO,
                        COD_ARQ_STATUS = dd.COD_ARQ_STATUS ?? 1,
                        DCR_ARQ_STATUS = dd.DCR_ARQ_STATUS ?? "Novo"
                    };
            return query.FirstOrDefault();
        }

        //public List<PRE_TBL_ARQ_ENVIO_STATUS> GetStatusDdl()
        //{
        //    IQueryable<PRE_TBL_ARQ_ENVIO_STATUS> query;

        //    query = from ptaes in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS
        //            where ptaes.COD_ARQ_STATUS < 5
        //            select ptaes;

        //    return query.ToList();
        //}

        //public List<PRE_TBL_GRUPO_EMPRS> GetGrupoDdl()
        //{
        //    IQueryable<PRE_TBL_GRUPO_EMPRS> query;

        //    query = from ptge in m_DbContext.PRE_TBL_GRUPO_EMPRS
        //            where ptge.COD_GRUPO_EMPRS != 50 && ptge.COD_GRUPO_EMPRS != 88
        //            select ptge;

        //    return query.ToList();
        //}

        //public List<PRE_TBL_ARQ_AREA_View> GetAreaDdl()
        //{
        //    IQueryable<PRE_TBL_ARQ_AREA_View> query;

        //    query = from aa in m_DbContext.PRE_TBL_ARQ_AREA
        //            where aa.COD_ARQ_AREA > 2
        //            select new PRE_TBL_ARQ_AREA_View()
        //            {
        //                COD_ARQ_AREA = aa.COD_ARQ_AREA,
        //                DCR_ARQ_AREA = aa.DCR_ARQ_AREA,
        //                DCR_ARQ_SUB_AREA = aa.DCR_ARQ_SUB_AREA
        //            };

        //    return query.ToList();
        //}

        public PRE_TBL_ARQ_ENV_REPASSE_LINHA_View GetTipoLancamento(int? verba)
        {
            PRE_TBL_ARQ_ENV_REPASSE_LINHA_View obj = new PRE_TBL_ARQ_ENV_REPASSE_LINHA_View();

            IQueryable<PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> query;
            query = from tbtp in m_DbContext.TB_SCR_SUBGRUPO_FINANC_VERBA
                    where (tbtp.COD_VERBA == verba)
                    select new PRE_TBL_ARQ_ENV_REPASSE_LINHA_View()
                    {
                        TIPO = tbtp.CRED_DEB
                    };
            obj = query.FirstOrDefault();

            return obj;
        }

        public Resultado SaveDataRepasse(PRE_TBL_ARQ_ENV_REPASSE_View objPai, int flag)
        {
            Resultado res = new Resultado();
            try
            {
                PRE_TBL_ARQ_ENV_REPASSE objTb = new PRE_TBL_ARQ_ENV_REPASSE();

                //var atualiza = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.FirstOrDefault(p => p.COD_ARQ_ENV_REPASSE == objPai.COD_ARQ_ENV_REPASSE 
                //                                                                || p.ANO_REF == objPai.ANO_REF && p.MES_REF == objPai.MES_REF
                //                                                                && p.COD_GRUPO_EMPRS == objPai.COD_GRUPO_EMPRS
                //                                                                && p.COD_ARQ_AREA == objPai.COD_ARQ_AREA
                //                                                                && p.DCR_ARQ_ENV_REPASSE == objPai.DCR_ARQ_ENV_REPASSE);

                var atualiza = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.Find(objPai.COD_ARQ_ENV_REPASSE);
                
                if (atualiza == null && (flag == 1 || flag == 2))
                {
                    objTb.COD_ARQ_ENV_REPASSE = GetMaxPkRepasse()+1; //Ver isso
                    objTb.DCR_ARQ_ENV_REPASSE = objPai.DCR_ARQ_ENV_REPASSE;
                    objTb.ANO_REF = objPai.ANO_REF;
                    objTb.MES_REF = objPai.MES_REF;
                    objTb.COD_GRUPO_EMPRS = objPai.COD_GRUPO_EMPRS;
                    objTb.COD_ARQ_AREA = objPai.COD_ARQ_AREA;
                    objTb.COD_ARQ_ENVIO = objPai.COD_ARQ_ENVIO;
                    objTb.DTH_INCLUSAO = objPai.DTH_INCLUSAO;
                    objTb.LOG_INCLUSAO = objPai.LOG_INCLUSAO;
                    m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.Add(objTb);

                    m_DbContext.SaveChanges();
                    res.Sucesso("Registro Inserido com sucesso.", objTb.COD_ARQ_ENV_REPASSE);
                }
                else if (atualiza != null && flag == 1)
                {
                    atualiza.ANO_REF = objPai.ANO_REF;
                    atualiza.MES_REF = objPai.MES_REF;
                    atualiza.COD_ARQ_AREA = objPai.COD_ARQ_AREA;
                    atualiza.COD_ARQ_ENVIO = objPai.COD_ARQ_ENVIO;
                    atualiza.DTH_INCLUSAO = objPai.DTH_INCLUSAO;
                    atualiza.LOG_INCLUSAO = objPai.LOG_INCLUSAO;

                    m_DbContext.SaveChanges();
                    res.Sucesso("Registro Atualizado com sucesso.", objPai.COD_ARQ_ENV_REPASSE);
                }
                else if (atualiza != null && flag == 2)
                {
                    res.Erro("Registro duplicado! Não foi possível importar!");
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

        public Resultado ExcluirRepasse(PRE_TBL_ARQ_ENV_REPASSE objRepasse, PRE_TBL_ARQ_ENV_REPASSE_LINHA objLinha)
        {
            Resultado res = new Resultado();

            try
            {
                var deletaRepasse = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.Find(objRepasse.COD_ARQ_ENV_REPASSE);

                if (deletaRepasse != null)
                {
                    deletaRepasse.PRE_TBL_ARQ_ENV_REPASSE_LINHA.Where(erl => erl.COD_ARQ_ENV_REPASSE == objLinha.COD_ARQ_ENV_REPASSE && erl.DTH_EXCLUSAO == null).ToList().ForEach(p => { p.DTH_EXCLUSAO = objRepasse.DTH_INCLUSAO; p.LOG_EXCLUSAO = objRepasse.LOG_INCLUSAO; });
                    m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.Remove(deletaRepasse);

                    int rows_deleted = m_DbContext.SaveChanges();
                    if (rows_deleted > 0)
                    {
                        res.Sucesso("Registro excluído com sucesso.");
                    }
                }
                else
                {
                    res.Erro("Registro não encontrado!");
                }
            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
            return res;
        }

        public Resultado ExcluirRepasseVazio(PRE_TBL_ARQ_ENV_REPASSE_View objRepasse)
        {
            Resultado res = new Resultado();
            try
            {
                var deletaRepasse = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.Find(objRepasse.COD_ARQ_ENV_REPASSE);

                if (deletaRepasse != null)
                {
                    m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.Remove(deletaRepasse);

                    int rows_deleted = m_DbContext.SaveChanges();
                    if (rows_deleted > 0)
                    {
                        res.Sucesso("Registro excluído com sucesso.");
                    }
                }
                else
                {
                    res.Erro("Registro não encontrado!");
                }
            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
            return res;
        }

        public Resultado SaveDataLinha(PRE_TBL_ARQ_ENV_REPASSE_LINHA_View objFilho)
        {
            Resultado res = new Resultado();
            try
            {
                PRE_TBL_ARQ_ENV_REPASSE_LINHA objTb = new PRE_TBL_ARQ_ENV_REPASSE_LINHA();

                var atualiza = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.FirstOrDefault(tb => tb.COD_ARQ_ENV_REPASSE == objFilho.COD_ARQ_ENV_REPASSE);

                if (atualiza == null)
                {

                    objTb.COD_ARQ_ENV_REPASSE = objFilho.COD_ARQ_ENV_REPASSE;
                    objTb.COD_ARQ_ENV_REP_LINHA = GetMaxPkLinha(objFilho.COD_ARQ_ENV_REPASSE);
                    objTb.COD_EMPRS = objFilho.COD_EMPRS;
                    objTb.NUM_RGTRO_EMPRG = objFilho.NUM_RGTRO_EMPRG;
                    objTb.COD_VERBA = objFilho.COD_VERBA;
                    objTb.COD_VERBA_PATROCINA = objFilho.COD_VERBA_PATROCINA;
                    objTb.VLR_PERCENTUAL = objFilho.VLR_PERCENTUAL;
                    objTb.VLR_DESCONTO = objFilho.VLR_DESCONTO;
                    objTb.DTH_INCLUSAO = objFilho.DTH_INCLUSAO;
                    objTb.LOG_INCLUSAO = objFilho.LOG_INCLUSAO;

                    m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.Add(objTb);
                }
                else
                {
                    objTb.COD_ARQ_ENV_REPASSE = objFilho.COD_ARQ_ENV_REPASSE;
                    objTb.COD_ARQ_ENV_REP_LINHA = GetMaxPkLinha(objFilho.COD_ARQ_ENV_REPASSE);
                    objTb.COD_EMPRS = objFilho.COD_EMPRS;
                    objTb.NUM_RGTRO_EMPRG = objFilho.NUM_RGTRO_EMPRG;
                    objTb.COD_VERBA = objFilho.COD_VERBA;
                    objTb.COD_VERBA_PATROCINA = objFilho.COD_VERBA_PATROCINA;
                    objTb.VLR_PERCENTUAL = objFilho.VLR_PERCENTUAL;
                    objTb.VLR_DESCONTO = objFilho.VLR_DESCONTO;
                    objTb.DTH_INCLUSAO = objFilho.DTH_INCLUSAO;
                    objTb.LOG_INCLUSAO = objFilho.LOG_INCLUSAO;

                    m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.Add(objTb);
                }

                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    res.Sucesso("Registro atualizado com sucesso.");
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

        public Resultado AtualizarLinha(PRE_TBL_ARQ_ENV_REPASSE_LINHA obj)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.FirstOrDefault(erl => erl.COD_ARQ_ENV_REPASSE == obj.COD_ARQ_ENV_REPASSE && erl.COD_ARQ_ENV_REP_LINHA == obj.COD_ARQ_ENV_REP_LINHA && erl.DTH_EXCLUSAO == null);

                if (atualiza != null)
                {
                    atualiza.DTH_EXCLUSAO = obj.DTH_INCLUSAO;
                    atualiza.LOG_EXCLUSAO = obj.LOG_INCLUSAO;

                    m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.Add(obj);

                    int rows_update = m_DbContext.SaveChanges();

                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registros atualizados.", rows_update));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

        public Resultado ExcluirLinha(PRE_TBL_ARQ_ENV_REPASSE_LINHA obj)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.FirstOrDefault(erl => erl.COD_ARQ_ENV_REPASSE == obj.COD_ARQ_ENV_REPASSE && erl.COD_ARQ_ENV_REP_LINHA == obj.COD_ARQ_ENV_REP_LINHA && erl.DTH_EXCLUSAO == null);

                if (atualiza != null)
                {
                    atualiza.DTH_EXCLUSAO = obj.DTH_INCLUSAO;
                    atualiza.LOG_EXCLUSAO = obj.LOG_INCLUSAO;

                    int rows_update = m_DbContext.SaveChanges();

                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registros atualizados.", rows_update));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }
        #endregion

        #region .: Importação :.

        protected Resultado Importar(PRE_TBL_ARQ_ENV_REPASSE_View obj, int empresa)
        {
            Resultado res = new Resultado();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("vMesRef", obj.MES_REF);
                objConexao.AdicionarParametro("vAnoRef", obj.ANO_REF);
                objConexao.AdicionarParametro("VID_USER", obj.LOG_INCLUSAO);
                objConexao.AdicionarParametro("VDTH_INCLUSAO", obj.DTH_INCLUSAO);
                objConexao.AdicionarParametro("vCod_Emprs", empresa);
                objConexao.AdicionarParametro("VCOD_ARQ_REPASSE", obj.COD_ARQ_ENV_REPASSE);
                objConexao.AdicionarParametroOut("V_OUT_QTD_IMPORTADO");
                switch (obj.COD_ARQ_AREA)
                {
                    case 3: //Joia
                        objConexao.ExecutarNonQuery("own_funcesp.PROC_SCR_ENVARQ_JOIACONTRIB");
                        break;
                    case 4: //AutoPatrocinio
                        objConexao.ExecutarNonQuery("OWN_FUNCESP.PROC_SCR_ENVARQ_AUTOPATROC");
                        break;
                    case 5: //Voluntária
                        objConexao.ExecutarNonQuery("OWN_FUNCESP.PROC_SCR_ENVARQ_VOLUNTARIA");
                        //objConexao.ExecutarNonQuery("own_funcesp.PROC_SCP_bate_arq_patroc");
                        break;
                    case 7: //Empréstimo
                        objConexao.ExecutarNonQuery("own_funcesp.PROC_SCR_ENVARQ_EMPREST");
                        objConexao.ExecutarNonQuery("OWN_FUNCESP.PROC_ENV_EMPREST_AUTOPATROC");
                        break;
                    case 8: //Saude
                        objConexao.ExecutarNonQuery("own_funcesp.PROC_SCR_ENVARQ_AMHEPES");
                        break;
                    case 9: //Seguro
                        objConexao.ExecutarNonQuery("own_funcesp.PROC_SCR_ENVARQ_SAP");
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }

            int qtd_importada = 0;
            if (objConexao.ReturnParemeterOut().Value != null)
            {
                int.TryParse(objConexao.ReturnParemeterOut().Value.ToString(), out qtd_importada);
                if (qtd_importada > 0)
                {
                    if (obj.COD_ARQ_AREA == 5) // Apenas para Voluntária
                    {
                        GeraRelConferenciaMensal(objConexao);
                    }
                    
                    res.Sucesso("Descontos importados com sucesso.", qtd_importada);
                }
                else
                {
                    res.Erro("Nenhum desconto foi processado para a empresa " + empresa);
                }
            }
            else
            {
                res.Sucesso("Nenhum desconto importado.", qtd_importada);
            }

            objConexao.Dispose();

            return res;

        }

        private void GeraRelConferenciaMensal(ConexaoOracle objConexao)
        {
            try
            {
                objConexao.ExecutarNonQuery("own_funcesp.PROC_SCP_bate_arq_patroc");
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region .:Métodos:.
        public string GetGrupoPortal(short? grupo)
        {
            string grupo_portal = "";

            var atualiza = m_DbContext.PRE_TBL_GRUPO_EMPRS.Find(grupo);

            grupo_portal = atualiza.GRUPO_PORTAL;

            return grupo_portal;
        }
        public bool ValidaEmpresa(short? grupo, short? empresa)
        {
            string grupo_portal = GetGrupoPortal(grupo);

            IEnumerable<FCESP_GRUPO_EMP_View> IEnum = m_DbContext.Database.SqlQuery<FCESP_GRUPO_EMP_View>("select EMPRESA from OWN_PORTAL.FCESP_GRUPO_EMP@PPORTAL.WORLD " +
                                                                                                               "where GRUPO LIKE " + "'%" + grupo_portal + "%'");
            List<FCESP_GRUPO_EMP_View> list = IEnum.ToList();

            var find = list.Find(l => l.EMPRESA == empresa);
            if (find == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }
        public int getCodArquivoRepasse()
        {

            var maxPK = from i in m_DbContext.PRE_TBL_ARQ_ENV_REPASSE
                        where i.DTH_INCLUSAO == (m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.Where(i1 => i1.COD_ARQ_ENV_REPASSE == i.COD_ARQ_ENV_REPASSE).Max(i2 => i2.DTH_INCLUSAO))
                        select i.COD_ARQ_ENV_REPASSE;
            return maxPK.Max();
        }
        public long GetCodLinhaRepasse(int codigo)
        {
            var maxPK = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.Where(tb => tb.COD_ARQ_ENV_REPASSE == codigo).Max(tb => tb.COD_ARQ_ENV_REP_LINHA);
            return maxPK;
        }
        public string GetDescricaoVerba(int? verba)
        {
            string dcr = "";

            IEnumerable<ATT_VIEW_VERBA> IEnum = m_DbContext.Database.SqlQuery<ATT_VIEW_VERBA>("SELECT COD_VERBA, DCR_VERBA FROM ATT.VERBA WHERE COD_VERBA = :p", verba);

            dcr = IEnum.FirstOrDefault().DCR_VERBA;

            return dcr;
        }
        public int GetMaxPkLinha(long? codigo)
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.Where(m => m.COD_ARQ_ENV_REPASSE == codigo).DefaultIfEmpty().Max(m => (m.COD_ARQ_ENV_REP_LINHA == null) ? 0 : m.COD_ARQ_ENV_REP_LINHA + 1);
            return maxPK;
        }
        public int GetMaxPkRepasse()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.DefaultIfEmpty().Max(n => (n.COD_ARQ_ENV_REPASSE == null) ? 1 : n.COD_ARQ_ENV_REPASSE);
            return maxPK;
        }
        public int GetCountInsert(int codigoRepasse)
        {
            int total = 0;
            total = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE_LINHA.Where(li => li.COD_ARQ_ENV_REPASSE == codigoRepasse).Count();
            return total;
        }
        #endregion

    }
}
