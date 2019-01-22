using IntegWeb.Administracao.Aplicacao.BLL;
using IntegWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;

namespace IntegWeb.Administracao.Web
{
    public partial class UsuarioTela : System.Web.UI.Page
    {
        #region Atributos
        BasePage objB = new BasePage();
        UsuarioSistema objU = new UsuarioSistema();
        UsuarioHistorico objUH = new UsuarioHistorico();
        UsuarioBLL objBll = new UsuarioBLL();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaCamposTela();

            }
        }

        #region Tela Importar Usuário Ad

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string msg;
            bool isValid = ValidaCamposTela(txtUsuario, drpUsuario, out msg);

            if (!isValid)
            {

                if (drpUsuario.SelectedValue == "1")
                    objU.login = txtUsuario.Text;
                else if (drpUsuario.SelectedValue == "2")
                    objU.nome = txtUsuario.Text;
                else if (drpUsuario.SelectedValue == "3")
                    objU.email = txtUsuario.Text;
                else
                    objU.departamento = txtUsuario.Text;

                CarregaGrid("grdUsuario", new UsuarioBLL().ConsultarUsuario(objU).ToList<Object>(), grdUsuario);
            }
            else
                objB.MostraMensagemTelaUpdatePanel(upUsuarioAd, msg);
        }

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "";
                bool isImport = new UsuarioBLL().InsereUsuarioAd(out msg);
                if (isImport)
                {
                    CarregaGrid("grdUsuario", new UsuarioBLL().ConsultarUsuario(new UsuarioSistema()).ToList<Object>(), grdUsuario);
                }

                objB.MostraMensagemTelaUpdatePanel(upUsuarioAd, msg);
            }
            catch (Exception ex)
            {

                objB.MostraMensagemTelaUpdatePanel(upUsuarioAd, "Atenção!\\n\\nNão foi possível concluir a operação.\\n\\nMotivo:\\n\\n" + ex.Message);
            }
           

        }

        protected void grdUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdUsuario"] != null)
            {
                grdUsuario.PageIndex = e.NewPageIndex;
                grdUsuario.DataSource = ViewState["grdUsuario"];
                grdUsuario.DataBind();
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            drpUsuario.SelectedIndex = 0;
            CarregaGrid("grdUsuario", new UsuarioBLL().ConsultarUsuario(new UsuarioSistema()).ToList<Object>(), grdUsuario);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, divAction);
            txtJustificativa.Text = "";
        }

        protected void grdUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "STATUS":
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                    lbStatus.Text = commandArgs[0];
                    lbUsuario.Text = commandArgs[1];
                    hdUsuario.Value = commandArgs[2];
                    DivAcao(divAction, divSelect);
                    break;
                default:
                    break;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Session["objUser"] != null)
            {
                string msg = "";
                objU = (ConectaAD)Session["objUser"];
                objUH.ds_justitificativa = txtJustificativa.Text;
                objUH.login_aplicacao = objU.login;
                objUH.login = hdUsuario.Value;
                objUH.id_status = lbStatus.Text == "INATIVO" ? 1 : 0;
                bool ret = objBll.InativarUsuario(objUH, out msg);

                objB.MostraMensagemTelaUpdatePanel(upUsuarioAd, msg);

                if (ret)
                {
                    DivAcao(divSelect, divAction);
                    txtJustificativa.Text = "";
                    CarregaGrid("grdUsuario", new UsuarioBLL().ConsultarUsuario(new UsuarioSistema()).ToList<Object>(), grdUsuario);
                }
            }
        }

        #endregion

        #region Tela Historico
        protected void lnkHistorico_Click(object sender, EventArgs e)
        {
            grdHistorico.Visible = false;
            drpHistUser.SelectedValue = "0";
            DivAcao(divHistorico, divMovimentacao);

        }

        protected void btnHistVol_Click(object sender, EventArgs e)
        {
            DivAcao(divMovimentacao, divHistorico);
        }

        protected void btnHistPes_Click(object sender, EventArgs e)
        {
            if (drpHistUser.SelectedIndex > 0)
            {
                objU.login = drpHistUser.SelectedValue;
                grdHistorico.Visible = true;
                CarregaGrid("grdHistorico", new UsuarioBLL().ConsultarMovimentacao(objU).ToList<Object>(), grdHistorico);
                btnHistExcel.Visible = grdHistorico.Rows.Count > 0;

            }
            else
            {

                objB.MostraMensagemTelaUpdatePanel(upUsuarioAd, "Selecione um usuário para continuar!!");

            }
        }

        protected void grdHistorico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdHistorico"] != null)
            {
                grdUsuario.PageIndex = e.NewPageIndex;
                grdUsuario.DataSource = ViewState["grdHistorico"];
                grdUsuario.DataBind();
            }
        }

        protected void btnHistExcel_Click(object sender, EventArgs e)
        {
            if (grdHistorico.Rows.Count > 0)
            {
                var nomeArquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_Historico_USUARIOS" + ".xls";
                objB.ExportarExcel(nomeArquivo, grdHistorico);
            }
            else
                objB.MostraMensagemTela(this, "Problema encontrado ao tentar exportar a Planilha.");
        }

        protected void drpHistUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdHistorico.Visible = false;
            btnHistExcel.Visible = false;
        }

        #endregion

        #endregion

        #region Métodos

        private bool ValidaCamposTela(TextBox text, DropDownList drp, out string mensagem)
        {

            StringBuilder msg = new StringBuilder();
            msg.Append("ERRO!\\n");
            bool isErro = false;
            if (drp.SelectedIndex < 1)
            {
                msg.Append("1 Selecione uma opção de busca.\\n");
                isErro = true;
            }

            if (text.Text == "")
            {
                msg.Append("2 Digite no campo de busca! ");
                isErro = true;
            }
            mensagem = msg.ToString();
            return isErro;
        }

        private void CarregaGrid(string nameView, List<Object> list, GridView grid)
        {

            ViewState[nameView] = list;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }
               
        private void CarregaCamposTela()
        {

            DivAcao(divMovimentacao, divHistorico);
            DivAcao(divSelect, divAction);
            txtJustificativa.Text = "";
            List<UsuarioSistema> list = new UsuarioBLL().ConsultarUsuario(new UsuarioSistema());
            CarregaGrid("grdUsuario", list.ToList<Object>(), grdUsuario);
            CarregaDropDown(drpHistUser, list.OrderBy(c => c.nome).OrderBy(d => d.nome).ToList<Object>());
        }

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        private void CarregaDropDown(DropDownList dropdownList, List<Object> list)
        {

            dropdownList.DataSource = list;
            dropdownList.DataTextField = "nome";
            dropdownList.DataValueField = "login";
            dropdownList.DataBind();

            dropdownList.Items.Insert(0, new ListItem("---Selecione---", "0"));


        }

        #endregion

        
    }
}