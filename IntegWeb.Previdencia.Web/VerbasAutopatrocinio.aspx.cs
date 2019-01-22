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
    public partial class VerbasAutopatrocinio : BasePage
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
                String.IsNullOrEmpty(txtPesqVerba.Text) &&
                String.IsNullOrEmpty(txtPesqGrupoContrib.Text) &&
                String.IsNullOrEmpty(txtPesqNumPlano.Text))
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
            short PK_COD_EMPRS = Int16.Parse(keys["COD_EMPRS"].ToString());
            int PK_NUM_VRBFSS = Int32.Parse(keys["NUM_VRBFSS"].ToString());
            //int PK_NUM_VER_FUND = Int32.Parse(keys["NUM_VER_FUND"].ToString());
            //int PK_NUM_VER_DEST = Int32.Parse(keys["NUM_VER_DEST"].ToString());
            CarregarDetalhes(PK_COD_EMPRS, PK_NUM_VRBFSS);
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;
            e.Cancel = true;
        }

        protected void grdCCusto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            short PK_COD_EMPRS = Int16.Parse(e.Keys["COD_EMPRS"].ToString());
            int   PK_NUM_VRBFSS = Int32.Parse(e.Keys["NUM_VRBFSS"].ToString());
            //int PK_NUM_VER_DEST = Int32.Parse(e.Keys["NUM_VER_DEST"].ToString());

            VerbasAutopatrocinioBLL obj = new VerbasAutopatrocinioBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteData(PK_COD_EMPRS, PK_NUM_VRBFSS);
            MostraMensagem(lblPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            CarregarTela();
            e.Cancel = true;
        }

        private void CarregarTela()
        {
            grdCCusto.DataBind();
        }

        private void CarregarDetalhes(short? pEmpresa, int? pVerba)
        {
            VerbasAutopatrocinioBLL obj = new VerbasAutopatrocinioBLL();
            TB_SCR_VERBAS_AUTOPATROC loadObj = new TB_SCR_VERBAS_AUTOPATROC();
            loadObj = obj.GetItem(pEmpresa, pVerba);
            txtEmpresa.Text = loadObj.COD_EMPRS.ToString();
            txtVerba.Text = loadObj.NUM_VRBFSS.ToString();
            txtGrupoContrib.Text = loadObj.COD_GRUPO_CONTRIB.ToString();
            txtNumPlano.Text = loadObj.NUM_PLBNF.ToString();
            txtEmpresa.Enabled = false;
            txtVerba.Enabled = false;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqEmpresa.Text = "";
            txtPesqVerba.Text = "";
            txtPesqGrupoContrib.Text = "";
            txtPesqNumPlano.Text = "";
            grdCCusto.EditIndex = -1;
            grdCCusto.PageIndex = 0;
            CarregarTela();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparControles(pnlNovo.Controls);
            txtEmpresa.Enabled = true;
            txtVerba.Enabled = true;
            txtGrupoContrib.Enabled = true;
            txtNumPlano.Enabled = true;
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            VerbasAutopatrocinioBLL obj = new VerbasAutopatrocinioBLL();
            TB_SCR_VERBAS_AUTOPATROC newobj = new TB_SCR_VERBAS_AUTOPATROC();  
            newobj.COD_EMPRS = Util.String2Short(txtEmpresa.Text) ?? 0;
            newobj.NUM_VRBFSS = Util.String2Int32(txtVerba.Text) ?? 0;
            newobj.COD_GRUPO_CONTRIB = Util.String2Int32(txtGrupoContrib.Text);
            newobj.NUM_PLBNF = Util.String2Short(txtNumPlano.Text);

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