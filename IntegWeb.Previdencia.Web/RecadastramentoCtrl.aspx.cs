using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.DAL;

namespace IntegWeb.Previdencia.Web
{
    public partial class RecadastramentoCtrl : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            pnlPesquisa_Mensagem.Visible = false;
            pnlDetalhe_Mensagem.Visible = false;

            if (!IsPostBack)
            {
                RecadastramentoBLL obj = new RecadastramentoBLL();
                CarregaDropDowList(ddlPesqDtBase, obj.GetLista_Recad_base());
                ddlPesqDtBase.SelectedIndex = (ddlPesqDtBase.Items.Count > 1) ? 1 : 0;
            }         
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            //if (String.IsNullOrEmpty(txtPesqEmpresa.Text) &&
            //    String.IsNullOrEmpty(txtPesqMatricula.Text) &&
            //    String.IsNullOrEmpty(txtPesqRepresentante.Text) &&
            //    String.IsNullOrEmpty(txtPesqParticipante.Text) &&
            //    String.IsNullOrEmpty(txtPesqDtIni.Text) &&
            //    String.IsNullOrEmpty(txtPesqDtFim.Text))
            if (ddlPesqDtBase.SelectedIndex < 1)
            {
                MostraMensagem(pnlPesquisa_Mensagem, "Selecione uma Dt. Base");
                return;
            }

            grdControleRecad.DataBind();

        }

        protected void grdControleRecad_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdControleRecad.EditIndex = e.NewEditIndex;
            IOrderedDictionary keys = grdControleRecad.DataKeys[e.NewEditIndex].Values;
            //   int PK_COD_DEPOSITO_JUDIC = Int32.Parse(keys["COD_DEPOSITO_JUDIC"].ToString());
            DateTime DAT_REF_RECAD = DateTime.Parse(keys["DAT_REF_RECAD"].ToString());
            short NUM_CONTRATO = Int16.Parse(keys["NUM_CONTRATO"].ToString());
            short COD_EMPRS = Int16.Parse(keys["COD_EMPRS"].ToString());
            int NUM_RGTRO_EMPRG = Int32.Parse(keys["NUM_RGTRO_EMPRG"].ToString());
            int NUM_IDNTF_RPTANT = Int32.Parse(keys["NUM_IDNTF_RPTANT"].ToString());
            int NUM_MATR_PARTF = Int32.Parse(keys["NUM_MATR_PARTF"].ToString());
            DateTime DTH_INCLUSAO = DateTime.Parse(keys["DTH_INCLUSAO"].ToString());
            CarregarRecad(DAT_REF_RECAD, NUM_CONTRATO, COD_EMPRS, NUM_RGTRO_EMPRG, NUM_IDNTF_RPTANT);
            pnlPesquisa.Visible = false;
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
            e.Cancel = true;
        }

        protected void grdControleRecad_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IOrderedDictionary keys = grdControleRecad.DataKeys[e.RowIndex].Values;
            short NUM_CONTRATO = Int16.Parse(keys["NUM_CONTRATO"].ToString());
            short COD_EMPRS = Int16.Parse(keys["COD_EMPRS"].ToString());
            int NUM_RGTRO_EMPRG = Int32.Parse(keys["NUM_RGTRO_EMPRG"].ToString());
            int NUM_IDNTF_RPTANT = Int32.Parse(keys["NUM_IDNTF_RPTANT"].ToString());
            int NUM_MATR_PARTF = Int32.Parse(keys["NUM_MATR_PARTF"].ToString());
            RecadastramentoBLL obj = new RecadastramentoBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteData(NUM_CONTRATO, COD_EMPRS, NUM_RGTRO_EMPRG, NUM_IDNTF_RPTANT, NUM_MATR_PARTF, (user != null) ? user.login : "Desenv");
            MostraMensagem(pnlPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            CarregarTela();
            e.Cancel = true;
        }

        private void CarregarTela()
        {
            grdControleRecad.DataBind();
            pnlPesquisa.Visible = true;
            pnlLista.Visible = true;
            pnlDetalhe.Visible = false;
        }

        private PRE_TBL_RECADASTRAMENTO IniRecad(DateTime DAT_REF_RECAD, short NUM_CONTRATO, short COD_EMPRS, int NUM_RGTRO_EMPRG, int NUM_IDNTF_RPTANT)
        {
            RecadastramentoBLL obj = new RecadastramentoBLL();
            return obj.GetDataBy(DAT_REF_RECAD, NUM_CONTRATO, COD_EMPRS, NUM_RGTRO_EMPRG, NUM_IDNTF_RPTANT);
        }

        private void CarregarRecad(DateTime DAT_REF_RECAD, short NUM_CONTRATO, short COD_EMPRS, int NUM_RGTRO_EMPRG, int NUM_IDNTF_RPTANT)
        {
            PRE_TBL_RECADASTRAMENTO loadObj = new PRE_TBL_RECADASTRAMENTO();
            loadObj = IniRecad(DAT_REF_RECAD, NUM_CONTRATO, COD_EMPRS, NUM_RGTRO_EMPRG, NUM_IDNTF_RPTANT);
            //loadObj.DTH_INCLUSAO
            //loadObj.LOG_INCLUSAO      
            //loadObj.DTH_EXCLUSAO      
            CarregaDrop();
            hfNUM_MATR_PARTF.Value = loadObj.NUM_MATR_PARTF.ToString();
            hfNUM_PRCINS_ASINSS.Value = loadObj.NUM_PRCINS_ASINSS.ToString();
            txtEmpresa.Text = loadObj.COD_EMPRS.ToString();
            txtContrato.Text = loadObj.NUM_CONTRATO.ToString();
            txtMatricula.Text = loadObj.NUM_RGTRO_EMPRG.ToString();
            txtNome.Text = loadObj.NOME.ToString();
            txtDataBaseRef.Text = Util.Date2String(loadObj.DAT_REF_RECAD);
            txtRepresentante.Text = loadObj.NUM_IDNTF_RPTANT.ToString();
            txtDtNascimento.Text = Util.Date2String(loadObj.DAT_NASCIMENTO);
            txtDtFalecimento.Text = Util.Date2String(loadObj.DAT_FALECIMENTO);
            txtDtRecadastrado.Text = Util.Date2String(loadObj.DAT_RECADASTRAMENTO);
            txtDtInclusao.Text = Util.Date2String(loadObj.DTH_INCLUSAO);
            txtDIB.Text = Util.Date2String(loadObj.DIB);
            ddlTipoAtendimento.SelectedValue = (loadObj.TIP_RECADASTRAMENTO ?? 0).ToString(); // loadObj.TIP_RECADASTRAMENTO.ToString();
            txtNovoPrazo.Text = Util.Date2String(loadObj.DAT_NOVO_PRAZO);
            if (String.IsNullOrEmpty(loadObj.OBS))
            {
                loadObj.OBS = "";
                txtObs.Text = loadObj.OBS.ToString();
            }
            else
            {
                txtObs.Text = loadObj.OBS.ToString();
            }
        }

        private void LimparDadosParticip()
        {
            hfNUM_MATR_PARTF.Value = "0";
            hfNUM_PRCINS_ASINSS.Value = "0";
            //txtEmpresa.Text = "";
            //txtMatricula.Text = "";
            //txtRepresentante.Text = "";
            //txtDataBaseRef.Text = "";
            //txtContrato.Text = "";
            txtNome.Text = "";
            txtDtNascimento.Text = "";
            txtDtFalecimento.Text = "";
            txtDtRecadastrado.Text = "";
            txtDtInclusao.Text = Util.Date2String(DateTime.Now);
            txtDIB.Text = "";
            ddlTipoAtendimento.SelectedValue = "0";
            txtNovoPrazo.Text = "";
        }

        private void CarregarDadosPartic(DateTime DAT_REF_RECAD, short NUM_CONTRATO, short pCOD_EMPRS, int pNUM_RGTRO_EMPRG, int pNUM_IDNTF_RPTANT)
        {

            LimparDadosParticip();

            PRE_TBL_RECADASTRAMENTO Recad = IniRecad(DAT_REF_RECAD, NUM_CONTRATO, pCOD_EMPRS, pNUM_RGTRO_EMPRG, pNUM_IDNTF_RPTANT);

            if (Recad != null)
            {
                MostraMensagem(pnlDetalhe_Mensagem, "Este participante já faz parte desta base de recadastro.");
                CarregarRecad(DAT_REF_RECAD, NUM_CONTRATO, pCOD_EMPRS, pNUM_RGTRO_EMPRG, pNUM_IDNTF_RPTANT);
            }
            else
            {
                ParticipanteBLL obj = new ParticipanteBLL();
                PARTICIPANTE pPartic = new PARTICIPANTE();
                pPartic = obj.GetParticipanteBy(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pNUM_IDNTF_RPTANT, false);

                if (pPartic != null)
                {
                    if (pPartic.DAT_FALEC_EMPRG != null)
                    {
                        txtRepresentante.Focus();
                        MostraMensagem(pnlDetalhe_Mensagem, "Participante não se enquadra na regra para recadastramento. Motivo: Dt. Falecimento = " + Util.Date2String(pPartic.DAT_FALEC_EMPRG), "n_error");
                        return;
                    }                    

                    txtEmpresa.Text = pPartic.COD_EMPRS.ToString();
                    //txtContrato.Text = pPartic.NUM_CONTRATO.ToString();
                    txtMatricula.Text = pPartic.NUM_RGTRO_EMPRG.ToString();
                    txtNome.Text = pPartic.NOM_EMPRG.ToString();
                    //txtDataBaseRef.Text = Util.Date2String(pPartic.DAT_REF_RECAD);
                    txtRepresentante.Text = pPartic.NUM_IDNTF_RPTANT.ToString();
                    txtDtNascimento.Text = Util.Date2String(pPartic.DAT_NASCM_EMPRG);
                    txtDtFalecimento.Text = Util.Date2String(pPartic.DAT_FALEC_EMPRG);
                    //txtDtRecadastrado.Text = Util.Date2String(pPartic.DAT_RECADASTRAMENTO);
                    //txtDtInclusao.Text = Util.Date2String(pPartic.DTH_INCLUSAO);
                    txtDIB.Text = Util.Date2String(pPartic.DAT_INICIO_BFPART);
                }
                else
                {
                    txtMatricula.Focus();
                    MostraMensagem(pnlDetalhe_Mensagem, "Participante não localizado");
                }
            }
        }

        protected void txtMatricula_TextChanged(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(txtEmpresa.Text) && !String.IsNullOrEmpty(txtMatricula.Text) && String.IsNullOrEmpty(txtRepresentante.Text))
            {
                txtRepresentante.Text = "0";
                txtRepresentante.Focus();
            }

            if (String.IsNullOrEmpty(txtEmpresa.Text)) txtEmpresa.Focus();
            if (String.IsNullOrEmpty(txtMatricula.Text)) txtMatricula.Focus();

            if (!String.IsNullOrEmpty(txtEmpresa.Text) && !String.IsNullOrEmpty(txtMatricula.Text) && !String.IsNullOrEmpty(txtRepresentante.Text))
            {
                CarregarDadosPartic(DateTime.Parse(txtDataBaseRef.Text), short.Parse(txtContrato.Text), short.Parse(txtEmpresa.Text), int.Parse(txtMatricula.Text), int.Parse(txtRepresentante.Text));
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqEmpresa.Text = "";
            txtPesqMatricula.Text = "";
            txtPesqRepresentante.Text = "";
            txtPesqParticipante.Text = "";
            ddlPesqSituacao.SelectedValue = "0";
            txtPesqDtIni.Text = "";
            txtPesqDtFim.Text = "";
            CarregarTela();
        }

        private void CarregaDrop()
        {
            if (ddlTipoAtendimento.Items.Count == 0)
            {
                RecadastramentoBLL obj = new RecadastramentoBLL();
                CarregaDropDowList(ddlTipoAtendimento, obj.CarregaTipos().ToList<object>(), "DSC_RECADASTRAMENTO", "TIP_RECADASTRAMENTO");
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {

            if (ddlPesqDtBase.SelectedIndex < 1)
            {
                MostraMensagem(pnlPesquisa_Mensagem, "Selecione uma Dt. Base");
                return;
            }

            LimparControles(pnlDetalhe.Controls);

            CarregaDrop();

            string chave = ddlPesqDtBase.SelectedValue;

            txtDataBaseRef.Text = chave.Split(',')[0];
            txtContrato.Text = chave.Split(',')[1];

            txtEmpresa.Enabled = true;
            txtMatricula.Enabled = true;
            txtRepresentante.Enabled = true;
            LimparDadosParticip();
            txtEmpresa.Focus();
            pnlPesquisa.Visible = false;
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            RecadastramentoBLL obj = new RecadastramentoBLL();
            PRE_TBL_RECADASTRAMENTO newobj = new PRE_TBL_RECADASTRAMENTO();

            var user = (ConectaAD)Session["objUser"];

            newobj.DAT_REF_RECAD = Convert.ToDateTime(txtDataBaseRef.Text);
            newobj.NUM_CONTRATO = Convert.ToInt16(txtContrato.Text);
            newobj.COD_EMPRS = Util.String2Short(txtEmpresa.Text) ?? 0;
            newobj.NUM_RGTRO_EMPRG = Util.String2Int32(txtMatricula.Text) ?? 0;
            newobj.NUM_IDNTF_RPTANT = Util.String2Int32(txtRepresentante.Text) ?? 0;
            newobj.NUM_MATR_PARTF = int.Parse(hfNUM_MATR_PARTF.Value);
            newobj.NOME = txtNome.Text;
            newobj.NUM_PRCINS_ASINSS = Util.String2Decimal(hfNUM_PRCINS_ASINSS.Value);
            newobj.DAT_NASCIMENTO = Util.String2Date(txtDtNascimento.Text);
            newobj.DAT_FALECIMENTO = Util.String2Date(txtDtFalecimento.Text);
            newobj.DIB = Util.String2Date(txtDIB.Text);
            newobj.DAT_RECADASTRAMENTO = Util.String2Date(txtDtRecadastrado.Text);
            newobj.DAT_NOVO_PRAZO = Util.String2Date(txtNovoPrazo.Text);
            if (ddlTipoAtendimento.SelectedValue != "0")
            {
                newobj.TIP_RECADASTRAMENTO = Util.String2Short(ddlTipoAtendimento.SelectedValue);
            }
            newobj.OBS = txtObs.Text;
            newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
            newobj.DTH_INCLUSAO = DateTime.Now;


            Resultado res = obj.Validar(newobj);

            if (res.Ok)
            {
                res = obj.Update(newobj);
                if (res.Ok)
                {

                    MostraMensagem(pnlPesquisa_Mensagem, res.Mensagem, "n_ok");
                    Voltar();
                }
                else
                {
                    MostraMensagem(pnlDetalhe_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
                }
            }
            else
            {
                MostraMensagem(pnlDetalhe_Mensagem, res.Mensagem);
            }

        }

        private void Voltar()
        {
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