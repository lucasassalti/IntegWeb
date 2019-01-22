using IntegWeb.Saude.Aplicacao.BLL.Controladoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class ReembolsoTela : BasePage
    {
        #region Atributos
        ItemoOrcReembBLL objbll = new ItemoOrcReembBLL();
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdReembolso_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdReembolso.EditIndex = -1;
            ListaGrid();
        }

        protected void grdReembolso_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdReembolso.EditIndex = e.NewEditIndex;
            ListaGrid();
        }

        protected void grdReembolso_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                string mensagem = "";

                objbll.cod_emprs = int.Parse(grdReembolso.DataKeys[e.RowIndex].Values["COD_EMPRS"].ToString());
                objbll.cod_plano = int.Parse(grdReembolso.DataKeys[e.RowIndex].Values["COD_PLANO"].ToString());



                TextBox txtCodNatureza = grdReembolso.Rows[index].FindControl("txtCodNatureza") as TextBox;
                TextBox txtNomAbrvoEmprs = grdReembolso.Rows[index].FindControl("txtNomAbrvoEmprs") as TextBox;
                TextBox txtNomRzsocEmprs = grdReembolso.Rows[index].FindControl("txtNomRzsocEmprs") as TextBox;
                TextBox txtDesItem = grdReembolso.Rows[index].FindControl("txtDesItem") as TextBox;
                TextBox txtTipoItemOrcam = grdReembolso.Rows[index].FindControl("txtTipoItemOrcam") as TextBox;
                TextBox txtSituacao = grdReembolso.Rows[index].FindControl("txtSituacao") as TextBox;
                TextBox txtCodEmprsCt = grdReembolso.Rows[index].FindControl("txtCodEmprsCt") as TextBox;
                TextBox txtCodPlanoCt = grdReembolso.Rows[index].FindControl("txtCodPlanoCt") as TextBox;
                TextBox txtDescPlano = grdReembolso.Rows[index].FindControl("txtDescPlano") as TextBox;
                TextBox txtDescNatureza = grdReembolso.Rows[index].FindControl("txtDescNatureza") as TextBox;
                TextBox txtDesClasse = grdReembolso.Rows[index].FindControl("txtDesClasse") as TextBox;

                objbll.cod_natureza_ct = txtCodNatureza.Text;
                objbll.nom_abrvo_emprs = txtNomAbrvoEmprs.Text;
                objbll.nom_rzsoc_emprs = txtNomRzsocEmprs.Text;
                objbll.desc_item = txtDesItem.Text;
                objbll.item_orcamentario = txtTipoItemOrcam.Text;
                objbll.situacao = txtSituacao.Text;
                objbll.cod_emprs_ct = int.Parse(txtCodEmprsCt.Text.Equals("") ? "0" : txtCodEmprsCt.Text);
                objbll.cod_plano_ct = int.Parse(txtCodPlanoCt.Text.Equals("") ? "0" : txtCodPlanoCt.Text);
                objbll.desc_plano = txtDescPlano.Text;
                objbll.desc_natureza = txtDescNatureza.Text;


                bool ret = objbll.Atualizar(out mensagem);

                if (ret)
                {
                    grdReembolso.EditIndex = -1;
                    CarregaGrid("grdReembolso", objbll.ListaDados(), grdReembolso);
                }

                MostraMensagemTelaUpdatePanel(upReembolso, mensagem);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upReembolso, "Atenção Erro!!\\nMotivo:\\n" + ex.Message);
            }
        }

        protected void grdReembolso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdReembolso"] != null)
            {
                grdReembolso.PageIndex = e.NewPageIndex;
                grdReembolso.DataSource = ViewState["grdReembolso"];
                grdReembolso.DataBind();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {

                string mensagem;

                objbll.cod_emprs = int.Parse(txtCodEmpresa.Text.Equals("") ? "0" : txtCodEmpresa.Text);
                objbll.cod_plano = int.Parse(txtCodPlano.Text.Equals("") ? "0" : txtCodPlano.Text);

                DataTable dt = objbll.ListaDados();

                CarregaGrid("grdReembolso", dt, grdReembolso);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upReembolso, "Atenção Erro!!\\nMotivo:\\n" + ex.Message);
            }



        }
        #endregion
        #region Métodos
        private void ListaGrid()
        {

            if (ViewState["grdReembolso"] != null)
            {
                DataTable dt = (DataTable)ViewState["grdReembolso"];
                CarregaGrid("grdReembolso", dt, grdReembolso);
            }
        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {
            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        #endregion

        
    }
}