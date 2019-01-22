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
    public partial class intP_CadEmpresaProtheus : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddEmpresaProtheus_Click(object sender, EventArgs e)
        {
            intP_CadEmpresaProtheusBLL EmpresaXProtheusBLL = new intP_CadEmpresaProtheusBLL();
            PATR_PRV obj = new PATR_PRV();

            if (validarCamposInsert())
            {
                obj.COD_EMPRS = Util.String2Short(txtCodEmpresa.Text);
                obj.COD_PATR = txtCodProtheus.Text;
                obj.COD_PATR_SUP = txtCodProtheusSub.Text;
                obj.DCR_PATR = txtDescricaoProtheus.Text;

                Resultado res = EmpresaXProtheusBLL.InseriCadEmpresaProtheus(obj);


                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                    grdDadosFundacaoXProtheus.EditIndex = -1;
                    limparCampos();
                    showGridFundacaoXProtheus();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            grdDadosFundacaoXProtheus.EditIndex = -1;
            showGridFundacaoXProtheus();
            grdDadosFundacaoXProtheus.Visible = true;
            pnlCadEmpresaPesquisar.Visible = true;
            txtFiltrarCodProtheus.Text = "";
        }

        protected void grdDadosFundacaoXProtheus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                intP_CadEmpresaProtheusBLL EmpresaXProtheusBLL = new intP_CadEmpresaProtheusBLL();
                PATR_PRV obj = new PATR_PRV();

                obj.COD_PATR_PRV = Convert.ToInt16(((Label)grdDadosFundacaoXProtheus.Rows[grdDadosFundacaoXProtheus.EditIndex].FindControl("lblIDFundacaoXProtheus")).Text);
                obj.COD_EMPRS = Util.String2Short(((TextBox)grdDadosFundacaoXProtheus.Rows[grdDadosFundacaoXProtheus.EditIndex].FindControl("txtCodEmpresa")).Text);
                obj.COD_PATR = ((TextBox)grdDadosFundacaoXProtheus.Rows[grdDadosFundacaoXProtheus.EditIndex].FindControl("txtCodProtheus")).Text;
                obj.COD_PATR_SUP = ((TextBox)grdDadosFundacaoXProtheus.Rows[grdDadosFundacaoXProtheus.EditIndex].FindControl("txtCodProtheusSub")).Text;
                obj.DCR_PATR = ((TextBox)grdDadosFundacaoXProtheus.Rows[grdDadosFundacaoXProtheus.EditIndex].FindControl("txtDescProtheus")).Text.ToUpper().Trim();

                Resultado res = EmpresaXProtheusBLL.AtualizaCadEmpresaProtheus(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdDadosFundacaoXProtheus.EditIndex = -1;
                    grdDadosFundacaoXProtheus.ShowFooter = false;
                    showGridFundacaoXProtheus();

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdDadosFundacaoXProtheus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDadosFundacaoXProtheus.EditIndex = e.NewEditIndex;
            showGridFundacaoXProtheus(txtFiltrarCodProtheus.Text);
        }

        protected void grdDadosFundacaoXProtheus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdDadosFundacaoXProtheus.EditIndex = -1;
            showGridFundacaoXProtheus(txtFiltrarCodProtheus.Text);
        }

        protected void grdDadosFundacaoXProtheus_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdDadosFundacaoXProtheus.EditIndex = -1;
            showGridFundacaoXProtheus();
        }

        protected void grdDadosFundacaoXProtheus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDadosFundacaoXProtheus.EditIndex = -1;
            grdDadosFundacaoXProtheus.PageIndex = e.NewPageIndex;
            showGridFundacaoXProtheus();
        }

        void limparCampos()
        {
            txtCodEmpresa.Text = "";
            txtCodProtheus.Text = "";
            txtCodProtheusSub.Text = "";
            txtDescricaoProtheus.Text = "";
        }

        bool validarCamposInsert()
        {
            //Valida Campo Empresa
            if (string.IsNullOrEmpty(txtCodEmpresa.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchida a Empresa !");
                return false;
            }

            return true;
        }

        void showGridFundacaoXProtheus(string texto = "")
        {
            intP_CadEmpresaProtheusBLL EmpresaXProtheusBLL = new intP_CadEmpresaProtheusBLL();

            if (!string.IsNullOrEmpty(texto))
            {
                DataView dv = new DataView(EmpresaXProtheusBLL.buscarCadEmpresaProtheus());
                dv.RowFilter = "Convert([COD_PATR], System.String) LIKE '" + texto + "%'";
                grdDadosFundacaoXProtheus.DataSource = dv;
            }
            else
            {
                DataView dv = new DataView(EmpresaXProtheusBLL.buscarCadEmpresaProtheus());
                grdDadosFundacaoXProtheus.DataSource = dv;
            }

            grdDadosFundacaoXProtheus.DataBind();

        }

        protected void btnFiltrarCodProtheus_Click(object sender, EventArgs e)
        {
            intP_CadEmpresaProtheusBLL EmpresaXProtheusBLL = new intP_CadEmpresaProtheusBLL();

            if (string.IsNullOrEmpty(txtFiltrarCodProtheus.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Favor informar um Código Protheus.");
            }
            else
            {
                DataView dv = new DataView(EmpresaXProtheusBLL.buscarCadEmpresaProtheus());
                dv.RowFilter = "Convert([COD_PATR], System.String) LIKE '" + txtFiltrarCodProtheus.Text + "%'";
                grdDadosFundacaoXProtheus.DataSource = dv;
                grdDadosFundacaoXProtheus.DataBind();
            }
        }
    }
}