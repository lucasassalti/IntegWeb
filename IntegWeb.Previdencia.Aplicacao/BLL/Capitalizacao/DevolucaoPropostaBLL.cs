using IntegWeb.Previdencia.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class DevolucaoPropostaBLL : DevolucaoPropostaDAL
    {
        public bool ValidaCampos(out string mensagem, bool isUpdate)
        {

            bool ret = false;
            StringBuilder str = new StringBuilder();

            if (registro==null)
            {
                str.Append("Informe o Registro Empregado!\\n");

            }

            if (string.IsNullOrEmpty(nome))
            {
                str.Append("Informe o Nome do Empregado!\\n");

            }

            if (dt_devolucao == null)
            {
                str.Append("Informe a Data de Devolução!\\n");

            }


            if (cod_emprs==null)
            {
                str.Append("Informe o Código da Empresa!\\n");

            }

            if (string.IsNullOrEmpty(destinatario))
            {
                str.Append("Informe o Destinatário!\\n");

            }

            if (string.IsNullOrEmpty(desc_motivo_dev))
            {
                str.Append("Informe o Motivo da Devolução!\\n");

            }

            if (str.Length>0)
            {
                mensagem = str.ToString();
                return false;
            }


            if (isUpdate)
                ret = Alterar(out mensagem);
            else

                ret = Inserir(out mensagem);

            return ret;

        }

        public DataTable ListarDevolucao()
        {

            return SelecionarDevolucao();
        }

        public bool DeletarDevolucao(out string msg)
        {

            return Deletar(out msg);
        }
    }
}
