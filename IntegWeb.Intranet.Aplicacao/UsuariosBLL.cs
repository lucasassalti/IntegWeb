using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; 
using Intranet.Aplicacao.DAL;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Framework;

namespace Intranet.Aplicacao

{
    public class UsuariosBLL : UsuariosDAL
    {        
        public new List<FUN_TBL_USUARIO> Listar()
        {
            return base.Listar();
        }

        public new List<FUN_TBL_USUARIO> ListarPorDepartamento(string DEPARTAMENTO)
        {
            return base.ListarPorDepartamento(DEPARTAMENTO);
        }

        public List<object> ListarCustom(string DEPARTAMENTO)
        {

            IEnumerable<FUN_TBL_USUARIO> SelectItem = from c in base.ListarPorDepartamento("Desenvolvimento de Sistemas")
                                                      select c;

            SelectItem = SelectItem.Select(funUser => new FUN_TBL_USUARIO
            {
                NOME = String.Format("{0} ({1})", funUser.NOME, funUser.LOGIN),
                ID_USUARIO = funUser.ID_USUARIO
            });

            return SelectItem.ToList<object>();
        }

        public new FUN_TBL_USUARIO Carregar(string ID_USUARIO)
        {
            return base.Carregar(ID_USUARIO);
        }
    }
}