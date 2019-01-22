using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegWeb.Administracao.Aplicacao.DAL;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Data;

namespace IntegWeb.Administracao.Aplicacao
{
    public class MenuBLL
    {

        public DataTable Consultar(Menu codigo)
        {
            return new MenuDAL().Consultar(codigo);
        }

        public List<Menu> Listar()
        {
            return new MenuDAL().Listar();
        }

        public List<Menu> ListarGrupo()
        {
            return new MenuDAL().Listar();
        }

        public List<Menu> ListarPorSistema(byte codigoSistema)
        {
            return new MenuDAL().ListarPorSistema(codigoSistema);
        }

        public List<Menu> ListarPorNivel(byte codigoSistema, short nivel, int? codigoMenuPai)
        {
            return new MenuDAL().ListarPorNivel(codigoSistema, nivel, codigoMenuPai);
        }

        public List<Menu> ListarPorUsuario(byte codigoSistema, short nivel, int? codigoMenuPai, string login)
        {
            return new MenuDAL().ListarPorUsuario(codigoSistema, nivel, codigoMenuPai, login);
        }

        public List<string> ListarAcesso(byte codigoSistema,  string login)
        {
            return new MenuDAL().ListarAcesso(codigoSistema, login);
        }

        public List<string> ListarAcessoPorGrupo(byte codigoSistema, string[] grupos)
        {
            List<string> lsGrupos = new List<string>();
            foreach (string grupo in grupos)
            {
                lsGrupos.AddRange(new MenuDAL().ListarAcessoPorGrupo(codigoSistema, grupo));
                //MenuDAL().ListarAcessoPorGrupo(codigoSistema, grupos);
            }
            return lsGrupos;

        }

        public Resultado Incluir(Menu objMenu)
        {
            Resultado retorno = new Resultado();

            //VALIDAÇÕES/REGRAS DE NEGÓCIO
            if (objMenu.Codigo == byte.MinValue)
            {
                retorno.Erro("Código não informado");
                return retorno;
            }

            if (objMenu.Nome.Trim() == string.Empty)
            {
                retorno.Erro("Nome não informada");
                return retorno;
            }

            if (objMenu.Link.Trim() == string.Empty && objMenu.Nivel == 4)
            {
                retorno.Erro("Link não informado");
                return retorno;
            }

            if (objMenu.MenuPai.Codigo == int.MinValue && objMenu.Nivel > 1)
            {
                retorno.Erro("Menu pai não informado");
                return retorno;
            }

            //TÉRMINO DAS VALIDAÇÕES

            retorno = new MenuDAL().Incluir(objMenu);

            return retorno;
        }

        public Resultado Alterar(Menu objMenu)
        {
            Resultado retorno = new Resultado();

            if (objMenu.Codigo == byte.MinValue)
            {
                retorno.Erro("Código não informado");
                return retorno;
            }

            if (String.IsNullOrEmpty(objMenu.Nome))
            {
                retorno.Erro("Nome não informada");
                return retorno;
            }

            if (objMenu.Link.Trim() == string.Empty && objMenu.Nivel == 4)
            {
                retorno.Erro("Link não informado");
                return retorno;
            }

            if (objMenu.MenuPai.Codigo == int.MinValue && objMenu.Nivel > 1)
            {
                retorno.Erro("Menu pai não informado");
                return retorno;
            }

            retorno = new MenuDAL().Alterar(objMenu);

            return retorno;
        }

        public Resultado AlterarStatus(int codigo, int status)
        {
            Resultado retorno = new Resultado();

            if (codigo == byte.MinValue || status < int.MinValue)
            {
                retorno.Erro("Problemas contate o administrador de sistemas!");
                return retorno;
            }

            retorno = new MenuDAL().AlterarStatus(codigo, status);

            return retorno;
        }

        public object ListarPorSistema()
        {
            throw new NotImplementedException();
        }
    }
}
