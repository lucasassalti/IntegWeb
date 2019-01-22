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
    public class ArqPatrocinadoraDAL
    {

        public partial class PRE_VIEW_ARQ_PATROCINA : PRE_TBL_ARQ_PATROCINA
        {

            //private PRE_TBL_ARQ_PATROCINA _PRE_TBL_ARQ_PATROCINA = new PRE_TBL_ARQ_PATROCINA();

            public PRE_VIEW_ARQ_PATROCINA()
            {

            }

            public PRE_VIEW_ARQ_PATROCINA(PRE_TBL_ARQ_PATROCINA ap)
            {
                COD_ARQ_PAT = ap.COD_ARQ_PAT;
                NOM_ARQUIVO = ap.NOM_ARQUIVO;
                TIP_ARQUIVO = ap.TIP_ARQUIVO;
                NUM_HASH = ap.NUM_HASH;
                LOG_INCLUSAO = ap.LOG_INCLUSAO;
                DTH_INCLUSAO = ap.DTH_INCLUSAO;
                ANO_REF = ap.ANO_REF;
                MES_REF = ap.MES_REF;
                COD_STATUS = ap.COD_STATUS;
                GRUPO_PORTAL = ap.GRUPO_PORTAL;
                DTH_EXCLUSAO = ap.DTH_EXCLUSAO;
                NUM_QTD_PROCESSADOS = ap.NUM_QTD_PROCESSADOS;
                NUM_QTD_VALIDOS = ap.NUM_QTD_VALIDOS;
                NUM_QTD_ERROS = ap.NUM_QTD_ERROS;
                NUM_QTD_ERROS_LINHAS = ap.NUM_QTD_ERROS_LINHAS;
                NUM_QTD_ALERTAS = ap.NUM_QTD_ALERTAS;
                NUM_QTD_ALERTAS_LINHAS = ap.NUM_QTD_ALERTAS_LINHAS;
                NUM_QTD_IMPORTADOS = ap.NUM_QTD_IMPORTADOS;
                NUM_QTD_PROCESSADOS = ap.NUM_QTD_PROCESSADOS;
                PRE_TBL_ARQ_PATROCINA_TIPO = ap.PRE_TBL_ARQ_PATROCINA_TIPO;
            }
            public string DCR_TIPO { get; set; }
            public Nullable<System.DateTime> DAT_REPASSE { get; set; }
            public Nullable<System.DateTime> DAT_CREDITO { get; set; }
            public string COD_EMPRS { get; set; }
            public string DCR_STATUS { get; set; }
            public Nullable<int> NUM_QTD_REGISTROS { get; set; }
            public int NUM_PERC_PROCESSADOS
            {
                get
                {
                    decimal r = (NUM_QTD_REGISTROS ?? 1);
                    decimal p = (NUM_QTD_PROCESSADOS ?? 0);
                    int ret = Convert.ToInt32((p / r) * 100);
                    int ret_aj = Convert.ToInt32(((NUM_QTD_REGISTROS / 20) / r) * 100);
                    ret = (ret == 0) ? ret_aj : ret;
                    return ret;
                    //return Convert.ToInt32(NUM_QTD_REGISTROS);
                }
            }
        }

        public partial class PRE_VIEW_ARQ_PATROCINA_CRITICA
        {
            public long COD_ARQ_PAT_CRITICA { get; set; }
            public Nullable<long> NUM_LINHA { get; set; }
            public Nullable<short> TIP_LINHA { get; set; }
            //public Nullable<int> COD_ARQ_PAT_CARGA { get; set; }
            //public Nullable<long> COD_ARQ_PAT_LINHA { get; set; }
            //public Nullable<int> COD_ARQ_PAT { get; set; }
            public Nullable<short> NUM_POSICAO { get; set; }
            public string NOM_CAMPO { get; set; }            
            public Nullable<short> COD_CRITICA { get; set; }
            public string DCR_CRITICA { get; set; }
            public short TIP_CRITICA { get; set; }
            public string COD_EMPRS { get; set; }
            public string NUM_RGTRO_EMPRG { get; set; }
            public string COD_VERBA { get; set; }

            public bool ALERTA
            {
                get
                {
                    if (TIP_CRITICA == 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public string DCR_TIP_CRITICA
            {
                get
                {
                    if (ALERTA)
                    {
                        return "Alerta";
                    } else {
                        return "ERRO";
                    }
                }
            }

            //public string COD_DCR_CRITICA
            //{
            //    get
            //    {
            //        return "[" + (ALERTA ? "A" : "E") + COD_CRITICA + "] " + DCR_CRITICA;
            //    }
            //}
        }

        public partial class PRE_VIEW_ARQ_PAT_DEMONSTRA
        {
            public Nullable<short> ANO_REF { get; set; }
            public Nullable<short> MES_REF { get; set; }
            public Nullable<System.DateTime> DAT_REPASSE { get; set; }
            public Nullable<System.DateTime> DAT_CREDITO { get; set; }
            public string GRUPO_PORTAL { get; set; }
        }

        public class PRE_VIEW_ARQ_RECEBIDO_CONTROLE
        {
            public short COD_GRUPO_EMPRS { get; set; }
            public string DCR_GRUPO_EMPRS { get; set; }
            public string GRUPO_PORTAL { get; set; }
            public int QTD_CADASTRAL_VALIDADO { get; set; }
            public int QTD_CADASTRAL_CARREGADO { get; set; }
            public int QTD_AFASTAMENTO_VALIDADO { get; set; }
            public int QTD_AFASTAMENTO_CARREGADO { get; set; }
            public int QTD_ORGAO_LOTACAO_VALIDADO { get; set; }
            public int QTD_ORGAO_LOTACAO_CARREGADO { get; set; }
            public int QTD_FINANCEIRO_VALIDADO { get; set; }
            public int QTD_FINANCEIRO_CARREGADO { get; set; }
            public string DCR_QTD_CADASTRAL { get; set; }
            public string DCR_QTD_AFASTAMENTO { get; set; }
            public string DCR_QTD_ORGAO_LOTACAO { get; set; }
            public string DCR_QTD_FINANCEIRO { get; set; }

            public string DCR_GRUPO_EMPRS_MASK
            {
                get
                {
                    string strMask = "{0}-{1}";
                    return String.Format(strMask, COD_GRUPO_EMPRS, DCR_GRUPO_EMPRS);
                }
            }

            public List<PRE_TBL_ARQ_PATROCINA> lstARQUIVOS_RECEBIDOS { get; set; }
        }

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public Resultado Persistir(PRE_TBL_ARQ_PATROCINA newArqPatrocina)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                int iMaxPk_ARQ = GetMaxPk();
                long iMaxPk_LINHA = GetMaxPk_LINHA();
                long iMaxPk_CRITICA = GetMaxPk_CRITICA();
                iMaxPk_ARQ++;
                newArqPatrocina.COD_ARQ_PAT = iMaxPk_ARQ;
                newArqPatrocina.PRE_TBL_ARQ_PATROCINA_LINHA.ToList()
                    .ForEach(l =>
                        {
                            iMaxPk_LINHA++;
                            l.COD_ARQ_PAT = iMaxPk_ARQ;
                            l.COD_ARQ_PAT_LINHA = iMaxPk_LINHA;
                            l.PRE_TBL_ARQ_PATROCINA_CRITICA.ToList().ForEach(c =>
                                {
                                    iMaxPk_CRITICA++;
                                    c.COD_ARQ_PAT_LINHA = iMaxPk_LINHA;
                                    c.COD_ARQ_PAT_CRITICA = iMaxPk_CRITICA;
                                    c.DCR_CRITICA = Util.String2Limit(c.DCR_CRITICA, 0, 200);
                                });
                        });

                newArqPatrocina.PRE_TBL_ARQ_PATROCINA_CRITICA.ToList()
                    .ForEach(cr =>
                        {
                            iMaxPk_CRITICA++;
                            cr.COD_ARQ_PAT = iMaxPk_ARQ;
                            cr.COD_ARQ_PAT_CRITICA = iMaxPk_CRITICA;
                            cr.DCR_CRITICA = Util.String2Limit(cr.DCR_CRITICA, 0, 200);
                        });

                m_DbContext.PRE_TBL_ARQ_PATROCINA.Add(newArqPatrocina);
                m_DbContext.SaveChanges();
                res.Sucesso("Arquivo(s) enviado(s) com sucesso.");
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

        public Resultado UpdateData(PRE_TBL_ARQ_PATROCINA uptArqPatrocina)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_PATROCINA.FirstOrDefault(p => p.COD_ARQ_PAT == uptArqPatrocina.COD_ARQ_PAT);

                if (atualiza != null)
                {
                    //atualiza.MES_REF = uptArqPatrocina.MES_REF;
                    //atualiza.ANO_REF = uptArqPatrocina.ANO_REF;                    
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(uptArqPatrocina);
                    m_DbContext.SaveChanges();
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

        public Resultado UpdateStatus(int pCOD_ARQ_PAT, short pStatus)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_PATROCINA.FirstOrDefault(p => p.COD_ARQ_PAT == pCOD_ARQ_PAT);

                if (atualiza != null)
                {
                    atualiza.COD_STATUS = pStatus;
                    m_DbContext.SaveChanges();

                    InsertLogArq(atualiza);

                    res.Sucesso("Status atualizado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }



        public int GetMaxPk()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_PATROCINA.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_ARQ_PAT);
            return maxPK;
        }

        public long GetMaxPk_CRITICA()
        {
            long maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_ARQ_PAT_CRITICA);
            return maxPK;
        }

        public List<PRE_TBL_ARQ_PATROCINA> GetData(int startRowIndex, int maximumRows, string sortParameter)
        {
            return GetWhere()
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public PRE_TBL_ARQ_PATROCINA GetDataByCod(int pCOD_ARQ_PAT)
        {
            return (from a in GetWhere()
                    where (a.COD_ARQ_PAT == pCOD_ARQ_PAT)
                    select a)
                   .ToList()
                   .FirstOrDefault();
        }

        public PRE_TBL_ARQ_PATROCINA GetDataByNome(string pNOM_ARQUIVO, string grupo_portal)
        {
            return (from a in GetWhere()
                    where (a.NOM_ARQUIVO == pNOM_ARQUIVO || pNOM_ARQUIVO == null)
                       && (a.GRUPO_PORTAL == grupo_portal)
                       && (a.DTH_EXCLUSAO == null)
                    select a)
                   .ToList()
                   .FirstOrDefault();
        }

        public PRE_TBL_ARQ_PATROCINA GetDataByHASH(long? pNUM_HASH, string grupo_portal)
        {
            return (from a in GetWhere()
                    where (a.NUM_HASH == pNUM_HASH)
                       && (a.GRUPO_PORTAL == grupo_portal)
                       && (a.DTH_EXCLUSAO == null)
                    orderby a.COD_ARQ_PAT descending
                    select a
                    )
                   .ToList()
                   .FirstOrDefault();
        }

        public List<PRE_TBL_ARQ_PATROCINA> GetDataByGrupoPortal(string grupo_portal, short? ano, short? mes)
        {
            return (from a in GetWhere()
                    where (a.GRUPO_PORTAL == grupo_portal || grupo_portal == null)
                       && (a.ANO_REF == ano || ano == null)
                       && (a.MES_REF == mes || mes == null)
                       && (a.DTH_EXCLUSAO == null)
                    select a)
                   .ToList();
        }

        public IQueryable<PRE_TBL_ARQ_PATROCINA> GetWhere()
        {
            IQueryable<PRE_TBL_ARQ_PATROCINA> query;
            query = from a in m_DbContext.PRE_TBL_ARQ_PATROCINA
                    select a;
            return query;
        }

        public List<PRE_VIEW_ARQ_PATROCINA> GetGroupBy(int startRowIndex, int maximumRows, string pGruposAcesso, int pExibir, int pAgrupar, string pGrupo, string sortParameter)
        {

            List<PRE_VIEW_ARQ_PATROCINA> ret = new List<PRE_VIEW_ARQ_PATROCINA>();

            if (pAgrupar == 1)
            {
                ret = GetGroupBy_ARQUIVO(pExibir, pGruposAcesso, pGrupo)
                      .GetData(startRowIndex, maximumRows, sortParameter).ToList();
            }
            else
            {
                ret = GetGroupBy_COD_EMPRS(pGruposAcesso, pGrupo)
                      .GetData(startRowIndex, maximumRows, sortParameter).ToList();
            }

            ret.ForEach(a => { a.COD_EMPRS = String.Join(",", GetCOD_EMPRSs(a.COD_ARQ_PAT)); });

            //String.Join(",", _myList.Select(x => x.Name));

            return ret;
        }

        public IQueryable<PRE_VIEW_ARQ_PATROCINA> GetGroupBy_ARQUIVO(int pExibir, string pGruposAcesso, string pGrupo)
        {

            string[] aAcesso = new string [] {"0"};
            if (pGruposAcesso!=null)
            {
                aAcesso = pGruposAcesso.Split(',');
            }

            IQueryable<PRE_VIEW_ARQ_PATROCINA> query;

            // 1ª ETAPA - Busca quantidades:
            query = qry_NUM_QTD_REGISTROS(aAcesso, pGrupo);
                   //.Concat(qry_NUM_QTD_ERROS(aAcesso, pGrupo, 1))
                   //.Concat(qry_NUM_QTD_ERROS(aAcesso, pGrupo, 2))
                   //.Concat(qry_NUM_QTD_IMPORTADOS(aAcesso, pGrupo));

            // 2ª ETAPA - Agrupa quantidades:
            query = qry_CONSOLIDA_QUANTIDADES(query);

            // 3ª ETAPA - Busca Demonstrativos Anteriores:
            query = qry_DEMONSTRATIVOS_ANTERIORES(query);
              
            DateTime ult_mes = DateTime.Now.AddMonths(-1);

            switch (pExibir)
            {
                case 1:
                    query = (from q in query
                             where q.DTH_INCLUSAO > ult_mes
                             select q);
                    break;
                case 2:
                    query = (from q in query
                             where q.NUM_QTD_ERROS > 0 || 
                                   q.NUM_QTD_ERROS_LINHAS > 0 || 
                                   q.NUM_QTD_ALERTAS > 0 || 
                                   q.NUM_QTD_ALERTAS_LINHAS > 0
                             select q);
                    break;
            }

            return query;
        }

        public IQueryable<PRE_VIEW_ARQ_PATROCINA> qry_NUM_QTD_REGISTROS(string[] aGrupos, string pGrupo)
        {
            return from a in m_DbContext.PRE_TBL_ARQ_PATROCINA
                   join t in m_DbContext.PRE_TBL_ARQ_PATROCINA_TIPO on a.TIP_ARQUIVO equals t.COD_TIPO
                   join s in m_DbContext.PRE_TBL_ARQ_PATROCINA_STATUS on a.COD_STATUS equals s.COD_STATUS
                   join l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA on a.COD_ARQ_PAT equals l.COD_ARQ_PAT
                   //where (l.TIP_LINHA == 2)
                   where (aGrupos.Contains(a.GRUPO_PORTAL) || aGrupos.Contains("9999"))
                      && (a.GRUPO_PORTAL == pGrupo || pGrupo == "9999")
                   group a by new { a.COD_ARQ_PAT, a.NOM_ARQUIVO, a.TIP_ARQUIVO, a.NUM_HASH, a.NUM_QTD_VALIDOS, a.NUM_QTD_ERROS, a.NUM_QTD_ERROS_LINHAS, a.NUM_QTD_ALERTAS, a.NUM_QTD_ALERTAS_LINHAS, a.NUM_QTD_IMPORTADOS, a.NUM_QTD_PROCESSADOS, t.DCR_TIPO, s.DCR_STATUS, a.LOG_INCLUSAO, a.DTH_INCLUSAO, a.ANO_REF, a.MES_REF, a.GRUPO_PORTAL, a.COD_STATUS } into g
                   select new PRE_VIEW_ARQ_PATROCINA
                   {
                       COD_ARQ_PAT = g.Key.COD_ARQ_PAT,
                       NOM_ARQUIVO = g.Key.NOM_ARQUIVO,
                       TIP_ARQUIVO = g.Key.TIP_ARQUIVO,
                       DCR_TIPO = g.Key.DCR_TIPO,
                       NUM_HASH = g.Key.NUM_HASH,
                       LOG_INCLUSAO = g.Key.LOG_INCLUSAO,
                       DTH_INCLUSAO = g.Key.DTH_INCLUSAO,
                       ANO_REF = g.Key.ANO_REF,
                       MES_REF = g.Key.MES_REF,
                       DAT_REPASSE = null,
                       DAT_CREDITO = null,
                       GRUPO_PORTAL = g.Key.GRUPO_PORTAL,
                       COD_STATUS = g.Key.COD_STATUS,
                       DCR_STATUS = g.Key.DCR_STATUS,
                       NUM_QTD_REGISTROS = g.Count(),
                       NUM_QTD_VALIDOS = g.Key.NUM_QTD_VALIDOS,
                       NUM_QTD_ERROS = g.Key.NUM_QTD_ERROS ?? 0,
                       NUM_QTD_ERROS_LINHAS = g.Key.NUM_QTD_ERROS_LINHAS,
                       NUM_QTD_ALERTAS = g.Key.NUM_QTD_ALERTAS,
                       NUM_QTD_ALERTAS_LINHAS = g.Key.NUM_QTD_ALERTAS_LINHAS,
                       NUM_QTD_IMPORTADOS = g.Key.NUM_QTD_IMPORTADOS,
                       NUM_QTD_PROCESSADOS = g.Key.NUM_QTD_PROCESSADOS
                   };
        }
        public IQueryable<PRE_VIEW_ARQ_PATROCINA> qry_NUM_QTD_ERROS(string[] aGrupos, string pGrupo, short? pTIP_CRITICA = 1)
        {
            IQueryable<PRE_VIEW_ARQ_PATROCINA> query = 
                   from a in m_DbContext.PRE_TBL_ARQ_PATROCINA
                   join t in m_DbContext.PRE_TBL_ARQ_PATROCINA_TIPO on a.TIP_ARQUIVO equals t.COD_TIPO
                   join s in m_DbContext.PRE_TBL_ARQ_PATROCINA_STATUS on a.COD_STATUS equals s.COD_STATUS
                   join l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA on a.COD_ARQ_PAT equals l.COD_ARQ_PAT
                   join cl in m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA on l.COD_ARQ_PAT_LINHA equals cl.COD_ARQ_PAT_LINHA
                   //where (cl.TIP_CRITICA == 1)
                   where (aGrupos.Contains(a.GRUPO_PORTAL) || aGrupos.Contains("9999"))
                     && (a.GRUPO_PORTAL == pGrupo || pGrupo == "9999")
                     && (cl.TIP_CRITICA == pTIP_CRITICA)
                   //&& (l.TIP_LINHA == 2)
                   group cl by new { a.COD_ARQ_PAT, a.NOM_ARQUIVO, a.TIP_ARQUIVO, a.NUM_HASH, a.NUM_QTD_PROCESSADOS, t.DCR_TIPO, s.DCR_STATUS, a.LOG_INCLUSAO, a.DTH_INCLUSAO, a.ANO_REF, a.MES_REF, a.GRUPO_PORTAL, a.COD_STATUS, l.COD_ARQ_PAT_LINHA } into g
                   select new PRE_VIEW_ARQ_PATROCINA
                   {
                       COD_ARQ_PAT = g.Key.COD_ARQ_PAT,
                       NOM_ARQUIVO = g.Key.NOM_ARQUIVO,
                       TIP_ARQUIVO = g.Key.TIP_ARQUIVO,
                       DCR_TIPO = g.Key.DCR_TIPO,
                       NUM_HASH = g.Key.NUM_HASH,
                       LOG_INCLUSAO = g.Key.LOG_INCLUSAO,
                       DTH_INCLUSAO = g.Key.DTH_INCLUSAO,
                       ANO_REF = g.Key.ANO_REF,
                       MES_REF = g.Key.MES_REF,
                       DAT_REPASSE = null,
                       DAT_CREDITO = null,
                       GRUPO_PORTAL = g.Key.GRUPO_PORTAL,
                       COD_STATUS = g.Key.COD_STATUS,
                       DCR_STATUS = g.Key.DCR_STATUS,
                       NUM_QTD_REGISTROS = 0,
                       NUM_QTD_VALIDOS = 0,
                       NUM_QTD_ERROS = 0,
                       NUM_QTD_ERROS_LINHAS = (pTIP_CRITICA == 1) ? g.Count() : 0,
                       NUM_QTD_ALERTAS = 0,
                       NUM_QTD_ALERTAS_LINHAS = (pTIP_CRITICA == 2) ? g.Count() : 0,
                       NUM_QTD_IMPORTADOS = 0,
                       NUM_QTD_PROCESSADOS = g.Key.NUM_QTD_PROCESSADOS
                   };

            query = query.Concat(
                   from a in m_DbContext.PRE_TBL_ARQ_PATROCINA
                   join t in m_DbContext.PRE_TBL_ARQ_PATROCINA_TIPO on a.TIP_ARQUIVO equals t.COD_TIPO
                   join s in m_DbContext.PRE_TBL_ARQ_PATROCINA_STATUS on a.COD_STATUS equals s.COD_STATUS
                   join ca in m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA on a.COD_ARQ_PAT equals ca.COD_ARQ_PAT
                   //where (ca.TIP_CRITICA == 1)
                   where (aGrupos.Contains(a.GRUPO_PORTAL) || aGrupos.Contains("9999"))
                      && (a.GRUPO_PORTAL == pGrupo || pGrupo == "9999")
                      && (ca.TIP_CRITICA == pTIP_CRITICA)
                   group ca by new { a.COD_ARQ_PAT, a.NOM_ARQUIVO, a.TIP_ARQUIVO, a.NUM_HASH, a.NUM_QTD_PROCESSADOS, t.DCR_TIPO, s.DCR_STATUS, a.LOG_INCLUSAO, a.DTH_INCLUSAO, a.ANO_REF, a.MES_REF, a.GRUPO_PORTAL, a.COD_STATUS } into g
                   select new PRE_VIEW_ARQ_PATROCINA
                   {
                       COD_ARQ_PAT = g.Key.COD_ARQ_PAT,
                       NOM_ARQUIVO = g.Key.NOM_ARQUIVO,
                       TIP_ARQUIVO = g.Key.TIP_ARQUIVO,
                       DCR_TIPO = g.Key.DCR_TIPO,
                       NUM_HASH = g.Key.NUM_HASH,
                       LOG_INCLUSAO = g.Key.LOG_INCLUSAO,
                       DTH_INCLUSAO = g.Key.DTH_INCLUSAO,
                       ANO_REF = g.Key.ANO_REF,
                       MES_REF = g.Key.MES_REF,
                       DAT_REPASSE = null,
                       DAT_CREDITO = null,
                       GRUPO_PORTAL = g.Key.GRUPO_PORTAL,
                       COD_STATUS = g.Key.COD_STATUS,
                       DCR_STATUS = g.Key.DCR_STATUS,
                       NUM_QTD_REGISTROS = 0,
                       NUM_QTD_VALIDOS = 0,
                       NUM_QTD_ERROS = (pTIP_CRITICA == 1) ? g.Count() : 0,
                       NUM_QTD_ERROS_LINHAS = 0,
                       NUM_QTD_ALERTAS = (pTIP_CRITICA == 2) ? g.Count() : 0,
                       NUM_QTD_ALERTAS_LINHAS = 0,
                       NUM_QTD_IMPORTADOS = 0,
                       NUM_QTD_PROCESSADOS = g.Key.NUM_QTD_PROCESSADOS
                   });
            
            return query;

        }
        public IQueryable<PRE_VIEW_ARQ_PATROCINA> qry_NUM_QTD_IMPORTADOS(string[] aGrupos, string pGrupo)
        {
            return from a in m_DbContext.PRE_TBL_ARQ_PATROCINA
                   join t in m_DbContext.PRE_TBL_ARQ_PATROCINA_TIPO on a.TIP_ARQUIVO equals t.COD_TIPO
                   join s in m_DbContext.PRE_TBL_ARQ_PATROCINA_STATUS on a.COD_STATUS equals s.COD_STATUS
                   join l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA on a.COD_ARQ_PAT equals l.COD_ARQ_PAT
                   where (l.DAT_IMPORTADO != null)
                      && (aGrupos.Contains(a.GRUPO_PORTAL) || aGrupos.Contains("9999"))
                      && (a.GRUPO_PORTAL == pGrupo || pGrupo == "9999")
                   group l by new { a.COD_ARQ_PAT, a.NOM_ARQUIVO, a.TIP_ARQUIVO, a.NUM_HASH, a.NUM_QTD_PROCESSADOS, t.DCR_TIPO, s.DCR_STATUS, a.LOG_INCLUSAO, a.DTH_INCLUSAO, a.ANO_REF, a.MES_REF, a.GRUPO_PORTAL, a.COD_STATUS, l.COD_ARQ_PAT_LINHA } into g
                   select new PRE_VIEW_ARQ_PATROCINA
                   {
                       COD_ARQ_PAT = g.Key.COD_ARQ_PAT,
                       NOM_ARQUIVO = g.Key.NOM_ARQUIVO,
                       TIP_ARQUIVO = g.Key.TIP_ARQUIVO,
                       DCR_TIPO = g.Key.DCR_TIPO,
                       NUM_HASH = g.Key.NUM_HASH,
                       LOG_INCLUSAO = g.Key.LOG_INCLUSAO,
                       DTH_INCLUSAO = g.Key.DTH_INCLUSAO,
                       ANO_REF = g.Key.ANO_REF,
                       MES_REF = g.Key.MES_REF,
                       DAT_REPASSE = null,
                       DAT_CREDITO = null,
                       GRUPO_PORTAL = g.Key.GRUPO_PORTAL,
                       COD_STATUS = g.Key.COD_STATUS,
                       DCR_STATUS = g.Key.DCR_STATUS,
                       NUM_QTD_REGISTROS = 0,
                       NUM_QTD_VALIDOS = 0,
                       NUM_QTD_ERROS = 0,
                       NUM_QTD_ERROS_LINHAS = 0,
                       NUM_QTD_ALERTAS = 0,
                       NUM_QTD_ALERTAS_LINHAS = 0,
                       NUM_QTD_IMPORTADOS = (g.Count() > 0) ? 1 : 0,
                       NUM_QTD_PROCESSADOS = g.Key.NUM_QTD_PROCESSADOS
                   };
        }

        public IQueryable<PRE_VIEW_ARQ_PATROCINA> qry_CONSOLIDA_QUANTIDADES(IQueryable<PRE_VIEW_ARQ_PATROCINA> query)
        {
            return from a in (query)
                   group a by new { a.COD_ARQ_PAT, a.NOM_ARQUIVO, a.TIP_ARQUIVO, a.NUM_HASH, a.DCR_TIPO, a.DCR_STATUS, a.LOG_INCLUSAO, a.DTH_INCLUSAO, a.ANO_REF, a.MES_REF, a.GRUPO_PORTAL, a.COD_STATUS, a.NUM_QTD_PROCESSADOS } into g
                   select new PRE_VIEW_ARQ_PATROCINA
                   {
                       COD_ARQ_PAT = g.Key.COD_ARQ_PAT,
                       NOM_ARQUIVO = g.Key.NOM_ARQUIVO,
                       TIP_ARQUIVO = g.Key.TIP_ARQUIVO,
                       DCR_TIPO = g.Key.DCR_TIPO,
                       NUM_HASH = g.Key.NUM_HASH,
                       LOG_INCLUSAO = g.Key.LOG_INCLUSAO,
                       DTH_INCLUSAO = g.Key.DTH_INCLUSAO,
                       ANO_REF = g.Key.ANO_REF,
                       MES_REF = g.Key.MES_REF,
                       DAT_REPASSE = null,
                       DAT_CREDITO = null,
                       GRUPO_PORTAL = g.Key.GRUPO_PORTAL,
                       COD_STATUS = g.Key.COD_STATUS,
                       DCR_STATUS = g.Key.DCR_STATUS,
                       NUM_QTD_REGISTROS = g.Sum(s => s.NUM_QTD_REGISTROS),
                       NUM_QTD_VALIDOS = g.Sum(s => s.NUM_QTD_REGISTROS - s.NUM_QTD_ERROS_LINHAS),
                       NUM_QTD_ERROS = g.Sum(s => s.NUM_QTD_ERROS + s.NUM_QTD_ERROS_LINHAS),
                       NUM_QTD_ERROS_LINHAS = g.Sum(s => s.NUM_QTD_ERROS_LINHAS),
                       NUM_QTD_ALERTAS = g.Sum(s => s.NUM_QTD_ALERTAS + s.NUM_QTD_ALERTAS_LINHAS),
                       NUM_QTD_ALERTAS_LINHAS = g.Sum(s => s.NUM_QTD_ALERTAS_LINHAS),
                       NUM_QTD_IMPORTADOS = g.Sum(s => s.NUM_QTD_IMPORTADOS),
                       NUM_QTD_PROCESSADOS = g.Key.NUM_QTD_PROCESSADOS
                   };
        }

        public IQueryable<PRE_VIEW_ARQ_PATROCINA> qry_DEMONSTRATIVOS_ANTERIORES(IQueryable<PRE_VIEW_ARQ_PATROCINA> query)
        {
            return from a in (query)
                   //let d = (from demon in m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA select demon)
                   join d in
                       (from dd in m_DbContext.PRE_TBL_ARQ_PAT_DEMONSTRA
                        group dd by new { dd.MES_REF, dd.ANO_REF, dd.GRUPO_PORTAL } into g                        
                        select new PRE_VIEW_ARQ_PAT_DEMONSTRA
                        {
                            MES_REF = g.Key.MES_REF,
                            ANO_REF = g.Key.ANO_REF,
                            DAT_REPASSE = g.Max(x => x.DAT_REPASSE),
                            DAT_CREDITO = g.Max(x => x.DAT_CREDITO),
                            GRUPO_PORTAL = g.Key.GRUPO_PORTAL
                        }
                        ) on new { a.MES_REF, a.ANO_REF, a.GRUPO_PORTAL } equals new { d.MES_REF, d.ANO_REF, d.GRUPO_PORTAL }
                        into leftjoin
                   from dd in leftjoin.DefaultIfEmpty()
                   select new PRE_VIEW_ARQ_PATROCINA
                   {
                       COD_ARQ_PAT = a.COD_ARQ_PAT,
                       NOM_ARQUIVO = a.NOM_ARQUIVO,
                       TIP_ARQUIVO = a.TIP_ARQUIVO,
                       DCR_TIPO = a.DCR_TIPO,
                       NUM_HASH = a.NUM_HASH,
                       LOG_INCLUSAO = a.LOG_INCLUSAO,
                       DTH_INCLUSAO = a.DTH_INCLUSAO,
                       ANO_REF = a.ANO_REF,
                       MES_REF = a.MES_REF,
                       DAT_REPASSE = dd.DAT_REPASSE,
                       DAT_CREDITO = dd.DAT_CREDITO,
                       GRUPO_PORTAL = a.GRUPO_PORTAL,
                       COD_STATUS = a.COD_STATUS,
                       DCR_STATUS = a.DCR_STATUS,
                       NUM_QTD_REGISTROS = a.NUM_QTD_REGISTROS,
                       NUM_QTD_VALIDOS = a.NUM_QTD_VALIDOS,
                       NUM_QTD_ERROS = a.NUM_QTD_ERROS ?? 0,
                       NUM_QTD_ERROS_LINHAS = a.NUM_QTD_ERROS_LINHAS ?? 0,
                       NUM_QTD_ALERTAS = a.NUM_QTD_ALERTAS ?? 0,
                       NUM_QTD_ALERTAS_LINHAS = a.NUM_QTD_ALERTAS_LINHAS ?? 0,
                       NUM_QTD_IMPORTADOS = a.NUM_QTD_IMPORTADOS ?? 0,
                       NUM_QTD_PROCESSADOS = a.NUM_QTD_PROCESSADOS
                   };
        }

        public IQueryable<PRE_VIEW_ARQ_PATROCINA> GetGroupBy_COD_EMPRS(string pGruposAcesso, string pGrupo)
        {

            string[] aGrupos = pGruposAcesso.Split(',');

            IQueryable<PRE_VIEW_ARQ_PATROCINA> query;
            query = from a in m_DbContext.PRE_TBL_ARQ_PATROCINA
                    from l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA
                    where (a.COD_ARQ_PAT == l.COD_ARQ_PAT)
                       && (aGrupos.Contains(a.GRUPO_PORTAL) || aGrupos.Contains("9999"))
                       && (a.GRUPO_PORTAL == pGrupo || pGrupo == "9999")
                    group a by new { l.COD_EMPRS } into g
                    select new PRE_VIEW_ARQ_PATROCINA()
                    {
                        COD_ARQ_PAT = 0,
                        COD_EMPRS = g.Key.COD_EMPRS,
                        NUM_QTD_REGISTROS = g.Count()
                    };
            return query;
        }

        public int GetDataCountGroupBy(string pGruposAcesso, int pExibir, int pAgrupar, string pGrupo)
        {
            return ((pAgrupar == 1) ? GetGroupBy_ARQUIVO(pExibir, pGruposAcesso, pGrupo) : GetGroupBy_COD_EMPRS(pGruposAcesso, pGrupo)).SelectCount();
        }

        public List<String> GetCOD_EMPRSs(long pCOD_ARQ_PAT)
        {
            IQueryable<String> query;
            query = from l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA
                    where (l.COD_ARQ_PAT == pCOD_ARQ_PAT)
                    group l by new { l.COD_EMPRS } into g
                    select g.Key.COD_EMPRS;
            return query.ToList();
        }

        //public IQueryable<PRE_VIEW_ARQ_PATROCINA> GetByStatus(short pStatus, string pGruposAcesso, string pGrupo)
        public List<PRE_VIEW_ARQ_PATROCINA> GetByStatus(short pStatus)
        {
            IQueryable<PRE_VIEW_ARQ_PATROCINA> query;
            query = qry_NUM_QTD_REGISTROS(new string[] { "9999" }, "9999");
            query = qry_DEMONSTRATIVOS_ANTERIORES(query);
            query = from a in query
                    where (a.COD_STATUS == pStatus)
                    select a;
            return query.ToList();
        }

        public List<PRE_VIEW_ARQ_RECEBIDO_CONTROLE> GetDataControle2(int startRowIndex, int maximumRows, short? grupo, string sortParameter)
        {
            return GetWhereControle(grupo)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int GetDataControleCount(short? ano, short? mes)
        {
            return GetWhereControle(null).SelectCount();
        }

        public IQueryable<PRE_VIEW_ARQ_RECEBIDO_CONTROLE> GetWhereControle(short? grupo)
        {
            IQueryable<PRE_VIEW_ARQ_RECEBIDO_CONTROLE> query;

            query = from a in m_DbContext.PRE_TBL_GRUPO_EMPRS
                    where a.COD_GRUPO_EMPRS != 41
                       && a.COD_GRUPO_EMPRS != 51
                       && (a.COD_GRUPO_EMPRS == grupo || grupo == null)
                    select new PRE_VIEW_ARQ_RECEBIDO_CONTROLE()
                    {
                        COD_GRUPO_EMPRS = a.COD_GRUPO_EMPRS,
                        DCR_GRUPO_EMPRS = a.DCR_GRUPO_EMPRS,
                        GRUPO_PORTAL = a.GRUPO_PORTAL,
                        QTD_CADASTRAL_VALIDADO = 0,
                        QTD_AFASTAMENTO_VALIDADO = 0,
                        QTD_ORGAO_LOTACAO_VALIDADO = 0,
                        QTD_FINANCEIRO_VALIDADO = 0,
                        QTD_CADASTRAL_CARREGADO = 0,
                        QTD_AFASTAMENTO_CARREGADO = 0,
                        QTD_ORGAO_LOTACAO_CARREGADO = 0,
                        QTD_FINANCEIRO_CARREGADO = 0
                    };

            return query;
        }

        //public string[] ConverteGrupoEmpresas(string pGrupos)
        //{
        //    string[] aGrupos = pGrupos.Split(',');

        //    if (aGrupos != null)
        //    {
        //        string formatado = String.Empty;
        //        foreach (string cod_emprs in aGrupos)
        //        {
        //            formatado += cod_emprs.PadLeft(3, '0') + ",";
        //        }
        //        aGrupos = formatado.Split(',');
        //    }
        //}

        public Resultado LOG_InsertData(PRE_TBL_ARQ_PATROCINA_LOG newLog)
        {

            Resultado res = new Entidades.Resultado();
            try
            {
                m_DbContext.PRE_TBL_ARQ_PATROCINA_LOG.Add(newLog);
                if (m_DbContext.SaveChanges() > 0)
                {
                    res.Sucesso("Registro inserido com sucesso.");
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

        public Resultado Delete(PRE_TBL_ARQ_PATROCINA oldArqPatrocina)
        {
            Resultado res = new Resultado();
            if (oldArqPatrocina != null)
            {
                try
                {
                    //oldArqPatrocina.PRE_TBL_ARQ_PATROCINA_LINHA.ToList().ForEach(p => { m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA.Remove(p); });
                    //oldArqPatrocina.PRE_TBL_ARQ_PATROCINA_LINHA.ToList().ForEach(p =>
                    //{
                    //    m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA " +
                    //                                           " WHERE COD_ARQ_PAT_LINHA = " + p.COD_ARQ_PAT_LINHA.ToString(), 0);
                    //});

                    //Exclui criticas do arquivo:
                    m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA " +
                                                           " WHERE COD_ARQ_PAT = " + oldArqPatrocina.COD_ARQ_PAT.ToString(), 0);

                    //Exclui todas criticas das linhas:
                    m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA   " +
                                                           " WHERE COD_ARQ_PAT_LINHA IN (SELECT L.COD_ARQ_PAT_LINHA " +
                                                           "                               FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_LINHA L " +
                                                           "                              INNER JOIN OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA C ON L.COD_ARQ_PAT_LINHA = C.COD_ARQ_PAT_LINHA " +
                                                           "                              WHERE L.COD_ARQ_PAT = " + oldArqPatrocina.COD_ARQ_PAT.ToString() + ")", 0);

                    //Exclui criticas da carga :
                    m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA   " +
                                                           " WHERE COD_ARQ_PAT_CARGA IN (SELECT C.COD_ARQ_PAT_CARGA " +
                                                           "                               FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CARGA C " +
                                                           "                              WHERE C.COD_ARQ_PAT = " + oldArqPatrocina.COD_ARQ_PAT.ToString() + ")", 0);

                    //Exclui dados da carga:
                    m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CARGA " +
                                                           " WHERE COD_ARQ_PAT = " + oldArqPatrocina.COD_ARQ_PAT.ToString(), 0);

                    //Exclui dados do demonstrativo:
                    m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PAT_DEMONSTRA_DET   " +
                                                           " WHERE COD_ARQ_PAT_LINHA IN (SELECT L.COD_ARQ_PAT_LINHA " +
                                                           "                               FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_LINHA L " +
                                                           "                              WHERE L.COD_ARQ_PAT = " + oldArqPatrocina.COD_ARQ_PAT.ToString() + ")", 0);

                    //Exclui linhas:
                    m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_LINHA " +
                                                           " WHERE COD_ARQ_PAT = " + oldArqPatrocina.COD_ARQ_PAT.ToString(), 0);


                    //Exclui log:
                    //m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_LOG " +
                    //                                       " WHERE COD_ARQ_PAT = " + oldArqPatrocina.COD_ARQ_PAT.ToString(), 0);

                    //Exclui arquivo:
                    //m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA " +
                    //                                       " WHERE COD_ARQ_PAT = " + oldArqPatrocina.COD_ARQ_PAT.ToString(), 0);

                    //
                    // Hard Delete da erro. Solução:
                    //
                    oldArqPatrocina.PRE_TBL_ARQ_PATROCINA_LOG.ToList().ForEach(l =>                    
                        m_DbContext.PRE_TBL_ARQ_PATROCINA_LOG.Remove(l)
                    );

                    m_DbContext.PRE_TBL_ARQ_PATROCINA.Remove(oldArqPatrocina);
                    int rows_deleted = m_DbContext.SaveChanges();
                    if (rows_deleted > 0)
                    {
                        res.Sucesso("Arquivo excluído.");
                    }
                }
                catch (Exception Ex)
                {
                    res.Erro(Util.GetInnerException(Ex));
                }
            }
            else
            {
                res.Sucesso("Ok");
            }
            return res;
        }

        //public Resultado Desativar(PRE_TBL_ARQ_PATROCINA dArqPatrocina)
        //{
        //    Resultado res = new Resultado();
        //    if (dArqPatrocina != null)
        //    {
        //        try
        //        {
        //            res = DeleteHard(dArqPatrocina, false);
        //            var atualiza = m_DbContext.PRE_TBL_ARQ_PATROCINA.FirstOrDefault(p => p.COD_ARQ_PAT == dArqPatrocina.COD_ARQ_PAT);
        //            if (atualiza != null)
        //            {
        //                atualiza = uptArqPatrocina.DTH_EXCLUSAO;
        //                //atualiza.ANO_REF = uptArqPatrocina.ANO_REF;                    
        //                m_DbContext.Entry(atualiza).CurrentValues.SetValues(dArqPatrocina);
        //                m_DbContext.SaveChanges();
        //            }
        //        }
        //        catch (Exception Ex)
        //        {
        //            res.Erro(Util.GetInnerException(Ex));
        //        }
        //    }
        //    else
        //    {
        //        res.Sucesso("Ok");
        //    }
        //    return res;
        //}

        public List<PRE_TBL_ARQ_PATROCINA_CRITICA> CRITICA_GetData(int startRowIndex, int maximumRows, int? pCOD_ARQ_PAT, short? pTIP_CRITICA, string sortParameter)
        {
            return CRITICA_GetBy_COD_ARQ_PAT(pCOD_ARQ_PAT, pTIP_CRITICA)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int CRITICA_GetDataCountGroup(int pCOD_ARQ_PAT, short? pTIP_CRITICA)
        {
            return CRITICA_GetBy_COD_ARQ_PAT(pCOD_ARQ_PAT, pTIP_CRITICA).SelectCount();
        }

        public List<PRE_VIEW_ARQ_PATROCINA_CRITICA> CRITICA_GetAllData(int startRowIndex, int maximumRows, int? pCOD_ARQ_PAT, short? pTIP_CRITICA, string sortParameter)
        {
            return CRITICA_GetAllBy_COD_ARQ_PAT(pCOD_ARQ_PAT, pTIP_CRITICA)
                   .GetData(startRowIndex, maximumRows, sortParameter)
                   .ToList();
        }

        public int CRITICA_GetAll_Count(int pCOD_ARQ_PAT, short? pTIP_CRITICA)
        {
            return CRITICA_GetAllBy_COD_ARQ_PAT(pCOD_ARQ_PAT, pTIP_CRITICA).SelectCount();
        }

        public List<PRE_TBL_ARQ_PATROCINA_LINHA> LINHA_GetData(int startRowIndex, int maximumRows, int? pCOD_ARQ_PAT, short? pTIP_CRITICA, string sortParameter)
        {
            return LINHA_ERRO_GetBy_COD_ARQ_PAT(pCOD_ARQ_PAT, pTIP_CRITICA)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int LINHA_GetDataCountGroup(int pCOD_ARQ_PAT, short? pTIP_CRITICA)
        {
            return LINHA_ERRO_GetBy_COD_ARQ_PAT(pCOD_ARQ_PAT, pTIP_CRITICA).SelectCount();
        }

        public List<PRE_TBL_ARQ_PATROCINA_LINHA> LINHA_GetAllByCOD_ARQ_PAT(int pCOD_ARQ_PAT, bool Apenas_nao_importadas = false)
        {
            IQueryable<PRE_TBL_ARQ_PATROCINA_LINHA> query;
            query = from a in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA
                    where (a.COD_ARQ_PAT == pCOD_ARQ_PAT)
                       && (a.DAT_IMPORTADO == null && Apenas_nao_importadas || Apenas_nao_importadas == false)
                    orderby a.NUM_LINHA descending
                    select a;
            return query.ToList();
        }

        public IQueryable<PRE_TBL_ARQ_PATROCINA_LINHA> LINHA_ERRO_GetBy_COD_ARQ_PAT(int? pCOD_ARQ_PAT, short? pTIP_CRITICA)
        {
            IQueryable<PRE_TBL_ARQ_PATROCINA_LINHA> query;
            query = from l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA
                    join ll in
                        (from l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA
                         join c in m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA on l.COD_ARQ_PAT_LINHA equals c.COD_ARQ_PAT_LINHA
                         where (l.COD_ARQ_PAT == pCOD_ARQ_PAT || pCOD_ARQ_PAT == null)
                            && (c.TIP_CRITICA == pTIP_CRITICA || pTIP_CRITICA == null)
                         group c by l into g
                         select new
                         {
                             COD_ARQ_PAT = g.Key.COD_ARQ_PAT,
                             COD_ARQ_PAT_LINHA = g.Key.COD_ARQ_PAT_LINHA,
                             NUM_LINHA = g.Key.NUM_LINHA
                         }) on l.COD_ARQ_PAT_LINHA equals ll.COD_ARQ_PAT_LINHA
                    select l;
            return query;
        }

        public IQueryable<PRE_TBL_ARQ_PATROCINA_CRITICA> CRITICA_GetBy_COD_ARQ_PAT(int? pCOD_ARQ_PAT, short? pTIP_CRITICA)
        {
            IQueryable<PRE_TBL_ARQ_PATROCINA_CRITICA> query;
            query = from c in m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA
                    where (c.COD_ARQ_PAT == pCOD_ARQ_PAT || pCOD_ARQ_PAT == null)
                       && (c.TIP_CRITICA == pTIP_CRITICA || pTIP_CRITICA == null)
                    select c;
            return query;
        }

        public IQueryable<PRE_VIEW_ARQ_PATROCINA_CRITICA> CRITICA_GetAllBy_COD_ARQ_PAT(int? pCOD_ARQ_PAT, short? pTIP_CRITICA)
        {
            IQueryable<PRE_VIEW_ARQ_PATROCINA_CRITICA> query;

            query = from c in m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA
                   where (c.COD_ARQ_PAT == pCOD_ARQ_PAT || pCOD_ARQ_PAT == null)
                      && (c.TIP_CRITICA == pTIP_CRITICA || pTIP_CRITICA == null)
                  select new PRE_VIEW_ARQ_PATROCINA_CRITICA()
                      {
                          COD_ARQ_PAT_CRITICA = c.COD_ARQ_PAT_CRITICA,
                          NUM_LINHA = null,
                          TIP_LINHA = null,
                          NUM_POSICAO = c.NUM_POSICAO,
                          NOM_CAMPO = c.NOM_CAMPO,
                          COD_CRITICA = c.COD_CRITICA,
                          DCR_CRITICA = c.DCR_CRITICA,                          
                          TIP_CRITICA = c.TIP_CRITICA,                          
                          COD_EMPRS = c.REF_1,
                          NUM_RGTRO_EMPRG = c.REF_2,
                          COD_VERBA = c.REF_3,
                      };

            query = query.Union(from c in m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA
                                join ll in
                               (from l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA
                         where (l.COD_ARQ_PAT == pCOD_ARQ_PAT || pCOD_ARQ_PAT == null)
                         select l) on c.COD_ARQ_PAT_LINHA equals ll.COD_ARQ_PAT_LINHA
                    where (c.TIP_CRITICA == pTIP_CRITICA || pTIP_CRITICA == null)
                    select new PRE_VIEW_ARQ_PATROCINA_CRITICA()
                      {
                          COD_ARQ_PAT_CRITICA = c.COD_ARQ_PAT_CRITICA,
                          NUM_LINHA = ll.NUM_LINHA,
                          TIP_LINHA = ll.TIP_LINHA,
                          NUM_POSICAO = c.NUM_POSICAO,
                          NOM_CAMPO = c.NOM_CAMPO,    
                          COD_CRITICA = c.COD_CRITICA,
                          DCR_CRITICA = c.DCR_CRITICA,                                                
                          TIP_CRITICA = c.TIP_CRITICA,
                          COD_EMPRS = c.REF_1 ?? ll.COD_EMPRS,
                          NUM_RGTRO_EMPRG = c.REF_2 ?? ll.NUM_RGTRO_EMPRG,
                          COD_VERBA = c.REF_3
                      });

            return query;
        }

        public List<PRE_TBL_ARQ_PATROCINA_CRITICA> CRITICA_LINHAS_GetBy_COD_ARQ_PAT(int? pCOD_ARQ_PAT, short? pTIP_CRITICA = null)
        {

            IQueryable<PRE_TBL_ARQ_PATROCINA_CRITICA> query;
            query = from c in m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA
                    join ll in
                        (from l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA
                         join a in m_DbContext.PRE_TBL_ARQ_PATROCINA on l.COD_ARQ_PAT equals a.COD_ARQ_PAT 
                         where (a.COD_ARQ_PAT == pCOD_ARQ_PAT || pCOD_ARQ_PAT == null)
                         select l) on c.COD_ARQ_PAT_LINHA equals ll.COD_ARQ_PAT_LINHA
                    select c;

            return query.ToList();

            //IQueryable<PRE_TBL_ARQ_PATROCINA_CRITICA> query;
            //query = from c in m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA
            //        where (c.COD_ARQ_PAT == pCOD_ARQ_PAT || pCOD_ARQ_PAT == null)
            //           && (c.TIP_CRITICA == pTIP_CRITICA || pTIP_CRITICA == null)
            //        select c;
            //return query;
        }

        public long GetMaxPk_LINHA()
        {
            long maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_ARQ_PAT_LINHA);
            return maxPK;
        }

        public PRE_TBL_ARQ_PATROCINA_TIPO GetTipoByTam(int TamLinha)
        {
            IQueryable<PRE_TBL_ARQ_PATROCINA_TIPO> query;
            query = from t in m_DbContext.PRE_TBL_ARQ_PATROCINA_TIPO
                    where t.NUM_TAM_LINHA < TamLinha
                    orderby t.NUM_TAM_LINHA descending
                    select t;

            return query.ToList().FirstOrDefault();
        }

        public List<PRE_TBL_ARQ_PATROCINA_LOG> GetLogBy(long pCOD_ARQ_PAT)
        {
            IQueryable<PRE_TBL_ARQ_PATROCINA_LOG> query;
            query = from l in m_DbContext.PRE_TBL_ARQ_PATROCINA_LOG
                    where (l.COD_ARQ_PAT==pCOD_ARQ_PAT)
                    orderby l.DTH_INCLUSAO descending
                    select l;

            return query.ToList();
        }


        internal void Refresh()
        {
            m_DbContext = new PREV_Entity_Conn(); 
        }

        public Resultado InsertLogArq(PRE_TBL_ARQ_PATROCINA newLog)
        {
            Resultado res = new Resultado();

            try
            {
                int insere = m_DbContext.Database.ExecuteSqlCommand("insert into OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_LOG (COD_ARQ_PAT,COD_ACAO,DTH_INCLUSAO,LOG_INCLUSAO)  values (" +
                        newLog.COD_ARQ_PAT + "," +
                        newLog.COD_STATUS + "," +
                        //"to_date('" + newLog.DTH_INCLUSAO.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS'),'" +
                        "to_date('" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS'),'" +
                        newLog.LOG_INCLUSAO + "')");

                if (insere != 0)
                {
                    res.Sucesso("Registro inserido com sucesso.");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
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

    }
}