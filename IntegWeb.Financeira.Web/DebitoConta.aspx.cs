using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Financeira.Aplicacao.BLL;
using IntegWeb.Financeira.Aplicacao.ENTITY;
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
using IntegWeb.Entidades.Framework;

namespace IntegWeb.Financeira.Web
{
    public partial class DebitoConta : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            TbConsulta_Mensagem.Visible = false;
            TbUpload_Mensagem.Visible = false;
            lkYes.Visible = false;

            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            if (FileUploadControl.HasFile)
            {

                if (FileUploadControl.PostedFile.ContentType.Equals("text/plain"))
                {

                    string path = "";

                    try
                    {

                        string filename = Path.GetFileName(FileUploadControl.FileName).ToString();
                        string[] name = filename.Split('.');
                        string UploadFilePath = Server.MapPath("UploadFile\\");
                        
                        path = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                        if (!Directory.Exists(UploadFilePath))
                        {
                            Directory.CreateDirectory(UploadFilePath);
                        }

                        FileUploadControl.SaveAs(path);
                        DataTable dt = ReadTextFile(path);

                        DebitoContaRetornoBLL bll = new DebitoContaRetornoBLL();
                        Resultado res = new Resultado();
                        //AAT_TBL_RET_DEB_CONTA ja_existe = bll.GetData(filename, "A");

                        //if (ja_existe == null)
                        //{
                            res = bll.DePara(dt, filename);
                        //}
                        //else
                        //{
                        //    lkYes.CommandArgument = filename;
                        //    lkYes.Visible = true;
                        //    MostraMensagem(TbUpload_Mensagem, "Este arquivo já foi importado anteriormente. Tem certeza que deseja importa-lo novamente?");
                        //}
          
                        if (res.Ok)
                        {
                            res = ConsolidaListaDebitoConta(filename);
                            MostraMensagem(TbUpload_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");                            
                        }

                    } 
                    catch (Exception ex)
                    {
                        MostraMensagem(TbUpload_Mensagem, "Atenção! O arquivo não pôde ser carregado. Motivo:\\n" + ex.Message,"n_error");
                    }
                    finally
                    {
                        FileUploadControl.FileContent.Dispose();
                        FileUploadControl.FileContent.Flush();
                        FileUploadControl.PostedFile.InputStream.Flush();
                        FileUploadControl.PostedFile.InputStream.Close();
                        File.Delete(path);
                    }

                }
                else
                {
                    MostraMensagem(TbUpload_Mensagem, "Atenção! Carregue apenas arquivos de retorno do banco");
                }

            }

            else
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Selecione um Arquivo para continuar");
            }

        }

        protected void lkYes_Click(object sender, EventArgs e)
        {
            ConsolidaListaDebitoConta(((LinkButton)sender).CommandArgument.ToString());
        }

        private Resultado ConsolidaListaDebitoConta(string filename)
        {
            DebitoContaRetornoBLL bll = new DebitoContaRetornoBLL();
            Resultado res = new Resultado();
            res = bll.ConsolidaListaDebitoConta(filename);
            CarregaDropDowList(ddlNomeArquivo, new DebitoContaRetornoBLL().GetListaDCR_NOM_ARQ());
            ddlNomeArquivo.SelectedValue = filename;
            ddlNomeArquivo.Items.Insert(1, new ListItem("<TODOS>", ""));
            CarregarDadosArquivo();
            grdDebitoContaRetorno.DataBind();
            ConsultarDebConta();
            return res;
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ConsultarDebConta();
        }

        private void ConsultarDebConta()
        {
            CarregaDropDowList(ddlNomeArquivo, new DebitoContaRetornoBLL().GetListaDCR_NOM_ARQ());
            ddlNomeArquivo.Items.Insert(1, new ListItem("<TODOS>", ""));
            pnlGridRetDebitoConta.Visible = true;
            grdDebitoContaRetorno.Sort("NUM_SEQ_LINHA", SortDirection.Descending); 
            grdDebitoConta.DataBind();
            btnExportarRet.Enabled = true;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPesqEmpresa.Text) &&
                String.IsNullOrEmpty(txtPesqMatricula.Text) &&
                String.IsNullOrEmpty(txtPesqRepresentante.Text) &&
                String.IsNullOrEmpty(txtPesqCpf.Text) &&
                String.IsNullOrEmpty(txtPesNome.Text))
            {
                MostraMensagem(TbConsulta_Mensagem, "Prencha um campo de pesquisa para continuar");
            }

            else grdDebitoConta.PageIndex = 0;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqEmpresa.Text = "";
            txtPesqMatricula.Text = "";
            txtPesqRepresentante.Text = "";
            txtPesqCpf.Text = "";
            txtPesNome.Text = "";             
            grdDebitoConta.PageIndex = 0;
            grdDebitoConta.EditIndex = -1;
            grdDebitoConta.ShowFooter = false; 
            grdDebitoConta.DataBind();
        }

        protected void ddlNomeArquivo_TextChanged(object sender, EventArgs e)
        {
            CarregarDadosArquivo();
        }

        private void CarregarDadosArquivo()
        {
            grdDebitoContaRetorno.PageIndex = 0;
            grdDebitoContaRetorno.EditIndex = -1;
            lblData.Text = "--/--/---";
            lblTotalLinhas.Text = "--";
            lblQtdErro.Text = "--";
            if (ddlNomeArquivo.SelectedValue != "0" && !String.IsNullOrEmpty(ddlNomeArquivo.SelectedValue))
            {
                DebitoContaRetornoBLL bll = new DebitoContaRetornoBLL();
                lblData.Text = bll.GetDtArquivo(ddlNomeArquivo.SelectedValue);
                lblTotalLinhas.Text = bll.GetDataCount(ddlNomeArquivo.SelectedValue, null, null).ToString();
                lblQtdErro.Text = bll.GetErros(ddlNomeArquivo.SelectedValue).ToString();
            }
        }
        //protected void btnConsultar2_Click(object sender, EventArgs e)
        //{
        //    grdDebitoContaRetorno.DataBind();

        //}


        protected void btnPesquisar2_Click(object sender, EventArgs e)
        {
            grdDebitoContaRetorno.DataBind();

        }

        protected void grdDebitoContaRetorno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == 0)
                {
                    Label lstCriticas = (Label)e.Row.FindControl("lstCriticas");
                    AAT_TBL_RET_DEB_CONTA retDebConta = (AAT_TBL_RET_DEB_CONTA)e.Row.DataItem;
                    string strCrit = "";
                    foreach (AAT_TBL_RET_DEB_CONTA_CRITICAS crit in retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS)
                    {
                        strCrit += String.Format("<li>{0} - {1}</li>", crit.COD_CRITICA, crit.DCR_CRITICA);
                    }
                    lstCriticas.Text = String.Format("<ul style='margin: 0px;'>{0}</ul>", strCrit);

                    Label lblCritica = (Label)e.Row.FindControl("lblCritica");
                    retDebConta = (AAT_TBL_RET_DEB_CONTA)e.Row.DataItem;
                    strCrit = "";
                    AAT_TBL_RET_DEB_CONTA_CRITICAS crit2 = retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.FirstOrDefault();
                    if (crit2 != null)
                    {
                        strCrit += String.Format("{0} - {1}", crit2.COD_CRITICA, crit2.DCR_CRITICA);
                        lblCritica.Text = strCrit;
                    }

                }
            }

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {


            DebitoContaBLL bll = new DebitoContaBLL();
            DataTable dt = bll.ListarDadosParaExcel(Util.String2Short(txtPesqEmpresa.Text),
                                                    Util.String2Int32(txtPesqMatricula.Text),
                                                    Util.String2Int32(txtPesqRepresentante.Text),
                                                    Util.String2Int64(txtPesqCpf.Text),
                                                    txtPesNome.Text);

            //DataTable dt = bll.GetWhere(Util.String2Short(txtPesqEmpresa.Text),
            //                            Util.String2Int32(txtPesqMatricula.Text),
            //                            Util.String2Int32(txtPesqRepresentante.Text),
            //                            Util.String2Int64(txtPesqCpf.Text),
            //                            txtPesNome.Text);


            //Download do Excel 
            ArquivoDownload adXlsDbtConta = new ArquivoDownload();
            adXlsDbtConta.nome_arquivo = "Arquivo_Exportado.xls";
            //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
            adXlsDbtConta.dados = dt;
            Session[ValidaCaracteres(adXlsDbtConta.nome_arquivo)] = adXlsDbtConta;
            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsDbtConta.nome_arquivo);
            //AdicionarAcesso(fUrl);
            AbrirNovaAba(upUpdatePanel, fUrl, adXlsDbtConta.nome_arquivo);

        }

        protected void btnExportarRet_Click(object sender, EventArgs e)
        {
            DebitoContaRetornoBLL bll = new DebitoContaRetornoBLL();
            DataTable dt = bll.ListarDadosParaExcel(ddlNomeArquivo.SelectedValue,
                                                    null,
                                                    ddlTipoRegistro.SelectedValue,
                                                    chkPesqComCritica.Checked);

            //Download do Excel 
            ArquivoDownload adXlsDbtConta = new ArquivoDownload();
            adXlsDbtConta.nome_arquivo = "Arquivo_Retorno.xls";
            //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
            adXlsDbtConta.dados = dt;
            Session[ValidaCaracteres(adXlsDbtConta.nome_arquivo)] = adXlsDbtConta;
            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsDbtConta.nome_arquivo);
            //AdicionarAcesso(fUrl);
            AbrirNovaAba(upUpdatePanel, fUrl, adXlsDbtConta.nome_arquivo);
        }
    }
}