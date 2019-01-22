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
    public class UserEngineBLL
    {

        public UsuarioPortal GetCurrentUser(ConectaAD ad, Singlesignon sso)
        {
            UsuarioPortal up = new UsuarioPortal();
            if (sso != null)
            {
                UsuariosPortalBLL uPortalBLL = new UsuariosPortalBLL();
                up = uPortalBLL.ConsultaUsuariosPortal(sso);
            }
            else if (ad != null)
            {
                up.login = ad.login;
                //up.login = up.Nome.Trim() + "_" + up.Sobrenome.Trim() + "-" + up.COD_EMPRS + "-" + up.NUM_RGTRO_EMPRG + "-" + up.NUM_IDNTF_RPTANT;
                up.login = ad.login + "_" + ad.nome.Replace(" ","_").Trim();
                up.ListaEmpresas = new short[] { 9999 };
            }
            else
            {
                up.login = "DESENV";
                up.ListaEmpresas = new short[] { 9999 } ;
            }
            return up;
        }

    }
}
