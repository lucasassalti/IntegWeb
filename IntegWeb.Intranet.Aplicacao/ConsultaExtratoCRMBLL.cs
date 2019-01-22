using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using Intranet.Aplicacao.DAL;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using IntegWeb.Intranet.Aplicacao.WS_FuncaoExtra_CRM;

namespace Intranet.Aplicacao.BLL
{
    public class ConsultaExtratoCRMBLL : ConsultaExtratoCRMDAL
    {
        public String Anexar_Email(String Arquivo, Filtro_Pesquisa Filtro)
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

    }

    public class Filtro_Pesquisa
    {
        public int TipoDocumento;
        public String CodigoEmpresa;
        public String Registro;
        public String Representante;
        public String PesquisaAnoInicio;
        public String PesquisaAnoFim;
        public String PesquisaMesInicio;
        public String PesquisaMesFim;
        public String ParticipanteNome;
        public String ParticipanteEmail;
        public String CPF;
        public String NrChamado;
        public String NrManifestacao;
        public String IdIdocs;
        public String Sexo;
        public String cnpj;
        public String contrato;
        //public String nom_convenente;

    }

    public class DocumentoPDF
    {
        public String Empresa;
        public String Matricula;
        public String Digito;
        public String Nome;
        public String TipoDocumento;
        public String Ano;
        public String Mes;
        public String Plano;
        public String DocumentoArquivo;
    }
}