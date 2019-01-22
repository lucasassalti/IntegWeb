using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mime;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Entidades.Previdencia.Concessao;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Web
{
    public partial class ExtratoPrev : BasePage 
    {
        Relatorio relatorio = new Relatorio();
        ArquivoDownload adPdf = new ArquivoDownload();
        string relatorio_nome = "ExtratoPrevidenciario";
        string relatorio_titulo = "Extrato Previdenciário";
        string relatorio_simples = @"~/Relatorios/Concessao/Rel_Extrato_Prev.rpt";
        string relatorio_detalhado = @"~/Relatorios/Concessao/Rel_Extrato_Prev_Detalhado.rpt";
        string relatorio_dados_previden = @"~/Relatorios/Concessao/Rel_Dados_Previdenciarios.rpt";
        ExtratoPrevidenciario epDados;
        int? NUM_IDNTF_RPTANT = null;
        bool integracao_crm = false;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["0"] != null)
            {
                string exibe_barra_superior = Request.QueryString["0"];
                if (exibe_barra_superior.ToLower() == "false")
                {
                    integracao_crm = true;
                    MasterPageFile = "~/Popup.Master";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string detalhado = Request.QueryString["hidDetalhado"] ?? "false";
            string visualizar = Request.QueryString["hidVisualizar"] ?? "true";
            string COD_EMPRS = Request.QueryString["nempr"];
            string NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            string PARTICIPANTEEMAIL = Request.QueryString["ParticipanteEmail"];
            NUM_IDNTF_RPTANT = Util.String2Int32(Request.QueryString["nrepr"]);

            ScriptManager.RegisterStartupScript(UpdatePanel,
                   UpdatePanel.GetType(),
                   "script",
                   "javascriptFunctionName()",
                    true);

            if (!IsPostBack)
            {

                for (int Ano = 2002; Ano <= DateTime.Now.Year; Ano++)
                {
                    ddlAnoDe.Items.Add(new ListItem(Ano.ToString(), Ano.ToString()));
                    ddlAnoAte.Items.Add(new ListItem(Ano.ToString(), Ano.ToString()));
                }

                if (!String.IsNullOrEmpty(COD_EMPRS) && !String.IsNullOrEmpty(NUM_RGTRO_EMPRG))
                {

                    txtCodEmpresa.Text = COD_EMPRS;
                    txtCodMatricula.Text = NUM_RGTRO_EMPRG;
                    if (!String.IsNullOrEmpty(PARTICIPANTEEMAIL) && PARTICIPANTEEMAIL != "undefined")
                    {
                        txtEMail.Text = PARTICIPANTEEMAIL;
                    }

                    CarregarDropDown();

                    if (bool.Parse(visualizar))
                    {
                        if (InicializaRelatorio())
                        {
                            ReportCrystal.VisualizaRelatorio();
                            ReportCrystal.Visible = true;
                        }
                    }

                }
            }
            else
            {

                RegraTela();

            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (ReportCrystal != null)
            {
                ReportCrystal.RelatorioID = null;
                ReportCrystal = null;
            }
        }

        protected void txtCodMatricula_TextChanged(object sender, EventArgs e)
        {
            if (optTipo.SelectedValue == "2")
            {
                ddlPeriodo.Items.Clear();
                CarregarDropDown();
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                switch (optTipo.SelectedValue)
                {
                    case "1":
                    case "2":
                    case "3":
                        if (InicializaRelatorio())
                        {
                            ReportCrystal.VisualizaRelatorio();
                            ifExtratoPrevSysDocs.Visible = false;
                            ReportCrystal.Visible = true;
                        };
                        break;
                    default:
                        CarregarExtratoPrevSysDocs();
                        ifExtratoPrevSysDocs.Visible = true;
                        break;
                }
            }

        }

        private void CarregarExtratoPrevSysDocs()
        {
            if (!ValidarCampos()) return;

            extratoPrevidenciarioBLL CredReeBLL = new extratoPrevidenciarioBLL();
            List<UsuarioPortal> usPortal = CredReeBLL.ConsultarRepresentantes(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), NUM_IDNTF_RPTANT);

            string fullUrl = "";

            if (usPortal.Count > 0)
            {

                UsuarioPortal userPortal = usPortal[0];

                string cp01 = userPortal.COD_EMPRS.ToString().PadLeft(3, '0'); //empresaFormat.format(dcVo.getEmpresa());
                string cp02 = userPortal.NUM_RGTRO_EMPRG.ToString().PadLeft(10, '0'); //matriculaFormat.format(dcVo.getMatricula());
                string cp03 = userPortal.NUM_DIGVR_EMPRG.ToString();
                string cp04 = ddlAnoDe.SelectedValue; //anoInicio;
                string cp05 = ddlAnoAte.SelectedValue; //anoFim;
                string cp06 = ddlTrimestreDe.SelectedValue; //mesInicio;
                string cp07 = ddlTrimestreAte.SelectedValue; //mesFim;
                string cp08 = userPortal.NomeCompleto.ToString().Replace(' ', '_'); //"0"; //dcVo.getNome().replace(' ', '_');
                string cp09 = userPortal.CPF.ToString(); //0; //dcVo.getCpf();

                string strUrl = String.Format("https://docs.prevcesp.com.br/ged/idocs_portal_ticket.php?BdTypeName={0}&CP01={1}&CP02={2}&CP03={3}&CP04={4}&CP05={5}&CP06={6}&CP07={7}&CP08={8}&CP09={9}",
                                                      2, //tp_contrib.php
                                                      cp01, cp02, cp03,
                                                      cp04, cp05, cp06,
                                                      cp07, cp08, cp09);

                try
                {
                    WebRequest request = WebRequest.Create(strUrl);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);

                    if (responseFromServer.Split('|').Length > 1)
                    {
                        string token = responseFromServer.Split('|')[1];
                        fullUrl = "https://docs.prevcesp.com.br/ged/idocs_portal_procrel.php?ticket=" + token;
                        ifExtratoPrevSysDocs.Src = fullUrl;
                        //AdicionarAcesso(fullUrl);
                        //AbrirNovaAba(UpdatePanel, fullUrl, "Sysdocs");
                    }
                    reader.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nErro ao consultar o Sysdocs.\\nMotivo:\\n" + ex.Message);
                    return;
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nSysDoc não disponível para esta Empresa e Matrícula");
            }
        }

        protected void btnGerarPdf_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorio())
            {
                ReportCrystal.ExportarRelatorioPdf(adPdf.caminho_arquivo);
                Session[ValidaCaracteres(adPdf.nome_arquivo)] = adPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(UpdatePanel, fullUrl, adPdf.nome_arquivo);

                if (ReportCrystal.Visible) ReportCrystal.VisualizaRelatorio();
            }
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorio())
            {

                txtEMail.Text = txtEMail.Text.Trim();

                if (String.IsNullOrEmpty(txtEMail.Text))
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nE-Mail obrigatório");
                    return;
                }
                else if (!Util.ValidaEmail(txtEMail.Text))
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nE-Mail inválido");
                    return;
                }

                switch (optTipo.SelectedValue)
                {
                    case "1":
                    case "2":
                        EnviaEmailExtrato(txtEMail.Text);
                        break;
                    case "3":
                        EnviaEmailFicha(txtEMail.Text);
                        break;
                    case "4":
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOpção inválida");
                        ReportCrystal.Visible = false;
                        break;
                }

                if (ReportCrystal.Visible) ReportCrystal.VisualizaRelatorio();
            }
        }

        private void RegraTela()
        {

            periodo1.Style.Add("display", "none");
            periodo2.Style.Add("display", "none");
            btnGerarPdf.Enabled = true;
            btnEmail.Enabled = true;

            switch (optTipo.SelectedValue)
            {
                case "2":
                    ddlPeriodo.Enabled = true;
                    periodo1.Style.Remove("display");
                    periodo2.Style.Add("display", "none");
                    break;
                    //case "1":
                    //case "3":
                    //    //ifExtratoPrevSysDocs.Visible = false;
                    //    //ReportCrystal.Visible = true;
                    //    periodo1.Style.Add("display", "none");
                    //    periodo2.Style.Add("display", "none");
                    break;
                case "4":
                    periodo1.Style.Add("display", "none");
                    periodo2.Style.Remove("display");
                    btnGerarPdf.Enabled = false;
                    btnEmail.Enabled = false;
                    break;
            }

        }

        private void CarregarDropDown()
        {
            int CodEmpresa, CodMatricula;
            if (int.TryParse(txtCodEmpresa.Text, out CodEmpresa) && int.TryParse(txtCodMatricula.Text, out CodMatricula))
            {
                extratoPrevidenciarioBLL extPrevBLL = new extratoPrevidenciarioBLL();
                CarregaDropDowDT(extPrevBLL.ListaPeriodos(CodEmpresa, CodMatricula), ddlPeriodo);
            }
        }

        private bool ValidarCampos()
        {
            int CodEmpresa, CodMatricula;
            bool detalhado = (optTipo.SelectedValue == "2"); //(TabContainer.ActiveTabIndex > 0);
            bool periodo_anterior = (optTipo.SelectedValue == "4"); //(TabContainer.ActiveTabIndex > 0);

            ReportCrystal.Visible = false;
            ifExtratoPrevSysDocs.Visible = false;

            if (String.IsNullOrEmpty(txtCodEmpresa.Text) || String.IsNullOrEmpty(txtCodMatricula.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOs campos Nº Empresa e Nº Matrícula são obrigatórios");
                ddlPeriodo.Items.Clear();
                return false;
            }
            else if (!int.TryParse(txtCodEmpresa.Text, out CodEmpresa) || !int.TryParse(txtCodMatricula.Text, out CodMatricula))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Empresa ou Matrícula inválido");
                ddlPeriodo.Items.Clear();
                return false;
            }

            extratoPrevidenciarioBLL extPrevBLL = new extratoPrevidenciarioBLL();
            epDados = extPrevBLL.Consultar(CodEmpresa, CodMatricula);

            if (ddlPeriodo.Items.Count == 0)
            {
                CarregarDropDown();
            }

            if (String.IsNullOrEmpty(epDados.empresa) && String.IsNullOrEmpty(epDados.registro))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nExtrato Previdenciário não localizado para a matrícula " + txtCodMatricula.Text);
                ddlPeriodo.Items.Clear();
                return false;
            }

            if ((string.IsNullOrEmpty(ddlPeriodo.SelectedValue) || ddlPeriodo.SelectedValue == "0") && detalhado)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO campo período é obrigatório para o extrato detalhado");
                return false;
            }

            if (ddlAnoDe.SelectedValue == "0" &&
                ddlAnoAte.SelectedValue == "0" &&
                ddlTrimestreDe.SelectedValue == "0" &&
                ddlTrimestreAte.SelectedValue == "0" && periodo_anterior)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOs campos período (de/até) são obrigatórios para a pesquisa");
                return false;
            }

            return true;
        }

        private bool InicializaRelatorio()
        {

            if (ValidarCampos())
            {

                relatorio.titulo = relatorio_titulo;
                relatorio.parametros = new List<Parametro>();

                switch (optTipo.SelectedValue)
                {
                    case "1":
                        relatorio.arquivo = relatorio_simples;
                        relatorio.parametros.Add(new Parametro() { parametro = "ANCODEMPRS", valor = txtCodEmpresa.Text });
                        relatorio.parametros.Add(new Parametro() { parametro = "ANNUMRGTROEMPRG", valor = txtCodMatricula.Text });
                        relatorio.parametros.Add(new Parametro() { parametro = "ASQUADRO", valor = "1" });
                        adPdf.nome_arquivo = "Extrato_Previdenciario_" + DateTime.Today.ToString("yyyy_MM_dd") + ".pdf";
                        adPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adPdf.nome_arquivo;
                        break;
                    case "2":
                        relatorio.arquivo = relatorio_detalhado;
                        relatorio.parametros.Add(new Parametro() { parametro = "ANCODEMPRS", valor = txtCodEmpresa.Text });
                        relatorio.parametros.Add(new Parametro() { parametro = "ANNUMRGTROEMPRG", valor = txtCodMatricula.Text });
                        relatorio.parametros.Add(new Parametro() { parametro = "ASQUADRO", valor = "1" });
                        relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(ddlPeriodo.SelectedValue).ToShortDateString() });
                        adPdf.nome_arquivo = "Extrato_Previdenciario_" + DateTime.Today.ToString("yyyy_MM_dd") + ".pdf";
                        adPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adPdf.nome_arquivo;
                        break;
                    case "3":
                        relatorio.arquivo = relatorio_dados_previden;
                        relatorio.parametros.Add(new Parametro() { parametro = "P_COD_EMPRS", valor = txtCodEmpresa.Text });
                        relatorio.parametros.Add(new Parametro() { parametro = "P_NUM_RGTRO_EMPRG", valor = txtCodMatricula.Text });
                        relatorio.parametros.Add(new Parametro() { parametro = "P_NUM_IDNTF_RPTANT", valor = (NUM_IDNTF_RPTANT != null) ? NUM_IDNTF_RPTANT.ToString() : "0" });
                        adPdf.nome_arquivo = "Ficha_Dados_Previdenciarios_" + DateTime.Today.ToString("yyyy_MM_dd") + ".pdf";
                        adPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adPdf.nome_arquivo;
                        break;
                    case "4":
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOpção inválida");
                        ReportCrystal.Visible = false;
                        return false;
                }


                Session[relatorio_nome] = relatorio;
                ReportCrystal.RelatorioID = relatorio_nome;

                return true;

            }
            else return false;


        }

        private void EnviaEmailExtrato(string emailPara)
        {
            string emailAssunto = "Extrato do seu Plano de Previdência - " + epDados.nome;
            string emailCorpo = "Em resposta a sua solicitação, anexamos a 2ª via do seu extrato previdenciário." + "<br/><br/>" +
                "Para obter o <strong>extrato previdenciário</strong>, orientamos a acessar na área restrita do portal (www.funcesp.com.br): Previdência / Extrato Previdenciário." + "<br/><br/>" +
                "Faça seu login com CPF e senha pessoal. Caso tenha esquecido a sua senha e já tenha e-mail cadastrado na Funcesp, clique no botão ‘Recuperar Senha’. Se não tiver e-mail cadastrado, entre em contato com o Disque-Fundação para obter a senha: 11. 3065 3000 ou 0800 012 7173. <br/><br/></p>";
            string emailNomeAnexo = adPdf.nome_arquivo;
            EnviaEmail(emailPara, emailAssunto, emailCorpo, emailNomeAnexo);
        }

        private void EnviaEmailFicha(string emailPara)
        {
            string emailAssunto = "Dados Previdenciários - " + epDados.nome;
            string emailCorpo = "Em resposta a sua solicitação, anexamos sua ficha de dados previdenciários." + "<br/><br/>" +
                "Para obter seus <strong>dados previdenciários</strong>, orientamos a acessar na área restrita do portal (www.funcesp.com.br): Previdência / Dados Previdenciários." + "<br/><br/>" +
                "Faça seu login com CPF e senha pessoal. Caso tenha esquecido a sua senha e já tenha e-mail cadastrado na Funcesp, clique no botão ‘Recuperar Senha’. Se não tiver e-mail cadastrado, entre em contato com o Disque-Fundação para obter a senha: 11. 3065 3000 ou 0800 012 7173. <br/><br/></p>";
            string emailNomeAnexo = adPdf.nome_arquivo;
            EnviaEmail(emailPara, emailAssunto, emailCorpo, emailNomeAnexo);
        }

        private void EnviaEmail(string emailPara, string emailAssunto, string emailCorpo, string emailNomeAnexo)
        {



            // DE
            string emailRemetente = "Atendimento Funcesp <atendimento@funcesp.com.br>";

            // ASSUNTO
            //string emailAssunto = "Aviso de Pagamento - " + ((ParameterDiscreteValue)(relatorios[0].ParameterFields["AVISO_NOM_EMPRG"].CurrentValues[0])).Value;

            //string emailAssunto = "Extrato do seu Plano de Previdência - " + epDados.nome;
            // MENSAGEM


            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            string str_periodo_dia = "";

            if (horarioAtual < periodo_dia)
                str_periodo_dia = "Bom Dia.";
            else
                str_periodo_dia = "Boa Tarde.";

            emailCorpo = "<p style='font-family:Arial, Helvetica, sans-serif; font-size:12px'>Sr(a). " +
                         str_periodo_dia + "<br/><br/>" +
                         emailCorpo;

            using (var message = new MailMessage(emailRemetente, Util.PreparaEmail(emailPara), emailAssunto, emailCorpo))
            {
                try
                {
                    //ANEXOS

                    //InicializaRelatorio();

                    System.IO.Stream pdfMemoria = ReportCrystal.ExportarRelatorioPdf();

                    //Exportar para Stream
                    //System.IO.Stream pdfMemoria = relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                    message.Attachments.Add(new Attachment(pdfMemoria, emailNomeAnexo +
                                                DateTime.Today.ToString("yyyy_MM_dd") + ".pdf"));

                    var contentID = "Image";
                    var inlineLogo = new Attachment(Server.MapPath("img/assinatura_email.jpg"));

                    //---------------------------------------------------------------------
                    // Ambiente de Produção
                    //var inlineLogo = new Attachment(@"D:\\avisopagto\img\assinatura_email.jpg");

                    inlineLogo.ContentId = contentID;
                    inlineLogo.ContentDisposition.Inline = true;
                    inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                    message.Attachments.Add(inlineLogo);

                    emailCorpo = emailCorpo + "<img src=\"cid:" + contentID + "\">";

                    message.IsBodyHtml = true;
                    message.Body = emailCorpo;

                    // ENVIAR COM CÓPIA
                    // MailAddress copy = new MailAddress("");
                    // message.CC.Add(copy);

                    // ENVIAR COM COPIA OCULTA
                    // MailAddress bcc = new MailAddress("");
                    // message.Bcc.Add(bcc);

                    new Email().EnviaEmailMensagem(message);
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "E-Mail enviado com sucesso");
                }
                catch (Exception ex)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO E-mail NÃO foi enviado.\\nMotivo:\\n" + ex.Message);
                }
            }
        }

    }
}
