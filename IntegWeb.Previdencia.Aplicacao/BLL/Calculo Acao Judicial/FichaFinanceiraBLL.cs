using IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial;
using IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial
{
    public class FichaFinanceiraBLL : FichaFinanceiraDal
    {
        StringBuilder str = new StringBuilder();
       
        public bool ExcluirVerba(out string mensagem)
        {
            if (num_matr_partf == 0)
            {

                str.Append("Participante não Existe!\\n");
            }

            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            return DeletarVerba(out  mensagem);
        }

        public bool AlteraVerba(out string mensagem)
        {
            
            if (num_matr_partf == 0)
            {

                str.Append("Participante não Existe!\\n");
            }

            if (!VerificaUsuarioVerba())
            {
                mensagem = "Existem verbas de incorporação para esse usuário";
                return false;
            }

            if (str.Length > 0)
            {
                mensagem = str.ToString();
                return false;
            }

            bool ret=TrocarVerba();

            if (ret)
            {
                mensagem = "Processado com sucesso";
            }
            else
                mensagem = "Não existem Verbas de Simulação para o usuário";

            return ret;
        }

        public bool InsereVerba(List<FichaFinanceira> list, out string mensagem )
        {
         
            mensagem = "";
            bool ret =true;
            if (list.Count >0)
            {
               num_matr_partf= list[0].num_matr_partf;

                //Verifica se o usuário tem verbas de incorporação
               if (VerificaUsuarioVerba())
               {
                   //Insere verbas na tabela de temporária
                   if (InserirVerba(list))
                   {        //Faz a consistencia de verbas antes de inserir na tabela
                        if (ConsistenciaVerba(list[0].dataInclusao, out mensagem))
                        {
                            mensagem = "Processado com Sucesso";
                        } 
                        else
                            mensagem = "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\nDevido à tentativa de carregar uma verba não configurada para os processos relacionados à ações judiciais.\\n\\nVerbas permitidas: 80998, 80991, 80992, 80995, 80996, 80998,81991, 81995, 81996, 81998";
	                }
                   else
                       mensagem = "Problemas contate o administrador de sistemas (Inserir Verba)";
               }
               else
               {
                   mensagem = "Existem verbas de incorporação para esse usuário";
                   return false;
               }
            }
            else
                mensagem = "Problemas contate o administrador de sistemas";

            return ret;
        }

        public DataTable CarregaVerbasIncorporacao()
        {
            return ListarVerbasIncorporacao();
        }

    
    }
}
