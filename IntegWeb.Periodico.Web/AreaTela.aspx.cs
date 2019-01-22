using IntegWeb.Entidades;
using IntegWeb.Entidades.Periodico;
using IntegWeb.Saude.Aplicacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Periodico.Web
{
    public partial class AreaTela : BasePage
    {
        #region Atributos
        string mensagem = "";
        AreaBLL objB = new AreaBLL();
        Area objM = new Area();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CarregaTela();
                if (Session["Mensagem"] != null)
                {
                    MostraMensagemTelaUpdatePanel(upArea, Session["Mensagem"].ToString());
                    Session.Remove("Mensagem");
                }

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!txtSigla.Text.Equals(""))
            {
                OrgaoBLL objO = new OrgaoBLL();

                DataTable dt = objO.ListaTodos(new Orgao() { cod_orgao = txtSigla.Text });

                if (dt.Rows.Count > 0)
                {
                    txtDescricao.Text = dt.Rows[0]["SETOR"].ToString();
                    txtResponsavel.Text = dt.Rows[0]["RESPONSAVEL"].ToString();
                }
                else
                {
                    txtDescricao.Text = "";
                    txtResponsavel.Text = "";
                    MostraMensagemTelaUpdatePanel(upArea, "Setor não encontrado");

                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upArea, "Informe a Sigla");
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool isUpdate = false;
            objM.andar = txtAndar.Text;
            objM.descricao = txtDescricao.Text;
            objM.edificio = txtEdificio.Text;
            objM.responsavel = txtResponsavel.Text;
            objM.sigla = txtSigla.Text;


            if (Session["id"] != null)
            {
                objM.id_area = int.Parse(Session["id"].ToString());
                isUpdate = true;
            }
            if (objB.ValidaCampos(out mensagem, objM, isUpdate))
            {
                Session["mensagem"] = mensagem;
                Session.Remove("id");
                Response.Redirect("AreaTela.aspx");
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upArea, mensagem);
            }
        }

        protected void gridArea_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Atualizar":

                    DataTable dt = objB.ListaTodos(new Area() { id_area = int.Parse(e.CommandArgument.ToString()) });

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtAndar.Text = row["ANDAR"].ToString();
                       // txtCodigo.Text = row["CODIGO"].ToString();
                        txtDescricao.Text = row["DESCRICAO"].ToString();
                        txtEdificio.Text = row["EDIFICIO"].ToString();
                        txtResponsavel.Text = row["RESPONSAVEL"].ToString();
                        txtSigla.Text = row["SIGLA"].ToString();
                        Session["id"] = e.CommandArgument.ToString();
                        DivAcao(divAction, divSelect);
                    }
                    break;

                case "Deletar":

                    string mensagem = "";
                    bool ret = objB.Deletar(new Area() { id_area = int.Parse(e.CommandArgument.ToString()) }, out mensagem);
                    MostraMensagemTelaUpdatePanel(upArea, mensagem);

                    if (ret)
                    {
                        ListaGrid(new Area());
                    }
                    break;
                default:
                    break;
            }
        }

        protected void gridArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["gridArea"] != null)
            {
                gridArea.PageIndex = e.NewPageIndex;
                gridArea.DataSource = ViewState["gridArea"];
                gridArea.DataBind();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool isErro = ValidaCamposTela(txtArea, drpArea, out msg);


            if (isErro)
            {
                MostraMensagemTelaUpdatePanel(upArea, msg.ToString());
            }
            else
            {
                if (drpArea.SelectedValue == "1")
                    objM.sigla = txtArea.Text;
                else if (drpArea.SelectedValue == "2")
                    objM.descricao = txtArea.Text;


                ListaGrid(objM);
            }
            DivAcao(divSelect, divAction);
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            ListaGrid(new Area());
        }

        protected void lnkInserirGrupo_Click(object sender, EventArgs e)
        {
            if (Session["id"] != null)
                Session.Remove("id");
            LimpaCampos();
            DivAcao(divAction, divSelect);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, divAction);
        }

        #endregion

        #region Métodos
        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        private void ListaGrid(Area obj)
        {

            DataTable dt = objB.ListaTodos(obj);
            ViewState["gridArea"] = dt;
            CarregarGridView(gridArea, dt);

        }

        private void CarregaTela()
        {

            DivAcao(divSelect, divAction);
            ListaGrid(new Area());
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

        private void LimpaCampos()
        {
            drpArea.SelectedValue = "0";
            txtAndar.Text = "";
          //  txtCodigo.Text = "";
            txtDescricao.Text = "";
            txtEdificio.Text = "";
            txtResponsavel.Text = "";
            txtSigla.Text = "";


        }
        #endregion 
    }
}