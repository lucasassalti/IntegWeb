using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Configuration;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Entidades.Framework;

namespace IntegWeb.Previdencia.Web
{

    public partial class ArquivoPatrocinadoraEnv : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            init_SSO_Session();

            //TbUpload_Mensagem.Visible = false;
            if (trFiltro.Visible)
            {
                PanelUpload.DefaultButton = btFiltrar.UniqueID;
            }

            if (!IsPostBack)
            {
                grdArqEnviados.Sort("DTH_INCLUSAO", SortDirection.Ascending);

                if (sso_session != null && sso_session.ListaGrupos != null)
                {
                    List<string> lsGrupos = sso_session.ListaGrupos.Where(g => g.ToUpper().Contains("DOWN")).ToList();
                    //foreach (string grupo in lsGrupos)
                    //{
                    //    ddlGrupoNovo.Items.Add(new ListItem(grupo, grupo));
                    //}
                    hidGrupos.Value = String.Join(",", lsGrupos);
                    ArqPatrocinadoraEnvioBLL ArqEnvioBLL = new ArqPatrocinadoraEnvioBLL();
                    CarregaDropDowList(ddlTipo, ArqEnvioBLL.GetTipoEnvioDdl().ToList<object>(), "DCR_ARQ_ENVIO_TIPO", "COD_ARQ_ENVIO_TIPO");

                    ddlTipo.Items[0].Text = "<TODOS>";
                }
            }


            //ScriptManager.RegisterStartupScript(upArqPatrocinadoras,
            //       upArqPatrocinadoras.GetType(),
            //       "script",
            //       "chkSelect_click();",
            //       true);

        }

        protected void rExibir_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["rExibir.SelectedValue"] = rExibir.SelectedValue;
            //trFiltro.Style.Remove("style");
            switch (rExibir.SelectedValue)
            {
                case "0":
                    trFiltro.Visible = false;
                    txtDatIni.Text = "";
                    break;
                case "1":
                    trFiltro.Visible = false;
                    break;
                case "2":
                    PanelUpload.DefaultButton = btFiltrar.UniqueID;
                    trFiltro.Visible = true;                    
                    break;
            }            
            Exibir();
        }

        protected void btnExibir_Click(object sender, EventArgs e)
        {
            Exibir();
        }

        protected void Exibir()
        {
            if (!grdArqEnviados.Visible)
            {
                if (String.IsNullOrEmpty(rExibir.SelectedValue))
                {
                    rExibir.SelectedValue = "1";
                }
                btnExibir.Visible = false;
            }

            grdArqEnviados.Visible = true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrocaArquivos.aspx");
        }

        protected void btFiltrar_Click(object sender, ImageClickEventArgs e)
        {
            PanelUpload.DefaultButton = btFiltrar.UniqueID;
            grdArqEnviados.DataBind();
        }

        protected void btLimparFiltro_Click(object sender, ImageClickEventArgs e)
        {
            rExibir.SelectedValue = "0";
            txtDatIni.Text = "";
            txtDatFim.Text = "";
            txtAno.Text = "";
            txtMes.Text = "";
            txtExtensao.Text = "";
            txtReferencia.Text = "";
            ddlTipo.SelectedValue = "0";
            trFiltro.Visible = false;
            grdArqEnviados.DataBind();
        }

        protected void grdArqEnviados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int iCOD_ARQ_ENVIO = 0;

            switch (e.CommandName)
            {
                case "Download":
                    string[] Arg = e.CommandArgument.ToString().Split(',');                    
                    iCOD_ARQ_ENVIO = (Arg.Length) > 0 ? Convert.ToInt32(Arg[0]) : 0;
                    ArqPatrocinadoraEnvioBLL ArqEnvioBLL = new ArqPatrocinadoraEnvioBLL();
                    
                    ArquivoDownload adPdf = new ArquivoDownload();
                    adPdf = ArqEnvioBLL.GetArqDownload(iCOD_ARQ_ENVIO);
                    string session_id = ValidaCaracteres(adPdf.nome_arquivo) + iCOD_ARQ_ENVIO;

                    Session[session_id] = adPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + session_id;
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(upArqPatrocinadoras, fullUrl, adPdf.nome_arquivo);

                    break;
            }
        }

    }
}