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
    public partial class intP_CadProcessoVerba : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                preencherComboTipoNegocio();
                preencherComboTipoSeg();
            }

            //1 - Folha de Assistido/Beneficio
            //2 - Saude
            //3 - Seguro e Peculio
            //4 - Capitalização Ativos
            //5 - Emprestimo

        }

        protected void btnAddProcessoVerba_Click(object sender, EventArgs e)
        {
            intP_CadProcessoVerbaBLL ProcessoVerbaBLL = new intP_CadProcessoVerbaBLL();
            VRB_NEGOCIO obj = new VRB_NEGOCIO();

            if (validarCamposInsert())
            {
                obj.NUM_VRBFSS = Util.String2Int32(txtNumVerbaFSS.Text);
                obj.COD_VERBA = Util.String2Int32(txtCodVerba.Text);
                obj.TIP_NEGOCIO = Util.String2Short(ddlTipoNegocioInsert.Text);
                obj.TIP_SEG = ddlTipoSegInsert.Text;

                Resultado res = ProcessoVerbaBLL.InseriCadProcessoVerbaProtheus(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                    grdDadosProcessoVerba.EditIndex = -1;
                    limparCampos();
                    showGridProcessoVerbaProtheus();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdDadosProcessoVerba_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    //Ini - Server para DropDownList de Tipo Negocio
                    DropDownList ddlTipoNegocio = (DropDownList)e.Row.FindControl("ddlTipoNegocio");

                    ddlTipoNegocio.Items.Insert(0, new ListItem("---Selecione---", ""));
                    ddlTipoNegocio.Items.Insert(1, new ListItem("Folha de Assistido/Beneficio", "1"));
                    ddlTipoNegocio.Items.Insert(2, new ListItem("Saúde", "2"));
                    ddlTipoNegocio.Items.Insert(3, new ListItem("Seguro e Pecúlio", "3"));
                    ddlTipoNegocio.Items.Insert(4, new ListItem("Capitalização Ativos", "4"));
                    ddlTipoNegocio.Items.Insert(5, new ListItem("Emprestimo", "5"));
                    ddlTipoNegocio.DataBind();

                    //bind dropdown-list
                    ddlTipoNegocio.SelectedValue = dr["TIP_NEGOCIO"].ToString();
                    //Fim - Server para DropDownList de Tipo Negocio

                    //Ini - Server para DropDownList de Tipo Seg
                    DropDownList ddlTipoSeg = (DropDownList)e.Row.FindControl("ddlTipoSeg");

                    //bind dropdown-list
                    ddlTipoSeg.Items.Insert(0, new ListItem("---Selecione---", ""));
                    ddlTipoSeg.Items.Insert(1, new ListItem("Pecúlio", "P"));
                    ddlTipoSeg.Items.Insert(2, new ListItem("Seguro", "S"));
                    ddlTipoSeg.DataBind();

                    ddlTipoSeg.SelectedValue = dr["TIP_SEG"].ToString();
                    //Fim - Server para DropDownList de Tipo Seg

                }
                else if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    //Ini - Server para DropDownList de Tipo Negocio
                    DropDownList ddListTipoNegocio = (DropDownList)e.Row.FindControl("ddlTipoNegocioDescInc");

                    ddListTipoNegocio.Items.Insert(0, new ListItem("---Selecione---", ""));
                    ddListTipoNegocio.Items.Insert(1, new ListItem("Folha de Assistido/Beneficio", "1"));
                    ddListTipoNegocio.Items.Insert(2, new ListItem("Saúde", "2"));
                    ddListTipoNegocio.Items.Insert(3, new ListItem("Seguro e Pecúlio", "3"));
                    ddListTipoNegocio.Items.Insert(4, new ListItem("Capitalização Ativos", "4"));
                    ddListTipoNegocio.Items.Insert(5, new ListItem("Emprestimo", "5"));
                    ddListTipoNegocio.DataBind();

                    //bind dropdown-list
                    ddListTipoNegocio.SelectedValue = dr["TIP_NEGOCIO"].ToString();
                    //Fim - Server para DropDownList de Tipo Negocio

                    //Ini - Server para DropDownList de Tipo Seg
                    DropDownList ddListTipoSeg = (DropDownList)e.Row.FindControl("ddlTipoSegDescInc");

                    //bind dropdown-list
                    ddListTipoSeg.Items.Insert(0, new ListItem("---Selecione---", ""));
                    ddListTipoSeg.Items.Insert(1, new ListItem("Pecúlio", "P"));
                    ddListTipoSeg.Items.Insert(2, new ListItem("Seguro", "S"));
                    ddListTipoSeg.DataBind();

                    ddListTipoSeg.SelectedValue = dr["TIP_SEG"].ToString();
                    //Fim - Server para DropDownList de Tipo Seg

                }
            }
        }

        protected void grdDadosProcessoVerba_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDadosProcessoVerba.EditIndex = -1;
            grdDadosProcessoVerba.PageIndex = e.NewPageIndex;
            showGridProcessoVerbaProtheus();
        }

        protected void grdDadosProcessoVerba_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdDadosProcessoVerba.EditIndex = -1;
            showGridProcessoVerbaProtheus();
        }

        protected void grdDadosProcessoVerba_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdDadosProcessoVerba.EditIndex = -1;
            showGridProcessoVerbaProtheus(txtFiltrarVerba.Text);
        }

        protected void grdDadosProcessoVerba_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDadosProcessoVerba.EditIndex = e.NewEditIndex;
            showGridProcessoVerbaProtheus(txtFiltrarVerba.Text);
        }

        protected void grdDadosProcessoVerba_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                intP_CadProcessoVerbaBLL ProcessoVerbaBLL = new intP_CadProcessoVerbaBLL();
                VRB_NEGOCIO obj = new VRB_NEGOCIO();

                obj.COD_VRB_NEGOCIO = Convert.ToInt16(((Label)grdDadosProcessoVerba.Rows[grdDadosProcessoVerba.EditIndex].FindControl("lblIDProcessoVerba")).Text);
                obj.NUM_VRBFSS = Util.String2Int32(((TextBox)grdDadosProcessoVerba.Rows[grdDadosProcessoVerba.EditIndex].FindControl("txtNumVerbaFSS")).Text);
                obj.TIP_NEGOCIO = Util.String2Short(((DropDownList)grdDadosProcessoVerba.Rows[grdDadosProcessoVerba.EditIndex].FindControl("ddlTipoNegocioDescInc")).SelectedValue);
                obj.TIP_SEG = ((DropDownList)grdDadosProcessoVerba.Rows[grdDadosProcessoVerba.EditIndex].FindControl("ddlTipoSegDescInc")).SelectedValue;
                obj.COD_VERBA = Util.String2Int32(((TextBox)grdDadosProcessoVerba.Rows[grdDadosProcessoVerba.EditIndex].FindControl("txtCodVerba")).Text);

                Resultado res = ProcessoVerbaBLL.AtualizaCadProcessoVerbaProtheus(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdDadosProcessoVerba.EditIndex = -1;
                    grdDadosProcessoVerba.ShowFooter = false;
                    showGridProcessoVerbaProtheus();

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            grdDadosProcessoVerba.EditIndex = -1;
            showGridProcessoVerbaProtheus();
            grdDadosProcessoVerba.Visible = true;
            pnlCadProcessoVerbaPesquisar.Visible = true;
            txtFiltrarVerba.Text = "";
        }

        protected void btnFiltrarVerba_Click(object sender, EventArgs e)
        {
            intP_CadProcessoVerbaBLL ProcessoVerbaBLL = new intP_CadProcessoVerbaBLL();

            if (string.IsNullOrEmpty(txtFiltrarVerba.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Favor informar um Código de Verba.");
            }
            else
            {
                DataView dv = new DataView(ProcessoVerbaBLL.buscarCadProcessoVerbaProtheus());
                dv.RowFilter = "Convert([NUM_VRBFSS], System.String) LIKE '" + txtFiltrarVerba.Text + "%'";
                grdDadosProcessoVerba.DataSource = dv;
                grdDadosProcessoVerba.DataBind();
            }
        }

        void showGridProcessoVerbaProtheus(string texto="")
        {
            intP_CadProcessoVerbaBLL ProcessoVerbaBLL = new intP_CadProcessoVerbaBLL();
            
            if (!string.IsNullOrEmpty(texto))
            {
                DataView dv = new DataView(ProcessoVerbaBLL.buscarCadProcessoVerbaProtheus());
                dv.RowFilter = "Convert([NUM_VRBFSS], System.String) LIKE '" + texto + "%'";
                grdDadosProcessoVerba.DataSource = dv;
            }
            else 
            {
                DataView dv = new DataView(ProcessoVerbaBLL.buscarCadProcessoVerbaProtheus());
                grdDadosProcessoVerba.DataSource = dv;
            }
            
            grdDadosProcessoVerba.DataBind();

        }

        void preencherComboTipoNegocio()
        {
            ddlTipoNegocioInsert.Items.Insert(0, new ListItem("---Selecione---", ""));
            ddlTipoNegocioInsert.Items.Insert(1, new ListItem("Folha de Assistido/Beneficio", "1"));
            ddlTipoNegocioInsert.Items.Insert(2, new ListItem("Saúde", "2"));
            ddlTipoNegocioInsert.Items.Insert(3, new ListItem("Seguro e Pecúlio", "3"));
            ddlTipoNegocioInsert.Items.Insert(4, new ListItem("Capitalização Ativos", "4"));
            ddlTipoNegocioInsert.Items.Insert(5, new ListItem("Emprestimo", "5"));
        }

        void preencherComboTipoSeg()
        {
            ddlTipoSegInsert.Items.Insert(0, new ListItem("---Selecione---", ""));
            ddlTipoSegInsert.Items.Insert(1, new ListItem("Pecúlio", "P"));
            ddlTipoSegInsert.Items.Insert(2, new ListItem("Seguro", "S"));
        }

        void limparCampos()
        {
            txtCodVerba.Text = "";
            txtNumVerbaFSS.Text = "";
            ddlTipoNegocioInsert.Text = "";
            ddlTipoSegInsert.Text = "";
        }

        bool validarCamposInsert()
        {
            //Valida Campo Empresa
            if (string.IsNullOrEmpty(txtNumVerbaFSS.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido o Codigo da Verba Fss !");
                return false;
            }

            return true;
        }

    }
}