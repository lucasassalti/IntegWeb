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
using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;

namespace IntegWeb.Previdencia.Web
{
    public partial class CadDepJudicial : BasePage
    {        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            pnlPesquisa_Mensagem.Visible = false;
            pnlDetalhe_Mensagem.Visible = false;

            if (!IsPostBack)
            {
                //CarregaTelaPesquisa();
                //grdDepositoJudicial.DataBind();
                //pnlGridVr.Visible = true;
                //grdValorReferencia.Sort("num_proc", SortDirection.Ascending);  
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtPesqEmpresa.Text) && String.IsNullOrEmpty(txtPesqMatricula.Text) && String.IsNullOrEmpty(txtPesquisa.Text))
            {
                MostraMensagem(pnlPesquisa_Mensagem, "Entre com a empresa, matrícula ou parâmetro para a consulta");
                return;
            }

            grdDepositoJudicial.DataBind();

        }

        protected void grdDepositoJudicial_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdDepositoJudicial.EditIndex = e.NewEditIndex;
            IOrderedDictionary keys = grdDepositoJudicial.DataKeys[e.NewEditIndex].Values;
            int PK_COD_DEPOSITO_JUDIC = Int32.Parse(keys["COD_DEPOSITO_JUDIC"].ToString());
            CarregarDeposito(PK_COD_DEPOSITO_JUDIC);
            pnlPesquisa.Visible = false;
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
            pnlPgtoLista.Visible = true;
            pnlPgtoDetalhe.Visible = false;
            e.Cancel = true;
        }

        protected void grdDepositoJudicial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int PK_COD_DEPOSITO_JUDIC = Int32.Parse(e.Keys["COD_DEPOSITO_JUDIC"].ToString());
            DepositoJudicialBLL obj = new DepositoJudicialBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteData(PK_COD_DEPOSITO_JUDIC, (user != null) ? user.login : "Desenv");
            MostraMensagem(pnlPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            CarregarTela();
            e.Cancel = true;
        }

        protected void grdDepPgto_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdDepositoJudicial.EditIndex = e.NewEditIndex;
            IOrderedDictionary keys = grdDepPgto.DataKeys[e.NewEditIndex].Values;
            int PK_COD_DEPOSITO_JUDIC_PGTO = Int32.Parse(keys["COD_DEPOSITO_JUDIC_PGTO"].ToString());
            CarregarPgto(PK_COD_DEPOSITO_JUDIC_PGTO);
            pnlPgtoLista.Visible = false;
            pnlPgtoDetalhe.Visible = true;
            e.Cancel = true;
            ScriptManager.RegisterStartupScript(upUpdatepanel,
                   upUpdatepanel.GetType(),
                   "script",
                   "{ CalculoBenef(); CalculoCustos(); }",
                    true);
        }

        protected void grdDepPgto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int PK_COD_DEPOSITO_JUDIC_PGTO = Int32.Parse(e.Keys["COD_DEPOSITO_JUDIC_PGTO"].ToString());
            DepositoJudicialBLL obj = new DepositoJudicialBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteDataPgto(PK_COD_DEPOSITO_JUDIC_PGTO, (user != null) ? user.login : "Desenv");
            MostraMensagem(pnlDetalhe_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            pnlPgtoDetalhe.Visible = false;
            pnlPgtoLista.Visible = true;
            grdDepPgto.DataBind(); 
            e.Cancel = true;
        }

        private void CarregarTela()
        {
            grdDepositoJudicial.DataBind();
            pnlPesquisa.Visible = true;
            pnlLista.Visible = true;
            pnlDetalhe.Visible = false;
            pnlPgtoDetalhe.Visible = false;
            pnlPgtoLista.Visible = false;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            pnlDetalhe_1.Enabled = false;
        }

        private void CarregarDeposito(int PK_COD_DEPOSITO_JUDIC)
        {
            DepositoJudicialBLL obj = new DepositoJudicialBLL();
            PRE_TBL_DEPOSITO_JUDIC loadObj = new PRE_TBL_DEPOSITO_JUDIC();
            CarregaDrop();
            loadObj = obj.GetData(PK_COD_DEPOSITO_JUDIC);
            hfCOD_DEPOSITO_JUDIC.Value = PK_COD_DEPOSITO_JUDIC.ToString();
            //hfDTH_INCLUSAO.Value = PK_DTH_INCLUSAO.ToString();
            txtDtCadastro.Text = Util.Date2String(loadObj.DAT_CADASTRO);
            //loadObj.DTH_INCLUSAO
            //loadObj.LOG_INCLUSAO      
            //loadObj.DTH_EXCLUSAO      
            hfNUM_MATR_PARTF.Value = loadObj.NUM_MATR_PARTF.ToString();
            txtEmpresa.Text = loadObj.COD_EMPRS.ToString();
            txtMatricula.Text = loadObj.NUM_RGTRO_EMPRG.ToString();
            txtCPF.Text = loadObj.CPF_EMPRG.ToString();
            txtNome.Text = loadObj.NOM_EMPRG.ToString();
            txtDtAdmissao.Text = Util.Date2String(loadObj.DAT_ADMISSAO);
            txtDtDemissao.Text = Util.Date2String(loadObj.DAT_DEMISSAO);
            txtDtNascimento.Text = Util.Date2String(loadObj.DAT_NASCTO);
            txtDtAdesao.Text = Util.Date2String(loadObj.DAT_ADESAO);         
            //loadObj.DIB = null;
            //loadObj.DIP = null;
            //loadObj.NUM_PLBNF         
            txtPlano.Text = loadObj.PLANO;
            //loadObj.COD_SITPAR = null;
            //loadObj.COD_TPPCP  = null;        
            txtPerfil.Text = loadObj.PERFIL;
            txtNumProcesso.Text=loadObj.NRO_PROCESSO;
            txtVara.Text=loadObj.COD_VARA_PROC;
            ddlPoloAcaoJudicial.SelectedValue=loadObj.POLO_ACJUD;
            ddlAssunto.SelectedValue=loadObj.COD_TIPLTO.ToString();
            txtPasta.Text = loadObj.NRO_PASTA;
            btnNovoPgto.Enabled = true;
            grdDepPgto.DataBind();
        }

        private void CarregarPgto(int PK_COD_DEPOSITO_JUDIC_PGTO)
        {
            DepositoJudicialBLL obj = new DepositoJudicialBLL();
            PRE_TBL_DEPOSITO_JUDIC_PGTO loadObj = new PRE_TBL_DEPOSITO_JUDIC_PGTO();
            
            loadObj = obj.GetDataPgto(PK_COD_DEPOSITO_JUDIC_PGTO);
            hfCOD_DEPOSITO_JUDIC_PGTO.Value = PK_COD_DEPOSITO_JUDIC_PGTO.ToString();

            //hfCOD_DEPOSITO_JUDIC.Value = loadObj.COD_DEPOSITO_JUDIC;
            //hfDTH_INCLUSAO_PGTO.Value = DTH_INCLUSAO.ToString();
            // DTH_INCLUSAO_FK            
            // LOG_INCLUSAO
            // DTH_EXCLUSAO
            ddlTipoCadastro.SelectedValue = loadObj.TIP_CADASTRO.ToString();
            txtNumPP.Text = loadObj.NUM_PP;
            ddlTipoSolicitacao.SelectedValue = loadObj.TIP_SOLICITACAO.ToString();
            txtDtPagamento.Text = Util.Date2String(loadObj.DTH_PAGAMENTO);
            ddlFormaPagamento.SelectedValue = loadObj.TIP_PAGAMENTO.ToString();
            txtNomCredor.Text = loadObj.NOM_CREDOR;
            txtVlrBSPS.Text = loadObj.VLR_BSPS.ToString();
            txtVlrBD.Text = loadObj.VLR_BD.ToString();
            txtVlrCV.Text = loadObj.VLR_CV.ToString();
            txtVlrBSPS_custo.Text = loadObj.VLR_BSPS_CUSTO.ToString();
            txtVlrBD_custo.Text = loadObj.VLR_BD_CUSTO.ToString();
            txtVlrCV_custo.Text = loadObj.VLR_CV_CUSTO.ToString();
            lblTotal.Text = "";
            lblTotal_custo.Text = "";
            txtDescricao.Text = loadObj.DSC_DESCRICAO;
            txtNumCarta.Text = loadObj.NUM_CARTA.ToString();
            txtDtEnvioCarta.Text =  Util.Date2String(loadObj.DAT_CARTA_ENVIO);
            txtDtEnvioEMail.Text = Util.Date2String(loadObj.DAT_EMAIL_ENVIO);
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesquisa.Text = "";
            txtPesqEmpresa.Text = "";
            txtPesqMatricula.Text = "";
            CarregarTela();
            //btnNovoPgto.Enabled = false;
            //pnlPgtoDetalhe.Visible = true;
            //pnlPgtoLista.Visible = false;
        }

        private void CarregaDrop()
        {
            ValorReferenciaBLL obj = new ValorReferenciaBLL();
            CarregaDropDowDT(obj.CarregaAssunto(), ddlAssunto);
        }

        protected void btnNovoPgto_Click(object sender, EventArgs e)
        {
            hfCOD_DEPOSITO_JUDIC_PGTO.Value = "0";
            //hfDTH_INCLUSAO_PGTO.Value = hfDTH_INCLUSAO.Value;
            LimparControles(pnlPgtoDetalhe.Controls);
            btnNovoPgto.Enabled = false;
            pnlPgtoDetalhe.Visible = true;
            pnlPgtoLista.Visible = false;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            pnlDetalhe_1.Enabled = true;
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparControles(pnlDetalhe_1.Controls);
            LimparControles(pnlPgtoDetalhe.Controls);
            hfCOD_DEPOSITO_JUDIC.Value = "0";
            hfCOD_DEPOSITO_JUDIC_PGTO.Value = "0";
            //hfDTH_INCLUSAO.Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            hfNUM_MATR_PARTF.Value = "0";
            txtDtCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CarregaDrop();            
            txtEmpresa.Enabled = true;
            txtMatricula.Enabled = true;
            txtEmpresa.Focus();
            ddlAssunto.SelectedValue = "0";
            btnNovoPgto.Enabled = true;
            pnlPesquisa.Visible = false;
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
            pnlPgtoDetalhe.Visible = true;
            pnlPgtoLista.Visible = false;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            pnlDetalhe_1.Enabled = true;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            DepositoJudicialBLL obj = new DepositoJudicialBLL();
            PRE_TBL_DEPOSITO_JUDIC newobj = new PRE_TBL_DEPOSITO_JUDIC();
            PRE_TBL_DEPOSITO_JUDIC_PGTO newPgto = null;

            var user = (ConectaAD)Session["objUser"];

            newobj.COD_DEPOSITO_JUDIC = int.Parse(hfCOD_DEPOSITO_JUDIC.Value);
            newobj.DTH_INCLUSAO = DateTime.Now;
            newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
            newobj.DAT_CADASTRO = Util .String2Date(txtDtCadastro.Text);
            newobj.NUM_MATR_PARTF = int.Parse(hfNUM_MATR_PARTF.Value);            
            newobj.COD_EMPRS = Util.String2Short(txtEmpresa.Text);
            newobj.NUM_RGTRO_EMPRG = Util.String2Int32(txtMatricula.Text);
            newobj.CPF_EMPRG = Util.String2Int64 (txtCPF.Text);
            newobj.NOM_EMPRG = txtNome.Text;
            newobj.DAT_ADMISSAO = Util.String2Date(txtDtAdmissao.Text);
            newobj.DAT_DEMISSAO = Util.String2Date(txtDtDemissao.Text);
            newobj.DAT_NASCTO = Util.String2Date(txtDtNascimento.Text);
            newobj.DAT_ADESAO = Util.String2Date(txtDtAdesao.Text);
            //newobj.DIB = null;
            //newobj.DIP = null;
            //newobj.NUM_PLBNF         
            newobj.PLANO = txtPlano.Text;
            //newobj.COD_SITPAR = null;
            //newobj.COD_TPPCP  = null;        
            newobj.PERFIL = txtPerfil.Text;
            newobj.NRO_PROCESSO = txtNumProcesso.Text;     
            newobj.COD_VARA_PROC = txtVara.Text;
            newobj.POLO_ACJUD = ddlPoloAcaoJudicial.SelectedValue; 
            newobj.COD_TIPLTO = short.Parse(ddlAssunto.SelectedValue);
            newobj.NRO_PASTA = txtPasta.Text;

            if (pnlPgtoDetalhe.Visible)
            {
                newPgto = new PRE_TBL_DEPOSITO_JUDIC_PGTO();
                newPgto.COD_DEPOSITO_JUDIC_PGTO =  int.Parse(hfCOD_DEPOSITO_JUDIC_PGTO.Value);
                newPgto.COD_DEPOSITO_JUDIC = int.Parse(hfCOD_DEPOSITO_JUDIC.Value);   
                newPgto.DTH_INCLUSAO =  newobj.DTH_INCLUSAO;
                newPgto.LOG_INCLUSAO = newobj.LOG_INCLUSAO;                
                newPgto.TIP_CADASTRO = ddlTipoCadastro.SelectedValue; 
                newPgto.NUM_PP = txtNumPP.Text;
                newPgto.TIP_SOLICITACAO = Util.String2Short(ddlTipoSolicitacao.SelectedValue);
                newPgto.DTH_PAGAMENTO = Util.String2Date(txtDtPagamento.Text);
                newPgto.TIP_PAGAMENTO = Util.String2Short(ddlFormaPagamento.SelectedValue);
                newPgto.NOM_CREDOR = txtNomCredor.Text;
                newPgto.VLR_BSPS = Util.String2Decimal(txtVlrBSPS.Text);
                newPgto.VLR_BD = Util.String2Decimal(txtVlrBD.Text);
                newPgto.VLR_CV = Util.String2Decimal(txtVlrCV.Text);
                newPgto.VLR_BSPS_CUSTO = Util.String2Decimal(txtVlrBSPS_custo.Text);
                newPgto.VLR_BD_CUSTO = Util.String2Decimal(txtVlrBD_custo.Text);
                newPgto.VLR_CV_CUSTO = Util.String2Decimal(txtVlrCV_custo.Text);
                newPgto.DSC_DESCRICAO = txtDescricao.Text;
                newPgto.NUM_CARTA = Util.String2Int32(txtNumCarta.Text);
                newPgto.DAT_CARTA_ENVIO = Util.String2Date(txtDtEnvioCarta.Text);
                newPgto.DAT_EMAIL_ENVIO = Util.String2Date(txtDtEnvioEMail.Text);

                //newobj.PRE_TBL_DEPOSITO_JUDIC_PGTO.Add(newPgto);
            }

            Resultado res = obj.Validar(newobj, newPgto);

            if (res.Ok)
            {
                res = obj.SaveData(newobj, newPgto);
                if (res.Ok)
                {
                    hfCOD_DEPOSITO_JUDIC.Value = newobj.COD_DEPOSITO_JUDIC.ToString();
                    btnNovoPgto.Enabled = true;
                    MostraMensagem((pnlPgtoDetalhe.Visible) ? pnlDetalhe_Mensagem : pnlPesquisa_Mensagem, res.Mensagem, "n_ok");
                    Voltar();
                }
                else
                {
                    MostraMensagem(pnlDetalhe_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
                }
            } else {
                MostraMensagem(pnlDetalhe_Mensagem, res.Mensagem);
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            pnlDetalhe_1.Enabled = true;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
        }

        private void Voltar()
        {
            if ((pnlPgtoDetalhe.Visible) && (hfCOD_DEPOSITO_JUDIC.Value != "0"))
            {
                grdDepPgto.DataBind();
                btnNovoPgto.Enabled = true;
                pnlPgtoDetalhe.Visible = false;
                pnlPgtoLista.Visible = true;
                //btnSalvar.Enabled = false;
                //btnEditar.Enabled = true;
                //pnlDetalhe_1.Enabled = false;
            }
            else
            {
                CarregarTela();
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Voltar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Voltar();
        }

        protected void txtMatricula_TextChanged(object sender, EventArgs e)
        {

            int Emp, Matr;

            if (int.TryParse(txtEmpresa.Text, out Emp) && int.TryParse(txtMatricula.Text, out Matr))
            {
                CarregarDadosPartic(Emp, Matr);
            }            
        }

        private void CarregarDadosPartic(int cod_emprs, int num_rgtro_emprg)
        {
            ValorReferenciaBLL obj = new ValorReferenciaBLL();
            obj.cod_emprs = cod_emprs;
            //obj.num_matr_partf = num_matr_partf;
            obj.num_rgtro_emprg = num_rgtro_emprg;
            DataTable dt = obj.CarregarDadosParticipante();

            LimparIdentParticip();

            if (dt.Rows.Count > 0)
            {
                hfNUM_MATR_PARTF.Value = dt.Rows[0]["NUM_MATR_PARTF"].ToString();
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

                txtPerfil.Text = dt.Rows[0]["PERFIL"].ToString();

            }
            else
            {
                MostraMensagem(pnlDetalhe_Mensagem, "Participante não localizado!");
            }
        }

        private void LimparIdentParticip()
        {
            txtCPF.Text = "";
            txtPlano.Text = "";
            txtNome.Text = "";
            txtDtAdesao.Text = "";
            txtDtAdmissao.Text = "";
            txtDtDemissao.Text = "";
            txtDtNascimento.Text = "";
            txtPerfil.Text = "";
        }

        protected void btnDadosPartic_Click(object sender, EventArgs e)
        {

            int Emp, Matr;

            if (int.TryParse(txtEmpresa.Text, out Emp) && int.TryParse(txtMatricula.Text, out Matr))
            {
                CarregarDadosPartic(Emp, Matr);
            } 
        }

    }
}