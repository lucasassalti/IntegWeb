﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
      
            //lbldata.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

                ContentPlaceHolder1.Visible = true;
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
                            if (!n2.Link.Equals(""))
                            {
                                Response.Redirect(n2.Link);
                            }
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

        protected void lnkMenuNivel2_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder1.Visible = false;
        }

      

 

    }
}