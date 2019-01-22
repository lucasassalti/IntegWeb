using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Financeira.Aplicacao.DAL.Tesouraria;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web
{
    public partial class AberturaFinanceira : BasePage
    {
        #region .: Propriedades :.

        Relatorio relatorio = new Relatorio();

        // Relatórios por Empresa/Plano
        string relatorio_titulo = "Relatório Abertura Financeira";
        string relatorio_val_participante = @"~/Relatorios/Rel_Abertura_Financeira.rpt";
        string relatorio_val_credenciado = @"~/Relatorios/Rel_Abertura_Financeira_Cred.rpt";
        string relatorio_nome_part = "AberturaFinanceiraPart";
        string relatorio_nome_cred = "AberturaFinanceiraCred";

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            AberturaFinanceiraDAL Dal = new AberturaFinanceiraDAL();

            int i = Dal.GetDataCount(Convert.ToInt32(txtMes.Text), Convert.ToInt32(txtAno.Text));

            if (i > 0)
            {

                grdAberturaFinanceira.DataBind();
                grdAberturaFinanceira.Visible = true;
                btnGerarRelatorio.Visible = true;

            }
            else
            {
                Resultado res = Dal.InsereResumoAberturaFinanceira(txtMes.Text, txtAno.Text);

                if (res.Ok)
                {
                    grdAberturaFinanceira.DataBind();
                    grdAberturaFinanceira.Visible = true;
                    btnGerarRelatorio.Visible = true;
                }

            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtMes.Text = "";
            txtAno.Text = "";
            grdAberturaFinanceira.Visible = false;
            btnGerarRelatorio.Visible = false;
            grdAberturaFinanceira.DataBind();
        }


        protected void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            AberturaFinanceiraDAL Dal = new AberturaFinanceiraDAL();

            //List<FIN_TBL_RES_ABERT_FINANCEIRA_view2> list = Dal.GetDataExportar(Util.String2Int32(txtMes.Text), Util.String2Int32(txtAno.Text));
            List<FIN_TBL_RES_ABERT_FINANCEIRA_view3> list = Dal.GetDataConsolidado(Util.String2Int32(txtMes.Text), Util.String2Int32(txtAno.Text));

            DataTable dt = list.ToDataTable();

            //Download do Excel 
            ArquivoDownload adXlsResAberturaFin = new ArquivoDownload();
            adXlsResAberturaFin.nome_arquivo = "Res_Abertura_Fin_" + txtMes.Text + "_" + txtAno.Text + ".xls";
            //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
            adXlsResAberturaFin.dados = dt;
            Session[ValidaCaracteres(adXlsResAberturaFin.nome_arquivo)] = adXlsResAberturaFin;
            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsResAberturaFin.nome_arquivo);
            //AdicionarAcesso(fUrl);
            AbrirNovaAba(UpdatePanel, fUrl, adXlsResAberturaFin.nome_arquivo);
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (ReportCrystal != null)
            {
                ReportCrystal.ID = null;
                ReportCrystal = null;
            }
        }

        protected void imgAprovacao_Click(object sender, ImageClickEventArgs e)
        {
            AberturaFinanceiraDAL dal = new AberturaFinanceiraDAL();
            ImageButton imgAprovacao = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgAprovacao.NamingContainer;
            int numRegEmpresa = 0;
            int numRegAprovado = 0;
            var user = (ConectaAD)Session["objUser"];

            Resultado res = dal.AprovacaoAberturaFinanceira(Convert.ToInt32((row.FindControl("lblIdReg") as Label).Text), "SYS_FUNCESP");//user.login);

            numRegEmpresa = dal.GetDataCountEmpresa(Convert.ToInt32((row.FindControl("lblEmpresa") as Label).Text), Convert.ToInt32(txtMes.Text), Convert.ToInt32(txtAno.Text));
            numRegAprovado = dal.GetDataCountAprovados(Convert.ToInt32((row.FindControl("lblEmpresa") as Label).Text), Convert.ToInt32(txtMes.Text), Convert.ToInt32(txtAno.Text));

            if (res.Ok)
            {
                if (numRegAprovado == numRegEmpresa)
                {

                    if (InicializaRelatorioValParticipante(((row.FindControl("lblEmpresa") as Label).Text), txtMes.Text.PadLeft(2,'0'), txtAno.Text))
                    {
                        ArquivoDownload adRelValPartExcel = new ArquivoDownload();
                        adRelValPartExcel.nome_arquivo = relatorio_nome_part + "_" + (row.FindControl("lblNomEmpresa") as Label).Text.Replace(" ","_") + "_" + txtMes.Text.PadLeft(2, '0') +  txtAno.Text + ".xls";
                        adRelValPartExcel.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelValPartExcel.nome_arquivo;
                        adRelValPartExcel.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioExcel(adRelValPartExcel.caminho_arquivo);

                        Session[ValidaCaracteres(adRelValPartExcel.nome_arquivo)] = adRelValPartExcel;
                        string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelValPartExcel.nome_arquivo);
                        AdicionarAcesso(fullUrl);
                        AbrirNovaAba(UpdatePanel, fullUrl, adRelValPartExcel.nome_arquivo);
                    }

                    if (InicializaRelatorioValCredenciado(((row.FindControl("lblEmpresa") as Label).Text), txtMes.Text.PadLeft(2, '0'), txtAno.Text))
                    {
                        ArquivoDownload adRelValCredExcel = new ArquivoDownload();
                        adRelValCredExcel.nome_arquivo = relatorio_nome_cred + "_" + (row.FindControl("lblNomEmpresa") as Label).Text.Replace(" ", "_") + "_" + txtMes.Text.PadLeft(2, '0') + txtAno.Text + ".xls";
                        adRelValCredExcel.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelValCredExcel.nome_arquivo;
                        adRelValCredExcel.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioExcel(adRelValCredExcel.caminho_arquivo);

                        Session[ValidaCaracteres(adRelValCredExcel.nome_arquivo)] = adRelValCredExcel;
                        string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelValCredExcel.nome_arquivo);
                        AdicionarAcesso(fullUrl);
                        AbrirNovaAba(UpdatePanel, fullUrl, adRelValCredExcel.nome_arquivo);

                    }

                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Todos os Movimentos da Empresa: " + (row.FindControl("lblNomEmpresa") as Label).Text + " Aprovados , Relatórios Gerados");
                    grdAberturaFinanceira.DataBind();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Aprovado com Sucesso");
                    grdAberturaFinanceira.DataBind();
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                grdAberturaFinanceira.DataBind();
            }

        }


        #endregion

        #region .: Métodos :.

        private bool InicializaRelatorioValParticipante(string codEmp, string mes, string ano)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_val_participante;
            relatorio.parametros.Add(new Parametro() { parametro = "codEmp", valor = codEmp });
            relatorio.parametros.Add(new Parametro() { parametro = "mes", valor = mes });
            relatorio.parametros.Add(new Parametro() { parametro = "ano", valor = ano });

            Session[relatorio_nome_part] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_part;
            return true;
        }

        private bool InicializaRelatorioValCredenciado(string codEmp, string mes, string ano)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_val_credenciado;
            relatorio.parametros.Add(new Parametro() { parametro = "codEmp", valor = codEmp });
            relatorio.parametros.Add(new Parametro() { parametro = "Mes", valor = mes });
            relatorio.parametros.Add(new Parametro() { parametro = "Ano", valor = ano });

            Session[relatorio_nome_cred] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_cred;
            return true;
        }

        #endregion

        





    }
}