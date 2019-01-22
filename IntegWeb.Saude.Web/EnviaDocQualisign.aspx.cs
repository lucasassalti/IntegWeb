using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Configuration;

namespace IntegWeb.Saude.Web
{

    public partial class EnviaDocQualisign : BasePage
    {

        string ws_ambiente = (ConfigurationManager.AppSettings["Config"] ?? String.Empty)=="P" ? "qswsdeSoap" : "qswsdeSoap_HOMOLOG";
        //string ws_sessao = (ConfigurationManager.AppSettings["Config"] ?? String.Empty) == "P" ? "@hfdjFhfr4" : "@feNxeHIxj";
        string ws_sessao = (ConfigurationManager.AppSettings["Config"] ?? String.Empty) == "P" ? "@hfdjFhfr4" : "@WREKZ6nUs";        

        protected void Page_Load(object sender, EventArgs e)
        {
            TbUpload_Mensagem.Visible = false;

            if (!IsPostBack)
            {
                grdDocsEnviados.Sort("DTH_INCLUSAO", SortDirection.Ascending);
                Session["lst_uploads"] = null;

                var user = (ConectaAD)Session["objUser"];

                UserEngineBLL ueBLL = new UserEngineBLL();
                if (user != null)
                {
                    switch (user.login.ToUpper())
                    {
                        case "F02442": //RICARDO LUIZ DOS REIS
                            txtCpfRepres.Text = "17015416883";
                            break;
                        case "F02654": // ELISANGELA SANTANA DO NASCIMENTO
                            txtCpfRepres.Text = "40211009890";
                            break;
                        case "F02213":  //Ronis A. Toni
                        default:
                            txtCpfRepres.Text = "17636905809";
                            break;
                       

                    }
                }
            }

        }

        protected void btnAtualizarStatus_Click(object sender, EventArgs e)
        {
            Resultado res = new Resultado();
            try
            {
                WsQualiSignBLL wsQBll = new WsQualiSignBLL();
                res = wsQBll.AtualizarStatusDocumentos(ws_sessao, ws_ambiente);

                if (res.Ok)
                {
                    MostraMensagem(TbUpload_Mensagem, res.Mensagem, "n_ok");
                    grdDocsEnviados.DataBind();
                }
                else
                {
                    throw new Exception(res.Mensagem);
                }

            }
            catch (Exception ex)
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Erro na tentativa de atualizar os status dos documentos.\\nMotivo:" + Util.GetInnerException(ex), "n_error");
            }

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdDocsEnviados.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtArqRef.Text = String.Empty;
            txtNomeArquivo.Text = String.Empty;
            txtDtIni.Text = String.Empty;
            txtDtFim.Text = String.Empty;
            ddlStatus.SelectedValue = String.Empty;
        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            List<ArquivoUpload> _lst_uploads = null;
            Resultado res = new Resultado();

            try
            {
                var user = (ConectaAD)Session["objUser"];
                //rAgrupa
                if (Session != null && Session["lst_uploads"] != null)
                {
                    //ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
                    _lst_uploads = (List<ArquivoUpload>)Session["lst_uploads"];
                    WsQualiSignBLL wsQBll = new WsQualiSignBLL();
                    res = wsQBll.ProcessaDocumentos(_lst_uploads, ws_sessao, ws_ambiente, Util.LimparCPF(txtCpfRepres.Text), (user != null) ? user.login : "DESENV", DateTime.Now);

                    if (res.Ok)
                    {
                        MostraMensagem(TbUpload_Mensagem, "Lote gerado e enviado com sucesso!", "n_ok");
                    }
                    else
                    {
                        throw new Exception(res.Mensagem);
                    }

                }
            }
            catch (Exception ex)
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! O arquivo não pôde ser processado. Motivo:\\n" + Util.GetInnerException(ex), "n_error");
            }
            finally
            {
                FileUploadControl.FileContent.Dispose();
                FileUploadControl.FileContent.Flush();
                if (FileUploadControl.PostedFile != null)
                {
                    FileUploadControl.PostedFile.InputStream.Flush();
                    FileUploadControl.PostedFile.InputStream.Close();
                }
                LimparUploads(_lst_uploads);
            }
        }

        private void LimparUploads(List<ArquivoUpload> _lst_uploads)
        {
            if (_lst_uploads != null && _lst_uploads.Count > 0)
            {
                for (int i = _lst_uploads.Count - 1; i >= 0; i--)
                {
                    if (_lst_uploads[i].processado)
                    {
                        File.Delete(_lst_uploads[i].caminho_arquivo);
                        _lst_uploads.RemoveAt(i);
                    }
                }
                if (_lst_uploads.Count == 0)
                {
                    //chkSobreporTodos.Checked = false;
                    grdDocsEnviados.DataBind();
                    //grdArqEnviadosPorEmpresa.DataBind();
                    Session["lst_uploads"] = null;
                }
            }
        }

        protected void grdDocsEnviados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == 0)
                {
                    Label lstCriticas = (Label)e.Row.FindControl("lstAcoes");
                    int COD_ARQ_PAT = (int)grdDocsEnviados.DataKeys[e.Row.RowIndex].Value;
                    //ArqPatrocinadoraBLL ArqPatBLL = new ArqPatrocinadoraBLL();

                }
            }

        }
   }
}