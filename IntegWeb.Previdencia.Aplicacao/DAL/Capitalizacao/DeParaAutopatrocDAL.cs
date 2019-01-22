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
    public class DeParaAutopatrocDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<TB_SCR_DEPARA_AUTOPATROC> GetData(int startRowIndex, int maximumRows, short? pEmpresa, int? pMatricula, short? pEmpresaDestino, bool? pFlagExclusao, bool? pFlagPatroc, bool? pFlagParticip, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, pEmpresaDestino, pFlagExclusao, pFlagPatroc, pFlagParticip)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<TB_SCR_DEPARA_AUTOPATROC> GetWhere(short? pEmpresa, int? pMatricula, short? pEmpresaDestino, bool? pFlagExclusao, bool? pFlagPatroc, bool? pFlagParticip)
        {

            string s_FlagExclusao = (pFlagExclusao == true ? "S" : "N");
            string s_FlagPatroc = (pFlagPatroc == true ? "S" : "N");
            string s_FlagPartic = (pFlagParticip == true ? "S" : "N");

            IQueryable<TB_SCR_DEPARA_AUTOPATROC> query;
            query = from r in m_DbContext.TB_SCR_DEPARA_AUTOPATROC
                    where (r.COD_EMPRS_ORIGEM == pEmpresa || pEmpresa == null)
                       && (r.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (r.COD_EMPRS_DESTINO == pEmpresaDestino || pEmpresaDestino == null)
                       && (r.FL_EXCLUSAO_TOTAL == s_FlagExclusao || s_FlagExclusao == "N")
                       && (r.VD_PATROC == s_FlagPatroc || s_FlagPatroc == "N")
                       && (r.VD_PARTIC == s_FlagPartic || s_FlagPartic == "N")
                    select r;
            return query;
        }

        public int GetDataCount(short? pEmpresa, int? pMatricula, short? pEmpresaDestino, bool? pFlagExclusao, bool? pFlagPatroc, bool? pFlagParticip)
        {
            return GetWhere(pEmpresa, pMatricula, pEmpresaDestino, pFlagExclusao, pFlagPatroc, pFlagParticip).SelectCount();
        }

        public TB_SCR_DEPARA_AUTOPATROC GetItem(short? pEmpresa, int? pMatricula, short? pEmpresaDestino)
        {
            return GetWhere(pEmpresa, pMatricula, pEmpresaDestino, null, null, null).FirstOrDefault();
        }

        public Resultado SaveData(TB_SCR_DEPARA_AUTOPATROC newCCusto)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.TB_SCR_DEPARA_AUTOPATROC.Find(newCCusto.COD_EMPRS_ORIGEM, newCCusto.NUM_RGTRO_EMPRG, newCCusto.COD_EMPRS_DESTINO);

                if (atualiza != null)
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newCCusto);
                } else {
                    m_DbContext.TB_SCR_DEPARA_AUTOPATROC.Add(newCCusto);
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

        public Resultado DeleteData(short? pEmpresa, int? pMatricula, short? pEmpresaDestino)
        {
            Resultado res = new Resultado();
          
            try
            {
                var delete = m_DbContext.TB_SCR_DEPARA_AUTOPATROC.Find(pEmpresa, pMatricula, pEmpresaDestino);

                if (delete != null)
                {
                    m_DbContext.TB_SCR_DEPARA_AUTOPATROC.Remove(delete);
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
