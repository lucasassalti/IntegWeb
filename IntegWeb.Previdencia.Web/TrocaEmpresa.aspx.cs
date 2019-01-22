using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao;
using IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class TrocaEmpresa : BasePage
    {

        #region .: Propriedades :.

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            TrocaEmpresaBLL obj = new TrocaEmpresaBLL();
            Resultado res = new Resultado();

            res = obj.Validar(txtMatricula.Text);
            if (res.Ok)
            {
                LimparObjetos();
                CarregaCorporativo();
                CarregaCapitalizacao();

            }
            else
            {
                MostraMensagem(pnlPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
                return;
            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtMatricula.Text = "";
            LimparObjetos();
        }

        protected void btnTrocarEmpresa_Click(object sender, EventArgs e)
        {
            TrocaEmpresaBLL obj = new TrocaEmpresaBLL();
            Resultado res = obj.Update(Convert.ToInt32(lblMatricula.Text), Convert.ToInt16(lblEmpresaCorporativo.Text), Convert.ToInt16(lblEmpresaCorporativo.Text));

            if (res.Ok)
            {
                MostraMensagem(pnlPesquisa_Mensagem, res.Mensagem, "n_ok");
                CarregaCorporativo();
                CarregaCapitalizacao();
                pnlPesquisa_Mensagem.Visible = true;
            }
            else
            {
                MostraMensagem(pnlPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            }
        }

        #endregion

        #region .: Métodos :.

        public void CarregaCorporativo()
        {
            TrocaEmpresaBLL obj = new TrocaEmpresaBLL();
            CORPORATIVO objcorp = new CORPORATIVO();
            objcorp = obj.GetDataCorporativo(Convert.ToInt32(txtMatricula.Text));
            if (objcorp == null)
            {
                MostraMensagem(pnlPesquisa_Mensagem, "Participante não encontrado");
                pnlCorporativo.Visible = false;
                pnlCapitalizacao.Visible = false;
                pnlPesquisa_Mensagem.Visible = true;
                return;
            }
            else
            {
                lblEmpresaCorporativo.Text = objcorp.COD_EMPRS.ToString();
                lblNomeCorporativo.Text = objcorp.NOM_RZSOC_EMPRS;
                lblMatricula.Text = objcorp.NUM_MATR_PARTF.ToString();
                lblNomeEmpregado.Text = objcorp.NOM_EMPRG;
                pnlCorporativo.Visible = true;
                pnlCapitalizacao.Visible = true;
                pnlPesquisa_Mensagem.Visible = false;
            }
        }

        public void CarregaCapitalizacao()
        {
            TrocaEmpresaBLL obj = new TrocaEmpresaBLL();
            CAPITALIZACAO objcap = new CAPITALIZACAO();

            objcap = obj.GetDataCapitalizacao(Convert.ToInt32(txtMatricula.Text));
            if (objcap == null)
            {
                MostraMensagem(pnlPesquisa_Mensagem, "Participante não encontrado");
                pnlCorporativo.Visible = false;
                pnlCapitalizacao.Visible = false;
                pnlPesquisa_Mensagem.Visible = true;
                return;
            }
            else
            {
                lblEmpresaCapitalizacao.Text = objcap.COD_EMPRS.ToString();
                lblNumeroPatrocinadora.Text = objcap.NUM_PATROC.ToString();
                lblNomeCapitalizacao.Text = objcap.NOM_PATROC;
                lblMatriculaCapitalizacao.Text = objcap.NUM_MATR_PARTF.ToString();
                lblParticipante.Text = objcap.NOM_EMPRG;
                pnlCorporativo.Visible = true;
                pnlCapitalizacao.Visible = true;
                pnlPesquisa_Mensagem.Visible = false;

            }
        }

        public void LimparObjetos()
        {

            pnlPesquisa_Mensagem.Visible = false;
            lblEmpresaCorporativo.Text = "";
            lblNomeCorporativo.Text = "";
            lblMatricula.Text = "";
            lblNomeEmpregado.Text = "";
            lblEmpresaCapitalizacao.Text = "";
            lblNumeroPatrocinadora.Text = "";
            lblNomeCapitalizacao.Text = "";
            lblMatriculaCapitalizacao.Text = "";
            lblParticipante.Text = "";
            pnlCorporativo.Visible = false;
            pnlCapitalizacao.Visible = false;
            pnlPesquisa_Mensagem.Visible = false;
        }



        #endregion


    }
}