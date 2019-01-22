using IntegWeb.Entidades.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IntegWeb.Framework.Aplicacao
{
    public class UsuariosPortalBLL
    {

        public UsuarioPortal ConsultaUsuariosPortal(int CodEmpresa, int CodMatricula, int NumIdntfRptant)
        {
            return ListarUsuariosPortal(CodEmpresa, CodMatricula, NumIdntfRptant).FirstOrDefault();
        }

        public UsuarioPortal ConsultaUsuariosPortal(Singlesignon sso)
        {
            UsuarioPortal up = new UsuarioPortal();
            up = ConsultaUsuariosPortal(sso.Empresa, sso.Prontuario, sso.Representante);
            up.login = up.Nome.Trim() + "_" + up.Sobrenome.Trim() + "-" + up.COD_EMPRS + "-" + up.NUM_RGTRO_EMPRG + "-" + up.NUM_IDNTF_RPTANT;
            up.ListaEmpresas = sso.ListaEmpresas;
            return up;
        }

        public UsuarioPortal ConsultaUsuariosPortal(ConectaAD cAD)
        {
            UsuarioPortal up = new UsuarioPortal();            
            up.login = cAD.login;
            //up.ListaEmpresas = sso.ListaEmpresas;
            return up;
        }

        public List<UsuarioPortal> ListarUsuariosPortal(int? CodEmpresa, int? CodMatricula, int? NumIdntfRptant)
        {
            return new UsuariosPortalDAL().ListarUsuariosPortal(CodEmpresa, CodMatricula, NumIdntfRptant); 
        }

    }
}
