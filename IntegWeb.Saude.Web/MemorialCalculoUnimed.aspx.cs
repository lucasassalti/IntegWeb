using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.DAL.Processos;

namespace IntegWeb.Saude.Web
{
    public partial class MemorialCalculoUnimed : BasePage
    {
        #region .: Propriedades :.

        Relatorio relatorio = new Relatorio();
        string relatorio_nomeDet = "RelatorioCIDet";
        string relatorio_nome = "RelatorioCIMov";
        string relatorio_nomeResumo = "RelatorioCIResumo";
        string relatorio_nomePlano = "RelatorioCIPlano";

        string relatorio_titulo = "Relatórios CI Unimed";

        string relatorio_simples = @"~/Relatorios/Rel_MemorialCalculoTipoCartao.rpt";
        string relatorio_detalhado = @"~/Relatorios/Rel_MemorialUnimed_Det.rpt";
        string relatorio_resumo = @"~/Relatorios/Rel_MemorialCalculoRes.rpt";
        string relatorio_plano = @"~/Relatorios/Rel_MemorialCalculoPlano.rpt";

        int quantidadeMov = 0;

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MemorialCalculoUnimedBLL bll = new MemorialCalculoUnimedBLL();

                ddlNomeUnimed.DataSource = bll.GetUnimed();
                ddlNomeUnimed.DataValueField = "COD_PLANO";
                ddlNomeUnimed.DataTextField = "DES_PLANO";
                ddlNomeUnimed.DataBind();
                ddlNomeUnimed.Items.Insert(0, new ListItem("---Selecione---", ""));

                List<MemorialCalculoUnimedDAL.CAD_TBL_CONTROLEUNIMED_view2> list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
                DataTable dt = list.ToDataTable();

                grdMemorialUnimed.DataSource = dt;
                grdMemorialUnimed.DataBind();



            }
        }

        protected void grdMemorialUnimed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            foreach (GridViewRow row in grdMemorialUnimed.Rows)
            {
                var chkBox = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

                if (chkBox.Checked)
                {
                    PersistRowIndex(container.DataItemIndex);
                }
                else
                {
                    RemoveRowIndex(container.DataItemIndex);

                }

            }

            MemorialCalculoUnimedBLL bll = new MemorialCalculoUnimedBLL();
            grdMemorialUnimed.PageIndex = e.NewPageIndex;
            List<MemorialCalculoUnimedDAL.CAD_TBL_CONTROLEUNIMED_view2> list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
            DataTable dt = list.ToDataTable();

            grdMemorialUnimed.DataSource = dt;
            grdMemorialUnimed.DataBind();
            RePopulateCheckBoxes();

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            MemorialCalculoUnimedBLL bll = new MemorialCalculoUnimedBLL();

            List<MemorialCalculoUnimedDAL.CAD_TBL_CONTROLEUNIMED_view2> list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
            DataTable dt = list.ToDataTable();

            grdMemorialUnimed.DataSource = dt;
            grdMemorialUnimed.DataBind();
            divPesquisar.Visible = true;
            divCamposPesquisa.Visible = false;
            lblQtdCartoes.Visible = true;
            lblQtdText.Visible = true;
            lblQtdCartoes.Text = quantidadeMov.ToString();
            lblValorText.Visible = true;
            lblValorTotal.Visible = true;

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtDatInicio.Text = "";
            txtDatFim.Text = "";
            ddlNomeUnimed.Text = "";
            divPesquisar.Visible = false;
            grdMemorialUnimed.DataBind();

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            divCamposPesquisa.Visible = true;
            txtDatInicio.Text = "";
            txtDatFim.Text = "";
            ddlNomeUnimed.Text = "";
            divPesquisar.Visible = false;
            lblValorTotal.Text = "0";
            lblQtdCartoes.Text = "0";
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            MemorialCalculoUnimedBLL bll = new MemorialCalculoUnimedBLL();
            FUN_TBL_MEMORIAL_UNIMED obj = new FUN_TBL_MEMORIAL_UNIMED();
            Resultado res = new Resultado();

            bll.Delete();//zera tabela

            foreach (GridViewRow row in grdMemorialUnimed.Rows)
            {
                var chkBox = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

                if (chkBox.Checked)
                {
                    PersistRowIndex(container.DataItemIndex);
                }
                else
                {
                    RemoveRowIndex(container.DataItemIndex);

                }

            }

            List<MemorialCalculoUnimedDAL.CAD_TBL_CONTROLEUNIMED_view2> list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
            DataTable dt = list.ToDataTable();
            grdMemorialUnimed.AllowPaging = false;
            grdMemorialUnimed.DataSource = dt;
            grdMemorialUnimed.DataBind();
            RePopulateCheckBoxes();


            foreach (GridViewRow row in grdMemorialUnimed.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

                    if (chkSelect.Checked == true)
                    {

                        Label lblDatMovimentacaoCod = (Label)row.FindControl("lblDatMovimentacao");
                        Label lblCodEmpresaCod = (Label)row.FindControl("lblCodEmpresa");
                        Label lblNumMatriculaCod = (Label)row.FindControl("lblNumMatricula");
                        Label lblSubCod = (Label)row.FindControl("lblSub");
                        Label lblCodIdentificacaoCod = (Label)row.FindControl("lblCodIdentificacao");
                        Label lblNomePartcipanteCod = (Label)row.FindControl("lblNome");
                        Label lblCodControleUnimedCod = (Label)row.FindControl("lblCodUnimed");
                        Label lblMovimentacaoCod = (Label)row.FindControl("lblMovimentacao");
                        Label lblCodigoCespCod = (Label)row.FindControl("lblCodigoCesp");

                        obj.DAT_GERACAO = Convert.ToDateTime(lblDatMovimentacaoCod.Text);
                        obj.COD_EMPRS = short.Parse(lblCodEmpresaCod.Text.ToUpper().Trim());
                        obj.NUM_MATRICULA = Convert.ToInt32(lblNumMatriculaCod.Text.ToUpper().Trim());
                        obj.SUB_MATRICULA = lblSubCod.Text.ToUpper().Trim();
                        obj.COD_IDENTIFICACAO = lblCodIdentificacaoCod.Text.ToUpper().Trim();
                        obj.NOM_PARTICIP = lblNomePartcipanteCod.Text.ToUpper().Trim();
                        obj.COD_UNIMED = lblCodControleUnimedCod.Text.ToUpper().Trim();
                        obj.MOVIMENTACAO = lblMovimentacaoCod.Text.ToUpper().Trim();
                        obj.COD_PLANO_CESP = lblCodigoCespCod.Text.ToUpper().Trim();
                        res = bll.Inserir(obj);

                    }

                }
            }

            try
            {
                if (InicializaRelatorioDet(txtDatInicio.Text, txtDatFim.Text))
                {
                    //Download Detalhado
                    ArquivoDownload adRelDetPdf = new ArquivoDownload();
                    adRelDetPdf.nome_arquivo = relatorio_nomeDet + ".pdf";
                    adRelDetPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelDetPdf.nome_arquivo;
                    adRelDetPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelDetPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelDetPdf.nome_arquivo)] = adRelDetPdf;
                    string fullUrlDet = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelDetPdf.nome_arquivo);
                    AdicionarAcesso(fullUrlDet);
                    AbrirNovaAba(upUpdatePanel, fullUrlDet, adRelDetPdf.nome_arquivo);
                }
                if (InicializaRelatorioResumo(txtDatInicio.Text, txtDatFim.Text))
                {
                    //Download Resumo
                    ArquivoDownload adRelResumoPdf = new ArquivoDownload();
                    adRelResumoPdf.nome_arquivo = relatorio_nomeResumo + ".pdf";
                    adRelResumoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelResumoPdf.nome_arquivo;
                    adRelResumoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelResumoPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelResumoPdf.nome_arquivo)] = adRelResumoPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelResumoPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(upUpdatePanel, fullUrl, adRelResumoPdf.nome_arquivo);
                }


                if (InicializaRelatorioPlano(txtDatInicio.Text, txtDatFim.Text))
                {
                    //Download Plano
                    ArquivoDownload adRelPlanoPdf = new ArquivoDownload();
                    adRelPlanoPdf.nome_arquivo = relatorio_nomePlano + ".pdf";
                    adRelPlanoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelPlanoPdf.nome_arquivo;
                    adRelPlanoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelPlanoPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelPlanoPdf.nome_arquivo)] = adRelPlanoPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelPlanoPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(upUpdatePanel, fullUrl, adRelPlanoPdf.nome_arquivo);
                }

                if (InicializaRelatorioMov(txtDatInicio.Text, txtDatFim.Text))
                {
                    //Download Movimentação
                    ArquivoDownload adRelMovPdf = new ArquivoDownload();
                    adRelMovPdf.nome_arquivo = relatorio_nome + ".pdf";
                    adRelMovPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelMovPdf.nome_arquivo;
                    adRelMovPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelMovPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelMovPdf.nome_arquivo)] = adRelMovPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelMovPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(upUpdatePanel, fullUrl, adRelMovPdf.nome_arquivo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

                //print
            }



            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Relatórios Gerados com Sucesso");
                list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
                dt = list.ToDataTable();
                grdMemorialUnimed.AllowPaging = true;
                grdMemorialUnimed.DataSource = dt;
                grdMemorialUnimed.DataBind();
                RePopulateCheckBoxes();
            }
        }

        protected void btnGeraAprova_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {

            MemorialCalculoUnimedBLL bll = new MemorialCalculoUnimedBLL();
            double valorTotalCartao = 0;
            foreach (GridViewRow row in grdMemorialUnimed.Rows)
            {
                var chkBox = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

                if (chkBox.Checked)
                {
                    PersistRowIndex(container.DataItemIndex);
                }
                else
                {
                    RemoveRowIndex(container.DataItemIndex);

                }

            }

            List<MemorialCalculoUnimedDAL.CAD_TBL_CONTROLEUNIMED_view2> list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
            DataTable dt = list.ToDataTable();
            grdMemorialUnimed.AllowPaging = false;
            grdMemorialUnimed.DataSource = dt;
            grdMemorialUnimed.DataBind();

            for (int c = 0; c < SelectedMemorialIndex.Count; c++)
            {
                valorTotalCartao = valorTotalCartao + Convert.ToDouble(dt.Rows[SelectedMemorialIndex[c]][12]);
            }
            RePopulateCheckBoxes();

            lblValorTotal.Text = valorTotalCartao.ToString("N2");


            int i = grdMemorialUnimed.Rows.Cast<GridViewRow>()
                    .Count(r => ((CheckBox)r.FindControl("chkSelect")).Checked);


            lblQtdCartoes.Text = i.ToString();


            list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
            dt = list.ToDataTable();
            grdMemorialUnimed.AllowPaging = true;
            grdMemorialUnimed.DataSource = dt;
            grdMemorialUnimed.DataBind();
            RePopulateCheckBoxes();


        }

        #region .: Eventos Pop Up :.

        protected void btnConfirmaGeracao_Click(object sender, EventArgs e)
        {
            MemorialCalculoUnimedBLL bll = new MemorialCalculoUnimedBLL();
            FUN_TBL_MEMORIAL_UNIMED obj = new FUN_TBL_MEMORIAL_UNIMED();
            FUN_TBL_MEMORIAL_UNIMED_HIST objHist = new FUN_TBL_MEMORIAL_UNIMED_HIST();
            Resultado res = new Resultado();
            Resultado resUpdate = new Resultado();
            Resultado resHist = new Resultado();
            bll.Delete();//zera tabela


            foreach (GridViewRow row in grdMemorialUnimed.Rows)
            {
                var chkBox = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

                if (chkBox.Checked)
                {
                    PersistRowIndex(container.DataItemIndex);
                }
                else
                {
                    RemoveRowIndex(container.DataItemIndex);

                }

            }

            List<MemorialCalculoUnimedDAL.CAD_TBL_CONTROLEUNIMED_view2> list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
            DataTable dt = list.ToDataTable();
            grdMemorialUnimed.AllowPaging = false;
            grdMemorialUnimed.DataSource = dt;
            grdMemorialUnimed.DataBind();
            RePopulateCheckBoxes();

            foreach (GridViewRow row in grdMemorialUnimed.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

                    if (chkSelect.Checked == true)
                    {

                        Label lblDatMovimentacaoCod = (Label)row.FindControl("lblDatMovimentacao");
                        Label lblCodEmpresaCod = (Label)row.FindControl("lblCodEmpresa");
                        Label lblNumMatriculaCod = (Label)row.FindControl("lblNumMatricula");
                        Label lblSubCod = (Label)row.FindControl("lblSub");
                        Label lblCodIdentificacaoCod = (Label)row.FindControl("lblCodIdentificacao");
                        Label lblNomePartcipanteCod = (Label)row.FindControl("lblNome");
                        Label lblCodControleUnimedCod = (Label)row.FindControl("lblCodUnimed");
                        Label lblMovimentacaoCod = (Label)row.FindControl("lblMovimentacao");
                        Label lblCodigoCespCod = (Label)row.FindControl("lblCodigoCesp");
                        Label lblValor = (Label)row.FindControl("lblValorCI");


                        obj.DAT_GERACAO = Convert.ToDateTime(lblDatMovimentacaoCod.Text);
                        obj.COD_EMPRS = short.Parse(lblCodEmpresaCod.Text.ToUpper().Trim());
                        obj.NUM_MATRICULA = Convert.ToInt32(lblNumMatriculaCod.Text.ToUpper().Trim());
                        obj.SUB_MATRICULA = lblSubCod.Text.ToUpper().Trim();
                        obj.COD_IDENTIFICACAO = lblCodIdentificacaoCod.Text.ToUpper().Trim();
                        obj.NOM_PARTICIP = lblNomePartcipanteCod.Text.ToUpper().Trim();
                        obj.COD_UNIMED = lblCodControleUnimedCod.Text.ToUpper().Trim();
                        obj.MOVIMENTACAO = lblMovimentacaoCod.Text.ToUpper().Trim();
                        obj.COD_PLANO_CESP = lblCodigoCespCod.Text.ToUpper().Trim();

                        res = bll.Inserir(obj);

                        objHist.DAT_GERACAO = Convert.ToDateTime(lblDatMovimentacaoCod.Text);
                        objHist.COD_EMPRS = short.Parse(lblCodEmpresaCod.Text.ToUpper().Trim());
                        objHist.NUM_MATRICULA = Convert.ToInt32(lblNumMatriculaCod.Text.ToUpper().Trim());
                        objHist.SUB_MATRICULA = lblSubCod.Text.ToUpper().Trim();
                        objHist.COD_IDENTIFICACAO = lblCodIdentificacaoCod.Text.ToUpper().Trim();
                        objHist.NOM_PARTICIP = lblNomePartcipanteCod.Text.ToUpper().Trim();
                        objHist.COD_UNIMED = lblCodControleUnimedCod.Text.ToUpper().Trim();
                        objHist.MOVIMENTACAO = lblMovimentacaoCod.Text.ToUpper().Trim();
                        objHist.COD_PLANO_CESP = lblCodigoCespCod.Text.ToUpper().Trim();
                        objHist.DAT_PAGAMENTO = Convert.ToDateTime(txtDatPagtoPopUp.Text);
                        objHist.VALOR = Convert.ToDecimal(lblValor.Text);

                        resHist = bll.InserirHist(objHist);

                        resUpdate = bll.Update(obj.COD_EMPRS, obj.NUM_MATRICULA, obj.SUB_MATRICULA,obj.MOVIMENTACAO,obj.DAT_GERACAO);

                    }

                }
            }

            if (InicializaRelatorioMov(txtDatInicio.Text, txtDatFim.Text))
            {
                //Download Movimentação
                ArquivoDownload adRelMovPdf = new ArquivoDownload();
                adRelMovPdf.nome_arquivo = relatorio_nome + ".pdf";
                adRelMovPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelMovPdf.nome_arquivo;
                adRelMovPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adRelMovPdf.caminho_arquivo);

                Session[ValidaCaracteres(adRelMovPdf.nome_arquivo)] = adRelMovPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelMovPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upUpdatePanel, fullUrl, adRelMovPdf.nome_arquivo);
            }

            if (InicializaRelatorioDet(txtDatInicio.Text, txtDatFim.Text))
            {
                //Download Detalhado
                ArquivoDownload adRelDetPdf = new ArquivoDownload();
                adRelDetPdf.nome_arquivo = relatorio_nomeDet + ".pdf";
                adRelDetPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelDetPdf.nome_arquivo;
                adRelDetPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adRelDetPdf.caminho_arquivo);

                Session[ValidaCaracteres(adRelDetPdf.nome_arquivo)] = adRelDetPdf;
                string fullUrlDet = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelDetPdf.nome_arquivo);
                AdicionarAcesso(fullUrlDet);
                AbrirNovaAba(upUpdatePanel, fullUrlDet, adRelDetPdf.nome_arquivo);
            }

            if (InicializaRelatorioPlano(txtDatInicio.Text, txtDatFim.Text))
            {
                //Download Plano
                ArquivoDownload adRelPlanoPdf = new ArquivoDownload();
                adRelPlanoPdf.nome_arquivo = relatorio_nomePlano + ".pdf";
                adRelPlanoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelPlanoPdf.nome_arquivo;
                adRelPlanoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adRelPlanoPdf.caminho_arquivo);

                Session[ValidaCaracteres(adRelPlanoPdf.nome_arquivo)] = adRelPlanoPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelPlanoPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upUpdatePanel, fullUrl, adRelPlanoPdf.nome_arquivo);
            }


            if (InicializaRelatorioResumo(txtDatInicio.Text, txtDatFim.Text))
            {
                //Download Resumo
                ArquivoDownload adRelResumoPdf = new ArquivoDownload();
                adRelResumoPdf.nome_arquivo = relatorio_nomeResumo + ".pdf";
                adRelResumoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelResumoPdf.nome_arquivo;
                adRelResumoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adRelResumoPdf.caminho_arquivo);

                Session[ValidaCaracteres(adRelResumoPdf.nome_arquivo)] = adRelResumoPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelResumoPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upUpdatePanel, fullUrl, adRelResumoPdf.nome_arquivo);
            }

            if (res.Ok && resUpdate.Ok && resHist.Ok)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Relatórios Gerados com Sucesso");
                list = bll.GetWhere(ddlNomeUnimed.SelectedValue, Util.String2Date(txtDatInicio.Text), Util.String2Date(txtDatFim.Text)).ToList();
                dt = list.ToDataTable();
                grdMemorialUnimed.AllowPaging = true;
                grdMemorialUnimed.DataSource = dt;
                grdMemorialUnimed.DataBind();
                txtDatPagtoPopUp.Text = "";
            }
        }

        protected void btnCancelarPopUp_Click(object sender, EventArgs e)
        {
            txtDatPagtoPopUp.Text = "";
        }

        #endregion


        #endregion

        #region .: Métodos :.

        private bool InicializaRelatorioMov(string datIni, string datFim)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_simples;
            relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
            relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });
            //relatorio.parametros.Add(new Parametro() { parametro = "codUnimed", valor = codUnimed });

            //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });


            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;

            return true;
        }

        private bool InicializaRelatorioDet(string datIni, string datFim)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_detalhado;
            relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
            relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });
            //relatorio.parametros.Add(new Parametro() { parametro = "codUnimed", valor = codUnimed });

            //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });


            Session[relatorio_nomeDet] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nomeDet;

            return true;
        }

        private bool InicializaRelatorioResumo(string datIni, string datFim)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_resumo;
            relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
            relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });
            //relatorio.parametros.Add(new Parametro() { parametro = "codUnimed", valor = codUnimed });

            //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });


            Session[relatorio_nomeResumo] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nomeResumo;

            return true;
        }

        private bool InicializaRelatorioPlano(string datIni, string datFim)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_plano;
            relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
            relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });
            //relatorio.parametros.Add(new Parametro() { parametro = "codUnimed", valor = codUnimed });

            //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });


            Session[relatorio_nomePlano] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nomePlano;

            return true;
        }

        private void PersistRowIndex(int index)
        {

            if (!SelectedMemorialIndex.Exists(i => i == index))
            {

                SelectedMemorialIndex.Add(index);

            }

        }

        private void RemoveRowIndex(int index)
        {
            SelectedMemorialIndex.Remove(index);
        }

        private List<Int32> SelectedMemorialIndex
        {

            get
            {

                if (ViewState["SELECTED_MEMORIAL_INDEX"] == null)
                {

                    ViewState["SELECTED_MEMORIAL_INDEX"] = new List<Int32>();

                }



                return (List<Int32>)ViewState["SELECTED_MEMORIAL_INDEX"];

            }

        }

        private void RePopulateCheckBoxes()
        {

            foreach (GridViewRow row in grdMemorialUnimed.Rows)
            {

                var chkBox = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

                if (SelectedMemorialIndex != null)
                {

                    if (SelectedMemorialIndex.Exists(i => i == container.DataItemIndex))
                    {

                        chkBox.Checked = true;

                    }
                }
            }
        }

        #endregion

    }
}