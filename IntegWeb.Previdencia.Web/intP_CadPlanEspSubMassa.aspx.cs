using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Int_Protheus;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class intP_CadPlanEspSubMassa : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                preecherComboPlano();
                preecherComboEspecie();
                preecherComboSubMassa();
            }
        }

        protected void btnAddPlanEspSubMassa_Click(object sender, EventArgs e)
        {
            intP_CadPlanEspSubMassaBLL PlanEspSubMassaBLL = new intP_CadPlanEspSubMassaBLL();
            PLN_SUBMASSA obj = new PLN_SUBMASSA();

            if (validarCamposInsert())
            {
                obj.COD_EMPRS = Util.String2Short(txtEmpresa.Text);
                obj.COD_ESPBNF = Util.String2Short(ddlEspecieInsert.Text);
                obj.NUM_PLBNF = Util.String2Short(ddlPlanoInsert.Text);
                obj.COD_SUBMASSA = ddlSubMassaInsert.Text;

                Resultado res = PlanEspSubMassaBLL.InseriCadPlanEspSubMassaProtheus(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                    grdDadosPlanEspSubMassa.EditIndex = -1;
                    limparCampos();
                    showGridPlanEspSubMassaProtheus();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            grdDadosPlanEspSubMassa.EditIndex = -1;
            showGridPlanEspSubMassaProtheus();
            grdDadosPlanEspSubMassa.Visible = true;
            pnlCadPlanEspSubMassaPesquisar.Visible = true;
            ddlSubMassaFiltro.Text = "";
        }

        protected void btnFiltrarSubMassa_Click(object sender, EventArgs e)
        {
            intP_CadPlanEspSubMassaBLL PlanEspSubMassaBLL = new intP_CadPlanEspSubMassaBLL();

            if (string.IsNullOrEmpty(ddlSubMassaFiltro.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Favor informar um Código de Verba.");
            }
            else
            {
                DataView dv = new DataView(PlanEspSubMassaBLL.buscarCadPlanEspSubMassaProtheus());
                dv.RowFilter = "Convert([COD_SUBMASSA], System.String) LIKE '" + ddlSubMassaFiltro.Text.Trim() + "%'";
                grdDadosPlanEspSubMassa.DataSource = dv;
                grdDadosPlanEspSubMassa.DataBind();
            }
        }

        protected void grdDadosPlanEspSubMassa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                intP_CadPlanEspSubMassaBLL PlanEspSubMassaBLL = new intP_CadPlanEspSubMassaBLL();
                PLN_SUBMASSA obj = new PLN_SUBMASSA();

                obj.COD_PLN_SUBMASSA = Convert.ToInt16(((Label)grdDadosPlanEspSubMassa.Rows[grdDadosPlanEspSubMassa.EditIndex].FindControl("lblIDPlanEspSubMassa")).Text);
                obj.COD_ESPBNF = Util.String2Short(((DropDownList)grdDadosPlanEspSubMassa.Rows[grdDadosPlanEspSubMassa.EditIndex].FindControl("ddlEspecieInc")).SelectedValue);
                obj.COD_SUBMASSA = ((DropDownList)grdDadosPlanEspSubMassa.Rows[grdDadosPlanEspSubMassa.EditIndex].FindControl("ddlSubMassaInc")).SelectedValue;
                obj.NUM_PLBNF = Util.String2Short(((DropDownList)grdDadosPlanEspSubMassa.Rows[grdDadosPlanEspSubMassa.EditIndex].FindControl("ddlPlanoInc")).SelectedValue);
                obj.COD_EMPRS = Util.String2Short(((TextBox)grdDadosPlanEspSubMassa.Rows[grdDadosPlanEspSubMassa.EditIndex].FindControl("txtEmpresa")).Text);

                Resultado res = PlanEspSubMassaBLL.AtualizaCadPlanEspSubMassaProtheus(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdDadosPlanEspSubMassa.EditIndex = -1;
                    grdDadosPlanEspSubMassa.ShowFooter = false;
                    showGridPlanEspSubMassaProtheus();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }

        }

        protected void grdDadosPlanEspSubMassa_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDadosPlanEspSubMassa.EditIndex = e.NewEditIndex;
            showGridPlanEspSubMassaProtheus(ddlSubMassaFiltro.Text);
        }

        protected void grdDadosPlanEspSubMassa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdDadosPlanEspSubMassa.EditIndex = -1;
            showGridPlanEspSubMassaProtheus(ddlSubMassaFiltro.Text);
        }

        protected void grdDadosPlanEspSubMassa_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdDadosPlanEspSubMassa.EditIndex = -1;
            showGridPlanEspSubMassaProtheus();
        }

        protected void grdDadosPlanEspSubMassa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDadosPlanEspSubMassa.EditIndex = -1;
            grdDadosPlanEspSubMassa.PageIndex = e.NewPageIndex;
            showGridPlanEspSubMassaProtheus();
        }

        protected void grdDadosPlanEspSubMassa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    //Ini - Server para DropDownList de Plano
                    DropDownList ddlPlanoDesc = (DropDownList)e.Row.FindControl("ddlPlanoDesc");
                    CloneDropDownList(ddlPlanoInsert, ddlPlanoDesc);
                    ddlPlanoDesc.DataBind();
                    //bind dropdown-list
                    ddlPlanoDesc.SelectedValue = dr["NUM_PLBNF"].ToString();
                    //Fim - Server para DropDownList de Plano

                    //Ini - Server para DropDownList de Especie
                    DropDownList ddlEspecieDesc = (DropDownList)e.Row.FindControl("ddlEspecieDesc");
                    CloneDropDownList(ddlEspecieInsert, ddlEspecieDesc);
                    ddlEspecieDesc.DataBind();
                    //bind dropdown-list
                    ddlEspecieDesc.SelectedValue = dr["COD_ESPBNF"].ToString();
                    //Fim - Server para DropDownList de Especie

                    //Ini - Server para DropDownList de SubMassa
                    DropDownList ddlSubMassaDesc = (DropDownList)e.Row.FindControl("ddlSubMassaDesc");
                    CloneDropDownList(ddlSubMassaInsert, ddlSubMassaDesc);
                    ddlSubMassaDesc.DataBind();
                    //bind dropdown-list
                    ddlSubMassaDesc.SelectedValue = dr["COD_SUBMASSA"].ToString();
                    //Fim - Server para DropDownList de SubMassa

                }
                else if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    //Ini - Server para DropDownList de Plano
                    DropDownList ddlPlanoInc = (DropDownList)e.Row.FindControl("ddlPlanoInc");
                    CloneDropDownList(ddlPlanoInsert, ddlPlanoInc);
                    ddlPlanoInc.DataBind();
                    //bind dropdown-list
                    ddlPlanoInc.SelectedValue = dr["NUM_PLBNF"].ToString();
                    //Fim - Server para DropDownList de Plano

                    //Ini - Server para DropDownList de Especie
                    DropDownList ddlEspecieInc = (DropDownList)e.Row.FindControl("ddlEspecieInc");
                    CloneDropDownList(ddlEspecieInsert, ddlEspecieInc);
                    ddlEspecieInc.DataBind();
                    //bind dropdown-list
                    ddlEspecieInc.SelectedValue = dr["COD_ESPBNF"].ToString();
                    //Fim - Server para DropDownList de Especie

                    //Ini - Server para DropDownList de SubMassa
                    DropDownList ddlSubMassaInc = (DropDownList)e.Row.FindControl("ddlSubMassaInc");
                    CloneDropDownList(ddlSubMassaInsert, ddlSubMassaInc);
                    ddlSubMassaInc.DataBind();
                    //bind dropdown-list
                    ddlSubMassaInc.SelectedValue = dr["COD_SUBMASSA"].ToString();
                    //Fim - Server para DropDownList de SubMassa

                }
            }
        }

        void preecherComboPlano()
        {
            intP_CadPlanEspSubMassaBLL PlanEspSubMassaBLL = new intP_CadPlanEspSubMassaBLL();
            ddlPlanoInsert.DataSource = PlanEspSubMassaBLL.GetPlano().ToList();
            ddlPlanoInsert.DataValueField = "NUM_PLBNF";
            ddlPlanoInsert.DataTextField = "DCR_PLBNF";
            ddlPlanoInsert.DataBind();
            ddlPlanoInsert.Items.Insert(0, new ListItem("---Selecione---", ""));

        }

        void preecherComboEspecie()
        {
            intP_CadPlanEspSubMassaBLL PlanEspSubMassaBLL = new intP_CadPlanEspSubMassaBLL();
            ddlEspecieInsert.DataSource = PlanEspSubMassaBLL.GetEspecie();
            ddlEspecieInsert.DataValueField = "COD_ESPBNF";
            ddlEspecieInsert.DataTextField = "DCR_ESPBNF";
            ddlEspecieInsert.DataBind();
            ddlEspecieInsert.Items.Insert(0, new ListItem("---Selecione---", ""));
        }

        void preecherComboSubMassa()
        {
            intP_CadPlanEspSubMassaBLL PlanEspSubMassaBLL = new intP_CadPlanEspSubMassaBLL();
            ddlSubMassaInsert.DataSource = PlanEspSubMassaBLL.GetSubMassa().ToList();
            ddlSubMassaInsert.DataValueField = "COD_SUBMASSA";
            ddlSubMassaInsert.DataTextField = "DCR_SUBMASSA";
            ddlSubMassaInsert.DataBind();
            ddlSubMassaInsert.Items.Insert(0, new ListItem("---Selecione---", ""));

            CloneDropDownList(ddlSubMassaInsert, ddlSubMassaFiltro);

        }

        void showGridPlanEspSubMassaProtheus(string texto = "")
        {
            intP_CadPlanEspSubMassaBLL PlanEspSubMassaBLL = new intP_CadPlanEspSubMassaBLL();
            if (!string.IsNullOrEmpty(texto))
            {
                DataView dv = new DataView(PlanEspSubMassaBLL.buscarCadPlanEspSubMassaProtheus());
                dv.RowFilter = "Convert([COD_SUBMASSA], System.String) LIKE '" + texto.Trim() + "%'";
                grdDadosPlanEspSubMassa.DataSource = dv;
            }
            else
            {
                DataView dv = new DataView(PlanEspSubMassaBLL.buscarCadPlanEspSubMassaProtheus());
                grdDadosPlanEspSubMassa.DataSource = dv;
            }

            grdDadosPlanEspSubMassa.DataBind();
        }

        void limparCampos()
        {
            txtEmpresa.Text = "";
            ddlEspecieInsert.Text = "";
            ddlPlanoInsert.Text = "";
            ddlSubMassaInsert.Text = "";
        }

        bool validarCamposInsert()
        {
            //Valida Campo Empresa
            if (string.IsNullOrEmpty(txtEmpresa.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido o Código Empresa !");
                return false;
            }

            //Valida Campo Empresa
            if (string.IsNullOrEmpty(ddlSubMassaInsert.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido a SubMassa !");
                return false;
            }

            //Valida Campo Empresa
            if (string.IsNullOrEmpty(ddlPlanoInsert.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido o Plano !");
                return false;
            }

            //Valida Campo Empresa
            if (string.IsNullOrEmpty(ddlEspecieInsert.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchida a Especie !");
                return false;
            }

            return true;
        }
    }
}