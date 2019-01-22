using IntegWeb.Entidades.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Concessao;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class BateCadastralCarga : BasePage
    {
        #region  .:Propriedades:.

        DataTable dtAltBanc = new DataTable();
        DataTable dtAltCad = new DataTable();

        #endregion

        #region .: Eventos :. 

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            //grdBateCadastral.Visible = false;
            Processo_Mensagem.Visible = false;
            //lblPartDel.Visible = false;

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
                        DataTable dt = ReadTextFile(path, System.Text.Encoding.GetEncoding("iso-8859-1"));
                        var codEmp = ReadTextFile(path, System.Text.Encoding.GetEncoding("iso-8859-1")).Rows[0]["Data"].ToString().Substring(0, 3);


                        //Regra de negócio
                        BateCadastralCargaBLL bll = new BateCadastralCargaBLL();
                        dtAltBanc = bll.ListarAlteracoesBanc(dt);
                        dtAltCad = bll.ListarAlteracoesCad(dt);


                        if (dtAltBanc.Rows.Count > 0)
                        {
                            //Download do Excel com os Participantes deletados do arquivo
                            ArquivoDownload adXlsBtCadastral = new ArquivoDownload();
                            adXlsBtCadastral.nome_arquivo = "Empresa_" + codEmp + "_Alteracao_Portal_Dados_Banco.xls";
                            //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
                            adXlsBtCadastral.dados = dtAltBanc;
                            Session[ValidaCaracteres(adXlsBtCadastral.nome_arquivo)] = adXlsBtCadastral;
                            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsBtCadastral.nome_arquivo);
                         //   AdicionarAcesso(fUrl);
                            AbrirNovaAba(this, fUrl, adXlsBtCadastral.nome_arquivo);
                        }

                        if (dtAltCad.Rows.Count > 0)
                        {
                            //Download do Excel com os Participantes deletados do arquivo
                            ArquivoDownload adXlsBtCadastral = new ArquivoDownload();
                            adXlsBtCadastral.nome_arquivo = "Empresa_" + codEmp + "_Alteracao_Portal_Dados_CAD.xls";
                            //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
                            adXlsBtCadastral.dados = dtAltCad;
                            Session[ValidaCaracteres(adXlsBtCadastral.nome_arquivo)] = adXlsBtCadastral;
                            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsBtCadastral.nome_arquivo);
                          //  AdicionarAcesso(fUrl);
                            AbrirNovaAba(this, fUrl, adXlsBtCadastral.nome_arquivo);
                        }

                        if (dtAltBanc.Rows.Count == 0 && dtAltCad.Rows.Count == 0)
                        {
                            MostraMensagem(Processo_Mensagem, "O ARQUIVO NÃO POSSUI NENHUM REGISTRO COM ALTERAÇÃO CADASTRAL NO PORTAL! LIBERADO PARA CARGA");
                            return;
                        }
                    }

                    catch (Exception ex)
                    {
                        MostraMensagem(Processo_Mensagem, "Atenção! O arquivo não pôde ser carregado. Motivo:\\n" + ex.Message, "n_error");
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
                    MostraMensagem(Processo_Mensagem, "Atenção! Carregue apenas arquivos com extensão .txt");
                }

            }

            else
            {
                MostraMensagem(Processo_Mensagem, "Atenção! Selecione um Arquivo para continuar");
            }

        }

        #endregion
    }
}
