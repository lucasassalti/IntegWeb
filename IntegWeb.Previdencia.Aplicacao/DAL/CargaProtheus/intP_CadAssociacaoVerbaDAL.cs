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

namespace IntegWeb.Previdencia.Aplicacao.DAL.Int_Protheus
{
    public class CadAssociacaoVerbaDAL
    {

        public class ORGAO_view
        {
            public int NUM_ORGAO { get; set; }
            public string NOM_ORGAO { get; set; }
            public string COD_ORGAO { get; set; }
            public string DCR_ORGAO
            {
                get
                {
                    string strMask = (String.IsNullOrEmpty(COD_ORGAO) ? "{0}-{1}" : "{0}-{1}-{2}");
                    return String.Format(strMask, NUM_ORGAO, NOM_ORGAO, COD_ORGAO);
                }
            }
        }

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<ASSOCIACAO_VERBA> GetData(int startRowIndex, int maximumRows, short? pCodAssociado, int? pNumVerba, string sortParameter)
        {
            return GetWhere(pCodAssociado, pNumVerba)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<ASSOCIACAO_VERBA> GetWhere(short? pCodAssociado, int? pNumVerba)
        {
            IQueryable<ASSOCIACAO_VERBA> query = 
                     from tb in m_DbContext.ASSOCIACAO_VERBA
                    where (tb.COD_ASSOC == pCodAssociado || pCodAssociado == null)
                       && (tb.NUM_VRBFSS == pNumVerba || pNumVerba == null)
                    select tb;
            return query;
        }

        public int GetDataCount(short? pCodAssociado, int? pNumVerba)
        {
            return GetWhere(pCodAssociado, pNumVerba).SelectCount();
        }

        public ASSOCIACAO_VERBA GetCCusto(int pCodAssociadoVerba)
        {
            return m_DbContext.ASSOCIACAO_VERBA.Find(pCodAssociadoVerba);
        }

        public List<ORGAO_view> GetOrgaoDdl(short pEmpresa)
        {
            return m_DbContext.ORGAO.Where(o => o.COD_EMPRS == pEmpresa)
                                    .OrderBy(o => o.NUM_ORGAO)
                                    .Select(o => new ORGAO_view { NUM_ORGAO = o.NUM_ORGAO, NOM_ORGAO = o.NOM_ORGAO, COD_ORGAO = o.COD_ORGAO })
                                    .ToList();
        }

        public Resultado SaveData(ASSOCIACAO_VERBA newCCusto)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.ASSOCIACAO_VERBA.Find(newCCusto.COD_ASSOCIACAO_VERBA);

                if (atualiza != null)
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newCCusto);
                } else {
                    m_DbContext.ASSOCIACAO_VERBA.Add(newCCusto);
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

        public Resultado DeleteData(short? pEmpresa, int? pNumOrgao, string pDspAdm)
        {
            Resultado res = new Resultado();
          
            try
            {
                var delete = m_DbContext.ASSOCIACAO_VERBA.Find(pEmpresa, pNumOrgao, pDspAdm);

                if (delete != null)
                {
                    m_DbContext.ASSOCIACAO_VERBA.Remove(delete);
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
