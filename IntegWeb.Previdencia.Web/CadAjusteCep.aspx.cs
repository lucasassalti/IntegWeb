using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class CadAjusteCep : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        #region "Chamadas da aba Municipio"

        protected void grdDadosMunicipio_PreRender(object sender, EventArgs e)
        {
            if (grdDadosMunicipio.Rows.Count > 0)
            {
                grdDadosMunicipio.UseAccessibleHeader = true;
                grdDadosMunicipio.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        public void likePesquisarMunicipio(string valorLike)
        {
            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();

            DataView dv = new DataView(CadAjuste.buscarMunicipio());

            dv.RowFilter = "DCR_MUNICI like'" + valorLike + "%'";

            grdDadosMunicipio.DataSource = dv;
            grdDadosMunicipio.DataBind();

        }

        //protected void btnHabilitaGrdMunic_Click(object sender, EventArgs e)
        //{
        //    grdDadosCEP.Visible = false;
        //    grdDadosMunicipio.Visible = true;
        //    pnlInserirMunic.Visible = false;
        //    txtPesquisarCodMunicDescMunic.Text = "";
        //    grdDadosMunicipio.EditIndex = -1;

        //    if (grdDadosMunicipio.Rows.Count < 1000)
        //    {
        //        showGridMunic();
        //    }


        //}

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqUF.Text = "";
            txtPesqCidade.Text = "";
            txtPesqCodMunic.Text = "";
            txtPesqCodIBGE.Text = "";
            pnlInserirMunic.Visible = false;
            grdDadosMunicipio.Visible = true;
            grdDadosMunicipio.DataBind();
        }

        protected void btnInserirMunicipio_Click(object sender, EventArgs e)
        {
            pnlInserirMunic.Visible = true;
            grdDadosMunicipio.Visible = false;
            preencherComboCEPMunic();

        }
        protected void btnPesquisarCodMunicDescMunic_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();

            if (string.IsNullOrEmpty(txtPesqUF.Text) &&
                string.IsNullOrEmpty(txtPesqCidade.Text) &&
                string.IsNullOrEmpty(txtPesqCodMunic.Text) &&
                string.IsNullOrEmpty(txtPesqCodIBGE.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Favor informar um campo para a pesquisa.");
            }
            else
            {
                grdDadosMunicipio.Visible = true;
                //DataView dv = new DataView(CadAjuste.buscarDescMunicOrCodMunic(txtPesquisarCodMunicDescMunic.Text));
                //DataView dv = new DataView(CadAjuste.SelectLikeDescMunicOrCodMunic(txtPesquisarCodMunicDescMunic.Text));
                //grdDadosMunicipio.DataSource = dv;
                //grdDadosMunicipio.DataSourceID = "";
                grdDadosMunicipio.DataBind();
            }
        }
        protected void btnCancelarMunicipio_Click(object sender, EventArgs e)
        {
            pnlInserirMunic.Visible = false;
            grdDadosMunicipio.Visible = true;
            grdDadosMunicipio.EditIndex = -1;
            grdDadosMunicipio.DataBind();

            LimparNovaInsercaoMunicipio();
        }
        protected void btnAddNewMunicipio_Click(object sender, EventArgs e)
        {

            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();
            MUNICIPIO obj = new MUNICIPIO();
            obj.COD_MUNICI_IBGE = txtCodMunicipioInserir.Text.Trim().ToUpper();
            obj.COD_MUNICI = txtCodMunicipioInserir.Text.Trim().ToUpper();
            obj.DCR_MUNICI = txtDescrMunicipio.Text.Trim().ToUpper();
            obj.COD_ESTADO = ddlCodEstadoMunicInsert.Text.Trim().ToUpper();
            obj.DCR_RSUMD_MUNICI = txtDescrResumidaMunicipio.Text.Trim().ToUpper();
            obj.COD_LOC_CEP = null;
            obj.PAICOD = 1;

            Resultado res = CadAjuste.InserirMunicipio(obj);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");                
                pnlInserirMunic.Visible = false;
                grdDadosMunicipio.Visible = true;
                grdDadosMunicipio.EditIndex = -1;
                grdDadosMunicipio.DataBind();
                LimparNovaInsercaoMunicipio();
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
            }

        }
        protected void grdDadosMunicipio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Resultado res = new Resultado();
            MUNICIPIO obj = new MUNICIPIO();
            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();

            switch (e.CommandName)
            {
                case "Gravar":

                    CadAjuste = new CadAjusteCepBLL();

                    
                    obj.COD_MUNICI_IBGE = Convert.ToString(((TextBox)grdDadosMunicipio.Rows[grdDadosMunicipio.EditIndex].FindControl("txtCodigoIBGE")).Text).Trim().ToUpper();
                    obj.DCR_MUNICI = Convert.ToString(((TextBox)grdDadosMunicipio.Rows[grdDadosMunicipio.EditIndex].FindControl("txtDcrMunicipio")).Text).Trim().ToUpper();
                    obj.DCR_RSUMD_MUNICI = Convert.ToString(((TextBox)grdDadosMunicipio.Rows[grdDadosMunicipio.EditIndex].FindControl("txtDcrResumida")).Text).Trim().ToUpper();

                    res = CadAjuste.AtualizaTabelaMunicipio(obj);

                    if (res.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                        grdDadosMunicipio.EditIndex = -1;
                        grdDadosMunicipio.ShowFooter = false;
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                    }
                    break;
                case "Excluir":

                    string[] Args = e.CommandArgument.ToString().Split(',');
                    
                    obj.COD_ESTADO = Args[0];
                    obj.COD_MUNICI = Args[1];
                    obj.COD_MUNICI_IBGE = Args[2];

                    res = CadAjuste.ExcluirMunicipio(obj);

                    if (res.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Excluido com Sucesso");
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de excluir.\\nErro: " + res.Mensagem);
                    }
                    //btnCancelar_Click(null, null);
                    grdDadosMunicipio.DataBind();

                    break;
            }
        }

        //protected void grdDadosMunicipio_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    int PK_COD_DEPOSITO_JUDIC = Int32.Parse(e.Keys["COD_DEPOSITO_JUDIC"].ToString());
        //    CadAjusteCepBLL obj = new CadAjusteCepBLL();
        //    //var user = (ConectaAD)Session["objUser"];
        //    //Resultado res = obj.DeleteData(PK_COD_DEPOSITO_JUDIC, (user != null) ? user.login : "Desenv");
        //    //MostraMensagem(pnlPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
        //    //CarregarTela();
        //    e.Cancel = true;
        //}

        protected void grdDadosMunicipio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdDadosMunicipio.EditIndex = -1;
            showGridMunic(Convert.ToString(((Label)grdDadosMunicipio.Rows[grdDadosMunicipio.EditIndex].FindControl("lblCodigoIBGE")).Text).Trim().ToUpper());
        }

        protected void grdDadosMunicipio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDadosMunicipio.EditIndex = e.NewEditIndex;
            showGridMunic(Convert.ToString(((Label)grdDadosMunicipio.Rows[grdDadosMunicipio.EditIndex].FindControl("lblCodigoIBGE")).Text).Trim().ToUpper());
        }

        protected void grdDadosMunicipio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdDadosMunicipio.EditIndex = -1;
            //showGridMunic(txtPesquisarCodMunicDescMunic.Text);
        }


        //protected void btnExcluir_Click(object sender, EventArgs e)
        //{
        //    CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();
        //    MUNICIPIO obj = new MUNICIPIO();

        //    Button btnExcluir = (Button)sender;

        //    if (btnExcluir.Text == "Excluir")
        //    {
        //        GridViewRow row = (GridViewRow)btnExcluir.NamingContainer;

        //        obj.COD_MUNICI_IBGE = (row.FindControl("lblCodigoIBGE") as Label).Text;
        //        obj.COD_ESTADO = (row.FindControl("lblCodigoEstado") as Label).Text;
        //        obj.COD_MUNICI = (row.FindControl("lblCodigoMunicipio") as Label).Text;


        //        Resultado res = CadAjuste.ExcluirMunicipio(obj);

        //        if (res.Ok)
        //        {
        //            MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Excluido com Sucesso");
        //        }
        //        else
        //        {
        //            MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de excluir.\\nErro: " + res.Mensagem);
        //        }
        //        //btnCancelar_Click(null, null);
        //        grdDadosMunicipio.DataBind();
        //    }
        //}


        /// <summary>
        /// Popula os dados na Grid de Municipio sem perde o DataSourceID
        /// </summary>
        /// <param name="texto"></param>
        void showGridMunic(String texto = "")
        {
            //DataTable dt = new DataTable();
            //CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();

            //DataView dv = new DataView();
            //if (string.IsNullOrEmpty(texto))
            //{
            //    dv = new DataView(CadAjuste.buscarMunicipio());
            //}
            //else
            //{
            //    dv = new DataView(CadAjuste.SelectLikeDescMunicOrCodMunic(texto));
            //}

            //grdDadosMunicipio.DataSource = dv;
            grdDadosMunicipio.DataBind();

        }
        void LimparNovaInsercaoMunicipio()
        {
            txtCodMunicipioInserir.Text = "";
            txtDescrMunicipio.Text = "";
            preencherComboCEPMunic();
        }

        void preencherComboCEPMunic()
        {
            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();
            ddlCodEstadoMunicInsert.DataSource = CadAjuste.GetEstado();
            ddlCodEstadoMunicInsert.DataValueField = "cod_estado";
            ddlCodEstadoMunicInsert.DataTextField = "dcr_estado";
            ddlCodEstadoMunicInsert.DataBind();
        }


        #endregion

        #region "Chamadas da aba CEP"

        protected void btnSalvarCep_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();

            // dt = CadAjuste.buscarCep();
        }
        protected void grdDadosCEP_PreRender(object sender, EventArgs e)
        {
            if (grdDadosCEP.Rows.Count > 0)
            {
                grdDadosCEP.UseAccessibleHeader = true;
                grdDadosCEP.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        protected void btnPesquisarCepLogradouro_Click(object sender, EventArgs e)
        {
            

            DataTable dt = new DataTable();
            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();

            if (string.IsNullOrEmpty(txtCEP_PesqUF.Text)
               && string.IsNullOrEmpty(txtCEP_PesqMunicipio.Text)
               && string.IsNullOrEmpty(txtCEP_PesqBairro.Text)
               && string.IsNullOrEmpty(txtCEP_PesqLogradouro.Text)
               && string.IsNullOrEmpty(txtCEP_PesqCEP.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Favor informar um campo para a pesquisa.");
            }
            else
            {
                //DataView dv = new DataView(CadAjuste.buscarPorCepOuLogradouro(txtPesquisarCepLogradouro.Text));
                //DataView dv = new DataView(CadAjuste.SelectLikeCEPOrLogradouro(txtPesquisarCepLogradouro.Text));
                //grdDadosCEP.DataSourceID = "";
                //grdDadosCEP.Visible = true;
                //grdDadosCEP.DataSource = dv;
                grdDadosCEP.DataBind();
            }

        }
        protected void btnLimparCep_Click(object sender, EventArgs e)
        {
            txtCEP_PesqUF.Text = "";
            txtCEP_PesqMunicipio.Text = "";
            txtCEP_PesqBairro.Text = "";
            txtCEP_PesqLogradouro.Text = "";
            txtCEP_PesqCEP.Text = "";
            pnlInserirCEP.Visible = false;
            grdDadosCEP.Visible = true;
            grdDadosCEP.DataBind();
        }
        //protected void btnBuscarCep_Click(object sender, EventArgs e)
        //{
        //    grdDadosMunicipio.Visible = false;
        //    pnlInserirCEP.Visible = false;
        //    grdDadosCEP.Visible = true;
        //    grdDadosCEP.EditIndex = -1;
        //    showGridCEP();
        //}
        protected void btnInserirCEP_Click(object sender, EventArgs e)
        {
            pnlInserirCEP.Visible = true;
            grdDadosCEP.Visible = false;
            preencherComboCEP();
        }
        protected void grdDadosCEP_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Gravar")
            {
                CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();
                TB_CEP obj = new TB_CEP();

                obj.NUM_CEP = Convert.ToString(((TextBox)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("txtNumCEP")).Text).Trim().ToUpper();
                //string teste = ((TextBox)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("txtCodEstado")).Text).Trim().ToUpper();
                obj.COD_ESTADO = Convert.ToString(((TextBox)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("txtCodigoEstado")).Text).Trim().ToUpper();
                obj.COD_MUNICI = Convert.ToString(((TextBox)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("txtCodigoMunicipio")).Text).Trim().ToUpper();
                obj.NOM_LOGRADOURO = Convert.ToString(((TextBox)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("txtNomeLogradouro")).Text).Trim().ToUpper();
                obj.DES_BAIRRO = Convert.ToString(((TextBox)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("txtDcrBairro")).Text).Trim().ToUpper();
                obj.DES_MUNICI = Convert.ToString(((TextBox)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("txtDcrMunicipio")).Text).Trim().ToUpper();
             
               
                if (string.IsNullOrEmpty(Convert.ToString(((DropDownList)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("ddlTipoLogradouroCEP")).Text)))
                {
                    obj.TIP_LOGRADOURO = Convert.ToString(((HiddenField)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("hidTipoLogradouroCEP")).Value).Trim().ToUpper();
                }
                else
                {
                    obj.TIP_LOGRADOURO = Convert.ToString(((DropDownList)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("ddlTipoLogradouroCEP")).Text).Trim().ToUpper();
                }

                Resultado res = CadAjuste.AtualizaTabelaCep(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                    grdDadosCEP.EditIndex = -1;
                    grdDadosCEP.ShowFooter = false;
                    grdDadosCEP.DataBind();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }
        protected void grdDadosCEP_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdDadosCEP.EditIndex = -1;
            showGridCEP();
        }

        protected void grdDadosCEP_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDadosCEP.EditIndex = e.NewEditIndex;

            //grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("ddlTipoLogradouroCEP")
            showGridCEP(Convert.ToString(((Label)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("lblNumCEP")).Text).Trim().ToUpper());
            //showGridCEP(Convert.ToString(((Label)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("lblCodEstadoCEP")).Text).Trim().ToUpper());
        }

        protected void grdDadosCEP_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdDadosCEP.EditIndex = -1;
            showGridCEP(Convert.ToString(((Label)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("lblNumCEP")).Text).Trim().ToUpper());
        }
        protected void btnAddNewCEP_Click(object sender, EventArgs e)
        {

            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();
            TB_CEP obj = new TB_CEP();
            obj.TIP_LOGRADOURO = ddlTipoLogradouroInsertCEP.Text.Trim().ToUpper();
            obj.NUM_CEP = txtNumeroCEP.Text.Trim().ToUpper();
            obj.DES_BAIRRO = txtBairroCEP.Text.Trim().ToUpper();
            obj.NOM_LOGRADOURO = txtDescLogradouroCEP.Text.Trim().ToUpper();
            obj.COD_ESTADO = ddlCodEstadoCEP.Text.Trim().ToUpper();
            obj.COD_MUNICI = txtCodMunicipioCEP.Text.Trim().ToUpper();
            obj.DES_MUNICI = txtDescMunicipioCEP.Text.Trim().ToUpper();
            obj.COD_LOC_CEP = null;
            obj.PAICOD = null;
            obj.COD_BAIRRO_INI = null;
            obj.COD_BAIRRO_FIM = null;

            Resultado res = CadAjuste.InseriCEP(obj);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                grdDadosCEP.EditIndex = -1;
                grdDadosCEP.DataBind();
                pnlInserirCEP.Visible = false;
                grdDadosCEP.Visible = true;
                LimparNovaInsercaoCEP();
                txtCEP_PesqUF.Text = "";
                txtCEP_PesqMunicipio.Text = "";
                txtCEP_PesqBairro.Text = "";
                txtCEP_PesqLogradouro.Text = "";
                txtCEP_PesqCEP.Text = "";

            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
            }

        }
        protected void btnCancelarCEP_Click(object sender, EventArgs e)
        {
            pnlInserirCEP.Visible = false;
            grdDadosCEP.EditIndex = -1;
            LimparNovaInsercaoCEP();
        }

        /// <summary>
        /// Popula os dados na Grid de CEP sem perde o DataSourceID
        /// </summary>
        /// <param name="texto"></param>
        void showGridCEP(String texto = "")
        {
            //DataView dv = new DataView();
            //CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();

            //if (!String.IsNullOrEmpty(texto))
            //{
            //    dv = new DataView(CadAjuste.SelectLikeCEPOrLogradouro(texto));

            //}
            //else
            //{
            //    dv = new DataView(CadAjuste.buscarCep());
            //}

            //grdDadosCEP.DataSource = dv;
            grdDadosCEP.DataBind();

        }
        void LimparNovaInsercaoCEP()
        {
            ddlTipoLogradouroInsertCEP.Text = "";
            txtNumeroCEP.Text = "";
            txtBairroCEP.Text = "";
            txtDescLogradouroCEP.Text = "";
            //ddlCodEstadoCEP.Text = "";
        }
        void preencherComboCEP()
        {

            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();
            ddlCodEstadoCEP.DataSource = CadAjuste.GetEstado();
            ddlCodEstadoCEP.DataValueField = "cod_estado";
            ddlCodEstadoCEP.DataTextField = "dcr_estado";
            ddlCodEstadoCEP.DataBind();
        }

        //protected void grdDadosCEP_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        //        {
        //            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();

        //            DropDownList ddList = (DropDownList)e.Row.FindControl("ddlTipoLogradouroCEP");

        //            DataView dv = new DataView(CadAjuste.buscarDescLogradouro(Convert.ToString(((HiddenField)grdDadosCEP.Rows[grdDadosCEP.EditIndex].FindControl("hidTipoLogradouroCEP")).Value)));
        //            ddList.DataSource = dv;
        //            ddList.DataTextField = "TIP_LOGRADOURO";
        //            ddList.DataValueField = "TIP_LOGRADOURO";
        //            ddList.DataBind();

        //        }
        //    }
        //}

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        protected DataTable tipoLogradouro()
        {

            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();
            DataTable dt = ConvertToDataTable(CadAjuste.GetCEP());

            return dt;
        }
        protected void grdDadosCEP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDadosCEP.EditIndex = -1;
            grdDadosCEP.PageIndex = e.NewPageIndex;
            showGridCEP();
        }

        protected void grdDadosCEP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    //Ini - Server para DropDownList de Tipo Negocio
                    DropDownList ddlTipoLogradouroCEP = (DropDownList)e.Row.FindControl("ddlTipoLogradouroCEP");
                    HiddenField hidTipoLog = (HiddenField)e.Row.FindControl("hidTipoLog");

                    ddlTipoLogradouroCEP.Items.Insert(0, new ListItem("---Selecione---", ""));
                    ddlTipoLogradouroCEP.Items.Insert(1, new ListItem("AVE", "AVE"));
                    ddlTipoLogradouroCEP.Items.Insert(2, new ListItem("ROD", "ROD"));
                    ddlTipoLogradouroCEP.Items.Insert(3, new ListItem("TRA", "TRA"));
                    ddlTipoLogradouroCEP.Items.Insert(4, new ListItem("VIL", "VIL"));
                    ddlTipoLogradouroCEP.Items.Insert(4, new ListItem("PCA", "PCA"));
                    ddlTipoLogradouroCEP.Items.Insert(5, new ListItem("RUA", "RUA"));
                    ddlTipoLogradouroCEP.Items.Insert(5, new ListItem("ALA", "ALA"));
                    ddlTipoLogradouroCEP.DataBind();

                    //bind dropdown-list
                    //ddlTipoLogradouroCEP.SelectedValue = (hidTipoLog.Value ?? "").Trim();
                    ddlTipoLogradouroCEP.SelectedValue = hidTipoLog.Value;
                    //Fim - Server para DropDownList de Tipo Negocio
                }

            }

        }

        #endregion

        protected void btnExcluirCep_Click(object sender, EventArgs e)
        {
            CadAjusteCepBLL CadAjuste = new CadAjusteCepBLL();
            TB_CEP obj = new TB_CEP();

            Button btnExcluir = (Button)sender;

            if (btnExcluir.Text == "Excluir")
            {
                GridViewRow row = (GridViewRow)btnExcluir.NamingContainer;

                obj.NUM_CEP = (row.FindControl("lblNumCEP") as Label).Text;
                obj.COD_ESTADO = (row.FindControl("lblCodigoEstado") as Label).Text;
                obj.COD_MUNICI = (row.FindControl("lblCodigoMunicipio") as Label).Text;


                Resultado res = CadAjuste.ExcluirCep(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Excluido com Sucesso");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de excluir.\\n " + res.Mensagem);
                }
                grdDadosCEP.DataBind();

                txtCEP_PesqUF.Text = "";
                txtCEP_PesqMunicipio.Text  = "";
                txtCEP_PesqBairro.Text  = "";
                txtCEP_PesqLogradouro.Text  = "";
                txtCEP_PesqCEP.Text = "";
                


            }
        }
    }
}