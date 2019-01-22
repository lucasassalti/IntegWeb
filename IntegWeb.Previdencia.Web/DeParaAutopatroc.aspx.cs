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
    public partial class DeParaAutopatroc : BasePage
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
                String.IsNullOrEmpty(txtPesqMatricula.Text) &&
                String.IsNullOrEmpty(txtPesqEmpresaDestino.Text) &&
                !chkPesqExclusaoTotal.Checked &&
                !chkPesqVDPatroc.Checked &&
                !chkPesqVDPartic.Checked)
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
            short PK_COD_EMPRS_ORIGEM = Int16.Parse(keys["COD_EMPRS_ORIGEM"].ToString());
            int PK_NUM_RGTRO_EMPRG = Int32.Parse(keys["NUM_RGTRO_EMPRG"].ToString());
            short PK_COD_EMPRS_DESTINO = Int16.Parse(keys["COD_EMPRS_DESTINO"].ToString());
            //int PK_NUM_VER_DEST = Int32.Parse(keys["NUM_VER_DEST"].ToString());
            CarregarDetalhes(PK_COD_EMPRS_ORIGEM, PK_NUM_RGTRO_EMPRG, PK_COD_EMPRS_DESTINO);
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;            
            e.Cancel = true;
        }

        protected void grdCCusto_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            short PK_COD_EMPRS_ORIGEM = Int16.Parse(e.Keys["COD_EMPRS_ORIGEM"].ToString());
            int PK_NUM_RGTRO_EMPRG = Int32.Parse(e.Keys["NUM_RGTRO_EMPRG"].ToString());
            short PK_COD_EMPRS_DESTINO = Int16.Parse(e.Keys["COD_EMPRS_DESTINO"].ToString());
            //int PK_NUM_VER_DEST = Int32.Parse(e.Keys["NUM_VER_DEST"].ToString());

            DeParaAutopatrocBLL obj = new DeParaAutopatrocBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteData(PK_COD_EMPRS_ORIGEM, PK_NUM_RGTRO_EMPRG, PK_COD_EMPRS_DESTINO);
            MostraMensagem(lblPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            CarregarTela();
            e.Cancel = true;
        }

        private void CarregarTela()
        {            
            grdCCusto.DataBind();
        }

        private void CarregarDetalhes(short? pEmpresa, int? pMatricula, short? pEmpresaDestino)
        {
            DeParaAutopatrocBLL obj = new DeParaAutopatrocBLL();
            TB_SCR_DEPARA_AUTOPATROC loadObj = new TB_SCR_DEPARA_AUTOPATROC();
            loadObj = obj.GetItem(pEmpresa, pMatricula, pEmpresaDestino);
            txtEmpresa.Text = loadObj.COD_EMPRS_ORIGEM.ToString();
            txtMatricula.Text = loadObj.NUM_RGTRO_EMPRG.ToString();
            txtEmpresaDestino.Text = loadObj.COD_EMPRS_DESTINO.ToString();
            chkExclusaoTotal.Checked = (loadObj.FL_EXCLUSAO_TOTAL == "S");
            chkVDPatroc.Checked = (loadObj.VD_PATROC == "S");
            chkVDPartic.Checked = (loadObj.VD_PARTIC == "S");
            txtEmpresa.Enabled = false;
            txtMatricula.Enabled = false;
            txtEmpresaDestino.Enabled = false;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqEmpresa.Text = "";
            txtPesqMatricula.Text = "";
            txtPesqEmpresaDestino.Text = "";
            chkPesqExclusaoTotal.Checked = false;
            chkPesqVDPatroc.Checked = false;
            chkPesqVDPartic.Checked = false;
            grdCCusto.EditIndex = -1;
            grdCCusto.PageIndex = 0;
            CarregarTela();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparControles(pnlNovo.Controls);
            txtEmpresa.Enabled = true;
            txtMatricula.Enabled = true;
            txtEmpresaDestino.Enabled = true;
            chkExclusaoTotal.Enabled = true;
            chkVDPatroc.Enabled = true;
            chkVDPartic.Enabled = true;
            pnlNovo.Visible = true;
            pnlLista.Visible = false;
            pnlPesquisa.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            DeParaAutopatrocBLL obj = new DeParaAutopatrocBLL();
            TB_SCR_DEPARA_AUTOPATROC newobj = new TB_SCR_DEPARA_AUTOPATROC();
            //TB_SCR_DEPARA_AUTOPATROC oldobj = (TB_SCR_DEPARA_AUTOPATROC)Session["TB_SCR_DEPARA_AUTOPATROC.obj"];     
            newobj.COD_EMPRS_ORIGEM = Util.String2Short(txtEmpresa.Text) ?? 0;
            newobj.NUM_RGTRO_EMPRG = Util.String2Int32(txtMatricula.Text) ?? 0;
            newobj.COD_EMPRS_DESTINO = Util.String2Short(txtEmpresaDestino.Text) ?? 0;
            newobj.FL_EXCLUSAO_TOTAL = chkExclusaoTotal.Checked ? "S" : "N";
            newobj.VD_PATROC = chkVDPatroc.Checked ? "S" : "N";
            newobj.VD_PARTIC = chkVDPartic.Checked ? "S" : "N";
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