using OfficeOpenXml;
using Intranet.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using IntegWeb.Intranet.Aplicacao;


namespace IntegWeb.Intranet.Web
{

    public partial class EMailMarketing : BasePage
    {

        DadosOperacao doDados = new DadosOperacao();

        protected void Page_Load(object sender, EventArgs e)
        {
            hlkHtml.Text = "";
        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            //trazer para uma variavel o vlaor digitado pelo usuario
            string nmCampanha = txtCampanha.Text.Trim();

            //tratar o texto, tirando espaço e acentuação
            nmCampanha = nmCampanha.Replace(" ", "");
            nmCampanha = Util.RemoverAcentuacao(nmCampanha);


            lblRegistros.Text = "";

            if (String.IsNullOrEmpty(nmCampanha))
            {
                MostraMensagemRodape("Atenção! Preencha o campo campanha para prosseguir");
                return;
            }

            if (uploadZip.HasFile)
            {
                
               
                if (uploadZip.PostedFile.ContentType.Equals("application/x-zip-compressed") || uploadZip.PostedFile.ContentType.Equals("application/octet-stream"))
                {
                    string pasta_html = "";
                    string arquivo_zip = "";
                    string index_html = "";
                    //string index_jpg = "";

                    try
                    {

                        string[] name = Path.GetFileName(uploadZip.FileName).ToString().Split('.');
                        string UploadFilePath = Server.MapPath("UploadFile\\");
                        pasta_html = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime();
                        arquivo_zip = pasta_html + "." + name[1];

                        if (name[1].ToUpper() != "ZIP")
                        {
                            throw new Exception("Extensão inválida. Carregue apenas arquivos compactados(.zip)");
                        }

                        if (!Directory.Exists(UploadFilePath))
                        {
                            Directory.CreateDirectory(UploadFilePath);
                        }

                        uploadZip.SaveAs(arquivo_zip);

                        ZipFile.ExtractToDirectory(arquivo_zip, pasta_html);
                        bool encontrou = false;
                        string pasta_alvo = pasta_html;

                        List<string> lista_pastas = new List<string>();
                        lista_pastas.Add(pasta_alvo);

                        int cont_pasta = 0;
                        while (cont_pasta  < lista_pastas.Count)
                        {
                            foreach (string pasta in Directory.GetFiles(lista_pastas[cont_pasta]))
                            {
                                if (pasta.ToUpper().IndexOf("INDEX.HTML") > -1)
                                {
                                    index_html = pasta;
                                    encontrou = true;
                                }
                            }

                            foreach (string pasta in Directory.GetDirectories(lista_pastas[cont_pasta]))
                            {
                                lista_pastas.Add(pasta);
                            }

                            cont_pasta++;
                        }

                        int cont_pastaI = 0;
                        List<string> lista_imagens = new List<string>();
                        while (cont_pastaI < lista_pastas.Count)
                        {
                            foreach (string pasta in Directory.GetFiles(lista_pastas[cont_pastaI]))
                            {
                                if (pasta.ToUpper().IndexOf(".JPG") > -1)
                                {
                                    //index_jpg = pasta;
                                    lista_imagens.Add(pasta);
                                    encontrou = true;
                                }
                            }

                            cont_pastaI++;
                        }

                        //Enviar arquivo para SFTP
                        string host = "fcespihsp001";
                        string username = "userftp";
                        string password = "Fcesp4*3-FTP";

                        if (encontrou == true)
                        {

                            EMailMarketingBLL emailMktBLL = new EMailMarketingBLL();

                            string index_html_conteudo = "";

                            try
                            {

                                index_html_conteudo = "";
                                using (StreamReader file = new StreamReader(index_html, Encoding.GetEncoding("ISO-8859-1")))
                                {
                                    index_html_conteudo = file.ReadToEnd();

                                    nmCampanha = emailMktBLL.ContCamp(host, username, password, nmCampanha);

                                    foreach (string index_jpg in lista_imagens)
                                    {
                                        //emailMktBLL.SendFTP(host, username, password, lista_imagens, nmCampanha);
                                        emailMktBLL.SendFTP(host, username, password, index_jpg, nmCampanha);
                                        string nom_arquivo = GetTagTarget(index_html_conteudo, "src=", Path.GetFileName(index_jpg));
                                        //string nom_arquivo = GetTagTarget(index_html_conteudo, "=", Path.GetFileName(index_jpg));
                                        index_html_conteudo = index_html_conteudo.Replace(nom_arquivo, "http://www.funcesp.com.br/emailmarketing/" + nmCampanha + "/" + Path.GetFileName(index_jpg));

                                        if (!index_html_conteudo.Contains("href=\"http://www."))
                                        {
                                            index_html_conteudo = index_html_conteudo.Replace("href=\"http://", "href=\"http://www.");
                                        }

                                        if (!index_html_conteudo.Contains("href"))
                                        {
                                            index_html_conteudo = index_html_conteudo.Replace("<img src", "<a href=\"https://www.funcesp.com.br\"target=\"_blank\" title=\"" + nmCampanha + "\">" + "\r\n" + "<img src");
                                        }

                                    }
                                }

                                if (!index_html.Contains("<p style"))
                                {
                                    //index_html_conteudo = index_html_conteudo.Replace("<body>", "<body>" + "\r\n" + "<p style=\"font-size:1px; color:#FFF\" align=\"center\"> Esta mensagem (incluindo seus anexos) é confidencial e é endereçada exclusivamente às pessoas e/ou instituições acima indicadas. Se você a recebeu por engano, por favor notifique o remetente e delete a mensagem do seu sistema. O uso não autorizado ou a disseminação desta mensagem por inteiro ou parcialmente é estritamente proibida. Os e-mails são suscetíveis a mudança, portanto, não somos responsáveis pela transmissão imprópria ou incompleta da informação contida nesta comunicação. </p>");
                                }

                                //download do arquivo html
                                ArquivoDownload htmlArquivo = new ArquivoDownload();
                                htmlArquivo.nome_arquivo = nmCampanha + ".txt";
                                htmlArquivo.dados = index_html_conteudo;
                                Session[ValidaCaracteres(htmlArquivo.nome_arquivo)] = htmlArquivo;
                                string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(htmlArquivo.nome_arquivo);
                                AbrirNovaAba(upSimulacao, fUrl, htmlArquivo.nome_arquivo);

                            }

                            catch (Exception ex)
                            {

                            }
                        }



                    }
                    catch (Exception ex)
                    {
                        MostraMensagemRodape("Atenção! O arquivo não pôde ser carregado. Motivo: " + ex.Message);
                    }
                    finally
                    {
                        uploadZip.FileContent.Dispose();
                        uploadZip.FileContent.Flush();
                        uploadZip.PostedFile.InputStream.Flush();
                        uploadZip.PostedFile.InputStream.Close();
                        if (File.Exists(arquivo_zip))
                        {
                            File.Delete(arquivo_zip);
                        }
                        if (Directory.Exists(pasta_html))
                        {
                            Directory.Delete(pasta_html, true);
                        }

                    }
                }
                else if (uploadZip.PostedFile.ContentType.ToLower() == "image/png" ||
                              uploadZip.PostedFile.ContentType.ToLower() ==  "image/jpeg" ||
                                  uploadZip.PostedFile.ContentType.ToLower() ==  "image/gif" ||
                                      uploadZip.PostedFile.ContentType.ToLower() ==  "image/tiff")
                {
                    string[] nameImagem = Path.GetFileName(uploadZip.FileName).ToString().Split('.');
                    string UploadFilePath = Server.MapPath("UploadFile\\");
                    string Imagem = UploadFilePath + nameImagem[0];
                    string UploadImagem = Imagem + "." + nameImagem[1];
                    uploadZip.SaveAs(UploadImagem);

                    //Enviar arquivo para SFTP
                    string host = "fcespihsp001";
                    string username = "userftp";
                    string password = "Fcesp4*3-FTP";

                    try
                    {
                        EMailMarketingBLL Upload = new EMailMarketingBLL();
                        string nmCampanha2 = Upload.ContCamp(host, username, password, nmCampanha);
                        //Upload.SendFTPImagem(host, username, password, UploadImagem, nmCampanha);
                        Upload.SendFTP(host, username, password, UploadImagem, nmCampanha2); 
                        hlkHtml.NavigateUrl = "http://www.funcesp.com.br/emailmarketing/" + nmCampanha2 + "/" + Path.GetFileName(uploadZip.FileName);
                        hlkHtml.Text = hlkHtml.NavigateUrl;
                    }
                    catch (Exception ex)
                    {
                        MostraMensagemRodape("Atenção! O arquivo não pôde ser carregado. Motivo: " + ex.Message);
                    }
                    finally
                    {
                        uploadZip.FileContent.Dispose();
                        uploadZip.FileContent.Flush();
                        uploadZip.PostedFile.InputStream.Flush();
                        uploadZip.PostedFile.InputStream.Close();
                        if (File.Exists(UploadImagem))
                        {
                            File.Delete(UploadImagem);
                        }
                    }
                }
                else
                {
                    MostraMensagemRodape("Atenção! Carregue apenas compactados(.zip) ou Imagens");   
                }
            }
            else
            {
                MostraMensagemRodape("Atenção! Selecione o arquivo ZIP para processar");
            }

        }

        private string GetTagTarget(string html, string tag_target, string file_name)
        {
            //int pos = html.IndexOf(file_name);

            int pos_ini = 0;
            int pos_final = html.IndexOf(file_name) + file_name.Length;

            for (int pos = html.IndexOf(file_name); pos >= 0; pos--)
            {
                if (html.Substring(pos, tag_target.Length).ToUpper() == tag_target.ToUpper())
                {
                    pos_ini = pos;
                    break;
                }
            }

            if (pos_ini > 0)
            {
                return html.Substring(pos_ini + tag_target.Length + 1, pos_final - pos_ini - tag_target.Length - 1);
            } else 
            {
                return file_name;
            }
            //return html.Substring(pos_ini + tag_target.Length + 1, pos_final - pos_ini - tag_target.Length - 1);
        }

    }
}