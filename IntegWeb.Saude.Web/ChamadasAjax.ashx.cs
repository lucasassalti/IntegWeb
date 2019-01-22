using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using IntegWeb.Entidades.Framework;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System.Web.Script.Services;
using System.Dynamic;

namespace IntegWeb.Saude.Web
{
    public class JqueryAjax : IHttpHandler, IRequiresSessionState
    {
        #region Classes utilizadas somente nas chamadas Ajax
        private class BuscaServicoPorPrestador
        {
            public string codigoHospital { get; set; }
            public string codigoServico { get; set; }
        }
        
        #endregion

        [WebMethod]
        public void ProcessRequest(HttpContext context)
        {
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd().Replace("]","").Replace("[","");
            
            //Deserealizar o json.
            var js = new JavaScriptSerializer();
            
            if (!String.IsNullOrEmpty(strJson))
            {
                if (context.Request.Path.Contains("changeListaDeServicos"))
                {
                    BuscaServicoPorPrestador objUsr = js.Deserialize<BuscaServicoPorPrestador>(strJson);

                    //AnexoIIBLL anexoBll = new AnexoIIBLL();
                    //string hosp = objUsr.codigoHospital;
                    //string serv = objUsr.codigoServico;
                    //var servicos = anexoBll.changeListaDeServicos(hosp, serv);
                    //var src = js.Serialize(servicos);
                    //context.Response.Write(src);
                }
                //else if (context.Request.Path.Contains("Atu@res3979alizarddlNmHospital"))
                //{
                //    AnexoIIBLL anexoBLL = new AnexoIIBLL();
                //    anexoBLL.carregarHospital(null, null).ToList<object>();
                //}
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