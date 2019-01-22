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
    public class ArqPatrocinadoraEnvioDAL
    {

        #region .: Views :.
        public class PRE_TBL_ARQ_ENVIO_View
        {
            public int COD_ARQ_ENVIO { get; set; }
            public short COD_ARQ_ENVIO_TIPO { get; set; }
            public Nullable<int> COD_ARQ_SUB_TIPO { get; set; }
            public string DCR_ARQ_ENVIO { get; set; }
            public string DCR_ARQ_EXT { get; set; }
            public Nullable<short> ANO_REF { get; set; }
            public Nullable<short> MES_REF { get; set; }
            public Nullable<short> COD_GRUPO_EMPRS { get; set; }
            public string DCR_GRUPO_EMPRS { get; set; }
            public Nullable<short> COD_ARQ_AREA_ORIG { get; set; }
            public Nullable<short> COD_ARQ_AREA_DEST { get; set; }
            public byte[] DAT_ARQUIVO { get; set; }
            public string DCR_CAMINHO_ARQUIVO { get; set; }
            public Nullable<int> COD_ARQ_ENVIO_PAI { get; set; }
            public System.DateTime DTH_INCLUSAO { get; set; }
            public string LOG_INCLUSAO { get; set; }
            public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
            public string LOG_EXCLUSAO { get; set; }
            public string DCR_ARQ_STATUS { get; set; }
            public short? COD_ARQ_STATUS { get; set; }
            public string DCR_ARQ_ENVIO_TIPO { get; set; }
            public int? COD_ARQ_ENV_REPASSE { get; set; }
        }
        public class PRE_TBL_ARQ_ENVIO_HIST_View
        {
            //private short? cod_arq_status;
            public long? COD_ARQ_ENVIO { get; set; }
            public short? COD_ARQ_STATUS { get; set; }
            public string DCR_ARQ_STATUS { get; set; }
            public DateTime DTH_INCLUSAO { get; set; }
        }
        public class FCESP_GRUPO_EMP_View
        {
            public string NOM_ABRVO_EMPRS { get; set; }
            public int EMPRESA { get; set; }
        }
        public class PRE_TBL_ARQ_AREA_View
        {
            public short COD_ARQ_AREA { get; set; }
            public string DCR_ARQ_AREA { get; set; }
            public string DCR_ARQ_SUB_AREA { get; set; }
            public string DCR_ARQ_C_AREA_SUB
            {
                get
                {
                    return String.Format("{0}    {1}", DCR_ARQ_AREA, DCR_ARQ_SUB_AREA);
                }
            }
        }
        public class PRE_TBL_ARQ_ENV_REPASSE_View
        {
            public int COD_ARQ_ENV_REPASSE { get; set; }
            public string DCR_ARQ_ENV_REPASSE { get; set; }
            public Nullable<short> ANO_REF { get; set; }
            public Nullable<short> MES_REF { get; set; }
            public Nullable<short> COD_GRUPO_EMPRS { get; set; }
            public string DCR_GRUPO_EMPRS { get; set; }
            public Nullable<short> COD_ARQ_AREA { get; set; }
            public Nullable<long> COD_ARQ_ENVIO { get; set; }
            public DateTime DTH_INCLUSAO { get; set; }
            public string LOG_INCLUSAO { get; set; }
            public string DCR_ARQ_STATUS { get; set; }
            public short? COD_ARQ_STATUS { get; set; }
        }

        public class PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA
        {
            public short COD_ARQ_AREA { get; set; }
            public string DCR_ARQ_AREA { get; set; }
            public string DCR_ARQ_SUB_AREA { get; set; }
            public string DCR_ARQ_ENVIADOS { get; set; }
            //public System.DateTime? DTH_GERADO { get; set; }
            //public System.DateTime? DTH_ENVIADO { get; set; }
            public Nullable<short> ANO_REF { get; set; }
            public Nullable<short> MES_REF { get; set; }
            public int QTD_GERADOS { get; set; }
            public int QTD_ENVIADOS { get; set; }
            public int QTD_PUBLICADOS { get; set; }
            public short? COD_ARQ_ENVIADOS_STATUS { get; set; }
            public string DCR_ARQ_AREA_SUB_AREA
            {
                get
                {
                    string strMask = (String.IsNullOrEmpty(DCR_ARQ_SUB_AREA) ? "{0}-{1}" : "{0}-{1}-{2}");
                    return String.Format(strMask, COD_ARQ_AREA, DCR_ARQ_AREA, DCR_ARQ_SUB_AREA);
                }
            }

            public List<PRE_TBL_ARQ_ENVIO_View> lstARQUIVOS_ENVIO { get; set; }
        }

        #endregion

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        #region .: Pesquisa :.
        public List<PRE_TBL_ARQ_ENVIO_View> GetData(int startRowIndex, int maximumRows, short? mes, short? ano, DateTime? datIni, DateTime? datFim, short? grupo, int? status, short? area, string referencia, int? tipoEnvio, string sortParameter)
        {
            return GetWhere(mes, ano, datIni, datFim, grupo, status, area, referencia, tipoEnvio)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }
        public int GetDataCount(short? mes, short? ano, DateTime? datIni, DateTime? datFim, short? grupo, int? status, short? area, string referencia, int? tipoEnvio)
        {
            return GetWhere(mes, ano, datIni, datFim, grupo, status, area, referencia, tipoEnvio).Count();
        }

        public IQueryable<PRE_TBL_ARQ_ENVIO_View> GetWhere(short? mes, short? ano, DateTime? datIni, DateTime? datFim, short? grupo, int? status, short? area, string referencia, int? tipoEnvio)
        {
            IQueryable<PRE_TBL_ARQ_ENVIO_View> query;

            query = from tb in m_DbContext.PRE_TBL_ARQ_ENVIO
                    join d in
                        (from dd in m_DbContext.PRE_TBL_ARQ_ENVIO_HIST
                         join s in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS on dd.COD_ARQ_STATUS equals s.COD_ARQ_STATUS
                         select new PRE_TBL_ARQ_ENVIO_HIST_View()
                         {
                             COD_ARQ_ENVIO = dd.COD_ARQ_ENVIO,
                             COD_ARQ_STATUS = s.COD_ARQ_STATUS,
                             DCR_ARQ_STATUS = s.DCR_ARQ_STATUS,
                             DTH_INCLUSAO = dd.DTH_INCLUSAO
                         }
                         ) on tb.COD_ARQ_ENVIO equals d.COD_ARQ_ENVIO
                     into leftjoin
                    from dd in leftjoin.DefaultIfEmpty()
                    join pp in
                        (
                            from ff in m_DbContext.PRE_TBL_ARQ_ENVIO_TIPO
                            select ff
                            ) on tb.COD_ARQ_ENVIO_TIPO equals pp.COD_ARQ_ENVIO_TIPO
                    where (dd.DTH_INCLUSAO == m_DbContext.PRE_TBL_ARQ_ENVIO_HIST.Where(h => h.COD_ARQ_ENVIO == tb.COD_ARQ_ENVIO).Max(h => h.DTH_INCLUSAO) || dd.COD_ARQ_ENVIO == null)
                     && (tb.COD_ARQ_ENVIO_TIPO == tipoEnvio || tipoEnvio == null)
                     && (tb.MES_REF == mes || mes == null)
                     && (tb.ANO_REF == ano || ano == null)
                     && (tb.COD_GRUPO_EMPRS == grupo || grupo == null)
                     && (dd.COD_ARQ_STATUS == status || status == null || (dd.COD_ARQ_STATUS == null && status == 1))
                     && (tb.COD_ARQ_AREA_ORIG == area || area == null)
                     && (tb.DCR_ARQ_ENVIO.Contains(referencia) || referencia == null || referencia == "")
                     && (tb.DTH_INCLUSAO >= datIni && datFim == null || tb.DTH_INCLUSAO <= datFim && datIni == null || tb.DTH_INCLUSAO >= datIni && tb.DTH_INCLUSAO <= datFim ||
                     datIni == null && datFim == null)
                     && (tb.DTH_EXCLUSAO == null)
                     && (tb.LOG_EXCLUSAO == null)
                    select new PRE_TBL_ARQ_ENVIO_View()
                    {
                        COD_ARQ_ENVIO = tb.COD_ARQ_ENVIO,
                        COD_ARQ_ENVIO_TIPO = tb.COD_ARQ_ENVIO_TIPO,
                        COD_ARQ_SUB_TIPO = tb.COD_ARQ_SUB_TIPO,
                        DCR_ARQ_ENVIO = tb.DCR_ARQ_ENVIO,
                        DCR_ARQ_EXT = tb.DCR_ARQ_EXT,
                        ANO_REF = tb.ANO_REF,
                        MES_REF = tb.MES_REF,
                        COD_GRUPO_EMPRS = tb.COD_GRUPO_EMPRS,
                        COD_ARQ_AREA_DEST = tb.COD_ARQ_AREA_DEST,
                        COD_ARQ_AREA_ORIG = tb.COD_ARQ_AREA_ORIG,
                        DAT_ARQUIVO = tb.DAT_ARQUIVO,
                        DCR_CAMINHO_ARQUIVO = tb.DCR_CAMINHO_ARQUIVO,
                        COD_ARQ_ENVIO_PAI = tb.COD_ARQ_ENVIO_PAI,
                        DTH_INCLUSAO = tb.DTH_INCLUSAO,
                        LOG_INCLUSAO = tb.LOG_INCLUSAO,
                        DTH_EXCLUSAO = tb.DTH_EXCLUSAO,
                        LOG_EXCLUSAO = tb.LOG_EXCLUSAO,
                        COD_ARQ_STATUS = dd.COD_ARQ_STATUS ?? 1,
                        DCR_ARQ_STATUS = dd.DCR_ARQ_STATUS ?? "Novo",
                        DCR_ARQ_ENVIO_TIPO = pp.DCR_ARQ_ENVIO_TIPO
                    };
            return query;
        }
        public List<PRE_TBL_ARQ_ENVIO_STATUS> GetStatusDdl()
        {
            IQueryable<PRE_TBL_ARQ_ENVIO_STATUS> query;

            query = from ptaes in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS
                    select ptaes;

            return query.ToList();
        }
        public List<PRE_TBL_GRUPO_EMPRS> GetGrupoDdl()
        {
            IQueryable<PRE_TBL_GRUPO_EMPRS> query;

            query = from ptge in m_DbContext.PRE_TBL_GRUPO_EMPRS
                    where ptge.COD_GRUPO_EMPRS != 51
                       && ptge.COD_GRUPO_EMPRS != 41
                    select ptge;

            return query.ToList();
        }
        public List<PRE_TBL_ARQ_ENVIO_TIPO> GetTipoEnvioDdl()
        {
            IQueryable<PRE_TBL_ARQ_ENVIO_TIPO> query;

            query = from ge in m_DbContext.PRE_TBL_ARQ_ENVIO_TIPO
                    select ge;

            return query.ToList();
        }

        public PRE_TBL_ARQ_ENVIO_View GetLinha(int codigo)
        {
            IQueryable<PRE_TBL_ARQ_ENVIO_View> query;

            query = from tb in m_DbContext.PRE_TBL_ARQ_ENVIO
                    join d in
                        (from dd in m_DbContext.PRE_TBL_ARQ_ENVIO_HIST
                         join s in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS on dd.COD_ARQ_STATUS equals s.COD_ARQ_STATUS
                         select new PRE_TBL_ARQ_ENVIO_HIST_View()
                         {
                             COD_ARQ_ENVIO = dd.COD_ARQ_ENVIO,
                             COD_ARQ_STATUS = s.COD_ARQ_STATUS,
                             DCR_ARQ_STATUS = s.DCR_ARQ_STATUS,
                             DTH_INCLUSAO = dd.DTH_INCLUSAO
                         }
                         ) on tb.COD_ARQ_ENVIO equals d.COD_ARQ_ENVIO
                     into leftjoin
                    from dd in leftjoin.DefaultIfEmpty()
                    join pp in
                        (
                            from ff in m_DbContext.PRE_TBL_ARQ_ENVIO_TIPO
                            select ff
                            ) on tb.COD_ARQ_ENVIO_TIPO equals pp.COD_ARQ_ENVIO_TIPO
                    where (dd.DTH_INCLUSAO == m_DbContext.PRE_TBL_ARQ_ENVIO_HIST.Where(h => h.COD_ARQ_ENVIO == tb.COD_ARQ_ENVIO).Max(h => h.DTH_INCLUSAO) || dd.COD_ARQ_ENVIO == null)
                       && (tb.COD_ARQ_ENVIO == codigo)
                    select new PRE_TBL_ARQ_ENVIO_View()
                    {
                        COD_ARQ_ENVIO = tb.COD_ARQ_ENVIO,
                        COD_ARQ_ENVIO_TIPO = tb.COD_ARQ_ENVIO_TIPO,
                        COD_ARQ_SUB_TIPO = tb.COD_ARQ_SUB_TIPO,
                        DCR_ARQ_ENVIO = tb.DCR_ARQ_ENVIO,
                        DCR_ARQ_EXT = tb.DCR_ARQ_EXT,
                        ANO_REF = tb.ANO_REF,
                        MES_REF = tb.MES_REF,
                        COD_GRUPO_EMPRS = tb.COD_GRUPO_EMPRS,
                        COD_ARQ_AREA_DEST = tb.COD_ARQ_AREA_DEST,
                        COD_ARQ_AREA_ORIG = tb.COD_ARQ_AREA_ORIG,
                        DAT_ARQUIVO = tb.DAT_ARQUIVO,
                        DCR_CAMINHO_ARQUIVO = tb.DCR_CAMINHO_ARQUIVO,
                        COD_ARQ_ENVIO_PAI = tb.COD_ARQ_ENVIO_PAI,
                        DTH_INCLUSAO = tb.DTH_INCLUSAO,
                        LOG_INCLUSAO = tb.LOG_INCLUSAO,
                        DTH_EXCLUSAO = tb.DTH_EXCLUSAO,
                        LOG_EXCLUSAO = tb.LOG_EXCLUSAO,
                        COD_ARQ_STATUS = dd.COD_ARQ_STATUS ?? 1,
                        DCR_ARQ_STATUS = dd.DCR_ARQ_STATUS ?? "Novo",
                        DCR_ARQ_ENVIO_TIPO = pp.DCR_ARQ_ENVIO_TIPO
                    };
            return query.FirstOrDefault();
        }

        public Resultado ExcluirEnvio(int pCOD_ARQ_ENVIO, DateTime pDTH_EXCLUSAO, string pLOG_EXCLUSAO, short pCOD_ARQ_STATUS = 6)
        {
            Resultado res = new Resultado();

            try
            {
                var deletaEnvio = m_DbContext.PRE_TBL_ARQ_ENVIO.Find(pCOD_ARQ_ENVIO);

                if (deletaEnvio != null)
                {
                    deletaEnvio.LOG_EXCLUSAO = pLOG_EXCLUSAO;
                    deletaEnvio.DTH_EXCLUSAO = pDTH_EXCLUSAO;
                    int rows_deleted = m_DbContext.SaveChanges();

                    PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
                    envioHist.COD_ARQ_ENVIO = pCOD_ARQ_ENVIO;
                    envioHist.COD_ARQ_STATUS = pCOD_ARQ_STATUS;
                    envioHist.DTH_INCLUSAO = pDTH_EXCLUSAO;
                    envioHist.LOG_INCLUSAO = pLOG_EXCLUSAO;
                    InsertHistorico(envioHist);

                    if (rows_deleted > 0)
                    {
                        res.Sucesso("Registro excluído com sucesso.");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
            return res;
        }


        public Resultado InsertHistorico(PRE_TBL_ARQ_ENVIO_HIST newHist)
        {
            Resultado res = new Resultado();

            try
            {
                int insere = m_DbContext.Database.ExecuteSqlCommand("insert into OWN_FUNCESP.PRE_TBL_ARQ_ENVIO_HIST (COD_ARQ_ENVIO,COD_ARQ_STATUS,DTH_INCLUSAO,LOG_INCLUSAO)  values (" +
                        newHist.COD_ARQ_ENVIO + "," + 
                        newHist.COD_ARQ_STATUS + "," +
                        "to_date('" + newHist.DTH_INCLUSAO.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS'),'" +
                        newHist.LOG_INCLUSAO + "')");

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


        public Resultado SaveData(PRE_TBL_ARQ_ENVIO newEnvio, short NOVO_COD_ARQ_STATUS = 1)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_ENVIO.Find(newEnvio.COD_ARQ_ENVIO);

                if (atualiza == null)
                {
                    newEnvio.COD_ARQ_ENVIO = GetMaxPkEnvio();
                    //newEnvio.PRE_TBL_ARQ_ENVIO_HIST.ToList().ForEach(a => { a.COD_ARQ_ENVIO = newEnvio.COD_ARQ_ENVIO; });
                    m_DbContext.PRE_TBL_ARQ_ENVIO.Add(newEnvio);

                    //m_DbContext.PRE_TBL_ARQ_ENVIO_HIST.Add(envioHist);
                    
                    //List<Object> modifiedOrAddedEntities = m_DbContext.ChangeTracker.Entries()
                    //// .Where(x => x.State == System.Data.EntityState.Modified 
                    ////        || x.State == System.Data.EntityState.Added)
                    // .Select(x=>x.Entity).ToList();

                    int rows_update = m_DbContext.SaveChanges();

                    PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
                    envioHist.COD_ARQ_ENVIO = newEnvio.COD_ARQ_ENVIO;
                    envioHist.COD_ARQ_STATUS = NOVO_COD_ARQ_STATUS;
                    envioHist.DTH_INCLUSAO = newEnvio.DTH_INCLUSAO;
                    envioHist.LOG_INCLUSAO = newEnvio.LOG_INCLUSAO;
                    //envioHist.PRE_TBL_ARQ_ENVIO_STATUS = new PRE_TBL_ARQ_ENVIO_STATUS();
                    //envioHist.PRE_TBL_ARQ_ENVIO = newEnvio;

                    InsertHistorico(envioHist);

                    res.Sucesso("Registro Inserido com sucesso. ", newEnvio.COD_ARQ_ENVIO);
                }
                else
                {
                    newEnvio.COD_ARQ_ENVIO_TIPO = newEnvio.COD_ARQ_ENVIO_TIPO;
                    atualiza.ANO_REF = newEnvio.ANO_REF;
                    atualiza.MES_REF = newEnvio.MES_REF;
                    newEnvio.COD_GRUPO_EMPRS = newEnvio.COD_GRUPO_EMPRS;
                    atualiza.COD_ARQ_AREA_ORIG = newEnvio.COD_ARQ_AREA_ORIG;
                    atualiza.DTH_INCLUSAO = newEnvio.DTH_INCLUSAO;
                    atualiza.LOG_INCLUSAO = newEnvio.LOG_INCLUSAO;

                    m_DbContext.SaveChanges();
                    res.Sucesso("Registro Atualizado com sucesso. ", newEnvio.COD_ARQ_ENVIO);
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

        public PRE_TBL_ARQ_ENVIO GetARQ_ENVIO(int COD_ARQ_ENVIO)
        {
            PRE_TBL_ARQ_ENVIO ret = new PRE_TBL_ARQ_ENVIO();
            ret = m_DbContext.PRE_TBL_ARQ_ENVIO.Find(COD_ARQ_ENVIO);
            return ret;
        }

        #endregion

        #region .:Painel Controle:.

        public List<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA> GetDataControle2(int startRowIndex, int maximumRows, short? grupo, short? ano, short? mes, short? area, string sortParameter)
        {
            return GetWhereControle(grupo, ano, mes, area)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int GetDataControleCount(short? grupo, short? ano, short? mes)
        {
            return GetWhereControle(grupo, ano, mes, null).SelectCount();
        }

        public IQueryable<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA> GetWhereControle(short? grupo, short? ano, short? mes, short? area)
        {
            IQueryable<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA> query;

            query = from a in m_DbContext.PRE_TBL_ARQ_AREA      
                    where a.COD_ARQ_AREA > 2
                    && (a.COD_ARQ_AREA == area || area == null)
                    select new PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA()
                    {
                        COD_ARQ_AREA  = a.COD_ARQ_AREA,
                        DCR_ARQ_AREA  = a.DCR_ARQ_AREA,
                        DCR_ARQ_SUB_AREA  = a.DCR_ARQ_SUB_AREA,
                        DCR_ARQ_ENVIADOS = null,
                        COD_ARQ_ENVIADOS_STATUS = 0,
                        QTD_GERADOS = 0,
                        QTD_ENVIADOS = 0,
                        QTD_PUBLICADOS = 0
                    };

            return query;
        }

        //private IQueryable<PRE_VIEW_ARQ_ENVIO_STATUS> GetArqEnvioStatus(string grupo, short? ano, short? mes, short? status)
        //{
        //    return (from dd in m_DbContext.PRE_TBL_ARQ_ENVIO
        //             join s in m_DbContext.PRE_TBL_ARQ_ENVIO_HIST on dd.COD_ARQ_AREA_ORIG.Value equals s.COD_ARQ_ENVIO
        //             where (dd.MES_REF == mes || mes == null)
        //                && (dd.ANO_REF == ano || ano == null)
        //                && (s.COD_ARQ_STATUS == status || status == null)
        //             //&& (dd.DTH_INCLUSAO >= datIni || datIni == null
        //             //&&  dd.DTH_INCLUSAO <= datFim || datFim == null)
        //             group dd by new { dd.COD_ARQ_AREA_ORIG, s.COD_ARQ_STATUS } into g
        //             select new PRE_VIEW_ARQ_ENVIO_STATUS()
        //             {
        //                 COD_ARQ_AREA = g.Key.COD_ARQ_AREA_ORIG,
        //                 COD_ARQ_STATUS = g.Key.COD_ARQ_STATUS,
        //                 DTH_INCLUSAO = g.Max(x => x.DTH_INCLUSAO)
        //             }
        //             );
        //}

        #endregion

        #region .:Portal:.

        public List<PRE_TBL_ARQ_ENVIO_View> GetDataPortal(int startRowIndex, int maximumRows, int pExibir, DateTime? datIni, DateTime? datFim, string grupos, short? ano, short? mes, string ext, string referencia, short? tipoEnvio, string sortParameter)
        {

            DateTime now = DateTime.Now;
            int last_day = DateTime.DaysInMonth(now.Year, now.Month);
            switch (pExibir)
            {
                case 0:
                default:
                    return GetWherePortal(null, null, grupos, null, null, null, null, 0)
                       .GetData(startRowIndex, maximumRows, sortParameter).ToList();
                case 1:
                    return GetWherePortal(new DateTime(now.Year, now.Month, 1), new DateTime(now.Year, now.Month, last_day), grupos, null, null, null, null, 0)
                           .GetData(startRowIndex, maximumRows, sortParameter).ToList();
                case 2:
                    return GetWherePortal(datIni, datFim, grupos, ano, mes, ext, referencia, tipoEnvio)
                           .GetData(startRowIndex, maximumRows, sortParameter).ToList();
            }

        }

        public int GetDataPortalCount(int pExibir, DateTime? datIni, DateTime? datFim, string grupos, short? ano, short? mes, string ext, string referencia, short? tipoEnvio)
        {
            switch (pExibir)
            {
                case 0:
                default:
                    return GetWherePortal(null, null, grupos, null, null, null, null, 0).SelectCount();
                case 1:
                    return GetWherePortal(DateTime.Now.AddDays(-30), DateTime.Now, grupos, null, null, null, null, 0).SelectCount();
                case 2:
                    return GetWherePortal(datIni, datFim, grupos, ano, mes, ext, referencia, tipoEnvio).SelectCount();
            }
        }
        private IQueryable<PRE_TBL_ARQ_ENVIO_View> GetWherePortal(DateTime? datIni, DateTime? datFim, string grupos, short? ano, short? mes, string ext, string referencia, short? tipoEnvio)
        {
            IQueryable<PRE_TBL_ARQ_ENVIO_View> query;

            string[] aAcesso = new string[] { "0" };
            if (grupos != null)
            {
                aAcesso = grupos.Split(',');
            }

            query = from tb in m_DbContext.PRE_TBL_ARQ_ENVIO
                    join d in
                        (from dd in m_DbContext.PRE_TBL_ARQ_ENVIO_HIST
                         join s in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS on dd.COD_ARQ_STATUS equals s.COD_ARQ_STATUS
                         //where (dd.COD_ARQ_ENVIO == tb.COD_ARQ_ENVIO)
                         select new PRE_TBL_ARQ_ENVIO_HIST_View()
                         {
                             COD_ARQ_ENVIO = dd.COD_ARQ_ENVIO,
                             COD_ARQ_STATUS = s.COD_ARQ_STATUS,
                             DCR_ARQ_STATUS = s.DCR_ARQ_STATUS,
                             DTH_INCLUSAO = dd.DTH_INCLUSAO
                         }
                         ) on tb.COD_ARQ_ENVIO equals d.COD_ARQ_ENVIO
                     into leftjoin
                    from dd in leftjoin.DefaultIfEmpty()
                    join pp in
                        (
                            from ff in m_DbContext.PRE_TBL_ARQ_ENVIO_TIPO
                            select ff
                            ) on tb.COD_ARQ_ENVIO_TIPO equals pp.COD_ARQ_ENVIO_TIPO
                    join ge in
                        m_DbContext.PRE_TBL_GRUPO_EMPRS on tb.COD_GRUPO_EMPRS equals ge.COD_GRUPO_EMPRS
                    where (dd.DTH_INCLUSAO == m_DbContext.PRE_TBL_ARQ_ENVIO_HIST.Where(h => h.COD_ARQ_ENVIO == tb.COD_ARQ_ENVIO).Max(h => h.DTH_INCLUSAO) || dd.COD_ARQ_ENVIO == null)
                     && (tb.COD_ARQ_AREA_DEST == 2)
                     && (dd.COD_ARQ_STATUS == 3 || dd.COD_ARQ_STATUS == 5)
                     && (tb.COD_ARQ_ENVIO_TIPO == tipoEnvio || tipoEnvio == 0)
                     && (tb.MES_REF == mes || mes == null)
                     && (tb.ANO_REF == ano || ano == null)
                     && (aAcesso.Contains(ge.GRUPO_PORTAL) || aAcesso.Contains("9999"))
                        //&& (tb.GRUPO_PORTAL == grupo || grupo == null)
                        //&& (dd.COD_ARQ_STATUS == status || status == null || (dd.COD_ARQ_STATUS == null && status == 1))
                     && (tb.DCR_ARQ_ENVIO.Equals(referencia) || referencia == null || referencia == "" || tb.DCR_ARQ_ENVIO.StartsWith(referencia))
                     && (tb.DTH_INCLUSAO >= datIni || datIni == null
                     && tb.DTH_INCLUSAO <= datFim || datFim == null)
                    select new PRE_TBL_ARQ_ENVIO_View()
                    {
                        COD_ARQ_ENVIO = tb.COD_ARQ_ENVIO,
                        COD_ARQ_ENVIO_TIPO = tb.COD_ARQ_ENVIO_TIPO,
                        COD_ARQ_SUB_TIPO = tb.COD_ARQ_SUB_TIPO,
                        DCR_ARQ_ENVIO = tb.DCR_ARQ_ENVIO,
                        DCR_ARQ_EXT = tb.DCR_ARQ_EXT,
                        ANO_REF = tb.ANO_REF,
                        MES_REF = tb.MES_REF,
                        COD_GRUPO_EMPRS = tb.COD_GRUPO_EMPRS,
                        DCR_GRUPO_EMPRS = ge.DCR_GRUPO_EMPRS,
                        COD_ARQ_AREA_DEST = tb.COD_ARQ_AREA_DEST,
                        COD_ARQ_AREA_ORIG = tb.COD_ARQ_AREA_ORIG,
                        DAT_ARQUIVO = tb.DAT_ARQUIVO,
                        DCR_CAMINHO_ARQUIVO = tb.DCR_CAMINHO_ARQUIVO,
                        COD_ARQ_ENVIO_PAI = tb.COD_ARQ_ENVIO_PAI,
                        DTH_INCLUSAO = tb.DTH_INCLUSAO,
                        LOG_INCLUSAO = tb.LOG_INCLUSAO,
                        DTH_EXCLUSAO = tb.DTH_EXCLUSAO,
                        LOG_EXCLUSAO = tb.LOG_EXCLUSAO,
                        COD_ARQ_STATUS = dd.COD_ARQ_STATUS ?? 1,
                        DCR_ARQ_STATUS = dd.DCR_ARQ_STATUS ?? "Novo",
                        DCR_ARQ_ENVIO_TIPO = pp.DCR_ARQ_ENVIO_TIPO
                    };
            return query;
        }

        #endregion
        #region .:Envio:.

        public List<FCESP_GRUPO_EMP_View> GetGrupoDdl(short? grupo/*, short? empresa*/)
        {
            List<FCESP_GRUPO_EMP_View> list = new List<FCESP_GRUPO_EMP_View>();
            if (grupo != null)
            {
                string grupo_portal = GetGrupoPortal(grupo).GRUPO_PORTAL;

                IEnumerable<FCESP_GRUPO_EMP_View> IEnum = m_DbContext.Database.SqlQuery<FCESP_GRUPO_EMP_View>("select distinct NOM_ABRVO_EMPRS,EMPRESA from EMPRESA e inner join OWN_PORTAL.FCESP_GRUPO_EMP@PPORTAL.WORLD g on g.EMPRESA = e.cod_emprs " +
                                                                                                                   "where GRUPO LIKE " + "'%" + grupo_portal + "%'");
                list = IEnum.ToList();

                return list;
            }
            return list;
        }

        public List<PRE_TBL_ARQ_ENV_REPASSE_View> GetDataRepasse(int startRowIndex, int maximumRows, short? area, short? grupo, int? mesRef, int? anoRef, string sortParameter)
        {
            return GetWhereRepasse(area, grupo, mesRef, anoRef)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int GetDataCountRepasse(short? area, short? grupo, int? mesRef, int? anoRef)
        {
            return GetWhereRepasse(area, grupo, mesRef, anoRef).SelectCount();
        }

        public IQueryable<PRE_TBL_ARQ_ENV_REPASSE_View> GetWhereRepasse(short? area, short? grupo, int? mesRef, int? anoRef)
        {
            IQueryable<PRE_TBL_ARQ_ENV_REPASSE_View> query;

            query = from tb in m_DbContext.PRE_TBL_ARQ_ENV_REPASSE
                    join d in
                        (from dd in m_DbContext.PRE_TBL_ARQ_ENVIO_HIST
                         join s in m_DbContext.PRE_TBL_ARQ_ENVIO_STATUS on dd.COD_ARQ_STATUS equals s.COD_ARQ_STATUS
                         select new PRE_TBL_ARQ_ENVIO_HIST_View()
                         {
                             COD_ARQ_ENVIO = dd.COD_ARQ_ENVIO,
                             COD_ARQ_STATUS = s.COD_ARQ_STATUS,
                             DCR_ARQ_STATUS = s.DCR_ARQ_STATUS,
                             DTH_INCLUSAO = dd.DTH_INCLUSAO
                         }
                         ) on tb.COD_ARQ_ENVIO equals d.COD_ARQ_ENVIO
                     into leftjoin
                    from dd in leftjoin.DefaultIfEmpty()
                    join g in m_DbContext.PRE_TBL_GRUPO_EMPRS on tb.COD_GRUPO_EMPRS equals g.COD_GRUPO_EMPRS
                    where (dd.DTH_INCLUSAO == m_DbContext.PRE_TBL_ARQ_ENVIO_HIST.Where(h => h.COD_ARQ_ENVIO == tb.COD_ARQ_ENVIO).Max(h => h.DTH_INCLUSAO) || dd.COD_ARQ_ENVIO == null)
                    && (tb.COD_ARQ_AREA == area || area == null)
                    && (tb.COD_GRUPO_EMPRS == grupo || grupo == null)
                    && (tb.MES_REF == mesRef || mesRef == null)
                    && (tb.ANO_REF == anoRef || anoRef == null)
                    //&& (dd.COD_ARQ_STATUS == status || status == null || (dd.COD_ARQ_STATUS == null && status == 1))
                    //&& (tb.DCR_ARQ_ENV_REPASSE.Equals(referencia) || referencia == null || referencia == "" || tb.DCR_ARQ_ENV_REPASSE.StartsWith(referencia) || tb.DCR_ARQ_ENV_REPASSE.ToUpper().Contains(referencia.ToUpper()))//to upper                  
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
                        DCR_ARQ_STATUS = dd.DCR_ARQ_STATUS ?? "Novo",
                        DCR_GRUPO_EMPRS = g.DCR_GRUPO_EMPRS
                    };
            return query;
        }
        #endregion
        #region .:Métodos:.
        public PRE_TBL_GRUPO_EMPRS GetGrupoPortal(short? COD_GRUPO)
        {
            //string grupo_portal = "";

            var atualiza = m_DbContext.PRE_TBL_GRUPO_EMPRS.Find(COD_GRUPO);

            //grupo_portal = atualiza.GRUPO_PORTAL;

            return atualiza;
        }

        public List<PRE_TBL_ARQ_AREA_View> GetAreaDdl()
        {
            IQueryable<PRE_TBL_ARQ_AREA_View> query;

            query = from aa in m_DbContext.PRE_TBL_ARQ_AREA
                    where aa.COD_ARQ_AREA > 2
                    select new PRE_TBL_ARQ_AREA_View()
                    {
                        COD_ARQ_AREA = aa.COD_ARQ_AREA,
                        DCR_ARQ_AREA = aa.DCR_ARQ_AREA,
                        DCR_ARQ_SUB_AREA = aa.DCR_ARQ_SUB_AREA
                    };

            return query.ToList();
        }

        public int GetMaxPkEnvio()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_ENVIO.DefaultIfEmpty().Max(n => (n.COD_ARQ_ENVIO == null) ? 1 : n.COD_ARQ_ENVIO + 1);
            return maxPK;
        }


        #endregion

        public Resultado SaveRepasse(int? codRepasse, int? codArqEnvio)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_ENV_REPASSE.Find(codRepasse);

                if (atualiza != null)
                {
                    atualiza.COD_ARQ_ENVIO = codArqEnvio;
                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
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
