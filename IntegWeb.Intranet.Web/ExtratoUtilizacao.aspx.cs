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
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Saude.Aplicacao;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System.Globalization;
using System.ComponentModel;

namespace IntegWeb.Intranet.Web
{

    public partial class ExtUtilizacao : BasePage
    {



        Relatorio relatorio = new Relatorio();
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string relatorio_nome = "ExtratoUtilizacao";
        string relatorio_titulo = "Extrato de Utilização";
        string relatorio_simples = @"~/Relatorios/ExtratoUtilizacao.rpt";
        string nome_anexo = "Extrato_Utilizacao_";
        ExtratoUtilizacao epDados;
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

                if (!String.IsNullOrEmpty(COD_EMPRS) && !String.IsNullOrEmpty(NUM_RGTRO_EMPRG))
                {

                    //ReportCrystal.Visible = false;
                    txtCodEmpresa.Text = COD_EMPRS;
                    txtCodMatricula.Text = NUM_RGTRO_EMPRG;
                    if (!String.IsNullOrEmpty(PARTICIPANTEEMAIL) && PARTICIPANTEEMAIL != "undefined")
                    {
                        txtEMail.Text = PARTICIPANTEEMAIL;
                    }
                    //txtDtIni.Text = DateTime.Now.AddMonths(-12).ToString("dd/MM/yyyy");
                    //txtDtFim.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    CarregarDropDown();
                    //if (ValidarCampos())
                    //{
                    ExtratoUtilizacaoBLL CredReeBLL = new ExtratoUtilizacaoBLL();
                    int iRepresentante = 0;
                    int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
                    DataTable dadosGrid = CredReeBLL.Listar(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), iRepresentante, " ", DateTime.Now.AddMonths(-2), DateTime.Now);
                    grdExtratoUtilizacao.DataSource = dadosGrid;
                    grdExtratoUtilizacao.DataBind();
                    grdExtratoUtilizacao.Visible = true;
                    ifExtratoUtilSysDocs_0.Visible = false;
                    ifExtratoUtilSysDocs_1.Visible = false;
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

                ExtratoUtilizacaoBLL CredReeBLL = new ExtratoUtilizacaoBLL();

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

        protected void txtCodMatricula_TextChanged(object sender, EventArgs e)
        {
            ddlRepresentante.Items.Clear();
            CarregarDropDown();
            grdExtratoUtilizacao.Visible = false;
            ifExtratoUtilSysDocs_0.Visible = false;
            ifExtratoUtilSysDocs_1.Visible = false;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                grdExtratoUtilizacao.Sort("DATA_EMISSAO", System.Web.UI.WebControls.SortDirection.Descending);
                grdExtratoUtilizacao.Visible = true;
                txtEMail.Enabled = true;
                btnEmail.Enabled = true;
                ifExtratoUtilSysDocs_0.Visible = false;
                ifExtratoUtilSysDocs_1.Visible = false;

            }
        }
        public void grdExtratoUtilizacao_Sorting(object sender, GridViewSortEventArgs e)
        {
            CarregarTela(e.SortExpression);
        }

        private void CarregarTela(string SortExpression)
        {
            if (IsPostBack)
            {
                ExtratoUtilizacaoBLL CredReeBLL = new ExtratoUtilizacaoBLL();
                int iRepresentante = 0;
                int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);

                var dadosGrid = CredReeBLL.Listar(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), iRepresentante, " ", DateTime.Parse(txtDtIni.Text), DateTime.Parse(txtDtFim.Text));

                DataTable dt1 = dadosGrid;
                DataTable tblOrdered = new DataTable();
                DataView view = new DataView();

                var vsSort = ViewState["SortDirection"];

                if (String.IsNullOrEmpty(SortExpression))
                {
                    grdExtratoUtilizacao.DataSource = dt1;
                    grdExtratoUtilizacao.DataBind();
                    return;
                }

                if (dt1.Rows.Count > 0)
                {

                    if (vsSort == null)
                    {
                        ViewState["SortDirection"] = System.Web.UI.WebControls.SortDirection.Descending;
                    }


                    System.Web.UI.WebControls.SortDirection lastDirection = (System.Web.UI.WebControls.SortDirection)ViewState["SortDirection"];

                    if (lastDirection != System.Web.UI.WebControls.SortDirection.Ascending)
                    {
                        EnumerableRowCollection<DataRow> query = from row in dt1.AsEnumerable()
                                                                 orderby DateTime.Parse(row.Field<string>(SortExpression)) descending
                                                                 select row;
                        tblOrdered = query.AsDataView().ToTable();

                        ViewState["SortDirection"] = System.Web.UI.WebControls.SortDirection.Descending;

                    }
                    else
                    {
                        EnumerableRowCollection<DataRow> query = from row in dt1.AsEnumerable()
                                                                 orderby DateTime.Parse(row.Field<string>(SortExpression)) ascending
                                                                 select row;

                        tblOrdered = query.AsDataView().ToTable();

                        ViewState["SortDirection"] = System.Web.UI.WebControls.SortDirection.Descending;

                    }

                    grdExtratoUtilizacao.DataSource = tblOrdered;
                    grdExtratoUtilizacao.DataBind();
                }
            }
        }

        //protected void grdExtratoUtilizacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grdExtratoUtilizacao.PageIndex = e.NewPageIndex;
        //    //grdExtratoUtilizacao.DataBind();
        //    CarregarTela(grdExtratoUtilizacao.SortExpression);

        //}

        private System.Web.UI.WebControls.SortDirection GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            System.Web.UI.WebControls.SortDirection sortDirection = System.Web.UI.WebControls.SortDirection.Ascending;

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    System.Web.UI.WebControls.SortDirection lastDirection = (System.Web.UI.WebControls.SortDirection)ViewState["SortDirection"];
                    if (lastDirection == System.Web.UI.WebControls.SortDirection.Ascending)
                    {
                        sortDirection = System.Web.UI.WebControls.SortDirection.Descending;
                    }
                }
            }
            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void btnPesquisarSysdocs_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(false)) return;

            //ifExtratoUtilSysDocs_0.Src = Solicita_Consulta_GED_CRM(9); //tp_utilamh.php
            ifExtratoUtilSysDocs_0.Src = Solicita_URL_SysDocs(9); //tp_utilamh.php
            ifExtratoUtilSysDocs_0.Visible = !String.IsNullOrEmpty(ifExtratoUtilSysDocs_0.Src);
            //ifExtratoUtilSysDocs_1.Src = Solicita_Consulta_GED_CRM(10); //tp_utilpes.php
            ifExtratoUtilSysDocs_1.Src = Solicita_URL_SysDocs(10); //tp_utilpes.php
            ifExtratoUtilSysDocs_1.Visible = !String.IsNullOrEmpty(ifExtratoUtilSysDocs_1.Src);
            if (ifExtratoUtilSysDocs_0.Visible || ifExtratoUtilSysDocs_1.Visible)
            {
                grdExtratoUtilizacao.DataSource = null;
                grdExtratoUtilizacao.DataBind();
                grdExtratoUtilizacao.Visible = false;
                txtEMail.Enabled = false;
                btnEmail.Enabled = false;
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nExtrato de Utilização não localizado no Sysdocs para a matrícula " + txtCodMatricula.Text);
            }

        }

        private string Solicita_URL_SysDocs(int BdTypeName)
        {
            ExtratoUtilizacaoBLL CredReeBLL = new ExtratoUtilizacaoBLL();
            int iRepresentante = 0;
            int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
            List<UsuarioPortal> usPortal = CredReeBLL.ConsultarRepresentantes(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), iRepresentante);

            string fullUrl = "";

            if (usPortal.Count > 0)
            {

                UsuarioPortal userPortal = usPortal[0];

                DateTime dtIni, dtFim;
                DateTime.TryParse(txtDtIni.Text, out dtIni);
                DateTime.TryParse(txtDtFim.Text, out dtFim);

                string cp01 = userPortal.COD_EMPRS.ToString().PadLeft(3, '0'); //empresaFormat.format(dcVo.getEmpresa());
                string cp02 = userPortal.NUM_RGTRO_EMPRG.ToString().PadLeft(10, '0'); //matriculaFormat.format(dcVo.getMatricula());
                string cp03 = userPortal.NUM_DIGVR_EMPRG.ToString();
                string cp04 = dtIni.Year.ToString(); //anoInicio;
                string cp05 = dtFim.Year.ToString(); //anoFim;
                string cp06 = dtIni.Month.ToString(); //mesInicio;
                string cp07 = dtFim.Month.ToString(); //mesFim;
                string cp08 = userPortal.NomeCompleto.ToString().Trim().Replace(' ', '_'); //"0"; //dcVo.getNome().replace(' ', '_');
                string cp09 = userPortal.CPF.ToString(); //0; //dcVo.getCpf();

                string strUrl = String.Format("https://docs.prevcesp.com.br/ged/idocs_portal_ticket.php?BdTypeName={0}&CP01={1}&CP02={2}&CP03={3}&CP04={4}&CP05={5}&CP06={6}&CP07={7}&CP08={8}&CP09={9}",
                                                      BdTypeName,
                                                      cp01, cp02, cp03,
                                                      cp04, cp05, cp06,
                                                      cp07, cp08, cp09);

                try
                {
                    WebRequest request = WebRequest.Create(strUrl);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    if (responseFromServer.Split('|').Length > 1)
                    {
                        string token = responseFromServer.Split('|')[1];
                        fullUrl = "https://docs.prevcesp.com.br/ged/idocs_portal_procrel.php?ticket=" + token;

                        request = WebRequest.Create(fullUrl);
                        request.Credentials = CredentialCache.DefaultCredentials;
                        response = request.GetResponse();
                        dataStream = response.GetResponseStream();
                        reader = new StreamReader(dataStream);
                        responseFromServer = reader.ReadToEnd();

                        if (responseFromServer.ToUpper().Contains("FORAM ENCONTRADOS"))
                        {
                            fullUrl = "";
                        }

                    }
                    reader.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nErro ao consultar o Sysdocs.\\nMotivo:\\n" + ex.Message);
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nSysDoc não disponível para esta Empresa e Matrícula");
            }

            return fullUrl;
        }

        private string Solicita_Consulta_GED_CRM(int BdTypeName)
        {
            ExtratoUtilizacaoBLL CredReeBLL = new ExtratoUtilizacaoBLL();
            int iRepresentante = 0;
            int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
            List<UsuarioPortal> usPortal = CredReeBLL.ConsultarRepresentantes(int.Parse(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), iRepresentante);

            string fullUrl = "";

            if (usPortal.Count > 0)
            {

                UsuarioPortal userPortal = usPortal[0];

                DateTime dtIni, dtFim;
                DateTime.TryParse(txtDtIni.Text, out dtIni);
                DateTime.TryParse(txtDtFim.Text, out dtFim);

                string cp01 = userPortal.COD_EMPRS.ToString().PadLeft(3, '0'); //empresaFormat.format(dcVo.getEmpresa());
                string cp02 = userPortal.NUM_RGTRO_EMPRG.ToString().PadLeft(10, '0'); //matriculaFormat.format(dcVo.getMatricula());
                string cp03 = userPortal.NUM_DIGVR_EMPRG.ToString();
                string cp04 = dtIni.Year.ToString(); //anoInicio;
                string cp05 = dtFim.Year.ToString(); //anoFim;
                string cp06 = dtIni.Month.ToString(); //mesInicio;
                string cp07 = dtFim.Month.ToString(); //mesFim;
                string cp08 = userPortal.NomeCompleto.ToString().Trim(); //"0"; //dcVo.getNome().replace(' ', '_');
                string cp09 = userPortal.CPF.ToString(); //0; //dcVo.getCpf();
                string cp10 = txtEMail.Text;
                string cp11 = "";
                string cp12 = "";
                string cp13 = "F";

                fullUrl = String.Format("http://fcespwebp001/Consulta_GED_CRM/Consulta.aspx?BdTypeName={0}&CP01={1}&CP02={2}&CP03={3}&CP04={4}&CP05={5}&CP06={6}&CP07={7}&CP08={8}&CP09={9}&CP10={10}&CP10={13}",
                                                      BdTypeName,
                                                      cp01, cp02, cp03,
                                                      cp04, cp05, cp06,
                                                      cp07, cp08, cp09,
                                                      cp10, cp11, cp12,
                                                      cp13);

                //string strUrl = String.Format("https://docs.prevcesp.com.br/ged/idocs_portal_ticket.php?BdTypeName={0}&CP01={1}&CP02={2}&CP03={3}&CP04={4}&CP05={5}&CP06={6}&CP07={7}&CP08={8}&CP09={9}",
                //                                      BdTypeName,
                //                                      cp01, cp02, cp03,
                //                                      cp04, cp05, cp06,
                //                                      cp07, cp08, cp09);

                try
                {
                    WebRequest request = WebRequest.Create(fullUrl);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    if (responseFromServer.ToUpper().Contains("NENHUM ARQUIVO ENCONTRADO"))
                    {
                        fullUrl = "";
                    }

                    reader.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nErro ao consultar o Sysdocs.\\nMotivo:\\n" + ex.Message);
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nSysDoc não disponível para esta Empresa e Matrícula");
            }

            return fullUrl;
        }

        protected void grdExtratoUtilizacao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] Args = e.CommandArgument.ToString().Split(',');

            if (e.CommandName != "Sort")
            {
                switch (e.CommandName)
                {
                    case "Visualizar":

                        if (InicializaRelatorio(Args[0], Args[1], Args[2], Args[3], Args[4], Args[5]))
                        {
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
                        }
                        break;
                    case "Email":

                        if (InicializaDadosEMail(Args[0], Args[1], Args[2], Args[3], Args[4], Args[6]))
                        {
                            EnviaEmailExtratoUtil(txtEMail.Text);
                        }

                        break;
                }
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
                            InicializaDadosEMail(Args[0], Args[1], Args[2], Args[3], Args[4], Args[6]);
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
                if (ifExtratoUtilSysDocs_0.Visible)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nFunção inválida para consulta ao SysDocs");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nSelecione ao menos um registro para envio");
                }
                return;
            }
        }

        private bool InicializaRelatorio(string CodEmpresa, string CodMatricula, string NumIdntfRptant, string DatIni, string DatFim, string DatMov, string NumSubMatric = " ")
        {

            relatorio.titulo = relatorio_titulo;

            relatorio.parametros = new List<Parametro>();
            relatorio.parametros.Add(new Parametro() { parametro = "ANCODEMPRS", valor = CodEmpresa });
            relatorio.parametros.Add(new Parametro() { parametro = "ANNUMRGTROEMPRG", valor = CodMatricula });
            relatorio.parametros.Add(new Parametro() { parametro = "ANNUMIDNTFRPTANT", valor = NumIdntfRptant });
            relatorio.parametros.Add(new Parametro() { parametro = "ADDATINI", valor = DatIni });
            relatorio.parametros.Add(new Parametro() { parametro = "ADDATFIM", valor = DatFim });
            relatorio.parametros.Add(new Parametro() { parametro = "ADDATMOV", valor = "01/" + DatMov });
            relatorio.parametros.Add(new Parametro() { parametro = "ANCODPLANO", valor = "0" });
            relatorio.parametros.Add(new Parametro() { parametro = "ASSUBMATRIC", valor = NumSubMatric });
            relatorio.parametros.Add(new Parametro() { parametro = "ASQUADRO", valor = "2" });

            relatorio.arquivo = relatorio_simples;

            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;

            return true;

        }

        private bool InicializaDadosEMail(string CodEmpresa, string CodMatricula, string NumIdntfRptant, string DatIni, string DatFim, string DatEmissao, string NumSubMatric = " ")
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

            ExtratoUtilizacaoBLL ExtUtilBLL = new ExtratoUtilizacaoBLL();
            int iRepresentante = 0;
            int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
            epDados = ExtUtilBLL.Consultar(int.Parse(CodEmpresa), int.Parse(CodMatricula), iRepresentante, DateTime.Parse(DatIni), DateTime.Parse(DatFim));
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
            DateTime DtIni, DtFim;

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

            if (String.IsNullOrEmpty(txtDtIni.Text) || String.IsNullOrEmpty(txtDtFim.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nOs campos do Período de emissão são obrigatórios");
                return false;
            }
            else if (!DateTime.TryParse(txtDtIni.Text, out DtIni) || !DateTime.TryParse(txtDtFim.Text, out DtFim))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nPeríodo de emissão inválido");
                return false;
            }

            if (!BuscarDados) return true;

            ExtratoUtilizacaoBLL ExtUtilBLL = new ExtratoUtilizacaoBLL();
            int iRepresentante = 0;
            int.TryParse(ddlRepresentante.SelectedValue, out iRepresentante);
            epDados = ExtUtilBLL.Consultar(CodEmpresa, CodMatricula, iRepresentante, DtIni, DtFim);

            if (String.IsNullOrEmpty(epDados.empresa) && String.IsNullOrEmpty(epDados.registro))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nExtrato de Utilização não localizado para a matrícula " + txtCodMatricula.Text);
                ReportCrystal.Visible = false;
                grdExtratoUtilizacao.Visible = false;
                ifExtratoUtilSysDocs_0.Visible = false;
                ifExtratoUtilSysDocs_1.Visible = false;
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
