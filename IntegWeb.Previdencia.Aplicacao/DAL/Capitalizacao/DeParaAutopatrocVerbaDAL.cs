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

namespace IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao
{
    public class DeParaAutopatrocVerbaDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<TB_SCR_DEPARA_AUTOPATROC_VERBA> GetData(int startRowIndex, int maximumRows, short? pEmpresa, short? pPlanoOrigem, int? pVerbaFund, int? pVerbaDest, string sortParameter)
        {
            return GetWhere(pEmpresa, pPlanoOrigem, pVerbaFund, pVerbaDest)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<TB_SCR_DEPARA_AUTOPATROC_VERBA> GetWhere(short? pEmpresa, short? pPlanoOrigem, int? pVerbaFund, int? pVerbaDest)
        {

            IQueryable<TB_SCR_DEPARA_AUTOPATROC_VERBA> query;
            query = from r in m_DbContext.TB_SCR_DEPARA_AUTOPATROC_VERBA
                    where (r.EMPRS_DEST == pEmpresa || pEmpresa == null)
                       && (r.PLANO_ORIGEM == pPlanoOrigem || pPlanoOrigem == null)
                       && (r.NUM_VER_FUND == pVerbaFund || pVerbaFund == null)
                       && (r.NUM_VER_DEST == pVerbaDest || pVerbaDest == null)
                    select r;
            return query;
        }

        public int GetDataCount(short? pEmpresa, short? pPlanoOrigem, int? pVerbaFund, int? pVerbaDest)
        {
            return GetWhere(pEmpresa, pPlanoOrigem, pVerbaFund, pVerbaDest).SelectCount();
        }

        public TB_SCR_DEPARA_AUTOPATROC_VERBA GetItem(short? pEmpresa, short? pPlanoOrigem, int? pVerbaFund)
        {
            return GetWhere(pEmpresa, pPlanoOrigem, pVerbaFund, null).FirstOrDefault();
        }

        public Resultado SaveData(TB_SCR_DEPARA_AUTOPATROC_VERBA newCCusto)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.TB_SCR_DEPARA_AUTOPATROC_VERBA.Find(newCCusto.PLANO_ORIGEM, newCCusto.NUM_VER_FUND, newCCusto.EMPRS_DEST);

                if (atualiza != null)
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newCCusto);
                } else {
                    m_DbContext.TB_SCR_DEPARA_AUTOPATROC_VERBA.Add(newCCusto);
                }

                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    if (atualiza != null)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
                    }
                    else
                    {
                        res.Sucesso("Registro inserido com sucesso.");
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

        public Resultado DeleteData(short? pEmpresa, short? pPlanoOrigem, int? pVerbaFund)
        {
            Resultado res = new Resultado();
          
            try
            {
                var delete = m_DbContext.TB_SCR_DEPARA_AUTOPATROC_VERBA.Find(pPlanoOrigem, pVerbaFund, pEmpresa);

                if (delete != null)
                {
                    m_DbContext.TB_SCR_DEPARA_AUTOPATROC_VERBA.Remove(delete);
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro excluído com sucesso.");
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
