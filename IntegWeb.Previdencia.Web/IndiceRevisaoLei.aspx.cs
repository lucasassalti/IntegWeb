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
    public partial class IndiceRevisaoLei : BasePage
    {
        #region Atributos
        IndiceRevisaoBLL objCoe = new IndiceRevisaoBLL();
        AcaoJudicialBLL obj = new AcaoJudicialBLL();
        RevisaoLeiBLL rev = new RevisaoLeiBLL();
        string mensagem = "";
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTela();
            }
          
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

                            objCoe.num_matr_partf = int.Parse(commandArgs[0]);
                            objCoe.num_lei = int.Parse(commandArgs[1]);

                            bool ret = objCoe.DeletarRegistro(out mensagem);

                            if (ret)
                            {
                                objCoe = new IndiceRevisaoBLL();
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
            txtCodMatricula.Text = "";
            txtCodEmpresa.Text = "";

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumMatr.Text))
                    objCoe.mes_lei = int.Parse(txtMeslei.Text);

                if (!string.IsNullOrEmpty(txtMeslei.Text))
                    objCoe.ano_lei = int.Parse(txtAnoLei.Text);

                if (!string.IsNullOrEmpty(drpNumLei.SelectedValue))
                    objCoe.num_lei = int.Parse(drpNumLei.SelectedValue);

                if (!string.IsNullOrEmpty(txtIndiceLei.Text))
                    objCoe.ind_lei = decimal.Parse(txtIndiceLei.Text);

                if (!string.IsNullOrEmpty(txtNumMatr.Text))
                    objCoe.num_matr_partf = int.Parse(txtNumMatr.Text);



                if (objCoe.InserirRegistro(out mensagem))
                {
                    txtNumMatr.Text = "";
                    txtMeslei.Text = "";
                    txtAnoLei.Text = "";
                    drpNumLei.SelectedIndex = 0;
                    txtIndiceLei.Text = "";

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
            if (!string.IsNullOrEmpty(txtCodMatricula.Text) && !string.IsNullOrEmpty(txtCodEmpresa.Text))
            {

                obj.cod_emprs = int.Parse(txtCodEmpresa.Text);
                obj.matricula = int.Parse(txtCodMatricula.Text);

                DataTable dt = obj.CarregaParticipante();

                HdEmpresa.Value = txtCodEmpresa.Text;
                hdMatricula.Value = txtCodMatricula.Text;

                if (dt.Rows.Count > 0)
                {

                    divParticip.Visible = true;

                    hdNumPartif.Value = dt.Rows[0]["num_matr_partf"].ToString();

                    objCoe.num_matr_partf = int.Parse(dt.Rows[0]["num_matr_partf"].ToString());

                    CarregaGrid("grd", objCoe.CarregarLista(), grd);

                }
                else
                {
                    divParticip.Visible = false;
                    MostraMensagemTelaUpdatePanel(update, "Nenhum Registro encontrado!");
                }

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

        private void CarregaTela()
        {
            CarregaGrid("grd", objCoe.CarregarLista(), grd);
            CarregaDropDowDT(rev.CarregarLista(), drpNumLei);

        }

        #endregion
    }
}