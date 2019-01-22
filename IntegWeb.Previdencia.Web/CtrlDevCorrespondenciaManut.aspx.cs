using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using System.Data;

namespace IntegWeb.Previdencia.Web
{
    public partial class CtrlDevCorrespondenciaManut : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                preencherComboTipoDocumento();
                preencherComboTipoAcao();
                preencherComboTipoPrazo();
            }
        }

        #region "TipoAcao"
        protected void grdControleTipoPlanoAcao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
                CAD_TBL_CTRLDEV_TIPOACAO obj = new CAD_TBL_CTRLDEV_TIPOACAO();
                obj.ID_REG = Convert.ToInt16(((Label)grdControleTipoPlanoAcao.Rows[grdControleTipoPlanoAcao.EditIndex].FindControl("lblIdReg")).Text);
                obj.DESCRICAO = Convert.ToString(((TextBox)grdControleTipoPlanoAcao.Rows[grdControleTipoPlanoAcao.EditIndex].FindControl("txtDescricao")).Text).Trim().ToUpper();

                Resultado res = CtrlCorrespBLL.AtualizaTipoAcao(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdControleTipoPlanoAcao.EditIndex = -1;
                    grdControleTipoPlanoAcao.ShowFooter = false;
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdControleTipoPlanoAcao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdControleTipoPlanoAcao.EditIndex = e.NewEditIndex;
            showGridTipoPlanoAcao();
        }

        protected void grdControleTipoPlanoAcao_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdControleTipoPlanoAcao.EditIndex = -1;
            showGridTipoPlanoAcao();
        }

        protected void grdControleTipoPlanoAcao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            grdControleTipoPlanoAcao.EditIndex = -1;
            grdControleTipoPlanoAcao.PageIndex = e.NewPageIndex;
            DataView dv = new DataView(CtrlCorrespBLL.buscarTipoAcao());
            grdControleTipoPlanoAcao.DataSource = dv;
            grdControleTipoPlanoAcao.DataBind();
        }

        protected void grdControleTipoPlanoAcao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdControleTipoPlanoAcao.EditIndex = -1;
            showGridTipoPlanoAcao();
        }

        protected void btnConsultarTipoAcao_Click(object sender, EventArgs e)
        {
            showGridTipoPlanoAcao();
        }

        protected void btnInserirPlanoAcao_Click(object sender, EventArgs e)
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            CAD_TBL_CTRLDEV_TIPOACAO obj = new CAD_TBL_CTRLDEV_TIPOACAO();

            if (validarCampoTipoAcao())
            {
                obj.DESCRICAO = txtDescricaoPlanoAcao.Text.Trim().ToUpper();
                Resultado res = CtrlCorrespBLL.InseriTipoAcao(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                    grdControleTipoPlanoAcao.EditIndex = -1;
                    txtDescricaoPlanoAcao.Text = "";
                    showGridTipoPlanoAcao();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        void showGridTipoPlanoAcao()
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            DataView dv = new DataView(CtrlCorrespBLL.buscarTipoAcao());
            grdControleTipoPlanoAcao.DataSource = dv;
            grdControleTipoPlanoAcao.DataBind();

        }
        
        bool validarCampoTipoAcao()
        {
            //Valida tela de Tipo Ação
            if (string.IsNullOrEmpty(txtDescricaoPlanoAcao.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido o Tipo de Ação !");
                return false;
            }

            return true;
        }

        #endregion

        #region "TipoDocumento"
        protected void btnConsultarTipoDocumento_Click(object sender, EventArgs e)
        {
            showGridTipoDocumento();
        }

        protected void btnInserirTipoDocumento_Click(object sender, EventArgs e)
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            CAD_TBL_CTRLDEV_TIPODOCUMENTO obj = new CAD_TBL_CTRLDEV_TIPODOCUMENTO();

            if (validarcampoTipoDocumento())
            {
                obj.DESCRICAO = txtDescTipoDocumento.Text.Trim().ToUpper();

                Resultado res = CtrlCorrespBLL.InseriTipoDocumento(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                    grdControleTipoDocumento.EditIndex = -1;
                    txtDescTipoDocumento.Text = "";
                    showGridTipoDocumento();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdControleTipoDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
                CAD_TBL_CTRLDEV_TIPODOCUMENTO obj = new CAD_TBL_CTRLDEV_TIPODOCUMENTO();
                obj.ID_REG = Convert.ToInt16(((Label)grdControleTipoDocumento.Rows[grdControleTipoDocumento.EditIndex].FindControl("lblIdReg")).Text);
                obj.DESCRICAO = Convert.ToString(((TextBox)grdControleTipoDocumento.Rows[grdControleTipoDocumento.EditIndex].FindControl("txtDescricao")).Text).Trim().ToUpper();

                Resultado res = CtrlCorrespBLL.AtualizaTipoDocumento(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdControleTipoDocumento.EditIndex = -1;
                    grdControleTipoDocumento.ShowFooter = false;
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdControleTipoDocumento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdControleTipoDocumento.EditIndex = e.NewEditIndex;
            showGridTipoDocumento();
        }

        protected void grdControleTipoDocumento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdControleTipoDocumento.EditIndex = -1;
            showGridTipoDocumento();
        }

        protected void grdControleTipoDocumento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdControleTipoDocumento.EditIndex = -1;
            showGridTipoDocumento();
        }

        protected void grdControleTipoDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            grdControleTipoDocumento.EditIndex = -1;
            grdControleTipoDocumento.PageIndex = e.NewPageIndex;
            DataView dv = new DataView(CtrlCorrespBLL.buscarTipoDocumento());
            grdControleTipoDocumento.DataSource = dv;
            grdControleTipoDocumento.DataBind();
        }

        void showGridTipoDocumento()
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            DataView dv = new DataView(CtrlCorrespBLL.buscarTipoDocumento());
            grdControleTipoDocumento.DataSource = dv;
            grdControleTipoDocumento.DataBind();

        }

        bool validarcampoTipoDocumento()
        {
            //Valida tela de Tipo Documento
            if (string.IsNullOrEmpty(txtDescTipoDocumento.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido o Tipo do documento !");
                return false;
            }
            return true;
        }

        #endregion

        #region "TipoMotivoDevolucao"

        protected void btnConsultarMotivoDevolucao_Click(object sender, EventArgs e)
        {
            showGridTipoMotivoDevolucao();
        }

        protected void btnInserirMotivoDevolucao_Click(object sender, EventArgs e)
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            CAD_TBL_CTRLDEV_TIPOMOTDEV obj = new CAD_TBL_CTRLDEV_TIPOMOTDEV();

            if (validarCampoTipoMotivoDevolucao())
            {
                obj.DESCRICAO = txtDescMotivoDevolucao.Text.Trim().ToUpper();

                Resultado res = CtrlCorrespBLL.InseriTipoMotivoDevolucao(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                    grdControleMotivoDevolucao.EditIndex = -1;
                    txtDescMotivoDevolucao.Text = "";
                    showGridTipoMotivoDevolucao();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdControleMotivoDevolucao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
                CAD_TBL_CTRLDEV_TIPOMOTDEV obj = new CAD_TBL_CTRLDEV_TIPOMOTDEV();
                obj.ID_REG = Convert.ToInt16(((Label)grdControleMotivoDevolucao.Rows[grdControleMotivoDevolucao.EditIndex].FindControl("lblIdReg")).Text);
                obj.DESCRICAO = Convert.ToString(((TextBox)grdControleMotivoDevolucao.Rows[grdControleMotivoDevolucao.EditIndex].FindControl("txtDescricao")).Text).Trim().ToUpper();

                Resultado res = CtrlCorrespBLL.AtualizaTipoMotivoDevolucao(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdControleMotivoDevolucao.EditIndex = -1;
                    grdControleMotivoDevolucao.ShowFooter = false;
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdControleMotivoDevolucao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdControleMotivoDevolucao.EditIndex = e.NewEditIndex;
            showGridTipoMotivoDevolucao();
        }

        protected void grdControleMotivoDevolucao_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdControleMotivoDevolucao.EditIndex = -1;
            showGridTipoMotivoDevolucao();
        }

        protected void grdControleMotivoDevolucao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdControleMotivoDevolucao.EditIndex = -1;
            showGridTipoMotivoDevolucao();
        }

        protected void grdControleMotivoDevolucao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            grdControleMotivoDevolucao.EditIndex = -1;
            grdControleMotivoDevolucao.PageIndex = e.NewPageIndex;
            DataView dv = new DataView(CtrlCorrespBLL.buscarTipoMotivoDevolucao());
            grdControleMotivoDevolucao.DataSource = dv;
            grdControleMotivoDevolucao.DataBind();
        }

        void showGridTipoMotivoDevolucao()
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            DataView dv = new DataView(CtrlCorrespBLL.buscarTipoMotivoDevolucao());
            grdControleMotivoDevolucao.DataSource = dv;
            grdControleMotivoDevolucao.DataBind();

        }

        bool validarCampoTipoMotivoDevolucao()
        {
            //Valida tela de Tipo Motivo
            if (string.IsNullOrEmpty(txtDescMotivoDevolucao.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido o Tipo do Motivo !");
                return false;
            }

            return true;
        }

        #endregion

        #region "FluxoAcao"

        protected void btnInserirFluxoAcao_Click(object sender, EventArgs e)
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            CAD_TBL_CTRLDEV_FLUXOACAO obj = new CAD_TBL_CTRLDEV_FLUXOACAO();

            if (validarCamposInsertFluxoAcao())
            {
                obj.TEMPO_PRAZO = Convert.ToString(ddlTempoPrazoInsert.Text);
                obj.ID_TIPOPLANOACAO = Convert.ToInt16(ddlTipoAcaoInsert.Text);
                obj.ID_TIPODOCUMENTO = Convert.ToInt16(ddlTipoDocumentoInsert.Text);

                Resultado res = CtrlCorrespBLL.InseriFluxoAcao(obj);


                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                    grdControleFluxoAcao.EditIndex = -1;
                    ddlTipoDocumentoInsert.Text = "";
                    ddlTipoAcaoInsert.Text = "";
                    ddlTempoPrazoInsert.Text = "";
                    showGridFluxoAcao();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdControleFluxoAcao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
                CAD_TBL_CTRLDEV_FLUXOACAO obj = new CAD_TBL_CTRLDEV_FLUXOACAO();

                obj.ID_REG = Convert.ToInt16(((Label)grdControleFluxoAcao.Rows[grdControleFluxoAcao.EditIndex].FindControl("lblIdReg")).Text);
                obj.ID_TIPODOCUMENTO = Convert.ToInt16(((DropDownList)grdControleFluxoAcao.Rows[grdControleFluxoAcao.EditIndex].FindControl("ddlTipoDocumentoDescInc")).SelectedValue);
                obj.ID_TIPOPLANOACAO = Convert.ToInt16(((DropDownList)grdControleFluxoAcao.Rows[grdControleFluxoAcao.EditIndex].FindControl("ddlTipoAcaoDescInc")).SelectedValue);
                obj.TEMPO_PRAZO = Convert.ToString(((DropDownList)grdControleFluxoAcao.Rows[grdControleFluxoAcao.EditIndex].FindControl("ddlTempoPrazoInc")).SelectedValue);

                Resultado res = CtrlCorrespBLL.AtualizaFluxoAcao(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdControleFluxoAcao.EditIndex = -1;
                    grdControleFluxoAcao.ShowFooter = false;
                    showGridFluxoAcao();

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdControleFluxoAcao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdControleFluxoAcao.EditIndex = e.NewEditIndex;
            showGridFluxoAcao();
        }

        protected void grdControleFluxoAcao_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdControleFluxoAcao.EditIndex = -1;
            //showGridFluxoAcao();
        }

        protected void grdControleFluxoAcao_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    //Ini - Server para DropDownList de Tipo Ação
                    DropDownList ddListAcao = (DropDownList)e.Row.FindControl("ddlTipoAcaoDescInc");
                    Label lblTipoAcaoDesc = (Label)e.Row.FindControl("lblTipoAcaoDesc");

                    //bind dropdown-list
                    CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
                    ddListAcao.DataSource = CtrlCorrespBLL.GetTipoAcao().ToList();
                    ddListAcao.DataTextField = "descricao";
                    ddListAcao.DataValueField = "ID_REG";
                    ddListAcao.DataBind();

                    ddListAcao.SelectedValue = dr["IdDescricaoAcao"].ToString();
                    //Fim - Server para DropDownList de Tipo Ação

                    //Ini - Server para DropDownList de Tipo Documento
                    DropDownList ddListDoc = (DropDownList)e.Row.FindControl("ddlTipoDocumentoDescInc");
                    Label lblTipoDocumentoDesc = (Label)e.Row.FindControl("lblTipoDocumentoDesc");

                    //bind dropdown-list
                    ddListDoc.DataSource = CtrlCorrespBLL.GetTipoDocumento().ToList();
                    ddListDoc.DataTextField = "descricao";
                    ddListDoc.DataValueField = "ID_REG";
                    ddListDoc.DataBind();

                    ddListDoc.SelectedValue = dr["IdDescricaoDoc"].ToString();
                    //Fim - Server para DropDownList de Tipo Documento

                    //Ini - Utilizado para DropDownList de Tempo Prazo
                    DropDownList ddlTempoPrazoInc = (DropDownList)e.Row.FindControl("ddlTempoPrazoInc");
                    Label lblTempoPrazo = (Label)e.Row.FindControl("lblTempoPrazo");

                    ddlTempoPrazoInc.Items.Insert(0, new ListItem("", ""));
                    ddlTempoPrazoInc.Items.Insert(1, new ListItem("SIM", "SIM"));
                    ddlTempoPrazoInc.Items.Insert(2, new ListItem("NÃO", "NAO"));
                    ddlTempoPrazoInc.DataBind();

                    ddlTempoPrazoInc.SelectedValue = dr["TempoPrazo"].ToString();
                    //Fim - Utilizado para DropDownList de Tempo Prazo

                }
            }
        }

        protected void grdControleFluxoAcao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdControleFluxoAcao.EditIndex = -1;
            showGridFluxoAcao();
        }

        protected void grdControleFluxoAcao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            grdControleFluxoAcao.EditIndex = -1;
            grdControleFluxoAcao.PageIndex = e.NewPageIndex;
            DataView dv = new DataView(CtrlCorrespBLL.buscarFluxoAcao());
            grdControleFluxoAcao.DataSource = dv;
            grdControleFluxoAcao.DataBind();
        }

        protected void btnConsultarFluxoAcao_Click(object sender, EventArgs e)
        {
            showGridFluxoAcao();
        }

        void preencherComboTipoAcao()
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            ddlTipoAcaoInsert.DataSource = CtrlCorrespBLL.GetTipoAcao().ToList();
            ddlTipoAcaoInsert.DataValueField = "ID_REG";
            ddlTipoAcaoInsert.DataTextField = "DESCRICAO";
            ddlTipoAcaoInsert.DataBind();
            ddlTipoAcaoInsert.Items.Insert(0, new ListItem("---Selecione---", ""));
        }

        void preencherComboTipoDocumento()
        {

            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            ddlTipoDocumentoInsert.DataSource = CtrlCorrespBLL.GetTipoDocumento().ToList();
            ddlTipoDocumentoInsert.DataValueField = "ID_REG";
            ddlTipoDocumentoInsert.DataTextField = "DESCRICAO";
            ddlTipoDocumentoInsert.DataBind();
            ddlTipoDocumentoInsert.Items.Insert(0, new ListItem("---Selecione---", ""));
        }

        void preencherComboTipoPrazo()
        {
            ddlTempoPrazoInsert.Items.Insert(0, new ListItem("---Selecione---", ""));
        }

        void showGridFluxoAcao()
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            DataView dv = new DataView(CtrlCorrespBLL.buscarFluxoAcao());
            grdControleFluxoAcao.DataSource = dv;
            grdControleFluxoAcao.DataBind();

        }

        bool validarCamposInsertFluxoAcao()
        {


            //Valida tela de Fluxo Acao TipoDocumento
            if (string.IsNullOrEmpty(ddlTipoDocumentoInsert.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido o Tipo do documento !");
                return false;
            }
            //Valida tela de Fluxo Acao TipoAcao
            if (string.IsNullOrEmpty(ddlTipoAcaoInsert.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Deve ser Preenchido o Tipo da Ação!");
                return false;
            }

            return true;
        }

        #endregion


       

       






    }
}