using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao;

namespace IntegWeb.Previdencia.Web
{
    public partial class ArquivoParam : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            lblPesquisa_Mensagem.Visible = false;
            lblNovo_Mensagem.Visible = false;

            if (!IsPostBack)
            {
                ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();
                CarregaDropDowList(ddlPesqGrupo, bll.GetGrupoDdl().ToList<object>(), "DCR_GRUPO_EMPRS", "COD_GRUPO_EMPRS");
                CarregaDropDowList(ddlPesqArea, bll.GetAreaDdl().ToList<object>(), "DCR_ARQ_C_AREA_SUB", "COD_ARQ_AREA");

                ListItem SELECIONE = ddlPesqGrupo.Items.FindByValue("0");
                SELECIONE.Value = "";
                CloneDropDownList(ddlPesqGrupo, ddlGrupo);
                SELECIONE.Text = "<TODOS>";

                SELECIONE = ddlPesqArea.Items.FindByValue("0");
                SELECIONE.Value = "";
                CloneDropDownList(ddlPesqArea, ddlArea);
                SELECIONE.Text = "<TODOS>";                
                
            }
        }

        protected void ddlPesqTipoParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPesqGrupo.Enabled = true;
            ddlPesqArea.Enabled = true;
            switch (ddlPesqTipoParam.SelectedValue)
            {
                case "EMAIL_PATROCINADORA":
                    ddlPesqArea.Enabled = false;
                    break;
                case "EMAIL_AREA":
                    ddlPesqGrupo.Enabled = false;
                    break;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(ddlPesqTipoParam.Text) &&
                String.IsNullOrEmpty(ddlPesqGrupo.Text))
            {
                MostraMensagem(lblPesquisa_Mensagem, "Entre com pelo menos um campo para pesquisar");
                return;
            }

            grdParam.EditIndex = -1;
            grdParam.PageIndex = 0;
            CarregarTela();
        }

        protected void grdParam_RowEditing(object sender, GridViewEditEventArgs e)
        {
            IOrderedDictionary keys = grdParam.DataKeys[e.NewEditIndex].Values;
            int PK_COD_PARAM = Int16.Parse(keys["COD_ARQ_PARAM"].ToString());
            //int PK_NUM_VER_FUND = Int32.Parse(keys["NUM_VER_FUND"].ToString());
            //int PK_NUM_VER_DEST = Int32.Parse(keys["NUM_VER_DEST"].ToString());
            CarregarParametro(PK_COD_PARAM);
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;
            e.Cancel = true;
        }

        protected void grdParam_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int PK_COD_PARAM = Int32.Parse(e.Keys["COD_ARQ_PARAM"].ToString());

            ArqParametrosBLL obj = new ArqParametrosBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteData(PK_COD_PARAM);
            MostraMensagem(lblPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            CarregarTela();
            e.Cancel = true;
        }

        private void CarregarTela()
        {
            grdParam.DataBind();
        }

        private void CarregarParametro(int pCodParametro)
        {
            ArqParametrosBLL obj = new ArqParametrosBLL();
            PRE_TBL_ARQ_PARAM loadObj = new PRE_TBL_ARQ_PARAM();
            loadObj = obj.GetParametro(pCodParametro);
            hidCodParametro.Value = loadObj.COD_ARQ_PARAM.ToString();
            LblParametro.Text  = loadObj.NOM_PARAM;
            hidParametro.Value = loadObj.NOM_PARAM;
            ddlGrupo.SelectedValue = "";
            if (loadObj.COD_GRUPO_EMPRS != null)
            {
                ddlGrupo.SelectedValue = loadObj.COD_GRUPO_EMPRS.ToString();
            }
            ddlArea.SelectedValue = "";
            if (loadObj.COD_ARQ_AREA != null)
            {
                ddlArea.SelectedValue = loadObj.COD_ARQ_AREA.ToString();
            }

            ddlGrupo.Enabled = true;
            ddlArea.Enabled = true;
            switch (LblParametro.Text)
            {
                case "EMAIL_PATROCINADORA":
                    ddlArea.Enabled = false;
                    break;
                case "EMAIL_AREA":
                    ddlGrupo.Enabled = false;
                    break;
            }

            txtParametro.Text = loadObj.DCR_PARAM;
            txtSubParam.Text = loadObj.NOM_PARAM_SUB;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            //txtPesqEmpresa.Text = "";
            //txtPesqVerba.Text = "";
            //ddlPesqTipoParam.SelectedValue = "0";
            ddlPesqGrupo.SelectedValue = "";
            ddlPesqArea.SelectedValue = "";
            //txtPesqGrupoContrib.Text = "";
            //txtPesqNumPlano.Text = "";
            grdParam.EditIndex = -1;
            grdParam.PageIndex = 0;
            CarregarTela();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparControles(pnlNovo.Controls);

            LblParametro.Text = ddlPesqTipoParam.SelectedItem.Text;
            hidParametro.Value = ddlPesqTipoParam.SelectedValue;
            hidCodParametro.Value = "0";            
            ddlGrupo.Enabled = ddlPesqGrupo.Enabled;
            if (ddlGrupo.Enabled)
            {
                ddlGrupo.SelectedValue = ddlPesqGrupo.SelectedValue;
            }
            else
            {
                ddlGrupo.SelectedValue = "";
            }
            ddlArea.Enabled = ddlPesqArea.Enabled;
            if (ddlArea.Enabled)
            {
                ddlArea.SelectedValue = ddlPesqArea.SelectedValue;
            }
            else
            {
                ddlArea.SelectedValue = "";
            }
            txtParametro.Text = "";
            txtSubParam.Text = "";
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            ArqParametrosBLL obj = new ArqParametrosBLL();
            PRE_TBL_ARQ_PARAM newobj = new PRE_TBL_ARQ_PARAM();
            var user = (ConectaAD)Session["objUser"];

            newobj.COD_ARQ_PARAM = Util.String2Int32(hidCodParametro.Value) ?? 0;
            newobj.NOM_PARAM = hidParametro.Value;
            newobj.COD_GRUPO_EMPRS = Util.String2Short(ddlGrupo.SelectedValue.ToString());
            newobj.COD_ARQ_AREA = Util.String2Short(ddlArea.SelectedValue.ToString());
            newobj.DCR_PARAM = txtParametro.Text;
            newobj.NOM_PARAM_SUB = txtSubParam.Text;
            newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
            newobj.DTH_INCLUSAO = DateTime.Now;
            
            Resultado res = obj.Validar(newobj);

            if (res.Ok)
            {
                res = obj.SaveData(newobj);
                if (res.Ok)
                {
                    MostraMensagem(lblPesquisa_Mensagem, res.Mensagem, "n_ok");
                    Voltar();
                }
                else
                {
                    MostraMensagem(lblNovo_Mensagem, res.Mensagem, "n_error");
                }
            }
            else
            {
                MostraMensagem(lblNovo_Mensagem, res.Mensagem);
            }
        }

        private void Voltar()
        {
            pnlNovo.Visible = false;
            pnlLista.Visible = true;
            pnlPesquisa.Visible = true;
            CarregarTela();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Voltar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Voltar();
        }

    }
}