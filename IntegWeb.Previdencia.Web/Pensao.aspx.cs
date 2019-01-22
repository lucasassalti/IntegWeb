using IntegWeb.Entidades.Previdencia;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL;
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
    public partial class Pensao : BasePage
    {
        #region Atributos
        PensaoBLL obj = new PensaoBLL();
        PercentualPensao per = new PercentualPensao();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DivAcao(divSelect, divAction);
            }
        }

        protected void txtCodRepresentante_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCodRepresentante.Text != "")
                {
                    long num = 0;
                    if (Int64.TryParse(txtCodRepresentante.Text.ToString(), out num))
                    {

                        per.num_idntf_rptant = num;
                        DataTable dt = obj.ListaReprsentante(per);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            lblRepresentante.Visible = true;
                            lblRepresentante.Text = row["NOM_REPRES"].ToString();
                            lblDigito.Text = row["NUM_DIGVR_EMPRG"].ToString();
                            lblEmpresa.Text = row["COD_EMPRS"].ToString();
                            lblMatricula.Text = row["NUM_RGTRO_EMPRG"].ToString();

                            DivAcao(divComDados, divSemDados);
                        }
                        else
                        {
                            DivAcao(divSemDados, divComDados);
                            txtCodRepresentante.Text = "";
                        }
                    }
                    else
                    {
                        txtCodRepresentante.Text = "";
                        MostraMensagemTelaUpdatePanel(upAcao, "Digite apenas números!");
                    }
                }
            }
            catch (Exception ex)
            {
                txtCodRepresentante.Text = "";
                MostraMensagemTelaUpdatePanel(upAcao, ex.Message);
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!txtRepres.Text.Equals(""))
            {
                per.num_idntf_rptant = Int64.Parse(txtRepres.Text);
                Session["id"] = per.num_idntf_rptant;
                DataTable dt = obj.ListaPensao(per);
                ViewState["grdPensao"] = dt;
                grdPensao.EditIndex = -1;
                CarregarGridView(grdPensao, dt);

            }
            else
                MostraMensagemTelaUpdatePanel(upAcao, "Informe o representante para continuar");
        }

        protected void grdPensao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdPensao"] != null)
            {
                grdPensao.PageIndex = e.NewPageIndex;
                grdPensao.DataSource = ViewState["grdPensao"];
                grdPensao.DataBind();
            }
        }

        protected void grdPensao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdPensao.EditIndex = -1;
            BindGrid();
        }

        protected void grdPensao_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var objuser = (ConectaAD)Session["objUser"];
                    var index = e.RowIndex;

                    var NUM_IDNTF_RPTANT = grdPensao.DataKeys[index].Values["NUM_IDNTF_RPTANT"].ToString();
                    var DAT_VALIDADE = grdPensao.DataKeys[index].Values["DAT_VALIDADE"].ToString();

                    TextBox txtP = grdPensao.Rows[index].FindControl("txtPenDiv") as TextBox;
                    TextBox txtC = grdPensao.Rows[index].FindControl("txtTotalPerc") as TextBox;
                    var user = objuser.login;

                    per.num_idntf_rptant = Int64.Parse(NUM_IDNTF_RPTANT);
                    per.dat_validade = DateTime.Parse(DAT_VALIDADE);
                    per.matricula = user;
                    string mensagem = "";

                    bool ret = new PensaoBLL().Deletar(out mensagem, per);
                    MostraMensagemTelaUpdatePanel(upAcao, mensagem);
                    DivAcao(divSelect, divAction);
                    BindGrid();
                }

            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upAcao, "Atenção!\\n\\nNão foi possível concluir a operação.\\n\\nMotivo:\\n\\n" + ex.Message);
            }
        }

        protected void grdPensao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdPensao.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void grdPensao_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {
                if (Session["objUser"] != null)
                {
                    var objuser = (ConectaAD)Session["objUser"];

                    var user = objuser.login;
                    bool ret;
                    string mensagem = "";
                    var index = e.RowIndex;

                    var NUM_IDNTF_RPTANT = grdPensao.DataKeys[index].Values["NUM_IDNTF_RPTANT"].ToString();
                    var DAT_VALIDADE = grdPensao.DataKeys[index].Values["DAT_VALIDADE"].ToString();

                    TextBox txtP = grdPensao.Rows[index].FindControl("txtPenDiv") as TextBox;
                    TextBox txtC = grdPensao.Rows[index].FindControl("txtTotalPerc") as TextBox;

                    per.num_idntf_rptant = Int64.Parse(NUM_IDNTF_RPTANT);
                    per.pct_pensao_dividida = decimal.Parse(txtP.Text);
                    per.pct_pensao_total = decimal.Parse(txtC.Text);
                    per.dat_validade = DateTime.Parse(DAT_VALIDADE);
                    per.matricula = user;

                    ret = obj.ValidaCampos(out mensagem, per, true);
                    MostraMensagemTelaUpdatePanel(upAcao, mensagem);
                    DivAcao(divAction, divSelect);

                    if (ret)
                    {
                        grdPensao.EditIndex = -1;
                        BindGrid();
                    }

                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upAcao, "Atenção!\\n\\nNão foi possível concluir a operação.\\n\\nMotivo:\\n\\n" + ex.Message);
            }

        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            DivAcao(divAction, divSelect);
            LimpaCampos();
            divSemDados.Visible = false;
            divComDados.Visible = false;
            grdPensao.EditIndex = -1;

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, divAction);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var objuser = (ConectaAD)Session["objUser"];

                    var user = objuser.login;
                    bool ret;
                    string mensagem = "";

                    per.num_idntf_rptant = Int64.Parse(txtCodRepresentante.Text);
                    per.pct_pensao_dividida = decimal.Parse(txtPercentualDivido.Text);
                    per.pct_pensao_total = decimal.Parse(txtPercentualTotal.Text);
                    per.dat_validade = DateTime.Parse(txtDataValidade.Text);
                    per.matricula = user;

                    ret = obj.ValidaCampos(out mensagem, per, false);
                    MostraMensagemTelaUpdatePanel(upAcao, mensagem);
                    DivAcao(divAction, divSelect);

                    if (ret)
                    {
                        divComDados.Visible = false;
                        divSemDados.Visible = false;
                        LimpaCampos();
                    }


                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upAcao, "Atenção!\\n\\nNão foi possível concluir a operação.\\n\\nMotivo:\\n\\n" + ex.Message);
            }

        }

        #endregion

        #region Métodos
        private void BindGrid()
        {
            if (Session["id"] != null)
            {
                DataTable dts = new PensaoBLL().ListaPensao(new PercentualPensao() { num_idntf_rptant = int.Parse(Session["id"].ToString()) });
                CarregaGrid("grdPensao", dts, grdPensao);
                DivAcao(divSelect, divAction);
            }
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
  
        private void LimpaCampos()
        {
            txtCodRepresentante.Text = string.Empty;
            txtDataValidade.Text = string.Empty;
            txtPercentualDivido.Text = string.Empty;
            txtPercentualTotal.Text = string.Empty;
            lblRepresentante.Visible = false;
        }
        #endregion
    }

}