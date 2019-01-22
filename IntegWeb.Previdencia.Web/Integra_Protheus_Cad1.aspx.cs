﻿using System;
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
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL.Int_Protheus;
using IntegWeb.Previdencia.Aplicacao.DAL.Int_Protheus;

namespace IntegWeb.Previdencia.Web
{
    public partial class Integra_Protheus_Cad1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnPesquisar.UniqueID;
            
            lblPesquisa_Mensagem.Visible = false;
            lblNovo_Mensagem.Visible = false;

            //ControleTela(ddlTabela.SelectedValue);

            if (!IsPostBack)
            {
                string telaACarregar = Request.QueryString["telaACarregar"];

                if (!string.IsNullOrEmpty(telaACarregar))
                {
                    ddlTabela.SelectedValue = telaACarregar;
                    ControleTela(ddlTabela.SelectedValue);
                    ddlTabela.Style.Add("display", "none");
                    lblDescricaoTela.Text = ddlTabela.SelectedItem.Text;
                }
                else {
                    MostraMensagemTelaUpdatePanel(upUpdatepanel, "Nenhum Parametro Encontrado !");
                }           
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtPesqCodigo.Text) && 
                String.IsNullOrEmpty(txtPesqDescricao.Text) && 
                (String.IsNullOrEmpty(txtPesqDescricao2.Text) && txtPesqDescricao2.Visible || txtPesqDescricao2.Visible==false))
            {
                MostraMensagem(lblPesquisa_Mensagem, "Entre com pelo menos um campo para pesquisar");
                return;
            }

            grdCCusto.EditIndex = -1;
            grdCCusto.PageIndex = 0;
            CarregarTela();

        }

        protected void grdCCusto_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdDepositoJudicial.EditIndex = e.NewEditIndex;
            IOrderedDictionary keys = grdCCusto.DataKeys[e.NewEditIndex].Values;
            //short PK_CODIGO = Int16.Parse(keys["CODIGO"].ToString());
            CarregarRegistro(int.Parse(keys["CODIGO"].ToString()));
            txtCodigo.Enabled = false;
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;
            e.Cancel = true;
        }

        protected void grdCCusto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int PK_CODIGO = 0;
            int.TryParse(e.Keys["CODIGO"].ToString(), out PK_CODIGO);
            IntegraProtheusBLL obj = new IntegraProtheusBLL();
            IntegraProtheusDAL.VIEW_DadosProtheus_Cad1 loadObj = new IntegraProtheusDAL.VIEW_DadosProtheus_Cad1();
            //CarregaDrop();
            loadObj.CODIGO = PK_CODIGO;
            loadObj.ENTITY_OBJ = ddlTabela.SelectedValue;
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteData(loadObj);
            MostraMensagem(lblPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            CarregarTela();
            e.Cancel = true;
        }

        //protected void grdDepPgto_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    //grdDepositoJudicial.EditIndex = e.NewEditIndex;
        //    IOrderedDictionary keys = grdDepPgto.DataKeys[e.NewEditIndex].Values;
        //    int PK_COD_DEPOSITO_JUDIC_PGTO = Int32.Parse(keys["COD_DEPOSITO_JUDIC_PGTO"].ToString());
        //    CarregarPgto(PK_COD_DEPOSITO_JUDIC_PGTO);
        //    pnlPgtoLista.Visible = false;
        //    pnlPgtoDetalhe.Visible = true;
        //    e.Cancel = true;
        //    ScriptManager.RegisterStartupScript(upUpdatepanel,
        //           upUpdatepanel.GetType(),
        //           "script",
        //           "{ CalculoBenef(); CalculoCustos(); }",
        //            true);
        //}

        //protected void grdDepPgto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    int PK_COD_DEPOSITO_JUDIC_PGTO = Int32.Parse(e.Keys["COD_DEPOSITO_JUDIC_PGTO"].ToString());
        //    DepositoJudicialBLL obj = new DepositoJudicialBLL();
        //    var user = (ConectaAD)Session["objUser"];
        //    Resultado res = obj.DeleteDataPgto(PK_COD_DEPOSITO_JUDIC_PGTO, (user != null) ? user.login : "Desenv");
        //    MostraMensagem(pnlDetalhe_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
        //    pnlPgtoDetalhe.Visible = false;
        //    pnlPgtoLista.Visible = true;
        //    grdDepPgto.DataBind();
        //    e.Cancel = true;
        //}

        private void ControleTela(string pTabela)
        {
            bool bVisible = false;
            switch (pTabela)
            {
                case "ASSOCIACAO_VERBA":
                    bVisible = true;
                    break;
            }

            ldlDescricao2.Visible = bVisible;
            txtDescricao2.Visible = bVisible;
            lblPesqDescricao2.Visible = bVisible;
            txtPesqDescricao2.Visible = bVisible;
            int cellColumnIndex = GetColumnIndex(grdCCusto, "Descrição2");
            grdCCusto.Columns[cellColumnIndex].Visible = bVisible;
        }

        private void CarregarTela()
        {            
            grdCCusto.DataBind();
        }

        private void CarregarRegistro(int codigo)
        {
            IntegraProtheusBLL obj = new IntegraProtheusBLL();
            IntegraProtheusDAL.VIEW_DadosProtheus_Cad1 loadObj = new IntegraProtheusDAL.VIEW_DadosProtheus_Cad1();
            //CarregaDrop();
            ControleTela(ddlTabela.SelectedValue);
            loadObj = obj.GetDataCad1(ddlTabela.SelectedValue, codigo);            
            txtCodigo.Text = loadObj.CODIGO.ToString();
            txtDescricao.Text = loadObj.DESCRICAO.Trim();
            txtDescricao2.Text = (loadObj.DESCRICAO2 ?? "").Trim();
            //txtDtAdmissao.Text = Util.Date2String(loadObj.DAT_ADMISSAO);
            //txtDtDemissao.Text = Util.Date2String(loadObj.DAT_DEMISSAO);
            //txtDtNascimento.Text = Util.Date2String(loadObj.DAT_NASCTO);
            //txtDtAdesao.Text = Util.Date2String(loadObj.DAT_ADESAO);
            ////loadObj.DIB = null;
            ////loadObj.DIP = null;
            ////loadObj.NUM_PLBNF         
            //txtPlano.Text = loadObj.PLANO;
            ////loadObj.COD_SITPAR = null;
            ////loadObj.COD_TPPCP  = null;        
            //txtPerfil.Text = loadObj.PERFIL;
            //txtNumProcesso.Text = loadObj.NRO_PROCESSO;
            //txtVara.Text = loadObj.COD_VARA_PROC;
            //ddlPoloAcaoJudicial.SelectedValue = loadObj.POLO_ACJUD;
            //ddlAssunto.SelectedValue = loadObj.COD_TIPLTO.ToString();
            //txtPasta.Text = loadObj.NRO_PASTA;
            //btnNovoPgto.Enabled = true;
            //grdDepPgto.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqCodigo.Text = "";
            txtPesqDescricao.Text = "";
            txtPesqDescricao2.Text = "";
            grdCCusto.EditIndex = -1;
            grdCCusto.PageIndex = 0;
            CarregarTela();
            //btnNovoPgto.Enabled = false;
            //pnlPgtoDetalhe.Visible = true;
            //pnlPgtoLista.Visible = false;
        }

        //private void CarregaDrop()
        //{
        //    ValorReferenciaBLL obj = new ValorReferenciaBLL();
        //    CarregaDropDowDT(obj.CarregaAssunto(), ddlAssunto);
        //}

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparControles(pnlNovo.Controls);
            txtCodigo.Enabled = true;
            txtDescricao.Enabled = true;
            //chkAdm.Enabled = true;
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            IntegraProtheusBLL obj = new IntegraProtheusBLL();
            IntegraProtheusDAL.VIEW_DadosProtheus_Cad1 newobj = new IntegraProtheusDAL.VIEW_DadosProtheus_Cad1();

            int iCodigo = 0;
            int.TryParse(txtCodigo.Text, out iCodigo);

            //CarregaDrop();
            //loadObj = obj.GetCCusto(pEmpresa, pNumOrgao, pDspAdm);
            newobj.CODIGO = iCodigo;
            newobj.DESCRICAO = txtDescricao.Text;
            newobj.DESCRICAO2 = txtDescricao2.Text;
            newobj.ENTITY_OBJ = ddlTabela.SelectedValue;
            //newobj.DSP_ADM = chkAdm.Checked ? "S" : "N";
            //newobj.CCUSTO = txtCCusto.Text;
            //newobj.DSC_CCUSTO = txtDescCCusto.Text;

            Resultado res = obj.Validar(newobj, txtCodigo.Enabled);

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
                    MostraMensagem(lblNovo_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
                }
            }
            else
            {
                MostraMensagem(lblNovo_Mensagem, res.Mensagem);
            }

            //DepositoJudicialBLL obj = new DepositoJudicialBLL();
            //PRE_TBL_DEPOSITO_JUDIC newobj = new PRE_TBL_DEPOSITO_JUDIC();
            //PRE_TBL_DEPOSITO_JUDIC_PGTO newPgto = null;

            //var user = (ConectaAD)Session["objUser"];

            //newobj.COD_DEPOSITO_JUDIC = int.Parse(hfCOD_DEPOSITO_JUDIC.Value);
            //newobj.DTH_INCLUSAO = DateTime.Now;
            //newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
            //newobj.DAT_CADASTRO = Util.String2Date(txtDtCadastro.Text);
            //newobj.NUM_MATR_PARTF = int.Parse(hfNUM_MATR_PARTF.Value);
            //newobj.COD_EMPRS = Util.String2Short(txtEmpresa.Text);
            //newobj.NUM_RGTRO_EMPRG = Util.String2Int32(txtNumOrgao.Text);
            //newobj.CPF_EMPRG = Util.String2Int64(txtCPF.Text);
            //newobj.NOM_EMPRG = txtNome.Text;
            //newobj.DAT_ADMISSAO = Util.String2Date(txtDtAdmissao.Text);
            //newobj.DAT_DEMISSAO = Util.String2Date(txtDtDemissao.Text);
            //newobj.DAT_NASCTO = Util.String2Date(txtDtNascimento.Text);
            //newobj.DAT_ADESAO = Util.String2Date(txtDtAdesao.Text);
            ////newobj.DIB = null;
            ////newobj.DIP = null;
            ////newobj.NUM_PLBNF         
            //newobj.PLANO = txtPlano.Text;
            ////newobj.COD_SITPAR = null;
            ////newobj.COD_TPPCP  = null;        
            //newobj.PERFIL = txtPerfil.Text;
            //newobj.NRO_PROCESSO = txtNumProcesso.Text;
            //newobj.COD_VARA_PROC = txtVara.Text;
            //newobj.POLO_ACJUD = ddlPoloAcaoJudicial.SelectedValue;
            //newobj.COD_TIPLTO = short.Parse(ddlAssunto.SelectedValue);
            //newobj.NRO_PASTA = txtPasta.Text;

            //if (pnlPgtoDetalhe.Visible)
            //{
            //    newPgto = new PRE_TBL_DEPOSITO_JUDIC_PGTO();
            //    newPgto.COD_DEPOSITO_JUDIC_PGTO = int.Parse(hfCOD_DEPOSITO_JUDIC_PGTO.Value);
            //    newPgto.COD_DEPOSITO_JUDIC = int.Parse(hfCOD_DEPOSITO_JUDIC.Value);
            //    newPgto.DTH_INCLUSAO = newobj.DTH_INCLUSAO;
            //    newPgto.LOG_INCLUSAO = newobj.LOG_INCLUSAO;
            //    newPgto.TIP_CADASTRO = ddlTipoCadastro.SelectedValue;
            //    newPgto.NUM_PP = txtNumPP.Text;
            //    newPgto.TIP_SOLICITACAO = Util.String2Short(ddlTipoSolicitacao.SelectedValue);
            //    newPgto.DTH_PAGAMENTO = Util.String2Date(txtDtPagamento.Text);
            //    newPgto.TIP_PAGAMENTO = Util.String2Short(ddlFormaPagamento.SelectedValue);
            //    newPgto.NOM_CREDOR = txtNomCredor.Text;
            //    newPgto.VLR_BSPS = Util.String2Decimal(txtVlrBSPS.Text);
            //    newPgto.VLR_BD = Util.String2Decimal(txtVlrBD.Text);
            //    newPgto.VLR_CV = Util.String2Decimal(txtVlrCV.Text);
            //    newPgto.VLR_BSPS_CUSTO = Util.String2Decimal(txtVlrBSPS_custo.Text);
            //    newPgto.VLR_BD_CUSTO = Util.String2Decimal(txtVlrBD_custo.Text);
            //    newPgto.VLR_CV_CUSTO = Util.String2Decimal(txtVlrCV_custo.Text);
            //    newPgto.DSC_DESCRICAO = txtDescricao.Text;
            //    newPgto.NUM_CARTA = Util.String2Int32(txtNumCarta.Text);
            //    newPgto.DAT_CARTA_ENVIO = Util.String2Date(txtDtEnvioCarta.Text);
            //    newPgto.DAT_EMAIL_ENVIO = Util.String2Date(txtDtEnvioEMail.Text);

            //    //newobj.PRE_TBL_DEPOSITO_JUDIC_PGTO.Add(newPgto);
            //}

            //Resultado res = obj.Validar(newobj, newPgto);

            //if (res.Ok)
            //{
            //    res = obj.SaveData(newobj, newPgto);
            //    if (res.Ok)
            //    {
            //        hfCOD_DEPOSITO_JUDIC.Value = newobj.COD_DEPOSITO_JUDIC.ToString();
            //        btnNovoPgto.Enabled = true;
            //        MostraMensagem((pnlPgtoDetalhe.Visible) ? pnlDetalhe_Mensagem : pnlPesquisa_Mensagem, res.Mensagem, "n_ok");
            //        Voltar();
            //    }
            //    else
            //    {
            //        MostraMensagem(pnlDetalhe_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            //    }
            //}
            //else
            //{
            //    MostraMensagem(pnlDetalhe_Mensagem, res.Mensagem);
            //}
        }

        private void Voltar()
        {
            //if ((pnlPgtoDetalhe.Visible) && (hfCOD_DEPOSITO_JUDIC.Value != "0"))
            //{
            //    grdDepPgto.DataBind();
            //    btnNovoPgto.Enabled = true;
            //    pnlPgtoDetalhe.Visible = false;
            //    pnlPgtoLista.Visible = true;
            //    //btnSalvar.Enabled = false;
            //    //btnEditar.Enabled = true;
            //    //pnlDetalhe_1.Enabled = false;
            //}
            //else
            //{
                pnlNovo.Visible = false;
                pnlLista.Visible = true;
                pnlPesquisa.Visible = true;
                CarregarTela();
            //}
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Voltar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Voltar();
        }

        //private void CarregarDadosPartic(int cod_emprs, int num_rgtro_emprg)
        //{
        //    ValorReferenciaBLL obj = new ValorReferenciaBLL();
        //    obj.cod_emprs = cod_emprs;
        //    //obj.num_matr_partf = num_matr_partf;
        //    obj.num_rgtro_emprg = num_rgtro_emprg;
        //    DataTable dt = obj.CarregarDadosParticipante();

        //    LimparIdentParticip();

        //    if (dt.Rows.Count > 0)
        //    {
        //        hfNUM_MATR_PARTF.Value = dt.Rows[0]["NUM_MATR_PARTF"].ToString();
        //        txtCPF.Text = dt.Rows[0]["CPF_EMPRG"].ToString();
        //        txtPlano.Text = dt.Rows[0]["PLANO"].ToString();
        //        txtNome.Text = dt.Rows[0]["NOME_EMPRG"].ToString();

        //        if (!dt.Rows[0]["DATA_ADESAO"].ToString().Equals(""))
        //            txtDtAdesao.Text = ((DateTime)dt.Rows[0]["DATA_ADESAO"]).ToString("dd/MM/yyyy");

        //        if (!dt.Rows[0]["DATA_ADMISSAO"].ToString().Equals(""))
        //            txtDtAdmissao.Text = ((DateTime)dt.Rows[0]["DATA_ADMISSAO"]).ToString("dd/MM/yyyy");

        //        if (!dt.Rows[0]["DATA_DEMISSAO"].ToString().Equals(""))
        //            txtDtDemissao.Text = ((DateTime)dt.Rows[0]["DATA_DEMISSAO"]).ToString("dd/MM/yyyy");

        //        if (!dt.Rows[0]["DATA_NASCTO"].ToString().Equals(""))
        //            txtDtNascimento.Text = ((DateTime)dt.Rows[0]["DATA_NASCTO"]).ToString("dd/MM/yyyy");

        //        txtPerfil.Text = dt.Rows[0]["PERFIL"].ToString();

        //    }
        //    else
        //    {
        //        MostraMensagem(pnlDetalhe_Mensagem, "Participante não localizado!");
        //    }
        //}

        //private void LimparIdentParticip()
        //{
        //    txtCPF.Text = "";
        //    txtPlano.Text = "";
        //    txtNome.Text = "";
        //    txtDtAdesao.Text = "";
        //    txtDtAdmissao.Text = "";
        //    txtDtDemissao.Text = "";
        //    txtDtNascimento.Text = "";
        //    txtPerfil.Text = "";
        //}

    }
}