using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao;
using System.Data;
using System.Net;
using System.IO;
using IntegWeb.Entidades.Framework;
using System.Web;

namespace IntegWeb.Portal.Web
{
    public partial class DirBoletoSau : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string bCod = Request.QueryString["bCod"];

                if (!String.IsNullOrEmpty(bCod))
                {

                    DownloadArquivosProd(bCod);

                     txt.Text = bCod;

                    //string caminhoArq = Server.MapPath(@"UploadFile\\" + bCod);

                     string caminhoArq = @"\\fcespwebd001\\PortalWeb\\UploadFile\\" + bCod;

                    if (File.Exists(caminhoArq))
                    {

                        string FileName = Path.GetFileName(caminhoArq);
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + bCod);
                        Response.ContentType = System.Net.Mime.DispositionTypeNames.Attachment;
                        Response.WriteFile(caminhoArq);
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        txt.Text = "Arquivo não encontrado, favor entrar em contato com o atendimento da Funcesp";
                    }

                }

            }
            catch(Exception ex)
            {
            }

        }

        public static void DownloadArquivosProd(string arq)
        {
            string diretorio = @"\\fcespwebd001\\PortalWeb\\UploadFile";

            FtpWebResponse response = null;
            FileStream writeStream = null;

            try
            {
                FtpWebRequest reqFTP;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://10.190.35.57:21/2018/" + arq));

                reqFTP.Timeout = 30000;

                reqFTP.Credentials = new NetworkCredential("integwebp", "cWp8d4t0");
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;

                response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                writeStream = new FileStream(diretorio + "\\" + arq, FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);

                

            }
            catch (Exception ex)
            {

            }
        }


    }
}