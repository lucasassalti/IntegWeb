using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades.Carga;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web
{
    public partial class Importacao : BasePage
    {
        CargaDadosBLL cdBLL = new CargaDadosBLL();

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string carga_nome = "Rel_Adesao_Nosso_Plano";
                string carga_nome = Request.QueryString["Carga_nome"];
                if (!String.IsNullOrEmpty(carga_nome))
                {
                    CargaDados carga = cdBLL.Listar(carga_nome);
                    Session["ObjCarga"] = carga;
                    //GeraHtml(res);
                }
                else
                    Response.Redirect("index.aspx");

            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            divselect.Visible = false;

            if (FileUploadControl.HasFile)
            {

                if (//FileUploadControl.PostedFile.ContentType.Equals("application/vnd.ms-excel") ||  //formato 2003
                   FileUploadControl.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")) // formato superior 2003
                {

                    try
                    {

                        string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                        string UploadFilePath = Server.MapPath("UploadFile\\");
                        string path = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                        if (!Directory.Exists(UploadFilePath))
                        {
                            Directory.CreateDirectory(UploadFilePath);
                        }

                        FileUploadControl.SaveAs(path);
                        BasePage obj = new BasePage();
                        //Lê o Excel e converte para DataSet
                        DataTable dt = obj.ReadExcelFile(path);

                        bool ret = false;
                        if (Session["ObjCarga"] != null)
                        {
                            ret = cdBLL.ImportarDados((CargaDados)Session["ObjCarga"], dt);
                        }

                        if (ret)
                        {
                            ListaTodos();
                            MostraMensagemTelaUpdatePanel(upSimulacao, " Arquivo Carregado com sucesso\\n\\nQuantidade de linhas importados " + dt.Rows.Count + " registros");
                        }

                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
                    }
                    finally
                    {
                        FileUploadControl.FileContent.Dispose();
                        FileUploadControl.FileContent.Flush();
                        FileUploadControl.PostedFile.InputStream.Flush();
                        FileUploadControl.PostedFile.InputStream.Close();
                    }

                }
                else
                {

                    MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nCarregue apenas arquivos Excel 2007 (.xlsx) ou superior!");
                }

            }

            else
            {

                MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nSelecione um Arquivo para continuar!");
            }

        }

        protected void grdSim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdSim"] != null)
            {
                grdSim.PageIndex = e.NewPageIndex;
                grdSim.DataSource = ViewState["grdSim"];
                grdSim.DataBind();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ListaTodos();
        }

        #endregion

        #region Métodos

        private void ListaTodos()
        {            
            DataTable dt = null;
            if (Session["ObjCarga"] != null)
            {
                dt = cdBLL.ListarDinamico((CargaDados)Session["ObjCarga"]);
            }
            divselect.Visible = true;
            ViewState["grdSim"] = dt;
            CarregarGridView(grdSim, dt);
        }

        #endregion
    }
}