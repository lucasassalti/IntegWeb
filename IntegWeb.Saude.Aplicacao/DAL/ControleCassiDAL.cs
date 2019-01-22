using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class ControleCassiDAL
    {
        private SAUDE_EntityConn entity = new SAUDE_EntityConn();

        #region ParticipanteCassi
        protected IEnumerable<TB_CASSI_PARTICIP_PLANO> GetParticipantesCassi()
        {
            return entity.TB_CASSI_PARTICIP_PLANO.OrderBy(item => item.NOME_PARTICIP);
        }

        protected IEnumerable<TB_CASSI_PARTICIP_PLANO> GetParticipantesCassi(Func<TB_CASSI_PARTICIP_PLANO, bool> criteria)
        {
            return entity.TB_CASSI_PARTICIP_PLANO.Where(criteria);
        }

        protected TB_CASSI_PARTICIP_PLANO GetParticipante(Func<TB_CASSI_PARTICIP_PLANO, bool> criteria)
        {
            return entity.TB_CASSI_PARTICIP_PLANO.FirstOrDefault(criteria);
        }

        protected void InsertParticipante(TB_CASSI_PARTICIP_PLANO participante)
        {
            throw new NotImplementedException();
        }

        protected void UpdateParticipante(TB_CASSI_PARTICIP_PLANO participante)
        {
            throw new NotImplementedException();
        }

        protected void BulkUpdateParticipanteCassi(List<TB_CASSI_PARTICIP_PLANO> participantes)
        {
            try
            {
                for (int i = 0; i < participantes.Count; i++)
                {
                    entity.TB_CASSI_PARTICIP_PLANO.Attach(participantes[i]);
                }
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Devolução
        protected void GetLoteDevolucao(Func<TB_CASSI_DEVOLUCAO, bool> criteria, out TB_CASSI_DEVOLUCAO lote)
        {
            lote = entity.TB_CASSI_DEVOLUCAO.FirstOrDefault(criteria);
        }

        protected void InsertLoteDevolucao(ref TB_CASSI_DEVOLUCAO lote)
        {
            try
            {
                entity.TB_CASSI_DEVOLUCAO.Add(lote);
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void UpdateLoteDevolucao(ref TB_CASSI_DEVOLUCAO lote)
        {
            try
            {
                entity.TB_CASSI_DEVOLUCAO.Attach(lote);
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
