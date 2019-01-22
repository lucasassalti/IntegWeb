using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class index : System.Web.UI.Page
    {
        BasePage obj = new BasePage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["menus"] != null)
            {

                var list = (List<IntegWeb.Entidades.Menu>)Session["menus"];
                RegraTela(list.Count > 0);
            }
            else
                Response.Redirect("~/login.aspx");

            if (Session["mensagem"] != null)
            {
                obj.MostraMensagemTela(this.Page, Session["mensagem"].ToString());
                Session.Remove("mensagem");
            }
        }

        private void RegraTela(bool isLogin) {

            DivLogado.Visible = isLogin;
            DivNLogado.Visible = !isLogin;
        
        }
    }
}