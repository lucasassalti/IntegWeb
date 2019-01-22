using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class ImportaDadosCassi : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ddlFitroTipoMotivo.Items.Insert(0, new ListItem("---Selecione---", ""));
            }
        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            Processo_Mensagem.Visible = false;
            List<ArquivoUpload> _lst_uploads = null;
            int qtqtsLinhas = 0;
            try
            {
                if (Session["lst_uploads"] != null)
                {
                    _lst_uploads = (List<ArquivoUpload>)Session["lst_uploads"];
                    qtqtsLinhas = ImportaArquivos(_lst_uploads);

                    if (qtqtsLinhas > 0)
                    {
                        MostraMensagem(Processo_Mensagem, "Arquivo Carregado com sucesso\\n\\nQuantidade de linhas importados " + qtqtsLinhas + " registros", "n_ok");
                    }
                    else
                    {
                        MostraMensagem(Processo_Mensagem, "Nenhum Registro foi importado", "n_ok");
                    }
                }
                else
                {
                    MostraMensagem(Processo_Mensagem, "Nenhum Arquivo foi selecionado", "n_error");
                }
            }
            catch (Exception ex)
            {
                MostraMensagem(Processo_Mensagem, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
            }
            finally
            {
                FileUploadControl.FileContent.Dispose();
                FileUploadControl.FileContent.Flush();
                if (FileUploadControl.PostedFile != null)
                {
                    FileUploadControl.PostedFile.InputStream.Flush();
                    FileUploadControl.PostedFile.InputStream.Close();
                }
                LimparUploads(_lst_uploads);
            }
        }

        private int ImportaArquivos(List<ArquivoUpload> _lst_uploads)
        {

            Resultado res = new Resultado();
            DataTable qtsDtLinhas;
            int linhasInseridas = 0;

            foreach (var item in _lst_uploads)
            {
                BasePage obj = new BasePage();
                //Lê o Excel e converte para DataSet
                DataTable ds = obj.ReadExcelFile(item.caminho_arquivo, 1, 3);
                DadosCartaoCassiBLL bll = new DadosCartaoCassiBLL();

                bool ret = bll.Importar(ds, out qtsDtLinhas);
                item.processado = true;
                linhasInseridas = +qtsDtLinhas.Rows.Count;
            }

            return linhasInseridas;
        }

        private void LimparUploads(List<ArquivoUpload> _lst_uploads)
        {
            if (_lst_uploads != null && _lst_uploads.Count > 0)
            {
                for (int i = _lst_uploads.Count - 1; i >= 0; i--)
                {
                    if (_lst_uploads[i].processado)
                    {
                        File.Delete(_lst_uploads[i].caminho_arquivo);
                        _lst_uploads.RemoveAt(i);
                    }
                }
                if (_lst_uploads.Count == 0)
                {
                    //chkSobreporTodos.Checked = false;
                    //grdDocsEnviados.DataBind();
                    //grdArqEnviadosPorEmpresa.DataBind();
                    Session["lst_uploads"] = null;
                }
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
            DadosCartaoCassiBLL bll = new DadosCartaoCassiBLL();
            bool ret = bll.DeleteDados();
            if (ret)
            {
                MostraMensagem(Processo_Mensagem, "Tabela Limpa", "n_ok");
            }
        }

        private void limpar()
        {

            Processo_MensagemPesquisar.Text = "";
            txtNome.Text = "";
            txtMatricula.Text = "";
        }

        protected void btnLimparPesquisa_Click(object sender, EventArgs e)
        {
            limpar();

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNome.Text) && String.IsNullOrEmpty(txtMatricula.Text))
            {
                MostraMensagem(Processo_MensagemPesquisar, "Entre com o Nome ou Matrícula para consulta");
                return;
            }

            grdDadosCassi.DataBind();
        }

        #region 'Tab Controle Cassi'

        void verificaConsultaControleCassi()
        {
            DataTable dt = new DataTable();
            DadosCartaoCassiBLL bllCassi = new DadosCartaoCassiBLL();
            dt = bllCassi.selectCassiControle();
            grdControleCassi.DataSource = dt;
            grdControleCassi.DataBind();
        }
        protected void btnPesquisarControleCassi_Click(object sender, EventArgs e)
        {
            pnlFiltro.Visible = true;
            limparFiltro();
            verificaConsultaControleCassi();

        }

        protected void grdControleCassi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DadosCartaoCassiBLL bllCassi = new DadosCartaoCassiBLL();

            if (e.CommandName == "Update")
            {
                DataTable dt = new DataTable();
                CAD_TBL_CONTROLECASSI obj = new CAD_TBL_CONTROLECASSI();
                obj.ID_REG = Convert.ToInt16(((Label)grdControleCassi.Rows[grdControleCassi.EditIndex].FindControl("lblIdReg")).Text);

                if (string.IsNullOrEmpty(((TextBox)grdControleCassi.Rows[grdControleCassi.EditIndex].FindControl("txtDtEnvio")).Text))
                {
                    obj.DT_ENVIO = null;
                }
                else
                {
                    obj.DT_ENVIO = Convert.ToDateTime(((TextBox)grdControleCassi.Rows[grdControleCassi.EditIndex].FindControl("txtDtEnvio")).Text);
                }

                Resultado res = bllCassi.atualizaTabelaControleCassi(obj);

                if (res.Ok)
                {
                    MostraMensagemTela(this, "Registro Atualizado com Sucesso");
                    grdControleCassi.EditIndex = -1;
                    grdControleCassi.ShowFooter = false;
                }
                else
                {
                    MostraMensagemTela(this, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }
            }
        }

        protected void grdControleCassi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdControleCassi.EditIndex = e.NewEditIndex;
            showGridControleCassi();
        }

        protected void grdControleCassi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdControleCassi.EditIndex = -1;
            showGridControleCassi();
        }

        protected void grdControleCassi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            grdControleCassi.EditIndex = -1;
            showGridControleCassi();
        }

        void showGridControleCassi()
        {
            DadosCartaoCassiBLL bllCassi = new DadosCartaoCassiBLL();
            DataView dv = new DataView(bllCassi.selectFiltroCassiControle(txtFiltroEmpresa.Text, txtFiltroMatricula.Text, txtFiltroSub.Text, txtFiltroNomeParticpante.Text, ddlFitroTipoMotivo.SelectedIndex.ToString()));
            grdControleCassi.DataSource = dv;
            grdControleCassi.DataBind();
        }

        protected void grdControleCassi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DadosCartaoCassiBLL bllCassi = new DadosCartaoCassiBLL();
            grdControleCassi.PageIndex = e.NewPageIndex;
            DataView dv = new DataView(bllCassi.selectFiltroCassiControle(txtFiltroEmpresa.Text, txtFiltroMatricula.Text, txtFiltroSub.Text, txtFiltroNomeParticpante.Text, ddlFitroTipoMotivo.SelectedIndex.ToString()));
            grdControleCassi.DataSource = dv;
            grdControleCassi.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            DadosCartaoCassiBLL bllCassi = new DadosCartaoCassiBLL();
            DataView dv = new DataView(bllCassi.selectFiltroCassiControle(txtFiltroEmpresa.Text, txtFiltroMatricula.Text, txtFiltroSub.Text, txtFiltroNomeParticpante.Text, ddlFitroTipoMotivo.SelectedIndex.ToString()));
            grdControleCassi.DataSource = dv;
            grdControleCassi.DataBind();
        }

        void limparFiltro()
        {
            txtFiltroEmpresa.Text = "";
            txtFiltroMatricula.Text = "";
            txtFiltroSub.Text = "";
            txtDtAlteracaoLote.Text = "";
            ddlFitroTipoMotivo.SelectedIndex = 0;
        }

        protected void btnLimparFiltro_Click(object sender, EventArgs e)
        {
            limparFiltro();
            showGridControleCassi();
        }

        protected void btnAlteracaoLote_Click(object sender, EventArgs e)
        {
            DadosCartaoCassiBLL bllCassi = new DadosCartaoCassiBLL();
            CAD_TBL_CONTROLECASSI obj = new CAD_TBL_CONTROLECASSI();

            if (!string.IsNullOrEmpty(txtDtAlteracaoLote.Text))
            {
                foreach (GridViewRow row in grdControleCassi.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSelect = (row.FindControl("chkSelect") as CheckBox);
                        if (chkSelect.Checked)
                        {
                            obj.ID_REG = Convert.ToInt64(((Label)row.FindControl("lblIdReg")).Text);
                            obj.DT_ENVIO = Util.String2Date(txtDtAlteracaoLote.Text);
                            Resultado res = bllCassi.atualizaTabelaControleCassi(obj);

                            if (!res.Ok)
                            {
                                MostraMensagemTela(this, res.Mensagem);
                                grdControleCassi.EditIndex = -1;
                                grdControleCassi.PageIndex = 0;
                                DataView dv = new DataView(bllCassi.selectFiltroCassiControle(txtFiltroEmpresa.Text, txtFiltroMatricula.Text, txtFiltroSub.Text, txtFiltroNomeParticpante.Text, ddlFitroTipoMotivo.SelectedIndex.ToString()));
                                grdControleCassi.DataSource = dv;
                                grdControleCassi.DataBind();
                            }
                        }
                    }
                }

                MostraMensagemTela(this, "Registros Atualizados com Sucesso");
                grdControleCassi.EditIndex = -1;
                grdControleCassi.PageIndex = 0;
                DataView dvAlterado = new DataView(bllCassi.selectFiltroCassiControle(txtFiltroEmpresa.Text, txtFiltroMatricula.Text, txtFiltroSub.Text, txtFiltroNomeParticpante.Text, ddlFitroTipoMotivo.SelectedIndex.ToString()));
                grdControleCassi.DataSource = dvAlterado;
                grdControleCassi.DataBind();

                txtDtAlteracaoLote.Text = "";
            }
            else
            {
                MostraMensagemTela(this, "Preencha um data para efetuar as alterações em Lote");
            }

        }

        #endregion



    }
}