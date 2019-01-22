using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Processos
{
    public class StatusExtratoUtilizacaoSaudeDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public class SAU_TBL_EXT_UTIL_DADGER_VIEW
        {
            public string DAT_REF { get; set; }
            public short COD_EMPRS { get; set; }
            public int NUM_RGTRO_EMPRG { get; set; }
            public System.DateTime DAT_MOVIMENTO { get; set; }
            public int NUM_IDNTF_RPTANT { get; set; }
            public string NOM_RESP { get; set; }
            public string IDC_INIBE_EXT { get; set; }

        }


        public List<SAU_TBL_EXT_UTIL_DADGER_VIEW> GetExtrato(short codEmpresa, int matricula,int numRepresen,DateTime dataMotiv)
        {
            IQueryable<SAU_TBL_EXT_UTIL_DADGER_VIEW> query;

            query = from dag in m_DbContext.SAU_TBL_EXT_UTIL_DADGER
                    where (dag.COD_EMPRS == codEmpresa)
                    && (dag.NUM_RGTRO_EMPRG == matricula)
                    && (dag.DAT_MOVIMENTO == dataMotiv)
                    && (dag.NUM_IDNTF_RPTANT == numRepresen)
                    select new SAU_TBL_EXT_UTIL_DADGER_VIEW
                    {
                        DAT_REF = dag.DAT_REF,
                        COD_EMPRS = dag.COD_EMPRS,
                        NUM_RGTRO_EMPRG = dag.NUM_RGTRO_EMPRG,
                        DAT_MOVIMENTO = dag.DAT_MOVIMENTO,
                        NUM_IDNTF_RPTANT = dag.NUM_IDNTF_RPTANT,
                        NOM_RESP = dag.NOM_RESP,
                        IDC_INIBE_EXT = dag.IDC_INIBE_EXT
                    };

            return query.ToList();
                        
        }

        public Resultado UpdateData(SAU_TBL_EXT_UTIL_DADGER obj, string inibir) 
        {
            Resultado res = new Resultado();

            try 
            {
                var atualiza = m_DbContext.SAU_TBL_EXT_UTIL_DADGER.Find(obj.DAT_REF, obj.COD_EMPRS, obj.NUM_RGTRO_EMPRG,obj.DAT_MOVIMENTO, obj.NUM_IDNTF_RPTANT);

                if (atualiza != null) 
                {
                    atualiza.IDC_INIBE_EXT = inibir;
                    m_DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        
        }

        public Resultado InsertLog(SAU_TBL_EXT_UTIL_DADGER_LOG obj) 
        {
            Resultado res = new Resultado();
            var atualiza = m_DbContext.SAU_TBL_EXT_UTIL_DADGER_LOG.Find(obj.HDRDATHOR,obj.DAT_REF,obj.COD_EMPRS,obj.NUM_RGTRO_EMPRG,obj.DAT_MOVIMENTO,obj.NUM_IDNTF_RPTANT);

                m_DbContext.SAU_TBL_EXT_UTIL_DADGER_LOG.Add(obj);
                Savechanges();

                res.Sucesso("ok");
           

            return res;
        }

        public string GetResultInibir(short codEmpresa, int matricula, int numRepresen, DateTime dataMotiv) 
        {
            IQueryable<SAU_TBL_EXT_UTIL_DADGER_VIEW> query;

            query = from dag in m_DbContext.SAU_TBL_EXT_UTIL_DADGER
                    where (dag.COD_EMPRS == codEmpresa)
                    && (dag.NUM_RGTRO_EMPRG == matricula)
                    && (dag.DAT_MOVIMENTO == dataMotiv)
                     && (dag.NUM_IDNTF_RPTANT == numRepresen)
                    select new SAU_TBL_EXT_UTIL_DADGER_VIEW
                    {
                        DAT_REF = dag.DAT_REF,
                        COD_EMPRS = dag.COD_EMPRS,
                        NUM_RGTRO_EMPRG = dag.NUM_RGTRO_EMPRG,
                        DAT_MOVIMENTO = dag.DAT_MOVIMENTO,
                        NUM_IDNTF_RPTANT = dag.NUM_IDNTF_RPTANT,
                        NOM_RESP = dag.NOM_RESP,
                        IDC_INIBE_EXT = dag.IDC_INIBE_EXT
                    };

            return query.FirstOrDefault().IDC_INIBE_EXT;
        }

        public Resultado Savechanges()
        {

            Resultado res = new Entidades.Resultado();
            try
            {
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
    }
}
