using IntegWeb.Administracao.Aplicacao.BLL;
using IntegWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;

namespace IntegWeb.Administracao.Web
{
    public partial class Ferramentas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               

            }
        }

        protected void btnExecutar_click(object sender, EventArgs e)
        {
            txtLog.Visible = false;
            lblResultado.Visible = false;
            try
            {

                using (ConexaoOracle conexao = new ConexaoOracle())
                {
                    lblResultado.Visible = conexao.ExecutarNonQuery(txtScript.Text);
                    txtLog.Text = "";
                }
            }
            catch (Exception ex)
            {
                txtLog.Visible = true;
                txtLog.Text += "<br/><br/>" + ex.Message;
            }


        }

               
    }
}