using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Faturamento
{
   public class ExportaRelCiDAL
    {
       public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

       public List<short> GetEmpresa(DateTime datIni, DateTime datFim)
       {
           datIni.ToShortDateString();
           datFim.ToShortDateString();

           IEnumerable<short> query = from e in m_DbContext.EMPRESA
                                      from c in m_DbContext.TAB_CARTEIRINHA_CARENCIA
                                      where (c.EMPRESA == e.COD_EMPRS)
                                      && (e.TIPO_EMPRS == "C")
                                      && (c.DT_SOLIC_CI >= datIni)
                                       && (c.DT_SOLIC_CI <= datFim)
                                      && (c.TIPO_CARTAO == 11)
                                      select c.COD_USU_EMP;

           query = query.Distinct();

           return query.ToList();

       }
    }
}
