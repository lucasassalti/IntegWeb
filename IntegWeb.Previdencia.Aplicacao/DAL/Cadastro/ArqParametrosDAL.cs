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
    public class ArqParametrosDAL
    {

        public partial class PRE_TBL_ARQ_PARAM_view
        {
            public int COD_ARQ_PARAM { get; set; }
            public Nullable<short> COD_GRUPO_EMPRS { get; set; }
            public Nullable<short> COD_ARQ_AREA { get; set; }
            public string NOM_GRUPO_EMPRS { get; set; }
            public string NOM_ARQ_AREA { get; set; }
            public string NOM_PARAM { get; set; }
            public string NOM_PARAM_SUB { get; set; }
            public string DCR_PARAM { get; set; }
            public System.DateTime DTH_INCLUSAO { get; set; }
            public string LOG_INCLUSAO { get; set; }
        }

        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<PRE_TBL_ARQ_PARAM_view> GetData(int startRowIndex, int maximumRows, string pParametro, short? pGrupo, short? pArea, string pSubParam, string sortParameter)
        {
            return GetWhere(pParametro, pGrupo, pArea, pSubParam)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_TBL_ARQ_PARAM_view> GetWhere(string pParametro, short? pGrupo, short? pArea, string pSubParam)
        {
            //m_DbContext.Configuration.ValidateOnSaveEnabled

            IQueryable<PRE_TBL_ARQ_PARAM_view> query;
            query = from p in m_DbContext.PRE_TBL_ARQ_PARAM
                    join g in m_DbContext.PRE_TBL_GRUPO_EMPRS on p.COD_GRUPO_EMPRS equals g.COD_GRUPO_EMPRS
                    into leftjoin from g in leftjoin.DefaultIfEmpty()
                    join a in m_DbContext.PRE_TBL_ARQ_AREA on p.COD_ARQ_AREA equals a.COD_ARQ_AREA
                    into leftjoin2 from a in leftjoin2.DefaultIfEmpty()
                    where (p.NOM_PARAM == pParametro || pParametro == null)
                       && (p.COD_GRUPO_EMPRS == pGrupo || pGrupo == null)
                       && (p.COD_ARQ_AREA == pArea || pArea == null)
                       && (p.NOM_PARAM_SUB.Contains(pSubParam.ToUpper()) || pSubParam == null)
                    select new PRE_TBL_ARQ_PARAM_view
                    {
                        COD_ARQ_PARAM = p.COD_ARQ_PARAM,
                        COD_GRUPO_EMPRS = p.COD_GRUPO_EMPRS,
                        COD_ARQ_AREA = p.COD_ARQ_AREA,
                        NOM_GRUPO_EMPRS = g.DCR_GRUPO_EMPRS,
                        NOM_ARQ_AREA = a.DCR_ARQ_AREA,
                        NOM_PARAM = p.NOM_PARAM,
                        NOM_PARAM_SUB = p.NOM_PARAM_SUB,
                        DCR_PARAM = p.DCR_PARAM,
                        DTH_INCLUSAO = p.DTH_INCLUSAO,
                        LOG_INCLUSAO = p.LOG_INCLUSAO
                    };
            return query;
        }

        public int GetDataCount(string pParametro, short? pGrupo, short? pArea, string pSubParam)
        {
            return GetWhere(pParametro, pGrupo, pArea, pSubParam).SelectCount();
        }

        internal int GetMaxPk()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_PARAM.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_ARQ_PARAM);
            return maxPK;
        }

        public PRE_TBL_ARQ_PARAM GetParametro(int pCodParametro)
        {
            IQueryable<PRE_TBL_ARQ_PARAM> query;
            query = from d in m_DbContext.PRE_TBL_ARQ_PARAM
                    where (d.COD_ARQ_PARAM == pCodParametro)
                    select d;
            return query.FirstOrDefault();
        }

        public Resultado SaveData(PRE_TBL_ARQ_PARAM newLancamento)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                PRE_TBL_ARQ_PARAM atualiza = null;

                if (newLancamento.COD_ARQ_PARAM > 0)
                {
                    atualiza = m_DbContext.PRE_TBL_ARQ_PARAM.Find(newLancamento.COD_ARQ_PARAM);
                }

                if (atualiza == null)  //Novo registro
                {
                    int iMaxPk = GetMaxPk();
                    newLancamento.COD_ARQ_PARAM = iMaxPk + 1;
                    m_DbContext.PRE_TBL_ARQ_PARAM.Add(newLancamento);
                    res.Sucesso("INSERT", newLancamento.COD_ARQ_PARAM);
                }
                else   //Atualiza registro
                {
                    //newLancamento.COD_ARQ_PARAM = atualiza.COD_ARQ_PARAM;
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newLancamento);
                    res.Sucesso("UPDATE", newLancamento.COD_ARQ_PARAM);
                }
                res = SaveChanges();
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        internal Resultado SaveChanges()
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                int rows_updated = m_DbContext.SaveChanges();
                //if (rows_updated > 0)
                //{
                res.Sucesso("Registro salvo com sucesso.");
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

        public Resultado DeleteData(int pCOD_ARQ_PARAM)
        {
            Resultado res = new Resultado();
            var deleta = m_DbContext.PRE_TBL_ARQ_PARAM.Find(pCOD_ARQ_PARAM);

            if (deleta != null)
            {
                //deleta.ToList().ForEach(d => { m_DbContext.PRE_TBL_ARQ_PARAM.Remove(d); });
                m_DbContext.PRE_TBL_ARQ_PARAM.Remove(deleta);
                int rows_deleted = m_DbContext.SaveChanges();
                if (rows_deleted > 0)
                {
                    res.Sucesso("Registro excluído com sucesso.");
                }
            }
            return res;
        }

    }

}
