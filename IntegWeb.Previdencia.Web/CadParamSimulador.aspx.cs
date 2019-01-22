using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao;
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
    public partial class CadParamSimulador : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CadParamSimuladorBLL bll = new CadParamSimuladorBLL();

                ddlUnidMonetaria.DataSource = bll.GetUnidMonetaria();
                ddlUnidMonetaria.DataValueField = "COD_UM";
                ddlUnidMonetaria.DataTextField = "NOM_ABRVO_UM";
                ddlUnidMonetaria.DataBind();
                ddlUnidMonetaria.Items.Insert(0, new ListItem("---Selecione---", ""));

                ddlPlano.DataSource = bll.GetPlano();
                ddlPlano.DataValueField = "NUM_PLBNF";
                ddlPlano.DataTextField = "DCR_PLBNF";
                ddlPlano.DataBind();
                ddlPlano.Items.Insert(0, new ListItem("---Selecione---", ""));

                //ABA 2
                ddlPlanoAB2.DataSource = bll.GetPlano();
                ddlPlanoAB2.DataValueField = "NUM_PLBNF";
                ddlPlanoAB2.DataTextField = "DCR_PLBNF";
                ddlPlanoAB2.DataBind();
                ddlPlanoAB2.Items.Insert(0, new ListItem("---Selecione---", ""));
            }
        }

        #region .: ABA 1 :.

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
            ddlPlanoInc.DataSource = bll.GetPlano();
            ddlPlanoInc.DataValueField = "NUM_PLBNF";
            ddlPlanoInc.DataTextField = "DCR_PLBNF";
            ddlPlanoInc.DataBind();

            ddlPlanoInc.Items.Insert(0, new ListItem("---Selecione---", ""));

            ddlUnidMonetariaInc.DataSource = bll.GetUnidMonetaria();
            ddlUnidMonetariaInc.DataValueField = "COD_UM";
            ddlUnidMonetariaInc.DataTextField = "NOM_ABRVO_UM";
            ddlUnidMonetariaInc.DataBind();
            ddlUnidMonetariaInc.Items.Insert(0, new ListItem("---Selecione---", ""));

            if (Session["objUser"] != null)
            {
                var userInc = (ConectaAD)Session["objUser"];
                txtUsuario.Text = userInc.login;
            }
            txtDtInclusao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtUsuario.ReadOnly = true;
            txtUsuario.Enabled = false;
            txtDtInclusao.ReadOnly = true;
            txtDtInclusao.Enabled = false;
            txtEmpresaInc.ReadOnly = false;
            txtEmpresaInc.Enabled = true;
            ddlPlanoInc.Enabled = true;
            ddlSexoInc.Enabled = true;

            divPesquisa.Visible = false;
            divInserir.Visible = true;

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdTabuaAtuarial.EditIndex = -1;
            grdTabuaAtuarial.PageIndex = 0;
            grdTabuaAtuarial.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtEmpresa.Text = "";
            ddlUnidMonetaria.SelectedValue = "";
            ddlPlano.SelectedValue = "";
            txtdatIniRef.Text = "";
            txtdatFimRef.Text = "";
            grdTabuaAtuarial.EditIndex = -1;
            grdTabuaAtuarial.PageIndex = 0;
            grdTabuaAtuarial.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtEmpresaInc.Text = "";
            txtDtReferencia.Text = "";
            ddlPlanoInc.Text = "";
            ddlUnidMonetariaInc.Text = "";
            txtTabAtuarial.Text = "";
            ddlSexoInc.SelectedValue = "";
            txtValor.Text = "";
            txtValorMedio.Text = "";
            txtINSS.Text = "";
            txtPorcentBD.Text = "";
            txtPorcentInvalid.Text = "";
            txtLimContribEmp.Text = "";
            txtUQP.Text = "";
            txtJurosAnu.Text = "";
            txtJurosPadrao.Text = "";
            txtJurosMax.Text = "";
            txtUsuario.Text = "";
            txtDtInclusao.Text = "";

            txtUsuario.Text = "";
            txtDtInclusao.Text = "";

            txtEmpresaInc.ReadOnly = false;
            txtEmpresaInc.Enabled = true;
            ddlPlanoInc.Enabled = true;
            ddlSexoInc.Enabled = true;

            divInserir.Visible = false;
            divPesquisa.Visible = true;

            grdTabuaAtuarial.EditIndex = -1;
            grdTabuaAtuarial.PageIndex = 0;
            grdTabuaAtuarial.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
            PRE_TBL_POR_UREF obj = new PRE_TBL_POR_UREF();
            UNIDADE_MONETARIA objUm = new UNIDADE_MONETARIA();

            Button btnEditar = (Button)sender;

            ddlPlanoInc.DataSource = bll.GetPlano();
            ddlPlanoInc.DataValueField = "NUM_PLBNF";
            ddlPlanoInc.DataTextField = "DCR_PLBNF";
            ddlPlanoInc.DataBind();

            ddlPlanoInc.Items.Insert(0, new ListItem("---Selecione---", ""));

            ddlUnidMonetariaInc.DataSource = bll.GetUnidMonetaria();
            ddlUnidMonetariaInc.DataValueField = "COD_UM";
            ddlUnidMonetariaInc.DataTextField = "NOM_ABRVO_UM";
            ddlUnidMonetariaInc.DataBind();
            ddlUnidMonetariaInc.Items.Insert(0, new ListItem("---Selecione---", ""));

            if (btnEditar.Text == "Editar")
            {
                GridViewRow row = (GridViewRow)btnEditar.NamingContainer;

                obj = bll.GetUrefData(Util.String2Short(row.Cells[0].Text), Util.String2Short((row.FindControl("lblPlano2") as Label).Text), Util.String2Short((row.FindControl("lblSexo2") as Label).Text));

                txtEmpresaInc.Text = obj.EMPRESA.ToString();
                DateTime dtReferencia = Convert.ToDateTime(obj.DT_REFERENCIA);
                txtDtReferencia.Text = dtReferencia.ToString("dd/MM/yyyy");
                ddlPlanoInc.SelectedValue = obj.PLANO.ToString();
                ddlUnidMonetariaInc.SelectedValue = obj.CODIGO_UM.ToString();
                txtTabAtuarial.Text = obj.TB_ATUARIAL.ToString();
                ddlSexoInc.SelectedValue = obj.SEXO.ToString();
                //CarregaValores();
                txtValor.Text = obj.VALOR.ToString();
                txtValorMedio.Text = obj.VALOR_MEDIO.ToString();                
                txtINSS.Text = obj.TETO_INSS.ToString();
                txtPorcentBD.Text = obj.PERC_MINIMO.ToString();
                txtPorcentInvalid.Text = obj.PERC_INV.ToString();
                txtLimContribEmp.Text = obj.LIM_PERC.ToString();
                txtUQP.Text = obj.UQP.ToString();
                txtJurosAnu.Text = obj.JUROSA.ToString();
                txtJurosPadrao.Text = obj.JUROSPADRAP.ToString();
                txtJurosMax.Text = obj.JUROSMAX.ToString();

                if (Session["objUser"] != null)
                {
                    var userEdt = (ConectaAD)Session["objUser"];
                    txtUsuario.Text = userEdt.login;
                }
                txtDtInclusao.Text = DateTime.Now.ToString();
                txtDtInclusao.ReadOnly = true;
                txtDtInclusao.Enabled = false;
                txtEmpresaInc.ReadOnly = true;
                txtEmpresaInc.Enabled = false;
                ddlPlanoInc.Enabled = false;
                ddlSexoInc.Enabled = false;

            }
            divPesquisa.Visible = false;
            divInserir.Visible = true;
            grdTabuaAtuarial.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
            PRE_TBL_POR_UREF obj = new PRE_TBL_POR_UREF();
            UNIDADE_MONETARIA objUm = new UNIDADE_MONETARIA();
            var user = (ConectaAD)Session["objUser"];

            Button btnNovo = (Button)sender;

            if (btnNovo.Text == "Salvar")
            {
                obj.EMPRESA = Convert.ToInt16(txtEmpresaInc.Text);
                obj.DT_REFERENCIA = Convert.ToDateTime(txtDtReferencia.Text);
                obj.PLANO = Convert.ToInt16(ddlPlanoInc.Text);
                obj.CODIGO_UM = Convert.ToInt16(ddlUnidMonetariaInc.Text);
                objUm.COD_UM = Convert.ToInt16(ddlUnidMonetariaInc.Text);
                objUm = bll.GetNomeUnidadeMonetaria(objUm.COD_UM);
                obj.DESCRICAO_UM = objUm.NOM_UM;
                obj.TB_ATUARIAL = Convert.ToInt16(txtTabAtuarial.Text);
                obj.SEXO = Convert.ToInt16(ddlSexoInc.Text);
                obj.VALOR = Convert.ToDecimal(txtValor.Text);
                obj.VALOR_MEDIO = Convert.ToDecimal(txtValorMedio.Text);
                obj.TETO_INSS = Convert.ToDecimal(txtINSS.Text);
                obj.PERC_MINIMO = Convert.ToDecimal(txtPorcentBD.Text);
                obj.PERC_INV = Convert.ToDecimal(txtPorcentInvalid.Text);
                obj.LIM_PERC = Convert.ToDecimal(txtLimContribEmp.Text);
                obj.UQP = Convert.ToDecimal(txtUQP.Text);
                obj.JUROSA = Convert.ToDecimal(txtJurosAnu.Text);
                obj.JUROSPADRAP = Convert.ToDecimal(txtJurosPadrao.Text);
                obj.JUROSMAX = Convert.ToDecimal(txtJurosMax.Text);
                obj.LOG_INCLUSAO = (user != null ? user.login : "Desenv").ToString();
                obj.DTH_INCLUSAO = Convert.ToDateTime(txtDtInclusao.Text);
                obj.DTH_EXCLUSAO = null;
                obj.LOG_EXCLUSAO = null;

                Resultado res = bll.InserirUref(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Novo Registro Inserido com Sucesso");
                    btnCancelar_Click(null, null);
                    grdTabuaAtuarial.DataBind();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
            PRE_TBL_POR_UREF obj = new PRE_TBL_POR_UREF();

            Button btnExcluir = (Button)sender;

            if (btnExcluir.Text == "Excluir")
            {
                GridViewRow row = (GridViewRow)btnExcluir.NamingContainer;

                obj = bll.GetUrefData(Util.String2Short(row.Cells[0].Text), Util.String2Short((row.FindControl("lblPlano2") as Label).Text), Util.String2Short((row.FindControl("lblSexo2") as Label).Text));
                Resultado res = bll.ExcluirUref(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Excluido com Sucesso");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de excluir.\\nErro: " + res.Mensagem);
                }
                btnCancelar_Click(null, null);
                grdTabuaAtuarial.DataBind();
            }
        }

        #endregion

        #region .: ABA 2 :.

        protected void btnPesquisarAB2_Click(object sender, EventArgs e)
        {
            grdTaxasContribucao.DataBind();
        }

        protected void btnLimparAB2_Click(object sender, EventArgs e)
        {
            ddlPlanoAB2.SelectedValue = "";
            grdTaxasContribucao.ShowFooter = false;
            grdTaxasContribucao.EditIndex = -1;
            grdTaxasContribucao.PageIndex = 0;
            grdTaxasContribucao.DataBind();
        }

        protected void btnNovoAB2_Click(object sender, EventArgs e)
        {
            grdTaxasContribucao.ShowFooter = true;
        }

        protected void grdTaxasContribucao_RowCommand(object sender, GridViewCommandEventArgs e)
         {

            if (e.CommandName == "UpdateAb2")
            {
                CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
                PRE_TBL_POR_CONT obj = new PRE_TBL_POR_CONT();
                var user = (ConectaAD)Session["objUser"];

                obj.PLANO = Convert.ToInt16(((Label)grdTaxasContribucao.Rows[grdTaxasContribucao.EditIndex].FindControl("lblPlanoAb2")).Text);
                obj.PERC_F1 = Convert.ToDecimal(((TextBox)grdTaxasContribucao.Rows[grdTaxasContribucao.EditIndex].FindControl("txtPercF1")).Text);
                obj.PERC_F2 = Convert.ToDecimal(((TextBox)grdTaxasContribucao.Rows[grdTaxasContribucao.EditIndex].FindControl("txtPercF2")).Text);
                obj.PERC_F3 = Convert.ToDecimal(((TextBox)grdTaxasContribucao.Rows[grdTaxasContribucao.EditIndex].FindControl("txtPercF3")).Text);
                obj.PERC_EM = Convert.ToDecimal(((TextBox)grdTaxasContribucao.Rows[grdTaxasContribucao.EditIndex].FindControl("txtPercEm")).Text);
                obj.LOG_INCLUSAO = user.login.ToString();
                obj.DTH_INCLUSAO = DateTime.Now;
                obj.DTH_EXCLUSAO = null;
                obj.LOG_EXCLUSAO = null;

                Resultado res = bll.AtualizarCont(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Atualizado com Sucesso");
                    grdTaxasContribucao.EditIndex = -1;
                    grdTaxasContribucao.PageIndex = 0;
                    grdTaxasContribucao.ShowFooter = false;
                    grdTaxasContribucao.DataBind();

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
            else if (e.CommandName == "AddNew")
            {
                CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
                PRE_TBL_POR_CONT obj = new PRE_TBL_POR_CONT();
                var user = (ConectaAD)Session["objUser"];

                obj.PLANO = Convert.ToInt16(((TextBox)grdTaxasContribucao.FooterRow.FindControl("txtPlanoAb2")).Text);
                obj.PERC_F1 = Convert.ToDecimal(((TextBox)grdTaxasContribucao.FooterRow.FindControl("txtPercf1f")).Text);
                obj.PERC_F2 = Convert.ToDecimal(((TextBox)grdTaxasContribucao.FooterRow.FindControl("txtPercf2f")).Text);
                obj.PERC_F3 = Convert.ToDecimal(((TextBox)grdTaxasContribucao.FooterRow.FindControl("txtPercf3f")).Text);
                obj.PERC_EM = Convert.ToDecimal(((TextBox)grdTaxasContribucao.FooterRow.FindControl("txtPercemf")).Text);
                obj.LOG_INCLUSAO = user.login.ToString();
                obj.DTH_INCLUSAO = DateTime.Now;
                obj.DTH_EXCLUSAO = null;
                obj.LOG_EXCLUSAO = null;
  
                    Resultado res = bll.AtualizarCont(obj);

                    if (res.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Novo Registro Inserido com Sucesso");
                        grdTaxasContribucao.EditIndex = -1;
                        grdTaxasContribucao.PageIndex = 0;
                        grdTaxasContribucao.ShowFooter = false;
                        grdTaxasContribucao.DataBind();
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                    }
            }
            else if (e.CommandName == "CancelAdd")
            {
                grdTaxasContribucao.ShowFooter = false;
            }
            else if (e.CommandName == "DeleteAb2")
            {
                CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
                PRE_TBL_POR_CONT obj = new PRE_TBL_POR_CONT();
                var user = (ConectaAD)Session["objUser"];

                obj.PLANO = Convert.ToInt16(e.CommandArgument.ToString());
                obj.LOG_INCLUSAO = user.login.ToString();
                obj.DTH_INCLUSAO = DateTime.Now;

                Resultado res = bll.ExcluirCont(obj);
                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Excluido com Sucesso");
                    grdTaxasContribucao.EditIndex = -1;
                    grdTaxasContribucao.PageIndex = 0;
                    grdTaxasContribucao.ShowFooter = false;
                    grdTaxasContribucao.DataBind();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de excluir.\\nErro: " + res.Mensagem);
                }

            }

        }

        protected void grdTaxasContribucao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdTaxasContribucao.EditIndex = e.NewEditIndex;
            grdTaxasContribucao.DataBind();
        }

        #endregion

        #region .: ABA 3 :.

        protected void btnSalvarAB3_Click(object sender, EventArgs e)
        {
            CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
            PRE_TBL_POR_DREF obj = new PRE_TBL_POR_DREF();
            PRE_TBL_POR_UREF objUref = new PRE_TBL_POR_UREF();
            

            var user = (ConectaAD)Session["objUser"];

                obj.COD_DREF = 1;
                obj.DT_REF_PROC = Util.String2Date(txtDatRefProcessamento.Text);
                obj.DT_REF_INSS = Util.String2Date(txtDatRefInss.Text);
                obj.PERC_INSS = Util.String2Decimal(txtPercentualInss.Text);
                obj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                obj.DTH_INCLUSAO = DateTime.Now;
                obj.DTH_EXCLUSAO = null;
                obj.LOG_EXCLUSAO = null;

                objUref.DT_REFERENCIA = obj.DT_REF_PROC;
                objUref.DTH_INCLUSAO = obj.DTH_INCLUSAO;
                objUref.LOG_INCLUSAO = obj.LOG_INCLUSAO;

                Resultado res = bll.InserirParametro(obj);
                if (res.Ok)
                {
                    res = bll.InserirDataReferencia(objUref);
                }

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Novo Registro Inserido com Sucesso");
                    txtDatRefProcessamento.Text = "";
                    txtDatRefInss.Text = "";
                    txtPercentualInss.Text = "";
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
                grdTabuaAtuarial.DataBind();
        }

        protected void btnLimparAB3_Click(object sender, EventArgs e)
        {
            txtDatRefProcessamento.Text = "";
            txtDatRefInss.Text = "";
            txtPercentualInss.Text = "";

            grdTabuaAtuarial.DataBind();
        }
        #endregion

        protected void txtDtReferencia_TextChanged(object sender, EventArgs e)
        {
            CarregaValores();
        }

        protected void btnCalcularValor_Click(object sender, EventArgs e)
        {
            CalcularValor();
        }

        protected void btnCalcularValorMedio_Click(object sender, EventArgs e)
        {
            CalcularValorMedio();
        }

        protected void CarregaValores()
        {
            CalcularValor();
            CalcularValorMedio();
        }

        protected void CalcularValor()
        {
            CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
            PRE_TBL_POR_UREF obj = new PRE_TBL_POR_UREF();
            obj.CODIGO_UM = Convert.ToInt16(ddlUnidMonetariaInc.Text);
            obj.DT_REFERENCIA = Util.String2Date(txtDtReferencia.Text);

            decimal? valor = bll.CarregarValor(Convert.ToInt16(ddlUnidMonetariaInc.Text), Util.String2Date(txtDtReferencia.Text));
            txtValor.Text = "";
            if (valor != null)
            {
                txtValor.Text = valor.ToString();
            }
        }

        protected void CalcularValorMedio()
        {
            CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
            PRE_TBL_POR_UREF obj = new PRE_TBL_POR_UREF();
            obj.CODIGO_UM = Convert.ToInt16(ddlUnidMonetariaInc.Text);
            obj.DT_REFERENCIA = Util.String2Date(txtDtReferencia.Text);
            //decimal? valorMedio = bll.CalculaValorMedio(obj.CODIGO_UM, obj.DT_REFERENCIA ?? DateTime.Now);
            decimal valorMedio =
                bll.CalculaValorMedio(obj.CODIGO_UM, obj.DT_REFERENCIA ?? DateTime.Now);
            txtValorMedio.Text = "";
            if (valorMedio > 0)
            {
                txtValorMedio.Text = Math.Round(valorMedio, 2).ToString();
            }
        }
    }
}