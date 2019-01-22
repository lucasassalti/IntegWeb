using IntegWeb.Entidades;
using IntegWeb.Entidades.Administracao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Administracao.Aplicacao.BLL
{
    public class GrupoAcessosBLL
    {
        public List<GrupoAcessos> ListarGrupo(GrupoAcessos grupo)
        {
            return new GrupoAcessosDAL().ConsultarGrupo(grupo);
        }

        public List<GrupoUsuario> ListarUsuario(GrupoAcessos grupo)
        {
            return new GrupoAcessosDAL().ConsultarUsuario(grupo);
        }

        public List<GrupoMenu> ListarMenu(GrupoAcessos grupo)
        {
            return new GrupoAcessosDAL().ConsultarMenu(grupo);
        }

        public DataTable ListarMenuSistema(int id_sistema)
        {
            return new GrupoAcessosDAL().ConsultarMenuSistema(id_sistema);
        }

        public DataTable ListarPagina(int id_grupo)
        {
            return new GrupoAcessosDAL().ConsultarPagina(id_grupo);
        }
        public DataTable ListarUsuario(int id_grupo)
        {
            return new GrupoAcessosDAL().ConsultarUsuario(id_grupo);
        }


        public GrupoAcessos ListarMenuUsuario(GrupoAcessos grupo)
        {
            return new GrupoAcessosDAL().ConsultarUsuarioMenu(grupo);
        }

        public DataSet ListarDrop()
        {

            return new GrupoAcessosDAL().PopularDropDow();

        }

        public List<GrupoAcessos> ListarGrupoMenuUsuario(GrupoAcessos grupo)
        {
           
            return new GrupoAcessosDAL().ConsultarGrupo(grupo);
        }

        public bool InserirGrupo(GrupoAcessos grupo, out string msg)
        {
            bool isErro = ValidaObjGrupo(grupo, out msg);
            bool retorno = false;
            if (!isErro)
            {
                retorno = new GrupoAcessosDAL().InsereGrupo(grupo);

                if (retorno)
                    msg = "Registro inserido com sucesso!";
                else
                    msg = "Problemas ao Inserir Grupo!";
            }
            return retorno;
        }

        public bool AtualizarGrupo(GrupoAcessos grupo, out string msg)
        {
            bool isErro = ValidaObjGrupo(grupo, out msg);
            bool retorno = false;
            if (grupo.id_grupo_acessos > 0)
            {
                if (!isErro)
                {
                    retorno = new GrupoAcessosDAL().AtualizaGrupo(grupo);

                    if (retorno)
                        msg = "Registro alterado com sucesso!";
                    else
                        msg = "Problemas ao alterar Grupo!";
                }
            }
            else
            {
                msg = "Problemas contate o administrador do sistema";
            }
            return retorno;
        }

        public bool AlterarStatus(GrupoAcessos grupo, out string msg)
        {
            bool retorno = false;
            if (grupo.id_grupo_acessos > 0)
            {

                retorno = new GrupoAcessosDAL().AlterarStatusGrupo(grupo);

                if (retorno)
                    msg = "Status do grupo alterado!";
                else
                    msg = "Problemas ao alterar status Grupo!";

            }
            else
            {
                msg = "Problemas contate o administrador do sistema";
            }
            return retorno;
        }

        public bool ValidaObjGrupo(GrupoAcessos grupo, out string msg)
        {

            msg = "";
            bool isErro = false;
            if (grupo.descricao == "")
            {
                msg = "Informe a descrição!";
                isErro = true;
            }
            if (grupo.area == "")
            {
                msg = "Informe a área!";
                isErro = true;
            }
            if (grupo.nome == "")
            {
                msg = "Informe o nome do grupo!";
                isErro = true;
            }
            return isErro;
        }

        public bool InserirUsuarioGrupo(GrupoAcessos grupo,  UsuarioSistema user, out string msg)
        {

            bool isErro = false;
            bool retorno = false;
            msg = "Erro! \\n";

            if (grupo.id_grupo_acessos == 0)
            {
                msg += "Selecione um grupo. \\n";
                isErro = true;
            }
            if (grupo.usuarios[0].listid.Equals("0"))
            {
                msg += "É necessário vincular um usuário ao grupo. \\n";
                isErro = true;
            }
            if (!isErro)
            {
                retorno=new GrupoAcessosDAL().InsereUsuarioGrupo(grupo, user);

                if (retorno)
                    msg = "Registro inserido com sucesso!";
                else
                    msg = "Problemas ao Inserir Usuário! \\nVerifique se o usuário esta vinculado ao grupo!";
            }
            return retorno;
        }

        public bool InativarUsuarioGrupo(GrupoAcessos grupo, UsuarioSistema user)
        {

            bool isErro = false;

            if (grupo.id_grupo_acessos == 0 || grupo.usuarios[0].matricula == 0  )
            {
                isErro = true;
            }
       
            if (!isErro)
            {
                isErro = new GrupoAcessosDAL().InativarUsuarioGrupo(grupo, user); ;
            }
            return isErro;
        }

        public bool InserirMenuGrupo(GrupoAcessos grupo, out string msg)
        {

            bool isErro = false;
            bool retorno = false;
            msg = "Erro! \\n";

            if (grupo.id_grupo_acessos == 0)
            {
                msg += "Selecione um grupo. \\n";
                isErro = true;
            }
            if (grupo.menus[0].listids.Equals("0"))
            {
                msg += "É necessário vincular uma página ao grupo. \\n";
                isErro = true;
            }
            if (!isErro)
            {
                retorno = new GrupoAcessosDAL().InserePaginaGrupo(grupo);

                if (retorno)
                    msg = "Registro inserido com sucesso!";
                else
                    msg = "Problemas ao Inserir Página! \\nVerifique se a página esta vinculado ao grupo!";
            }
            return retorno;
        }

        public bool InativarPaginaGrupo(GrupoAcessos grupo)
        {

            bool isErro = false;
            bool retorno = false;

            if (grupo.id_grupo_acessos == 0 || grupo.menus[0].id_menu == 0)
            {
                isErro = true;
            }

            if (!isErro)
            {
                retorno = new GrupoAcessosDAL().InativarPaginaGrupo(grupo);
            }
            return retorno;
        }

        public List<GrupoAcessos> ConsultarGrupoUsuario(GrupoAcessos grupo)
        {


            return new GrupoAcessosDAL().ConsultarUsuarioGrupo(grupo);
        
        } 

        public List<GrupoAcessos> ConsultarPaginaUsuario(GrupoAcessos grupo)
        {


            return new GrupoAcessosDAL().ConsultarPaginaGrupo(grupo);
        
        }
    }
}
