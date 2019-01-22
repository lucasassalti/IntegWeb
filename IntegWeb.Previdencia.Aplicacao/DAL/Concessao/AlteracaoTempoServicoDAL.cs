using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Concessao
{
    public class AlteracaoTempoServicoDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<PLANO_BENEFICIO_FSS> GetPlano()
        {
            IQueryable<PLANO_BENEFICIO_FSS> query;

            query = from un in m_DbContext.PLANO_BENEFICIO_FSS
                    select un;

            return query.ToList();
        } 
    }
}
