using IntegWeb.Entidades;
using IntegWeb.Framework;
//using IntegWeb.Financeira.Aplicacao.BLL;
//using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Intranet.Web
{
    public partial class ContribEsporadica : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //TbConsulta_Mensagem.Visible = false;
            //TbUpload_Mensagem.Visible = false;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPesqEmpresa.Text) &&
                String.IsNullOrEmpty(txtPesqMatricula.Text) &&
                String.IsNullOrEmpty(txtPesqCpf.Text) &&
                String.IsNullOrEmpty(txtPesNome.Text) &&
                String.IsNullOrEmpty(txtDtVencIni.Text) &&
                String.IsNullOrEmpty(txtDtVencFim.Text))
            {
                MostraMensagem(pnlPesquisa_Mensagem, "Prencha ao menos um campo de pesquisa para continuar");
            }
            else grdLista.PageIndex = 0;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqEmpresa.Text = "";
            txtPesqMatricula.Text = "";
            txtPesqCpf.Text = "";
            txtPesNome.Text = "";
            txtDtVencIni.Text = "";
            txtDtVencFim.Text = "";
            grdLista.PageIndex = 0;
            grdLista.EditIndex = -1;
            grdLista.ShowFooter = false;
            grdLista.DataBind();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            LimparControles(pnlDetalhe.Controls);
            LimparControles(pnlPgtoDetalhe.Controls);
            hfPK.Value = "0";
            //hfCOD_DEPOSITO_JUDIC_PGTO.Value = "0";
            //hfDTH_INCLUSAO.Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            hfNUM_MATR_PARTF.Value = "0";
            txtDtCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //CarregaDrop();
            txtEmpresa.Enabled = true;
            txtMatricula.Enabled = true;
            txtEmpresa.Focus();
            ddlAssunto.SelectedValue = "0";
            //btnNovoPgto.Enabled = true;
            pnlPesquisa.Visible = false;
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
            pnlPgtoDetalhe.Visible = true;
            pnlPgtoLista.Visible = false;
        }

        protected void grdLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdDepositoJudicial.EditIndex = e.NewEditIndex;
            IOrderedDictionary keys = grdLista.DataKeys[e.NewEditIndex].Values;
            int PK_COD_DEPOSITO_JUDIC = Int32.Parse(keys["COD_DEPOSITO_JUDIC"].ToString());
            //CarregarDeposito(PK_COD_DEPOSITO_JUDIC);
            pnlPesquisa.Visible = false;
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
            pnlPgtoLista.Visible = true;
            pnlPgtoDetalhe.Visible = false;
            e.Cancel = true;
        }

        protected void grdLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int _pk = Int32.Parse(e.Keys["COD_CONTRIB_ESPORADICA"].ToString());
            ContribEsporadicaBLL obj = new ContribEsporadicaBLL();
            var user = (ConectaAD)Session["objUser"];
            Resultado res = obj.DeleteData(_pk, (user != null) ? user.login : "Desenv");
            MostraMensagem(pnlPesquisa_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            //CarregarTela();
            e.Cancel = true;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            ContribEsporadicaBLL obj = new ContribEsporadicaBLL();
            PRE_TBL_CONTRIB_ESPORADICA newobj = new PRE_TBL_CONTRIB_ESPORADICA();

            var user = (ConectaAD)Session["objUser"];

            newobj.COD_CONTRIB_ESPORADICA = int.Parse(hfPK.Value);
            newobj.DTH_INCLUSAO = DateTime.Now;
            newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
            //newobj.DAT_CADASTRO = Util.String2Date(txtDtCadastro.Text);
            //newobj.NUM_MATR_PARTF = int.Parse(hfNUM_MATR_PARTF.Value);
            //newobj.COD_EMPRS = Util.String2Short(txtEmpresa.Text);
            //newobj.NUM_RGTRO_EMPRG = Util.String2Int32(txtMatricula.Text);
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

            Resultado res = obj.Validar(newobj);

            if (res.Ok)
            {
                res = obj.SaveData(newobj);
                if (res.Ok)
                {
                    hfPK.Value = newobj.COD_CONTRIB_ESPORADICA.ToString();                    
                    MostraMensagem((pnlPgtoDetalhe.Visible) ? pnlDetalhe_Mensagem : pnlPesquisa_Mensagem, res.Mensagem, "n_ok");
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Voltar();
        }

        private void Voltar()
        {
            if ((pnlPgtoDetalhe.Visible) && (hfPK.Value != "0"))
            {
                grdDepPgto.DataBind();
                pnlPgtoDetalhe.Visible = false;
                pnlPgtoLista.Visible = true;
            }
            else
            {
                //CarregarTela();
            }
        }


    }
}