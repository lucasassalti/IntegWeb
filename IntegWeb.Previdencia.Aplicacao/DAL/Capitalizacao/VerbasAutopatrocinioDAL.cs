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
    public class VerbasAutopatrocinioDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<TB_SCR_VERBAS_AUTOPATROC> GetData(int startRowIndex, int maximumRows, short? pEmpresa, int? pVerba, int? pGrupoContrib, short? pNumPlano, string sortParameter)
        {
            return GetWhere(pEmpresa, pVerba, pGrupoContrib, pNumPlano)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<TB_SCR_VERBAS_AUTOPATROC> GetWhere(short? pEmpresa, int? pVerba, int? pGrupoContrib, short? pNumPlano)
        {

            IQueryable<TB_SCR_VERBAS_AUTOPATROC> query;
            query = from r in m_DbContext.TB_SCR_VERBAS_AUTOPATROC
                    where (r.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (r.NUM_VRBFSS == pVerba || pVerba == null)
                       && (r.COD_GRUPO_CONTRIB == pGrupoContrib || pGrupoContrib == null)
                       && (r.NUM_PLBNF == pNumPlano || pNumPlano == null)
                    select r;
            return query;
        }

        public int GetDataCount(short? pEmpresa, int? pVerba, int? pGrupoContrib, short? pNumPlano)
        {
            return GetWhere(pEmpresa, pVerba, pGrupoContrib, pNumPlano).SelectCount();
        }

        public TB_SCR_VERBAS_AUTOPATROC GetItem(short? pEmpresa, int? pVerba)
        {
            return GetWhere(pEmpresa, pVerba, null, null).FirstOrDefault();
        }

        public Resultado SaveData(TB_SCR_VERBAS_AUTOPATROC newCCusto)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.TB_SCR_VERBAS_AUTOPATROC.Find(newCCusto.NUM_VRBFSS, newCCusto.COD_EMPRS);

                if (atualiza != null)
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newCCusto);
                } else {
                    m_DbContext.TB_SCR_VERBAS_AUTOPATROC.Add(newCCusto);
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

        public Resultado DeleteData(short? pEmpresa, int? pVerba)
        {
            Resultado res = new Resultado();
          
            try
            {
                var delete = m_DbContext.TB_SCR_VERBAS_AUTOPATROC.Find(pVerba, pEmpresa);

                if (delete != null)
                {
                    m_DbContext.TB_SCR_VERBAS_AUTOPATROC.Remove(delete);
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
