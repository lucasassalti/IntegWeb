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
    public partial class IRTela : BasePage
    {
        #region Atributos
        ItemOrcIRBLL objbll = new ItemOrcIRBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }
        protected void grdIR_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdIR.EditIndex = -1;
            ListaGrid();
        }

        protected void grdIR_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdIR.EditIndex = e.NewEditIndex;
            ListaGrid();
        }

        protected void grdIR_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                string mensagem = "";

                objbll.cod_emprs = int.Parse(grdIR.DataKeys[e.RowIndex].Values["COD_EMPRS"].ToString());
                objbll.cod_plano = int.Parse(grdIR.DataKeys[e.RowIndex].Values["COD_PLANO"].ToString());



                TextBox txtCodNatureza = grdIR.Rows[index].FindControl("txtCodNatureza") as TextBox;
                TextBox txtNomAbrvoEmprs = grdIR.Rows[index].FindControl("txtNomAbrvoEmprs") as TextBox;
                TextBox txtNomRzsocEmprs = grdIR.Rows[index].FindControl("txtNomRzsocEmprs") as TextBox;
                TextBox txtTipoItemOrc = grdIR.Rows[index].FindControl("txtTipoItemOrc") as TextBox;
                TextBox txtTipoItemOrcam = grdIR.Rows[index].FindControl("txtTipoItemOrcam") as TextBox;
                TextBox txtConsolidaPlano = grdIR.Rows[index].FindControl("txtConsolidaPlano") as TextBox;
                TextBox txtCodEmprsCt = grdIR.Rows[index].FindControl("txtCodEmprsCt") as TextBox;
                TextBox txtCodPlanoCt = grdIR.Rows[index].FindControl("txtCodPlanoCt") as TextBox;
                TextBox txtDescPlano = grdIR.Rows[index].FindControl("txtDescPlano") as TextBox;
                TextBox txtDescNatureza = grdIR.Rows[index].FindControl("txtDescNatureza") as TextBox;
                TextBox txtDesClasse = grdIR.Rows[index].FindControl("txtDesClasse") as TextBox;

                objbll.cod_natureza_ct = txtCodNatureza.Text;
                objbll.nom_abrvo_emprs = txtNomAbrvoEmprs.Text;
                objbll.nom_rzsoc_emprs = txtNomRzsocEmprs.Text;
                objbll.tipo_item_orc = txtTipoItemOrc.Text;
                objbll.item_orcamentario = txtTipoItemOrcam.Text;
                objbll.consolida_plano = txtConsolidaPlano.Text;
                objbll.cod_emprs_ct = int.Parse(txtCodEmprsCt.Text.Equals("") ? "0" : txtCodEmprsCt.Text);
                objbll.cod_plano_ct = int.Parse(txtCodPlanoCt.Text.Equals("") ? "0" : txtCodPlanoCt.Text);
                objbll.desc_plano = txtDescPlano.Text;
                objbll.desc_natureza = txtDescNatureza.Text;


                bool ret = objbll.Atualizar(out mensagem);

                if (ret)
                {
                    grdIR.EditIndex = -1;
                    CarregaGrid("grdIR", objbll.ListaDados(), grdIR);
                }

                MostraMensagemTelaUpdatePanel(upIr, mensagem);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upIr, "Atenção Erro!!\\nMotivo:\\n" + ex.Message);
            }
        }

        protected void grdIR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdIR"] != null)
            {
                grdIR.PageIndex = e.NewPageIndex;
                grdIR.DataSource = ViewState["grdIR"];
                grdIR.DataBind();
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

                CarregaGrid("grdIR", dt, grdIR);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upIr, "Atenção Erro!!\\nMotivo:\\n" + ex.Message);
            }



        }
        #endregion

        #region Métodos
        private void ListaGrid()
        {

            if (ViewState["grdIR"] != null)
            {
                DataTable dt = (DataTable)ViewState["grdIR"];
                CarregaGrid("grdIR", dt, grdIR);
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