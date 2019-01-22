using IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial
{
    public class RevisaoLeiBLL : RevisaoLeiDAL
    {
        StringBuilder str = new StringBuilder();

        public DataTable CarregarLista()
        {
            return Listar();
        }

        public bool DeletarRegistro(out string mensagem)
        {

            if (num_lei == null)
            {

                str.Append("Informe o número da lei \\n");
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

            if (num_lei == null)
            {
                str.Append("Informe o número da lei .\\n");
            }

            if (dsc_lei == null)
            {
                str.Append("Informe a descrição lei.\\n");
            }

            if (data_inic_vig == null)
            {
                str.Append("Informe a data inicio vigência\\n");
            }

            if (data_fim_vig == null)
            {
                str.Append("Informe a data fim vigência\n");
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
