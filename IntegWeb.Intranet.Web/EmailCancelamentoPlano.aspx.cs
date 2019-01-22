using IntegWeb.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Intranet.Aplicacao;
using IntegWeb.Framework;
using System.Text;
using IntegWeb.Entidades.Atendimento.Saude;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Threading;

namespace IntegWeb.Intranet.Web
{
    public partial class EmailCancelamentoPlano : BasePage
    {
        Relatorio relatorio = new Relatorio();
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string empresa;
        string matricula;
        string nrepr;
        string email;
        string ResponsavelPlano;

        string protocoloCancelamento;
        string tipoAcao;
        string cham_ds_protocolo;
        string nQry_NumChamado;
        string nQry_NumManifestacao;
        string nQry_NumDep;
        string nQry_NumDigito;

        int enviarEmail;

        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(UpdatePanel,
                  UpdatePanel.GetType(),
                  "script",
                  "_client_side_script();",
                   true);

            //Teste
            //empresa = "4";
            //matricula = "1225";
            ////nrepr = "936666";
            //nrepr = "";
            //ResponsavelPlano = "Nilce";
            //email = "guilherme.provenzano@funcesp.com.br";
            //protocoloCancelamento = "não sei como pegar"; //"0015181811581";
            //txtEmail.Text = email;
            //hfEmpresa.Value = empresa;
            //hfMatricula.Value = matricula;
            //tipoAcao =Request.QueryString["tipoAcao"];

            ////Por parametro
            empresa = Request.QueryString["nempr"];
            matricula = Request.QueryString["nreg"];
            cham_ds_protocolo = Request.QueryString["cham_ds_protocolo"];
            nrepr = Request.QueryString["nrepr"];
            ResponsavelPlano = Request.QueryString["cpart"];
            email = Request.QueryString["ParticipanteEmail"]; //"bruno.borges@funcesp.com.br";
            //protocoloCancelamento = //"0015181811581";
            tipoAcao = Request.QueryString["tipoAcao"];
            nQry_NumChamado = Request.QueryString["idChamCdChamado"];
            nQry_NumManifestacao = Request.QueryString["mani_nr_sequencia"];
            nQry_NumDigito = Request.QueryString["ndigreg"];
            nQry_NumDep = Request.QueryString["ndep"];

            //limparDados();

            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(email) && email != "undefined")
                {
                    txtEmail.Text = email;
                }                
                hfEmpresa.Value = empresa;
                hfMatricula.Value = matricula;
                
                //protocoloCancelamento = selectRetornaProtocolo(nQry_NumChamado);

                verificaConsultaProtocolo();
                verificaConsultaCancelarPlano();
            }
        }

        void limparDados()
        {
            txtEmail.Text = "";
        }

        void verificaConsultaProtocolo()
        {
            DataTable dt = new DataTable();

            EmailCancelamentoPlanoBLL protocolo = new EmailCancelamentoPlanoBLL();

            if (string.IsNullOrEmpty(nQry_NumChamado))
            {
                protocoloCancelamento = "";
            }
            else
            {
                dt = protocolo.selectRetornaProtocolo(nQry_NumChamado.ToString());

                if (dt.Rows.Count >= 1)
                {
                    hfProtocolo.Value = dt.Rows[0]["cham_ds_protocolo"].ToString();
                }
                else
                {
                    protocoloCancelamento = "";
                }
            }
        }

        void verificaConsultaCancelarPlano()
        {
            DataTable dt = new DataTable();

            EmailCancelamentoPlanoBLL cancelPlano = new EmailCancelamentoPlanoBLL();

            if (string.IsNullOrEmpty(nrepr))
            {
                if (string.IsNullOrEmpty(empresa) || string.IsNullOrEmpty(matricula))
                {
                    dt = null;
                }
                else
                {
                    dt = cancelPlano.selectCancelarPlanoTitular(empresa, matricula);
                }
            }
            else
            {
                dt = cancelPlano.selectCancelarPlanoRepress(empresa, matricula, nrepr);
            }

            grdCancelPlano.DataSource = dt;
            grdCancelPlano.DataBind();
        }

        void redirect()
        {
            StringBuilder urqValores = new StringBuilder();

            urqValores.Append("nempr=" + empresa.ToString());
            urqValores.Append("&nreg=" + matricula.ToString());
            urqValores.Append("&nrepr=" + nrepr.ToString());
            urqValores.Append("&ParticipanteEmail=" + txtEmail.Text.ToString());
            urqValores.Append("&enviarEmail=" + enviarEmail.ToString());
            urqValores.Append("&mani_nr_sequencia=" + nQry_NumManifestacao.ToString());
            urqValores.Append("&ndep=" + (nQry_NumDep ?? "").ToString());
            urqValores.Append("&ndigreg=" + (nQry_NumDigito ?? "").ToString());
            urqValores.Append("&idChamCdChamado=" + (nQry_NumChamado ?? "").ToString());
            urqValores.Append("&cpart=" + ResponsavelPlano.ToString());
            urqValores.Append("&protocoloCancelamento=" + hfProtocolo.Value.ToString());

            Response.Redirect("~/EmailCancelamentoPlanoCarta.aspx?" + urqValores.ToString() + "");

        }

        public void btnImprimirSelecao_Click(object sender, EventArgs e)
        {
            ArquivoDownload adExtratoPdf = new ArquivoDownload();
            adExtratoPdf.nome_arquivo = "Rel_Carta_Cancelamento.pdf";
            adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + empresa.ToString() + "_" + matricula.ToString() + "_" + adExtratoPdf.nome_arquivo;
            adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;            
            Gera_Pdf(adExtratoPdf, true);
        }

        private void Gera_Pdf(ArquivoDownload adExtratoPdf, bool Download = false)
        {
            List<String> listaSubMatricula = new List<String>();

            foreach (GridViewRow row in grdCancelPlano.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string[] planos = new string[3];

                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    HiddenField hidSubMatricula = (HiddenField)row.FindControl("hidSubMatricula");

                    if (chkSelect.Checked == true)
                    {
                        listaSubMatricula.Add(hidSubMatricula.Value);
                    }
                }
            }

            if (listaSubMatricula.Count > 0)
            {

                String SubMats = String.Join(",", listaSubMatricula.ToArray());

                if (InicializaRelatorio(empresa.ToString(), matricula.ToString(), SubMats, ResponsavelPlano, hfProtocolo.Value))
                {

                    //ArquivoDownload adExtratoPdf = new ArquivoDownload();
                    //adExtratoPdf.nome_arquivo = "Rel_Carta_Cancelamento.pdf";
                    //adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + empresa.ToString() + "_" + matricula.ToString() + "_" + adExtratoPdf.nome_arquivo;
                    //adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                    if (Download)
                    {
                        Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
                        string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
                        AdicionarAcesso(fullUrl);
                        AbrirNovaAba(UpdatePanel, fullUrl, adExtratoPdf.nome_arquivo);
                    }
                }
            }
        }



        private bool InicializaRelatorio(string CodEmpresa, string CodMatricula, string SubMatriculas, string Responsavel, string Protocolo)
        {

            relatorio.titulo = "Carta de Cancelamento";
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = @"~/Relatorios/Rel_Cancela_Plano.rpt";
            relatorio.parametros.Add(new Parametro() { parametro = "cod_emprs", valor = CodEmpresa });
            relatorio.parametros.Add(new Parametro() { parametro = "num_matricula", valor = CodMatricula });
            relatorio.parametros.Add(new Parametro() { parametro = "num_sub_matric", valor = SubMatriculas });
            relatorio.parametros.Add(new Parametro() { parametro = "responsavel", valor = Responsavel });
            relatorio.parametros.Add(new Parametro() { parametro = "protocolo", valor = Protocolo });

            Session["Carta_Cancelamento"] = relatorio;
            ReportCrystal.RelatorioID = "Carta_Cancelamento";
            return true;

            //}
            //else return false;
        }

        protected void btnEnviarSelecao_Click(object sender, EventArgs e)
        {

            if (verificarEmail())
            {
                enviarEmail = 1;
                email = txtEmail.Text;

                ArquivoDownload adExtratoPdf = new ArquivoDownload();
                adExtratoPdf.nome_arquivo = "Rel_Carta_Cancelamento.pdf";
                adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + empresa.ToString() + "_" + matricula.ToString() + "_" + adExtratoPdf.nome_arquivo;
                adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                Gera_Pdf(adExtratoPdf);

                enviarEmailPdf(ReportCrystal.ExportarRelatorioPdf());

                if (File.Exists(adExtratoPdf.caminho_arquivo))
                {
                    File.Delete(adExtratoPdf.caminho_arquivo);
                }

            }
        }

        bool verificarEmail()
        {
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nE-Mail é obrigatório");
                return false;
            }
            else if (!Util.ValidaEmail(txtEmail.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nE-Mail está inválido");
                return false;
            }
            return true;
        }

        public void enviarEmailPdf(Stream Attach)
        {
            string emailCorpo = "Em resposta a sua solicitação, anexamos o Protocolo de exclusão do Plano de Saúde." + "<br/><br/>" +
                    "Caso tenha alguma dúvida poderá nos contatar por email: atendimento@funcesp.com.br, Disque Funcesp 11. 3065 3000 ou 0800 012 7173." + "<br/><br/><br/>" +
                    "Atenciosamente, </p>";

            // DE
            string emailRemetente = "Atendimento Funcesp <atendimento@funcesp.com.br>";
            // Para
            string emailRecebidor = email;

            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            string str_periodo_dia = (horarioAtual < periodo_dia) ? "Bom Dia." : "Boa Tarde.";

            emailCorpo = "<p style='font-family:Arial, Helvetica, sans-serif; font-size:12px'>Sr(a). " +
                         ResponsavelPlano + " " +
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
                mm.Attachments.Add(new Attachment(Attach, "CancelamentoPlano.pdf"));
                mm.IsBodyHtml = true;
                
                SmtpClient mailClient = new Email().EnviaEmailMensagem(mm);                
                montarAnexoViaEmailManifestacao(mailClient, mm);
                MostraMensagemTelaUpdatePanel(UpdatePanel, "E-Mail enviado com sucesso");
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO E-mail NÃO foi enviado.\\nMotivo:\\n" + ex.Message);
            }
        }

        void montarAnexoViaEmailManifestacao(SmtpClient mailClient, MailMessage mm)
        {

            //Essa outra etapa serve para grava o arquivo na manifestação
            //salvar e-mail em disco
            String pastaTemporariaEmail = criar_pasta_temporaria("Email");
            mailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            mailClient.PickupDirectoryLocation = pastaTemporariaEmail;
            mailClient.Send(mm);

            EmailCancelamentoPlanoBLL.Classe_Manifestacao filtro = new EmailCancelamentoPlanoBLL.Classe_Manifestacao(); //ler_variaveis_ambiente();
            filtro.CodigoEmpresa = empresa;
            filtro.Registro = matricula;
            filtro.Representante = nrepr;
            filtro.ParticipanteNome = ResponsavelPlano;
            filtro.ParticipanteEmail = email;
            filtro.NrChamado = nQry_NumChamado;
            filtro.NrManifestacao = nQry_NumManifestacao;

            // Ler o e-mail salvo no passo anterior e renomear 
            string[] filePaths = Directory.GetFiles(pastaTemporariaEmail);
            String novo_nome_de_arquivo = pastaTemporariaEmail + "\\email_CancelamentoPlano_" + "(Enviado em " + DateTime.Now.ToString("ddMMyyyy_hhm") + ").eml";
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

        //public void btnImprimirSelecao2_Click(object sender, EventArgs e)
        //{
        //    List<Cancelamento> listaCancelamento = new List<Cancelamento>();

        //    foreach (GridViewRow row in grdCancelPlano.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            string[] planos = new string[3];

        //            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

        //            if (chkSelect.Checked == true)
        //            {
        //                Label lblNomeParticipanteCod = (Label)row.FindControl("lblNomeParticipante");
        //                Label lblPlanoCod = (Label)row.FindControl("lblNomePlano");

        //                Cancelamento item = new Cancelamento();
        //                item.nomeBeneficiario = lblNomeParticipanteCod.Text.ToUpper().Trim();
        //                item.nomePlano = lblPlanoCod.Text.ToUpper().Trim();
        //                item.empresa = hfEmpresa.Value;
        //                item.matricula = hfMatricula.Value;
        //                item.responsavel = ResponsavelPlano;
        //                item.protocolo = hfProtocolo.Value;

        //                listaCancelamento.Add(item);
        //            }
        //        }
        //    }

        //    Session.Add("sessionCancelamento", listaCancelamento);
        //    if (validarQtdList(listaCancelamento.Count))
        //    {
        //        redirect();
        //    }

        //}

        //bool validarQtdList(int lista)
        //{

        //    if (lista <= 0)
        //    {
        //        MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nSelecione ao menos um registro!");
        //        return false;
        //    }


        //    return true;
        //}

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdCancelPlano.HeaderRow.FindControl("chkSelectAll");

            foreach (GridViewRow row in grdCancelPlano.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSelect");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }
    }
}