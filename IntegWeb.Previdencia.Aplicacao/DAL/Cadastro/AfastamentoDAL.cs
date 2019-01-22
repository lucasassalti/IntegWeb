using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class AfastamentoDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<AFASTAMENTO> GetData(int startRowIndex, int maximumRows, short? pEmpresa, int? pMatricula, short? pTipoAfasta, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, pTipoAfasta)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<AFASTAMENTO> GetWhere(short? pEmpresa, int? pMatricula, short? pTipoAfasta)
        {
            IQueryable<AFASTAMENTO> query;
            query = from e in m_DbContext.AFASTAMENTO
                    where (e.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (e.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (e.COD_TIPAFT == pTipoAfasta || pTipoAfasta == null)
                    select e;
            
            return query;
        }

        public List<AFASTAMENTO> GetList(short? pEmpresa, int? pMatricula, short? pTipoAfasta)
        {
            return GetWhere(pEmpresa, pMatricula, pTipoAfasta).ToList();
        }

        public int GetDataCount(short? pEmpresa, int? pMatricula, short? pTipoAfasta)
        {
            return GetWhere(pEmpresa, pMatricula, pTipoAfasta).SelectCount();
        }

        public AFASTAMENTO GetAfastamentoEmAberto(short? pEmpresa, int? pMatricula)
        {
            IQueryable<AFASTAMENTO> query;
            query = from e in m_DbContext.AFASTAMENTO
                    where (e.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (e.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       //&& (e.COD_TIPAFT == pTipoAfasta || pTipoAfasta == null)
                       && (e.DAT_FMAFT_AFAST==null)
                    select e;

            return query.FirstOrDefault();
        }

        public AFASTAMENTO GetAfastamento(short? pEmpresa, int? pMatricula, DateTime pDAT_INAFT_AFAST)
        {
            IQueryable<AFASTAMENTO> query;
            query = from e in m_DbContext.AFASTAMENTO
                    where (e.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (e.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (e.DAT_INAFT_AFAST == pDAT_INAFT_AFAST || pDAT_INAFT_AFAST == null)
                    select e;
            return query.FirstOrDefault();
        }

        public TIPO_AFASTAMENTO GetTipoAfastamento(short pTipoAfasta)
        {
            IQueryable<TIPO_AFASTAMENTO> query;
            query = from e in m_DbContext.TIPO_AFASTAMENTO
                    where (e.COD_TIPAFT == pTipoAfasta)
                    select e;
            return query.FirstOrDefault();
        }
     
    }
}
