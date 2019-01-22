
using IntegWeb.Entidades.Saude.Controladoria;
using IntegWeb.Saude.Aplicacao.BLL.Controladoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class ControlCobranca : BasePage
    {
        #region Atributos
        ItemOrcCobranca obj = new ItemOrcCobranca();
        ItemOrcCobrancaBLL objBLL = new ItemOrcCobrancaBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdCobranca_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCobranca.EditIndex = -1;
            ListaGrid();
        }

        protected void grdCobranca_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCobranca.EditIndex = e.NewEditIndex;
            ListaGrid();
        }

        protected void grdCobranca_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                string mensagem = "";
                obj.Cod_Emprs = int.Parse(grdCobranca.DataKeys[e.RowIndex].Values["Cod_Emprs"].ToString());
                obj.Cod_Grupo = int.Parse(grdCobranca.DataKeys[e.RowIndex].Values["Cod_Grupo"].ToString());
                obj.Cod_Tipo_Comp = int.Parse(grdCobranca.DataKeys[e.RowIndex].Values["Cod_Tipo_Comp"].ToString());
                obj.Cod_Plano = int.Parse(grdCobranca.DataKeys[e.RowIndex].Values["Cod_Plano"].ToString());

                TextBox txtPatroc = grdCobranca.Rows[index].FindControl("txtPatroc") as TextBox;
                TextBox txtCompl = grdCobranca.Rows[index].FindControl("txtCompl") as TextBox;
                TextBox txtSuplem = grdCobranca.Rows[index].FindControl("txtSuplem") as TextBox;
                TextBox txtFcespNat = grdCobranca.Rows[index].FindControl("txtFcespNat") as TextBox;

                obj.Suplem_Natureza = txtSuplem.Text;
                obj.Fcesp_Natureza = txtFcespNat.Text;
                obj.Compl_Natureza = txtCompl.Text;
                obj.Patroc_Natureza = txtPatroc.Text;

                bool ret = objBLL.Atualizar(obj, out mensagem);

                if (ret)
                {
                    grdCobranca.EditIndex = -1;
                    CarregaGrid("grdCobranca", new ItemOrcCobrancaBLL().ListaTodos(obj), grdCobranca);
                }

                MostraMensagemTelaUpdatePanel(upCobranca, mensagem);
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upCobranca, "Atenção \\n\\nRegistro não atualizado.\\nMotivo:\\n" + ex.Message);
            }
          

        }

        protected void grdCobranca_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdCobranca"] != null)
            {
                grdCobranca.PageIndex = e.NewPageIndex;
                grdCobranca.DataSource = ViewState["grdCobranca"];
                grdCobranca.DataBind();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            string mensagem = ValidaTela();
            if (mensagem.Equals(""))
            {
                obj.Cod_Emprs = int.Parse(txtEmpresa.Text);
                obj.Cod_Plano = int.Parse(txtPlano.Text);
                CarregaGrid("grdCobranca", new ItemOrcCobrancaBLL().ListaTodos(obj), grdCobranca);
            }
            else
                MostraMensagemTelaUpdatePanel(upCobranca, mensagem);

        }
        #endregion

        #region Métodos
       
        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {
            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        private string ValidaTela(){

            StringBuilder str = new StringBuilder();

            if (txtEmpresa.Text.Equals("") )
            {
                str.Append("Digite o código da empresa.\\n");
            }
            if (txtPlano.Text.Equals(""))
            {
                str.Append("Digite o código do plano.\\n");
            }

            return str.ToString();
        }

        private void ListaGrid()
        {

            if (ViewState["grdCobranca"] != null)
            {
                DataTable dt = (DataTable)ViewState["grdCobranca"];
                CarregaGrid("grdCobranca", dt, grdCobranca);
            }
        }
        #endregion
    }
}