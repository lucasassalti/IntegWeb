using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.DAL;

namespace IntegWeb.Portal.Web
{
    public partial class ExtUtilizacaoComponente : BasePage
    {
        Relatorio relatorio = new Relatorio();
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string relatorio_nome = "Detalhe da Utilização dos Serviços";
        string relatorio_titulo = "Detalhe da Utilização dos Serviços";
        string relatorio_simples = @"~/Relatorios/ExtratoUtilizacaoComponente.rpt";
        string nome_anexo = "Detalhe da Utilização dos Serviços_";
        ExtratoComponenteDAL.ExtratoComponente epDados;
        int? NUM_IDNTF_RPTANT = null;
        bool integracao_crm = false;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //string exibe_barra_superior = Request.QueryString["0"];
            if ((!String.IsNullOrEmpty(Request.QueryString["nempr"])) &&
                (!String.IsNullOrEmpty(Request.QueryString["nreg"])) &&
                (!String.IsNullOrEmpty(Request.QueryString["cpart"])))
            {
                integracao_crm = true;
                MasterPageFile = "~/Popup.Master";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string detalhado = Request.QueryString["hidDetalhado"] ?? "false";
            string visualizar = Request.QueryString["hidVisualizar"] ?? "false";
            string COD_EMPRS = Request.QueryString["nempr"];
            string NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            string PARTICIPANTEEMAIL = Request.QueryString["ParticipanteEmail"];
            NUM_IDNTF_RPTANT = Util.String2Int32(Request.QueryString["nrepr"]);

            ScriptManager.RegisterStartupScript(UpdatePanel,
                   UpdatePanel.GetType(),
                   "script",
                   "_client_side_script()",
                    true);

            Page.Form.DefaultButton = btnPesquisar.UniqueID;

            if (!IsPostBack)
            {
                CarregaDropDownList();
                if (!String.IsNullOrEmpty(COD_EMPRS) && !String.IsNullOrEmpty(NUM_RGTRO_EMPRG))
                {

                    //ReportCrystal.Visible = false;
                    txtCodEmpresa.Text = COD_EMPRS;
                    txtCodMatricula.Text = NUM_RGTRO_EMPRG;
                    if (!String.IsNullOrEmpty(PARTICIPANTEEMAIL) && PARTICIPANTEEMAIL != "undefined")
                    {
                        txtEMail.Text = PARTICIPANTEEMAIL;
                    }
                    CarregarDropDown();
                    ExtratoComponenteBLL CredReeBLL = new ExtratoComponenteBLL();
                    int iRepresentante = 0;
                    int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
                    grdExtratoUtilizacao.DataSource = CredReeBLL.Listar(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), iRepresentante, "00", 1, 2016);
                    grdExtratoUtilizacao.DataBind();
                    grdExtratoUtilizacao.Visible = true;
                    txtEMail.Enabled = true;
                    btnEmail.Enabled = true;
                    //}
                }
            }
        }




        private void CarregarDropDown()
        {
            int CodEmpresa, CodMatricula, CodRepresentante;
            if (int.TryParse(txtCodEmpresa.Text, out CodEmpresa) && int.TryParse(txtCodMatricula.Text, out CodMatricula))
            {

                ExtratoComponenteBLL CredReeBLL = new ExtratoComponenteBLL();

                if (ddlRepresentante.Items.Count == 0)
                {
                    List<UsuarioPortal> lstRepresentantes = CredReeBLL.ConsultarRepresentantes(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), NUM_IDNTF_RPTANT ?? 0);

                    if (lstRepresentantes.Count == 0)
                    {
                        lstRepresentantes = CredReeBLL.ConsultarRepresentantes(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), null);
                    }

                    if (lstRepresentantes.Count > 0)
                    {
                        CarregaDropDowList(ddlRepresentante, lstRepresentantes.ToList<object>(), "NomeCompleto", "NUM_IDNTF_RPTANT");
                        ddlRepresentante.Items.RemoveAt(0);
                        if (lstRepresentantes.Count < 2)
                        {
                            ddlRepresentante.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        DataTable dt = CredReeBLL.ListarUsuarios(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), 0);
                        if (dt.Rows.Count > 0)
                        {
                            CarregaDropDowDT(dt, ddlRepresentante);
                            ddlRepresentante.Items.RemoveAt(0);
                            for (int i = 0; i < ddlRepresentante.Items.Count; i++)
                            {
                                ddlRepresentante.Items[i].Value = "0";
                            }
                            ddlRepresentante.SelectedIndex = 0;
                        }
                    }
                }
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

        public class GetSemestre
        {
            public string semestre { get; set; }
            public string valor { get; set; }
        }

        public void CarregaDropDownList()
        {
            var lista = new List<GetSemestre>();
            var mes = DateTime.Today.Month;
            if (mes <= 6)
            {
                lista.Add(new GetSemestre { valor = "1", semestre = "1° semestre" });
            }
            else
            {
                lista.Add(new GetSemestre { valor = "2", semestre = "2° semestre" });
            }
            ddlSemestre.DataSource = lista;
            ddlSemestre.DataValueField = "valor";
            ddlSemestre.DataTextField = "semestre";

            ddlSemestre.DataBind();
        }


        protected void txtCodMatricula_TextChanged(object sender, EventArgs e)
        {
            ddlRepresentante.Items.Clear();
            CarregarDropDown();
            grdExtratoUtilizacao.Visible = false;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                ExtratoComponenteBLL CredReeBLL = new ExtratoComponenteBLL();
                int iRepresentante = 0;
                int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
                grdExtratoUtilizacao.DataSource = CredReeBLL.Listar(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), iRepresentante, "00", 1, 2016);
                grdExtratoUtilizacao.DataBind();
                grdExtratoUtilizacao.Visible = true;
                txtEMail.Enabled = true;
                btnEmail.Enabled = true;
            }
        }

        protected void grdExtratoUtilizacao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] Args = e.CommandArgument.ToString().Split(',');

            if (!InicializaRelatorio(Args[0], Args[1], Args[2], Args[3], Args[4], Args[5]))
            {
                return;
            }

            switch (e.CommandName)
            {
                case "Visualizar":
                    //ReportCrystal.VisualizaRelatorio();
                    //ReportCrystal.Visible = true;
                    ArquivoDownload adExtratoPdf = new ArquivoDownload();
                    adExtratoPdf.nome_arquivo = nome_anexo + Args[5].Replace("/", "_") + ".pdf";
                    adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + Args[0] + "_" + Args[1] + "_" + adExtratoPdf.nome_arquivo;
                    adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adExtratoPdf.nome_arquivo);
                    break;
                case "Email":

                    if (InicializaDadosEMail(Args[0], Args[1], Args[2], Args[3], Args[4], Args[6]))
                    {
                        EnviaEmailExtratoUtil(txtEMail.Text);
                    }

                    break;
            }
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            bool selecionado = false;
            foreach (GridViewRow row in grdExtratoUtilizacao.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelect = (row.FindControl("chkSelect") as CheckBox);
                    if (chkSelect.Checked)
                    {
                        selecionado = true;
                        Button btDetalhes = (row.FindControl("btDetalhes") as Button);
                        string[] Args = btDetalhes.CommandArgument.ToString().Split(',');

                        if (InicializaRelatorio(Args[0], Args[1], Args[2], Args[3], Args[4], Args[5]))
                        {
                            InicializaDadosEMail(Args[0], Args[1], Args[2], Args[3], Args[4], Args[5]);
                        }
                    }
                }
            }
            if (selecionado)
            {
                if (lstAdPdf.Count > 0)
                {
                    EnviaEmailExtratoUtil(txtEMail.Text);
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nSelecione ao menos um registro para envio");
                return;
            }
        }

        private bool InicializaRelatorio(string CodEmpresa, string CodMatricula, string NumIdntfRptant, string NumSubMatric, string Semestre, string NumAno)
        {

            relatorio.titulo = relatorio_titulo;

            relatorio.parametros = new List<Parametro>();
            relatorio.parametros.Add(new Parametro() { parametro = "ANCODEMPRS", valor = CodEmpresa });
            relatorio.parametros.Add(new Parametro() { parametro = "ANNUMRGTROEMPRG", valor = CodMatricula });
            relatorio.parametros.Add(new Parametro() { parametro = "ANNUMIDNTFRPTANT", valor = NumIdntfRptant });
            relatorio.parametros.Add(new Parametro() { parametro = "ASSUBMATRIC", valor = NumSubMatric });
            relatorio.parametros.Add(new Parametro() { parametro = "ADSEMESTRE", valor = Semestre });
            relatorio.parametros.Add(new Parametro() { parametro = "ADNUMANO", valor = NumAno });
            relatorio.parametros.Add(new Parametro() { parametro = "ASQUADRO", valor = "3" });

            relatorio.arquivo = relatorio_simples;

            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;

            return true;

        }

        private bool InicializaDadosEMail(string CodEmpresa, string CodMatricula, string NumIdntfRptant, string NumSubMatric, string Semestre, string DatEmissao, string DatIni = " ")
        {

            txtEMail.Text = txtEMail.Text.Trim();

            if (String.IsNullOrEmpty(txtEMail.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nE-Mail obrigatório");
                return false;
            }
            else if (!Util.ValidaEmail(txtEMail.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nE-Mail inválido");
                return false;
            }

            ExtratoComponenteBLL ExtUtilBLL = new ExtratoComponenteBLL();
            int iRepresentante = 0;
            int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
            epDados = ExtUtilBLL.Consultar(int.Parse(CodEmpresa), int.Parse(CodMatricula), iRepresentante, 1, 2016);
            epDados.usuario = ddlRepresentante.SelectedItem.Text;

            if (String.IsNullOrEmpty(epDados.empresa) && String.IsNullOrEmpty(epDados.registro))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nDados não localizados para a matrícula " + CodMatricula);
                return false;
            }

            ArquivoDownload newAd = new ArquivoDownload();
            newAd.nome_arquivo = nome_anexo + DatEmissao.Replace("/", "_") + ".pdf";
            newAd.dados = ReportCrystal.ExportarRelatorioPdf();
            lstAdPdf.Add(newAd);

            return true;

        }

        private bool ValidarCampos(bool BuscarDados = true)
        {
            int CodEmpresa, CodMatricula;
            int Semestre, NumAno;

            if (String.IsNullOrEmpty(txtCodEmpresa.Text) || String.IsNullOrEmpty(txtCodMatricula.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOs campos Empresa e Matrícula são obrigatórios");
                ddlRepresentante.Items.Clear();
                return false;
            }
            else if (!int.TryParse(txtCodEmpresa.Text, out CodEmpresa) || !int.TryParse(txtCodMatricula.Text, out CodMatricula))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Empresa ou Matrícula inválido");
                ddlRepresentante.Items.Clear();
                return false;
            }

            if (String.IsNullOrEmpty(ddlSemestre.SelectedValue))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO Semestre é obrigatório");
                return false;
            }
            else if (!int.TryParse(ddlSemestre.SelectedValue, out Semestre))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nSemestre inválido");
                return false;
            }

            if (String.IsNullOrEmpty(ddlNumAno.SelectedValue))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO Ano é obrigatório");
                return false;
            }
            else if (!int.TryParse(ddlNumAno.SelectedValue, out NumAno))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nAno inválido");
                return false;
            }

            if (!BuscarDados) return true;

            ExtratoComponenteBLL ExtUtilBLL = new ExtratoComponenteBLL();
            int iRepresentante = 0;
            int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
            epDados = ExtUtilBLL.Consultar(CodEmpresa, CodMatricula, iRepresentante, 1, 2016);

            if (String.IsNullOrEmpty(epDados.empresa) && String.IsNullOrEmpty(epDados.registro))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nExtrato de Utilização não localizado para a matrícula " + txtCodMatricula.Text);
                ReportCrystal.Visible = false;
                grdExtratoUtilizacao.Visible = false;
                return false;
            }

            return true;
        }

        private void EnviaEmailExtratoUtil(string emailPara)
        {
            string emailAssunto = "Extrato de Utilização";
            string emailCorpo = "Em resposta a sua solicitação, anexamos a 2ª via do seu extrato de utilização." + "<br/><br/>" +
                "Para mais informações, orientamos a acessar na área restrita do portal (www.funcesp.com.br): Saúde / Extrato de Utilização." + "<br/><br/>" +
                "Faça seu login com CPF e senha pessoal. Caso tenha esquecido a sua senha e já tenha e-mail cadastrado na Funcesp, clique no botão ‘Recuperar Senha’. Se não tiver e-mail cadastrado, entre em contato com o Disque-Fundação para obter a senha: 11. 3065 3000 ou 0800 012 7173. <br/><br/></p>";
            //string emailNomeAnexo = adPdf.nome_arquivo;
            EnviaEmail(emailPara, emailAssunto, emailCorpo);
        }

        private void EnviaEmail(string emailPara, string emailAssunto, string emailCorpo)
        {

            // DE
            string emailRemetente = "Atendimento Funcesp <atendimento@funcesp.com.br>";

            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            string str_periodo_dia = (horarioAtual < periodo_dia) ? "Bom Dia." : "Boa Tarde.";

            emailCorpo = "<p style='font-family:Arial, Helvetica, sans-serif; font-size:12px'>Sr(a). " +
                         str_periodo_dia + "<br/><br/>" +
                         emailCorpo;

            using (var message = new MailMessage(emailRemetente, Util.PreparaEmail(emailPara), emailAssunto, emailCorpo))
            {
                try
                {
                    //ANEXOS
                    foreach (ArquivoDownload ad in lstAdPdf)
                    {
                        message.Attachments.Add(new Attachment((Stream)ad.dados, ad.nome_arquivo));
                    }

                    var contentID = "Image";
                    var inlineLogo = new Attachment(Server.MapPath("img/assinatura_email.jpg"));

                    //---------------------------------------------------------------------
                    // Ambiente de Produção
                    //var inlineLogo = new Attachment(@"D:\\avisopagto\img\assinatura_email.jpg");

                    //----------------------------------------------------------------------
                    //---------------------------------------------------------------------

                    inlineLogo.ContentId = contentID;
                    inlineLogo.ContentDisposition.Inline = true;
                    inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                    message.Attachments.Add(inlineLogo);

                    emailCorpo = emailCorpo + "<img src=\"cid:" + contentID + "\">";

                    //---------------------------------------------------------------------
                    //---------------------------------------------------------------------

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
