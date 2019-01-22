using IntegWeb.Intranet.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Intranet.Aplicacao.WS_FuncaoExtra_CRM;
using System.ServiceModel;
using System.IO;

namespace IntegWeb.Intranet.Aplicacao
{
    public class EmailCancelamentoPlanoBLL : EmailCancelamentoPlanoDAL
    {
        /// <summary>
        /// Anexa o email na manisfestação
        /// </summary>
        /// <param name="Arquivo"></param>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public String Anexar_Email_Manifest(String Arquivo, Classe_Manifestacao Filtro)
        {
            String resultado = "";

            try
            {
                //Criar Request para chamar WebService
                adicionarArquivoFuncaoExtraRequest xRequest = new adicionarArquivoFuncaoExtraRequest();
                // Teste // Adicionar parametros
                byte[] blob = File.ReadAllBytes(@Arquivo);
                xRequest.strIdChamado = Filtro.NrChamado;
                xRequest.strManiNrSequencia = Filtro.NrManifestacao;
                xRequest.strNomeArquivo = Path.GetFileName(Arquivo);
                xRequest.arquivo = blob;

                // Criar instancia da Interface
                ChannelFactory<WebServiceArquivoFuncaoExtra> factory = new ChannelFactory<WebServiceArquivoFuncaoExtra>("WebServiceArquivoFuncaoExtra");
                var proxy = factory.CreateChannel();

                // Criar response com o resultado da chamada a interface
                adicionarArquivoFuncaoExtraResponse xResponse = proxy.adicionarArquivoFuncaoExtra(xRequest);
                //var response = proxy.adicionarArquivoFuncaoExtra(xRequest);

                // Liberar 
                ((IDisposable)proxy).Dispose();

                // retornar valor
                resultado = xResponse.adicionarArquivoFuncaoExtraReturn[0].descErro;

                blob = null;

            }
            catch (Exception ee)
            {
                resultado = "erro " + ee.Message;
            }

            return resultado;
        }

        public class Classe_Manifestacao
        {
            public int TipoDocumento;
            public String CodigoEmpresa;
            public String Registro;
            public String Representante;
            public String ParticipanteNome;
            public String ParticipanteEmail;
            public String CPF;
            public String NrChamado;
            public String NrManifestacao;

        }


    }
}
