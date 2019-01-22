using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;
using IntegWeb.Entidades.Framework;
//using IntegWeb.Saude.Aplicacao.ENTITY;

namespace IntegWeb.Saude.Web
{
    public class UploadFile : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                //string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
                string UploadFilePath = context.Server.MapPath("UploadFile\\");
                if (!Directory.Exists(UploadFilePath))
                {
                    Directory.CreateDirectory(UploadFilePath);
                }

                List<ArquivoUpload> _lst_uploads = null;
                if (context.Session != null && context.Session["lst_uploads"] != null)
                {
                    _lst_uploads = (List<ArquivoUpload>)context.Session["lst_uploads"];
                }
                else
                {
                    _lst_uploads = new List<ArquivoUpload>();
                }
               
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    
                    //string fileName = file.FileName;

                    if (!string.IsNullOrEmpty(file.FileName))
                    {

                        string filename = Path.GetFileName(file.FileName).ToString();
                        string[] name = filename.Split('.');
                        string full_path_filename = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                        string fileExtension = file.ContentType;

                        file.SaveAs(full_path_filename);

                        ArquivoUpload newFile = new ArquivoUpload();
                        newFile.nome_arquivo = filename;
                        newFile.caminho_arquivo = full_path_filename;
                        _lst_uploads.Add(newFile);

                    }
                }
                context.Session["lst_uploads"] = _lst_uploads;        
                context.Response.Write("Ok");
            }
            catch (Exception ex)
            {
                context.Response.Write("ERRO! " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}