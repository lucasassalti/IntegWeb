using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class ImportVr : BasePage
    {
        #region Atributos
        AcaoJudicialBLL obj = new AcaoJudicialBLL();
        string mensagem = "";

        #endregion

        #region Métodos
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CarregaTela();
            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, divInsert);
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtCodMatricula.Equals("") && !txtCodEmpresa.Equals(""))
                {

                    obj.cod_emprs = int.Parse(txtCodEmpresa.Text);
                    obj.matricula = int.Parse(txtCodMatricula.Text);

                    DataTable dt = obj.CarregaProcessoVr();

                    HdEmpresa.Value = txtCodEmpresa.Text;
                    hdMatricula.Value = txtCodMatricula.Text;

                    if (dt.Rows.Count > 0)
                    {

                        divParticip.Visible = true;
                        TxtNom.Text = dt.Rows[0]["nom_emprg"].ToString();
                        hdNumPartif.Value = dt.Rows[0]["num_matr_partf"].ToString();

                        CarregaGrid("grdSrc", dt, grdSrc);

                    }
                    else
                    {
                        divParticip.Visible = false;
                        MostraMensagemTelaUpdatePanel(upImport, "Nenhum Registro encontrado!");
                    }

                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upImport, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            divParticip.Visible = false;
            txtCodMatricula.Text = "";
            txtCodEmpresa.Text = "";

        }

        protected void grdSrc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdSrc"] != null)
            {
                grdSrc.PageIndex = e.NewPageIndex;
                grdSrc.DataSource = ViewState["grdSrc"];
                grdSrc.DataBind();
            }
        }

        protected void grdSrc_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {

                case "Importar":

                    hdSeq.Value = e.CommandArgument.ToString();
                    obj.num_seq_prc = int.Parse(e.CommandArgument.ToString());
                    obj.num_matr_partf = int.Parse(hdNumPartif.Value);
                    if (obj.VerificaVrDados())
                    {
                        DivAcao(divInsert, divSelect);
                        
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upImport, "Atenção \\n\\nO sequêncial foi importado anteriormente.\\n\\n");

                    break;

                case "Desfazer":
                       obj.num_seq_prc = int.Parse(e.CommandArgument.ToString());
                       obj.num_matr_partf = int.Parse(hdNumPartif.Value);

                       if (obj.VerificaVrDados())
                       {
                           MostraMensagemTelaUpdatePanel(upImport, "Atenção \\n\\nO sequêncial não foi importado para VR.\\n\\n");
                       }
                       else {

                           if (obj.DeletarVr(out mensagem))
                           {
                               obj.cod_emprs = int.Parse(HdEmpresa.Value);
                               obj.matricula = int.Parse(hdMatricula.Value);

                               DataTable dt = obj.CarregaProcessoVr();

                               CarregaGrid("grdSrc", dt, grdSrc);

                           }

                           MostraMensagemTelaUpdatePanel(upImport, mensagem);
                       }

                   

                    break;
                default:
                    break;
            }

        }

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerificaTipoaAtualizao())
                {
                    if (ValidaHiddenfield())
                    {
                        obj.num_matr_partf = int.Parse(hdNumPartif.Value);
                        obj.num_seq_prc = int.Parse(hdSeq.Value);

                       bool ret= obj.ImportacaoVr(out mensagem);

                       if (ret)
                       {
                           ckTipImportacao.ClearSelection();
                           MostraMensagemTelaUpdatePanel(upImport, mensagem);
                       }
                       else
                           MostraMensagemTelaUpdatePanel(upImport, "Atenção \\n\\nO sequêncial foi importado anteriormente.\\n\\n");

                       
                    }
               
                    
                }
                else
                    MostraMensagemTelaUpdatePanel(upImport, "Selecione uma opção de importação para continuar.");
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upImport, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        #endregion

        #region Métodos

        private bool VerificaTipoaAtualizao()
        {

            List<String> roles = new List<string>();

            for (int i = 0; i < ckTipImportacao.Items.Count; i++)
            {
                if (ckTipImportacao.Items[i].Selected)
                {
                    if (ckTipImportacao.Items[i].Value == "I")
                        obj.mrc_atlz_igpdi = "S";
                    else if (ckTipImportacao.Items[i].Value == "T")
                        obj.mrc_atlz_trab = "S";
                    else
                        obj.mrc_catlz_civ = "S";

                    roles.Add(ckTipImportacao.Items[i].Value);
                }
            }

            return roles.Count > 0;

        }

        private bool ValidaHiddenfield()
        {
            return (!hdNumPartif.Value.Equals("0") && !hdSeq.Value.Equals("0"));
        }
        private void CarregaTela()
        {

            DivAcao(divSelect, divInsert);
        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }


        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        #endregion

    }
}