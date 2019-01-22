using System;
using System.Web.UI;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Entidades;

namespace IntegWeb.Previdencia.Web
{
    public partial class RelatorioWeb : BaseReportPage
    {
        RelatorioBLL rel = new RelatorioBLL();
        Relatorio relatorio = null;
        string exibe_alerta_verifica;
        bool popup = false;
        bool PromptParam = true;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["Popup"] != null)
                popup = Boolean.Parse(Request.QueryString["Popup"]);

            if (popup)
                MasterPageFile = "~/Popup.Master";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string relatorio_nome = Request.QueryString["Relatorio_nome"];
            if (!String.IsNullOrEmpty(relatorio_nome))
            {

                if (Request.QueryString["Alert"] != null)
                    exibe_alerta_verifica = Request.QueryString["Alert"];
                if (Request.QueryString["PromptParam"] != null)
                    PromptParam = Boolean.Parse(Request.QueryString["PromptParam"]);
                btnRelatorio.Visible = PromptParam;
                tabelaPagina.Visible = PromptParam;
                //Relatorio res = null;
                ReportCrystal.RelatorioID = relatorio_nome;
                if (Session[relatorio_nome] != null && (IsPostBack || !PromptParam))
                {
                    relatorio = (Relatorio)Session[relatorio_nome];
                    //ReportCrystal.GeraRelatorio();
                    ReportCrystal.VisualizaRelatorio();
                }
                else
                {
                    relatorio = rel.Listar(relatorio_nome);
                    Session[relatorio_nome] = relatorio;
                    NomeRelatorio.Text = relatorio.titulo;
                    GeraHtml(table, relatorio);
                }
                NomeRelatorio.Text = "Relatorio " + relatorio.titulo;
            }
            else
                MostraMensagemTela(this.Page, "Atenção \\n\\n Para acessar o relatório é necessário estar logado.");

        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {

            try
            {
                Resultado res = ValidaCampos(relatorio);
                if (res.Ok)
                {
                    ReportCrystal.VisualizaRelatorio();
                }
                else
                {
                    throw new Exception(res.Mensagem);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
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
    }
}

