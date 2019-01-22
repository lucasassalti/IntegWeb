using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;
//using StackExchange.Profiling;

namespace IntegWeb.Saude.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //MiniProfilerEF.Initialize();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //VerificaAcessso(Request.RawUrl);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            VerificaAcessso(Request.RawUrl);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
             
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        
        private void VerificaAcessso(string url)
        {

            if (url.IndexOf("/login.aspx") > -1 ||
                url.IndexOf("/Content") > -1 ||
                url.IndexOf("/css") > -1 ||
                url.IndexOf("/js") > -1 ||
                url.IndexOf("/img") > -1 ||
                url.IndexOf("/WebResource.axd") > -1 ||
                url.IndexOf("/ScriptResource.axd") > -1 ||
                url.IndexOf("/CrystalImageHandler.aspx") > -1 ||
                url.IndexOf("/favicon.ico") > -1 ||
                url.IndexOf("/UploadFile.ashx") > -1 ||
                url.IndexOf("/ChamadasAjax.ashx") > -1 ||
                (ConfigurationManager.AppSettings["Config"] ?? String.Empty)== "D")
            {
                return;
            }

            if ((HttpContext.Current.Session != null) &&
                HttpContext.Current.Session["Acessos"] != null)
            {
                var list = (List<string>)HttpContext.Current.Session["Acessos"];
                string[] vet = url.Split(char.Parse("/"));
                bool isValid = false;
                url = vet[vet.Length - 1].ToString();
                foreach (var item in list)
                {
                    //if (item.Contains(url))
                    //    isValid = true;
                    // Habilita querystring
                    if (url.Contains(item))
                        isValid = true;
                }
                if (!isValid)
                {
                    Session["mensagem"] = "Erro!!!\\nVocê não tem permissão para essa página!";
                    Response.Redirect("~/index.aspx");
                }

            }
            else
            {
                    Response.Redirect("~/login.aspx");
            }
        }
    }
}