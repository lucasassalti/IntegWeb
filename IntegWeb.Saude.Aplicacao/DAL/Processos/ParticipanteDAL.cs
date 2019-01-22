using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades.Saude;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class ParticipanteDAL 
    {
        private SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();
        public List<SAU_VW_CONSULTA_USUARIO_SUS> Listar()
        {
            var query = from c in m_DbContext.SAU_VW_CONSULTA_USUARIO_SUS
                        orderby c.COD_IDENTIFICACAO
                        select c;
            return query.ToList();
        }

      
        public SAU_VW_CONSULTA_USUARIO_SUS ListarPorParticipante(string sNUM_SEQ_PARTICIP, string sCOD_IDENTIFICACAO, string sAtendimento )
        {

            decimal dNUM_SEQ_PARTICIP;
            decimal.TryParse(sNUM_SEQ_PARTICIP, out dNUM_SEQ_PARTICIP);

            decimal dCOD_IDENTIFICACAO;
            decimal.TryParse(sCOD_IDENTIFICACAO, out dCOD_IDENTIFICACAO);
            
            DateTime dtAtendimento;
            DateTime.TryParse(sAtendimento, out dtAtendimento);
            
            var query = from c in m_DbContext.SAU_VW_CONSULTA_USUARIO_SUS
                        where (c.NUM_SEQ_PARTICIP == dNUM_SEQ_PARTICIP || sNUM_SEQ_PARTICIP == null) &&
                              (c.COD_IDENTIFICACAO.Trim() == sCOD_IDENTIFICACAO.Trim() || sCOD_IDENTIFICACAO == null) &&
                              ((dtAtendimento >= c.DAT_ADESAO && dtAtendimento <= c.DAT_CANCELAMENTO) ||
                              (dtAtendimento >= c.DAT_ADESAO && c.DAT_CANCELAMENTO == null) || sAtendimento == null) 
                        orderby c.NUM_SEQ_PARTICIP
                        select c;
            //return query.ToList();

            return query.FirstOrDefault();
        }

        public SAU_VW_CONSULTA_USUARIO_SUS ListarPorParticipanteSemPlano(string sCOD_IDENTIFICACAO)
        {

            decimal dCOD_IDENTIFICACAO;
            decimal.TryParse(sCOD_IDENTIFICACAO, out dCOD_IDENTIFICACAO);

            var query = from c in m_DbContext.SAU_VW_CONSULTA_USUARIO_SUS
                        where (c.NUM_SEQ_PARTICIP == dCOD_IDENTIFICACAO || sCOD_IDENTIFICACAO == null)    
                         
                        orderby c.NUM_SEQ_PARTICIP
                        select c;
            //return query.ToList();

            return query.FirstOrDefault();
        }

        //public string VerificaABI(string ABI)
        //{
        //    int Total;
        //    string retorno;

        //    decimal dNUMEROOFICIO;
        //    decimal.TryParse("1385", out dNUMEROOFICIO);

        //    var query = from c in m_DbContext.SAU_TBL_IMPUGNACAOSUS
        //                where c.NUMEROOFICIO = dNUMEROOFICIO
        //                select c;

        //    string SQL = @"SELECT count(*) FROM own_funcesp.sau_tbl_impugnacaosus WHERE numerooficio = 1385";
        //    Total = m_DbContext.Database.ExecuteSqlCommand(SQL);

        //    if (Total > 0)
        //    {
        //        retorno = "S";
        //    }
        //    else
        //    {
        //        retorno = "N";
        //    }

        //    return retorno;
        //}

        public void DeletaRegistros()
        {
            //m_DbContext.SAU_TBL_IMPUGNACAOSUS.SqlQuery("delete from own_funcesp.sau_tbl_impugnacaosus");
            m_DbContext.Database.ExecuteSqlCommand("delete from own_funcesp.sau_tbl_impugnacaosus");        
        }
    }
}
