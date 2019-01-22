using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class CoefPensao : BasePage
    {
        #region Atributos
        CoeficientePensaoBLL objCoe = new CoeficientePensaoBLL();
        AcaoJudicialBLL obj = new AcaoJudicialBLL();
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
                            objCoe.num_idntf_rptant = int.Parse(commandArgs[1]);
                            objCoe.ano_de = int.Parse(commandArgs[2]);

                            bool ret = objCoe.DeletarRegistro(out mensagem);

                            if (ret)
                            {
                                objCoe = new CoeficientePensaoBLL();
                                CarregaTela();
                            }
                            MostraMensagemTelaUpdatePanel(update, mensagem);
                            btnLimpar_Click(null, null);
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
            txtCodMatricula.Text = "";
            txtCodEmpresa.Text = "";
            grd.EditIndex = -1;
            grd.PageIndex = 0;
            grd.DataBind();
            CarregaTela();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAnoAte.Text))
                    objCoe.ano_ate = int.Parse(txtAnoAte.Text);

                if (!string.IsNullOrEmpty(txtAnoDe.Text))
                    objCoe.ano_de = int.Parse(txtAnoDe.Text);

                if (!string.IsNullOrEmpty(txtCoeficiente.Text))
                    objCoe.coef_pens_mes = decimal.Parse(txtCoeficiente.Text);

                if (!string.IsNullOrEmpty(txtMesAte.Text))
                    objCoe.mes_ate = int.Parse(txtMesAte.Text);

                if (!string.IsNullOrEmpty(txtMesDe.Text))
                    objCoe.mes_de = int.Parse(txtMesDe.Text);

                if (!string.IsNullOrEmpty(txtNumMatr.Text))
                    objCoe.num_matr_partf = int.Parse(txtNumMatr.Text);

                if (!string.IsNullOrEmpty(txtRepresentante.Text))
                    objCoe.num_idntf_rptant = int.Parse(txtRepresentante.Text);


                if (objCoe.InserirRegistro(out mensagem))
                {
                    txtAnoAte.Text = "";
                    txtAnoDe.Text = "";
                    txtMesAte.Text = "";
                    txtCoeficiente.Text = "";
                    txtMesDe.Text = "";
                    txtNumMatr.Text = "";
                    txtRepresentante.Text = "";
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
                    MostraMensagemTelaUpdatePanel(update, "Nenhum Registro encontrado!");
                    btnLimpar_Click(null, null);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtCodMatricula.Text) && string.IsNullOrEmpty(txtCodEmpresa.Text))
                {
                    MostraMensagemTelaUpdatePanel(update, "Os campos não podem estar vazios para pesquisar!");
                }
                else if (string.IsNullOrEmpty(txtCodMatricula.Text) && !string.IsNullOrEmpty(txtCodEmpresa.Text))
                {
                    MostraMensagemTelaUpdatePanel(update, "Campo de matricula obrigatório para pesquisa!");
                }
                else if (!string.IsNullOrEmpty(txtCodMatricula.Text) && string.IsNullOrEmpty(txtCodEmpresa.Text))
                {
                    MostraMensagemTelaUpdatePanel(update, "Campo de empresa obrigatório para pesquisa!");
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

        private void CarregaTela() { CarregaGrid("grd", objCoe.CarregarLista(), grd); }

        #endregion
    }
}