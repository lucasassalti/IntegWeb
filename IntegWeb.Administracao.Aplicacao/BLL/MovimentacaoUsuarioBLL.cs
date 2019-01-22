using IntegWeb.Administracao.Aplicacao.DAL;
using IntegWeb.Entidades.Administracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Administracao.Aplicacao.BLL
{
    public class MovimentacaoUsuarioBLL
    {

        public List<MovimentacaoUsuario> Consultar(MovimentacaoUsuario mov)
        {
            return new MovimentacaoUsuarioDAL().ConsultarMovimentacao(mov);
        }
    }
}
