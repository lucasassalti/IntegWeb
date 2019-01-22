using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mime;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Saude.Aplicacao;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.BLL.Processos;

namespace IntegWeb.Intranet.Web
{
    public partial class ConsultaProvisoriaAtendimento : BasePage
    {
        bool integracao_crm = false;

        #region .: Page Load :.

        //Validação de conexão com CRM - Ainda nao testada
        protected void Page_PreInit(object sender, EventArgs e)
        {            
            if ((!String.IsNullOrEmpty(Request.QueryString["nempr"])) &&
                (!String.IsNullOrEmpty(Request.QueryString["nreg"])) ||
                (!String.IsNullOrEmpty(Request.QueryString["nrepr"])))
            {
                integracao_crm = true;
            }
            else if ((!String.IsNullOrEmpty(Request.QueryString["nempr"])) &&
                (!String.IsNullOrEmpty(Request.QueryString["nreg"])) ||
                (!String.IsNullOrEmpty(Request.QueryString["nCodDep"])))
            {
                integracao_crm = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                provisoriaAtendimentoBLL bll = new provisoriaAtendimentoBLL();
                provisoriaAtendimentoBLL.SAU_TBL_Provisoria_Atendimento_view obj = new provisoriaAtendimentoBLL.SAU_TBL_Provisoria_Atendimento_view();

                //Recupera os parâmetros da URL
                int empresa = Convert.ToInt32(Request.QueryString["nempr"]);
                int registroEmpregado = Convert.ToInt32(Request.QueryString["nreg"]);
                int? representante = Util.String2Int32(Request.QueryString["nrepr"]);
                int? dependente = Util.String2Int32(Request.QueryString["nDep"]);

                txtEmpresa.Text = empresa.ToString();
                txtRegistroEmpregado.Text = registroEmpregado.ToString();
                lblRepresentante.Text = representante.ToString();
                lblDependente.Text = dependente.ToString();

                btnPesquisar_Click(null, null);
            }
        }

        #endregion        

        #region .: Acao Botoes :.  

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdProvisoriaAtendimento.Visible = true;
            grdProvisoriaAtendimento.DataBind();
            btnImprimir.Visible = grdProvisoriaAtendimento.Rows.Count > 0;
            MostraBotao(ddlPlano.SelectedValue.ToString());
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            //provisoriaAtendimentoBLL bll = new provisoriaAtendimentoBLL();
            //provisoriaAtendimentoBLL.SAU_TBL_Provisoria_Atendimento_view obj = new provisoriaAtendimentoBLL.SAU_TBL_Provisoria_Atendimento_view();

            //foreach (GridViewRow row in grdProvisoriaAtendimento.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox chkSelect = (row.FindControl("chkSelect") as CheckBox);
            //        if (chkSelect.Checked)
            //        {
            //            obj.NOM_PARTICIP = row.Cells[1].Text;//nome
            //            obj.DES_PLANO = row.Cells[2].Text;//plano
            //            obj.DAT_VALIDADECI = Util.String2Date(row.Cells[3].Text);//validade ci
            //            obj.DAT_PARTO = Util.String2Date(row.Cells[4].Text);//dat parto
            //            obj.DAT_NASCM_EMPRG = Convert.ToDateTime(row.Cells[5].Text);//nascimento
            //            obj.NOM_MAE = row.Cells[6].Text;//mae
            //            obj.NUM_CPF = row.Cells[7].Text;//cpf
            //            int? empresa = Util.String2Int32(txtEmpresa.Text);
            //            int? matricula = Util.String2Int32(txtRegistroEmpregado.Text);
            //            int? representante = Util.String2Int32(lblRepresentante.Text);
            //            int? dependente = Util.String2Int32(lblDependente.Text);

            //            //Resultado res = bll.AtualizaTabelaControleUnimed(obj);
            //            //if (!res.Ok)
            //            //{
            //            //    MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
            //            //    grdControleUnimed.EditIndex = -1;
            //            //    grdControleUnimed.PageIndex = 0;
            //            //    grdControleUnimed.DataBind();
            //            //    LimpaCampoPopUp();
            //            //}
            //        }
            //    }
            //}
            //MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registros Atualizados com Sucesso");
            //grdControleUnimed.EditIndex = -1;
            //grdControleUnimed.PageIndex = 0;
            //grdControleUnimed.DataBind();
            //LimpaCampoPopUp();
        }
        #endregion

        #region .: Metodos :.

        // Metodo define qual botão de inserção manual será exibido, de acordo com o plano selecionado na dropdown
        private void MostraBotao(string plano)
        {
            switch (plano)
            {
                case "1111":
                    btnDigna.Visible = true;
                    btnDigna.Enabled = true;

                    btnPesNosso.Visible = false;
                    btnPesNosso.Enabled = false;
                    btnEmTransito.Visible = false;
                    btnEmTransito.Enabled = false;
                    btnConveniada.Visible = false;
                    btnConveniada.Enabled = false;
                    break;

                case "1112":
                    btnPesNosso.Visible = true;
                    btnPesNosso.Enabled = true;

                    btnDigna.Visible = false;
                    btnDigna.Enabled = false;
                    btnEmTransito.Visible = false;
                    btnEmTransito.Enabled = false;
                    btnConveniada.Visible = false;
                    btnConveniada.Enabled = false;
                    break;

                case "1113":
                    btnEmTransito.Visible = true;
                    btnEmTransito.Enabled = true;

                    btnDigna.Visible = false;
                    btnDigna.Enabled = false;
                    btnPesNosso.Visible = false;
                    btnPesNosso.Enabled = false;
                    btnConveniada.Visible = false;
                    btnConveniada.Enabled = false;
                    break;

                case "1114":
                    btnConveniada.Visible = true;
                    btnConveniada.Enabled = true;

                    btnDigna.Visible = false;
                    btnDigna.Enabled = false;
                    btnPesNosso.Visible = false;
                    btnPesNosso.Enabled = false;
                    btnEmTransito.Visible = false;
                    btnEmTransito.Enabled = false;
                    break;
            }
        }

        private void LimpaCampos()
        {
            txtNomeDigna1.Text = "";
            txtNomeMaeDigna1.Text = "";
            txtPlanoDigna1.Text = "";
            txtCpfDigna1.Text = "";
            txtCodigoDigna1.Text = "";
            txtDataNascimentoDigna1.Text = "";
            txtDataAutorizacaoNovoDigna1.Text = "";

            txtNomePesNosso1.Text = "";
            txtNomeMaePesNosso1.Text = "";
            txtPlanoPesNosso1.Text = "";
            txtCpfPesNosso1.Text = "";
            txtCodigoPesNosso1.Text = "";
            txtDataNascimentoPesNosso1.Text = "";
            txtDataAutorizacaoNovoPesNosso1.Text = "";

            txtNomeEmTransito1.Text = "";
            txtNomeMaeEmTransito1.Text = "";
            txtPlanoEmTransito1.Text = "";
            txtCpfEmTransito1.Text = "";
            txtEstCivilEmTransito1.Text = "";
            txtParentescoEmTransito1.Text = "";
            txtCodigoEmTransito1.Text = "";
            txtDataNascimentoEmTransito1.Text = "";
            txtParaEmTransito1.Text = "";
            txtLocalEmTransito1.Text = "";
            txtFaxEmTransito1.Text = "";
            txtViasEmTransito1.Text = "";
            txtAttEmTransito1.Text = "";
            txtValidadeNovoEmTransito1.Text = "";
            txtPartoEmTransito1.Text = "";

            txtNomeConveniada1.Text = "";
            txtNomeMaeConveniada1.Text = "";
            txtPlanoConveniada1.Text = "";
            txtCpfConveniada1.Text = "";
            txtCodigoConveniada1.Text = "";
            txtDataNascimentoConveniada1.Text = "";
            txtRecursoConveniada1.Text = "";
            txtValidadeNovoConveniada1.Text = "";
            txtEmissaoConveniada1.Text = "";

        }

        protected void ddlPlano_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdProvisoriaAtendimento.Visible = true;
            grdProvisoriaAtendimento.DataBind();
            btnImprimir.Visible = grdProvisoriaAtendimento.Rows.Count > 0;
            MostraBotao(ddlPlano.SelectedValue.ToString());
        }
        #endregion

        #region .: DIGNA/AMH :.

        // Exibe div de inserção manual digna
        protected void btnDigna_Click(object sender, EventArgs e)
        {
            divImpressao.Visible = false;
            divInserirDigna.Visible = true;
            divInserirPesNosso.Visible = false;
            divInserirEmTransito.Visible = false;
            divInserirConveniada.Visible = false;
        }

        protected void btnImprimirNovoDigna_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarDigna_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            divImpressao.Visible = true;
            divInserirDigna.Visible = false;

            grdProvisoriaAtendimento.EditIndex = -1;
            grdProvisoriaAtendimento.PageIndex = 0;
            grdProvisoriaAtendimento.DataBind();
        }

        protected void btnLimparDigna_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }
        #endregion

        #region .: PES/NOSSO :.

        // Exibe div de inserção manual Pes/Nosso
        protected void btnPesNosso_Click(object sender, EventArgs e)
        {
            divImpressao.Visible = false;
            divInserirDigna.Visible = false;
            divInserirPesNosso.Visible = true;
            divInserirEmTransito.Visible = false;
            divInserirConveniada.Visible = false;
        }

        protected void btnImprimirNovoPesNosso_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarPesNosso_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            divImpressao.Visible = true;
            divInserirPesNosso.Visible = false;

            grdProvisoriaAtendimento.EditIndex = -1;
            grdProvisoriaAtendimento.PageIndex = 0;
            grdProvisoriaAtendimento.DataBind();
        }        

        protected void btnLimparPesNosso_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }
        #endregion
        
        #region .: Em Transito :.

        // Exibe div de inserção manual EmTransito
        protected void btnEmTransito_Click(object sender, EventArgs e)
        {
            divImpressao.Visible = false;
            divInserirDigna.Visible = false;
            divInserirPesNosso.Visible = false;
            divInserirEmTransito.Visible = true;
            divInserirConveniada.Visible = false;
        }

        protected void btnImprimirNovoEmTransito_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarEmTransito_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            divImpressao.Visible = true;
            divInserirEmTransito.Visible = false;

            grdProvisoriaAtendimento.EditIndex = -1;
            grdProvisoriaAtendimento.PageIndex = 0;
            grdProvisoriaAtendimento.DataBind();
        }

        protected void btnLimparEmTransito_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }      

        #endregion

        #region .: Conveniada :.

        // Exibe div de inserção manual Conveniada
        protected void btnConveniada_Click(object sender, EventArgs e)
        {
            divImpressao.Visible = false;
            divInserirDigna.Visible = false;
            divInserirPesNosso.Visible = false;
            divInserirEmTransito.Visible = false;
            divInserirConveniada.Visible = true;
        }

        protected void btnImprimirNovoConveniada_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelarConveniada_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            divImpressao.Visible = true;
            divInserirConveniada.Visible = false;

            grdProvisoriaAtendimento.EditIndex = -1;
            grdProvisoriaAtendimento.PageIndex = 0;
            grdProvisoriaAtendimento.DataBind();
        }

        protected void btnLimparConveniada_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        #endregion

        protected void radTipoAtend1_CheckedChanged(object sender, EventArgs e)
        {
            radTipoAtend2.Checked = false;
            lblTipoAtend1.Visible = true;
            lblTipoAtend2.Visible = false;
        }

        protected void radTipoAtend2_CheckedChanged(object sender, EventArgs e)
        {
            radTipoAtend1.Checked = false;
            lblTipoAtend1.Visible = false;
            lblTipoAtend2.Visible = true;
        }




    }
}