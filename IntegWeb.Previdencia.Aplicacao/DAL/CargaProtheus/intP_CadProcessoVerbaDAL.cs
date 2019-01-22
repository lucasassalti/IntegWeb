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
    public class intP_CadProcessoVerbaDAL
    {
        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public IQueryable<VRB_NEGOCIO> GetCadProcessoVerbaProtheus()
        {
            IQueryable<VRB_NEGOCIO> query;

            query = from u in m_DbContext.VRB_NEGOCIO
                    select u;
            return query;
        }

        public Resultado AtualizaCadProcessoVerbaProtheus(VRB_NEGOCIO obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.VRB_NEGOCIO.FirstOrDefault(p => p.COD_VRB_NEGOCIO == obj.COD_VRB_NEGOCIO);

                if (atualiza != null)
                {
                    atualiza.COD_VERBA = obj.COD_VERBA;
                    atualiza.NUM_VRBFSS = obj.NUM_VRBFSS;
                    atualiza.TIP_NEGOCIO = obj.TIP_NEGOCIO;
                    atualiza.TIP_SEG = obj.TIP_SEG;

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

        public Resultado InseriCadProcessoVerbaProtheus(VRB_NEGOCIO obj)
        {
            Resultado res = new Resultado();

            try
            {
                //Valida a combinação de Fluxo e ação;
                var atualiza = m_DbContext.VRB_NEGOCIO.FirstOrDefault(p =>
                    (p.NUM_VRBFSS == obj.NUM_VRBFSS || obj.NUM_VRBFSS == null)
                     && (p.TIP_NEGOCIO == obj.TIP_NEGOCIO || obj.TIP_NEGOCIO == null)
                     && (p.TIP_SEG == obj.TIP_SEG || obj.TIP_SEG == null)
                     && (p.COD_VERBA == obj.COD_VERBA || obj.COD_VERBA == null)
                     );

                if (atualiza == null)
                {
                    obj.COD_VRB_NEGOCIO = GetMaxPk() + 1;
                    m_DbContext.VRB_NEGOCIO.Add(obj);
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

        public int GetMaxPk()
        {
            int maxPK = 0;
            maxPK = Convert.ToInt16(m_DbContext.VRB_NEGOCIO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_VRB_NEGOCIO));
            return maxPK;
        }
    }
}
