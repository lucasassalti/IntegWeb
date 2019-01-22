using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;
//using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Web
{
    public class ArquivoPatrocinadora_uploader : IHttpHandler, IRequiresSessionState
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

                List<PRE_TBL_ARQ_PATROCINA> _lst_uploads = null;
                if (context.Session != null && context.Session["lst_uploads"] != null)
                {
                    _lst_uploads = (List<PRE_TBL_ARQ_PATROCINA>)context.Session["lst_uploads"];
                }
                else
                {
                    _lst_uploads = new List<PRE_TBL_ARQ_PATROCINA>();
                }
               
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    
                    //string fileName = file.FileName;

                    bool ja_existe_na_sessao = _lst_uploads.Any(f => f.NOM_ARQUIVO == file.FileName && f._TAMANHO_ARQUIVO == file.ContentLength);

                    if (!string.IsNullOrEmpty(file.FileName) && !ja_existe_na_sessao)
                    {

                        string filename = Path.GetFileName(file.FileName).ToString();
                        string[] name = filename.Split('.');
                        string full_path_filename = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                        string fileExtension = file.ContentType;

                        file.SaveAs(full_path_filename);

                        PRE_TBL_ARQ_PATROCINA newFile = new PRE_TBL_ARQ_PATROCINA();
                        newFile.NOM_ARQUIVO = filename;
                        newFile._TAMANHO_ARQUIVO = file.ContentLength;
                        newFile._CAMINHO_COMPLETO_ARQUIVO = full_path_filename;
                        newFile._PROCESSADO = false;

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