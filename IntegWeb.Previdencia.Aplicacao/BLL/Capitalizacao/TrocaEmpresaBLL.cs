using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao
{
    public class TrocaEmpresaBLL : TrocaEmpresaDAL
    {

        public Resultado Validar(string num_matr_partf)
        {
            Resultado retorno = new Resultado(true);

            if (num_matr_partf == null || num_matr_partf == "")
            {
                retorno.Erro("Campo Obrigatório");
                return retorno;
            }

            return retorno;

        }
    }
}
