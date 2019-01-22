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
    public class CCustoDAL
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

        public List<CCUSTO_SAU> GetData(int startRowIndex, int maximumRows, short? pEmpresa, int? pNumOrgao, bool pDspAdm, string pCcusto, string pDscCcusto, string sortParameter)
        {
            return GetWhere(pEmpresa, pNumOrgao, pDspAdm, pCcusto, pDscCcusto)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<CCUSTO_SAU> GetWhere(short? pEmpresa, int? pNumOrgao, bool pDspAdm, string pCcusto, string pDscCcusto)
        {

            string s_DspAdm = pDspAdm ? "S" : "N";

            IQueryable<CCUSTO_SAU> query;
            query = from r in m_DbContext.CCUSTO_SAU
                    where (r.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (r.NUM_ORGAO == pNumOrgao || pNumOrgao == null)
                       && (r.DSP_ADM == s_DspAdm || s_DspAdm == "N")
                       && (r.CCUSTO == pCcusto || pCcusto == null)
                       && (r.DSC_CCUSTO.ToUpper().Trim().Contains(pDscCcusto.ToUpper().Trim()) || pDscCcusto == null)
                    select r;
            return query;
        }

        public int GetDataCount(short? pEmpresa, int? pNumOrgao, bool pDspAdm, string pCcusto, string pDscCcusto)
        {
            return GetWhere(pEmpresa, pNumOrgao, pDspAdm, pCcusto, pDscCcusto).SelectCount();
        }

        public CCUSTO_SAU GetCCusto(short? pEmpresa, int? pNumOrgao, bool pDspAdm)
        {
            return GetWhere(pEmpresa, pNumOrgao, pDspAdm, null, null).FirstOrDefault();
        }

        public List<ORGAO_view> GetAssociados(short pEmpresa)
        {
            return m_DbContext.ORGAO.Where(o => o.COD_EMPRS == pEmpresa)
                                    .OrderBy(o => o.NUM_ORGAO)
                                    .Select(o => new ORGAO_view { NUM_ORGAO = o.NUM_ORGAO, NOM_ORGAO = o.NOM_ORGAO, COD_ORGAO = o.COD_ORGAO })
                                    .ToList();
        }

        public Resultado SaveData(CCUSTO_SAU newCCusto)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.CCUSTO_SAU.Find(newCCusto.COD_EMPRS, newCCusto.NUM_ORGAO, newCCusto.DSP_ADM);

                if (atualiza != null)
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newCCusto);
                } else {
                    m_DbContext.CCUSTO_SAU.Add(newCCusto);
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
                var delete = m_DbContext.CCUSTO_SAU.Find(pEmpresa, pNumOrgao, pDspAdm);

                if (delete != null)
                {
                    m_DbContext.CCUSTO_SAU.Remove(delete);
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
