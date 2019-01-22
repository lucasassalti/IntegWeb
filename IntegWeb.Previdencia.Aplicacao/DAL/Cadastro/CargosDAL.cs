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
    public class CargosDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<CARGOS> GetData(int startRowIndex, int maximumRows, int? pNUM_CARGO, string sortParameter)
        {
            return GetWhere(pNUM_CARGO)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<CARGOS> GetWhere(int? pNUM_CARGO)
        {
            IQueryable<CARGOS> query;
            query = from e in m_DbContext.CARGOS
                    where (e.NUM_CARGO == pNUM_CARGO || pNUM_CARGO == null)
                    select e;
            
            return query;
        }

        public int GetDataCount(int pNUM_CARGO)
        {
            return GetWhere(pNUM_CARGO).SelectCount();
        }

        public CARGOS GetCargo(int pNUM_CARGO)
        {
            return GetWhere(pNUM_CARGO).ToList().FirstOrDefault();
        }
     
    }
}
