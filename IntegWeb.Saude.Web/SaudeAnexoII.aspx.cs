using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class SaudeAnexoII : BasePage
    {
        #region .: Propriedades :.

        Relatorio relatorio = new Relatorio();

        string relatorio_nome_aprovacao = "RelatorioAprovacao";
        string relatorio_nomeAnexoII = "RelatorioAnexoII";
        string relatorio_nome_consultaAnexoII = "ConsultaAnexoII";
        string relatorio_nome_consultaAnexoAntII = "ConsultaAnexoAntII";
        string relatorio_nome_histAnexoII = "HistAnexoII";

        string relatorio_titulo_aprovacao = "Relatório Aprovação de Aumento";
        string relatorio_titulo_AnexoII = "Relatório Anexo II";
        string relatorio_titulo_consultaAnexoII = "Consulta Anexo II";
        string relatorio_titulo_consultaAnexoAntII = "Consulta Anexo II Anterior";
        string relatorio_titulo_histAnexoII = "Histórico Anexo II";


        string relatorio_caminho_aprovacao = @"~/Relatorios/Rel_Planilha_Aprovacao.rpt";
        string relatorio_AnexoII = @"~/Relatorios/Rel_AnexoII.rpt";
        string relatorio_caminho_consultaAnexoII = @"~/Relatorios/Rel_AnexoIIConsulta.rpt";
        string relatorio_caminho_consultaAnexoAntII = @"~/Relatorios/Rel_AnexoIIConsultaAnt.rpt";
        string relatorio_caminho_histAnexoII = @"~/Relatorios/Rel_AnexoII_Hist_Exportacao.rpt";

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var user = (ConectaAD)Session["objUser"];


                AcessoAbas(user.login.ToUpper());

                SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();

                CarregaDropDowList(ddlContratoAB1, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
                CarregaDropDowList(ddlContratoAntAB1, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
                CarregaDropDowList(ddlContratoAB2, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
                CarregaDropDowList(ddlContratoAB3, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
                CarregaDropDowList(ddlContratoAB5, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
                CarregaDropDowList(ddlContratoPopUpAB5, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
                CarregaDropDowList(ddlServicoPopUpAB5, anexoBLL.CarregarServicos(null).ToList<object>(), "DESCRICAO", "COD_SERV");
                CarregaDropDowList(ddlContratoAB6, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");

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

        #region .: Aba 1 :.

        protected void btnGerarRelatorioAB1_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorios(txtNumContratoAB1.Text, "relAnexoII"))
            {
                ArquivoDownload adExtratoPdf = new ArquivoDownload();
                adExtratoPdf.nome_arquivo = relatorio_nome_consultaAnexoII + ".pdf";
                adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adExtratoPdf.nome_arquivo;
                adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(UpdatePanel, fullUrl, adExtratoPdf.nome_arquivo);
    

            }
        }

        protected void btnLimparAB1_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();

            txtNumContratoAB1.Text = "";
            CarregaDropDowList(ddlContratoAB1, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
        }

        protected void btnConsultaAntAB1_Click(object sender, EventArgs e)
        {
            divPesquisaAB1.Visible = false;
            divPesquisaAntAB1.Visible = true;
        }

        protected void btnVoltarAB1_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();

            divPesquisaAntAB1.Visible = false;
            divPesquisaAB1.Visible = true;
            txtContratoAntAB1.Text = "";
            grdContratoAnt.Visible = false;
            CarregaDropDowList(ddlContratoAntAB1, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
        }

        protected void btnPesquisaAntAB1_Click(object sender, EventArgs e)
        {
            grdContratoAnt.Visible = true;
            grdContratoAnt.DataBind();

        }

        protected void grdContratoAnt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Visualizar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string datInicio = (((Label)grdContratoAnt.Rows[rowIndex].FindControl("lblDatIniVigenciaAntAB1")).Text);
                string datFim = (((Label)grdContratoAnt.Rows[rowIndex].FindControl("lblDatFimVigenciaAntAB1")).Text);

                if (InicializaRelatorioConsultaAnt(txtContratoAntAB1.Text, datInicio, datFim))
                {
                    ArquivoDownload adConsultaAntPdf = new ArquivoDownload();
                    adConsultaAntPdf.nome_arquivo = relatorio_nome_consultaAnexoAntII + ".pdf";
                    adConsultaAntPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adConsultaAntPdf.nome_arquivo;
                    adConsultaAntPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adConsultaAntPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adConsultaAntPdf.nome_arquivo)] = adConsultaAntPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adConsultaAntPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adConsultaAntPdf.nome_arquivo);

                }
            }
        }

        protected void btnGerarExcel_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorios(txtNumContratoAB1.Text, "relAnexoII"))
            {
                ArquivoDownload adExtratoExcel = new ArquivoDownload();
                adExtratoExcel.nome_arquivo = relatorio_nome_consultaAnexoII + txtNumContratoAB1.Text + ".xls";
                adExtratoExcel.caminho_arquivo = Server.MapPath(@"UploadFile\") + adExtratoExcel.nome_arquivo;
                adExtratoExcel.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioExcel(adExtratoExcel.caminho_arquivo);

                Session[ValidaCaracteres(adExtratoExcel.nome_arquivo)] = adExtratoExcel;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoExcel.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(UpdatePanel, fullUrl, adExtratoExcel.nome_arquivo);
            }
        }

        #endregion

        #region .: Aba 2 :.

        protected void rdblTipoAumentoAB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();
            LimpaCamposAB2();
            CarregaDropDowList(ddlContratoAB2, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");

            if (rdblTipoAumentoAB2.SelectedValue == "LOTE")
            {
                divPesquisarAB2.Visible = false;
                divValorAB2.Visible = false;
                divLoteAB2.Visible = true;
                divCamposPercAumentoAB2.Visible = true;
                divBotoesAB2.Visible = true;
            }
            else
            {
                divValorAB2.Visible = false;
                divPesquisarAB2.Visible = true;
                divLoteAB2.Visible = false;
                divCamposPercAumentoAB2.Visible = false;
                divBotoesAB2.Visible = false;
            }

        }

        protected void btnPesquisarAB2_Click(object sender, EventArgs e)
        {
            if (rdblTipoAumentoAB2.SelectedValue == "GERAL")
            {

                grdServPrestAB2.DataBind();
                divServPrestAB2.Visible = true;
                divCamposPercAumentoAB2.Visible = true;
                divBotoesAB2.Visible = true;
                divValorAB2.Visible = false;

            }

            else if (rdblTipoAumentoAB2.SelectedValue == "PORVALOR")
            {
                grdServPrestAB2.DataBind();
                divServPrestAB2.Visible = true;
                divCamposPercAumentoAB2.Visible = false;
                divValorAB2.Visible = true;
                divBotoesAB2.Visible = true;
            }

            else if (rdblTipoAumentoAB2.SelectedValue == "ESCALONADO")
            {
                grdServPrestAB2.DataBind();
                divServPrestAB2.Visible = true;
                divCamposPercAumentoAB2.Visible = true;
                divBotoesAB2.Visible = true;
                divValorAB2.Visible = false;

            }

        }

        protected void btnLimparAB2_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();
            LimpaCamposAB2();
            CarregaDropDowList(ddlContratoAB2, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
            divServPrestAB2.Visible = false;
            divValorAB2.Visible = false;
            divCamposPercAumentoAB2.Visible = false;
            divBotoesAB2.Visible = false;

        }

        protected void grdServPrestAB2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkSelect = (e.Row.FindControl("chkSelect") as CheckBox);

                if (rdblTipoAumentoAB2.SelectedValue == "GERAL")
                {
                    chkSelect.Checked = true;
                    chkSelect.Enabled = false;
                }
                else
                {
                    chkSelect.Checked = false;
                    chkSelect.Enabled = true;
                }
            }
        }

        protected void btnPlanilhaAprovacaoAB2_Click(object sender, EventArgs e)
        {

            SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
            SAU_TB_SERV_X_HOSP_AND obj = new SAU_TB_SERV_X_HOSP_AND();
            var user = (ConectaAD)Session["objUser"];

            if (rdblTipoAumentoAB2.SelectedValue == "LOTE")
            {

                string caminho_servidor = Server.MapPath(@"~/");
                string Pasta_Server = caminho_servidor + @"\UploadFile";
                string Pasta_Server_Download = caminho_servidor + @"\UploadFile";

                Pasta_Server = Pasta_Server + @"\" + "APROVACAO" + "\\";
                Pasta_Server_Download = Pasta_Server_Download + @"\" + "EXTRAIR_APROVACAO_ANEXOII";

                if (Directory.Exists(Pasta_Server_Download))
                {
                    Directory.Delete(Pasta_Server_Download, true);
                }

                Directory.CreateDirectory(Pasta_Server);
                Directory.CreateDirectory(Pasta_Server_Download);


                if (String.IsNullOrEmpty(txtDatPercVigenciaAB2.Text))
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inserir a Data Vigência");
                    return;
                }

                if (!String.IsNullOrEmpty(txtPercReajAB2.Text) && Util.String2Decimal(txtPercReajAB2.Text) != 0 && !String.IsNullOrEmpty(txtPercDescAB2.Text) && Util.String2Decimal(txtPercDescAB2.Text) != 0)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Fazer Somente um Percentual por Operação");
                    return;
                }


                for (int i = 0; i < grdHospitalAB2.Rows.Count; i++)
                {
                    CheckBox chkSelect = grdHospitalAB2.Rows[i].FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {

                        obj.COD_HOSP = Convert.ToDecimal(((Label)grdHospitalAB2.Rows[i].FindControl("lblNumContratoLoteAB2")).Text);

                        List<decimal?> codServ = bll.GetServHosp(Convert.ToInt32(obj.COD_HOSP), null).Select(s => s.COD_SERV).ToList();

                        for (int serv = 0; serv < codServ.Count; serv++)
                        {

                            obj.DAT_PROPOSTA = Convert.ToDateTime(txtDatPercVigenciaAB2.Text);
                            obj.PORC_PROPOSTA = Util.String2Decimal(txtPercReajAB2.Text) ?? 0;
                            obj.PORC_DESC_PROPOSTO = Util.String2Decimal(txtPercDescAB2.Text) ?? 0;
                            obj.COD_SERV = Convert.ToInt32(codServ[serv]);
                            obj.VALOR_PROPOSTO = bll.CalculoValProposto(Convert.ToInt32(obj.COD_HOSP), Convert.ToInt32(obj.COD_SERV), Convert.ToDecimal(obj.PORC_PROPOSTA), Convert.ToDecimal(obj.PORC_DESC_PROPOSTO));
                            obj.USU_ALTERACAO = user.login;
                            Resultado res = bll.AplicarPorcentagemProposta(obj);

                            if (!res.Ok)
                            {
                                MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                                return;
                            }

                        }

                        if (InicializaRelatorios(obj.COD_HOSP.ToString(), "relAprovacao"))
                        {
                            ArquivoDownload adRelAprovacaoPdf = new ArquivoDownload();
                            adRelAprovacaoPdf.nome_arquivo = relatorio_nome_aprovacao + "_" + obj.COD_HOSP.ToString() + ".pdf";
                            adRelAprovacaoPdf.caminho_arquivo = Pasta_Server + adRelAprovacaoPdf.nome_arquivo;
                            adRelAprovacaoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                            ReportCrystal.ExportarRelatorioPdf(adRelAprovacaoPdf.caminho_arquivo);
                        }


                    }
                }

                if (Directory.GetFiles(Pasta_Server).Length > 0)
                {
                    ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\APROVACAO_ANEXOII_" + DateTime.Today.ToString("ddMMyyyy") + ".zip");

                    //Download do arquivo
                    ArquivoDownload adZipAprovLote = new ArquivoDownload();
                    adZipAprovLote.nome_arquivo = "APROVACAO_ANEXOII_" + DateTime.Today.ToString("ddMMyyyy") + ".zip";
                    adZipAprovLote.caminho_arquivo = Pasta_Server_Download + "\\" + adZipAprovLote.nome_arquivo;
                    Session[ValidaCaracteres(adZipAprovLote.nome_arquivo)] = adZipAprovLote;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipAprovLote.nome_arquivo);
                    AbrirNovaAba(UpdatePanel, fUrl, adZipAprovLote.nome_arquivo);
                }

                if (Directory.Exists(Pasta_Server))
                {
                    Directory.Delete(Pasta_Server, true);

                }

            }

            if (rdblTipoAumentoAB2.SelectedValue == "GERAL" || rdblTipoAumentoAB2.SelectedValue == "ESCALONADO")
            {

                if (String.IsNullOrEmpty(txtDatPercVigenciaAB2.Text))
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inserir a Data Vigência");
                    return;
                }

                if (!String.IsNullOrEmpty(txtPercReajAB2.Text) && Util.String2Decimal(txtPercReajAB2.Text) != 0 && !String.IsNullOrEmpty(txtPercDescAB2.Text) && Util.String2Decimal(txtPercDescAB2.Text) != 0)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Fazer Somente um Percentual por Operação");
                    return;
                }

                obj.COD_HOSP = Convert.ToDecimal(txtNumContratoAB2.Text);

                for (int i = 0; i < grdServPrestAB2.Rows.Count; i++)
                {
                    CheckBox chkSelect = grdServPrestAB2.Rows[i].FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        obj.DAT_PROPOSTA = Convert.ToDateTime(txtDatPercVigenciaAB2.Text);
                        obj.PORC_PROPOSTA = Util.String2Decimal(txtPercReajAB2.Text) ?? 0;
                        obj.PORC_DESC_PROPOSTO = Util.String2Decimal(txtPercDescAB2.Text) ?? 0;
                        obj.ID_REG = Convert.ToDecimal(((Label)grdServPrestAB2.Rows[i].FindControl("lblIdRegAB2")).Text);
                        obj.COD_SERV = Convert.ToInt32(((Label)grdServPrestAB2.Rows[i].FindControl("lblCodServAB2")).Text);
                        obj.VALOR_PROPOSTO = bll.CalculoValProposto(Convert.ToInt32(obj.COD_HOSP), Convert.ToInt32(obj.COD_SERV), Convert.ToDecimal(obj.PORC_PROPOSTA), Convert.ToDecimal(obj.PORC_DESC_PROPOSTO));
                        obj.USU_ALTERACAO = user.login;
                        Resultado res = bll.AplicarPorcentagemProposta(obj);

                        if (!res.Ok)
                        {
                            MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                            return;
                        }
                    }
                }

                if (InicializaRelatorios(txtNumContratoAB2.Text, "relAprovacao"))
                {
                    ArquivoDownload adRelAprovacaoPdf = new ArquivoDownload();
                    adRelAprovacaoPdf.nome_arquivo = relatorio_nome_aprovacao + "_" + txtNumContratoAB2.Text + ".pdf";
                    adRelAprovacaoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adRelAprovacaoPdf.nome_arquivo;
                    adRelAprovacaoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelAprovacaoPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelAprovacaoPdf.nome_arquivo)] = adRelAprovacaoPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelAprovacaoPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adRelAprovacaoPdf.nome_arquivo);

                }

            }

            if (rdblTipoAumentoAB2.SelectedValue == "PORVALOR")
            {
                if (String.IsNullOrEmpty(txtDatValVigenciaAB2.Text))
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inserir a Data Vigência");
                    return;
                }

                obj.COD_HOSP = Convert.ToDecimal(txtNumContratoAB2.Text);

                for (int i = 0; i < grdServPrestAB2.Rows.Count; i++)
                {
                    CheckBox chkSelect = grdServPrestAB2.Rows[i].FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        obj.DAT_PROPOSTA = Convert.ToDateTime(txtDatValVigenciaAB2.Text);
                        obj.PORC_PROPOSTA = 0;
                        obj.PORC_DESC_PROPOSTO = 0;
                        obj.COD_SERV = Convert.ToInt32(((Label)grdServPrestAB2.Rows[i].FindControl("lblCodServAB2")).Text);
                        obj.VALOR_PROPOSTO = Convert.ToDecimal(txtValPropostoAB2.Text);
                        obj.USU_ALTERACAO = user.login;

                        Resultado res = bll.AplicarPorcentagemProposta(obj);

                        if (!res.Ok)
                        {
                            MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                            return;
                        }


                    }
                }

                if (InicializaRelatorios(txtNumContratoAB2.Text, "relAprovacao"))
                {
                    ArquivoDownload adRelAprovacaoPdf = new ArquivoDownload();
                    adRelAprovacaoPdf.nome_arquivo = relatorio_nome_aprovacao + "_" + txtNumContratoAB2.Text + ".pdf";
                    adRelAprovacaoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adRelAprovacaoPdf.nome_arquivo;
                    adRelAprovacaoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelAprovacaoPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelAprovacaoPdf.nome_arquivo)] = adRelAprovacaoPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelAprovacaoPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adRelAprovacaoPdf.nome_arquivo);

                }
            }

        }

        protected void btnConfirmaAumentoAB2_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
            SAU_TB_SERV_X_HOSP_AND obj = new SAU_TB_SERV_X_HOSP_AND();
            var user = (ConectaAD)Session["objUser"];

            if (rdblTipoAumentoAB2.SelectedValue == "LOTE")
            {
                string caminho_servidor = Server.MapPath(@"~/");
                string Pasta_ServerConf = caminho_servidor + @"\UploadFile";
                string Pasta_Server_DownloadConf = caminho_servidor + @"\UploadFile";

                Pasta_ServerConf = Pasta_ServerConf + @"\" + "CONFIRMACAO" + "\\";
                Pasta_Server_DownloadConf = Pasta_Server_DownloadConf + @"\" + "EXTRAIR_CONFIRMACAO_ANEXOII";

                if (Directory.Exists(Pasta_Server_DownloadConf))
                {
                    Directory.Delete(Pasta_Server_DownloadConf, true);
                }

                Directory.CreateDirectory(Pasta_ServerConf);
                Directory.CreateDirectory(Pasta_Server_DownloadConf);


                for (int i = 0; i < grdHospitalAB2.Rows.Count; i++)
                {
                    CheckBox chkSelect = grdHospitalAB2.Rows[i].FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        obj.COD_HOSP = Convert.ToDecimal(((Label)grdHospitalAB2.Rows[i].FindControl("lblNumContratoLoteAB2")).Text);

                        List<decimal?> codServ = bll.GetServHosp(Convert.ToInt32(obj.COD_HOSP), null).Select(s => s.COD_SERV).ToList();

                        for (int serv = 0; serv < codServ.Count; serv++)
                        {

                            obj.COD_SERV = Convert.ToInt32(codServ[serv]);
                            obj.USU_ALTERACAO = user.login;

                            Resultado resConf = bll.ConfirmarAumento(obj);

                            if (!resConf.Ok)
                            {
                                MostraMensagemTelaUpdatePanel(UpdatePanel, resConf.Mensagem);
                                return;
                            }

                        }


                        if (InicializaRelatorios(obj.COD_HOSP.ToString(), "relAnexoII"))
                        {
                            ArquivoDownload adRelAnexoIIPdf = new ArquivoDownload();
                            adRelAnexoIIPdf.nome_arquivo = relatorio_nomeAnexoII + "_" + obj.COD_HOSP.ToString() + ".pdf";
                            adRelAnexoIIPdf.caminho_arquivo = Pasta_ServerConf + adRelAnexoIIPdf.nome_arquivo;
                            adRelAnexoIIPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                            ReportCrystal.ExportarRelatorioPdf(adRelAnexoIIPdf.caminho_arquivo);
                        }
                    }
                }

                if (Directory.GetFiles(Pasta_ServerConf).Length > 0)
                {
                    ZipFile.CreateFromDirectory(Pasta_ServerConf, Pasta_Server_DownloadConf + "\\CONFIRMACAO_ANEXOII_" + DateTime.Today.ToString("ddMMyyyy") + ".zip");

                    //Download do arquivo
                    ArquivoDownload adZipAnexoLote = new ArquivoDownload();
                    adZipAnexoLote.nome_arquivo = "CONFIRMACAO_ANEXOII_" + DateTime.Today.ToString("ddMMyyyy") + ".zip";
                    adZipAnexoLote.caminho_arquivo = Pasta_Server_DownloadConf + "\\" + adZipAnexoLote.nome_arquivo;
                    Session[ValidaCaracteres(adZipAnexoLote.nome_arquivo)] = adZipAnexoLote;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipAnexoLote.nome_arquivo);
                    AbrirNovaAba(UpdatePanel, fUrl, adZipAnexoLote.nome_arquivo);
                }

                if (Directory.Exists(Pasta_ServerConf))
                {
                    Directory.Delete(Pasta_ServerConf, true);

                }
            }

            if (rdblTipoAumentoAB2.SelectedValue == "GERAL" || rdblTipoAumentoAB2.SelectedValue == "ESCALONADO")
            {


                for (int i = 0; i < grdServPrestAB2.Rows.Count; i++)
                {
                    CheckBox chkSelect = grdServPrestAB2.Rows[i].FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        obj.COD_HOSP = Convert.ToDecimal(((Label)grdServPrestAB2.Rows[i].FindControl("lblContratoAB2")).Text);
                        obj.COD_SERV = Convert.ToInt32(((Label)grdServPrestAB2.Rows[i].FindControl("lblCodServAB2")).Text);
                        obj.USU_ALTERACAO = user.login;
                        Resultado res = bll.ConfirmarAumento(obj);

                        if (!res.Ok)
                        {
                            MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                            return;
                        }
                    }
                }

                if (InicializaRelatorios(txtNumContratoAB2.Text, "relAnexoII"))
                {
                    ArquivoDownload adRelAnexoIIPdf = new ArquivoDownload();
                    adRelAnexoIIPdf.nome_arquivo = relatorio_nomeAnexoII + "_" + txtNumContratoAB2.Text + ".pdf";
                    adRelAnexoIIPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adRelAnexoIIPdf.nome_arquivo;
                    adRelAnexoIIPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelAnexoIIPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelAnexoIIPdf.nome_arquivo)] = adRelAnexoIIPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelAnexoIIPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adRelAnexoIIPdf.nome_arquivo);

                }

            }

            if (rdblTipoAumentoAB2.SelectedValue == "PORVALOR")
            {

                for (int i = 0; i < grdServPrestAB2.Rows.Count; i++)
                {
                    CheckBox chkSelect = grdServPrestAB2.Rows[i].FindControl("chkSelect") as CheckBox;

                    if (chkSelect.Checked)
                    {
                        obj.COD_HOSP = Convert.ToDecimal(((Label)grdServPrestAB2.Rows[i].FindControl("lblContratoAB2")).Text);
                        obj.COD_SERV = Convert.ToInt32(((Label)grdServPrestAB2.Rows[i].FindControl("lblCodServAB2")).Text);
                        obj.USU_ALTERACAO = user.login;
                        Resultado res = bll.ConfirmarAumento(obj);

                        if (!res.Ok)
                        {
                            MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                            return;
                        }

                    }
                }

                if (InicializaRelatorios(txtNumContratoAB2.Text, "relAnexoII"))
                {
                    ArquivoDownload adRelAnexoIIPdf = new ArquivoDownload();
                    adRelAnexoIIPdf.nome_arquivo = relatorio_nomeAnexoII + "_" + txtNumContratoAB2.Text + ".pdf";
                    adRelAnexoIIPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adRelAnexoIIPdf.nome_arquivo;
                    adRelAnexoIIPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelAnexoIIPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelAnexoIIPdf.nome_arquivo)] = adRelAnexoIIPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelAnexoIIPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adRelAnexoIIPdf.nome_arquivo);

                }

            }


        }

        #endregion

        #region .: Aba 3 :.

        protected void btnPesquisarAB3_Click(object sender, EventArgs e)
        {
            grdHospital.DataBind();
            grdHospital.Visible = true;

        }

        protected void btnLimparAB3_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();
            txtNumContratoAB3.Text = "";
            grdHospital.Visible = false;
            CarregaDropDowList(ddlContratoAB3, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
        }

        protected void grdHospital_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "UpdateAB3")
            {
                SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
                SAU_TB_HOSP_AND obj = new SAU_TB_HOSP_AND();
                var user = (ConectaAD)Session["objUser"];

                obj.ID_REG = Convert.ToInt64(((Label)grdHospital.Rows[grdHospital.EditIndex].FindControl("lblIdRegAB3")).Text);
                obj.COD_HOSP = Convert.ToDecimal(((TextBox)grdHospital.Rows[grdHospital.EditIndex].FindControl("txtContratoAB3")).Text);
                obj.NOME_FANTASIA = (((TextBox)grdHospital.Rows[grdHospital.EditIndex].FindControl("txtNomeAB3")).Text);
                obj.DAT_INICIO_CONTRATO = Convert.ToDateTime(((TextBox)grdHospital.Rows[grdHospital.EditIndex].FindControl("txtDatIniContratoAB3")).Text);
                obj.CREDENCIADOR = (((TextBox)grdHospital.Rows[grdHospital.EditIndex].FindControl("txtCredenciadorAB3")).Text);
                obj.CIDADE = (((TextBox)grdHospital.Rows[grdHospital.EditIndex].FindControl("txtCidadeAB3")).Text);
                obj.REGIONAL = (((TextBox)grdHospital.Rows[grdHospital.EditIndex].FindControl("txtRegionalAB3")).Text);
                obj.CONTATO = (((TextBox)grdHospital.Rows[grdHospital.EditIndex].FindControl("txtContatoAB3")).Text);
                obj.USU_ALTERACAO = user.login;

                Resultado res = bll.AtualizaHospital(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdHospital.EditIndex = -1;
                    grdHospital.PageIndex = 0;
                    grdHospital.DataBind();
                }

            }

            if (e.CommandName == "DeleteAB3")
            {
                SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
                SAU_TB_HOSP_AND obj = new SAU_TB_HOSP_AND();
                var user = (ConectaAD)Session["objUser"];
                int rowIndex = Convert.ToInt32(e.CommandArgument);



                obj.ID_REG = Convert.ToInt64(((Label)grdHospital.Rows[rowIndex].FindControl("lblIdRegAB3")).Text);
                obj.COD_HOSP = Convert.ToInt32(((Label)grdHospital.Rows[rowIndex].FindControl("lblContratoAB3")).Text);
                obj.USU_ALTERACAO = user.login;

                Resultado res = bll.DeleteHospital(obj);
                Resultado resDelAll = bll.DeleteAllServicos(Convert.ToInt32(obj.COD_HOSP), user.login);

                if (res.Ok && resDelAll.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Deletado com Sucesso");
                    grdHospital.EditIndex = -1;
                    grdHospital.PageIndex = 0;
                    grdHospital.DataBind();
                    grdHospital.Visible = false;
                    txtNumContratoAB3.Text = "";
                }
                else if (!res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                    grdHospital.EditIndex = -1;
                    grdHospital.PageIndex = 0;
                    grdHospital.DataBind();
                    grdHospital.Visible = false;
                    txtNumContratoAB3.Text = "";
                }
                else if (!resDelAll.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, resDelAll.Mensagem);
                    grdHospital.EditIndex = -1;
                    grdHospital.PageIndex = 0;
                    grdHospital.DataBind();
                    grdHospital.Visible = false;
                    txtNumContratoAB3.Text = "";
                }

            }


        }

        protected void btnInserirAB3_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
            SAU_TB_HOSP_AND obj = new SAU_TB_HOSP_AND();
            var user = (ConectaAD)Session["objUser"];

            obj.COD_HOSP = Convert.ToInt32(txtNumContratoPopAB3.Text);
            obj.NOME_FANTASIA = txtNomePopAB3.Text.ToUpper();
            obj.DAT_INICIO_CONTRATO = Util.String2Date(txtDatInicioContratoPopAB3.Text);
            obj.CREDENCIADOR = txtCredenciadorPopAB3.Text.ToUpper();
            obj.CIDADE = txtCidadePopAB3.Text.ToUpper();
            obj.REGIONAL = txtRegionalPopAB3.Text.ToUpper();
            obj.CONTATO = txtContatoPopAB3.Text.ToUpper();
            obj.OBSERVACAOCONTRATUAL = null;
            obj.DAT_INI_VIGENCIA = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            obj.USU_ALTERACAO = user.login;

            Resultado res = bll.InserirHospital(obj);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Inserido com Sucesso");
                grdHospital.DataBind();
                grdHospital.Visible = false;
                LimpaCamposPopUpAB3();

            }
            else
            {
                lblCriticaAB3.Text = "Erro: " + res.Mensagem;
                lblCriticaAB3.Visible = true;
                ModalPopupExtenderAB3.Show();

            }

        }

        protected void btnCancelarAB3_Click(object sender, EventArgs e)
        {
            LimpaCamposPopUpAB3();

        }

        #endregion

        #region .: Aba 4 :.

        protected void btnPesquisarAB4_Click(object sender, EventArgs e)
        {
            grdServico.DataBind();
            grdServico.Visible = true;
        }

        protected void btnLimparAB4_Click(object sender, EventArgs e)
        {
            txtCodServicoAB4.Text = "";
            grdServico.Visible = false;
        }

        protected void grdServico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAB4")
            {
                SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
                SAU_TB_SERVICO_AND obj = new SAU_TB_SERVICO_AND();
                var user = (ConectaAD)Session["objUser"];


                obj.ID_REG = Convert.ToInt64(((Label)grdServico.Rows[grdServico.EditIndex].FindControl("lblIdRegAB4")).Text);
                obj.COD_SERV = Convert.ToInt64(((Label)grdServico.Rows[grdServico.EditIndex].FindControl("lblCodServAB4")).Text);
                obj.DESCRICAO = (((TextBox)grdServico.Rows[grdServico.EditIndex].FindControl("txtDescricaoAB4")).Text.ToUpper());
                obj.USU_ALTERACAO = user.login;

                Resultado res = bll.AtualizaServico(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Serviço Atualizado com Sucesso");
                    grdServico.EditIndex = -1;
                    grdServico.PageIndex = 0;
                    grdServico.DataBind();
                }

            }

            if (e.CommandName == "DeleteAB4")
            {
                SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
                SAU_TB_SERVICO_AND obj = new SAU_TB_SERVICO_AND();
                var user = (ConectaAD)Session["objUser"];
                int rowIndex = Convert.ToInt32(e.CommandArgument);


                obj.ID_REG = Convert.ToInt64(((Label)grdServico.Rows[rowIndex].FindControl("lblIdRegAB4")).Text);
                obj.COD_SERV = Convert.ToInt32(((Label)grdServico.Rows[rowIndex].FindControl("lblCodServAB4")).Text);
                obj.USU_ALTERACAO = user.login;

                Resultado res = bll.DeleteServico(obj);
                Resultado resDel = bll.DeleteAllServPrest(Convert.ToInt32(obj.COD_SERV), user.login);

                if (res.Ok && resDel.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Serviço Deletado com Sucesso");
                    grdServico.EditIndex = -1;
                    grdServico.PageIndex = 0;
                    grdServico.DataBind();
                    grdServico.Visible = false;
                    txtCodServicoAB4.Text = "";
                }

            }
        }

        protected void btnInserirServPopAB4_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
            SAU_TB_SERVICO_AND obj = new SAU_TB_SERVICO_AND();
            var user = (ConectaAD)Session["objUser"];

            obj.COD_SERV = Convert.ToInt64(txtCodServPopAB4.Text);
            obj.DESCRICAO = txtDescServPopAB4.Text.ToUpper();
            obj.DAT_INI_ALTERACAO = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            obj.USU_ALTERACAO = user.login;

            Resultado res = bll.InserirServico(obj);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Serviço Inserido com Sucesso");
                grdServico.DataBind();
                grdServico.Visible = false;
                LimpaCamposPopUpAB4();

            }
            else
            {
                lblCriticaPopAB4.Text = "Erro: " + res.Mensagem;
                lblCriticaPopAB4.Visible = true;
                ModalPopupServico.Show();
            }
        }

        protected void btnCancelaServPopAB4_Click(object sender, EventArgs e)
        {
            LimpaCamposPopUpAB4();
        }

        #endregion

        #region .: Aba 5 :.

        protected void btnPesquisarAB5_Click(object sender, EventArgs e)
        {
            grdServPrestAB5.DataBind();
            grdServPrestAB5.Visible = true;

        }

        protected void btnLimparAB5_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();
            txtNumContratoAB5.Text = "";
            txtCodServicoAB5.Text = "";
            grdServPrestAB5.Visible = false;
            CarregaDropDowList(ddlContratoAB5, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
        }

        protected void grdServPrestAB5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAB5")
            {
                SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
                SAU_TB_SERV_X_HOSP_AND obj = new SAU_TB_SERV_X_HOSP_AND();
                var user = (ConectaAD)Session["objUser"];


                obj.ID_REG = Convert.ToInt64(((Label)grdServPrestAB5.Rows[grdServPrestAB5.EditIndex].FindControl("lblIdRegAB5")).Text);
                obj.COD_HOSP = Convert.ToDecimal(((Label)grdServPrestAB5.Rows[grdServPrestAB5.EditIndex].FindControl("lblContratoAB5")).Text);
                obj.COD_SERV = Convert.ToInt64(((Label)grdServPrestAB5.Rows[grdServPrestAB5.EditIndex].FindControl("lblCodServAB5")).Text);
                obj.VALOR = Convert.ToDecimal(((TextBox)grdServPrestAB5.Rows[grdServPrestAB5.EditIndex].FindControl("txtValorAB5")).Text);
                obj.DAT_INI_VIGENCIA = Convert.ToDateTime(((TextBox)grdServPrestAB5.Rows[grdServPrestAB5.EditIndex].FindControl("txtDatIniVigenciaAB5")).Text);

                obj.USU_ALTERACAO = user.login;

                Resultado res = bll.AtualizaServPrest(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Serviço Atualizado com Sucesso");
                    grdServPrestAB5.EditIndex = -1;
                    grdServPrestAB5.PageIndex = 0;
                    grdServPrestAB5.DataBind();
                }
            }

            if (e.CommandName == "DeleteAB5")
            {
                SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
                SAU_TB_SERV_X_HOSP_AND obj = new SAU_TB_SERV_X_HOSP_AND();
                var user = (ConectaAD)Session["objUser"];
                int rowIndex = Convert.ToInt32(e.CommandArgument);


                obj.ID_REG = Convert.ToInt64(((Label)grdServPrestAB5.Rows[rowIndex].FindControl("lblIdRegAB5")).Text);
                obj.USU_ALTERACAO = user.login;

                Resultado res = bll.DeleteServPrest(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Serviço Deletado com Sucesso");
                    grdServPrestAB5.EditIndex = -1;
                    grdServPrestAB5.PageIndex = 0;
                    grdServPrestAB5.DataBind();
                    grdServPrestAB5.Visible = false;

                }
            }
        }

        protected void btnInserirServPrestPopUpAB5_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
            SAU_TB_SERV_X_HOSP_AND obj = new SAU_TB_SERV_X_HOSP_AND();
            var user = (ConectaAD)Session["objUser"];

            obj.COD_HOSP = Convert.ToDecimal(txtNumContratoPopAB5.Text);
            obj.COD_SERV = Convert.ToDecimal(txtNumServicoPopAB5.Text);
            obj.VALOR = Convert.ToDecimal(txtValorPopUpAB5.Text);
            obj.DAT_INI_VIGENCIA = Convert.ToDateTime(txtDatIniVigenciaPopUpAB5.Text);
            obj.USU_ALTERACAO = user.login;

            Resultado res = bll.InserirServPrest(obj);

            if (res.Ok)
            {
                lblCriticaAB5.Text = "Serviço " + txtNumServicoPopAB5.Text + " Inserido no Prestador " + txtNumContratoPopAB5.Text + " com Sucesso";
                lblCriticaAB5.Visible = true;
                lblCriticaAB5.ForeColor = System.Drawing.Color.Green;
                ModalPopupServPrest.Show();
            }
            else
            {
                lblCriticaAB5.Text = "Erro: " + res.Mensagem;
                lblCriticaAB5.Visible = true;
                lblCriticaAB5.ForeColor = System.Drawing.Color.Red;
                ModalPopupServPrest.Show();

            }


        }

        protected void btnCancelarServPrestPopUpAB5_Click(object sender, EventArgs e)
        {
            LimpaCamposPopUpAB5();
            grdServPrestAB5.DataBind();
        }

        #endregion

        #region .: Aba 6 :.

        protected void btnPesquisarAB6_Click(object sender, EventArgs e)
        {
            grdObsContratual.DataBind();
            grdObsContratual.Visible = true;
        }

        protected void btnLimparAB6_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();
            txtNumContratoAB6.Text = "";
            grdObsContratual.Visible = false;
            CarregaDropDowList(ddlContratoAB6, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
        }

        protected void grdObsContratual_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAB6")
            {
                SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
                SAU_TB_HOSP_AND obj = new SAU_TB_HOSP_AND();

                obj.ID_REG = Convert.ToInt64(((Label)grdObsContratual.Rows[grdObsContratual.EditIndex].FindControl("lblIdRegAB6")).Text);
                obj.COD_HOSP = Convert.ToDecimal(((Label)grdObsContratual.Rows[grdObsContratual.EditIndex].FindControl("lblContratoAB6")).Text);
                obj.OBSERVACAOCONTRATUAL = (((TextBox)grdObsContratual.Rows[grdObsContratual.EditIndex].FindControl("txtObsContratualAB6")).Text);

                Resultado res = bll.AtualizaObservacaoContratual(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdObsContratual.EditIndex = -1;
                    grdObsContratual.PageIndex = 0;
                    grdObsContratual.DataBind();
                    btnLimparAB6_Click(null, null);

                }
            }
        }

        #endregion

        #region .: Aba 7 :.

        protected void btnExportacaoAB7_Click(object sender, EventArgs e)
        {
            SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();
            var user = (ConectaAD)Session["objUser"];

            for (int hospExp = 0; hospExp < grdExportacao.Rows.Count; hospExp++)
            {
                CheckBox chkSelect = grdExportacao.Rows[hospExp].FindControl("chkSelect") as CheckBox;

                int codHosp = Convert.ToInt32(((Label)grdExportacao.Rows[hospExp].FindControl("lblCodContratoAB7")).Text);

                if (chkSelect.Checked)
                {
                    List<SAU_TB_SERV_X_HOSP_AND> codServ = bll.GetServHosp(codHosp, null).ToList();
                    List<ExportaArquivoScam> arquivoScam = bll.ExportaArquivoScam(codHosp);

                    foreach (var item in arquivoScam)
                    {
                        TB_VAL_RECURSO objExp = new TB_VAL_RECURSO();
                        SAU_TB_LOG_AND objLog = new SAU_TB_LOG_AND();

                        objExp.COD_RECURSO = item.Cod_Recurso;
                        objExp.RCOSEQ = item.RCOSEQ;
                        objExp.COD_TAB_RECURSO = item.Cod_Tab_Servicos;
                        objExp.RCOCODPROCEDIMENTO = item.RCOCODPROCEDIMENTO;
                        objExp.VAL_RECURSO = (Decimal?)codServ.Find(x => x.COD_HOSP == item.CodHosp && item.RCOCODPROCEDIMENTO == x.COD_SERV.ToString() && x.DAT_FIM_VIGENCIA == null).VALOR;
                        objExp.DAT_VAL_RECURSO = (DateTime)codServ.Find(x => x.COD_HOSP == item.CodHosp && x.COD_SERV == Convert.ToInt32(item.RCOCODPROCEDIMENTO)).DAT_INI_VIGENCIA;

                        Resultado res = bll.AtualizaTabelaValoresSCAM(objExp, item);

                        if (res.Mensagem != "Registro Existente na Base")
                        {
                            objLog.COD_HOSP = codHosp;
                            objLog.COD_SERV = Convert.ToDecimal(item.RCOCODPROCEDIMENTO);
                            objLog.COD_TAB_SERV = item.Cod_Tab_Servicos;
                            objLog.DAT_PROCESSAMENTO = DateTime.Today;
                            objLog.DAT_VIGENCIA_EXPORTADO = objExp.DAT_VAL_RECURSO;
                            objLog.DESC_PROCESSAMENTO = res.Ok ? "Exportado Com Sucesso" : res.Mensagem;
                            objLog.VALOR_EXPORTADO = objExp.VAL_RECURSO;
                            objLog.USU_PROCESSAMENTO = user.login;

                            Resultado resLog = bll.InserirLog(objLog);
                        }

                    }

                }
            }

            MostraMensagemTelaUpdatePanel(UpdatePanel, "Exportação Finalizada com Sucesso, Mais Informações no Histórico !");
            grdExportacao.DataBind();
        }

        protected void btnHistoricoExportacaoAB7_Click(object sender, EventArgs e)
        {
            divExportacaoAB7.Visible = false;
            divHistoricoExportacao.Visible = true;
        }

        protected void btnVoltarAB7_Click(object sender, EventArgs e)
        {
            divExportacaoAB7.Visible = true;
            divHistoricoExportacao.Visible = false;
            txtNumContratoHistAB7.Text = "";
            txtDatExportacaoHistAB7.Text = "";
            grdHistExportacaoAB7.DataBind();

        }

        protected void btnPesquisarAB7_Click(object sender, EventArgs e)
        {
            grdHistExportacaoAB7.DataBind();
        }

        protected void grdHistExportacaoAB7_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Visualizar")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);


                string codHosp = (((Label)grdHistExportacaoAB7.Rows[rowIndex].FindControl("lblCodContratoHistAB7")).Text);
                string datExportacao = (((Label)grdHistExportacaoAB7.Rows[rowIndex].FindControl("lblDatProcessamentoHistAB7")).Text);


                if (InicializaRelatorioHistorico(codHosp, datExportacao))
                {
                    ArquivoDownload adHistPdf = new ArquivoDownload();
                    adHistPdf.nome_arquivo = relatorio_nome_histAnexoII + codHosp + ".pdf";
                    adHistPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adHistPdf.nome_arquivo;
                    adHistPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adHistPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adHistPdf.nome_arquivo)] = adHistPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adHistPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adHistPdf.nome_arquivo);
                }
            }

        }


        #endregion

        #endregion

        #region .: Métodos :.

        public void AcessoAbas(string usuario)
        {
            if (usuario == "F02637" || usuario == "F02605" || usuario == "F02606" || usuario == "F02667" || usuario == "F02376" || usuario == "F02665" ||
                usuario == "F02661" || usuario == "F02460" || usuario == "F02608" || usuario == "F02552" || usuario == "F02464" || usuario == "F00749" ||
                usuario == "F02392" || usuario == "F02646" || usuario == "F02547" || usuario == "F02656" || usuario == "F02483" || usuario == "F02369" ||
                usuario == "F02218" || usuario == "F02427" || usuario == "F02198" || usuario == "F02657" || usuario == "F02276" || usuario == "F02503" ||
                usuario == "F02277" || usuario == "F02442" || usuario == "F02056" || usuario == "F02491" || usuario == "F02502" || usuario == "F02481" )
            {
                TbEmissaoAnexoII.Visible = true;
                TbAumento.Visible = false;
                TbHospital.Visible = false;
                TbCodServico.Visible = false;
                TbServPrestador.Visible = false;
                TbObservacao.Visible = false;
                TbExportacao.Visible = true;
                divExportacaoAB7.Visible = false;
                divHistoricoExportacao.Visible = true;
            }
            else
            {
                TbEmissaoAnexoII.Visible = true;
                TbAumento.Visible = true;
                TbHospital.Visible = true;
                TbCodServico.Visible = true;
                TbServPrestador.Visible = true;
                TbObservacao.Visible = true;
                TbExportacao.Visible = true;
                
            }


        }

        #region .: Aba 1 :.

        private bool InicializaRelatorios(string Cod_hosp, string tipoRel)
        {
            // Faz a seleção se o relatório será o Relatório AnexoII ou Relatório de Aprovação.
            relatorio.titulo = (tipoRel == "relAprovacao") ? relatorio_titulo_aprovacao : relatorio_titulo_AnexoII;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = (tipoRel == "relAprovacao") ? relatorio_caminho_aprovacao : relatorio_AnexoII;
            relatorio.parametros.Add(new Parametro() { parametro = "cod_hosp", valor = Cod_hosp });

            string nomeRel = (tipoRel == "relAprovacao") ? relatorio_nome_aprovacao : relatorio_nomeAnexoII;

            Session[nomeRel] = relatorio;
            ReportCrystal.RelatorioID = nomeRel;
            return true;
        }

        private bool InicializaRelatorioConsulta(string Cod_hosp)
        {

            relatorio.titulo = relatorio_titulo_consultaAnexoII;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_caminho_consultaAnexoII;
            relatorio.parametros.Add(new Parametro() { parametro = "cod_hosp", valor = Cod_hosp });

            Session[relatorio_nome_consultaAnexoII] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_consultaAnexoII;
            return true;
        }

        private bool InicializaRelatorioConsultaAnt(string Cod_hosp, string datIni, string datFim)
        {

            relatorio.titulo = relatorio_titulo_consultaAnexoAntII;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_caminho_consultaAnexoAntII;
            relatorio.parametros.Add(new Parametro() { parametro = "cod_hosp", valor = Cod_hosp });
            relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
            relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });

            Session[relatorio_nome_consultaAnexoAntII] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_consultaAnexoAntII;
            return true;
        }

        private bool InicializaRelatorioHistorico(string Cod_hosp, string datExportacao)
        {

            relatorio.titulo = relatorio_titulo_histAnexoII;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_caminho_histAnexoII;
            relatorio.parametros.Add(new Parametro() { parametro = "codHosp", valor = Cod_hosp });
            relatorio.parametros.Add(new Parametro() { parametro = "datExport", valor = datExportacao });

            Session[relatorio_nome_histAnexoII] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_histAnexoII;
            return true;
        }




        #endregion

        #region .: Aba 2 :.

        public void LimpaCamposAB2()
        {
            txtNumContratoAB2.Text = "";
            txtPercDescAB2.Text = "";
            txtPercReajAB2.Text = "";
            txtValPropostoAB2.Text = "";
            txtDatPercVigenciaAB2.Text = "";
            txtDatValVigenciaAB2.Text = "";

        }

        #endregion

        #region .: Aba 3 :.

        public void LimpaCamposPopUpAB3()
        {
            SaudeAnexoIIBLL bll = new SaudeAnexoIIBLL();

            txtNumContratoPopAB3.Text = "";
            txtNomePopAB3.Text = "";
            txtDatInicioContratoPopAB3.Text = "";
            txtCredenciadorPopAB3.Text = "";
            txtCidadePopAB3.Text = "";
            txtRegionalPopAB3.Text = "";
            txtContatoPopAB3.Text = "";
            lblCriticaAB3.Visible = false;
            CarregaDropDowList(ddlContratoAB3, bll.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
        }

        #endregion

        #region .: Aba 4 :.

        public void LimpaCamposPopUpAB4()
        {
            txtCodServPopAB4.Text = "";
            txtDescServPopAB4.Text = "";
            lblCriticaPopAB4.Visible = false;

        }

        #endregion

        #region .: Aba 5 :.

        public void LimpaCamposPopUpAB5()
        {
            SaudeAnexoIIBLL anexoBLL = new SaudeAnexoIIBLL();

            txtNumContratoPopAB5.Text = "";
            txtNumServicoPopAB5.Text = "";
            txtValorPopUpAB5.Text = "";
            txtDatIniVigenciaPopUpAB5.Text = "";
            CarregaDropDowList(ddlContratoPopUpAB5, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
            CarregaDropDowList(ddlServicoPopUpAB5, anexoBLL.CarregarServicos(null).ToList<object>(), "DESCRICAO", "COD_SERV");
            lblCriticaAB5.Visible = false;
        }


        #endregion

      

        #endregion


    }
}