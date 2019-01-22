using IntegWeb.Previdencia.Aplicacao.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class BeneficiarioTipo : BasePage
    {
        #region Atributos
        TipoBeneficioBLL obj = new TipoBeneficioBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTela();
            }
        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                string mensagem = "";
                obj.descricao = txtBenef.Text;


                if (obj.ValidaCampos(out mensagem, false))
                {
                    ListaGrid();
                }

                MostraMensagemTelaUpdatePanel(upPanel, mensagem);
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upPanel, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }

        }

        protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grd.EditIndex = -1;
            ListaGrid();
        }

        protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = int.Parse(grd.DataKeys[e.RowIndex].Value.ToString());

                string mensagem = "";
                obj.id_tpbeneficio = id;
                bool ret = obj.DeletarDados(out mensagem);
                MostraMensagemTelaUpdatePanel(upPanel, mensagem);

                if (ret)
                {
                    obj.id_tpbeneficio = null;
                    ListaGrid();
                }

            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upPanel, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }

        }

        protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grd.EditIndex = e.NewEditIndex;
            ListaGrid();
        }

        protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                int index = e.RowIndex;
                int id = int.Parse(grd.DataKeys[e.RowIndex].Value.ToString());
                string mensagem = "";
                TextBox txtP = grd.Rows[index].FindControl("txtTexto") as TextBox;

                obj.id_tpbeneficio = id;
                obj.descricao = txtP.Text;

                bool ret = obj.ValidaCampos(out mensagem, true);

                MostraMensagemTelaUpdatePanel(upPanel, mensagem);
                if (ret)
                {
                    grd.EditIndex = -1;
                    obj.id_tpbeneficio = null;
                    ListaGrid();
                }
                else
                    MostraMensagemTelaUpdatePanel(upPanel, "Problemas contate o administrador do sistema!");
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upPanel, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grd"] != null)
            {
                grd.PageIndex = e.NewPageIndex;
                grd.DataSource = ViewState["grd"];
                grd.DataBind();
            }
        }
        #endregion

        #region Metodos
        private void CarregaTela()
        {
            ListaGrid();
        }
        private void ListaGrid()
        {
            DataTable dt = obj.ListaDados();
            ViewState["grd"] = dt;
            CarregarGridView(grd, dt);

        }
        #endregion
    }
}