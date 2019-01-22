using CrystalDecisions.CrystalReports.Engine;
using IntegWeb.Administracao.Aplicacao;
using IntegWeb.Administracao.Aplicacao.BLL;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Administracao.Aplicacao.ENTITY;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Administracao.Web
{
    public partial class Relatorio : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(grdRelatorio.SortExpression))
                grdRelatorio.Sort("ID_RELATORIO", SortDirection.Descending);

            spanMensagem.Visible = false;
            spanMensagemDetalhe.Visible = false;
            spanMensagemParametro.Visible = false;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtPesqRelatorio.Text.Equals("") && txtPesqTitulo.Text.Equals(""))
            {
                //MostraMensagemTelaUpdatePanel(upUpdatepanel, "Prencha um campo de pesquisa para continuar");
                MostraMensagem(spanMensagem, "Prencha um campo de pesquisa para continuar");
            }
            else grdRelatorio.PageIndex = 0;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesqRelatorio.Text = "";
            txtPesqTitulo.Text = "";
            grdRelatorio.PageIndex = 0;
        }

        protected void grdRelatorio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdRelatorio.EditIndex = e.NewEditIndex;
            int CodRelatorio = Int32.Parse(grdRelatorio.DataKeys[e.NewEditIndex].Value.ToString());
            CarregarDetalhesRelatorio(CodRelatorio);
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
            btnInserirParam.Enabled = true;
            e.Cancel = true;
        }

        private void CarregarDetalhesRelatorio(int CodRelatorio)
        {
            RelatorioBLL RelBLL = new RelatorioBLL();
            FUN_TBL_RELATORIO rel = RelBLL.ConsultarRelatorio(CodRelatorio);
            CarregaDropDowList(ddlTipo, RelBLL.GetRelatorioTipos().ToList<Object>(), "NM_TIPO", "ID_TIPO_RELATORIO");
            txtCodigo.Text = rel.ID_RELATORIO.ToString();
            txtRelatorio.Text = rel.RELATORIO;
            ddlTipo.SelectedValue = rel.ID_TIPO_RELATORIO.ToString();
            txtTitulo.Text = rel.TITULO;
            txtOrigem.Text = rel.ARQUIVO; 
            txtExtensao.Text = rel.RELATORIO_EXTENSAO;
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtRelatorio.Text = String.Empty;
            txtTitulo.Text = String.Empty;
            txtOrigem.Text = String.Empty;
            txtExtensao.Text = String.Empty;
            RelatorioBLL RelBLL = new RelatorioBLL();
            CarregaDropDowList(ddlTipo, RelBLL.GetRelatorioTipos().ToList<Object>(), "NM_TIPO", "ID_TIPO_RELATORIO");
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
            btnInserirParam.Enabled = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos()){
                RelatorioBLL RelBLL = new RelatorioBLL();
                FUN_TBL_RELATORIO uptRel = new FUN_TBL_RELATORIO();
                Resultado res = new Resultado();

                uptRel.RELATORIO = txtRelatorio.Text;
                uptRel.TITULO = txtTitulo.Text;
                uptRel.ARQUIVO = txtOrigem.Text;
                uptRel.ID_TIPO_RELATORIO = short.Parse(ddlTipo.SelectedValue);
                uptRel.RELATORIO_EXTENSAO = txtExtensao.Text;

                if (!String.IsNullOrEmpty(txtCodigo.Text))
                {
                    uptRel.ID_RELATORIO = int.Parse(txtCodigo.Text);
                    res = RelBLL.UpdateData(uptRel);
                }
                else
                {
                    uptRel.ID_RELATORIO = 0;
                    res = RelBLL.InsertData(uptRel);
                }

                if (res.Ok)
                {
                    txtCodigo.Text = uptRel.ID_RELATORIO.ToString();
                    btnInserirParam.Enabled = true;
                    grdRelatorio.DataBind();
                }

                MostraMensagem(spanMensagemDetalhe, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");

            }

        }

        private bool ValidarCampos()
        {
            if (ddlTipo.SelectedValue == "0")
            {
                MostraMensagem(spanMensagemDetalhe, "Selecione o tipo de relatório");
                return false;
            }
            return true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            pnlLista.Visible = true;
            pnlDetalhe.Visible = false;
        }

        protected void grdParametro_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdParametro.EditIndex = e.NewEditIndex;            
            int CodRelatorioParam = Int32.Parse(grdParametro.DataKeys[e.NewEditIndex].Value.ToString());
            CarregarParametrosRelatorio(CodRelatorioParam);
            pnlLista.Visible = false;
            pnlDetalhe.Visible = false;
            pnlParametro.Visible = true;
            e.Cancel = true;
        }

        private void CarregarParametrosRelatorio(int CodRelatorioParam)
        {
            RelatorioBLL RelBLL = new RelatorioBLL();            
            FUN_TBL_RELATORIO_PARAM param = RelBLL.ConsultarParametro(CodRelatorioParam);
            //FUN_TBL_RELATORIO rel = RelBLL.ConsultarRelatorio(int.Parse(param.ID_RELATORIO.ToString()));
            //CarregaDropDowList(ddlTipoParam, RelBLL.GetParametrosTipos().ToList<Object>(), "Text", "Value");
            txtRelatorioParam.Text = txtTitulo.Text;
            txtCodigoParam.Text = param.ID_RELATORIO_PARAMETRO.ToString();
            txtOrdem.Text = param.ORDEM.ToString();
            ddlStatus.SelectedValue = param.HABILITADO;
            txtParametro.Text = param.PARAMETRO;
            txtDescricao.Text = param.DESCRICAO;
            ddlTipoParam.SelectedValue = param.TIPO;
            ddlComponente.SelectedValue = param.COMPONENTE_WEB;
            txtConsultaDropdownList.Text = param.DROPDOWLIST_CONSULTA;
            chkPermiteNulo.Checked = (param.PERMITE_NULL == "S");
            chkVisivel.Checked = (param.VISIVEL == "S");
        }

        protected void btnInserirParam_Click(object sender, EventArgs e)
        {
            txtRelatorioParam.Text = txtTitulo.Text;
            txtCodigoParam.Text = "";
            txtOrdem.Text = "";
            ddlStatus.SelectedValue = "S";
            txtParametro.Text = "";
            txtDescricao.Text = "";
            ddlTipoParam.SelectedValue = "StringField";
            ddlComponente.SelectedValue = "TextBox";
            txtConsultaDropdownList.Text = "";
            txtValorInicial.Text = "";
            chkPermiteNulo.Checked = false;
            chkVisivel.Checked = true;
            pnlLista.Visible = false;
            pnlDetalhe.Visible = false;
            pnlParametro.Visible = true;
        }

        protected void btnParamSalvar_Click(object sender, EventArgs e)
        {
            //if (ValidarCamposParam())
            RelatorioBLL RelBLL = new RelatorioBLL();
            FUN_TBL_RELATORIO_PARAM uptParam = new FUN_TBL_RELATORIO_PARAM();
            Resultado res = new Resultado();

            //ID_RELATORIO_PARAMETRO 
            uptParam.ID_RELATORIO = int.Parse(txtCodigo.Text);
            uptParam.PARAMETRO = txtParametro.Text;
            uptParam.DESCRICAO = txtDescricao.Text;
            uptParam.TIPO = ddlTipoParam.SelectedValue; 
            uptParam.COMPONENTE_WEB = ddlComponente.SelectedValue; 
            uptParam.DROPDOWLIST_CONSULTA = txtConsultaDropdownList.Text;
            uptParam.VALOR_INICIAL = txtValorInicial.Text;
            uptParam.HABILITADO = ddlStatus.SelectedValue;
            uptParam.VISIVEL = (chkVisivel.Checked ? "S" : "N");
            uptParam.PERMITE_NULL = (chkPermiteNulo.Checked ? "S" : "N");
            uptParam.ORDEM = int.Parse(txtOrdem.Text);

            if (!String.IsNullOrEmpty(txtCodigoParam.Text))
            {
                uptParam.ID_RELATORIO_PARAMETRO = int.Parse(txtCodigoParam.Text);
                res = RelBLL.UpdateParam(uptParam);
            }
            else
            {
                uptParam.ID_RELATORIO_PARAMETRO = 0;
                res = RelBLL.InsertParam(uptParam);
            }

            if (res.Ok)
            {
                txtCodigoParam.Text = uptParam.ID_RELATORIO_PARAMETRO.ToString();
                grdParametro.DataBind();
                btnParamVoltar_Click(sender, e);
            }

            MostraMensagem(spanMensagemParametro, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");


        }

        //private bool ValidarCamposParam()
        //{
        //    if (ddlTipo.SelectedValue == "0")
        //    {
        //        MostraMensagem(spanMensagemDetalhe, "Selecione o tipo de relatório");
        //        return false;
        //    }
        //    return true;
        //}

        protected void btnParamVoltar_Click(object sender, EventArgs e)
        {
            pnlLista.Visible = false;
            pnlDetalhe.Visible = true;
            pnlParametro.Visible = false;
        }        

    }
}