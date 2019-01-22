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
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao;

namespace IntegWeb.Previdencia.Web
{
    public partial class DeParaAutopatroc_Verba : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            lblPesquisa_Mensagem.Visible = false;
            lblNovo_Mensagem.Visible = false;

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

            if (String.IsNullOrEmpty(txtPesqEmpresa.Text) &&
                String.IsNullOrEmpty(txtPesqPlanoOrigem.Text) &&
                String.IsNullOrEmpty(txtPesqVerbaFund.Text) &&
                String.IsNullOrEmpty(txtPesqVerbaDest.Text))
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
            IOrderedDictionary keys = grdCCusto.DataKeys[e.NewEditIndex].Values;
            short PK_COD_EMPRS = Int16.Parse(keys["EMPRS_DEST"].ToString());
            short PK_PLANO_ORIGEM = Int16.Parse(keys["PLANO_ORIGEM"].ToString());
            int PK_NUM_VER_FUND = Int32.Parse(keys["NUM_VER_FUND"].ToString());
            //int PK_NUM_VER_DEST = Int32.Parse(keys["NUM_VER_DEST"].ToString());
            CarregarDetalhes(PK_COD_EMPRS, PK_PLANO_ORIGEM, PK_NUM_VER_FUND);
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;            
            e.Cancel = true;
        }

        protected void grdCCusto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            short PK_COD_EMPRS = Int16.Parse(e.Keys["EMPRS_DEST"].ToString());
            short PK_PLANO_ORIGEM = Int16.Parse(e.Keys["PLANO_ORIGEM"].ToString());
            int PK_NUM_VER_FUND = Int32.Parse(e.Keys["NUM_VER_FUND"].ToString());
            //int PK_NUM_VER_DEST = Int32.Parse(e.Keys["NUM_VER_DEST"].ToString());

            DeParaAutopatrocVerbaBLL obj = new DeParaAutopatrocVerbaBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteData(PK_COD_EMPRS, PK_PLANO_ORIGEM, PK_NUM_VER_FUND);
            MostraMensagem(lblPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            CarregarTela();
            e.Cancel = true;
        }

        private void CarregarTela()
        {            
            grdCCusto.DataBind();
        }

        private void CarregarDetalhes(short? pEmpresa, short? pPlanoOrigem, int? pVerbaFund)
        {
            DeParaAutopatrocVerbaBLL obj = new DeParaAutopatrocVerbaBLL();
            TB_SCR_DEPARA_AUTOPATROC_VERBA loadObj = new TB_SCR_DEPARA_AUTOPATROC_VERBA();
            loadObj = obj.GetItem(pEmpresa, pPlanoOrigem, pVerbaFund);
            txtEmpresa.Text = loadObj.EMPRS_DEST.ToString();
            txtPlanoOrigem.Text = loadObj.PLANO_ORIGEM.ToString();
            txtVerbaFund.Text = loadObj.NUM_VER_FUND.ToString();
            txtVerbaDest.Text = loadObj.NUM_VER_DEST.ToString();
            txtEmpresa.Enabled = false;
            txtPlanoOrigem.Enabled = false;
            txtVerbaFund.Enabled = false;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqEmpresa.Text = "";
            txtPesqPlanoOrigem.Text = "";
            txtPesqVerbaFund.Text = "";
            txtPesqVerbaDest.Text = "";
            grdCCusto.EditIndex = -1;
            grdCCusto.PageIndex = 0;
            CarregarTela();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparControles(pnlNovo.Controls);
            txtEmpresa.Enabled = true;
            txtPlanoOrigem.Enabled = true;
            txtVerbaFund.Enabled = true;
            txtVerbaDest.Enabled = true;
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            DeParaAutopatrocVerbaBLL obj = new DeParaAutopatrocVerbaBLL();
            TB_SCR_DEPARA_AUTOPATROC_VERBA newobj = new TB_SCR_DEPARA_AUTOPATROC_VERBA();
            //TB_SCR_DEPARA_AUTOPATROC_VERBA oldobj = (TB_SCR_DEPARA_AUTOPATROC_VERBA)Session["TB_SCR_DEPARA_AUTOPATROC_VERBA.obj"];     
            newobj.EMPRS_DEST = Util.String2Short(txtEmpresa.Text) ?? 0;
            newobj.PLANO_ORIGEM = Util.String2Short(txtPlanoOrigem.Text) ?? 0;
            newobj.NUM_VER_FUND = Util.String2Int32(txtVerbaFund.Text) ?? 0;
            newobj.NUM_VER_DEST = Util.String2Int32(txtVerbaDest.Text) ?? 0;

            Resultado res = obj.Validar(newobj, txtEmpresa.Enabled);

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