using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Entidades.Cartas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Web
{
    public partial class DevolucaoProposta : BasePage
    {
        #region Atributos
        DevolucaoPropostaBLL obj = new DevolucaoPropostaBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTela();
            }
        }

        protected void btnSalvarCad_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.matricula = user.login;
                    obj.tipo_doc = (int)tipoDocumento.Devolucao;
                    if (!txtRegistro.Text.Equals(""))
                    obj.registro = int.Parse(txtRegistro.Text);
                    if (!txtEmpresa.Text.Equals(""))
                        obj.cod_emprs = int.Parse(txtEmpresa.Text);
                    obj.nome = txtNome.Text;
                    if (!txtDevolucao.Text.Equals(""))
                        obj.dt_devolucao = DateTime.Parse(txtDevolucao.Text);
                    obj.destinatario = txtDestinatario.Text;
                    obj.desc_motivo_dev = txtMotDevolucao.Text;
                    string msg = "";
                    bool isupdate = false;

                    if (!hdId.Value.Equals("0"))
                    {
                        obj.id_pradprev = int.Parse(hdId.Value);
                        isupdate = true;
                    }


                    bool ret = obj.ValidaCampos(out msg, isupdate);

                    if (ret)
                    {
                        if (!isupdate)
                        {
                            LimpaCampos();
                        }
                    }

                    MostraMensagemTelaUpdatePanel(upDevolucao, "Atenção \\n\\n" + msg);
                }
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upDevolucao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }


        }

        protected void grdDevolucao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdDevolucao"] != null)
            {
                grdDevolucao.PageIndex = e.NewPageIndex;
                grdDevolucao.DataSource = ViewState["grdDevolucao"];
                grdDevolucao.DataBind();
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            CarregaTela();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPesquisa.Text))
            {
                obj.registro = int.Parse(txtPesquisa.Text);

                CarregaGrid("grdDevolucao", obj.ListarDevolucao(), grdDevolucao);
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upDevolucao, "Informe um registro para pesquisa.");
            }
        }

        protected void lnkInserir_Click(object sender, EventArgs e)
        {
            DivAcao(divAction, divSelect);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            CarregaTela();
            LimpaCampos();
            hdId.Value = "0";
            DivAcao(divSelect, divAction);
        }

        protected void grdDevolucao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            string msg = "";
            switch (e.CommandName)
            {
                case "Alterar":

                    hdId.Value = id.ToString();
                    obj.id_pradprev = id;
                    DataTable dt = obj.ListarDevolucao();
                    if (dt.Rows.Count > 0)
                    {
                        obj.id_pradprev = id;
                        txtDestinatario.Text = dt.Rows[0]["DESTINATARIO"].ToString();
                        txtDevolucao.Text = DateTime.Parse(dt.Rows[0]["DT_DEVOLUCAO"].ToString()).ToString("dd/MM/yyyy");
                        txtEmpresa.Text = dt.Rows[0]["COD_EMPRS"].ToString();
                        txtMotDevolucao.Text = dt.Rows[0]["DESC_MOTIVO_DEV"].ToString();
                        txtNome.Text = dt.Rows[0]["NOM_PARTICIP"].ToString();
                        txtRegistro.Text = dt.Rows[0]["REGISTRO"].ToString();
                    }

                    DivAcao(divAction, divSelect);
                    break;

                case "Deletar":

                    try
                    {
                        obj.id_pradprev = id;
                        bool ret = obj.DeletarDevolucao(out msg);
                        if (ret)
                        {

                            obj.id_pradprev = null;
                            MostraMensagemTelaUpdatePanel(upDevolucao, msg);
                            CarregaGrid("grdDevolucao", obj.ListarDevolucao(), grdDevolucao);
                        }
                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upDevolucao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Métodos

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

            txtDestinatario.Text = "";
            txtDevolucao.Text = "";
            txtEmpresa.Text = "";
            txtMotDevolucao.Text = "";
            txtNome.Text = "";
            txtRegistro.Text = "";
        }

        private void CarregaTela()
        {

            txtDevolucao.Text = DateTime.Now.ToShortDateString();
            CarregaGrid("grdDevolucao", obj.ListarDevolucao(), grdDevolucao);
        }
        #endregion
    }
}