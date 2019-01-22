using IntegWeb.Entidades;
using IntegWeb.Periodico.Aplicacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Periodico.Web
{
    public partial class PeriodoTela : BasePage
    {
        #region Atributos
        PeriodoPeriodico objM = new PeriodoPeriodico();
        PeriodoPeriodicoBLL objB = new PeriodoPeriodicoBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTela();
            }
        }

        protected void btnInserirPeiriodo_Click(object sender, EventArgs e)
        {
            string mensagem = "";
            objM.desc_periodo = txtPeriodo.Text;


            if (objB.ValidaCampos(out mensagem, objM, false))
            {
                ListaGrid(new PeriodoPeriodico());
            }

            MostraMensagemTelaUpdatePanel(upPeriodo, mensagem);

        }

        protected void grdPeriodo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdPeriodo.EditIndex = -1;
            ListaGrid(new PeriodoPeriodico());
        }

        protected void grdPeriodo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int id = int.Parse(grdPeriodo.DataKeys[e.RowIndex].Value.ToString());

            string mensagem = "";
            bool ret = new PeriodoPeriodicoBLL().Deletar(out mensagem, new PeriodoPeriodico() { cod_periodico = id });
            MostraMensagemTelaUpdatePanel(upPeriodo, mensagem);

            if (ret)
            {
                ListaGrid(new PeriodoPeriodico());
            }
        }

        protected void grdPeriodo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdPeriodo.EditIndex = e.NewEditIndex;
            ListaGrid(new PeriodoPeriodico());
        }

        protected void grdPeriodo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
   
                int index = e.RowIndex;
                int id = int.Parse(grdPeriodo.DataKeys[e.RowIndex].Value.ToString());
                string mensagem = "";
                TextBox txtP = grdPeriodo.Rows[index].FindControl("txtPer") as TextBox;

                PeriodoPeriodicoBLL bll = new PeriodoPeriodicoBLL();

                bool ret = bll.ValidaCampos(out mensagem, new PeriodoPeriodico()
                {
                    cod_periodico = id,
                    desc_periodo = txtP.Text
                },
                                                                   true);
                MostraMensagemTelaUpdatePanel(upPeriodo, mensagem);
                if (ret)
                {
                    grdPeriodo.EditIndex = -1;
                    ListaGrid(new PeriodoPeriodico());
                }
                else
                    MostraMensagemTelaUpdatePanel(upPeriodo, "Problemas contate o administrador do sistema!");
            
        }

        protected void grdPeriodo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdPeriodo"] != null)
            {
                grdPeriodo.PageIndex = e.NewPageIndex;
                grdPeriodo.DataSource = ViewState["grdPeriodo"];
                grdPeriodo.DataBind();
            }
        }
        #endregion

        #region Metodos
        private void CarregaTela()
        {
            ListaGrid(new PeriodoPeriodico());
        }
        private void ListaGrid(PeriodoPeriodico obj)
        {
            DataTable dt = objB.ListaTodos(obj);
            ViewState["grdPeriodo"] = dt;
            CarregarGridView(grdPeriodo, dt);

        }
        #endregion
    }
}