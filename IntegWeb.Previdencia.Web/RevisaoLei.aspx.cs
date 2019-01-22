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
    public partial class RevisaoLei : BasePage
    {
        #region Atributos
        RevisaoLeiBLL objCoe = new RevisaoLeiBLL();
        AcaoJudicialBLL obj = new AcaoJudicialBLL();
        string mensagem = "";
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaTela();
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs;

            switch (e.CommandName)
            {

                case "Deletar":

                    try
                    {
                        if (!hdNumPartif.Value.Equals("0"))
                        {
                            commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                            objCoe.num_lei = int.Parse(commandArgs[0]);

                            bool ret = objCoe.DeletarRegistro(out mensagem);

                            if (ret)
                            {
                                objCoe = new RevisaoLeiBLL();
                                CarregaTela();
                            }
                            MostraMensagemTelaUpdatePanel(update, mensagem);

                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(update, "Problemas contate o administrador do sistema ");
                        }

                    }
                    catch (Exception ex)
                    {

                        MostraMensagemTelaUpdatePanel(update, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
                    }



                    break;
                default:
                    break;
            }
        }


        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grd"] != null)
            {
                grd.PageIndex = e.NewPageIndex;
                grd.DataSource = ViewState["grd"];
                grd.DataBind();
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            divParticip.Visible = false;
            txtNumeroLei.Text = "";


        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDtIni.Text))
                    objCoe.data_inic_vig = DateTime.Parse(txtDtIni.Text);

                if (!string.IsNullOrEmpty(txtDtFim.Text))
                    objCoe.data_fim_vig = DateTime.Parse(txtDtFim.Text);

                if (!string.IsNullOrEmpty(txtNumLei.Text))
                    objCoe.num_lei = int.Parse(txtNumLei.Text);


                if (!string.IsNullOrEmpty(txtDscLei.Text))
                    objCoe.dsc_lei = txtDscLei.Text;




                if (objCoe.InserirRegistro(out mensagem))
                {

                    txtDtFim.Text = "";
                    txtDtIni.Text = "";
                    txtNumLei.Text = "";
                    txtDscLei.Text = "";
                }

                MostraMensagemTelaUpdatePanel(update, mensagem);

            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(update, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            DivAcao(divInsert, divSelect);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, divInsert);
            CarregaGrid("grd", objCoe.CarregarLista(), grd);
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroLei.Text))
            {



                divParticip.Visible = true;

                objCoe.num_lei = int.Parse(txtNumeroLei.Text);

                CarregaGrid("grd", objCoe.CarregarLista(), grd);

            }
            else
            {
                divParticip.Visible = false;
                MostraMensagemTelaUpdatePanel(update, "Nenhum Registro encontrado!");
            }


        }

        #endregion

        #region Metodos
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

        private void CarregaTela() { CarregaGrid("grd", objCoe.CarregarLista(), grd); }

        #endregion
    }
}