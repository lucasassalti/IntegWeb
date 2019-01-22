using IntegWeb.Saude.Aplicacao.BLL.Cobranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using IntegWeb.Entidades.Saude.Cobranca;
using System.Text;
using IntegWeb.Framework;

namespace IntegWeb.Saude.Web
{
    public partial class FusaoCisao : BasePage
    {
        #region Atributos
        CisaoFusaoBLL objBLL = new CisaoFusaoBLL();
        CisaoFusao obj = new CisaoFusao();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                   
                    bool ret = new CisaoFusaoBLL().Processar( user.login);
                    if (ret)
                    {
                        MostraMensagemTelaUpdatePanel(upCisao,"CISÃO/FUSÃO processado com sucesso\\nVerifique o último status do processo.");
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upCisao, "Atenção \\n\\nProcesso não executado.\\nVerifique o último status do processo.\\n");
                }
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção \\n\\nProcesso não executado.\\nMotivo:\\n" + ex.Message);
            }


        }

        protected void grdProcesso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdProcesso"] != null)
            {
                grdProcesso.PageIndex = e.NewPageIndex;
                grdProcesso.DataSource = ViewState["grdProcesso"];
                grdProcesso.DataBind();
            }
        }

        protected void btnVerifica_Click(object sender, EventArgs e)
        {
            CarregaGrid("grdProcesso", new CisaoFusaoBLL().BuscaCisaoLog(), grdProcesso);
        }

        protected void grdFusao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdFusao"] != null)
            {
                grdFusao.PageIndex = e.NewPageIndex;
                grdFusao.DataSource = ViewState["grdFusao"];
                grdFusao.DataBind();
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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string mesangem = ValidaTela();
                if (mesangem.Equals(""))
                {
                    obj.mes = int.Parse(drpMes.SelectedValue);
                    obj.ano = int.Parse(txtAno.Text);

                    DataTable dt = objBLL.BuscaCisao(obj);
                    CarregaGrid("grdFusao", dt, grdFusao);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\n" + mesangem);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        

    }
}