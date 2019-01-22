using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IntegWeb.Administracao.Aplicacao;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Text;
using System.Data;

namespace IntegWeb.Administracao.Web
{
    public partial class MenuTela : System.Web.UI.Page
    {
        #region Atributos
        BasePage objB = new BasePage();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTela();
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            //posiciona na visão cadastro
            mvwMenu.ActiveViewIndex = 1;

            //limpar os campos
            txtCodigo.Text =
                txtNome.Text = string.Empty;

            //posicionar o foco inicial
            txtCodigo.Enabled = false;
            txtNome.Focus();

            //Visualiza botões corretos
            btnSalvar.Visible = true;
            btnAlterar.Visible = false;

            ddlSistema.Enabled = true;
            ddlNivel.Enabled = true;
            ddlMenuPai.Enabled = true;

            LimpaCampos();

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            IntegWeb.Entidades.Menu objMenu = new IntegWeb.Entidades.Menu();

            int iCodigo = 0;
            int.TryParse(txtCodigo.Text, out iCodigo);
            if (iCodigo>0)
            {
                objMenu.Codigo = iCodigo;
            }
            objMenu.Nome = txtNome.Text;
            objMenu.Sistema.Codigo = Convert.ToByte(ddlSistema.SelectedValue);
            objMenu.Nivel = Convert.ToInt16(ddlNivel.SelectedValue);

            if (ddlNivel.SelectedValue == "4")
            {
                objMenu.Link = txtLink.Text;
            }

            objMenu.MenuPai = new IntegWeb.Entidades.Menu();
            if (ddlNivel.SelectedValue != "1")
            {
                objMenu.MenuPai.Codigo = Convert.ToInt32(ddlMenuPai.SelectedValue);
            }

            MenuBLL bll = new MenuBLL();
            Resultado retorno = new Resultado();

            if (objMenu.Codigo > 0)
            {
                retorno = bll.Alterar(objMenu);
            }
            else
            {
                retorno = bll.Incluir(objMenu);
            }            

            if (retorno.Ok)
            {
                CarregaGrid(new MenuBLL().Consultar(new IntegWeb.Entidades.Menu()));

                //retorna visão de lista
                mvwMenu.ActiveViewIndex = 0;

                objB.MostraMensagemTelaUpdatePanel(upMenu, (objMenu.Codigo > 0) ? "Registro alterado com sucesso!" :  "Menu inserido com sucesso!");
            }
            else
            {
                objB.MostraMensagemTelaUpdatePanel(upMenu, retorno.Mensagem);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mvwMenu.ActiveViewIndex = 0;
        }

        //protected void btnAlterar_Click(object sender, EventArgs e)
        //{
        //    if (!IsValid)
        //        return;

        //    IntegWeb.Entidades.Menu objMenu = new IntegWeb.Entidades.Menu();

        //    MenuBLL bll = new MenuBLL();

        //    objMenu.MenuPai = new IntegWeb.Entidades.Menu();

        //    objMenu.Codigo = Convert.ToInt32(txtCodigo.Text);

        //    objMenu.Sistema.Codigo = Convert.ToByte(ddlSistema.SelectedValue);
        //    objMenu.Nivel = Convert.ToInt16(ddlNivel.SelectedValue);
        //    objMenu.MenuPai.Codigo = Convert.ToInt32(ddlMenuPai.SelectedValue);
        //    objMenu.Nome = txtNome.Text;
        //    objMenu.Link = txtLink.Text;
            

        //    Resultado retorno = bll.Alterar(objMenu);

        //    if (retorno.Ok)
        //    {
        //        CarregaGrid(new MenuBLL().Consultar(new IntegWeb.Entidades.Menu()));

        //        //Posiciona na visão de Listagem
        //        mvwMenu.ActiveViewIndex = 0;

        //        objB.MostraMensagemTelaUpdatePanel(upMenu, "Cadastro alterado com sucesso!");
        //    }
        //    else
        //    {
        //        objB.MostraMensagemTelaUpdatePanel(upMenu, retorno.Mensagem);
        //    }
        //}

        protected void grvMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grvMenu"] != null)
            {
                grvMenu.PageIndex = e.NewPageIndex;
                grvMenu.DataSource = ViewState["grvMenu"];
                grvMenu.DataBind();
            }
        }

        protected void grvMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EDITAR")
            {

                //Consulta objeto no Banco de Dados
                MenuBLL bll = new MenuBLL();
                IntegWeb.Entidades.Menu obj = new IntegWeb.Entidades.Menu();
                obj.Codigo = Convert.ToInt32(e.CommandArgument);
                DataTable dt = new DataTable();
                dt = bll.Consultar(obj);

                //Preenche dados da Tela
                txtCodigo.Text = dt.Rows[0]["ID_MENU"].ToString();
                txtNome.Text = dt.Rows[0]["NM_MENU"].ToString();

                ddlSistema.SelectedValue = dt.Rows[0]["ID_SISTEMA"].ToString();
                ddlNivel.SelectedValue = dt.Rows[0]["CD_NIVEL"].ToString();

                CarregarMenuPai();

                if (!string.IsNullOrEmpty(dt.Rows[0]["ID_MENU_PAI"].ToString()))
                {
                    ddlMenuPai.Items.Add(new ListItem(dt.Rows[0]["MENU_PAI"].ToString(), dt.Rows[0]["ID_MENU_PAI"].ToString()));
                    ddlMenuPai.Text = dt.Rows[0]["ID_MENU_PAI"].ToString();
                }

                txtLink.Text = dt.Rows[0]["DS_LINK"].ToString();

                //Posiciona o foco inicial
                txtCodigo.Enabled = false;
                ddlSistema.Enabled = true;
                ddlNivel.Enabled = true;
                ddlMenuPai.Enabled = true;
                txtNome.Focus();

                //Mudar visualização para Cadastro
                mvwMenu.ActiveViewIndex = 1;

                //Visualiza Botões Corretos
                btnSalvar.Visible = false;
                btnAlterar.Visible = true;
            }
            else if (e.CommandName == "Status")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string id = commandArgs[0];
                string status = commandArgs[1];

                MenuBLL bll = new MenuBLL();

                Resultado retorno = bll.AlterarStatus(int.Parse(id), int.Parse(status));

                if (retorno.Ok)
                {
                    CarregaGrid(new MenuBLL().Consultar(new IntegWeb.Entidades.Menu()));
                }
                else
                {
                    objB.MostraMensagemTelaUpdatePanel(upMenu, retorno.Mensagem);
                }
            }
        }

        protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarMenuPai();
        }

        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarMenuPai();
        }

        protected void btnPesquisaMenu_Click(object sender, EventArgs e)
        {
            IntegWeb.Entidades.Menu objM = new IntegWeb.Entidades.Menu();
            string msg = "";
            int number;
            bool isErro = ValidaCamposTela(txtPequisaMenu, drpMenu, out msg);

            if (isErro)
            {
                objB.MostraMensagemTelaUpdatePanel(upMenu, msg.ToString());
            }
            else
            {
                switch (drpMenu.SelectedValue)
                {
                    case "1":

                        if (int.TryParse(txtPequisaMenu.Text, out number))
                        {
                            objM.Codigo = number;
                        }
                        else
                            objB.MostraMensagemTelaUpdatePanel(upMenu, " Erro! \\nDigite apenas Números!");
                        break;
                    case "2":
                        objM.Nome = txtPequisaMenu.Text;
                        break;
                    case "3":
                        objM.Sistema.Nome = txtPequisaMenu.Text;
                        break;
                    case "4":
                        if (int.TryParse(txtPequisaMenu.Text, out number))
                        {
                            objM.Nivel = short.Parse(number.ToString());
                        }
                        else
                            objB.MostraMensagemTelaUpdatePanel(upMenu, " Erro! \\nDigite apenas Números!");
                        break;
                    case "5":

                        objM.Link = txtPequisaMenu.Text;

                        break;
                    default:
                        if (int.TryParse(txtPequisaMenu.Text, out number))
                        {
                            objM.Status = number;
                        }
                        else
                            objB.MostraMensagemTelaUpdatePanel(upMenu, " Erro! \\n\\nDigite apenas Números!\\n0=INATIVO\\n1=ATIVO");
                        break;
                }

                CarregaGrid(new MenuBLL().Consultar(objM));

            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            drpMenu.SelectedValue = "0";
            txtPequisaMenu.Text = "";
            CarregaGrid(new MenuBLL().Consultar(new IntegWeb.Entidades.Menu()));
        }
        #endregion

        #region Métodos
        private void CarregaGrid(DataTable dt)
        {
            ViewState["grvMenu"] = dt;
            grvMenu.DataSource = dt;
            grvMenu.DataBind();
        }

        private void CarregarSistema()
        {
            ddlSistema.DataSource = new SistemaBLL().Listar();
            ddlSistema.DataValueField = "Codigo";
            ddlSistema.DataTextField = "Nome";
            ddlSistema.DataBind();

            ddlSistema.Items.Insert(0, new ListItem("Selecione...", ""));
        }

        private void CarregarMenuPai()
        {
            if (ddlSistema.SelectedValue == string.Empty)
                return;

            byte codigoSistema = Convert.ToByte(ddlSistema.SelectedValue);
            short nivel = Convert.ToInt16(ddlNivel.SelectedValue);

            MenuBLL bll = new MenuBLL();

            if (nivel == 1)
            {
                ddlMenuPai.Items.Clear();
                ddlMenuPai.Items.Add("Selecione o Sistema e o Nivel");
                ddlMenuPai.Enabled = false;
            }
            else
            {
                --nivel;
                ddlMenuPai.DataSource = bll.ListarPorNivel(codigoSistema, nivel, null);
                ddlMenuPai.DataValueField = "Codigo";
                ddlMenuPai.DataTextField = "Nome";
                ddlMenuPai.DataBind();

                ddlMenuPai.Items.Insert(0, new ListItem("Selecione...", ""));

                ddlMenuPai.Enabled = true;
            }
        }

        private void CarregaTela()
        {


            CarregaGrid(new MenuBLL().Consultar(new IntegWeb.Entidades.Menu()));

            CarregarSistema();

            CarregarMenuPai();

            //posiciona na primeira visão (lista)
            mvwMenu.ActiveViewIndex = 0;

        }

        private void LimpaCampos()
        {

            txtLink.Text = "";
            txtNome.Text = "";
            ddlMenuPai.Items.Clear();
            ddlNivel.SelectedIndex = 0;
            ddlSistema.SelectedIndex = 0;

        }

        private bool ValidaCamposTela(TextBox text, DropDownList drp, out string mensagem)
        {

            StringBuilder msg = new StringBuilder();
            msg.Append("ERRO!\\n");
            bool isErro = false;
            if (drp.SelectedIndex < 1)
            {
                msg.Append("1 Selecione uma opção de busca.\\n");
                isErro = true;
            }

            if (text.Text == "")
            {
                msg.Append("2 Digite no campo de busca! ");
                isErro = true;
            }
            mensagem = msg.ToString();
            return isErro;
        }
        #endregion
    }
}