using IntegWeb.Entidades.Saude.Auditoria;
using IntegWeb.Saude.Aplicacao.BLL.Auditoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class RegraControleCRC : BasePage
    {
        #region Atributos
        RegraCRC obj = new RegraCRC();
        RegraCRCBLL objB = new RegraCRCBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTela();
            }

        }

        protected void btnInserirRegra_Click(object sender, EventArgs e)
        {
            string mensagem = "";
            decimal valor = 0;
            obj.des_regra = txtDescr.Text;

            if (decimal.TryParse(TxtVl.Text,out valor))
            {
                obj.valor =valor;
            }
            
           

            if (objB.ValidaCampos(out mensagem, obj, false))
            {
                LimpaCampos();
                ListaGrid(new RegraCRC());
            }

            MostraMensagemTelaUpdatePanel(upRegra, mensagem);
            DivAcao(divAction, divSelect);
        }

        protected void grdRegra_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdRegra.EditIndex = -1;
            ListaGrid(new RegraCRC());
        }

        protected void grdRegra_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(grdRegra.DataKeys[e.RowIndex].Value.ToString());

            string mensagem = "";
            bool ret = objB.Deletar(out mensagem, new RegraCRC() { id_regra = id });
            MostraMensagemTelaUpdatePanel(upRegra, mensagem);

            if (ret)
            {
                ListaGrid(new RegraCRC());
            }
        }

        protected void grdRegra_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdRegra.EditIndex = e.NewEditIndex;
            ListaGrid(new RegraCRC());

        }

        protected void grdRegra_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            int id = int.Parse(grdRegra.DataKeys[e.RowIndex].Value.ToString());
            string mensagem = "";
            TextBox txtDescricao = grdRegra.Rows[index].FindControl("txtDescricao") as TextBox;
            TextBox txtValor = grdRegra.Rows[index].FindControl("txtValor") as TextBox;
            decimal valor = 0;

            if (decimal.TryParse(txtValor.Text, out valor))
            {
                obj.valor = valor;
            }
            obj.id_regra = id;
            obj.des_regra = txtDescricao.Text;
            bool ret = objB.ValidaCampos(out mensagem, obj, true);
            MostraMensagemTelaUpdatePanel(upRegra, mensagem);
            if (ret)
            {
                grdRegra.EditIndex = -1;
                ListaGrid(new RegraCRC());
            }
            else
                MostraMensagemTelaUpdatePanel(upRegra, "Problemas contate o administrador do sistema!");
        }

        protected void grdRegra_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdRegra"] != null)
            {
                grdRegra.PageIndex = e.NewPageIndex;
                grdRegra.DataSource = ViewState["grdRegra"];
                grdRegra.DataBind();
            }
        }

        protected void lnkInserirRegra_Click(object sender, EventArgs e)
        {
            DivAcao(divAction, divSelect);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, divAction);
        }
        #endregion

        #region Métodos
        private void CarregaTela()
        {
            ListaGrid(new RegraCRC());
        }

        private void ListaGrid(RegraCRC obj)
        {
            DataTable dt = objB.ListaTodos(obj);
            ViewState["grdRegra"] = dt;
            CarregarGridView(grdRegra, dt);
            DivAcao(divSelect, divAction);
        }

        private void LimpaCampos()
        {
            txtDescr.Text = "";
            TxtVl.Text = "";
        }

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }
        #endregion

    }
}