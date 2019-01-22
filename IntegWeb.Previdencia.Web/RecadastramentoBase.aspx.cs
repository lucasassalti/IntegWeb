using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
//using IntegWeb.Previdencia.Aplicacao.DAL
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;

namespace IntegWeb.Previdencia.Web
{
    public partial class RecadastramentoBase : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TbImportar_Mensagem.Visible = false;
            TbExportar_Mensagem.Visible = false;
            TbGerar_Mensagem.Visible = false;

            if (!IsPostBack)
            {
                //CarregaTela();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            //ParticipanteBLL teste = new ParticipanteBLL();
            //teste.GetWhere(1, 474282, 2346);
            RegarregarGridExporta();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtDtRefIni.Text = "";
            txtDtRefFim.Text = "";
            txtNumContrato.Text = "";
            RegarregarGridExporta();
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

                        RecadastramentoBLL bll = new RecadastramentoBLL();
                        Resultado res = new Resultado();
                        var user = (ConectaAD)Session["objUser"];

                        res = bll.RetornoRecadastro(dt, (user != null) ? user.login : "DESENV", DateTime.Now);

                        if (res.Ok)
                        {
                            //res = ConsolidaListaDebitoConta(filename);
                            MostraMensagem(TbImportar_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
                        }

                    }
                    catch (Exception ex)
                    {
                        MostraMensagem(TbImportar_Mensagem, "Atenção! O arquivo não pôde ser carregado. Motivo:\\n" + ex.Message, "n_error");
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
                    MostraMensagem(TbImportar_Mensagem, "Atenção! Carregue apenas arquivos de retorno do banco");
                }

            }

            else
            {
                MostraMensagem(TbImportar_Mensagem, "Atenção! Selecione um Arquivo para continuar");
            }

        }

        protected void grdRecadastramento1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string[] pk = e.CommandArgument.ToString().Split(',');

            try{
                switch (e.CommandName)
                {

                    case "Exportar":
                        DateTime PK_DAT_REF_RECAD = DateTime.Parse(pk[0]);
                        int NUM_CONTRATO = int.Parse(pk[1]);
                        RecadastramentoBLL bll = new RecadastramentoBLL();
                        DataTable dt = bll.Exportar(PK_DAT_REF_RECAD, NUM_CONTRATO, bool.Parse(pk[2].Trim()));
                        
                        ArquivoDownload adTxtRecad = new ArquivoDownload();
                        adTxtRecad.nome_arquivo = "ARQ_RECAD_ASSISTIDOS_" + PK_DAT_REF_RECAD.ToString("ddMMyyyy") + ".TXT";
                        //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
                        adTxtRecad.dados = dt;
                        adTxtRecad.opcao_arquivo = "\n"; // Usar quebra de linha modo UNIX
                        Session[ValidaCaracteres(adTxtRecad.nome_arquivo)] = adTxtRecad;
                        string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adTxtRecad.nome_arquivo);
                        AdicionarAcesso(fullUrl);
                        AbrirNovaAba(upRecadastramento, fullUrl, adTxtRecad.nome_arquivo);

                        //MostraMensagem(TbExportar_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
                        MostraMensagem(TbExportar_Mensagem, "Arquivo gerado com sucesso!", "n_ok" );

                        break;
                    default:
                        break;            
                }
            }
            catch (Exception ex)
            {
                MostraMensagem(TbExportar_Mensagem, "Atenção! O arquivo não pôde ser carregado. Motivo:\\n" + ex.Message, "n_error");
            }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                RecadastramentoBLL bll = new RecadastramentoBLL();
                var user = (ConectaAD)Session["objUser"];
                bll.Gerar_Nova_Base(DateTime.Parse(txtDtBase.Text), Int32.Parse(txtNumContratoGerar.Text), (user != null) ? user.login : "DESENV", DateTime.Now);
                MostraMensagem(TbGerar_Mensagem, "Base gerada com sucesso!", "n_ok");
                RegarregarGridGera();
                RegarregarGridExporta();
            }
            catch (Exception ex)
            {
                MostraMensagem(TbGerar_Mensagem, "Atenção! A base não pode ser gerada. Motivo:\\n" + Util.GetInnerException(ex), "n_error");
            }
        }

        protected void btnLimparGerar_Click(object sender, EventArgs e)
        {
            txtDtBase.Text = "";
            txtNumContratoGerar.Text = "";
            RegarregarGridGera();
        }

        private void RegarregarGridExporta()
        {
            grdRecadastramento1.PageIndex = 0;
            grdRecadastramento1.EditIndex = -1;
            grdRecadastramento1.DataBind();
        }

        private void RegarregarGridGera()
        {
            grdRecadastramento2.PageIndex = 0;
            grdRecadastramento2.EditIndex = -1;
            grdRecadastramento2.DataBind();
        }

    }
}