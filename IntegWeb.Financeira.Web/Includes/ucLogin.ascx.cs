using IntegWeb.Administracao.Aplicacao;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web.Includes
{
    public partial class ucLogin : System.Web.UI.UserControl
    {
        #region Atributos
        private ConectaAD objBLL = new ConectaAD();
        private string _strUrl;
        private bool _bln;
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            _strUrl = Request.RawUrl;


            _bln = (Session["objUser"] == null);

            if (!_strUrl.Contains("login.aspx") && _bln)
                Response.Redirect("~/login.aspx", false);

            if (Session["objUser"] != null)
            {
                objBLL = (ConectaAD)Session["objUser"];
            }

            MostraTelaConformeLogin(!_bln);
        }
        protected void btnAcessar_Click(object sender, EventArgs e)
        {

            objBLL.login = txtLogin.Text.ToUpper();
            objBLL.senha = txtSenha.Text;

            if (!string.IsNullOrEmpty(objBLL.senha))
            {
                int cont = 0;
                if (objBLL.AutenticaUsuarioAD())
                {
                    Session["erro"] = null;
                    Session.Timeout = 60;
                    Session["objUser"] = objBLL;
                    MostraTelaConformeLogin(true);
                    MontaMenu();
                    Response.Redirect("index.aspx");

                }
                else
                {
                    cont = int.Parse(Session["erro"] == null ? "0" : Session["erro"].ToString());
                    cont++;
                    Session["erro"] = cont;
                    lbSenha.Text = "Senha Inválida";

                    if (cont < 3)
                    {
                        cont = 3 - cont;
                        lblMsgAviso.Text = "Você só tem apenas " + cont.ToString() + " tentativas, se errar irá bloquear o computador!";
                    }
                    else
                    {
                        lblMsgAviso.Text = "Computador Bloqueado!";
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "reset", " MostraMsg();", true);
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "reset", " MostraMsg();", true);
                lbSenha.Text = "Digite a senha de rede para logar no Sistema!!";
                lblMsgAviso.Text = "";
            }
        }
        protected void lnkSair_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
        #endregion

        #region Métodos
        protected void MostraTelaConformeLogin(Boolean blnLogado)
        {
            string[] username;
            if (Context.User.Identity.IsAuthenticated)
            {
                username = Context.User.Identity.Name.Split(char.Parse(@"\"));
                txtLogin.Text = username[1].ToString();
            }

            divLogado.Visible = blnLogado;
            divNLogado.Visible = !blnLogado;

            txtSenha.Focus();
            if (blnLogado)
            {
                lbNome.Text = objBLL.nome;
                lbDepartamento.Text = objBLL.departamento;

                if (ConfigAplication.GetOwner().Equals("D"))
                    lblambiente.Text = "Ambiente de Desenvolvimento";
                else if (ConfigAplication.GetOwner().Equals("T"))
                    lblambiente.Text = "Ambiente de Homologação";
                else //P
                    lblambiente.Text = "Ambiente de Produção";
            }
        }
        private void MontaMenu()
        {

            MenuBLL bll = new MenuBLL();
            byte codigoSistema = 6;

            List<IntegWeb.Entidades.Menu> menusNivel1 = bll.ListarPorUsuario(codigoSistema, 1, null, txtLogin.Text);

            foreach (IntegWeb.Entidades.Menu n1 in menusNivel1)
            {
                n1.MenusFilhos = bll.ListarPorUsuario(codigoSistema, 2, n1.Codigo, txtLogin.Text);

                foreach (IntegWeb.Entidades.Menu n2 in n1.MenusFilhos)
                {
                    n2.MenusFilhos = bll.ListarPorUsuario(codigoSistema, 3, n2.Codigo, txtLogin.Text);

                    foreach (IntegWeb.Entidades.Menu n3 in n2.MenusFilhos)
                    {
                        n3.MenusFilhos = bll.ListarPorUsuario(codigoSistema, 4, n3.Codigo, txtLogin.Text);

                    }
                }
            }

            List<string> list = bll.ListarAcesso(codigoSistema, txtLogin.Text);
            list.Add("index.aspx");
            Session["Acessos"] = list;
            Session["menus"] = menusNivel1;

        }
        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}