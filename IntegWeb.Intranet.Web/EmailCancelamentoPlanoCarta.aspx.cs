using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Text;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.css;
using IntegWeb.Entidades.Atendimento.Saude;
using IntegWeb.Framework;
using System.Net.Mail;
using IntegWeb.Entidades.Framework;
using System.Net.Mime;
using System.Net;
using IntegWeb.Entidades;
using System.Threading;
using IntegWeb.Intranet.Aplicacao;
using Intranet.Aplicacao.BLL;

namespace IntegWeb.Intranet.Web
{
    public partial class EmailCancelamentoPlanoCarta : BasePage
    {
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string email;
        int enviarEmail;
        string empresa;
        string matricula;
        string nrepr;
        string ResponsavelPlano;
        string protocoloCancelamento;

        protected IntegWeb.Intranet.Aplicacao.EmailCancelamentoPlanoBLL.Classe_Manifestacao ler_variaveis_ambiente()
        {
            IntegWeb.Intranet.Aplicacao.EmailCancelamentoPlanoBLL.Classe_Manifestacao filtro = new IntegWeb.Intranet.Aplicacao.EmailCancelamentoPlanoBLL.Classe_Manifestacao();
            filtro.CodigoEmpresa = Request.QueryString["nempr"];
            filtro.Registro = Request.QueryString["nreg"];
            filtro.Representante = Request.QueryString["nrepr"];
            filtro.ParticipanteNome = Request.QueryString["cpart"];
            filtro.ParticipanteEmail = Request.QueryString["ParticipanteEmail"];
            filtro.NrChamado = Request.QueryString["idChamCdChamado"];
            filtro.NrManifestacao = Request.QueryString["mani_nr_sequencia"];

            return filtro;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            email = Request.QueryString["ParticipanteEmail"]; //"bruno.borges@funcesp.com.br";
            enviarEmail = Convert.ToInt16(Request.QueryString["enviarEmail"]); //"bruno.borges@funcesp.com.br";

                       if (!IsPostBack)
            {
                //Email ou Impressao/visualizar
                if (enviarEmail == 1)
                {
                    InicializaDadosEMail();
                }
                else
                {
                    ConverteAspx2Pdf();
                }
            }
        }

        void limpar()
        {
            lblPlano1.Text = "";
            lblPlano2.Text = "";
            lblPlano3.Text = "";

            lblBeneficiario1.Text = "";
            lblBeneficiario2.Text = "";
            lblBeneficiario3.Text = "";
        }

        private void ConverteAspx2PdfEnviaEmail()
        {
            // Cria o documento aplicando o tamanho e margens
            //Document documento = new Document(PageSize.A4, 80, 50, 30, 65);
            Document documento = new Document(PageSize.A4, 30, 30, 20, 20);
            //Document documento = new Document();      


            try
            {// Memory Stream para ser usado na conversão e emissão
                using (MemoryStream ms = new MemoryStream())
                {
                    // Inicializa o gravador
                    PdfWriter writer = PdfWriter.GetInstance(documento, ms);

                    StringWriter stw = new StringWriter();

                    HtmlTextWriter htextw = new HtmlTextWriter(stw);

                    // Objeto de conversão do HTML
                    HTMLWorker objeto = new HTMLWorker(documento);
                    // Abre o documento
                    documento.Open();

                    var list = Session["sessionCancelamento"] as List<Cancelamento>;

                    lblDataCancelamento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    lblHoraCancelamento.Text = DateTime.Now.ToString("HH:mm:ss");

                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            lblMatricula.Text = list[i].matricula;
                            lblEmpresa.Text = list[i].empresa;
                            lblProtocoCancelamento.Text = list[i].protocolo;
                            lblResponsavelPlano.Text = list[i].responsavel;

                            if (i % 3 == 0)
                            {
                                limpar();
                                lblPlano1.Text = list[i].nomePlano;
                                lblBeneficiario1.Text = list[i].nomeBeneficiario;
                            }
                            else if (i % 3 == 1)
                            {
                                lblPlano2.Text = list[i].nomePlano;
                                lblBeneficiario2.Text = list[i].nomeBeneficiario;
                            }
                            else if (i % 3 == 2)
                            {
                                lblPlano3.Text = list[i].nomePlano;
                                lblBeneficiario3.Text = list[i].nomeBeneficiario;
                                conteudo.RenderControl(htextw);
                            }

                            if (i == (list.Count - 1))
                            {
                                if (i % 3 != 2)
                                {
                                    conteudo.RenderControl(htextw);
                                }
                                using (TextReader reader = new StringReader(stw.ToString()))
                                {
                                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, documento, reader);
                                }
                            }
                        }
                    }

                    // Fecha o documento
                    documento.Close();

                    // Força o download do PDF gerado - se desejável você pode salvar em disco também
                    Response.Clear();
                    byte[] bytes = ms.ToArray();
                    Response.Buffer = true;
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    Response.OutputStream.Flush();
                    documento.Dispose();
                    conteudo.Dispose();

                    enviarEmailPdf(bytes);

                }
            }
            finally
            {
                voltarTelaconsulta("EnviouEmail");
            }
        }

        private void ConverteAspx2Pdf()
        {
            // Cria o documento aplicando o tamanho e margens
            //Document documento = new Document(PageSize.A4, 80, 50, 30, 65);
            Document documento = new Document(PageSize.A4, 30, 30, 20, 20);
            //Document documento = new Document();      
            try
            {
                // Memory Stream para ser usado na conversão e emissão
                MemoryStream ms = new MemoryStream();
                // Inicializa o gravador
                PdfWriter writer = PdfWriter.GetInstance(documento, ms);

                StringWriter stw = new StringWriter();

                HtmlTextWriter htextw = new HtmlTextWriter(stw);

                // Objeto de conversão do HTML
                HTMLWorker objeto = new HTMLWorker(documento);
                // Abre o documento
                documento.Open();

                var list = Session["sessionCancelamento"] as List<Cancelamento>;

                lblDataCancelamento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblHoraCancelamento.Text = DateTime.Now.ToString("HH:mm:ss");

                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        lblMatricula.Text = list[i].matricula;
                        lblEmpresa.Text = list[i].empresa;
                        lblProtocoCancelamento.Text = list[i].protocolo;
                        lblResponsavelPlano.Text = list[i].responsavel;

                        if (i % 3 == 0)
                        {
                            limpar();
                            lblPlano1.Text = list[i].nomePlano;
                            lblBeneficiario1.Text = list[i].nomeBeneficiario;
                        }
                        else if (i % 3 == 1)
                        {
                            lblPlano2.Text = list[i].nomePlano;
                            lblBeneficiario2.Text = list[i].nomeBeneficiario;
                        }
                        else if (i % 3 == 2)
                        {
                            lblPlano3.Text = list[i].nomePlano;
                            lblBeneficiario3.Text = list[i].nomeBeneficiario;
                            conteudo.RenderControl(htextw);
                        }

                        if (i == (list.Count - 1))
                        {
                            if (i % 3 != 2)
                            {
                                conteudo.RenderControl(htextw);
                            }
                            using (TextReader reader = new StringReader(stw.ToString()))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, documento, reader);
                            }
                        }
                    }
                }

                //return;

                // Fecha o documento                
                documento.Close();

                // Força o download do PDF gerado - se desejável você pode salvar em disco também
                Response.Clear();

                Response.AddHeader("content-disposition", "attachment; filename=CancelamentoPlano.pdf");
                //Response.AddHeader("content-disposition", "filename=CancelamentoPesNosso.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                //Limpandos os itens de memoria.
                documento.Dispose();
                documento.Close();
                conteudo.Dispose();

                //Response.End();

            }
            finally
            {
                // voltarTelaconsulta("Visualizar");
            }


        }
        
        #region 'EMAIL'

        public void enviarEmailPdf(byte[] bytes)
        {
            string emailCorpo = "Em resposta a sua solicitação, anexamos o Protocolo de exclusão do Plano de Saúde." + "<br/><br/>" +
                    "Caso tenha alguma dúvida poderá nos contatar por email: atendimento@funcesp.com.br, Disque Funcesp 11. 3065 3000 ou 0800 012 7173 <br/><br/>" +
                    "Atenciosamente, </p>";

            // DE
            string emailRemetente = "Atendimento Funcesp <atendimento@funcesp.com.br>";
            // Para
            string emailRecebidor = email;

            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            string str_periodo_dia = (horarioAtual < periodo_dia) ? "Bom Dia." : "Boa Tarde.";

            emailCorpo = "<p style='font-family:Arial, Helvetica, sans-serif; font-size:12px'>Sr(a). " +
                         str_periodo_dia + "<br/><br/>" +
                         emailCorpo;

            try
            {
                //Prepara email
                //string emailAssunto = "ESSE EMAIL É APENAS PARA TESTE PEÇO DESCONSIDERAR - Cancelamento de Plano de Saúde - RN-412";
                string emailAssunto = "Cancelamento de Plano de Saúde - RN-412";

                var contentID = "Image";
                var inlineLogo = new Attachment(Server.MapPath("img/assinatura_email.jpg"));

                //Enviar email.
                MailMessage mm = new MailMessage(emailRemetente, email);
                //MailMessage mm = new MailMessage(emailRemetente, "nilce.kamiji@funcesp.com.br");
                inlineLogo.ContentId = contentID;
                inlineLogo.ContentDisposition.Inline = true;
                inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                // mm.Attachments.Add(inlineLogo);

                emailCorpo = emailCorpo + "<img src=\"cid:" + contentID + "\">";

                mm.Attachments.Add(inlineLogo);

                mm.Subject = emailAssunto;
                mm.Body = emailCorpo;
                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "CancelamentoPlano.pdf"));
                mm.IsBodyHtml = true;

                SmtpClient mailClient = new Email().EnviaEmailMensagem(mm);
                montarAnexoViaEmailManifestacao(mailClient, mm);

            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO E-mail NÃO foi enviado.\\nMotivo:\\n" + ex.Message);
            }
        }

        void montarAnexoViaEmailManifestacao(SmtpClient mailClient,MailMessage mm){

            //Essa outra etapa serve para grava o arquivo na manifestação
            //salvar e-mail em disco
            String pastaTemporariaEmail = criar_pasta_temporaria("Email");
            mailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            mailClient.PickupDirectoryLocation = pastaTemporariaEmail;
            mailClient.Send(mm);

            IntegWeb.Intranet.Aplicacao.EmailCancelamentoPlanoBLL.Classe_Manifestacao filtro = ler_variaveis_ambiente();

            // Ler o e-mail salvo no passo anterior e renomear 
            string[] filePaths = Directory.GetFiles(pastaTemporariaEmail);
            String novo_nome_de_arquivo = pastaTemporariaEmail + "\\email_CancelamentoPlano_"  + "(Enviado em " + DateTime.Now.ToString("ddMMyyyy_hhm") + ").eml";
            System.IO.File.Move(@filePaths[0], novo_nome_de_arquivo);
                        
            // Anexar e-mail salvo em disco a aplicacao.
            EmailCancelamentoPlanoBLL CancelamentoPlanoBLL = new EmailCancelamentoPlanoBLL();
            String resultado = CancelamentoPlanoBLL.Anexar_Email_Manifest(novo_nome_de_arquivo, filtro);

            System.IO.File.Delete(novo_nome_de_arquivo);

            if (resultado != "Arquivo adicionado com sucesso!")
            {
                throw new ArgumentNullException(resultado);
            }
        
        }

        private bool InicializaDadosEMail()
        {
            if (String.IsNullOrEmpty(email))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nE-Mail obrigatório");
                return false;
            }
            else if (!Util.ValidaEmail(email))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nE-Mail inválido");
                return false;
            }
            ConverteAspx2PdfEnviaEmail();

            return true;
        }

        protected static String criar_pasta_temporaria(String sufixo)
        {
            // criar pasta temporaria para identificar o email       
            // Gerar numero randomico para criar a pasta
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int rnd = rndNum.Next(0, 1000000000);
            // criar a pasta utilizando a data e o numero randomico
            String pastaTemporaria = HttpContext.Current.Server.MapPath(@"UploadFile\CancelamentoPlano");
            if (!Directory.Exists(pastaTemporaria))
            {
                Directory.CreateDirectory(pastaTemporaria);
            }
            pastaTemporaria = pastaTemporaria + "\\" + sufixo + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + rnd.ToString();
            System.IO.Directory.CreateDirectory(pastaTemporaria);
            return pastaTemporaria;
        }

        //Voltar a pagina de consulta
        void voltarTelaconsulta(string tipoAcao)
        {
            ////Teste
            //empresa = "4";
            //matricula = "1225";
            //nrepr = "936666";
            //nrepr = "";
            //ResponsavelPlano = "Nilce";
            //email = "bruno.borges@funcesp.com.br";
            //protocoloCancelamento = "não sei como pegar"; //"0015181811581";

            //Por parametro
            empresa = Request.QueryString["nempr"];
            matricula = Request.QueryString["nreg"];
            nrepr = Request.QueryString["nrepr"];
            ResponsavelPlano = Request.QueryString["cpart"];
            email = Request.QueryString["ParticipanteEmail"]; //"bruno.borges@funcesp.com.br";
            protocoloCancelamento = "não sei como pegar"; //"0015181811581";

            StringBuilder urqValores = new StringBuilder();
            string voltouEnvioEmail = tipoAcao;
            urqValores.Append("nempr=" + empresa.ToString() + "&nreg=" + matricula.ToString());
            urqValores.Append("&ParticipanteEmail=" + email.ToString());
            urqValores.Append("&nrepr=" + nrepr.ToString());
            urqValores.Append("&tipoAcao=" + voltouEnvioEmail);
            urqValores.Append("&protocoloCancelamento=" + protocoloCancelamento.ToString());

            Response.Redirect("~/EmailCancelamentoPlano.aspx?" + urqValores.ToString() + "");

        }




        #endregion



    }
}