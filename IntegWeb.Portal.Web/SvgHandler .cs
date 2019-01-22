using System.IO;
using System.Web;

namespace IntegWeb.Portal.Web
{
    //Esta classe foi criada para renderização de arquivos SVG na página.
    public class SvgHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/svg+xml";
            context.Response.BinaryWrite(File.ReadAllBytes(context.Request.PhysicalPath));
            context.Response.End();
        }
    }
}