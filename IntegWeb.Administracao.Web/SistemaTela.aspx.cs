using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IntegWeb.Administracao.Aplicacao;
using IntegWeb.Entidades;
using IntegWeb.Framework;

namespace IntegWeb.Administracao.Web
{
    public partial class SistemaTela : System.Web.UI.Page
    {
        BasePage objPage = new BasePage();
        private void CarregarLista()
        {
            SistemaBLL bll = new SistemaBLL();

            grvSistema.DataSource = bll.Listar();
            grvSistema.DataBind();
        }

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarLista();

                //posiciona na primeira visão (lista)
                mvwSistema.ActiveViewIndex = 0;
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            //posiciona na visão cadastro
            mvwSistema.ActiveViewIndex = 1;

            //limpar os campos
            txtCodigo.Text =
                txtNome.Text = string.Empty;

            //posicionar o foco inicial
            txtCodigo.Enabled = true;
            txtNome.Focus();

            //Visualiza botões corretos
            btnSalvar.Visible = true;
            btnAlterar.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            Sistema objSistema = new Sistema();

            objSistema.Nome = txtNome.Text;

            SistemaBLL bll = new SistemaBLL();

            Resultado retorno = bll.Incluir(objSistema);

            if (retorno.Ok)
            {
                CarregarLista();

                //retorna visão de lista
                mvwSistema.ActiveViewIndex = 0;

                objPage.MostraMensagemTelaUpdatePanel(upSistema,"Cadastro realizado com sucesso!");
            }
            else
            {
                objPage.MostraMensagemTelaUpdatePanel(upSistema, retorno.Mensagem);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mvwSistema.ActiveViewIndex = 0;
        }

        protected void grvSistema_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            byte codigoSelecionado;

            if (e.CommandName == "EDITAR")
            {
                codigoSelecionado = Convert.ToByte(e.CommandArgument);

                //Consulta objeto no Banco de Dados
                SistemaBLL bll = new SistemaBLL();
                Sistema objConsulta = bll.Consultar(codigoSelecionado);

                //Preenche dados da Tela
                txtCodigo.Text = objConsulta.Codigo.ToString();
                txtNome.Text = objConsulta.Nome;

                //Posiciona o foco inicial
                txtCodigo.Enabled = false;
                txtNome.Focus();

                //Mudar visualização para Cadastro
                mvwSistema.ActiveViewIndex = 1;

                //Visualiza Botões Corretos
                btnSalvar.Visible = false;
                btnAlterar.Visible = true;
            }
            else if (e.CommandName == "STATUS")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string id = commandArgs[0];
                string status = commandArgs[1];

                SistemaBLL bll = new SistemaBLL();

                Resultado retorno = bll.AlterarStatus(byte.Parse(id), int.Parse(status));

                if (retorno.Ok)
                {
                    CarregarLista();
                }
                else
                {
                    objPage.MostraMensagemTelaUpdatePanel(upSistema, retorno.Mensagem);
                }
            }
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            Sistema objSistema = new Sistema();

            SistemaBLL bll = new SistemaBLL();

            objSistema.Codigo = Convert.ToByte(txtCodigo.Text);
            objSistema.Nome = txtNome.Text;

            Resultado retorno = bll.Alterar(objSistema);

            if (retorno.Ok)
            {
                CarregarLista();

                //Posiciona na visão de Listagem
                mvwSistema.ActiveViewIndex = 0;

                objPage.MostraMensagemTelaUpdatePanel(upSistema, "Cadastro alterado com sucesso!");
            }
            else
            {
                objPage.MostraMensagemTelaUpdatePanel(upSistema, retorno.Mensagem);
            }
        }

        protected void grvSistema_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvSistema.PageIndex = e.NewPageIndex;

            CarregarLista();
        }
    }
}