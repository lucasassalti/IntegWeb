using IntegWeb.Entidades.Saude.Auditoria;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Auditoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class ControleCRC : BasePage
    {
        #region Atributos
        PagamentoCRC obj = new PagamentoCRC();
        ControleCRCBLL objB = new ControleCRCBLL();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExel_Click(object sender, EventArgs e)
        {
            try
            {
                string mesangem = ValidaTela();
                if (mesangem.Equals(""))
                {
                    obj.mesano = drpMes.SelectedValue + "/" + txtAno.Text;

                    CarregaGrid("grdCrc", objB.ListaUsuario(obj), grdCrc);
                }
                else
                    MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\n" + mesangem);
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\n" + ex.Message);
            }
        }

        protected void btProcessar_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = objB.ListaUsuario(new PagamentoCRC() );
                if (dt.Rows.Count == 0)
                {

                    ProcessarRelatorio();
                }
                else
                {
                    lblResponsavel.Text = "Responsável: " + dt.Rows[0]["MATRICULA"].ToString();
                    lblData.Text = "Data: " + DateTime.Parse(dt.Rows[0]["DT_INCLUSAO"].ToString()).ToShortDateString();
                    lblHorario.Text = "Horário: " + String.Format("{0:T}", DateTime.Parse(dt.Rows[0]["DT_INCLUSAO"].ToString()));

                    ModalBox(Page, lnkErro.ClientID);
                }
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\n" + ex.Message);
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
           ProcessarRelatorio();
        }

        protected void grdCrc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Exportar":
                    obj.dt_inclusao = DateTime.Parse(e.CommandArgument.ToString());
                    ConsultarVidas(obj);

                    break;
                default:
                    break;
            }
        }

        protected void grdCrc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdCrc"] != null)
            {
                grdCrc.PageIndex = e.NewPageIndex;
                grdCrc.DataSource = ViewState["grdCrc"];
                grdCrc.DataBind();
            }
        }

        #endregion

        #region  Métodos
        private string ValidaTela()
        {

            StringBuilder str = new StringBuilder();
            int ano = 0;

            if (drpMes.SelectedValue == "0")
            {
                str.Append("Selecione um Mês.\\n");
            }


            if (!txtAno.Text.Equals(""))
            {
                if (int.TryParse(txtAno.Text, out ano))
                {
                    if (ano < 1000)
                    {
                        txtAno.Text = "";
                        str.Append("Digite o ano no formato (YYYY).\\n");
                    }
                }
                else
                {
                    txtAno.Text = "";
                    str.Append("Digite apenas números.\\n");
                }
            }
            else
            {

                str.Append("Digite o Ano.\\n");

            }

            return str.ToString();

        }

        private void ProcessarRelatorio()
        {
            bool ret = false;

            if (Session["objUser"] != null)
            {
                var objuser = (ConectaAD)Session["objUser"];
                obj.matricula = objuser.login;
                //obj.matricula = "F02450";
                ret = objB.Inserir(obj);
            }
            if (ret)
            {

                ConsultarVidas(obj);
                MostraMensagemTelaUpdatePanel(upSys, "Relatório processado com sucesso!");
            }
            else
                MostraMensagemTelaUpdatePanel(upSys, "Problemas contate o Administrador de Sistemas");
        }

        private void ConsultarVidas(PagamentoCRC objs)
        {

            DataTable dt = objB.ListaVidas(objs);

            if (dt.Rows.Count > 0)
            {

                var nomeArquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_ControleCRC" + ".xls";

                Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                dtRelatorio.Add(nomeArquivo, dt);
                Session["DtRelatorio"] = dtRelatorio;
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                BasePage.AbrirNovaAba(upSys, "WebFile.aspx", "OPEN_WINDOW");
            }
            else
                MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\n" + "A consulta não retornou dados");
        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        #endregion

    

    }
}