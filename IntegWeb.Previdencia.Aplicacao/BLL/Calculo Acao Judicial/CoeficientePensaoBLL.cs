using IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial
{
    public class CoeficientePensaoBLL : CoeficientePensaoDAL
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

            if (num_idntf_rptant == null)
            {

                str.Append("Informe o representante.\\n");
            }

            if (ano_de == null)
            {

                str.Append("Informe o ano de\\n");
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

            if (num_idntf_rptant == null)
            {
                str.Append("Informe o representante.\\n");
            }

            if (ano_de == null)
            {
                str.Append("Informe o ano de\\n");
            }

            if (mes_de == null)
            {
                str.Append("Informe o mes de\\n");
            }

            if (ano_ate == null)
            {
                str.Append("Informe o ano de\\n");
            }

            if (mes_ate == null)
            {
                str.Append("Informe o mes ate\\n");
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
