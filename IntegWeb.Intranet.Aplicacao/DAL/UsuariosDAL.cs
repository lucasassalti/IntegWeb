using IntegWeb.Intranet.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace Intranet.Aplicacao.DAL
{
    public class UsuariosDAL
    {
        private INTRA_Entity_Conn m_DbContext = new INTRA_Entity_Conn();
        public List<FUN_TBL_USUARIO> Listar()
        {
            var query = from c in m_DbContext.FUN_TBL_USUARIO 
                        orderby c.NOME 
                        select c;
            return query.ToList();
        }

        public List<FUN_TBL_USUARIO> ListarPorDepartamento(string sDEPARTAMENTO)
        {
            var query = from c in m_DbContext.FUN_TBL_USUARIO
                        where c.DEPARTAMENTO.Contains(sDEPARTAMENTO)
                           && c.STATUS == 1 
                        orderby c.NOME
                        select c;
            return query.ToList();
        }

        public FUN_TBL_USUARIO Carregar(string sIdUsuario)
        {
            decimal dID_USUARIO;
            decimal.TryParse(sIdUsuario, out dID_USUARIO);

            var query = from c in m_DbContext.FUN_TBL_USUARIO
                        where c.ID_USUARIO == dID_USUARIO
                        orderby c.NOME
                        select c;
            return query.FirstOrDefault();
        }

     }
}