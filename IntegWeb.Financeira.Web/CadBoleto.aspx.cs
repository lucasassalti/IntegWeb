using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Financeira.Aplicacao.BLL;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web
{
    public partial class CadBoleto : BasePage
    {
        Relatorio relatorio = new Relatorio();
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string relatorio_nome = "Boleto";
        string relatorio_titulo = "Boleto";
        string relatorio_simples = @"~/Relatorios/Rel_Boleto.rpt";
        string nome_anexo = "Boleto_";

        protected void Page_Load(object sender, EventArgs e)
        {

            string COD_EMPRS = Request.QueryString["nEmpr"];
            string NUM_RGTRO_EMPRG = Request.QueryString["nReg"];
            string NUM_DIVR_EMPRG = Request.QueryString["nDigReg"];
            string NUM_IDNTF_RPTANT = Request.QueryString["nrepr"];
            //string NUM_DEPEND = Request.QueryString["ndep"];

            string NOM_EMPR = Request.QueryString["cPart"];

            string End1 = Request.QueryString["cEnd1"];
            string End2 = Request.QueryString["cEnd2"];
            string COD_TIPO_BOLETO = Request.QueryString["nIdBol"];

            string PARTICIPANTEEMAIL = Request.QueryString["ParticipanteEmail"];            

            ScriptManager.RegisterStartupScript(UpdatePanel,
                   UpdatePanel.GetType(),
                   "script",
                   "_client_side_script()",
                    true);

            Page.Form.DefaultButton = btnPesquisar.UniqueID;
            lblMensagem.Visible = false;
            lblMensagemNovo.Visible = false;

            if (!IsPostBack)
            {
                grdBoleto.Sort("DT_PROCESSAMENTO", SortDirection.Ascending);

                CarregarDropDown();

                if (!String.IsNullOrEmpty(COD_EMPRS) && !String.IsNullOrEmpty(NUM_RGTRO_EMPRG))
                {
                    ParticipanteBLL bll_p = new ParticipanteBLL();
                    PARTICIPANTE part = bll_p.GetParticipanteBy(Util.String2Short(COD_EMPRS) ?? 0, Util.String2Int32(NUM_RGTRO_EMPRG) ?? 0, Util.String2Int32(NUM_IDNTF_RPTANT) ?? 0, true);

                    txtPesqCodEmpresa.Text = COD_EMPRS;
                    txtPesqMatricula.Text = NUM_RGTRO_EMPRG;
                    txtPesqNome.Text = NOM_EMPR;
                    hidPesqNUM_IDNTF_RPTANT.Value = NUM_IDNTF_RPTANT;
                    ddlPesqTipoBoleto.SelectedValue = COD_TIPO_BOLETO;
                    if (part != null)
                    {
                        txtPesqDigito.Text = part.NUM_DIGVR_EMPRG.ToString();
                        txtPesqCpf.Text = part.NUM_CPF_EMPRG.ToString();
                        txtEMail.Text = part.COD_EMAIL_EMPRG;
                        lblPesqEnd1.Text = part.DCR_ENDER_EMPRG + ", " + part.NUM_ENDER_EMPRG;
                        lblPesqEnd2.Text = part.COD_CEP_EMPRG + "  " + part.NOM_BAIRRO_EMPRG + "  " + part.NOM_CIDRS_EMPRG + "-" + part.COD_UNDFD_EMPRG;
                    }

                    if (!String.IsNullOrEmpty(PARTICIPANTEEMAIL) && PARTICIPANTEEMAIL != "undefined")
                    {
                        txtEMail.Text = PARTICIPANTEEMAIL;
                    }

                    if (!String.IsNullOrEmpty(NUM_DIVR_EMPRG) && End2 != "undefined")
                    {
                        txtPesqDigito.Text = NUM_DIVR_EMPRG;
                    }

                    if (!String.IsNullOrEmpty(End1) && End1 != "undefined")
                    {
                        lblPesqEnd1.Text = End1;
                    }

                    if (!String.IsNullOrEmpty(End2) && End2 != "undefined")
                    {
                        lblPesqEnd2.Text = End2;
                    }

                    BoletoBLL bll = new BoletoBLL();
                    AAT_TBL_BOLETO_TIPO BOLETO_TIPO = bll.GetBoletoTipo(Util.String2Short(COD_TIPO_BOLETO) ?? 0);

                    if (BOLETO_TIPO != null)
                    {
                        txtInstrucoes.Text = BOLETO_TIPO.DCR_OBSERVACAO;
                    }

                    //ReportCrystal.Visible = false;
                    //txtCodEmpresa.Text = COD_EMPRS;
                    //txtMatricula.Text = NUM_RGTRO_EMPRG;
                    //txtDigito.Text = NUM_DIVR_EMPRG;
                    //txtCpf.Text = part.NUM_CPF_EMPRG.ToString();
                    //txtNome.Text = NOM_EMPR;
                    //ddlTipoBoleto.SelectedValue = COD_TIPO_BOLETO;
                    //lblEnd1.Text = End1;
                    //lblEnd2.Text = End2;

                    
                    //if (ValidarCampos())
                    //{
                    //txtEMail.Enabled = true;
                    //btnEmail.Enabled = true;
                    //}
                }
            }

        }

        private void CarregarDropDown()
        {
            BoletoBLL bll = new BoletoBLL();
            CarregaDropDowList(ddlPesqTipoBoleto, bll.GetBoletoTipos().ToList<object>(), "NOM_BOLETO", "COD_BOLETO_TIPO");
            ddlPesqTipoBoleto.Items[0].Text = "<TODOS>";

            CloneDropDownList(ddlPesqTipoBoleto, ddlTipoBoleto);
            ddlTipoBoleto.Items[0].Text = "";
            ddlTipoBoleto.Items[0].Value = null;
            ddlTipoBoleto.SelectedValue = null;

            CarregaDropDowList(ddlPesqSubTipoBoleto, bll.GetBoletoSubTipos().ToList<object>(), "DCR_BOLETO_SUBTIPO", "COD_BOLETO_SUBTIPO");
            ddlPesqSubTipoBoleto.Items[0].Text = "<TODOS>";

            CloneDropDownList(ddlPesqSubTipoBoleto, ddlSubTipoBoleto);
            ddlSubTipoBoleto.Items[0].Text = "";
            ddlSubTipoBoleto.Items[0].Value = null;
            ddlSubTipoBoleto.SelectedValue = null;
        }

        protected void ddlPesqTipoBoleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            BoletoBLL bll = new BoletoBLL();
            CarregaDropDowList(ddlPesqSubTipoBoleto, bll.GetBoletoSubTipos(Util.String2Short(ddlPesqTipoBoleto.SelectedValue)).ToList<object>(), "DCR_BOLETO_SUBTIPO", "COD_BOLETO_SUBTIPO");
            ddlPesqSubTipoBoleto.Items[0].Text = "<TODOS>";
        }

        protected void ddlTipoBoleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            BoletoBLL bll = new BoletoBLL();
            CarregaDropDowList(ddlSubTipoBoleto, bll.GetBoletoSubTipos(Util.String2Short(ddlTipoBoleto.SelectedValue)).ToList<object>(), "DCR_BOLETO_SUBTIPO", "COD_BOLETO_SUBTIPO");
            ddlSubTipoBoleto.Items[0].Text = "";
            ddlSubTipoBoleto.Items[0].Value = null;
            //ddlSubTipoBoleto.Items[0].Text = "<TODOS>";
            //ddlSubTipoBoleto.SelectedValue = null;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposPesq())
            {
                grdBoleto.DataBind();
            };

            //if (String.IsNullOrEmpty(txtDtIni.Text) || String.IsNullOrEmpty(txtDtFim.Text))
            //{
            //    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOs campos do Período são obrigatórios");
            //}
            //else if (!DateTime.TryParse(txtDtIni.Text, out DtIni) || !DateTime.TryParse(txtDtFim.Text, out DtFim))
            //{
            //    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nPeríodo de emissão inválido");
            //}

            //txtDtIni.Text = Util.PrimeiroDiaMes(DtIni).ToString("dd/MM/yyyy");
            //txtDtFim.Text = Util.UltimoDiaMes(DtFim).ToString("dd/MM/yyyy");

            //if (String.IsNullOrEmpty(txtPesqEmpresa.Text) &&
            //    String.IsNullOrEmpty(txtPesqMatricula.Text) &&
            //    String.IsNullOrEmpty(txtPesqCpf.Text) &&
            //    String.IsNullOrEmpty(txtPesNome.Text) &&
            //    String.IsNullOrEmpty(txtDtVencIni.Text) &&
            //    String.IsNullOrEmpty(txtDtVencFim.Text))
            //{
            //    MostraMensagem(TbConsulta_Mensagem, "Prencha ao menos um campo de pesquisa para continuar");
            //}
            //else grdDebitoConta.PageIndex = 0;
        }

        protected void btLimpar_Click(object sender, EventArgs e)
        {
            //ddlPesqTipoBoleto.SelectedValue = "0";
            txtPesqCodEmpresa.Text = "";
            txtPesqMatricula.Text = "";            
            txtPesqDigito.Text = "";
            txtPesqCpf.Text = "";
            txtPesqLote.Text = "";
            txtPesqNome.Text = "";
            lblPesqEnd1.Text = "";
            lblPesqEnd2.Text = "";
            hidPesqNUM_IDNTF_RPTANT.Value = "";
            txtDtIni.Text = "";
            txtDtFim.Text = "";
            txtEMail.Text = "";
            grdBoleto.PageIndex = 0;
            grdBoleto.EditIndex = -1;
            grdBoleto.ShowFooter = false;
            grdBoleto.DataBind();
        }

        protected void grdBoleto_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName != "Sort" && e.CommandName != "Page")
            {
                string[] Args = e.CommandArgument.ToString().Split(',');
                int PK_BOLETO = Util.String2Int32(Args[0].ToString()) ?? 0;
                int NOSSO_NUMERO = Util.String2Int32(Args[1].ToString()) ?? 0;
                string TIPO_BOLETO = Args[2];
                switch (e.CommandName)
                {
                    case "Visualizar":

                        if (InicializaRelatorio(PK_BOLETO))
                        {
                            VisualizarBoleto(PK_BOLETO);
                        }
                        break;
                    case "Email":

                        if (InicializaDadosEMail(PK_BOLETO, NOSSO_NUMERO, TIPO_BOLETO))
                        {
                            EnviaEmailExtratoUtil(txtEMail.Text);
                        }

                        break;
                }
            }
        }

        private void VisualizarBoleto(int PK_BOLETO)
        {
            //ReportCrystal.VisualizaRelatorio();
            //ReportCrystal.Visible = true;
            ArquivoDownload adExtratoPdf = new ArquivoDownload();
            adExtratoPdf.nome_arquivo = nome_anexo + PK_BOLETO.ToString() + ".pdf";
            adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + PK_BOLETO.ToString() + "_" + adExtratoPdf.nome_arquivo;
            adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
            ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

            Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
            string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
            AdicionarAcesso(fullUrl);
            AbrirNovaAba(UpdatePanel, fullUrl, adExtratoPdf.nome_arquivo);
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            bool selecionado = false;
            foreach (GridViewRow row in grdBoleto.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelect = (row.FindControl("chkSelect") as CheckBox);
                    if (chkSelect.Checked)
                    {
                        selecionado = true;
                        Button btDetalhes = (row.FindControl("btVisualizar") as Button);
                        string[] Args = btDetalhes.CommandArgument.ToString().Split(',');
                        int PK_BOLETO = Util.String2Int32(Args[0].ToString()) ?? 0;
                        int? NOSSO_NUMERO = Util.String2Int32(Args[1].ToString());
                        string TIPO_BOLETO = Args[2];

                        if (InicializaRelatorio(PK_BOLETO))
                        {
                            InicializaDadosEMail(PK_BOLETO, NOSSO_NUMERO, TIPO_BOLETO);
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
                //MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nSelecione ao menos um registro para envio");
                MostraMensagem(lblMensagem, "Atenção! Selecione ao menos um boleto para envio");
                return;
            }
        }

        private bool InicializaRelatorio(int pCOD_BOLETO, int? pNUM_DCMCOB_BLPGT = 0)
        {

            relatorio.titulo = relatorio_titulo;

            relatorio.parametros = new List<Parametro>();
            relatorio.parametros.Add(new Parametro() { parametro = "pCOD_BOLETO", valor = pCOD_BOLETO.ToString() });
            relatorio.parametros.Add(new Parametro() { parametro = "pNUM_DCMCOB_BLPGT", valor = pNUM_DCMCOB_BLPGT.ToString() });

            relatorio.arquivo = relatorio_simples;

            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;

            return true;

        }

        private void EnviaEmailExtratoUtil(string emailPara)
        {
            string emailAssunto = "2ª via do boleto";
            string emailCorpo = "Em resposta a sua solicitação, estamos enviando em anexo a 2ª via do boleto." + "<br/><br/>";
            //"Para mais informações, orientamos a acessar na área restrita do portal (www.funcesp.com.br): Saúde / Extrato de Utilização." + "<br/><br/>" +
            //"Faça seu login com CPF e senha pessoal. Caso tenha esquecido a sua senha e já tenha e-mail cadastrado na Funcesp, clique no botão ‘Recuperar Senha’. Se não tiver e-mail cadastrado, entre em contato com o Disque-Fundação para obter a senha: 11. 3065 3000 ou 0800 012 7173. <br/><br/></p>";
            //string emailNomeAnexo = adPdf.nome_arquivo;
            EnviaEmail(emailPara, emailAssunto, emailCorpo);
        }


        private bool InicializaDadosEMail(int pCOD_BOLETO, int? pNUM_DCMCOB_BLPGT = 0, string TIPO_BOLETO = "")
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

            //ExtratoUtilizacaoBLL ExtUtilBLL = new ExtratoUtilizacaoBLL();
            //int iRepresentante = 0;
            //int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
            //epDados = ExtUtilBLL.Consultar(int.Parse(CodEmpresa), int.Parse(CodMatricula), iRepresentante, DateTime.Parse(DatIni), DateTime.Parse(DatFim));
            //epDados.usuario = ddlRepresentante.SelectedItem.Text;

            //if (String.IsNullOrEmpty(epDados.empresa) && String.IsNullOrEmpty(epDados.registro))
            //{
            //    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nDados não localizados para a matrícula " + CodMatricula);
            //    return false;
            //}

            ArquivoDownload newAd = new ArquivoDownload();
            newAd.nome_arquivo = nome_anexo + TIPO_BOLETO + "_" + pNUM_DCMCOB_BLPGT.ToString() + ".pdf";
            newAd.dados = ReportCrystal.ExportarRelatorioPdf();
            lstAdPdf.Add(newAd);

            return true;

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

                    emailCorpo = emailCorpo + "<img src=\"cid:" + contentID + "\" width=\"733\" height=\"513\" >";

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

        protected void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            short Emp;
            int Matr;
            if (short.TryParse(txtCodEmpresa.Text, out Emp) && 
                int.TryParse(txtMatricula.Text, out Matr) &&
                String.IsNullOrEmpty(txtCpf.Text) &&
                String.IsNullOrEmpty(txtNome.Text))
            {
                CarregaDadosPartic();
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {

            if (ValidarCamposPesq())
            {
                txtCodEmpresa.Text = txtPesqCodEmpresa.Text;
                txtMatricula.Text = txtPesqMatricula.Text;
                txtDigito.Text = txtPesqDigito.Text;                
                txtCpf.Text = txtPesqCpf.Text;
                txtNome.Text = txtPesqNome.Text;
                hidNUM_IDNTF_RPTANT.Value = hidPesqNUM_IDNTF_RPTANT.Value;
                ddlTipoBoleto.SelectedValue = null;
                ddlSubTipoBoleto.SelectedValue = null;
                if (ddlPesqTipoBoleto.SelectedValue != "0")
                {
                    ddlTipoBoleto.SelectedValue = ddlPesqTipoBoleto.SelectedValue;
                }
                if (ddlPesqSubTipoBoleto.SelectedValue != "0")
                {
                    ddlSubTipoBoleto.SelectedValue = ddlPesqSubTipoBoleto.SelectedValue;
                }
                lblEnd1.Text = lblPesqEnd1.Text;
                lblEnd2.Text = lblPesqEnd2.Text;

                txtValor.Text = "";
                txtDtVencimento.Text = "";

                pnlGrid.Visible = false;
                pnlNovoBoleto.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = true;
            pnlNovoBoleto.Visible = false;
        }

        private bool ValidarCamposPesq()
        {
            int CodEmpresa, CodMatricula;
            long Cpf;
            DateTime DtIni, DtFim;

            if (String.IsNullOrEmpty(txtPesqCodEmpresa.Text) &&
                String.IsNullOrEmpty(txtPesqMatricula.Text) &&
                String.IsNullOrEmpty(txtPesqCpf.Text) &&
                String.IsNullOrEmpty(txtPesqLote.Text) &&
                String.IsNullOrEmpty(txtPesqNome.Text))
            {
                //MostraMensagem(TbConsulta_Mensagem, "Prencha ao menos um campo de pesquisa para continuar");
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nPrencha ao menos um campo de pesquisa para continuar:\\nEmpresa / Matrícula / CPF / Nome / Lote");
                return false;
            }

            if (!String.IsNullOrEmpty(txtPesqCodEmpresa.Text) && !int.TryParse(txtPesqCodEmpresa.Text, out CodEmpresa))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Empresa inválido");
                return false;
            }

            if (!String.IsNullOrEmpty(txtPesqMatricula.Text) && !int.TryParse(txtPesqMatricula.Text, out CodMatricula))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Matrícula inválido");
                return false;
            }

            if (!String.IsNullOrEmpty(txtPesqCpf.Text) && !Int64.TryParse(txtPesqCpf.Text, out Cpf))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo CPF inválido");
                return false;
            }

            //if (ddlPesqTipoBoleto.SelectedValue == "0")
            //{
            //    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO campo Tipo deve ser preenchido");
            //    return false;
            //}

            return true;
        }

        private bool ValidarCampos()
        {

            int CodEmpresa, CodMatricula;
            decimal Valor;
            DateTime DtVencimento;

            if (String.IsNullOrEmpty(ddlTipoBoleto.SelectedValue))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO campo Tipo é obrigatório");
                ddlTipoBoleto.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtCodEmpresa.Text) || String.IsNullOrEmpty(txtMatricula.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOs campos Nº Empresa e Nº Matrícula são obrigatórios");
                return false;
            }
            else if (!int.TryParse(txtCodEmpresa.Text, out CodEmpresa) || !int.TryParse(txtMatricula.Text, out CodMatricula))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Empresa ou Matrícula inválido");
                return false;
            }

            if (String.IsNullOrEmpty(txtNome.Text))
            {
                //MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Valor obrigatório");
                MostraMensagem(lblMensagemNovo, "Atenção! Campo Nome é obrigatório");
                txtNome.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtValor.Text))
            {
                //MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Valor obrigatório");
                MostraMensagem(lblMensagemNovo, "Atenção! Campo Valor é obrigatório");
                return false;
            }
            else if (!decimal.TryParse(txtValor.Text, out Valor))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Valor inválido");
                return false;
            }

            if (String.IsNullOrEmpty(txtDtVencimento.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Dt. vencimento é obrigatório");
                return false;
            }
            else if (!DateTime.TryParse(txtDtVencimento.Text, out DtVencimento))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Dt. vencimento inválido");
                return false;
            }
            else if (DtVencimento <= DateTime.Now)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Dt. vencimento inválido. Vencimento deve ser maior que hoje");
                return false;
            }

            //if ((string.IsNullOrEmpty(ddlPeriodo.SelectedValue) || ddlPeriodo.SelectedValue == "0") && detalhado)
            //{
            //    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO campo período é obrigatório para o extrato detalhado");
            //    return false;
            //}

            //if (ddlAnoDe.SelectedValue == "0" &&
            //    ddlAnoAte.SelectedValue == "0" &&
            //    ddlTrimestreDe.SelectedValue == "0" &&
            //    ddlTrimestreAte.SelectedValue == "0" && periodo_anterior)
            //{
            //    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOs campos período (de/até) são obrigatórios para a pesquisa");
            //    return false;
            //}

            return true;
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {

            if (!ValidarCampos()) return;

            CarregaDadosPartic();

            BoletoBLL bll = new BoletoBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = bll.GerarNovoBoleto(Util.String2Short(ddlTipoBoleto.SelectedValue) ?? 0,
                                                Util.String2Short(ddlSubTipoBoleto.SelectedValue),
                                                Util.String2Short(txtCodEmpresa.Text) ?? 0,
                                                Util.String2Int32(txtMatricula.Text) ?? 0,
                                                Util.String2Short(txtDigito.Text),
                                                Util.String2Int32(hidNUM_IDNTF_RPTANT.Value),
                                                Util.String2Int64(txtCpf.Text) ?? 0,
                                                Util.String2Int32(txtLote.Text) ?? 0,
                                                txtNome.Text.ToUpper(),
                                                Util.String2Date(txtDtVencimento.Text) ?? DateTime.Now,
                                                Util.String2Decimal(txtValor.Text) ?? 0,
                                                txtInstrucoes.Text,
                                                lblEnd1.Text,
                                                lblEnd2.Text,
                                                " ",
                                                " ",
                                                " ",
                                                (user != null) ? user.login : "Desenv"                                    
                                               );

            if (res.Ok)
            {
                int PK_BOLETO = Util.String2Int32(res.CodigoCriado.ToString()) ?? 0;
                if (InicializaRelatorio(PK_BOLETO))
                {
                    VisualizarBoleto(PK_BOLETO);
                }
                MostraMensagem(lblMensagem, "Boleto gerado com sucesso!", "n_ok");
                grdBoleto.DataBind();
                pnlGrid.Visible = true;
                pnlNovoBoleto.Visible = false;
            } else {
                //MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção! Ocorreu um erro na geração do boleto.\\nMotivo:\\n" + res.Mensagem);
                MostraMensagem(lblMensagemNovo, "Atenção! Ocorreu um erro na geração do boleto.\\nMotivo:\\n" + res.Mensagem, "n_erro");
            }
        }

        private void CarregaDadosPartic()
        {
            ParticipanteBLL bll_p = new ParticipanteBLL();
            PARTICIPANTE part = bll_p.GetParticipanteBy(Util.String2Short(txtCodEmpresa.Text) ?? 0, Util.String2Int32(txtMatricula.Text) ?? 0, Util.String2Int32(hidNUM_IDNTF_RPTANT.Value) ?? 0, true);

            if (part != null)
            {
                if (String.IsNullOrEmpty(txtDigito.Text))
                {
                    txtDigito.Text = part.NUM_DIGVR_EMPRG.ToString();
                }

                if (String.IsNullOrEmpty(txtCpf.Text))
                {
                    txtCpf.Text = part.NUM_CPF_EMPRG.ToString();
                }

                if (String.IsNullOrEmpty(txtNome.Text))
                {
                    txtNome.Text = part.NOM_EMPRG.ToUpper();
                }

                if (String.IsNullOrEmpty(lblEnd1.Text) || String.IsNullOrEmpty(lblEnd2.Text))
                {
                    lblEnd1.Text = part.DCR_ENDER_EMPRG.Trim() + ", " + part.NUM_ENDER_EMPRG;
                    lblEnd2.Text = part.COD_CEP_EMPRG + "  " + part.NOM_BAIRRO_EMPRG.Trim() + "  " + part.NOM_CIDRS_EMPRG + "-" + part.COD_UNDFD_EMPRG;
                }

                //if (String.IsNullOrEmpty(lblEnd2.Text))
                //{
                //    lblEnd2.Text = part.COD_CEP_EMPRG + "  " + part.NOM_BAIRRO_EMPRG + "  " + part.NOM_CIDRS_EMPRG + "-" + part.COD_UNDFD_EMPRG;
                //}
            }
        }
    }
}