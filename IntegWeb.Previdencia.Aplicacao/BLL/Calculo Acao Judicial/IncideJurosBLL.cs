using IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial
{
    public class IncideJurosBLL : IncideJurosDAL
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

            if (dt_inic_vig == null)
            {

                str.Append("Informe a data de inicio\\n");
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

            if (tx_juros == null)
            {
                str.Append("Informe a taxa de juros\\n");
            }

            if (dt_inic_vig == null)
            {
                str.Append("Informe a data de início vigente\\n");
            }

            if (dt_fim_vig == null)
            {
                str.Append("Informe a data fim  vigente\\n");
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
