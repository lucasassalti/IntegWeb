using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Saude.Aplicacao.ENTITY;

namespace IntegWeb.Saude.Aplicacao.BLL
{
    public class ParticipantesBLL : ParticipanteDAL
    {
        
        public new List<SAU_VW_CONSULTA_USUARIO_SUS> Listar()
        {
            return base.Listar();
        }


        public new SAU_VW_CONSULTA_USUARIO_SUS ListarPorParticipante(string sNUM_SEQ_PARTICIP, string sCOD_IDENTIFICACAO, string dtAtendimento)
        {
            return base.ListarPorParticipante(sNUM_SEQ_PARTICIP, sCOD_IDENTIFICACAO, dtAtendimento);
        }

        public new SAU_VW_CONSULTA_USUARIO_SUS ListarPorParticipanteSemPlano(string sCOD_IDENTIFICACAO)
        {
            return base.ListarPorParticipanteSemPlano(sCOD_IDENTIFICACAO);
        }

        public void DeletaRegistros()
        {
            base.DeletaRegistros();
        }

    }
}
   