using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.IO;
using System.Data;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.DAL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Framework;
using IntegWeb.Entidades;

namespace IntegWeb.Previdencia.Web
{
    public partial class ArquivoRepasse1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagemInicial.Visible = false;
            lblMensagemNovo.Visible = false;
            lblMensagemImportacao.Visible = false;
            lblMensagemNovoAcerto.Visible = false;
            lblGridDetalhes_Mensagem.Visible = false;            

            if (!IsPostBack)
            {
                ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();

                 CarregaDropDowList(ddlStatusFiltro, bll.GetStatusDdl().ToList<object>(), "DCR_ARQ_STATUS", "COD_ARQ_STATUS");
                 ListItem SELECIONE = ddlStatusFiltro.Items.FindByValue("0");
                 //CloneDropDownList(ddlGrupo, ddlGrupoAb2);
                 SELECIONE.Text = "<TODOS>";
                 SELECIONE.Value = "";

                 CarregaDropDowList(ddlGrupoFiltro, bll.GetGrupoDdl().ToList<object>(), "DCR_GRUPO_EMPRS", "COD_GRUPO_EMPRS");
                 SELECIONE = ddlGrupoFiltro.Items.FindByValue("0");
                 CloneDropDownList(ddlGrupoFiltro, ddlGrupoAb2);
                 SELECIONE.Text = "<TODOS>";
                 SELECIONE.Value = "";
                 CloneDropDownList(ddlGrupoFiltro, ddlGrupo);

                 ConectaAD user = (ConectaAD)Session["objUser"];
                 List<ArqPatrocinadoraEnvioBLL.PRE_TBL_ARQ_AREA_View> lista_ddl = bll.GetAreaDdl(user);

                 CarregaDropDowList(ddlAreaFiltro, lista_ddl.ToList<object>(), "DCR_ARQ_C_AREA_SUB", "COD_ARQ_AREA");
                 SELECIONE = ddlAreaFiltro.Items.FindByValue("0");
                 CloneDropDownList(ddlAreaFiltro, ddlAreaAb2);
                 CloneDropDownList(ddlAreaFiltro, ddlArea);
                 SELECIONE.Text = "<TODOS>";
                 SELECIONE.Value = "";
                 if (lista_ddl.Count == 1)
                 {
                     ddlArea.Visible = false;
                     lblArea.Visible = false;
                     ddlArea.SelectedValue = lista_ddl[0].COD_ARQ_AREA.ToString();
                     ddlAreaFiltro.SelectedValue = ddlArea.SelectedValue;
                     ddlAreaAb2.SelectedValue = ddlArea.SelectedValue;
                 }

                 //string areas = "";
                 //lista_ddl.ForEach(a => areas += a.COD_ARQ_AREA + ",");
                 //SELECIONE.Value = areas.Substring(0, areas.Length-1);
            }
        }

        #region .:Aba 1:.

        #region .:Div Geracao:.

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdGeracao.EditIndex = -1;
            grdGeracao.PageIndex = 0;
            grdGeracao.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparFiltro();
            btnFiltrar.Enabled = true;
            pnlFiltro.Visible = false;
        }

        protected void btnNovoEmBranco_Click(object sender, EventArgs e)
        {
            ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();

            LimparCampos();

            hdfCodigoRepasse.Value = "0";
            hdfLinhaRep.Value = "0";
            hdfCodArqEnvio.Value = "0";

            ddlStatusDetalhes.DataSource = bll.GetStatusDdl();
            ddlStatusDetalhes.DataValueField = "COD_ARQ_STATUS";
            ddlStatusDetalhes.DataTextField = "DCR_ARQ_STATUS";
            ddlStatusDetalhes.DataBind();

            //ddlAreaDetalhes.DataSource = bll.GetAreaDdl();
            //ddlAreaDetalhes.DataValueField = "COD_ARQ_AREA";
            //ddlAreaDetalhes.DataTextField = "DCR_ARQ_AREA";
            //ddlAreaDetalhes.DataBind();

            ddlAreaDetalhes.DataSource = bll.GetAreaDdl();
            ddlAreaDetalhes.DataValueField = "COD_ARQ_AREA";
            ddlAreaDetalhes.DataTextField = "DCR_ARQ_C_AREA_SUB";
            ddlAreaDetalhes.DataBind();

            ddlGrupoDetalhes.DataSource = bll.GetGrupoDdl();
            ddlGrupoDetalhes.DataValueField = "COD_GRUPO_EMPRS";
            ddlGrupoDetalhes.DataTextField = "DCR_GRUPO_EMPRS";
            ddlGrupoDetalhes.DataBind();

            grdDetalhes.EditIndex = -1;
            grdDetalhes.PageIndex = 0;
            grdDetalhes.DataBind();

            ValidaComando("Novo", 0);

            divDetalhes.Visible = true;
            divPesquisa.Visible = false;
        }

        //protected void btnImportar_Click(object sender, EventArgs e)
        //{
        //    TabContainer.TabIndex = 1;
        //    TabContainer.ActiveTabIndex = 1;
        //}

        protected void grdGeracao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            txtReferenciaDetalhes.ReadOnly = true;
            txtReferenciaDetalhes.Enabled = false;
            ddlGrupoDetalhes.Enabled = false;

            IOrderedDictionary keys = grdGeracao.DataKeys[e.NewEditIndex].Values;
            int codigo = Int32.Parse(keys["COD_ARQ_ENV_REPASSE"].ToString());

            //hdfCodigoArquivo.Value = codigo.ToString();

            ValidaComando("Editar", codigo);

            divDetalhes.Visible = true;
            divPesquisa.Visible = false;

            grdDetalhes.EditIndex = -1;
            grdDetalhes.PageIndex = 0;
            grdDetalhes.DataBind();

            e.Cancel = true;
        }

        protected void grdGeracao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Visualizar")
            //{
            //    ValidaComando(e.CommandName, Convert.ToInt32(e.CommandArgument));
            //    divDetalhes.Visible = true;
            //    divPesquisa.Visible = false;
            //}
            //else 
            if (e.CommandName == "DeleteRepasse")
            {

                Resultado res = ExcluirRepasse(Convert.ToInt32(e.CommandArgument));

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Excluido com Sucesso");
                    grdGeracao.EditIndex = -1;
                    grdGeracao.PageIndex = 0;
                    grdGeracao.DataBind();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro durante a exclusão.\\nErro: " + res.Mensagem);
                }
            }
        }

        private Resultado ExcluirRepasse(int pCOD_ARQ_ENV_REPASSE)
        {
            ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
            PRE_TBL_ARQ_ENV_REPASSE objRepasse = new PRE_TBL_ARQ_ENV_REPASSE();
            PRE_TBL_ARQ_ENV_REPASSE_LINHA objLinha = new PRE_TBL_ARQ_ENV_REPASSE_LINHA();
            var user = (ConectaAD)Session["objUser"];

            objRepasse.COD_ARQ_ENV_REPASSE = pCOD_ARQ_ENV_REPASSE;
            objRepasse.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
            objRepasse.DTH_INCLUSAO = DateTime.Now;

            objLinha.COD_ARQ_ENV_REPASSE = pCOD_ARQ_ENV_REPASSE;
            objLinha.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
            objLinha.DTH_INCLUSAO = DateTime.Now;

            return bll.ExcluirRepasse(objRepasse, objLinha);
        }

        private Resultado ExcluirRepasseLinhas(PRE_TBL_ARQ_ENV_REPASSE_LINHA objLinha)
        {
            ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
            return bll.ExcluirLinha(objLinha);
        }

        #endregion

        #region .:Div Detalhes:.

        protected void btnSalvarDetalhes_Click(object sender, EventArgs e)
        {

            IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View obj = new IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View();
            ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
            var user = (ConectaAD)Session["objUser"];

            int CodArqEnvio = 0;
            int.TryParse(hdfCodArqEnvio.Value, out CodArqEnvio);

            obj.COD_ARQ_ENV_REPASSE = Convert.ToInt32(hdfCodigoRepasse.Value);
            if (CodArqEnvio > 0)
            {
                obj.COD_ARQ_ENVIO = CodArqEnvio;
            }
            else
            {
                obj.COD_ARQ_ENVIO = null;
            }
            obj.DCR_ARQ_ENV_REPASSE = txtReferenciaDetalhes.Text;
            obj.ANO_REF = Util.String2Short(txtAnoDetalhes.Text);
            obj.MES_REF = Util.String2Short(txtMesDetalhes.Text);            
            obj.COD_GRUPO_EMPRS = Util.String2Short(ddlGrupoDetalhes.SelectedValue);
            obj.COD_ARQ_AREA = Util.String2Short(ddlAreaDetalhes.SelectedValue);
            obj.DTH_INCLUSAO = Convert.ToDateTime(txtDataCriacaoDetalhes.Text);
            obj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";

            Resultado res = bll.SaveData(null, obj);

            if (res.Ok == true)
            {
                LimparCamposPanels();
                LimparCampos();

                grdDetalhes.EditIndex = -1;
                grdDetalhes.PageIndex = 0;
                grdDetalhes.DataBind();

                grdGeracao.DataBind();

                divDetalhes.Visible = false;
                divPesquisa.Visible = true;
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro! Entre em contato com o administrador \\n\\n Descrição: " + res.Mensagem);
            }
        }

        protected void btnCancelarDetalhes_Click(object sender, EventArgs e)
        {
            btnCancelarDetalhes.Text = "Cancelar";
            LimparCampos();
            LimparCamposPanels();

            grdDetalhes.EditIndex = -1;
            grdDetalhes.PageIndex = 0;
            grdDetalhes.DataBind();

            grdGeracao.DataBind();

            divDetalhes.Visible = false;
            divPesquisa.Visible = true;
        }

        protected void btnNovoDetalhes_Click(object sender, EventArgs e)
        {
            grdDetalhes.EditIndex = -1;
            grdDetalhes.PageIndex = 0;
            grdDetalhes.DataBind();

            LimparCamposPanels();
            txtNovoAcertoEmpresa.Focus();
            pnlNovoAcerto.Visible = true;
            pnlFiltrarAcerto.Visible = false;
        }

        protected void btnFiltrarDetalhes_Click(object sender, EventArgs e)
        {
            grdDetalhes.EditIndex = -1;
            grdDetalhes.PageIndex = 0;
            grdDetalhes.DataBind();

            LimparCamposPanels();
            txtFiltroAcertoEmpresa.Focus();
            pnlNovoAcerto.Visible = false;
            pnlFiltrarAcerto.Visible = true;
        }

        protected void grdDetalhes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateGrd")
            {

                bool validacao = ValidaGrupoEmpresa(Util.String2Short(ddlGrupoDetalhes.SelectedValue), Convert.ToInt16(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtEmpresaGrd")).Text));

                if (validacao == true)
                {
                    ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                    PRE_TBL_ARQ_ENV_REPASSE_LINHA obj = new PRE_TBL_ARQ_ENV_REPASSE_LINHA();
                    var user = (ConectaAD)Session["objUser"];

                    obj.COD_ARQ_ENV_REPASSE = Convert.ToInt32(hdfCodigoRepasse.Value);
                    obj.COD_ARQ_ENV_REP_LINHA = Convert.ToInt32(hdfLinhaRep.Value);
                    obj.COD_EMPRS = Convert.ToInt16(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtEmpresaGrd")).Text);
                    obj.NUM_RGTRO_EMPRG = Convert.ToInt32(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtMatriculaGrd")).Text);
                    obj.COD_VERBA = Convert.ToInt32(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtVerbaGrd")).Text);
                    obj.COD_VERBA_PATROCINA = ((Label)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("lblVerbaPatroc")).Text;
                    obj.VLR_PERCENTUAL = Convert.ToDecimal(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtPercentualGrd")).Text);
                    obj.VLR_DESCONTO = Convert.ToDecimal(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtValorGrd")).Text);
                    obj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                    obj.DTH_INCLUSAO = DateTime.Now;
                    obj.DTH_EXCLUSAO = null;
                    obj.LOG_EXCLUSAO = null;

                    Resultado res = bll.AtualizarLinha(obj);

                    if (res.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Atualizado com Sucesso");
                        grdDetalhes.EditIndex = -1;
                        //grdDetalhes.PageIndex = 0;
                        grdDetalhes.DataBind();
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro na tentativa de atualizar.\\nErro: " + res.Mensagem);
                    }
                }
                else
                {
                    MostraMensagem(lblGridDetalhes_Mensagem, "Empresa " + ((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtEmpresaGrd")).Text + " não pertence ao grupo selecionado!");
                    return;
                }
            }
            else if (e.CommandName == "DeleteGrd")
            {
                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                PRE_TBL_ARQ_ENV_REPASSE_LINHA obj = new PRE_TBL_ARQ_ENV_REPASSE_LINHA();
                var user = (ConectaAD)Session["objUser"];

                obj.COD_ARQ_ENV_REPASSE = Convert.ToInt32(hdfCodigoRepasse.Value);
                obj.COD_ARQ_ENV_REP_LINHA = Convert.ToInt32(e.CommandArgument);
                obj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                obj.DTH_INCLUSAO = DateTime.Now;

                Resultado res = bll.ExcluirLinha(obj);

                if (res.Ok)
                {
                    lblVerbaFiltro_Mensagem.Visible = false;
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Excluido com Sucesso");
                    grdDetalhes.EditIndex = -1;
                    grdDetalhes.PageIndex = 0;
                    grdDetalhes.DataBind();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro durante a exclusão.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdDetalhes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDetalhes.EditIndex = e.NewEditIndex;

            IOrderedDictionary keys = grdDetalhes.DataKeys[e.NewEditIndex].Values;
            hdfLinhaRep.Value = keys["COD_ARQ_ENV_REP_LINHA"].ToString();

            grdDetalhes.DataBind();
        }

        protected void grdDetalhes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblGridDetalhes_Mensagem.Visible = false;
        }

        protected void btnVerbaGrd_Click(object sender, ImageClickEventArgs e)
        {
            int? verba = Util.String2Int32(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtVerbaGrd")).Text);
            if (verba > 0)
            {
                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_LINHA_View obj = bll.GetTipoLancamento(verba);
                ((Label)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("lblTipoGrd")).Text = "";
                ((Label)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("lblDescricaoGrd")).Text = "";
                ((Label)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("lblVerbaPatroc")).Text = "";
                if (obj == null)
                {
                    MostraMensagem(lblGridDetalhes_Mensagem, "Verba não localizada");
                    return;
                }
                else
                {
                    obj.COD_EMPRS = Convert.ToInt16(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtEmpresaGrd")).Text);
                    obj.NUM_RGTRO_EMPRG = Convert.ToInt32(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtMatriculaGrd")).Text);
                    obj.COD_VERBA = Convert.ToInt32(((TextBox)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtVerbaGrd")).Text);                    
                    ArqPatrocinaDemonstrativoBLL Demons = new ArqPatrocinaDemonstrativoBLL();
                    PRE_TBL_ARQ_PAT_VERBA VerbaFuncesp = Demons.GetVerbaPatrocinadora(obj.COD_EMPRS, obj.COD_VERBA, null);
                    if (VerbaFuncesp != null)
                    {
                        obj.COD_VERBA_PATROCINA = VerbaFuncesp.COD_VERBA_PATROCINA ?? "";
                    }
                    obj.DCR_VERBA = bll.GetDescricaoVerba(verba);
                    ((Label)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("lblVerbaPatroc")).Text = obj.COD_VERBA_PATROCINA;
                    ((Label)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("lblTipoGrd")).Text = obj.TIPO == "C" ? "Crédito" : "Débito";
                    ((Label)grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("lblDescricaoGrd")).Text = obj.DCR_VERBA.ToString();
                    lblGridDetalhes_Mensagem.Visible = false;
                }
            }
            grdDetalhes.Rows[grdDetalhes.EditIndex].FindControl("txtPercentualGrd").Focus();
        }

        #region .: Novo Acerto:.

        protected void btnInserirNovoAcerto_Click(object sender, EventArgs e)
        {
            bool validacao = ValidaGrupoEmpresa((Util.String2Short(ddlGrupoDetalhes.SelectedValue)), (Util.String2Short(txtNovoAcertoEmpresa.Text)));

            if (validacao == true)
            {
                IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_LINHA_View obj = new IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_LINHA_View();
                IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View objPai = new IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View();
                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                var user = (ConectaAD)Session["objUser"];

                int CodArqEnvio = 0;
                int.TryParse(hdfCodArqEnvio.Value, out CodArqEnvio);

                objPai.COD_ARQ_ENV_REPASSE = Convert.ToInt32(hdfCodigoRepasse.Value);

                if (CodArqEnvio > 0)
                {
                    objPai.COD_ARQ_ENVIO = CodArqEnvio;
                }
                else
                {
                    objPai.COD_ARQ_ENVIO = null;
                }

                objPai.DCR_ARQ_ENV_REPASSE = txtReferenciaDetalhes.Text;
                objPai.ANO_REF = Util.String2Short(txtAnoDetalhes.Text);
                objPai.MES_REF = Util.String2Short(txtMesDetalhes.Text);
                objPai.COD_GRUPO_EMPRS = Util.String2Short(ddlGrupoDetalhes.SelectedValue);
                objPai.COD_ARQ_AREA = Util.String2Short(ddlAreaDetalhes.SelectedValue);                
                objPai.DTH_INCLUSAO = Convert.ToDateTime(txtDataCriacaoDetalhes.Text);
                objPai.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";

                obj.COD_ARQ_ENV_REPASSE = Convert.ToInt32(hdfCodigoRepasse.Value);
                obj.COD_EMPRS = Util.String2Short(txtNovoAcertoEmpresa.Text);
                obj.NUM_RGTRO_EMPRG = Util.String2Int32(txtNovoAcertoMatricula.Text);
                obj.COD_VERBA = Util.String2Int32(txtNovoAcertoVerba.Text);
                obj.COD_VERBA_PATROCINA = hNovaVerbaPatrocina.Value;
                obj.VLR_PERCENTUAL = Util.String2Decimal(txtNovoAcertoPercentual.Text);
                obj.VLR_DESCONTO = Util.String2Decimal(txtNovoAcertoValor.Text);
                obj.DTH_INCLUSAO = DateTime.Now;
                obj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                obj.DTH_EXCLUSAO = null;
                obj.LOG_EXCLUSAO = null;

                Resultado res = bll.SaveData(obj, objPai);
                if (res.Ok)
                {
                    //lblMensagemNovoAcerto.Visible = false;
                    hdfCodigoRepasse.Value = obj.COD_ARQ_ENV_REPASSE.ToString();
                    hdfCodArqEnvio.Value = objPai.COD_ARQ_ENVIO.ToString();
                    LimparCamposPanels();
                    grdDetalhes.DataBind();
                    grdDetalhes.Columns[7].Visible = true;
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Novo Registro Inserido com Sucesso");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro durante a inserção.\\nErro: " + res.Mensagem);
                }
            }
            else
            {
                MostraMensagem(lblMensagemNovoAcerto, "A empresa " + txtNovoAcertoEmpresa.Text + " não pertence ao grupo selecionado!");
                return;
            }
        }

        protected void btnCancelarNovoAcerto_Click(object sender, EventArgs e)
        {
            LimparCamposPanels();
            pnlNovoAcerto.Visible = false;
        }

        protected void btnNovoAcertoCarregaVerba_Click(object sender, ImageClickEventArgs e)
        {
            if (Util.String2Int32(txtNovoAcertoVerba.Text) > 0)
            {
                int? verba = Util.String2Int32(txtNovoAcertoVerba.Text);
                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_LINHA_View obj = bll.GetTipoLancamento(verba);
                txtNovoAcertoTipo.Text = "";
                txtNovoAcertoDcrVerba.Text = "";
                if (obj == null)
                {
                    MostraMensagem(lblMensagemNovoAcerto, "Verba não localizada");
                    return;
                }
                else
                {

                    ArqPatrocinaDemonstrativoBLL Demons = new ArqPatrocinaDemonstrativoBLL();
                    PRE_TBL_ARQ_PAT_VERBA VerbaFuncesp = Demons.GetVerbaPatrocinadora(short.Parse(txtNovoAcertoEmpresa.Text), int.Parse(txtNovoAcertoVerba.Text), null);
                    if (VerbaFuncesp != null)
                    {
                        obj.COD_VERBA_PATROCINA = VerbaFuncesp.COD_VERBA_PATROCINA ?? "";
                    }

                    obj.DCR_VERBA = bll.GetDescricaoVerba(verba);
                    hNovaVerbaPatrocina.Value = obj.COD_VERBA_PATROCINA;
                    txtNovoAcertoTipo.Text = obj.TIPO.ToString() == "C" ? "Crédito" : "Débito";
                    txtNovoAcertoDcrVerba.Text = obj.DCR_VERBA;
                    lblMensagemNovoAcerto.Visible = false;
                }
            }
            txtNovoAcertoPercentual.Focus();
        }
        #endregion

        #region .: Filtro Acerto:.

        protected void btnFiltrarNovoAcerto_Click(object sender, EventArgs e)
        {
            grdDetalhes.EditIndex = -1;
            grdDetalhes.PageIndex = 0;
            grdDetalhes.DataBind();
        }

        protected void btnLimparNovoAcerto_Click(object sender, EventArgs e)
        {
            LimparCamposPanels();
        }

        protected void btnCancelarFiltro_Click(object sender, EventArgs e)
        {
            LimparCamposPanels();
            pnlFiltrarAcerto.Visible = false;
        }

        protected void btnFiltroCarregaVerba_Click(object sender, ImageClickEventArgs e)
        {
            if (Util.String2Int32(txtFiltroAcertoVerba.Text) > 0)
            {
                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_LINHA_View obj = bll.GetTipoLancamento(Util.String2Int32(txtFiltroAcertoVerba.Text));
                ddlFiltroAcertoTipo.SelectedValue = "";
                if (obj == null)
                {
                    MostraMensagem(lblVerbaFiltro_Mensagem, "Verba não localizada");
                    return;
                }
                else
                {
                    ddlFiltroAcertoTipo.SelectedValue = obj.TIPO;
                    lblVerbaFiltro_Mensagem.Visible = false;
                }
            }
            btnFiltrarNovoAcerto.Focus();
        }
        #endregion

        #endregion

        #endregion

        #region .:Aba 2:.

        protected void ImportacaoAba2_Click(object sender, EventArgs e)
        {
            if (rdTipoImport.SelectedValue == "1")
            {
                string horainicio = DateTime.Now.ToShortTimeString();
                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View obj = new IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View();
                var user = (ConectaAD)Session["objUser"];

                obj.COD_ARQ_ENV_REPASSE = 0;
                obj.DCR_ARQ_ENV_REPASSE = txtReferenciaAba2.Text;
                obj.MES_REF = Convert.ToInt16(txtMesAb2.Text);
                obj.ANO_REF = Convert.ToInt16(txtAnoAb2.Text);
                obj.COD_ARQ_AREA = Convert.ToInt16(ddlAreaAb2.SelectedValue);
                obj.COD_GRUPO_EMPRS = Convert.ToInt16(ddlGrupoAb2.SelectedValue);
                obj.DTH_INCLUSAO = System.DateTime.Now;
                obj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";

                Resultado res = bll.Importar(obj);

                if (res.Ok == true)
                {
                    int totalInseridos = bll.GetCountInsert();

                    if (totalInseridos > 0)
                    {
                        LimparCampos();
                        lblMensagemImportacao.CssClass = "n_ok";
                        MostraMensagem(lblMensagemImportacao, "Dados Carregados com sucesso!");
                        StatusLabel.Text = "Upload Status: Início " + horainicio + " >>>> Fim    " + DateTime.Now.ToShortTimeString();
                        contador.Text = "Número de Registros importados: " + totalInseridos;                        
                        grdGeracao.EditIndex = -1;
                        grdGeracao.PageIndex = 0;
                        grdGeracao.DataBind();
                    }
                    else
                    {
                        bll.ExcluirRepasseVazio(obj);
                        lblMensagemImportacao.CssClass = "n_error";
                        MostraMensagem(lblMensagemImportacao, "Nenhum Dado encontrado!" );
                        StatusLabel.Text = "Upload Status: Início " + horainicio + " >>>> Fim    " + DateTime.Now.ToShortTimeString();
                        contador.Text = "Número de Registros importados: " + totalInseridos;
                    }
                }
                else
                {
                    //lblMensagemImportacao.Visible = false;
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro! Entre em contato com o administrador!");
                    StatusLabel.Text = "Upload Status: Erro!";
                    contador.Text = "Nenhum Registro importado: ";
                }
            }
            else if (rdTipoImport.SelectedValue == "2")
            {
                if (ValidaGrupoEmpresa(Convert.ToInt16(ddlGrupoAb2.SelectedValue), Convert.ToInt16(txtCodEmpresa.Text)))
                {
                    ImportarArquivo();
                }
                else
                {
                    MostraMensagem(lblMensagemImportacao, "Empresa " + txtCodEmpresa.Text + " não pertence ao grupo selecionado!");
                }

            }
        }

        private Resultado ImportarArquivo()
        {
            Resultado res = new Resultado();

            string path = "";
            try
            {
                string filename = Path.GetFileName(fuNovoProcessamento.FileName).ToString();
                string[] name = filename.Split('.');
                string UploadFilePath = Server.MapPath("UploadFile\\");

                path = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                if (!Directory.Exists(UploadFilePath))
                {
                    Directory.CreateDirectory(UploadFilePath);
                }

                fuNovoProcessamento.SaveAs(path);
                DataTable dt = ReadTextFile(path);

                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                var user = (ConectaAD)Session["objUser"];

                //string Referencia = "vb_Movfin_" + txtCodEmpresa.Text.PadLeft(3, '0') + "_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                string Referencia = "MovFin_" + txtMes.Text.PadLeft(2,'0') + "_" +  txtAno.Text.PadLeft(4,'0') + "_" + ddlGrupo.SelectedItem.Text;


                List<ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View> lsRepasses = null;
                lsRepasses = bll.Listar_Repasses(Convert.ToInt16(txtMes.Text), Convert.ToInt16(txtAno.Text), Convert.ToInt16(ddlGrupo.SelectedValue), null, null, Convert.ToInt16(ddlArea.SelectedValue));
                int pCOD_ARQ_ENV_REPASSE = 0;
                if (lsRepasses.Count>0)
                {
                    pCOD_ARQ_ENV_REPASSE = lsRepasses.Where(r => r.COD_ARQ_STATUS < 2).FirstOrDefault().COD_ARQ_ENV_REPASSE;
                }

                res = bll.Importar(dt,
                                    Convert.ToInt16(txtCodEmpresa.Text),
                                    Referencia,
                                    Convert.ToInt16(txtAno.Text),
                                    Convert.ToInt16(txtMes.Text),
                                    Convert.ToInt16(ddlGrupo.SelectedValue),
                                    Convert.ToInt16(ddlArea.SelectedValue),
                                    (user != null) ? user.login : "Desenv",
                                    pCOD_ARQ_ENV_REPASSE);
            }
            catch (Exception ex)
            {
                res.Erro("Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
            }
            finally
            {
                fuNovoProcessamento.FileContent.Dispose();
                fuNovoProcessamento.FileContent.Flush();
                fuNovoProcessamento.PostedFile.InputStream.Flush();
                fuNovoProcessamento.PostedFile.InputStream.Close();
            }
 
            return res;
        }

        protected void rdTipoImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdTipoImport.SelectedValue == "2")
                this.fuNovoProcessamento.Visible = true;
            else
                this.fuNovoProcessamento.Visible = false;
        }

        #endregion

        #region .:Metodos:.

        protected void LimparFiltro()
        {
            //Campos de pesquisa
            txtAnoFiltro.Text = "";
            txtMesFiltro.Text = "";
            ddlGrupoFiltro.SelectedValue = "";
            ddlStatusFiltro.Text = "";
            txtRefFiltro.Text = "";
            //ddlAreaFiltro.SelectedValue = "";
            ddlAreaFiltro.SelectedValue = ddlArea.SelectedValue;

            //HiddenField
            hdfCodigoRepasse.Value = "";
            hdfLinhaRep.Value = "";
            hdfCodArqEnvio.Value = "";

        }

        protected void LimparCampos()
        {
           
            //Campo dos detalhes
            txtMesDetalhes.Text = "";
            txtAnoDetalhes.Text = "";
            txtReferenciaDetalhes.Text = "";

            //HiddenField
            hdfCodigoRepasse.Value = "";
            hdfLinhaRep.Value = "";
            hdfCodArqEnvio.Value = "";

            //Painel do novo acerto
            txtNovoAcertoEmpresa.Text = "";
            txtNovoAcertoVerba.Text = "";
            hNovaVerbaPatrocina.Value = "";
            txtNovoAcertoMatricula.Text = "";
            txtNovoAcertoTipo.Text = "";
            txtNovoAcertoDcrVerba.Text = "";
            txtNovoAcertoValor.Text = "";
            txtNovoAcertoPercentual.Text = "";

            //Painel filtro
            txtFiltroAcertoEmpresa.Text = "";
            txtFiltroAcertoVerba.Text = "";
            txtFiltroAcertoMatricula.Text = "";
            ddlFiltroAcertoTipo.SelectedValue = "0";

            //Aba2
            txtReferenciaAba2.Text = "";
            txtMesAb2.Text = "";
            txtAnoAb2.Text = "";
            txtCodEmpresa.Text = "";

            //lblGridDetalhes_Mensagem.Visible = false;

            pnlNovoAcerto.Visible = false;
            pnlFiltrarAcerto.Visible = false;
        }

        protected void LimparCamposPanels()
        {
            txtNovoAcertoEmpresa.Text = "";
            txtNovoAcertoVerba.Text = "";
            hNovaVerbaPatrocina.Value = "";
            txtNovoAcertoMatricula.Text = "";
            txtNovoAcertoTipo.Text = "";
            txtNovoAcertoDcrVerba.Text = "";
            txtNovoAcertoValor.Text = "";
            txtNovoAcertoPercentual.Text = "";

            txtFiltroAcertoEmpresa.Text = "";
            txtFiltroAcertoVerba.Text = "";
            txtFiltroAcertoMatricula.Text = "";
            ddlFiltroAcertoTipo.SelectedValue = "0";

            //lblGridDetalhes_Mensagem.Visible = false;
        }
        protected void ValidaComando(string comando, int codigo)
        {
            switch (comando)
            {
                case "Editar":                    
                    btnCancelarDetalhes.Text = "Cancelar";
                    txtMesDetalhes.ReadOnly = true;
                    txtMesDetalhes.Enabled = false;
                    txtAnoDetalhes.ReadOnly = true;
                    txtAnoDetalhes.Enabled = false;
                    ddlAreaDetalhes.Enabled = true;
                    btnNovoDetalhes.Visible = true;
                    txtReferenciaDetalhes.ReadOnly = true;
                    txtReferenciaDetalhes.Enabled = false;
                    ddlGrupoDetalhes.Enabled = false;

                    grdDetalhes.Enabled = true;
                    btnSalvarDetalhes.Visible = true;
                    btnSalvarDetalhes.Enabled = true;
                    btnNovoDetalhes.Enabled = true;

                    CarregaDetalhes(codigo, comando);
                    //VerificaGrid();
                    break;

                case "Visualizar":
                    btnSalvarDetalhes.Visible = false;
                    btnCancelarDetalhes.Text = "Voltar";
                    txtMesDetalhes.ReadOnly = true;
                    txtMesDetalhes.Enabled = false;
                    txtAnoDetalhes.ReadOnly = true;
                    txtAnoDetalhes.Enabled = false;
                    ddlAreaDetalhes.Enabled = false;
                    ddlGrupoDetalhes.Enabled = false;
                    btnNovoDetalhes.Visible = false;
                    txtReferenciaDetalhes.ReadOnly = true;
                    txtReferenciaDetalhes.Enabled = false;
                    CarregaDetalhes(codigo, comando);
                    break;

                case "Novo":

                    btnCancelarDetalhes.Text = "Cancelar";
                    txtMesDetalhes.ReadOnly = false;
                    txtMesDetalhes.Enabled = true;
                    txtAnoDetalhes.ReadOnly = false;
                    txtAnoDetalhes.Enabled = true;
                    ddlAreaDetalhes.Enabled = true;
                    ddlGrupoDetalhes.Enabled = true;
                    btnNovoDetalhes.Visible = true;
                    txtReferenciaDetalhes.ReadOnly = false;
                    txtReferenciaDetalhes.Enabled = true;
                    txtDataCriacaoDetalhes.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                    grdDetalhes.Enabled = true;
                    btnSalvarDetalhes.Visible = true;
                    btnSalvarDetalhes.Enabled = true;
                    btnNovoDetalhes.Enabled = true;
                    break;
            }

        }
        protected void CarregaDetalhes(int codigo, string comando)
        {
            IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View obj = new IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View();
            ArqPatrocinadoraEnvioBLL EnvioBLL = new ArqPatrocinadoraEnvioBLL();

            ddlStatusDetalhes.DataSource = EnvioBLL.GetStatusDdl();
            ddlStatusDetalhes.DataValueField = "COD_ARQ_STATUS";
            ddlStatusDetalhes.DataTextField = "DCR_ARQ_STATUS";
            ddlStatusDetalhes.DataBind();

            ddlAreaDetalhes.DataSource = EnvioBLL.GetAreaDdl();
            ddlAreaDetalhes.DataValueField = "COD_ARQ_AREA";
            ddlAreaDetalhes.DataTextField = "DCR_ARQ_C_AREA_SUB";
            ddlAreaDetalhes.DataBind();

            ddlGrupoDetalhes.DataSource = EnvioBLL.GetGrupoDdl();
            ddlGrupoDetalhes.DataValueField = "COD_GRUPO_EMPRS";
            ddlGrupoDetalhes.DataTextField = "DCR_GRUPO_EMPRS";
            ddlGrupoDetalhes.DataBind();

            ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();

            obj = bll.GetLinha(codigo);

            hdfCodigoRepasse.Value = obj.COD_ARQ_ENV_REPASSE.ToString();
            txtMesDetalhes.Text = obj.MES_REF.ToString();
            txtAnoDetalhes.Text = obj.ANO_REF.ToString();
            ddlStatusDetalhes.SelectedValue = obj.COD_ARQ_STATUS.ToString();
            txtReferenciaDetalhes.Text = obj.DCR_ARQ_ENV_REPASSE.ToString();
            ddlAreaDetalhes.SelectedValue = obj.COD_ARQ_AREA.ToString();
            DateTime dataCriacaoDetalhes = Convert.ToDateTime(obj.DTH_INCLUSAO);
            txtDataCriacaoDetalhes.Text = dataCriacaoDetalhes.ToString("dd/MM/yyyy");
            hdfCodArqEnvio.Value = obj.COD_ARQ_ENVIO.ToString();
            ddlGrupoDetalhes.SelectedValue = obj.COD_GRUPO_EMPRS.ToString();

            grdDetalhes.Columns[7].Visible = true;
            grdDetalhes.DataBind();

            if (obj.READ_ONLY)
            {
                grdDetalhes.Enabled = false;
                btnSalvarDetalhes.Enabled = false;
                btnNovoDetalhes.Enabled = false;
                grdDetalhes.Columns[7].Visible = false;
            }


            if (comando == "Editar")
            {
                VerificaGrid();
            }
        }
        public void VerificaGrid()
        {
            if (grdDetalhes.Rows.Count != 0)
            {
                pnlNovoAcerto.Visible = false;
            }
            else
            {
                pnlNovoAcerto.Visible = true;
                txtNovoAcertoEmpresa.Focus();
            }
        }
        public bool ValidaGrupoEmpresa(short? grupo, short? empresa)
        {
            ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();

            bool validacao = bll.ValidaEmpresa(grupo, empresa);
            if (validacao == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            if (ValidaInicial())
            {
                btnGerar.Visible = true;
                btnReprocessar.Visible = false;
                btnLiberar2.Visible = false;
                lblNovoProcessamento.Visible = false;
                fuNovoProcessamento.Visible = false;
                lblCodEmpresa.Visible = false;
                txtCodEmpresa.Visible = false;
                if (ddlArea.SelectedValue == "6")
                {
                    lblNovoProcessamento.Visible = true;
                    fuNovoProcessamento.Visible = true;
                    lblCodEmpresa.Visible = true;
                    txtCodEmpresa.Visible = true;
                }
            }
        }

        private bool ValidaInicial()
        {
            if (ddlArea.SelectedValue == "0")
            {
                MostraMensagem(lblMensagemInicial, "Atenção! A Area deve ser selecionada para prosseguir!");
                ddlArea.Focus();
                return false;
            }
            lblArea2.Text = ddlArea.SelectedItem.Text;
            pnlNovoProcessamento.Visible = true;
            pnlInicial.Visible = false;
            pnlPesquisa.Enabled = false;
            LimparFiltro();
            btnFiltrar.Enabled = true;
            pnlFiltro.Visible = false;
            return true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlNovoProcessamento.Visible = false;
            pnlPesquisa.Enabled = true;
            if (!pnlPesquisa.Visible)
            {
                pnlInicial.Visible = true;
            }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {            
            short? sMes = Util.String2Short(txtMes.Text);
            short? sAno = Util.String2Short(txtAno.Text);
            short? sArea = Util.String2Short(ddlArea.SelectedValue);
            short? sEmpresa = Util.String2Short(txtCodEmpresa.Text);


            if (ValidarTelaProcessamento(sMes, sAno, sArea))
            {
                if (ValidarRegraNegocio(sMes, sAno, sArea, sEmpresa))
                {
                    Resultado res = ValidaEnvio();
                    if (res.Ok)
                    {
                        GerarProcessamento(sMes, sAno, sArea);                        
                    }
                    else
                    {
                        MostraMensagem(lblMensagemNovo, res.Mensagem);
                    }
                }
            }
        }

        private bool ValidarTelaProcessamento(short? sMes, short? sAno, short? sArea)
        {

            if (String.IsNullOrEmpty(txtMes.Text))
            {
                MostraMensagem(lblMensagemNovo, "Campo Mês é obrigatório");
                return false;
            }

            if (String.IsNullOrEmpty(txtAno.Text))
            {
                MostraMensagem(lblMensagemNovo, "Campo Ano é obrigatório");
                return false;
            }

            if (sArea == null || sArea == 0)
            {
                MostraMensagem(lblMensagemNovo, "Uma area deve ser selecionada para prosseguir");
                return false;
            }

            if (fuNovoProcessamento.Visible)
            {
                if (String.IsNullOrEmpty(ddlGrupo.SelectedValue))
                {
                    MostraMensagem(lblMensagemNovo, "Campo Grupo é obrigatório");
                    return false;
                }

                if (String.IsNullOrEmpty(txtCodEmpresa.Text))
                {
                    MostraMensagem(lblMensagemNovo, "Campo Empresa é obrigatório");
                    return false;
                }
                else
                {
                    if (!ValidaGrupoEmpresa(Convert.ToInt16(ddlGrupo.SelectedValue), Convert.ToInt16(txtCodEmpresa.Text)))
                    {
                        MostraMensagem(lblMensagemNovo, "Empresa " + txtCodEmpresa.Text + " não pertence ao grupo selecionado!");
                        return false;
                    }
                }

                if (!fuNovoProcessamento.HasFile)
                {
                    MostraMensagem(lblMensagemNovo, "Entre com o arquivo para prosseguir");
                    return false;
                }
                else if (!fuNovoProcessamento.PostedFile.ContentType.Equals("text/plain") && !fuNovoProcessamento.PostedFile.ContentType.Equals("application/vnd.ms-excel"))
                {
                    MostraMensagem(lblMensagemNovo, "Atenção! Formato do arquivo invalido. Carregue apenas arquivos texto simples (.txt) ou .CSV!");
                    return false;
                }
            }

            return true;

        }

        private bool ValidarRegraNegocio(short? sMes, short? sAno, short? sArea, short? sEmpresa)
        {
            ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();

            Int16 iGrupo = 0;
            List<ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View> lsRepasses = null;
            if (Int16.TryParse(ddlGrupo.SelectedValue, out iGrupo))
            {
                lsRepasses = bll.Listar_Repasses(sMes, sAno, iGrupo, null, null, sArea);
            }
            else
            {
                lsRepasses = bll.Listar_Repasses(sMes, sAno, null, null, null, sArea);
            }

            //Adicionado texto do Cod_Empresa para buscar a empresa certa na geração de arquivo
            //List<ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View> lsRepasses = bll.Listar_Repasses(sMes, sAno, Convert.ToInt16(txtCodEmpresa.Text), null, null, sArea);

            string lista_gerados = "";
            string lista_liberados = "";
            foreach (ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View rep in lsRepasses)
            {
                string lista_empresa_gerada = "";
                bool b_empresa_gerada = false;

                if (sArea == 6)
                {
                    List<ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> linhas = bll.GetWhereDetalhes(rep.COD_ARQ_ENV_REPASSE, sEmpresa, null, null, "0").ToList();
                    if (linhas.Count() > 0)
                    {
                        lista_empresa_gerada = " (empresa: " + linhas.FirstOrDefault().COD_EMPRS.ToString() + ")";
                        b_empresa_gerada = true;
                    }                    
                }

                switch (rep.COD_ARQ_STATUS)
                {
                    case 1:
                    default:
                        if (lista_gerados.IndexOf(rep.COD_GRUPO_EMPRS.ToString() + ",") == -1 &&
                            lista_gerados.IndexOf(rep.COD_GRUPO_EMPRS.ToString() + " (") == -1 &&
                            (sArea != 6 || (sArea == 6 && b_empresa_gerada)))
                        {
                            lista_gerados += rep.COD_GRUPO_EMPRS.ToString() + lista_empresa_gerada + "," + Environment.NewLine;
                        }
                        break;
                    case 2:
                    case 3:
                        if (lista_liberados.IndexOf(rep.COD_GRUPO_EMPRS.ToString() + ",") == -1 &&
                            lista_liberados.IndexOf(rep.COD_GRUPO_EMPRS.ToString() + " (") == -1 &&
                            (sArea != 6 || (sArea == 6 && b_empresa_gerada)))
                        {
                            lista_liberados += rep.COD_GRUPO_EMPRS.ToString() + lista_empresa_gerada + "," + Environment.NewLine;
                        }
                        break;
                }
            }

            if (!String.IsNullOrEmpty(lista_liberados))
            {
                MostraMensagem(lblMensagemNovo, "Atenção! Já existe(m) arquivo(s) de desconto ENVIADO(s) para as seguintes patrocinadoras: " + Environment.NewLine + Environment.NewLine + lista_liberados.Substring(0, lista_liberados.Length - 1) + "<br><b>Impossível gerar novamente</b>", "n_error");
                return false;
            }

            if (!String.IsNullOrEmpty(lista_gerados))
            {
                MostraMensagem(lblMensagemNovo, "Atenção! Já existe(m) arquivo(s) de desconto gerados(s) para as seguintes patrocinadoras: " + Environment.NewLine + Environment.NewLine + lista_gerados.Substring(0, lista_gerados.Length - 1) + "<br>Para gerar novamente estes arquivos clique em <a href='#' onclick='btnReprocessar_click();'>'Reprocessar'</a>");
                btnGerar.Visible = false;
                btnReprocessar.Visible = true;
                return false;
            }

            return true;
        }

        private void GerarProcessamento(short? sMes, short? sAno, short? sArea)
        {
            Resultado res = new Resultado();
            string strMensagem = "";
            int count = 0;
            long total_registros = 0;
            DateTime dDTH_INCLUSAO = DateTime.Now;

            ConectaAD user = (ConectaAD)Session["objUser"];

            if (ddlArea.SelectedValue == "6")
            {
                res = ImportarArquivo();
                if (res.Ok)
                {
                    total_registros = res.CodigoCriado;
                }
                else
                {
                    strMensagem = res.Mensagem;
                }
            }
            else
            {
                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                if (String.IsNullOrEmpty(ddlGrupo.SelectedValue))
                {
                    foreach (ListItem item in ddlGrupo.Items)
                    {
                        if (Util.TryParseShort(item.Value) != null)
                        {
                            res = bll.GerarArquivo(sMes, sAno, Util.String2Short(item.Value), item.Text.ToLower(), sArea, dDTH_INCLUSAO, (user != null) ? user.login : "Desenv");
                            if (res.Ok)
                            {
                                total_registros += res.CodigoCriado;
                            }
                            else
                            {
                                strMensagem += res.Mensagem + "\\n";
                                if (res.Mensagem.ToUpper().IndexOf("JÁ EXISTE") > -1)
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    res = bll.GerarArquivo(sMes, sAno, Util.String2Short(ddlGrupo.SelectedValue), ddlGrupo.SelectedItem.Text.ToLower(), sArea, dDTH_INCLUSAO, (user != null) ? user.login : "Desenv");
                    if (!res.Ok)
                    {
                        strMensagem = res.Mensagem + "\\n";
                    }
                    else
                    {
                        strMensagem = "";
                        total_registros = res.CodigoCriado;
                    }
                }

                if (ddlGrupo.Items.Count - 1 == count)
                {
                    strMensagem = "Todos os arquivos de descontos da area " + ddlArea.SelectedItem.Text + " já foram gerados anteriormente.";
                }
            }

            if (String.IsNullOrEmpty(strMensagem))
            {
                MostraMensagem(lblMensagemInicial, "Arquivos de desconto gerados com sucesso! (" + total_registros + " registros)", "n_ok");
                pnlNovoProcessamento.Visible = false;
                pnlInicial.Visible = true;
                pnlPesquisa.Enabled = true;
                pnlPesquisa.Visible = false;
            }
            else
            {
                MostraMensagem(lblMensagemNovo, strMensagem);
            }
        }

        protected void btnReprocessar_Click(object sender, EventArgs e)
        {
            short? sMes = Util.String2Short(txtMes.Text);
            short? sAno = Util.String2Short(txtAno.Text);
            short? sArea = Util.String2Short(ddlArea.SelectedValue);
            short? sEmpresa = Util.String2Short(txtCodEmpresa.Text);

            ConectaAD user = (ConectaAD)Session["objUser"];

            if (ValidarTelaProcessamento(sMes, sAno, sArea))
            {
                Int16 iGrupo = 0;
                ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
                List<ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View> lsRepasses = null;
                if (Int16.TryParse(ddlGrupo.SelectedValue, out iGrupo))
                {
                    lsRepasses = bll.Listar_Repasses(sMes, sAno, iGrupo, null, null, sArea);
                } 
                else 
                {
                    lsRepasses = bll.Listar_Repasses(sMes, sAno, null, null, null, sArea);
                }


                foreach (ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View rep in lsRepasses)
                {

                    if (sArea == 6)
                    {
                        List<ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_LINHA_View> linhas = bll.GetWhereDetalhes(rep.COD_ARQ_ENV_REPASSE, sEmpresa, null, null, "0").ToList();
                        if (linhas.Count() > 0)
                        {
                            foreach (ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_LINHA_View rep_linha in linhas)
                            {
                                PRE_TBL_ARQ_ENV_REPASSE_LINHA deleteLinha = new PRE_TBL_ARQ_ENV_REPASSE_LINHA();
                                deleteLinha.COD_ARQ_ENV_REPASSE = rep_linha.COD_ARQ_ENV_REPASSE;
                                deleteLinha.COD_ARQ_ENV_REP_LINHA = int.Parse(rep_linha.COD_ARQ_ENV_REP_LINHA.ToString());
                                deleteLinha.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                                deleteLinha.DTH_INCLUSAO = DateTime.Now;
                                ExcluirRepasseLinhas(deleteLinha);
                            }
                        }
                    }
                    else
                    {
                        ExcluirRepasse(rep.COD_ARQ_ENV_REPASSE);
                    }                    
                }

                GerarProcessamento(sMes, sAno, sArea);
            }

            btnGerar.Visible = true;
            btnReprocessar.Visible = false;
        }

        protected void btnLiberar2_Click(object sender, EventArgs e)
        {
            Resultado res = new Resultado();
            short? sMes = Util.String2Short(txtMes.Text);
            short? sAno = Util.String2Short(txtAno.Text);
            short? sArea = Util.String2Short(ddlArea.SelectedValue);

            if (String.IsNullOrEmpty(txtMes.Text))
            {
                MostraMensagem(lblMensagemNovo, "Campo Mês é obrigatório");
                return;
            }

            if (String.IsNullOrEmpty(txtAno.Text))
            {
                MostraMensagem(lblMensagemNovo, "Campo Ano é obrigatório");
                return;
            }

            if (sArea == null || sArea == 0)
            {
                MostraMensagem(lblMensagemNovo, "Uma area deve ser selecionada para prosseguir");
                return;
            }

            ConectaAD user = (ConectaAD)Session["objUser"];

            ArqPatrocinadoraRepasseBLL bll = new ArqPatrocinadoraRepasseBLL();
            string strMensagem = "";
            DateTime dDTH_INCLUSAO = DateTime.Now;

            if (String.IsNullOrEmpty(ddlGrupo.SelectedValue))
            {
                foreach (ListItem item in ddlGrupo.Items)
                {
                    if (Util.TryParseShort(item.Value) != null)
                    {
                        //string ret = bll.PacoteJaLiberado(sMes, sAno, sArea, Util.String2Short(item.Value));
                        res = ValidaEnvio();
                        if (res.Ok)
                        {
                            res = bll.LiberarArquivo(sMes, sAno, Util.String2Short(item.Value), sArea, dDTH_INCLUSAO, (user != null) ? user.login : "Desenv");
                            if (!res.Ok)
                            {
                                strMensagem += res.Mensagem + "\\n";
                            }
                        }
                        else
                        {
                            if (strMensagem.IndexOf(res.Mensagem) == -1)
                            {
                                strMensagem += res.Mensagem + "\\n";
                            }
                        }
                    }
                }
            }
            else
            {
                //string ret = bll.PacoteJaLiberado(sMes, sAno, sArea, Util.String2Short(ddlGrupo.SelectedValue));
                res = ValidaEnvio();
                if (res.Ok)
                {
                    res = bll.LiberarArquivo(sMes, sAno, Util.String2Short(ddlGrupo.SelectedValue), sArea, dDTH_INCLUSAO, (user != null) ? user.login : "Desenv");
                    if (!res.Ok)
                    {
                        strMensagem = res.Mensagem + "\\n";
                    }
                }
                else
                {
                    strMensagem = res.Mensagem + "\\n";
                }

            }

            if (String.IsNullOrEmpty(strMensagem))
            {
                MostraMensagem(lblMensagemInicial, "Arquivos de desconto liberado com sucesso!", "n_ok");
                pnlNovoProcessamento.Visible = false;
                pnlInicial.Visible = true;
                pnlPesquisa.Enabled = true;
                pnlPesquisa.Visible = false;
            }
            else
            {
                MostraMensagem(lblMensagemNovo, strMensagem);
            }
        }

        private Resultado ValidaEnvio()
        {
            Resultado result = new Resultado(true);
            ArqPatrocinadoraEnvioBLL BLL = new ArqPatrocinadoraEnvioBLL();
            ArqPatrocinadoraEnvioDAL.PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA ARQ_ENVIO_CONTROLE = BLL.PacoteJaLiberado(Util.String2Short(this.txtMes.Text), Util.String2Short(this.txtAno.Text), null, Util.String2Short(this.ddlGrupo.SelectedValue));

            if (ARQ_ENVIO_CONTROLE.QTD_PUBLICADOS > 0)
            {
                //base.MostraMensagem(lblMensagemInicial, "Atenção! O pacote mensal já foi liberado para a Patrocinadora<br><b>Impossível LIBERAR novamente</b>", "n_warning");
                result.Erro("Atenção! O pacote mensal já foi liberado para a Patrocinadora " + this.ddlGrupo.SelectedItem.Text + "<br><b>Impossível liberar um novo arquivo</b>");
            }
            //else if (ARQ_ENVIO_CONTROLE.QTD_ENVIADOS > 0)
            //{
            //    //base.MostraMensagem(lblMensagemInicial, "Atenção! O pacote mensal já foi liberado para a Patrocinadora<br><b>Impossível LIBERAR novamente</b>", "n_warning");
            //    result.Erro("Atenção! Arquivo já liberado para o Cadastro<br><b>Impossível liberar um novo arquivo</b>");
            //} 
            //else if (ARQ_ENVIO_CONTROLE.QTD_ENVIADOS == 0)
            //{
            //    //base.MostraMensagem(lblMensagemInicial, "Não existem arquivos disponíveis para envio para as Patrocinadoras", "n_warning");
            //    result.Erro("Não existem arquivos disponíveis para envio para as Patrocinadoras");
            //}

            return result;
        }

        protected void btnExibirAnteriores_Click(object sender, EventArgs e)
        {
            if (ddlArea.SelectedValue == "0")
            {
                MostraMensagem(lblMensagemInicial, "Atenção! A Area deve ser selecionada para prosseguir!");
                ddlArea.Focus();
            }
            else
            {
                ddlAreaFiltro.SelectedValue = ddlArea.SelectedValue;
                grdGeracao.PageIndex = 0;
                grdGeracao.DataBind();
                pnlPesquisa.Visible = true;
                pnlInicial.Visible = false;
            }
            //if (ddlAreaFiltro.Items.Count > 1)
            //{
            //    ddlAreaFiltro.Visible = true;
            //}
            //if (ddlArea.SelectedValue != "0")
            //{
            //    ddlAreaFiltro.SelectedValue = ddlArea.SelectedValue;
            //}
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            pnlFiltro.Visible = true;
            btnFiltrar.Enabled = false;
        }

        protected void btnLiberar_Click(object sender, EventArgs e)
        {
            if (ValidaInicial())
            {
                btnGerar.Visible = false;                
                btnLiberar2.Visible = true;
                btnReprocessar.Visible = false;
                lblNovoProcessamento.Visible = false;
                fuNovoProcessamento.Visible = false;
                lblCodEmpresa.Visible = false;
                txtCodEmpresa.Visible = false;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            pnlPesquisa.Visible = false;
            pnlInicial.Visible = true;
        }
    }
}