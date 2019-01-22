using IntegWeb.Entidades.Saude.Controladoria;
using IntegWeb.Saude.Aplicacao.BLL.Controladoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.BLL;
//using StackExchange.Profiling.Data;

namespace IntegWeb.Saude.Web
{
    public partial class CadCentroCusto : BasePage
    {
        #region Atributos
        CentroCusto obj = new CentroCusto();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(grdCCusto.SortExpression))
                grdCCusto.Sort("CCUSTO_DEB_UTIL", SortDirection.Descending);    
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)               
        {
            if (txtNumOrgao.Text.Equals("") && txtCodPlano.Text.Equals(""))
            {
                MostraMensagemTelaUpdatePanel(upOrgao, "Prencha um campo de pesquisa para continuar");
            }
            else grdCCusto.PageIndex = 0; 

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNumOrgao.Text = "";
            txtCodPlano.Text = "";
            grdCCusto.PageIndex = 0;
        }

        protected void grdCCusto_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCCusto.EditIndex = -1;
            grdCCusto.PageIndex = 0; 
        }

        protected void grdCCusto_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCCusto.EditIndex = e.NewEditIndex;
        }

        #endregion

    }
}