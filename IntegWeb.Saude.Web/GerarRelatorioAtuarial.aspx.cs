using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.DAL.Financeiro;
using System.Text;
using IntegWeb.Entidades.Framework;
using System.IO;
using IntegWeb.Framework;
using System.IO.Compression;

namespace IntegWeb.Saude.Web
{
    public partial class GeraRelatorioAtuarial : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTipoCarga.Items.Insert(0, new ListItem("---Selecione---", ""));

            }
        }
        protected void btnGerarCarga_Click(object sender, EventArgs e)
        {
            #region DATA INÍCIO /FIM  - DdlTipoCarga

            String dataInicial = txtDataInicio.Text;

            String dataFinal = txtDataFim.Text;

            String ValorSelecionado = ddlTipoCarga.SelectedValue;
            #endregion


            GerarRelatorioAtuarialDAL gerarRel = new GerarRelatorioAtuarialDAL();

            if (ddlTipoCarga.SelectedValue == "REL_SINISTRO")
            {
                DataTable retorno = gerarRel.ListaSinistro(txtMes.Text, txtAno.Text);
                if (retorno.Rows.Count > 0)
                {
                    GeraSaidaCsv(retorno, "CSV", ValorSelecionado);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Nenhum registro encontrado!");
                }
            }
            else
            {
                DataTable retorno = gerarRel.ListaDatas(ValorSelecionado, dataInicial, dataFinal);
                if (retorno.Rows.Count > 0)
                {
                    GeraSaidaCsv(retorno, "CSV", ValorSelecionado);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Nenhum registro encontrado!");
                }
            }



        }

        public void GeraSaidaCsv(DataTable arquivo, string extensao_anexo, string tp_rel)
        {
            var user = (ConectaAD)Session["objUser"];
            string caminho_servidor = Server.MapPath(@"~/");
            string Pasta_Server = caminho_servidor + @"\UploadFile";
            string Pasta_Server_Download = caminho_servidor + @"\UploadFile";

            //if (Directory.Exists(Pasta_Server_Download))
            //{
            //    Directory.Delete(Pasta_Server_Download, true);
            //}

            //Directory.CreateDirectory(Pasta_Server);
            //Directory.CreateDirectory(Pasta_Server_Download);

            using (var memoryStream = new MemoryStream())
            {

                String ArquiveName = String.Format(@"{0}_{1}", tp_rel.Replace("REL_", ""), System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var demoFile = archive.CreateEntry(ArquiveName + ".csv");

                    var entryStream = demoFile.Open();
                    using (var streamWriter = new StreamWriter(entryStream, Encoding.UTF8))
                    {

                        foreach (DataRow row in arquivo.Rows)
                        {

                            for (int i = 0; i < arquivo.Columns.Count; i++)
                            {

                                streamWriter.WriteLine(row[i].ToString());
                            }
                        }
                    }
                    entryStream.Close();
                    entryStream.Dispose();
                    string nomeArquivo = ArquiveName;
                    //string diretorio = Pasta_Server_Download + "\\ARQUIVOS_ATUARIAIS" + ArquiveName);
                    string diretorio = string.Format("{0}\\{1}", Pasta_Server_Download, ArquiveName);

                    using (var fileStream = new FileStream(diretorio + ".zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }


                    ////Download do arquivo
                    ArquivoDownload downloadArquivo = new ArquivoDownload();
                    downloadArquivo.nome_arquivo = ArquiveName + ".zip";
                    downloadArquivo.caminho_arquivo = diretorio + ".zip";
                    Session[ValidaCaracteres(downloadArquivo.nome_arquivo)] = downloadArquivo;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(downloadArquivo.nome_arquivo);
                    AbrirNovaAba(upUpdatePanel, fUrl, downloadArquivo.nome_arquivo);
                }


                //string diretorio = String.Format(@"D:\Users\{0}\Desktop\Carga_Atuarial", user.login);
                //string diretorio = Server.MapPath("UploadFile\\");

                //try 
                //{
                //    if (!Directory.Exists(diretorio))
                //    {
                //        Directory.CreateDirectory(diretorio);
                //    }

                //    //String ArquiveName = String.Format(@"{2}\{0}_{1}.csv", tp_rel.Replace("REL_", ""), System.DateTime.Now.ToString("yyyyMMdd_HHmmss"), diretorio);
                //    FileInfo aFile = new FileInfo(ArquiveName);

                //    aFile.Create().Close();

                //    var entryStream = aFile.OpenWrite();
                //    if (Directory.Exists(diretorio))
                //    {
                //        using (var streamWriter = new StreamWriter(entryStream, Encoding.UTF8))
                //        {
                //            foreach (DataRow row in arquivo.Rows)
                //            {
                //                for (int i = 0; i < arquivo.Columns.Count; i++)
                //                {
                //                    streamWriter.WriteLine(row[i].ToString());
                //                }
                //            }
                //        }
                //     MostraMensagemTelaUpdatePanel(upUpdatePanel, "O arquivo foi gerado no caminho: \\n\\" + ArquiveName + "!");
                //    }
                //    else
                //    {
                //        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro ao tentar gerar o arquivo CSV.");
                //        return;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception("Problemas ao gerar arquivo: //" + ex.Message);
                //}
            }
        }

        protected void ddlTipoCarga_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCarga.SelectedValue == "REL_PRESTADORES" ||
                ddlTipoCarga.SelectedValue == "REL_ESTUDO_SAUDE" ||
                ddlTipoCarga.SelectedValue == "REL_PLANOS" ||
                  ddlTipoCarga.SelectedValue == "REL_BENEFICIARIO" ||
                ddlTipoCarga.SelectedValue == "")
            {
                txtDataInicio.Text = "";
                txtDataFim.Text = "";
                divData.Visible = false;
                divSinistro.Visible = false;
            }
            else if (ddlTipoCarga.SelectedValue == "REL_SINISTRO")
            {
                txtDataInicio.Text = "";
                txtDataFim.Text = "";
                txtMes.Text = "";
                txtAno.Text = "";
                divData.Visible = false;
                divSinistro.Visible = true;
            }
            else
            {
                divSinistro.Visible = false;
                divData.Visible = true;
            }
        }
    }
}
