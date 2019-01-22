using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;

namespace IntegWeb.Previdencia.Web
{
    public partial class CadValorReferencia : BasePage
    {
        #region .: Propriedades :.

        ValorReferenciaBLL obj = new ValorReferenciaBLL();

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            msgAlerta.Visible = false;

            if (!IsPostBack)
            {
                CarregaTelaPesquisa();
                grdValorReferencia.DataBind();
                pnlGridVr.Visible = true;
                //grdValorReferencia.Sort("num_proc", SortDirection.Ascending);    
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Resultado res = new Resultado();

                obj.cod_emprs = Util.String2Int32(txtEmpresa.Text);
                obj.cpf_emprg = Util.String2Int64(txtCPF.Text);
                obj.nome_emprg = txtNome.Text;
                obj.num_rgtro_emprg = Util.String2Int32(txtMatricula.Text);
                obj.data_admissao = Util.String2Date(txtDtAdmissao.Text);
                obj.data_demissao = Util.String2Date(txtDtDemissao.Text);
                obj.data_nascto = Util.String2Date(txtDtNascimento.Text);
                obj.plano = txtPlano.Text;
                //obj.plano = txtDtCadastro.Text;
                obj.data_adesao = Util.String2Date(txtDtAdesao.Text);
                obj.perfil = txtPerfil.Text;
                obj.dib = Util.String2Date(txtDIB.Text);

                obj.cod_origem_dados = ddlOrigem.SelectedValue;
                obj.cod_situacao = ddlSituacao.SelectedValue;
                obj.num_pasta = txtPasta.Text;
                obj.num_proc = txtNumProcesso.Text;
                obj.polo_acjud = ddlPoloAcaoJudicial.SelectedValue;

                if (int.Parse(ddlTipoAtualizacao.SelectedValue) > 0)
                    obj.cod_tip_atlz = int.Parse(ddlTipoAtualizacao.SelectedValue);

                obj.dta_retr_atlz = Util.String2Date(txtBaseAtualizacao.Text ?? null);
                obj.cod_vara_proc = txtVara.Text;

                if (int.Parse(ddlAssunto.SelectedValue) > 0)
                    obj.cod_tiplto = int.Parse(ddlAssunto.SelectedValue);

                obj.dta_status = Util.String2Date(txtEfetivacao.Text);

                if (int.Parse(ddlHistorico.SelectedValue) > 0)
                    obj.cod_hstplto = int.Parse(ddlHistorico.SelectedValue);

                obj.dat_prescr = Util.String2Date(txtDIP.Text);

                obj.bsps_dib_splto = Util.String2Decimal(bsps_dib_sem_pleito.Text);
                obj.bd_dib_splto = Util.String2Decimal(bd_dib_sem_pleito.Text);
                obj.cv_dib_splto = Util.String2Decimal(cv_dib_sem_pleito.Text);

                obj.bsps_atu_splto = Util.String2Decimal(bsps_atu_sem_pleito.Text);
                obj.bd_atu_splto = Util.String2Decimal(bd_atu_sem_pleito.Text);
                obj.cv_atu_splto = Util.String2Decimal(cv_atu_sem_pleito.Text);

                obj.bsps_dib_cplto = Util.String2Decimal(bsps_dib_com_pleito.Text);
                obj.bd_dib_cplto = Util.String2Decimal(bd_dib_com_pleito.Text);
                obj.cv_dib_cplto = Util.String2Decimal(cv_dib_com_pleito.Text);

                obj.bsps_atu_cplto = Util.String2Decimal(bsps_atu_com_pleito.Text);
                obj.bd_atu_cplto = Util.String2Decimal(bd_atu_com_pleito.Text);
                obj.cv_atu_cplto = Util.String2Decimal(cv_atu_com_pleito.Text);

                obj.cntr_part_at_bsps = Util.String2Decimal(cntr_part_at_bsps.Text);
                obj.bnf_part_ret_bsps = Util.String2Decimal(bnf_part_ret_bsps.Text);
                obj.cntr_part_ret_bsps = Util.String2Decimal(cntr_part_ret_bsps.Text);
                obj.resmat_part_bsps = Util.String2Decimal(resmat_part_bsps.Text);
                obj.resmat_ant_part_bsps = Util.String2Decimal(resmat_ant_part_bsps.Text);
                obj.cntr_part_at_bd = Util.String2Decimal(cntr_part_at_bd.Text);
                obj.bnf_part_ret_bd = Util.String2Decimal(bnf_part_ret_bd.Text);
                obj.cntr_part_ret_bd = Util.String2Decimal(cntr_part_ret_bd.Text);
                obj.resmat_part_bd = Util.String2Decimal(resmat_part_bd.Text);
                obj.cntr_part_at_cv = Util.String2Decimal(cntr_part_at_cv.Text);
                obj.bnf_part_ret_cv = Util.String2Decimal(bnf_part_ret_cv.Text);

                obj.cntr_patr_at_bsps = Util.String2Decimal(cntr_patr_at_bsps.Text);
                obj.bnf_patr_ret_bsps = Util.String2Decimal(bnf_patr_ret_bsps.Text);
                obj.resmat_patr_bsps = Util.String2Decimal(resmat_patr_bsps.Text);
                obj.resmat_ant_patr_bsps = Util.String2Decimal(resmat_ant_patr_bsps.Text);
                obj.cntr_patr_at_bd = Util.String2Decimal(cntr_patr_at_bd.Text);
                obj.bnf_patr_ret_bd = Util.String2Decimal(bnf_patr_ret_bd.Text);
                obj.resmat_patr_bd = Util.String2Decimal(resmat_patr_bd.Text);
                obj.cntr_patr_at_cv = Util.String2Decimal(cntr_patr_at_cv.Text);
                obj.bnf_patr_ret_cv = Util.String2Decimal(bnf_patr_ret_cv.Text);

                obj.prc_part_resmat_bsps = Util.String2Decimal(prc_part_resmat_bsps.Text);
                obj.prc_part_resmat_bd = Util.String2Decimal(prc_part_resmat_bd.Text);
                obj.prc_patr_resmat_bsps = Util.String2Decimal(prc_patr_resmat_bsps.Text);
                obj.prc_patr_resmat_bd = Util.String2Decimal(prc_patr_resmat_bd.Text);

                obj.nota = nota.Text;
                obj.obs = obs.Text;

                if (!String.IsNullOrEmpty(hdIdVR.Value))
                {
                    string[] id = hdIdVR.Value.ToString().Split(',');
                    obj.num_matr_partf = int.Parse(id[0]);
                    obj.num_sqncl_prc = int.Parse(id[1]);
                    obj.num_proc = id[2];
                    //obj.cod_tip_atlz = Util.String2Int32(id[3]);
                    Button btn = (Button)sender;
                    if (btn.CommandName == "Duplicar")
                        res = obj.DuplicarVr();
                    else
                        res = obj.AtualizarProcessoVr();
                }
                else
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.responsavel = user.login;
                    //obj.num_matr_partf = Util.String2Int32(txtMatricula.Text);
                    obj.num_matr_partf = Util.String2Int32(hfNUM_MATR_PARTF.Value);   
                    obj.num_sqncl_prc = Util.String2Int32(txtNumSeqProcesso.Text);
                    res = obj.InserirProcessoVr();
                }

                if (res.Ok)
                {
                    //if (id > 0)
                    //{
                    //    hdIdVR.Value = id.ToString();
                    //}
                    //hdIdVR.Value = txtMatricula.Text + "," + txtNumSeqProcesso.Text + "," + txtNumProcesso.Text + "," + ddlTipoAtualizacao.SelectedValue;
                    hdIdVR.Value = hfNUM_MATR_PARTF.Value.ToString() + "," + txtNumSeqProcesso.Text + "," + txtNumProcesso.Text + "," + ddlTipoAtualizacao.SelectedValue;
                    btnDuplicar.Enabled = true;
                    btnImprimir.Enabled = true;
                    btnSalvar.Enabled = false;
                    btnEditar.Enabled = true;
                    pnlDetalhes.Enabled = false;
                    grdValorReferencia.DataBind();
                    MostraMensagemTelaUpdatePanel(upValorReferencia, res.Mensagem);
                }
                else
                    MostraMensagemTelaUpdatePanel(upValorReferencia, "Atenção !\\n\\n" + res.Mensagem);
            }

            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upValorReferencia, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }


        }

        protected void grdValorReferencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] id = e.CommandArgument.ToString().Split(',');
            switch (e.CommandName)
            {
                case "Alterar":
                    hdIdVR.Value = e.CommandArgument.ToString();
                    obj.num_matr_partf = int.Parse(id[0]);
                    obj.num_sqncl_prc = int.Parse(id[1]);
                    obj.num_proc = id[2];
                    obj.cod_tip_atlz = Util.String2Int32(id[3]);
                    btnEditar.Visible = true;
                    CarregarVr();
                    break;
                case "Deletar":
                    obj.num_matr_partf = int.Parse(id[0]);
                    obj.num_sqncl_prc = int.Parse(id[1]);
                    obj.num_proc = id[2];
                    obj.cod_tip_atlz = Util.String2Int32(id[3]);
                    try
                    {
                        Resultado msg = new Resultado();
                        obj.DeletarVr(out msg);
                        if (msg.Ok)
                        {
                            obj.id_acao_processo = null;
                            grdValorReferencia.DataBind();
                            CarregaDrop();
                            MostraMensagemTelaUpdatePanel(upValorReferencia, msg.Mensagem);
                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(upValorReferencia, "Atenção !\\n\\n" + msg.Mensagem);
                        }
                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upValorReferencia, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void grdImportar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] Argument = e.CommandArgument.ToString().Split(',');
            switch (e.CommandName)
            {
                case "Importar":
                    Resultado res = new Resultado();
                    obj.num_matr_partf = int.Parse(Argument[1]);
                    obj.num_sqncl_prc = int.Parse(Argument[2]);
                    int index = int.Parse(Argument[0]) - (grdImportar.PageIndex * grdImportar.PageSize);
                    bool ATLZ_IGPDI = ((CheckBox)grdImportar.Rows[index].FindControl("chkIGPDI")).Checked;
                    bool ATLZ_TRAB = ((CheckBox)grdImportar.Rows[index].FindControl("chkTrab")).Checked;
                    bool ATLZ_CIV = ((CheckBox)grdImportar.Rows[index].FindControl("chkCivil")).Checked;
                    if (ATLZ_IGPDI || ATLZ_TRAB || ATLZ_CIV)
                    {
                        obj.ImportacaoVr(out res, ATLZ_IGPDI, ATLZ_TRAB, ATLZ_CIV);
                        if (res.Ok)
                        {
                            this.Form.DefaultButton = this.btnPesquisar.UniqueID;
                            grdValorReferencia.DataBind();
                            CarregaDrop();
                            pnlSelectVr.Visible = true;
                            pnlGridVr.Visible = true;
                            pnlGridImportarVr.Visible = false;
                            SubTituloTela.Visible = false;
                            MostraMensagemTelaUpdatePanel(upValorReferencia, res.Mensagem);
                        }
                        else
                            MostraMensagemTelaUpdatePanel(upValorReferencia, "Atenção !\\n\\n" + res.Mensagem);
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upValorReferencia, "Atenção !\\n\\nObrigatório selecionar pelo manos uma opção de atualização (IGP-DI, Trabalhista ou Cívil)");
                    }
                    break;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(grdValorReferencia.SortExpression))
            //    grdValorReferencia.Sort("num_proc", SortDirection.Ascending);    
            //else
            //    grdValorReferencia.DataBind();

            pnlGridImportarVr.Visible = false;
            SubTituloTela.Visible = false;

            string msg = "";

            if (ValidaCamposPesquisa(txtCodEmpresa, txtCodMatricula, txtPesquisa, out msg))
            {
                MostraMensagemTelaUpdatePanel(upValorReferencia, msg.ToString());
            }
            else
            {
                grdValorReferencia.DataBind();
                //if (grdValorReferencia.Rows.Count > 0)   
                pnlGridVr.Visible = true;
            }
        }

        protected void btnPesquisarImportar_Click(object sender, EventArgs e)
        {
            pnlGridVr.Visible = false;

            string msg = "";
            if (ValidaCamposPesquisa(txtCodEmpresaImportar, txtCodMatriculaImportar, txtPesquisaImportar, out msg))
            {
                MostraMensagemTelaUpdatePanel(upValorReferencia, msg.ToString());
            }
            else
            {
                grdImportar.DataBind();
                //if (grdValorReferencia.Rows.Count > 0)   
                pnlGridImportarVr.Visible = true;
                SubTituloTela.Visible = true;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesquisa.Text = "";
            txtCodEmpresa.Text = "";
            txtCodMatricula.Text = "";
            CarregaTela();
            grdValorReferencia.DataBind();
            pnlGridVr.Visible = true;
            pnlGridImportarVr.Visible = false;
            SubTituloTela.Visible = false;
        }

        protected void btnLimparImportar_Click(object sender, EventArgs e)
        {
            txtPesquisaImportar.Text = "";
            txtCodEmpresaImportar.Text = "";
            txtCodMatriculaImportar.Text = "";
            //CarregaTela();
            //grdValorReferencia.DataBind();
            grdImportar.PageIndex = 0;
            pnlGridVr.Visible = false;
            pnlGridImportarVr.Visible = true;
            SubTituloTela.Visible = true;
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparTodosCampos();
            hdIdVR.Value = "";
            hfNUM_MATR_PARTF.Value = "0";
            ddlOrigem.SelectedValue = "M";
            ddlSituacao.SelectedValue = "VR";
            txtEmpresa.Enabled = true;
            txtMatricula.Enabled = true;
            txtPasta.Enabled = true;
            txtEmpresa.Focus();
            txtNumSeqProcesso.Text = "1";
            txtNumProcesso.Enabled = true;
            ddlTipoAtualizacao.Enabled = true;
            txtDtCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            pnlDetalhesVr.Visible = true;
            pnlDetalhes.Enabled = true;
            pnlGridVr.Visible = false;
            pnlSelectVr.Visible = false;
            btnDuplicar.Enabled = false;
            btnImprimir.Enabled = false;
            btnEditar.Visible = false;
            btnSalvar.Enabled = true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            CarregaTela();
            grdValorReferencia.DataBind();
        }

        protected void btnVoltarImportar_Click(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnPesquisar.UniqueID;
            pnlSelectVr.Visible = true;
            pnlGridVr.Visible = true;
            pnlGridImportarVr.Visible = false;
            SubTituloTela.Visible = false;
        }

        protected void btnImportarVr_Click(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnPesquisarImportar.UniqueID;
            pnlSelectVr.Visible = false;
            pnlDetalhesVr.Visible = false;
            pnlGridVr.Visible = false;
            grdImportar.PageIndex = 0;
            grdImportar.DataBind();
            pnlGridImportarVr.Visible = true;
            SubTituloTela.Visible = true;
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            RelatorioBLL relBLL = new RelatorioBLL();
            Relatorio rel = new Relatorio();
            rel = relBLL.Listar("Rel_Lista_Consolidado_Vr.rpt");
            string[] id = hdIdVR.Value.ToString().Split(',');
            rel.get_parametro("v_num_matr_partf").valor = id[0];
            rel.get_parametro("v_num_sqncl_prc").valor = id[1];
            rel.get_parametro("v_cod_tip_atlz").valor = id[3];
            Session[rel.relatorio] = rel;
            //AbrirNovaAba(this.Page, "Report.aspx?Param=" + item.relatorio + (alerta ? "&Alert=true" : ""), item.relatorio);
            AbrirNovaAba(this.Page, "RelatorioWeb.aspx?Relatorio_nome=" + rel.relatorio + "&PromptParam=false&Popup=true", rel.relatorio);
            grdValorReferencia.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            pnlDetalhes.Enabled = true;
        }

        protected void txtMatricula_TextChanged(object sender, EventArgs e)
        {
           
            StringBuilder msg = new StringBuilder();
            if (String.IsNullOrEmpty(txtEmpresa.Text))
            {
                msg.Append("Empresa obrigatório");
                MostraMensagemTelaUpdatePanel(upValorReferencia, msg.ToString());
                return;
            }
            if (String.IsNullOrEmpty(txtMatricula.Text))
            {
                msg.Append("Matrícula obrigatório");
                MostraMensagemTelaUpdatePanel(upValorReferencia, msg.ToString());
                return;
            }  
            else
            {
                CarregarDadosPartic(int.Parse(txtEmpresa.Text), int.Parse(txtMatricula.Text));
            }
        }

        #endregion

        #region .: Métodos :.

        private void CarregarDadosPartic(int cod_emprs, int num_rgtro_emprg)
        {
            obj.cod_emprs = cod_emprs;
            //obj.num_matr_partf = num_rgtro_emprg;
            obj.num_rgtro_emprg = num_rgtro_emprg;
            DataTable dt = obj.CarregarDadosParticipante();

            LimparIdentParticip();

            if (dt.Rows.Count > 0)
            {
                hfNUM_MATR_PARTF.Value = dt.Rows[0]["NUM_MATR_PARTF"].ToString();
                txtNumSeqProcesso.Text = dt.Rows[0]["NUM_SQNCL_PRC"].ToString();
                txtCPF.Text = dt.Rows[0]["CPF_EMPRG"].ToString();
                txtPlano.Text = dt.Rows[0]["PLANO"].ToString();
                txtNome.Text = dt.Rows[0]["NOME_EMPRG"].ToString();

                if (!dt.Rows[0]["DATA_ADESAO"].ToString().Equals(""))
                    txtDtAdesao.Text = ((DateTime)dt.Rows[0]["DATA_ADESAO"]).ToString("dd/MM/yyyy");

                if (!dt.Rows[0]["DATA_ADMISSAO"].ToString().Equals(""))
                    txtDtAdmissao.Text = ((DateTime)dt.Rows[0]["DATA_ADMISSAO"]).ToString("dd/MM/yyyy");

                if (!dt.Rows[0]["DATA_DEMISSAO"].ToString().Equals(""))
                    txtDtDemissao.Text = ((DateTime)dt.Rows[0]["DATA_DEMISSAO"]).ToString("dd/MM/yyyy");

                if (!dt.Rows[0]["DATA_NASCTO"].ToString().Equals(""))
                    txtDtNascimento.Text = ((DateTime)dt.Rows[0]["DATA_NASCTO"]).ToString("dd/MM/yyyy");

                if (!dt.Rows[0]["DATA_DIB"].ToString().Equals(""))
                    txtDIB.Text = ((DateTime)dt.Rows[0]["DATA_DIB"]).ToString("dd/MM/yyyy");

                txtPerfil.Text = dt.Rows[0]["PERFIL"].ToString();

            }
            else
            {
                msgAlerta.Visible = true;
            }
        }

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {
            divExibir.Visible = true;
            divOcultar.Visible = false;
        }

        private void CarregaTela()
        {
            pnlDetalhesVr.Visible = false;
            pnlGridVr.Visible = true;
            pnlSelectVr.Visible = true;
            CarregaDrop();
        }

        private void CarregaTelaPesquisa()
        {
            pnlDetalhesVr.Visible = false;
            pnlGridVr.Visible = false;
            pnlSelectVr.Visible = true;
            CarregaDrop();
        }

        private void CarregaDrop()
        {
            CarregaDropDowDT(obj.CarregaTipoAtualizacao(), ddlTipoAtualizacao);
            CarregaDropDowDT(obj.CarregaAssunto(), ddlAssunto);
            CarregaDropDowDT(obj.CarregaHistorico(), ddlHistorico);
        }

        private void CarregarVr()
        {
            LimparTodosCampos();
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            pnlDetalhes.Enabled = false;
            txtEmpresa.Enabled = false;
            txtMatricula.Enabled = false;
            DataTable dt = obj.CarregaProcessoVr();
            if (dt.Rows.Count > 0)
            {
                // Identificação do participante:
                txtEmpresa.Text = dt.Rows[0]["cod_emprs"].ToString();
                txtMatricula.Text = dt.Rows[0]["num_rgtro_emprg"].ToString();
                hfNUM_MATR_PARTF.Value = dt.Rows[0]["num_matr_partf"].ToString();
                txtNumSeqProcesso.Text = dt.Rows[0]["num_sqncl_prc"].ToString();
                txtCPF.Text = dt.Rows[0]["cpf_emprg"].ToString();
                txtNome.Text = dt.Rows[0]["nome_emprg"].ToString();
                if (!dt.Rows[0]["data_admissao"].ToString().Equals(""))
                    txtDtAdmissao.Text = ((DateTime)dt.Rows[0]["data_admissao"]).ToString("dd/MM/yyyy");
                if (!dt.Rows[0]["data_demissao"].ToString().Equals(""))
                    txtDtDemissao.Text = ((DateTime)dt.Rows[0]["data_demissao"]).ToString("dd/MM/yyyy");
                if (!dt.Rows[0]["data_nascto"].ToString().Equals(""))
                    txtDtNascimento.Text = ((DateTime)dt.Rows[0]["data_nascto"]).ToString("dd/MM/yyyy");
                txtPlano.Text = dt.Rows[0]["plano"].ToString();
                if (!dt.Rows[0]["hdrdathor"].ToString().Equals(""))
                    txtDtCadastro.Text = ((DateTime)dt.Rows[0]["hdrdathor"]).ToString("dd/MM/yyyy");
                if (!dt.Rows[0]["COD_ORIGEM_DADOS"].ToString().Equals(""))
                    ddlOrigem.SelectedValue = dt.Rows[0]["COD_ORIGEM_DADOS"].ToString();
                if (!dt.Rows[0]["COD_SITUACAO"].ToString().Equals(""))
                    ddlSituacao.SelectedValue = dt.Rows[0]["COD_SITUACAO"].ToString();
                if (!dt.Rows[0]["data_adesao"].ToString().Equals(""))
                    txtDtAdesao.Text = ((DateTime)dt.Rows[0]["data_adesao"]).ToString("dd/MM/yyyy");
                txtPerfil.Text = dt.Rows[0]["perfil"].ToString();
                if (!dt.Rows[0]["dib"].ToString().Equals(""))
                    txtDIB.Text = ((DateTime)dt.Rows[0]["dib"]).ToString("dd/MM/yyyy");

                //txtPasta.Text = dt.Rows[0]["pasta"].ToString();
                txtPasta.Text = dt.Rows[0]["num_pasta"].ToString();
                // txtNumProcesso.Enabled = String.IsNullOrEmpty(dt.Rows[0]["num_proc"].ToString());
                txtNumProcesso.Text = dt.Rows[0]["num_proc"].ToString();

                ddlPoloAcaoJudicial.SelectedValue = dt.Rows[0]["polo_acjud"].ToString();
                ddlTipoAtualizacao.Enabled = true;
                if (!dt.Rows[0]["cod_tip_atlz"].ToString().Equals(""))
                {
                    ddlTipoAtualizacao.Enabled = false;
                    ddlTipoAtualizacao.SelectedValue = dt.Rows[0]["cod_tip_atlz"].ToString();
                }

                if (!dt.Rows[0]["dta_retr_atlz"].ToString().Equals(""))
                    txtBaseAtualizacao.Text = ((DateTime)dt.Rows[0]["dta_retr_atlz"]).ToString("dd/MM/yyyy");
                txtVara.Text = dt.Rows[0]["cod_vara_proc"].ToString();
                if (!dt.Rows[0]["cod_tiplto"].ToString().Equals(""))
                    ddlAssunto.SelectedValue = dt.Rows[0]["cod_tiplto"].ToString();

                if (!dt.Rows[0]["dta_status"].ToString().Equals(""))
                    txtEfetivacao.Text = ((DateTime)dt.Rows[0]["dta_status"]).ToString("dd/MM/yyyy");

                if (ddlSituacao.SelectedValue != "VR")
                {
                    txtEfetivacao.Visible = false;
                    lblEfetivacao.Visible = false;
                    //RegularExpressionValidator2.Visible = txtEfetivacao.Visible;
                }

                if (!dt.Rows[0]["cod_hstplto"].ToString().Equals(""))
                    ddlHistorico.SelectedValue = dt.Rows[0]["cod_hstplto"].ToString();
                if (!dt.Rows[0]["dat_prescr"].ToString().Equals(""))
                    txtDIP.Text = ((DateTime)dt.Rows[0]["dat_prescr"]).ToString("dd/MM/yyyy");

                bsps_dib_sem_pleito.Text = dt.Rows[0]["bsps_dib_splto"].ToString();
                bd_dib_sem_pleito.Text = dt.Rows[0]["bd_dib_splto"].ToString();
                cv_dib_sem_pleito.Text = dt.Rows[0]["cv_dib_splto"].ToString();

                bsps_atu_sem_pleito.Text = dt.Rows[0]["bsps_atu_splto"].ToString();
                bd_atu_sem_pleito.Text = dt.Rows[0]["bd_atu_splto"].ToString();
                cv_atu_sem_pleito.Text = dt.Rows[0]["cv_atu_splto"].ToString();

                bsps_dib_com_pleito.Text = dt.Rows[0]["bsps_dib_cplto"].ToString();
                bd_dib_com_pleito.Text = dt.Rows[0]["bd_dib_cplto"].ToString();
                cv_dib_com_pleito.Text = dt.Rows[0]["cv_dib_cplto"].ToString();

                bsps_atu_com_pleito.Text = dt.Rows[0]["bsps_atu_cplto"].ToString();
                bd_atu_com_pleito.Text = dt.Rows[0]["bd_atu_cplto"].ToString();
                cv_atu_com_pleito.Text = dt.Rows[0]["cv_atu_cplto"].ToString();

                cntr_part_at_bsps.Text = dt.Rows[0]["cntr_part_at_bsps"].ToString();
                bnf_part_ret_bsps.Text = dt.Rows[0]["bnf_part_ret_bsps"].ToString();
                cntr_part_ret_bsps.Text = dt.Rows[0]["cntr_part_ret_bsps"].ToString();
                resmat_part_bsps.Text = dt.Rows[0]["resmat_part_bsps"].ToString();
                resmat_ant_part_bsps.Text = dt.Rows[0]["resmat_ant_part_bsps"].ToString();
                cntr_part_at_bd.Text = dt.Rows[0]["cntr_part_at_bd"].ToString();
                bnf_part_ret_bd.Text = dt.Rows[0]["bnf_part_ret_bd"].ToString();
                cntr_part_ret_bd.Text = dt.Rows[0]["cntr_part_ret_bd"].ToString();
                resmat_part_bd.Text = dt.Rows[0]["resmat_part_bd"].ToString();
                cntr_part_at_cv.Text = dt.Rows[0]["cntr_part_at_cv"].ToString();
                bnf_part_ret_cv.Text = dt.Rows[0]["bnf_part_ret_cv"].ToString();

                txtTotalParticipante.Text = dt.Rows[0]["total_part"].ToString();

                cntr_patr_at_bsps.Text = dt.Rows[0]["cntr_patr_at_bsps"].ToString();
                bnf_patr_ret_bsps.Text = dt.Rows[0]["bnf_patr_ret_bsps"].ToString();
                resmat_patr_bsps.Text = dt.Rows[0]["resmat_patr_bsps"].ToString();
                resmat_ant_patr_bsps.Text = dt.Rows[0]["resmat_ant_patr_bsps"].ToString();
                cntr_patr_at_bd.Text = dt.Rows[0]["cntr_patr_at_bd"].ToString();
                bnf_patr_ret_bd.Text = dt.Rows[0]["bnf_patr_ret_bd"].ToString();
                resmat_patr_bd.Text = dt.Rows[0]["resmat_patr_bd"].ToString();
                cntr_patr_at_cv.Text = dt.Rows[0]["cntr_patr_at_cv"].ToString();
                bnf_patr_ret_cv.Text = dt.Rows[0]["bnf_patr_ret_cv"].ToString();

                txtTotalPatrocinador.Text = dt.Rows[0]["total_patr"].ToString();

                prc_part_resmat_bsps.Text = dt.Rows[0]["prc_part_resmat_bsps"].ToString();
                prc_part_resmat_bd.Text = dt.Rows[0]["prc_part_resmat_bd"].ToString();
                prc_patr_resmat_bsps.Text = dt.Rows[0]["prc_patr_resmat_bsps"].ToString();
                prc_patr_resmat_bd.Text = dt.Rows[0]["prc_patr_resmat_bd"].ToString();

                nota.Text = dt.Rows[0]["nota"].ToString();
                obs.Text = dt.Rows[0]["obs"].ToString();

                pnlDetalhesVr.Visible = true;
                pnlGridVr.Visible = false;
                pnlSelectVr.Visible = false;

                btnDuplicar.Enabled = true;
                btnImprimir.Enabled = true;

                //RegraTela(int.Parse(dt.Rows[0]["id_status"].ToString()));

            }

            //DivAcao(divActionVR, divSelectVR);
        }

        private void LimparTodosCampos()
        {
            LimparControles(upValorReferencia.Controls[0].Controls[7].Controls[3].Controls);
        }

        private void LimparIdentParticip()
        {
            //hdIdVR.Value = "0";
            //txtEmpresa.Text = "";
            //txtMatricula.Text = "";
            txtNumSeqProcesso.Text = "1";
            txtCPF.Text = "";
            txtPlano.Text = "";
            //txtDtCadastro.Text = "";
            txtNome.Text = "";
            txtDtAdesao.Text = "";
            txtDtAdmissao.Text = "";
            txtDtDemissao.Text = "";
            txtDtNascimento.Text = "";
            txtPerfil.Text = "";
            txtDIB.Text = "";
        }

        private bool ValidaCamposPesquisa(TextBox Empresa, TextBox Matricula, TextBox Pesquisa, out string mensagem)
        {

            StringBuilder msg = new StringBuilder();
            bool isErro = false;
            if (String.IsNullOrEmpty(Empresa.Text) && String.IsNullOrEmpty(Matricula.Text) && String.IsNullOrEmpty(Pesquisa.Text))
            {
                msg.Append("Entre com a empresa, matrícula ou parâmetro para a consulta.\\n");
                isErro = true;
            }
            mensagem = msg.ToString();
            return isErro;
        }

        #endregion

      
    }
}