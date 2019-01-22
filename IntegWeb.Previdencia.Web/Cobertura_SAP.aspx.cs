using IntegWeb.Entidades.Saude.Controladoria;
//using IntegWeb.Previdencia.Aplicacao.BLL.Controladoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using IntegWeb.Saude.Aplicacao.ENTITY;
//using IntegWeb.Saude.Aplicacao.BLL;
//using StackExchange.Profiling.Data;

namespace IntegWeb.Previdencia.Web
{
    public partial class CoberturaSAP : BasePage
    {
        #region Atributos
        CentroCusto obj = new CentroCusto();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(grdCoberturaSAP.SortExpression))
              //  grdCoberturaSAP.Sort("data_solic", SortDirection.Descending);    
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)               
        {
            if (txtEmpresa.Text.Equals("") && txtMatricula.Text.Equals("") && txtNumChamado.Text.Equals("") && txtDtAbertura.Text.Equals(""))
            {
                MostraMensagemTelaUpdatePanel(upOrgao, "Prencha um campo de pesquisa para continuar");
            }
            else
            {
                grdCoberturaSAP.Visible = true;
                grdCoberturaSAP.PageIndex = 0;
            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtEmpresa.Text = "";
            txtMatricula.Text = "";
            txtNumChamado.Text = "";
            txtDtAbertura.Text = "";
            grdCoberturaSAP.PageIndex = 0;
        }

        protected void grdCoberturaSAP_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCoberturaSAP.EditIndex = -1;
            grdCoberturaSAP.PageIndex = 0; 
        }

        protected void grdCoberturaSAP_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCoberturaSAP.EditIndex = e.NewEditIndex;
        }

        #endregion

    }
}