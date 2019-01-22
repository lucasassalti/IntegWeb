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
    public partial class OrçamentoTela : BasePage
    {
        #region Atributos
        ItemOrcBLL objbll = new ItemOrcBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e){

            if (!IsPostBack)
            {
                CarregaTela();
            }
        
        }

        protected void grdOrcamento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdOrcamento.EditIndex = -1;
            ListaGrid();
        }

        protected void grdOrcamento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                string mensagem = "";

                objbll.cod_emprs = int.Parse(grdOrcamento.DataKeys[e.RowIndex].Values["COD_EMPRS"].ToString());
                objbll.cod_plano = int.Parse(grdOrcamento.DataKeys[e.RowIndex].Values["COD_PLANO"].ToString());
                objbll.cod_natureza_ct = grdOrcamento.DataKeys[e.RowIndex].Values["COD_NATUREZA_CT"].ToString();
                objbll.cod_classe = int.Parse(grdOrcamento.DataKeys[e.RowIndex].Values["COD_CLASSE"].ToString());
                objbll.idc_convenio_esp = grdOrcamento.DataKeys[e.RowIndex].Values["IDC_CONVENIO_ESP"].ToString();

                TextBox txtNomAbrvoEmprs = grdOrcamento.Rows[index].FindControl("txtNomAbrvoEmprs") as TextBox;
                TextBox txtNomRzsocEmprs = grdOrcamento.Rows[index].FindControl("txtNomRzsocEmprs") as TextBox;
                TextBox txtTipoItemOrc = grdOrcamento.Rows[index].FindControl("txtTipoItemOrc") as TextBox;
                TextBox txtTipoItemOrcam = grdOrcamento.Rows[index].FindControl("txtTipoItemOrcam") as TextBox;
                TextBox txtConsolidaPlano = grdOrcamento.Rows[index].FindControl("txtConsolidaPlano") as TextBox;
                TextBox txtCodEmprsCt = grdOrcamento.Rows[index].FindControl("txtCodEmprsCt") as TextBox;
                TextBox txtCodPlanoCt = grdOrcamento.Rows[index].FindControl("txtCodPlanoCt") as TextBox;
                TextBox txtDescPlano = grdOrcamento.Rows[index].FindControl("txtDescPlano") as TextBox;
                TextBox txtDescNatureza = grdOrcamento.Rows[index].FindControl("txtDescNatureza") as TextBox;
                TextBox txtDesClasse = grdOrcamento.Rows[index].FindControl("txtDesClasse") as TextBox;

                objbll.nom_abrvo_emprs = txtNomAbrvoEmprs.Text;
                objbll.nom_rzsoc_emprs = txtNomRzsocEmprs.Text;
                objbll.tipo_item_orc = txtTipoItemOrc.Text;
                objbll.item_orcamentario = txtTipoItemOrcam.Text;
                objbll.consolida_plano = txtConsolidaPlano.Text;
                objbll.cod_emprs_ct = int.Parse(txtCodEmprsCt.Text.Equals("") ? "0" : txtCodEmprsCt.Text);
                objbll.cod_plano_ct = int.Parse(txtCodPlanoCt.Text.Equals("") ? "0" : txtCodPlanoCt.Text);
                objbll.desc_plano = txtDescPlano.Text;
                objbll.desc_natureza = txtDescNatureza.Text;
                objbll.des_classe = txtDesClasse.Text;


                bool ret = objbll.Atualizar( out mensagem);

                if (ret)
                {
                    grdOrcamento.EditIndex = -1;
                    CarregaGrid("grdOrcamento", objbll.ListaDados(), grdOrcamento);
                }

                MostraMensagemTelaUpdatePanel(upOrcamento, mensagem);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upOrcamento, "Atenção Erro!!\\nMotivo:\\n" + ex.Message);
            }
        }

        protected void grdOrcamento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdOrcamento.EditIndex = e.NewEditIndex;
            ListaGrid();
        }

        protected void grdOrcamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdOrcamento"] != null)
            {
                grdOrcamento.PageIndex = e.NewPageIndex;
                grdOrcamento.DataSource = ViewState["grdFusao"];
                grdOrcamento.DataBind();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {

                string mensagem;
                objbll.idc_convenio_esp = drpTipoConvenio.Text;
                objbll.cod_classe = int.Parse(txtCodClasse.Text.Equals("") ? "0" : txtCodClasse.Text);
                objbll.cod_emprs = int.Parse(txtCodEmpresa.Text.Equals("") ? "0" : txtCodEmpresa.Text);
                objbll.cod_natureza_ct = txtCodNatureza.Text;
                objbll.cod_plano = int.Parse(txtCodPlano.Text.Equals("") ? "0" : txtCodPlano.Text);

                DataTable dt = objbll.ListaDados();
        
                CarregaGrid("grdOrcamento", dt, grdOrcamento);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upOrcamento, "Atenção Erro!!\\nMotivo:\\n" + ex.Message);
            }


           
        }
        #endregion

        #region Métodos
        private void CarregaTela(){

            CarregaDropDowDT(objbll.ListaCombo(), drpTipoConvenio);
        
        }

        private void ListaGrid()
        {

            if (ViewState["grdOrcamento"] != null)
            {
                DataTable dt = (DataTable)ViewState["grdOrcamento"];
                CarregaGrid("grdOrcamento", dt, grdOrcamento);
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