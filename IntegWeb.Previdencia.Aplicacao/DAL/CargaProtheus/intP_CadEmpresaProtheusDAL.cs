using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Int_Protheus
{
    public class intP_CadEmpresaProtheusDAL
    {
        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public IQueryable<PATR_PRV> GetCadEmpresaProtheus()
        {
            IQueryable<PATR_PRV> query;

            query = from u in m_DbContext.PATR_PRV
                    select u;
            return query;
        }

        public Resultado InseriCadEmpresaProtheus(PATR_PRV obj)
        {
            Resultado res = new Resultado();

            try
            {
                //Valida a combinação de Fluxo e ação;
                var atualiza = m_DbContext.PATR_PRV.FirstOrDefault(p => 
                    (p.COD_EMPRS == obj.COD_EMPRS || obj.COD_EMPRS == null)
                     && (p.COD_PATR == obj.COD_PATR || obj.COD_PATR == null) 
                     && (p.COD_PATR_SUP == obj.COD_PATR_SUP || obj.COD_PATR_SUP == null));

                if (atualiza == null)
                {
                    obj.COD_PATR_PRV = GetMaxPk() + 1;
                    m_DbContext.PATR_PRV.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Essa Combinação de Fluxo e ação já existe! ");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado AtualizaCadEmpresaProtheus(PATR_PRV obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.PATR_PRV.FirstOrDefault(p => p.COD_PATR_PRV == obj.COD_PATR_PRV);

                if (atualiza != null)
                {
                    atualiza.DCR_PATR = obj.DCR_PATR;
                    atualiza.COD_PATR_SUP = obj.COD_PATR_SUP;
                    atualiza.COD_EMPRS = obj.COD_EMPRS;
                    atualiza.COD_PATR = obj.COD_PATR;
                    
                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro Atualizado com Sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }


        public int GetMaxPk()
        {
            int maxPK = 0;
            maxPK = Convert.ToInt16(m_DbContext.PATR_PRV.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_PATR_PRV));
            return maxPK;
        }
    }
}
