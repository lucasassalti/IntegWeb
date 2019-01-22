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
    public partial class InssTela : BasePage
    {
        #region Atributos
        ItemOrcInssBLL objbll = new ItemOrcInssBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdInss_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdInss.EditIndex = -1;
            ListaGrid();
        }

        protected void grdInss_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdInss.EditIndex = e.NewEditIndex;
            ListaGrid();
        }

        protected void grdInss_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                string mensagem = "";

                objbll.cod_emprs = int.Parse(grdInss.DataKeys[e.RowIndex].Values["COD_EMPRS"].ToString());
                objbll.cod_plano = int.Parse(grdInss.DataKeys[e.RowIndex].Values["COD_PLANO"].ToString());
                


                TextBox txtCodNatureza = grdInss.Rows[index].FindControl("txtCodNatureza") as TextBox;
                TextBox txtNomAbrvoEmprs = grdInss.Rows[index].FindControl("txtNomAbrvoEmprs") as TextBox;
                TextBox txtNomRzsocEmprs = grdInss.Rows[index].FindControl("txtNomRzsocEmprs") as TextBox;
                TextBox txtTipoItemOrc = grdInss.Rows[index].FindControl("txtTipoItemOrc") as TextBox;
                TextBox txtTipoItemOrcam = grdInss.Rows[index].FindControl("txtTipoItemOrcam") as TextBox;
                TextBox txtConsolidaPlano = grdInss.Rows[index].FindControl("txtConsolidaPlano") as TextBox;
                TextBox txtCodEmprsCt = grdInss.Rows[index].FindControl("txtCodEmprsCt") as TextBox;
                TextBox txtCodPlanoCt = grdInss.Rows[index].FindControl("txtCodPlanoCt") as TextBox;
                TextBox txtDescPlano = grdInss.Rows[index].FindControl("txtDescPlano") as TextBox;
                TextBox txtDescNatureza = grdInss.Rows[index].FindControl("txtDescNatureza") as TextBox;
                TextBox txtDesClasse = grdInss.Rows[index].FindControl("txtDesClasse") as TextBox;

                objbll.cod_natureza_ct = txtCodNatureza.Text;
                objbll.nom_abrvo_emprs = txtNomAbrvoEmprs.Text;
                objbll.nom_rzsoc_emprs = txtNomRzsocEmprs.Text;
                objbll.tipo_item_orc = txtTipoItemOrc.Text;
                objbll.item_orcamentario = txtTipoItemOrcam.Text;
                objbll.consolida_plano = txtConsolidaPlano.Text;
                objbll.cod_emprs_ct = int.Parse(txtCodEmprsCt.Text.Equals("") ? "0" : txtCodEmprsCt.Text);
                objbll.cod_plano_ct = int.Parse(txtCodPlanoCt.Text.Equals("") ? "0" : txtCodEmprsCt.Text);
                objbll.desc_plano = txtDescPlano.Text;
                objbll.desc_natureza = txtDescNatureza.Text;


                bool ret = objbll.Atualizar( out mensagem);

                if (ret)
                {
                    grdInss.EditIndex = -1;
                    CarregaGrid("grdInss", objbll.ListaDados(), grdInss);
                }

                MostraMensagemTelaUpdatePanel(upInss, mensagem);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upInss, "Atenção Erro!!\\nMotivo:\\n" + ex.Message);
            }
        }

        protected void grdInss_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdInss"] != null)
            {
                grdInss.PageIndex = e.NewPageIndex;
                grdInss.DataSource = ViewState["grdInss"];
                grdInss.DataBind();
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

                CarregaGrid("grdInss", dt, grdInss);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upInss, "Atenção Erro!!\\nMotivo:\\n" + ex.Message);
            }



        }
        #endregion

        #region Métodos


        private void ListaGrid()
        {

            if (ViewState["grdInss"] != null)
            {
                DataTable dt = (DataTable)ViewState["grdInss"];
                CarregaGrid("grdInss", dt, grdInss);
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