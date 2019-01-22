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
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Web
{
    public partial class CtrlDevCorrespondencia : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                preencherComboTipoDocumento();
                preencherComboTipoMotivoDevolucao();
            }
        }

        #region "Inserção"


        protected void btnAddCtrlDevCorresp_Click(object sender, EventArgs e)
        {
            CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
            CAD_TBL_CTRLDEV_CORRESP obj = new CAD_TBL_CTRLDEV_CORRESP();

            Resultado res = new Resultado();


            obj.MATRICULA = Util.String2Int32(txtMatriculaInsert.Text);
            obj.COD_EMPRS = Util.String2Int32(txtCodEmpresaInsert.Text);
            obj.NUM_REP = Util.String2Int32(ddlCodRepresInsert.Text);

            DataTable dtTitular = new DataTable();
            if (string.IsNullOrEmpty(obj.NUM_REP.ToString()))
            {
                if (!string.IsNullOrEmpty(obj.COD_EMPRS.ToString()) && !string.IsNullOrEmpty(obj.MATRICULA.ToString()))
                {
                    dtTitular = CorrespondenciaBLL.GetTitular(Convert.ToInt32(obj.COD_EMPRS), Convert.ToInt32(obj.MATRICULA));

                    if (dtTitular.Rows.Count > 0)
                    {
                        obj.NOME = lblTitular.Text.Trim();
                        obj.ENDERECO = dtTitular.Rows[0]["DCR_ENDER_EMPRG"].ToString();
                        obj.MUNICIPIO = dtTitular.Rows[0]["NOM_CIDRS_EMPRG"].ToString();
                        obj.COMPLEMENTO = dtTitular.Rows[0]["DCR_COMPL_EMPRG"].ToString();
                        obj.NUMERO = dtTitular.Rows[0]["NUM_ENDER_EMPRG"].ToString();
                        obj.UF = dtTitular.Rows[0]["COD_UFCI_EMPRG"].ToString();
                        obj.BAIRRO = dtTitular.Rows[0]["NOM_BAIRRO_EMPRG"].ToString();
                        obj.CEP = dtTitular.Rows[0]["COD_CEP_EMPRG"].ToString();

                        res = CorrespondenciaBLL.InseriCorrespondencia(obj);
                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "A Empresa e a Matricula tem que está preenchida !");
                }
            }

            DataTable dtRepres = new DataTable();
            if (!string.IsNullOrEmpty(obj.NUM_REP.ToString()))
            {
                dtRepres = CorrespondenciaBLL.GetRepressEnd(Convert.ToInt32(obj.NUM_REP));
                if (dtRepres.Rows.Count > 0)
                {
                    var ini = ddlCodRepresInsert.SelectedItem.Text.IndexOf("-");
                    var fim = ddlCodRepresInsert.SelectedItem.Text.Length - ini;

                    obj.NOME = ddlCodRepresInsert.SelectedItem.Text.Substring(ini + 1, fim - 1);
                    obj.ENDERECO = dtRepres.Rows[0]["DCR_ENDER_REPRES"].ToString();
                    obj.MUNICIPIO = dtRepres.Rows[0]["COD_ESTADO_REPRES"].ToString();
                    obj.COMPLEMENTO = dtRepres.Rows[0]["DCR_COMPL_REPRES"].ToString();
                    obj.NUMERO = dtRepres.Rows[0]["NUM_ENDER_REPRES"].ToString();
                    obj.UF = dtRepres.Rows[0]["COD_ESTADO_REPRES"].ToString();
                    obj.BAIRRO = dtRepres.Rows[0]["NOM_BAIRRO_REPRES"].ToString();
                    obj.CEP = dtRepres.Rows[0]["COD_CEP_REPRES"].ToString();

                    res = CorrespondenciaBLL.InseriCorrespondencia(obj);
                }
            }

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                showGridCtrlDevCorrespondecia(CorrespondenciaBLL.GetMaxPk().ToString());
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
            }

        }

        protected void btnAddCredCorresp_Click(object sender, EventArgs e)
        {
            CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
            CAD_TBL_CTRLDEV_CORRESP obj = new CAD_TBL_CTRLDEV_CORRESP();

            Resultado res = new Resultado();

            obj.NUM_CONTRATO = Util.String2Int32(txtCodContratoInsert.Text);

            DataTable dtCredenc = new DataTable();
            if (!string.IsNullOrEmpty(obj.NUM_CONTRATO.ToString()))
            {
                dtCredenc = CorrespondenciaBLL.GetCredenEnd(Convert.ToInt32(obj.NUM_CONTRATO));

                if (dtCredenc.Rows.Count > 0)
                {
                    obj.NOME = lblCredenciado.Text.Trim();
                    obj.ENDERECO = dtCredenc.Rows[0]["nom_logradouro"].ToString();
                    obj.MUNICIPIO = dtCredenc.Rows[0]["dcr_munici"].ToString();
                    obj.COMPLEMENTO = dtCredenc.Rows[0]["des_complemento"].ToString();
                    obj.NUMERO = dtCredenc.Rows[0]["num_logradouro"].ToString();
                    obj.UF = dtCredenc.Rows[0]["cod_estado"].ToString();
                    obj.BAIRRO = dtCredenc.Rows[0]["nom_bairro"].ToString().Trim();
                    obj.CEP = dtCredenc.Rows[0]["num_cep"].ToString();
                    res = CorrespondenciaBLL.InseriCorrespondencia(obj);
                }

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                    showGridCtrlDevCorrespondecia(CorrespondenciaBLL.GetMaxPk().ToString());
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "O Contrato não pode ser em branco !");
            }
        }

        protected void btnInserirCredenciado_Click(object sender, EventArgs e)
        {
            pnlCtrlDevCorrespInsert.Visible = false;
            pnlInsertCredenciado.Visible = true;
            pnlBotoesInsert.Visible = true;
            btnAddCtrlDevCorresp.Visible = false;
            btnAddCredCorresp.Visible = true;
            limparCamposParticipantes();
            lblCodContratoInsert.Style.Add("display", "inline");
            txtCodContratoInsert.Style.Add("display", "inline");

            lblCodEmpresaInsert.Style.Add("display", "none");
            txtCodEmpresaInsert.Style.Add("display", "none");

            lblMatriculaInsert.Style.Add("display", "none");
            txtMatriculaInsert.Style.Add("display", "none");

            lblCodRepresInsert.Style.Add("display", "none");
            ddlCodRepresInsert.Style.Add("display", "none");
        }

        protected void btnInserirParticipante_Click(object sender, EventArgs e)
        {
            pnlCtrlDevCorrespInsert.Visible = true;
            pnlInsertCredenciado.Visible = false;
            pnlBotoesInsert.Visible = true;
            btnAddCtrlDevCorresp.Visible = true;
            btnAddCredCorresp.Visible = false;
            limparCamposCredenciado();
            lblCodEmpresaInsert.Style.Add("display", "inline");
            txtCodEmpresaInsert.Style.Add("display", "inline");

            lblMatriculaInsert.Style.Add("display", "inline");
            txtMatriculaInsert.Style.Add("display", "inline");

            lblCodRepresInsert.Style.Add("display", "inline");
            ddlCodRepresInsert.Style.Add("display", "inline");

            lblCodContratoInsert.Style.Add("display", "none");
            txtCodContratoInsert.Style.Add("display", "none");

        }

        private void CarregarDadosPartic(int cod_emprs, int num_rgtro_emprg)
        {
            CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
            DataTable dtTitular = CorrespondenciaBLL.GetTitular(cod_emprs, num_rgtro_emprg);
            DataTable dtRepres = CorrespondenciaBLL.GetRepress(cod_emprs, num_rgtro_emprg);

            limparCamposCredenciado();

            if (dtTitular.Rows.Count > 0)
            {
                lblTitular.Text = dtTitular.Rows[0]["NOM_EMPRG"].ToString();

            }
            else
            {
                lblTitular.Text = "Digite uma Empresa e Matricula Valido";
            }

            if (dtRepres.Rows.Count > 0)
            {
                ddlCodRepresInsert.DataSource = dtRepres;
                ddlCodRepresInsert.DataValueField = "num_idntf_rptant";
                ddlCodRepresInsert.DataTextField = "CodigoENomeRepress";
                ddlCodRepresInsert.DataBind();
                ddlCodRepresInsert.Items.Insert(0, new ListItem("---Selecione---", ""));
            }
        }

        private void CarregarDadosCreden(int cod_Contrato)
        {
            CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
            DataTable dt = CorrespondenciaBLL.GetCredenciado(cod_Contrato);

            limparCamposParticipantes();

            if (dt.Rows.Count > 0)
            {
                lblCredenciado.Text = dt.Rows[0]["NOM_CONVENENTE"].ToString();
            }
            else
            {
                lblCredenciado.Text = "Digite um Contrato Valido";
            }
        }

        protected void btnDadosPartic_Click(object sender, EventArgs e)
        {
            int Emp, Matr;

            if (int.TryParse(txtCodEmpresaInsert.Text, out Emp) && int.TryParse(txtMatriculaInsert.Text, out Matr))
            {
                CarregarDadosPartic(Emp, Matr);
            }
        }

        protected void btnDadosCredenciado_Click(object sender, EventArgs e)
        {
            int cod_Contrato;

            if (int.TryParse(txtCodContratoInsert.Text, out cod_Contrato))
            {
                CarregarDadosCreden(cod_Contrato);
            }
        }

        #endregion

        #region "Consulta/alteração"

        protected void grdEditAjusteCorresp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    //Ini - Server para DropDownList de Motivo Devolução
                    DropDownList ddlTipoMotiDev = (DropDownList)e.Row.FindControl("ddlTipoMotiDev");
                    CloneDropDownList(ddlMotivoDevolucaoInsert, ddlTipoMotiDev);
                    ddlTipoMotiDev.DataBind();
                    ddlTipoMotiDev.SelectedValue = dr["ID_TIPOMOTIVODEVOLUCAO"].ToString();
                    //Fim - Server para DropDownList de Motivo Devolução

                    //Ini - Server para DropDownList de Tipo Documento
                    DropDownList ddlTipoDocumento = (DropDownList)e.Row.FindControl("ddlTipoDocumento");
                    CloneDropDownList(ddlTipoDocumentoInsert, ddlTipoDocumento);
                    ddlTipoDocumento.DataBind();
                    ddlTipoDocumento.SelectedValue = dr["ID_TIPODOCUMENTO"].ToString();
                    //Fim - Server para DropDownList de Tipo Documento

                }
                else if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    //Ini - Server para DropDownList de Motivo Devolução
                    DropDownList ddlTipoMotiDevUpd = (DropDownList)e.Row.FindControl("ddlTipoMotiDevUpd");
                    CloneDropDownList(ddlMotivoDevolucaoInsert, ddlTipoMotiDevUpd);
                    ddlTipoMotiDevUpd.DataBind();
                    ddlTipoMotiDevUpd.SelectedValue = dr["ID_TIPOMOTIVODEVOLUCAO"].ToString();
                    //Fim - Server para DropDownList de Motivo Devolução

                    //Ini - Server para DropDownList de Tipo Documento
                    DropDownList ddlTipoDocumentoUpd = (DropDownList)e.Row.FindControl("ddlTipoDocumentoUpd");
                    CloneDropDownList(ddlTipoDocumentoInsert, ddlTipoDocumentoUpd);
                    ddlTipoDocumentoUpd.DataBind();
                    ddlTipoDocumentoUpd.SelectedValue = dr["ID_TIPODOCUMENTO"].ToString();
                    //Fim - Server para DropDownList de Tipo Documento

                    //Ini - Utilizado para DropDownList de Tempo Prazo
                    DropDownList ddlTempoPrazoInc = (DropDownList)e.Row.FindControl("ddlTempoPrazoInc");
                    Label lblTempoPrazo = (Label)e.Row.FindControl("lblTempoPrazo");

                    ddlTempoPrazoInc.Items.Insert(0, new ListItem("---Selecione---", ""));
                    ddlTempoPrazoInc.Items.Insert(1, new ListItem("SIM", "SIM"));
                    ddlTempoPrazoInc.Items.Insert(2, new ListItem("NÃO", "NAO"));
                    ddlTempoPrazoInc.DataBind();

                    ddlTempoPrazoInc.SelectedValue = dr["Tempo_Prazo"].ToString();
                    //Fim - Utilizado para DropDownList de Tempo Prazo

                }
            }
        }

        #endregion

        #region "Aba de Inserir - Ajuste Linha Inseridas"

        protected void grdEditAjusteCorresp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
                CAD_TBL_CTRLDEV_CORRESP obj = new CAD_TBL_CTRLDEV_CORRESP();
                obj.ID_REG = Convert.ToInt16(((Label)grdEditAjusteCorresp.Rows[grdEditAjusteCorresp.EditIndex].FindControl("lblIdReg")).Text);
                obj.ID_TIPODOCUMENTO = Util.String2Int32(((DropDownList)grdEditAjusteCorresp.Rows[grdEditAjusteCorresp.EditIndex].FindControl("ddlTipoDocumento")).SelectedValue);

                Resultado res = CorrespondenciaBLL.AtualizaControleCorrespondencia(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdEditAjusteCorresp.EditIndex = -1;
                    grdEditAjusteCorresp.ShowFooter = false;
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdEditAjusteCorresp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdEditAjusteCorresp.EditIndex = e.NewEditIndex;
            showGridCtrlDevCorrespondecia(((Label)grdEditAjusteCorresp.Rows[grdEditAjusteCorresp.EditIndex].FindControl("lblIdReg")).Text);
        }

        protected void grdEditAjusteCorresp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdEditAjusteCorresp.EditIndex = -1;
            showGridCtrlAlteracoesCorresp();
        }

        protected void grdEditAjusteCorresp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdEditAjusteCorresp.EditIndex = -1;
            showGridCtrlAlteracoesCorresp();
        }

        #endregion

        #region "Aba Consulta - Ajuste das linhas"

        protected void btnFiltroTipoDocumento_Click(object sender, EventArgs e)
        {
            CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
            if (!string.IsNullOrEmpty(ddlFiltroTipoDocumento.SelectedItem.Text))
            {
                DataView dv = new DataView(CorrespondenciaBLL.buscarControleCorrespondencia());
                grdAlteracoesCorresp.DataSource = dv;
                dv.RowFilter = "Convert([ID_TIPODOCUMENTO], System.String) LIKE '" + ddlFiltroTipoDocumento.SelectedItem.Text.Trim() + "%'";
                grdAlteracoesCorresp.DataBind();
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Selecione um Tipo de Documento.");
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            showGridCtrlAlteracoesCorresp();
            grdAlteracoesCorresp.Visible = true;
            pnlGrdAlteracoesCorresp.Visible = true;
            pnlCtrlDevCorrespPesquisar.Visible = true;
            ddlFiltroTipoDocumento.Text = "";
        }


        protected void grdAlteracoesCorresp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAlteracoesCorresp.EditIndex = e.NewEditIndex;
            showGridCtrlAlteracoesCorresp(((Label)grdAlteracoesCorresp.Rows[grdAlteracoesCorresp.EditIndex].FindControl("lblIdReg")).Text);
        }

        protected void grdAlteracoesCorresp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdAlteracoesCorresp.EditIndex = -1;
            showGridCtrlAlteracoesCorresp();
        }

        protected void grdAlteracoesCorresp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAlteracoesCorresp.EditIndex = -1;
            showGridCtrlAlteracoesCorresp();
        }

        protected void grdAlteracoesCorresp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    //Ini - Server para DropDownList de Motivo Devolução
                    DropDownList ddlTipoMotiDev = (DropDownList)e.Row.FindControl("ddlTipoMotiDev");
                    CloneDropDownList(ddlMotivoDevolucaoInsert, ddlTipoMotiDev);
                    ddlTipoMotiDev.DataBind();
                    ddlTipoMotiDev.SelectedValue = dr["ID_TIPOMOTIVODEVOLUCAO"].ToString();
                    //Fim - Server para DropDownList de Motivo Devolução

                    //Ini - Server para DropDownList de Tipo Documento
                    DropDownList ddlTipoDocumento = (DropDownList)e.Row.FindControl("ddlTipoDocumento");
                    CloneDropDownList(ddlTipoDocumentoInsert, ddlTipoDocumento);
                    ddlTipoDocumento.DataBind();
                    ddlTipoDocumento.SelectedValue = dr["ID_TIPODOCUMENTO"].ToString();
                    //Fim - Server para DropDownList de Tipo Documento


                }
                else if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    //Ini - Server para DropDownList de Motivo Devolução
                    DropDownList ddlTipoMotiDevUpd = (DropDownList)e.Row.FindControl("ddlTipoMotiDevUpd");
                    CloneDropDownList(ddlMotivoDevolucaoInsert, ddlTipoMotiDevUpd);
                    ddlTipoMotiDevUpd.DataBind();
                    ddlTipoMotiDevUpd.SelectedValue = dr["ID_TIPOMOTIVODEVOLUCAO"].ToString();
                    //Fim - Server para DropDownList de Motivo Devolução

                    //Ini - Server para DropDownList de Tipo Documento
                    DropDownList ddlTipoDocumentoUpd = (DropDownList)e.Row.FindControl("ddlTipoDocumentoUpd");
                    CloneDropDownList(ddlTipoDocumentoInsert, ddlTipoDocumentoUpd);
                    ddlTipoDocumentoUpd.DataBind();
                    ddlTipoDocumentoUpd.SelectedValue = dr["ID_TIPODOCUMENTO"].ToString();
                    //Fim - Server para DropDownList de Tipo Documento

                    //Ini - Utilizado para DropDownList de Tempo Prazo
                    DropDownList ddlTempoPrazoInc = (DropDownList)e.Row.FindControl("ddlTempoPrazoInc");
                    Label lblTempoPrazo = (Label)e.Row.FindControl("lblTempoPrazo");

                    ddlTempoPrazoInc.Items.Insert(0, new ListItem("---Selecione---", ""));
                    ddlTempoPrazoInc.Items.Insert(1, new ListItem("SIM", "SIM"));
                    ddlTempoPrazoInc.Items.Insert(2, new ListItem("NÃO", "NAO"));
                    ddlTempoPrazoInc.DataBind();

                    ddlTempoPrazoInc.SelectedValue = dr["Tempo_Prazo"].ToString();
                    //Fim - Utilizado para DropDownList de Tempo Prazo

                }
            }
        }

        #endregion

        protected void btnLimparDados_Click(object sender, EventArgs e)
        {
            limparCamposCredenciado();
            limparCamposParticipantes();

        }

        void showGridCtrlDevCorrespondecia(string texto = "")
        {
            CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
            if (!string.IsNullOrEmpty(texto))
            {
                DataView dv = new DataView(CorrespondenciaBLL.buscarControleCorrespondencia());
                dv.RowFilter = "Convert([ID_REG], System.String) LIKE '" + texto.Trim() + "%'";
                grdControleDevCorresp.DataSource = dv;
                grdEditAjusteCorresp.DataSource = dv;
                pnlGrdDetalhe.Visible = true;
                pnlGrdCabecalhoCorresp.Visible = true;
                pnlGrdDetalhe.Visible = true;
                grdControleDevCorresp.DataBind();
                grdEditAjusteCorresp.DataBind();
            }

        }

        void showGridCtrlAlteracoesCorresp(string texto = "")
        {
            CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
            if (!string.IsNullOrEmpty(texto))
            {
                DataView dv = new DataView(CorrespondenciaBLL.buscarControleCorrespondencia());
                grdAlteracoesCorresp.DataSource = dv;
                dv.RowFilter = "Convert([ID_REG], System.String) LIKE '" + texto.Trim() + "%'";
            }
            else
            {
                DataView dv = new DataView(CorrespondenciaBLL.buscarControleCorrespondencia());
                grdAlteracoesCorresp.DataSource = dv;
            }
            grdAlteracoesCorresp.Visible = true;
            pnlGrdAlteracoesCorresp.Visible = true;
            pnlCtrlDevCorrespPesquisar.Visible = true;
            grdAlteracoesCorresp.DataBind();
        }

        bool validaCamposInsert()
        {

            //Valida Insert Titular
            if (string.IsNullOrEmpty(ddlTipoDocumentoInsert.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "O Contrato não pode ser em branco !");
                return false;
            }

            return true;

        }

        void preencherComboTipoMotivoDevolucao()
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            ddlMotivoDevolucaoInsert.DataSource = CtrlCorrespBLL.buscarTipoMotivoDevolucao();
            ddlMotivoDevolucaoInsert.DataValueField = "ID_REG";
            ddlMotivoDevolucaoInsert.DataTextField = "DESCRICAO";
            ddlMotivoDevolucaoInsert.DataBind();
            ddlMotivoDevolucaoInsert.Items.Insert(0, new ListItem("---Selecione---", ""));
        }

        void preencherComboTipoDocumento()
        {
            CtrlDevCorrespondenciaManutBLL CtrlCorrespBLL = new CtrlDevCorrespondenciaManutBLL();
            ddlTipoDocumentoInsert.DataSource = CtrlCorrespBLL.GetTipoDocumento().ToList();
            ddlTipoDocumentoInsert.DataValueField = "ID_REG";
            ddlTipoDocumentoInsert.DataTextField = "DESCRICAO";
            ddlTipoDocumentoInsert.DataBind();
            ddlTipoDocumentoInsert.Items.Insert(0, new ListItem("---Selecione---", ""));

            CloneDropDownList(ddlTipoDocumentoInsert, ddlFiltroTipoDocumento);
            ddlFiltroTipoDocumento.DataBind();
            ddlFiltroTipoDocumento.SelectedValue = "";
        }

        void limparCamposParticipantes()
        {
            lblTitular.Text = "";
            txtCodEmpresaInsert.Text = "";
            txtMatriculaInsert.Text = "";
            ddlCodRepresInsert.Items.Clear();
            ddlCodRepresInsert.DataBind();
            grdAlteracoesCorresp.Dispose();
            grdControleDevCorresp.Dispose();
            pnlGrdCabecalhoCorresp.Visible = false;
            pnlGrdDetalhe.Visible = false;
        }

        void limparCamposCredenciado()
        {
            txtCodContratoInsert.Text = "";
            lblCredenciado.Text = "";
            grdAlteracoesCorresp.Dispose();
            grdControleDevCorresp.Dispose();
            pnlGrdCabecalhoCorresp.Visible = false;
            pnlGrdDetalhe.Visible = false;
        }

        protected void btnCancelarPopUp_Click(object sender, EventArgs e)
        {
            lblNomeCredenciadoEnd.Text = "";
            lblContratoCredenciadoEnd.Text = "";
            lblEmpresaPatrociEnd.Text = "";
            lblMatriculaPatrociEnd.Text = "";
            lblNomePatrociEnd.Text = "";
            lblRepressPatrociEnd.Text = "";
            lblPopEndereco.Text = "";
            lblPopCep.Text ="";
            lblPopNumero.Text = "";
            lblPopComplemento.Text = "";
            lblPopBairro.Text = "";
            lblPopMunicipio.Text = "";
            lblPopUF.Text = "";
            pnlPatrocinadorEnd.Visible = false;
            pnlCredenciadoEnd.Visible = false;

        }

        protected void grdAlteracoesCorresp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;

                if (!string.IsNullOrEmpty(((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblNumContrato")).Text))
                {
                    lblNomeCredenciadoEnd.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblNome")).Text;
                    lblContratoCredenciadoEnd.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblNumContrato")).Text;
                    pnlPatrocinadorEnd.Visible = false;
                    pnlCredenciadoEnd.Visible = true;
                }
                else {

                    lblEmpresaPatrociEnd.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblEmpresa")).Text;
                    lblMatriculaPatrociEnd.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblMatricula")).Text;
                    lblRepressPatrociEnd.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblNumRepress")).Text;
                    lblNomePatrociEnd.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblNome")).Text;
                    pnlPatrocinadorEnd.Visible = true;
                    pnlCredenciadoEnd.Visible = false;
                }


                lblPopEndereco.Text = " " +  ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblEndereco")).Text;
                lblPopCep.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblCEP")).Text;
                lblPopNumero.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblNumero")).Text;
                lblPopComplemento.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblComplente")).Text;
                lblPopBairro.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblBairro")).Text;
                lblPopMunicipio.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblMunicipio")).Text;
                lblPopUF.Text = " " + ((Label)grdAlteracoesCorresp.Rows[rowIndex].FindControl("lblUF")).Text;
              
                ModalPopupExtender1.Show();
            }

            if (e.CommandName == "Update")
            {
                CtrlDevCorrespondenciaBLL CorrespondenciaBLL = new CtrlDevCorrespondenciaBLL();
                CAD_TBL_CTRLDEV_CORRESP obj = new CAD_TBL_CTRLDEV_CORRESP();
                obj.ID_REG = Convert.ToInt16(((Label)grdAlteracoesCorresp.Rows[grdAlteracoesCorresp.EditIndex].FindControl("lblIdReg")).Text);
                obj.ID_TIPODOCUMENTO = Util.String2Int32(((DropDownList)grdAlteracoesCorresp.Rows[grdAlteracoesCorresp.EditIndex].FindControl("ddlTipoDocumento")).SelectedValue);

                Resultado res = CorrespondenciaBLL.AtualizaControleCorrespondencia(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdAlteracoesCorresp.EditIndex = -1;
                    grdAlteracoesCorresp.ShowFooter = false;
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

    }
}