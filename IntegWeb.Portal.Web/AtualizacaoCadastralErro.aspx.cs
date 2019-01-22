using System;

namespace IntegWeb.Portal.Web
{
    public partial class AtualizacaoCadastralErro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = Request.QueryString["msg"];
            hMensagemErro.InnerText = msg;
        }
    }
}