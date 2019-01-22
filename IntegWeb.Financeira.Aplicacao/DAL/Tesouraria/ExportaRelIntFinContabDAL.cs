using IntegWeb.Financeira.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL.Tesouraria
{
    public class ExportaRelIntFinContabDAL
    {
        public EntitiesConn m_DbContext = new EntitiesConn();

        public List<CC_INTEGR_CT> GetData(DateTime datIni, DateTime datFim)
        {
            IQueryable<CC_INTEGR_CT> query;

            query = from cc in m_DbContext.CC_INTEGR_CT
                    where cc.DT_CD_MOV >= datIni
                    && cc.DT_CD_MOV <= datFim
                    select cc;

            return query.ToList();

        }

        public List<GB_INTEGR_CT> GetDataGB(DateTime datIni, DateTime datFim)
        {
            IQueryable<GB_INTEGR_CT> query;

            query = from gb in m_DbContext.GB_INTEGR_CT
                    where gb.DT_CD_MOV >= datIni
                    && gb.DT_CD_MOV <= datFim
                    select gb;

            return query.ToList();
        }
    }
}
