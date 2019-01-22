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
    public partial class QtdAtendimentos : BasePage
    {
        #region Atributos
        CentroCusto obj = new CentroCusto();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(grdAtendimento.SortExpression))
                grdAtendimento.Sort("num_matricula", SortDirection.Descending);    
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)               
        {
            if (txtNumMatricula.Text.Equals("") && txtNumSubMatricula.Text.Equals(""))
            { 
                MostraMensagemTelaUpdatePanel(upOrgao, "Prencha um campo de pesquisa para continuar");
            }
            else grdAtendimento.PageIndex = 0; 

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCodEmpresa.Text = "";
            txtNumMatricula.Text = "";
            txtNumSubMatricula.Text = "";
            grdAtendimento.PageIndex = 0;
        }

        protected void grdAtendimento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAtendimento.EditIndex = -1;
            grdAtendimento.PageIndex = 0; 
        }

        protected void grdAtendimento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAtendimento.EditIndex = e.NewEditIndex;
        }

        #endregion

    }
}