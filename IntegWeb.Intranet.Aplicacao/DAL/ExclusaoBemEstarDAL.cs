using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    public class ExclusaoBemEstarDAL
    {
        public INTRA_Entity_Conn m_DbContext = new INTRA_Entity_Conn();

        public List<FUN_TBL_EXCLUSAO_REVISTA> GetData(short codEmp, Int32 numRgtroEmp)
        {
            IQueryable<FUN_TBL_EXCLUSAO_REVISTA> query;

            query = from r in m_DbContext.FUN_TBL_EXCLUSAO_REVISTA
                    where (r.COD_EMPRS == codEmp)
                    && (r.NUM_RGTRO_EMPRG == numRgtroEmp)
                    select r;

            return query.ToList();
        }

        public void SaveLog(FUN_TBL_EXCLUSAO_REVISTA_LOG parametroLog)
        {
            FUN_TBL_EXCLUSAO_REVISTA_LOG obj = new FUN_TBL_EXCLUSAO_REVISTA_LOG();

            obj.ID_LOG = GetMaxPk();
            obj.COD_EMPRS = parametroLog.COD_EMPRS;
            obj.NUM_RGTRO_EMPRG = parametroLog.NUM_RGTRO_EMPRG;
            obj.NUM_IDNTF_RPTANT = parametroLog.NUM_IDNTF_RPTANT;
            obj.DATA_ATU = DateTime.Now;
            obj.TP_ATU = parametroLog.TP_ATU;
            obj.USU_INC = parametroLog.USU_INC;
            m_DbContext.FUN_TBL_EXCLUSAO_REVISTA_LOG.Add(obj);
            m_DbContext.SaveChanges();
        }

        public Resultado DeleteData(short codEmp, Int32 numRgtroEmp)
        {
            Resultado res = new Resultado();

            var exclui = m_DbContext.FUN_TBL_EXCLUSAO_REVISTA.FirstOrDefault(p => p.COD_EMPRS == codEmp
                                                                            && p.NUM_RGTRO_EMPRG == numRgtroEmp);
            if (exclui != null)
            {

                m_DbContext.FUN_TBL_EXCLUSAO_REVISTA.Remove(exclui);

                int rows_deleted = m_DbContext.SaveChanges();
                if (rows_deleted > 0)
                {
                    res.Sucesso(String.Format("Registro excluído.", rows_deleted));
                }
            }

            return res;
        }

        public Resultado Inserir(short codEmp, Int32 numRgtroEmp, Int32? numIdntfRptant, string usuInc)
        {
            Resultado res = new Resultado();
            FUN_TBL_EXCLUSAO_REVISTA obj = new FUN_TBL_EXCLUSAO_REVISTA();

            var inserir = m_DbContext.FUN_TBL_EXCLUSAO_REVISTA.FirstOrDefault(p => p.COD_EMPRS == codEmp
                                                                            && p.NUM_RGTRO_EMPRG == numRgtroEmp);
            if (inserir == null)
            {
                obj.COD_EMPRS = codEmp;
                obj.NUM_RGTRO_EMPRG = numRgtroEmp;
                obj.NUM_IDNTF_RPTANT = numIdntfRptant;
                obj.DATA_INC = DateTime.Now;
                obj.USU_INC = usuInc;
                m_DbContext.FUN_TBL_EXCLUSAO_REVISTA.Add(obj);
                int rows_insert = m_DbContext.SaveChanges();

                if (rows_insert > 0)
                {
                    res.Sucesso("Registro incluído com Sucesso", rows_insert);
                }

            }
            return res;
        }

        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.FUN_TBL_EXCLUSAO_REVISTA_LOG.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_LOG) + 1;
            return maxPK;
        }

    }
}
