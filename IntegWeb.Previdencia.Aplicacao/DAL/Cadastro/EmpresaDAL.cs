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
    public class EmpresaDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<EMPRESA> GetData(int startRowIndex, int maximumRows, short? pCOD_EMPRS, string sortParameter)
        {
            return GetWhere(pCOD_EMPRS)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<EMPRESA> GetWhere(short? pCOD_EMPRS)
        {
            return GetWhere(pCOD_EMPRS ?? 0);
        }

        public IQueryable<EMPRESA> GetWhere(short pCOD_EMPRS)
        {
            IQueryable<EMPRESA> query;
            query = from e in m_DbContext.EMPRESA
                    where (e.COD_EMPRS.Equals(pCOD_EMPRS) || pCOD_EMPRS == 0)
                    select e;
            return query;
        }

        public List<EMPRESA> GetList(short? pCOD_EMPRS)
        {
            return GetWhere(pCOD_EMPRS).ToList();
        }

        public int GetDataCount(short? pCOD_EMPRS)
        {
            return GetWhere(pCOD_EMPRS).SelectCount();
        }

        public EMPRESA GetEMPRESA(short? pCOD_EMPRS)
        {
            return GetWhere(pCOD_EMPRS).FirstOrDefault();
        }

    }
}
