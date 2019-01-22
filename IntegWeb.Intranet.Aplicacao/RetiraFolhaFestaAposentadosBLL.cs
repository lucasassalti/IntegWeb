using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Intranet.Aplicacao.DAL;

namespace IntegWeb.Intranet.Aplicacao
{
    public class RetiraFolhaFestaAposentadosBLL:RetiraFolhaFestaAposentadoDAL
    {
        public DataTable retornaUsuarioMatricula(string matricula)
        {
            DataTable dt = new DataTable();
           dt = selecionaUsuarioMatricula(matricula);
            return dt;
        }

        public DataTable retornaUsuarioNome(string nome)
        {
            DataTable dt = selecionaUsuarioNome(nome);
            return dt;
        }

        public void insereUsuarioDisque(string matricula, string nome)
        {
            insereUsuario(matricula, nome);

        }

        public DataTable consultaNomeUsuario()
        {
            DataTable dt = consultaNome();
            return dt;

        }

        public void excluiUsuarioBLL(string matricula)
        {
            excluiUsuario(matricula);
        }
    }
}
