using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Administracao.Web
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //VerificaAcessso(Request.RawUrl);

            if (!IsPostBack)
            {
                if (Session["menus"] != null)
                {
                    rptMenuNivel1.DataSource = (List<IntegWeb.Entidades.Menu>)Session["menus"];
                    rptMenuNivel1.DataBind();
                    rptMenuNivel3.DataSource = (List<IntegWeb.Entidades.Menu>)Session["menusNivel3"];
                    rptMenuNivel3.DataBind();
                }
                else
                {
                    Response.Redirect("~/login.aspx");
                }


            }
      
        }
        private void VerificaAcessso(string url)
        {

            if (Session["Acessos"] != null)
            {
                var list = (List<string>)Session["Acessos"];
                string[] vet = url.Split(char.Parse("/"));
                bool isValid = false;

                url = vet[vet.Length - 1].ToString();

                if (url.Contains("Ferramentas.aspx")) return; 

                foreach (var item in list)
                {
                    if (item.Contains(url))
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

        protected void rptMenuNivel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptItem = (Repeater)e.Item.FindControl("rptMenuNivel2");

            rptItem.DataSource = ((IntegWeb.Entidades.Menu)e.Item.DataItem).MenusFilhos;
            rptItem.DataBind();
        }

        protected void rptMenuNivel2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "NIVEL2")
            {
                int codigoNivel2 = Convert.ToInt32(e.CommandArgument);

                List<IntegWeb.Entidades.Menu> menusNivel1 = (List<IntegWeb.Entidades.Menu>)Session["menus"];
                List<IntegWeb.Entidades.Menu> menusNivel3 = new List<IntegWeb.Entidades.Menu>();

                foreach (IntegWeb.Entidades.Menu n1 in menusNivel1)
                {
                    foreach (IntegWeb.Entidades.Menu n2 in n1.MenusFilhos)
                    {
                        if (n2.Codigo == codigoNivel2)
                        {
                            menusNivel3 = n2.MenusFilhos;

                            Session["menusNivel3"] = menusNivel3;

                            rptMenuNivel3.DataSource = menusNivel3;
                            rptMenuNivel3.DataBind();

                            break;
                        }
                    }
                }
            }
        }

        protected void rptMenuNivel3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptItem = (Repeater)e.Item.FindControl("rptMenuNivel4");

            rptItem.DataSource = ((IntegWeb.Entidades.Menu)e.Item.DataItem).MenusFilhos;
            rptItem.DataBind();
        }
    }
}