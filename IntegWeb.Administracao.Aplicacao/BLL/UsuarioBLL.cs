using IntegWeb.Administracao.Aplicacao.DAL;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Administracao.Aplicacao.BLL
{
    public class UsuarioBLL
    {
        public List<UsuarioSistema> ConsultarUsuario(UsuarioSistema user)
        {
            return new UsuarioDAL().ConsultarUsuario(user);
        }

        public List<UsuarioHistorico> ConsultarMovimentacao(UsuarioSistema user)
        {
            return new UsuarioDAL().ConsultarMovimentacao(user);
        }

        public bool ImportaDados( DataTable dt) {

            return new UsuarioDAL().ImportaDados(dt);
        
        }

        public bool BuscaUsuarioAD(out string  msg ) {
            msg = "";
            bool ret = false;
            try
            {
                DataTable dt = new ConectaAD().ListarTodosUsuariosAD();


                if (dt.Rows.Count > 0)
                {
                    ImportaDados(dt);
                    ret = true;
                }
                else
                {
                    msg = "Problema ao conectar no Active Directory, tente novamente mais tarde!!!";
                    ret = false;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Atenção!\\n\\nErro ao Listar Usuarios AD .\\n\\nMotivo:\\n\\n" + ex.Message);
            }
          
            return ret;
        
        }

        public bool InsereUsuarioAd(out string msg)
        {
            bool ret = false;
            if (BuscaUsuarioAD(out msg))
            {
                ret = new UsuarioDAL().InsereUsuarioAD();
                if (ret)
                    msg = "Registros Importados com Sucesso!!!";
                else
                    msg = "Não existem usuários novos!!!";
            }
           
            return ret;
        }

        public bool InativarUsuario(UsuarioHistorico user, out string msg)
        {
            msg = "";
            bool ret = true;
            if (string.IsNullOrEmpty(user.ds_justitificativa))
            {
                msg = "Digite uma justificativa!";
                ret = false;
            }
            if (string.IsNullOrEmpty(user.login_aplicacao) || string.IsNullOrEmpty(user.login) || user.login==null)
            {
                msg = "Problemas contate o administrador do sistema";
                ret = false;
            }
            if (ret)
            {
                ret=new UsuarioDAL().InativarUsuario(user);

                if (ret)
                    msg = "Usuário Inativado com sucesso!!";
                else
                    msg = "Problemas contate o administrador do sistema";
                
            }

            return ret;
    
       }
    }
}
