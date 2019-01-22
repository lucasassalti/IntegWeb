using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegWeb.Administracao.Aplicacao.DAL;
using IntegWeb.Entidades;
using IntegWeb.Framework;

namespace IntegWeb.Administracao.Aplicacao
{
    public class SistemaBLL
    {
        public Sistema Consultar(byte codigo)
        {
            return new SistemaDAL().Consultar(codigo);
        }

        public List<Sistema> Listar()
        {
            return new SistemaDAL().Listar();
        }

        public Resultado Incluir(Sistema objSistema)
        {
            Resultado retorno = new Resultado();

            if (objSistema.Nome.Trim() == string.Empty)
            {
                retorno.Erro("Nome não informada");
                return retorno;
            }


            retorno = new SistemaDAL().Incluir(objSistema);

            return retorno;
        }

        public Resultado Alterar(Sistema objSistema)
        {
            Resultado retorno = new Resultado();

            if (objSistema.Codigo == byte.MinValue)
            {
                retorno.Erro("Código não informado");
                return retorno;
            }

            if (String.IsNullOrEmpty(objSistema.Nome))
            {
                retorno.Erro("Nome não informada");
                return retorno;
            }

            retorno = new SistemaDAL().Alterar(objSistema);

            return retorno;
        }

        public Resultado AlterarStatus(byte codigo, int status)
        {
            Resultado retorno = new Resultado();

            if (codigo == byte.MinValue)
            {
                retorno.Erro("Código não informado");
                return retorno;
            }

            retorno = new SistemaDAL().AlterarStatus(codigo, status);

            return retorno;
        }
    }
}
