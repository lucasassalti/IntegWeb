using IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial
{
    public class IndiceRevisaoBLL : IndiceRevisaoDAL
    {
        StringBuilder str = new StringBuilder();

        public DataTable CarregarLista()
        {
            return Listar();
        }

        public bool DeletarRegistro(out string mensagem)
        {

            if (num_matr_partf == null)
            {

                str.Append("Informe o participante.\\n");
            }

            if (num_lei == null)
            {

                str.Append("Informe a lei.\\n");
            }

            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            return Deletar(out  mensagem);
        }

        public bool InserirRegistro(out string mensagem)
        {

            if (num_matr_partf == null)
            {
                str.Append("Informe o participante.\\n");
            }

            if (num_lei == null)
            {
                str.Append("Informe o Número da lei .\\n");
            }

            if (ano_lei == null)
            {
                str.Append("Informe o ano da lei.\\n");
            }

            if (num_lei == null)
            {
                str.Append("Informe o número da lei.\\n");
            }


            if (mes_lei == null)
            {
                str.Append("Informe o  mês da lei.\\n");
            }

            if (ind_lei == null)
            {
                str.Append("Informe indice de lei.\\n");
            }



            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            return Inserir(out  mensagem);
        }
    }
}
