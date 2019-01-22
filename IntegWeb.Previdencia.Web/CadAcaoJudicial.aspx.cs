using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Concessao;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class CadAcaoJudicial : BasePage
    {

        #region .: Propriedades :.

        Relatorio relatorio = new Relatorio();
        string relatorio_nome_capa = "AcaoJudicial";
        string relatorio_titulo = "Relatório processo ação judicial";
        string relatorio_caminho_capa = @"~/Relatorios/Rel_Acao_Judicial.rpt";

        string relatorio_nome_deposito = "Deposito";
        string relatorio_titulo_deposito = "Relatório Depósito Judicial";
        string relatorio_caminho_deposito = @"~/Relatorios/Rel_beneficio.rpt";

        string relatorio_nome_gerencial = "Gerencial";
        string relatorio_titulo_gerencial = "Relatório Gerencial - Ação Judicial";
        string relatorio_caminho_gerencial = @"~/Relatorios/Rel_Gerencial_Acao_Judicial.rpt";

        CadAcaoJudicialBLL bll = new CadAcaoJudicialBLL();
        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdAcaoJudic.Visible = false;

                ddlFatorGerador.DataSource = bll.GetFato();
                ddlFatorGerador.DataValueField = "COD_TIPLTO";
                ddlFatorGerador.DataTextField = "DESC_TIPLTO";
                ddlFatorGerador.DataBind();
                ddlFatorGerador.Items.Insert(0, new ListItem("---Selecione---", ""));

                ddlPlano.DataSource = bll.GetPlano();
                ddlPlano.DataValueField = "NUM_PLBNF";
                ddlPlano.DataTextField = "DCR_PLBNF";
                ddlPlano.DataBind();
                ddlPlano.Items.Insert(0, new ListItem("---Selecione---", ""));

                ddlPlanoRelGeral.DataSource = bll.GetPlano();
                ddlPlanoRelGeral.DataValueField = "NUM_PLBNF";
                ddlPlanoRelGeral.DataTextField = "DCR_PLBNF";
                ddlPlanoRelGeral.DataBind();
                ddlPlanoRelGeral.Items.Insert(0, new ListItem("---Selecione---", ""));

                ddlFatorGeradorRelGeral.DataSource = bll.GetFato();
                ddlFatorGeradorRelGeral.DataValueField = "COD_TIPLTO";
                ddlFatorGeradorRelGeral.DataTextField = "DESC_TIPLTO";
                ddlFatorGeradorRelGeral.DataBind();
                ddlFatorGeradorRelGeral.Items.Insert(0, new ListItem("---Selecione---", ""));
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



        protected void btnNovo_Click(object sender, EventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            divPesquisa.Visible = false;
            divNovoCadastro.Visible = true;
            lblPagina.Text = "1";
            hfCOD_ACAO_JUDIC.Value = "0";
            btnAnterior.Visible = false;
            btnProximo.Visible = false;
            btnDuplicar.Enabled = false;
            btnDeposito.Enabled = false;
            btnRelatorio.Enabled = false;
            btnExcluir.Enabled = false;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdAcaoJudic.DataBind();
            grdAcaoJudic.Visible = true;
        }

        protected void btnPesqLimpar_Click(object sender, EventArgs e)
        {
            txtPesqEmpresa.Text = "";
            txtPesqMatricula.Text = "";
            txtPesquisa.Text = "";
            ddlPesquisa.SelectedValue = "1";

            grdAcaoJudic.DataBind();
            grdAcaoJudic.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            if (Util.String2Date(txtDtSolic.Text) > Util.String2Date(txtDtPrazo.Text))
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "A Data de Solicitação não pode ser maior que a Data Prazo");
            }
            else
            {

                PRE_TBL_ACAO_JUDIC obj = new PRE_TBL_ACAO_JUDIC();
                var user = (ConectaAD)Session["objUser"];

                obj.ID_REG = Convert.ToInt32(hfCOD_ACAO_JUDIC.Value);
                obj.COD_EMPRS = Convert.ToInt16(txtEmpresa.Text);
                obj.NUM_RGTRO_EMPRG = Convert.ToInt32(txtMatricula.Text);
                obj.CPF_EMPRG = Convert.ToInt64(txtCPF.Text);
                obj.NUM_SEQ_PROC = Convert.ToInt32(lblPagina.Text);
                obj.NOM_PARTICIP = txtNome.Text.ToUpper();
                obj.NOM_RECLAMANTE = txtReclamante.Text.ToUpper();
                obj.DAT_DIB = Util.String2Date(txtDtDib.Text);
                obj.NUM_PLBNF = Util.String2Short(ddlPlano.SelectedValue);
                obj.DAT_SOLIC = Util.String2Date(txtDtSolic.Text);
                obj.TIP_SOLIC = ddlTipSolic.SelectedValue;
                obj.NRO_PROCESSO = txtNumProcesso.Text.ToUpper();
                obj.DAT_PRAZO = Util.String2Date(txtDtPrazo.Text);
                obj.COD_VARA_PROC = txtLocalVara.Text.ToUpper();
                obj.COD_TIPLTO = Convert.ToInt16(ddlFatorGerador.SelectedValue);
                obj.TIP_PLTO = ddlTipoAndamento.SelectedValue;
                obj.POLO_ACJUD = ddlPoloAcaoJudicial.SelectedValue;
                obj.NRO_PASTA = txtPasta.Text;
                obj.NOM_ADVOG = ddlLawyer.SelectedValue;
                obj.CALC_APRESENT = ddlCalcHomolog.SelectedValue;
                obj.NRO_MEDICAO = txtMedicao.Text;
                obj.DESC_PROC = txtDescricaoAcao.Text;
                obj.OBS_PROC = txtObservacao.Text;
                obj.USU_RESPON = ddlResponsavel.SelectedValue;
                obj.DAT_SOLIC_SCR = Util.String2Date(txtDtSolicSRC.Text);
                obj.DAT_RESP = Util.String2Date(txtDtResposta.Text);
                obj.TP_DOC = ddlTipoDocumento.SelectedValue;
                obj.LOCAL_ARQ = Util.String2Decimal(txtLocalArquivo.Text);
                obj.LOG_INCLUSAO = "SYS_FUNCESP";
                obj.DTH_INCLUSAO = DateTime.Today;
                //obj.LOG_EXCLUSAO =;
                //obj.LOG_EXCLUSAO=;
                obj.IDC_CANC_REV = chkRevisao.Checked ? "S" : "N";
                obj.IDC_RECEB_SOLIC_SCR = chkRecebTemplate.Checked ? "S" : "N";

                Resultado res = bll.Inserir(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
                    grdAcaoJudic.DataBind();
                    btnDuplicar.Enabled = true;
                    btnDeposito.Enabled = true;
                    btnRelatorio.Enabled = true;
                    btnExcluir.Enabled = true;
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtEmpresa.Text = "";
            txtMatricula.Text = "";
            txtCPF.Text = "";
            txtNome.Text = "";
            txtReclamante.Text = "";
            txtDtDib.Text = "";
            txtDtSolic.Text = "";
            txtNumProcesso.Text = "";
            txtDtPrazo.Text = "";
            txtLocalVara.Text = "";
            txtPasta.Text = "";
            txtDescricaoAcao.Text = "";
            txtObservacao.Text = "";
            txtDtSolicSRC.Text = "";
            txtDtResposta.Text = "";
            ddlTipoDocumento.SelectedValue = "";
            txtLocalArquivo.Text = "";
            ddlResponsavel.SelectedValue = "";
            ddlPlano.SelectedValue = "";
            ddlFatorGerador.SelectedValue = "";
            ddlTipSolic.SelectedValue = "";
            ddlLawyer.SelectedValue = "";
            divPesquisa.Visible = false;
            divNovoCadastro.Visible = true;
        }

        protected void btnDeposito_Click(object sender, EventArgs e)
        {
            ModalPopupExtender.Show();
        }

        protected void btnCapa_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorioCapa(txtEmpresa.Text, txtMatricula.Text, lblPagina.Text))
            {
                ArquivoDownload adRelEtqPdf = new ArquivoDownload();
                adRelEtqPdf.nome_arquivo = relatorio_nome_capa + "_" + DateTime.Now.ToFileTime() + ".pdf";
                adRelEtqPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adRelEtqPdf.nome_arquivo;
                adRelEtqPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adRelEtqPdf.caminho_arquivo);

                Session[ValidaCaracteres(adRelEtqPdf.nome_arquivo)] = adRelEtqPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelEtqPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upUpdatePanel, fullUrl, adRelEtqPdf.nome_arquivo);

            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorioDeposito(txtEmpresa.Text, txtMatricula.Text, txtDtPag.Text))
            {
                ArquivoDownload adRelEtqPdf = new ArquivoDownload();
                adRelEtqPdf.nome_arquivo = relatorio_nome_deposito + "_" + DateTime.Now.ToFileTime() + ".pdf";
                adRelEtqPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adRelEtqPdf.nome_arquivo;
                adRelEtqPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adRelEtqPdf.caminho_arquivo);

                Session[ValidaCaracteres(adRelEtqPdf.nome_arquivo)] = adRelEtqPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelEtqPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upUpdatePanel, fullUrl, adRelEtqPdf.nome_arquivo);

            }
        }

        //protected void btnGerencial_Click(object sender, EventArgs e)
        //{
        //    if (InicializaRelatorioGerencial(txtEmpresa_rpt.Text, txtMatricula_rpt.Text))
        //    {
        //        ArquivoDownload adRelEtqPdf = new ArquivoDownload();
        //        adRelEtqPdf.nome_arquivo = relatorio_nome_gerencial + "_" + DateTime.Now.ToFileTime() + ".pdf";
        //        adRelEtqPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adRelEtqPdf.nome_arquivo;
        //        adRelEtqPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
        //        ReportCrystal.ExportarRelatorioPdf(adRelEtqPdf.caminho_arquivo);

        //        Session[ValidaCaracteres(adRelEtqPdf.nome_arquivo)] = adRelEtqPdf;
        //        string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelEtqPdf.nome_arquivo);
        //        AdicionarAcesso(fullUrl);
        //        AbrirNovaAba(upUpdatePanel, fullUrl, adRelEtqPdf.nome_arquivo);
        //    }
        //}
        //protected void btnGerencialReset_Click(object sender, EventArgs e)
        //{
        //    txtEmpresa_rpt.Text = "";
        //    txtMatricula_rpt.Text = "";
        //}

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            int numPag = Convert.ToInt32(hfNUM_PAG.Value) - 1;
            int idReg = bll.GetID(Convert.ToInt16(txtEmpresa.Text), Convert.ToInt32(txtMatricula.Text), numPag, txtNumProcesso.Text);
            CarregarAcaoJudic(idReg);
            divPesquisa.Visible = false;
            divNovoCadastro.Visible = true;
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            int numPag = Convert.ToInt32(hfNUM_PAG.Value) + 1;
            int idReg = bll.GetID(Convert.ToInt16(txtEmpresa.Text), Convert.ToInt32(txtMatricula.Text), numPag, txtNumProcesso.Text);
            CarregarAcaoJudic(idReg);
            divPesquisa.Visible = false;
            divNovoCadastro.Visible = true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            txtEmpresa.Text = "";
            txtMatricula.Text = "";
            txtCPF.Text = "";
            txtNome.Text = "";
            txtReclamante.Text = "";
            txtDtDib.Text = "";
            txtDtSolic.Text = "";
            txtNumProcesso.Text = "";
            txtDtPrazo.Text = "";
            txtLocalVara.Text = "";
            txtPasta.Text = "";
            txtDescricaoAcao.Text = "";
            txtObservacao.Text = "";
            txtDtSolicSRC.Text = "";
            txtDtResposta.Text = "";
            ddlTipoDocumento.SelectedValue = "";
            txtLocalArquivo.Text = "";

            ddlLawyer.SelectedValue = "";
            ddlPlano.SelectedValue = "";
            ddlFatorGerador.SelectedValue = "";
            ddlTipSolic.SelectedValue = "";
            ddlResponsavel.SelectedValue = "";
            divPesquisa.Visible = true;
            divNovoCadastro.Visible = false;
            grdAcaoJudic.Visible = false;
        }

        protected void grdAcaoJudic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int idReg = Convert.ToInt32(e.CommandArgument);
                CarregarAcaoJudic(idReg);
                btnExcluir.Enabled = true;
                btnDeposito.Enabled = true;
                btnRelatorio.Enabled = true;
                divPesquisa.Visible = false;
                divNovoCadastro.Visible = true;
            }

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            PRE_TBL_ACAO_JUDIC obj = new PRE_TBL_ACAO_JUDIC();
            var user = (ConectaAD)Session["objUser"];
            obj.ID_REG = Convert.ToInt32(hfCOD_ACAO_JUDIC.Value);
            obj.LOG_EXCLUSAO = user.login;
            Resultado res = bll.DeleteAcaoJudic(obj);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Deletado com Sucesso");
                grdAcaoJudic.EditIndex = -1;
                grdAcaoJudic.PageIndex = 0;
                grdAcaoJudic.DataBind();

                btnVoltar_Click(sender, e);
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
                grdAcaoJudic.EditIndex = -1;
                grdAcaoJudic.PageIndex = 0;
                grdAcaoJudic.DataBind();
            }
        }

        protected void btnDuplicar_Click(object sender, EventArgs e)
        {
            PRE_TBL_ACAO_JUDIC obj = new PRE_TBL_ACAO_JUDIC();
            var user = (ConectaAD)Session["objUser"];

            obj.COD_EMPRS = Convert.ToInt16(txtEmpresa.Text);
            obj.NUM_RGTRO_EMPRG = Convert.ToInt32(txtMatricula.Text);
            obj.CPF_EMPRG = Convert.ToInt64(txtCPF.Text);
            obj.NOM_PARTICIP = Convert.ToString(txtNome.Text).ToUpper();
            obj.NOM_RECLAMANTE = Convert.ToString(txtReclamante.Text).ToUpper();
            obj.DAT_DIB = Util.String2Date(txtDtDib.Text);
            obj.NUM_PLBNF = Util.String2Short(ddlPlano.SelectedValue);
            obj.DAT_SOLIC = Util.String2Date(txtDtSolic.Text);
            obj.TIP_SOLIC = ddlTipSolic.SelectedValue;
            obj.NRO_PROCESSO = txtNumProcesso.Text;
            obj.DAT_PRAZO = Util.String2Date(txtDtPrazo.Text); ;
            obj.COD_VARA_PROC = Convert.ToString(txtLocalVara.Text);
            obj.COD_TIPLTO = Convert.ToInt16(ddlFatorGerador.SelectedValue);
            obj.NUM_SEQ_PROC = Convert.ToInt32(lblPagina.Text) + 1;
            //            obj.DESC_PROC = txtDescricaoAcao.Text;

            obj.LOG_INCLUSAO = "SYS_FUNCESP"; //user.login;
            obj.DTH_INCLUSAO = DateTime.Today;

            Resultado res = bll.Inserir(obj);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Duplicado Com Sucesso");
                int numPag = Convert.ToInt32(lblPagina.Text) + 1;
                int idReg = bll.GetID(Convert.ToInt16(txtEmpresa.Text), Convert.ToInt32(txtMatricula.Text), numPag, txtNumProcesso.Text);
                CarregarAcaoJudic(idReg);
                grdAcaoJudic.EditIndex = -1;
                grdAcaoJudic.PageIndex = 0;
                grdAcaoJudic.DataBind();
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de Duplicar.\\nErro: " + res.Mensagem);
            }

        }

        protected void btnRelCompSal_Click(object sender, EventArgs e)
        {
            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Em Desenvolvimento");
        }

        protected void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            int? pEmpresa = Util.String2Int32(txtEmpresa.Text);
            int? pMatricula = Util.String2Int32(txtMatricula.Text);
            // long? pCpf = Util.String2Int64(txtCPF.Text);
            string DIB;
            IntegWeb.Previdencia.Aplicacao.DAL.Concessao.CadAcaoJudicialDAL.DADOS_CADASTRAIS_view obj = new Aplicacao.DAL.Concessao.CadAcaoJudicialDAL.DADOS_CADASTRAIS_view();

            obj = bll.GetNome(pEmpresa, pMatricula, null);
            if (obj == null)
            {
                lblMensagem.Text = "Participante não encontrado";
                lblMensagem.Visible = true;
            }
            else
            {
                txtEmpresa.Text = obj.EMPRESA.ToString();
                txtMatricula.Text = obj.MATRICULA.ToString();
                txtCPF.Text = obj.CPF.ToString();
                txtNome.Text = obj.NOME;
                txtReclamante.Text = obj.NOME;
                DIB = Convert.ToDateTime(obj.DIB, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                if (DIB == "01/01/0001")
                    DIB = "";
                txtDtDib.Text = DIB;
                ddlPlano.SelectedValue = obj.PLANO.ToString();
                lblMensagem.Visible = false;
            }


        }

        protected void txtCPF_TextChanged(object sender, EventArgs e)
        {
            int? pEmpresa = Util.String2Int32(txtEmpresa.Text);
            int? pMatricula = Util.String2Int32(txtMatricula.Text);
            long? pCpf = Util.String2Int64(txtCPF.Text);
            string DIB;
            IntegWeb.Previdencia.Aplicacao.DAL.Concessao.CadAcaoJudicialDAL.DADOS_CADASTRAIS_view obj = new Aplicacao.DAL.Concessao.CadAcaoJudicialDAL.DADOS_CADASTRAIS_view();

            obj = bll.GetNome(pEmpresa, null, pCpf);
            if (obj == null)
            {
                lblMensagem.Text = "Participante não encontrado";
                lblMensagem.Visible = true;
            }
            else
            {
                txtEmpresa.Text = obj.EMPRESA.ToString();
                txtMatricula.Text = obj.MATRICULA.ToString();
                txtCPF.Text = obj.CPF.ToString();
                txtNome.Text = obj.NOME;
                txtReclamante.Text = obj.NOME;
                DIB = Convert.ToDateTime(obj.DIB, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                if (DIB == "01/01/0001")
                    DIB = "";
                txtDtDib.Text = DIB;
                ddlPlano.SelectedValue = obj.PLANO.ToString();
                lblMensagem.Visible = false;
            }

        }

        protected void relAcoesJud_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (relAcoesJud.SelectedValue)
            {
                case "Geral":
                    trRelGeral.Visible = true;
                    trRelEstatistico.Visible = false;
                    trRelPendente.Visible = false;
                    break;

                case "Estatistico":
                    trRelGeral.Visible = false;
                    trRelEstatistico.Visible = true;
                    trRelPendente.Visible = false;
                    break;

                case "Pendentes":
                    trRelGeral.Visible = false;
                    trRelEstatistico.Visible = false;
                    trRelPendente.Visible = true;
                    break;
            }

        }

        protected void btnGerarRelatorioPendentes_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet("Relatorio");

            DataTable table = ds.Tables.Add("Pendentes");

            table.Columns.Add("COD_EMPRS");
            table.Columns.Add("NUM_RGTRO_EMPRG");
            table.Columns.Add("CPF_EMPRG");
            table.Columns.Add("NOM_PARTICIP");
            table.Columns.Add("NOM_RECLAMANTE");
            table.Columns.Add("DAT_DIB");
            table.Columns.Add("NUM_PLBNF");
            table.Columns.Add("DAT_SOLIC");
            table.Columns.Add("TIP_SOLIC");
            table.Columns.Add("NRO_PROCESSO");
            table.Columns.Add("DAT_PRAZO");
            table.Columns.Add("COD_VARA_PROC");
            table.Columns.Add("COD_TIPLTO");
            table.Columns.Add("TIP_PLTO");
            table.Columns.Add("POLO_ACJUD");
            table.Columns.Add("NRO_PASTA");
            table.Columns.Add("NOM_ADVOG");
            table.Columns.Add("CALC_APRESENT");
            table.Columns.Add("NRO_MEDICAO");
            table.Columns.Add("DESC_PROC");
            table.Columns.Add("OBS_PROC");
            table.Columns.Add("USU_RESPON");
            table.Columns.Add("DAT_SOLIC_SCR");
            table.Columns.Add("DAT_RESP");
            table.Columns.Add("TP_DOC");
            table.Columns.Add("LOCAL_ARQ");
            table.Columns.Add("IDC_RECEB_SOLIC_SCR");
            table.Columns.Add("IDC_CANC_REV");

            DataRow row;

            foreach (var rel in bll.GetDadosRespostaPendentes())
            {
                row = table.NewRow();

                row["COD_EMPRS"] = rel.COD_EMPRS;
                row["NUM_RGTRO_EMPRG"] = rel.NUM_RGTRO_EMPRG;
                row["CPF_EMPRG"] = rel.CPF_EMPRG;
                row["NOM_PARTICIP"] = rel.NOM_PARTICIP;
                row["NOM_RECLAMANTE"] = rel.NOM_RECLAMANTE;
                row["DAT_DIB"] = rel.DAT_DIB;
                row["NUM_PLBNF"] = rel.NUM_PLBNF;
                row["DAT_SOLIC"] = rel.DAT_SOLIC;
                row["TIP_SOLIC"] = rel.TIP_SOLIC;
                row["NRO_PROCESSO"] = rel.NRO_PROCESSO;
                row["DAT_PRAZO"] = rel.DAT_PRAZO;
                row["COD_VARA_PROC"] = rel.COD_VARA_PROC;
                row["COD_TIPLTO"] = rel.COD_TIPLTO;
                row["TIP_PLTO"] = rel.TIP_PLTO;
                row["POLO_ACJUD"] = rel.POLO_ACJUD;
                row["NRO_PASTA"] = rel.NRO_PASTA;
                row["NOM_ADVOG"] = rel.NOM_ADVOG;
                row["CALC_APRESENT"] = rel.CALC_APRESENT;
                row["NRO_MEDICAO"] = rel.NRO_MEDICAO;
                row["DESC_PROC"] = rel.DESC_PROC;
                row["OBS_PROC"] = rel.OBS_PROC;
                row["USU_RESPON"] = rel.USU_RESPON;
                row["DAT_SOLIC_SCR"] = rel.DAT_SOLIC_SCR;
                row["DAT_RESP"] = rel.DAT_RESP;
                row["TP_DOC"] = rel.TP_DOC;
                row["LOCAL_ARQ"] = rel.LOCAL_ARQ;
                row["IDC_RECEB_SOLIC_SCR"] = rel.IDC_RECEB_SOLIC_SCR;
                row["IDC_CANC_REV"] = rel.IDC_CANC_REV;


                table.Rows.Add(row);
            }

            if (table.Rows.Count > 0)
            {
                ArquivoDownload adMedAberta = new ArquivoDownload();

                adMedAberta.nome_arquivo = "RELATORIO_PROCESSOS_JUDICIAIS_PENDENTES.xlsx";

                adMedAberta.dados = ds.Tables[0];

                Session[ValidaCaracteres(adMedAberta.nome_arquivo)] = adMedAberta;

                string fullMedAberta = "WebFile.aspx?dwFile=" + ValidaCaracteres(adMedAberta.nome_arquivo);

                AdicionarAcesso(fullMedAberta);

                AbrirNovaAba(upUpdatePanel, fullMedAberta, adMedAberta.nome_arquivo);

                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Relatório de Respostas Pendentes Gerado com sucesso!");
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Nenhum resultado retornado");
                return;
            }
        }

        protected void btnGerarRelatorioEstatistico_Click(object sender, EventArgs e)
        {
            #region validações
            DateTime? dataInicio = null;
            DateTime? dataFinal = null;

            DateTime resultado = DateTime.MinValue;
            if (DateTime.TryParse(dataIni.Text, out resultado))
            {
                dataInicio = Convert.ToDateTime(dataIni.Text);
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Data Inicio inválida");
                return;
            }

            if (DateTime.TryParse(dataFim.Text, out resultado))
            {
                dataFinal = Convert.ToDateTime(dataFim.Text);
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Data Fim inválida");
                return;
            }

            if (Util.String2Date(dataIni.Text) > Util.String2Date(dataFim.Text))
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "A Data de Inicio não pode ser maior que a Data Fim");
                return;
            }
            #endregion

            DataSet ds = new DataSet("Relatorio");

            DataTable table = ds.Tables.Add("Estatistico");

            table.Columns.Add("MES");
            table.Columns.Add("ANO");
            table.Columns.Add("QTD");

            DataRow row;

            var culture = new System.Globalization.CultureInfo("pt-BR", true);

            DateTime DatIni, DatFim;
            DateTime.TryParse(dataIni.Text, out DatIni);
            DateTime.TryParse(dataFim.Text, out DatFim);

            var dadosEstatisticos = bll.GetDadosEstatistico(DatIni.ToString("dd/MM/yyyy"), DatFim.ToString("dd/MM/yyyy"));

            foreach (var dados in dadosEstatisticos)
            {
                row = table.NewRow();

                row["MES"] = culture.TextInfo.ToTitleCase(culture.DateTimeFormat.GetMonthName(int.Parse(dados.MES)));
                row["ANO"] = dados.ANO;
                row["QTD"] = dados.QTD;

                table.Rows.Add(row);
            }

            if (table.Rows.Count > 0)
            {
                ArquivoDownload adMedAberta = new ArquivoDownload();

                adMedAberta.nome_arquivo = "RELATORIO_PROCESSOS_JUDICIAIS_ESTATISTICO.xlsx";

                adMedAberta.dados = ds.Tables[0];

                Session[ValidaCaracteres(adMedAberta.nome_arquivo)] = adMedAberta;

                string fullMedAberta = "WebFile.aspx?dwFile=" + ValidaCaracteres(adMedAberta.nome_arquivo);

                AdicionarAcesso(fullMedAberta);

                AbrirNovaAba(upUpdatePanel, fullMedAberta, adMedAberta.nome_arquivo);

                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Relatório Estatistico Gerado com sucesso!");
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Nenhum resultado retornado");
                return;
            }

        }

        protected void btnGerarRelatorioGeral_Click(object sender, EventArgs e)
        {
            DateTime? validaDataResposta = null;
            DateTime? validaDataPrazo = null;
            DateTime resultado = DateTime.MinValue;

            #region validações
            if (txtdataPrazo.Text != "")
            {
                if (DateTime.TryParse(txtdataPrazo.Text, out resultado))
                {
                    validaDataPrazo = Convert.ToDateTime(txtdataPrazo.Text);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Data Prazo inválida");
                    return;
                }
            }

            if (txtdataResposta.Text != "")
            {
                if (DateTime.TryParse(txtdataResposta.Text, out resultado))
                {
                    validaDataResposta = Convert.ToDateTime(txtdataResposta.Text);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Data Resposta inválida");
                    return;
                }
            }
            #endregion


            DataSet ds = new DataSet("Relatorio");

            DataTable table = ds.Tables.Add("Geral");

            table.Columns.Add("PLANO");
            table.Columns.Add("FATO_GERADOR");
            table.Columns.Add("COD_EMPRS");
            table.Columns.Add("NUM_RGTRO_EMPRG");
            table.Columns.Add("CPF_EMPRG");
            table.Columns.Add("NOM_PARTICIP");
            table.Columns.Add("NOM_RECLAMANTE");
            table.Columns.Add("DAT_DIB");
            table.Columns.Add("NUM_PLBNF");
            table.Columns.Add("DAT_SOLIC");
            table.Columns.Add("TIP_SOLIC");
            table.Columns.Add("NRO_PROCESSO");
            table.Columns.Add("DAT_PRAZO");
            table.Columns.Add("COD_VARA_PROC");
            table.Columns.Add("COD_TIPLTO");
            table.Columns.Add("TIP_PLTO");
            table.Columns.Add("POLO_ACJUD");
            table.Columns.Add("NRO_PASTA");
            table.Columns.Add("NOM_ADVOG");
            table.Columns.Add("CALC_APRESENT");
            table.Columns.Add("NRO_MEDICAO");
            table.Columns.Add("DESC_PROC");
            table.Columns.Add("OBS_PROC");
            table.Columns.Add("USU_RESPON");
            table.Columns.Add("DAT_SOLIC_SCR");
            table.Columns.Add("DAT_RESP");
            table.Columns.Add("TP_DOC");
            table.Columns.Add("LOCAL_ARQ");
            table.Columns.Add("IDC_RECEB_SOLIC_SCR");
            table.Columns.Add("IDC_CANC_REV");

            DataRow row;

            foreach (var rel in bll.GetDadosGeral(ddlPlanoRelGeral.SelectedItem.Value, ddlFatorGeradorRelGeral.SelectedItem.Value, ddlTipoAndamentoRelGeral.SelectedItem.Value, ddlResponsavelRelGeral.SelectedItem.Value, txtLocalArquivoGeral.Text, validaDataResposta, validaDataPrazo))
            {
                row = table.NewRow();

                row["PLANO"] = rel.PLANO_BENEFICIO_FSS.DCR_PLBNF;
                row["FATO_GERADOR"] = rel.PRE_TBL_ACAO_VR_TIPLTO.DESC_TIPLTO;
                row["COD_EMPRS"] = rel.COD_EMPRS;
                row["NUM_RGTRO_EMPRG"] = rel.NUM_RGTRO_EMPRG;
                row["CPF_EMPRG"] = rel.CPF_EMPRG;
                row["NOM_PARTICIP"] = rel.NOM_PARTICIP;
                row["NOM_RECLAMANTE"] = rel.NOM_RECLAMANTE;
                row["DAT_DIB"] = rel.DAT_DIB;
                row["NUM_PLBNF"] = rel.NUM_PLBNF;
                row["DAT_SOLIC"] = rel.DAT_SOLIC;
                row["TIP_SOLIC"] = rel.TIP_SOLIC;
                row["NRO_PROCESSO"] = rel.NRO_PROCESSO;
                row["DAT_PRAZO"] = rel.DAT_PRAZO;
                row["COD_VARA_PROC"] = rel.COD_VARA_PROC;
                row["COD_TIPLTO"] = rel.COD_TIPLTO;
                row["TIP_PLTO"] = rel.TIP_PLTO;
                row["POLO_ACJUD"] = rel.POLO_ACJUD;
                row["NRO_PASTA"] = rel.NRO_PASTA;
                row["NOM_ADVOG"] = rel.NOM_ADVOG;
                row["CALC_APRESENT"] = rel.CALC_APRESENT;
                row["NRO_MEDICAO"] = rel.NRO_MEDICAO;
                row["DESC_PROC"] = rel.DESC_PROC;
                row["OBS_PROC"] = rel.OBS_PROC;
                row["USU_RESPON"] = rel.USU_RESPON;
                row["DAT_SOLIC_SCR"] = rel.DAT_SOLIC_SCR;
                row["DAT_RESP"] = rel.DAT_RESP;
                row["TP_DOC"] = rel.TP_DOC;
                row["LOCAL_ARQ"] = rel.LOCAL_ARQ;
                row["IDC_RECEB_SOLIC_SCR"] = rel.IDC_RECEB_SOLIC_SCR;
                row["IDC_CANC_REV"] = rel.IDC_CANC_REV;


                table.Rows.Add(row);
            }

            if (table.Rows.Count > 0)
            {
                ArquivoDownload adMedAberta = new ArquivoDownload();

                adMedAberta.nome_arquivo = "RELATORIO_PROCESSOS_JUDICIAIS_GERAL.xlsx";

                adMedAberta.dados = ds.Tables[0];

                Session[ValidaCaracteres(adMedAberta.nome_arquivo)] = adMedAberta;

                string fullMedAberta = "WebFile.aspx?dwFile=" + ValidaCaracteres(adMedAberta.nome_arquivo);

                AdicionarAcesso(fullMedAberta);

                AbrirNovaAba(upUpdatePanel, fullMedAberta, adMedAberta.nome_arquivo);

                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Relatório Geral Gerado com sucesso!");
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Nenhum resultado retornado");
                return;
            }

        }

        #endregion

        #region .: Métodos :.

        private void CarregarAcaoJudic(int idReg)
        {
            PRE_TBL_ACAO_JUDIC loadObj = new PRE_TBL_ACAO_JUDIC();

            loadObj = bll.GetAcaoJudic(idReg);

            hfCOD_ACAO_JUDIC.Value = idReg.ToString();
            txtEmpresa.Text = loadObj.COD_EMPRS.ToString();
            txtMatricula.Text = loadObj.NUM_RGTRO_EMPRG.ToString();
            txtCPF.Text = loadObj.CPF_EMPRG.ToString();
            txtNome.Text = loadObj.NOM_PARTICIP;
            txtReclamante.Text = loadObj.NOM_RECLAMANTE;
            txtDtDib.Text = Util.Date2String(loadObj.DAT_DIB);
            txtDtSolic.Text = Util.Date2String(loadObj.DAT_SOLIC);
            txtNumProcesso.Text = loadObj.NRO_PROCESSO;
            txtDtPrazo.Text = Util.Date2String(loadObj.DAT_PRAZO);
            txtLocalVara.Text = loadObj.COD_VARA_PROC;
            txtPasta.Text = loadObj.NRO_PASTA;
            txtDescricaoAcao.Text = loadObj.DESC_PROC;
            txtObservacao.Text = loadObj.OBS_PROC;
            txtDtSolicSRC.Text = Util.Date2String(loadObj.DAT_SOLIC_SCR);
            txtDtResposta.Text = Util.Date2String(loadObj.DAT_RESP);
            ddlTipoDocumento.SelectedValue = loadObj.TP_DOC;
            txtLocalArquivo.Text = loadObj.LOCAL_ARQ.ToString();
            lblPagina.Text = loadObj.NUM_SEQ_PROC.ToString();
            ddlResponsavel.SelectedIndex = 0;
            if (loadObj.USU_RESPON != null) ddlResponsavel.SelectedValue = loadObj.USU_RESPON.ToUpper();
            ddlPlano.SelectedValue = loadObj.NUM_PLBNF.ToString();
            ddlFatorGerador.SelectedValue = loadObj.COD_TIPLTO.ToString();
            ddlTipSolic.SelectedValue = loadObj.TIP_SOLIC;
            if (loadObj.NRO_MEDICAO != null) txtMedicao.Text = loadObj.NRO_MEDICAO;
            if (loadObj.NOM_ADVOG != null) ddlLawyer.SelectedValue = loadObj.NOM_ADVOG;
            if (loadObj.TIP_PLTO != null) ddlTipoAndamento.SelectedValue = loadObj.TIP_PLTO.Replace("¿", "–");
            hfNUM_PAG.Value = lblPagina.Text;
            loadObj.MAX_PAG = bll.GetLastPage(Convert.ToInt32(txtMatricula.Text));
            btnAnterior.Visible = true;
            btnProximo.Visible = true;
            btnAnterior.Enabled = Convert.ToInt32(hfNUM_PAG.Value) > 1;
            btnProximo.Enabled = Convert.ToInt32(hfNUM_PAG.Value) < loadObj.MAX_PAG;
            btnDuplicar.Enabled = Convert.ToInt32(hfNUM_PAG.Value) == loadObj.MAX_PAG;
            divPesquisa.Visible = false;
            divNovoCadastro.Visible = true;

        }

        public bool InicializaRelatorioCapa(string codEmp, string matricula, string pagina)
        {
            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_caminho_capa;
            relatorio.parametros.Add(new Parametro() { parametro = "empresa", valor = codEmp });
            relatorio.parametros.Add(new Parametro() { parametro = "matricula", valor = matricula });
            relatorio.parametros.Add(new Parametro() { parametro = "numseqProc", valor = pagina });

            Session[relatorio_nome_capa] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_capa;
            return true;
        }

        public bool InicializaRelatorioDeposito(string pCodEmp, string pMatricula, string pDatPagto)
        {
            relatorio.titulo = relatorio_titulo_deposito;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_caminho_deposito;
            relatorio.parametros.Add(new Parametro() { parametro = "datPagto", valor = pDatPagto });
            relatorio.parametros.Add(new Parametro() { parametro = "empresa", valor = pCodEmp });
            relatorio.parametros.Add(new Parametro() { parametro = "matricula", valor = pMatricula });

            Session[relatorio_nome_deposito] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_deposito;
            return true;
        }

        public bool InicializaRelatorioGerencial(string pCodEmp, string pMatricula)
        {
            relatorio.titulo = relatorio_titulo_gerencial;
            relatorio.parametros = new List<Parametro>();
            pCodEmp = pCodEmp.Length <= 0 ? "0" : pCodEmp;
            pMatricula = pMatricula.Length <= 0 ? "0" : pMatricula;

            relatorio.arquivo = relatorio_caminho_gerencial;
            relatorio.parametros.Add(new Parametro() { parametro = "empresa", valor = pCodEmp });
            relatorio.parametros.Add(new Parametro() { parametro = "matricula", valor = pMatricula });

            Session[relatorio_nome_gerencial] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_gerencial;
            return true;
        }

        #endregion


    }
}