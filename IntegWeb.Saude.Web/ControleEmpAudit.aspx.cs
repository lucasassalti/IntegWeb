using IntegWeb.Entidades.Saude.Auditoria;
using IntegWeb.Saude.Aplicacao.BLL.Auditoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class ControleEmpAudit : BasePage
    {
        #region Atributos
        EmpAudit objM = new EmpAudit();
        EmpAuditBLL objB = new EmpAuditBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTela();
            }
        }

        protected void btnInserirEmpresa_Click(object sender, EventArgs e)
        {
            string mensagem = "";
            objM.descricao = txtEmpresa.Text;


            if (objB.ValidaCampos(out mensagem, objM, false))
            {
                ListaGrid(new EmpAudit());
                txtEmpresa.Text = "";
            }

            MostraMensagemTelaUpdatePanel(upPeriodo, mensagem);

        }

        protected void grdEmpresa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdEmpresa.EditIndex = -1;
            ListaGrid(new EmpAudit());
        }

        protected void grdEmpresa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int id = int.Parse(grdEmpresa.DataKeys[e.RowIndex].Value.ToString());

            string mensagem = "";
            bool ret = new EmpAuditBLL().Deletar(out mensagem, new EmpAudit() { id_empid = id });
            MostraMensagemTelaUpdatePanel(upPeriodo, mensagem);

            if (ret)
            {
                ListaGrid(new EmpAudit());
            }
        }

        protected void grdEmpresa_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdEmpresa.EditIndex = e.NewEditIndex;
            ListaGrid(new EmpAudit());
        }

        protected void grdEmpresa_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int index = e.RowIndex;
            int id = int.Parse(grdEmpresa.DataKeys[e.RowIndex].Value.ToString());
            string mensagem = "";
            TextBox txtP = grdEmpresa.Rows[index].FindControl("txtPer") as TextBox;

            EmpAuditBLL bll = new EmpAuditBLL();

            bool ret = bll.ValidaCampos(out mensagem, new EmpAudit()
            {
                id_empid = id,
                descricao = txtP.Text
            },
                                                               true);
            MostraMensagemTelaUpdatePanel(upPeriodo, mensagem);
            if (ret)
            {
                grdEmpresa.EditIndex = -1;
                ListaGrid(new EmpAudit());
            }
            else
                MostraMensagemTelaUpdatePanel(upPeriodo, "Problemas contate o administrador do sistema!");

        }

        protected void grdEmpresa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdEmpresa"] != null)
            {
                grdEmpresa.PageIndex = e.NewPageIndex;
                grdEmpresa.DataSource = ViewState["grdEmpresa"];
                grdEmpresa.DataBind();
            }
        }
        #endregion

        #region Metodos
        private void CarregaTela()
        {
            ListaGrid(new EmpAudit());
        }
        private void ListaGrid(EmpAudit obj)
        {
            DataTable dt = objB.ListaTodos(obj);
            ViewState["grdEmpresa"] = dt;
            CarregarGridView(grdEmpresa, dt);

        }
        #endregion
    }
}