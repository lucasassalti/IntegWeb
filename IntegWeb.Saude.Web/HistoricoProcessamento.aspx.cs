using IntegWeb.Entidades.Saude.Financeiro;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IntegWeb.Saude.Web
{
    public partial class HistoricoProcessamento : BasePage
    {
           
      #region Metodos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaCamposTela();

            }
        }
        private void CarregaCamposTela()
        {                
       
            List<HistProcessaBoleto> list = new HistoricoBoletoBLL().ConsultarHistorico();
            CarregaGrid("grdHistorico", list.ToList<Object>(), grdHistorico);
            
        }
        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
            {

                divExibir.Visible = true;
                divOcultar.Visible = false;

            }
        private void CarregaGrid(string nameView, List<Object> list, GridView grid)
            {
                ViewState[nameView] = list;
                grid.DataSource = ViewState[nameView];
                grid.DataBind();
            }
    #endregion

        protected void btnTodasAsCobrancas_Click(object sender, EventArgs e)
        {
            DataTable dt = new HistoricoBoletoBLL().ListaCobrancas();

            if (dt.Rows.Count > 0)
            {
               // ExportarExcel(null, "TodasCobrancas.xls", dt);

                Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                var nomeArquivo = "TodasCobrancas.xls";
                dtRelatorio.Add(nomeArquivo, dt);
                Session["DtRelatorio"] = dtRelatorio;
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                BasePage.AbrirNovaAba(upHistBoleto, "WebFile.aspx", "OPEN_WINDOW");
            }
            else
            {
               // MostraMensagemTela(Page, "Não foram encotrados dados para a consulta");
                MostraMensagemTelaUpdatePanel(upHistBoleto, "Não foram encotrados dados para a consulta");
            }
            

        }

        protected void btnFlagAtivo_Click(object sender, EventArgs e)
        {
            DataTable dt = new HistoricoBoletoBLL().ListaFlagAtivo();

            if (dt.Rows.Count > 0)
            {
                //ExportarExcel(null, "FlagAtivo.xls", dt);
                Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                var nomeArquivo = "FlagAtivo.xls";
                dtRelatorio.Add(nomeArquivo, dt);
                Session["DtRelatorio"] = dtRelatorio;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);


            }
            else
            {
               // MostraMensagemTela(Page, "Não foram encotrados dados para a consulta");
                MostraMensagemTelaUpdatePanel(upHistBoleto, "Não foram encotrados dados para a consulta");
            }
        }

        protected void btnInadimplentes_Click(object sender, EventArgs e)
        {
            DataTable dt = new HistoricoBoletoBLL().ListaInadimplentes();

            if (dt.Rows.Count > 0)
            {
                //ExportarExcel(null, "Inadimplentes.xls", dt);

                Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                var nomeArquivo = "Inadimplentes.xls";
                dtRelatorio.Add(nomeArquivo, dt);
                Session["DtRelatorio"] = dtRelatorio;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);


            }
            else
            {
               // MostraMensagemTela(Page, "Não foram encotrados dados para a consulta");
                MostraMensagemTelaUpdatePanel(upHistBoleto, "Não foram encotrados dados para a consulta");
            }
        }

        protected void btnEnderecoNulo_Click(object sender, EventArgs e)
        {
            DataTable dt = new HistoricoBoletoBLL().ListaEnderecoNulo();

            if (dt.Rows.Count > 0)
            {
                //ExportarExcel(null, "Inadimplentes.xls", dt);

                Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                var nomeArquivo = "EnderecoNulo.xls";
                dtRelatorio.Add(nomeArquivo, dt);
                Session["DtRelatorio"] = dtRelatorio;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                
            }
            else
            {
               // MostraMensagemTela(Page, "Não foram encotrados dados para a consulta");
                MostraMensagemTelaUpdatePanel(upHistBoleto, "Não foram encotrados dados para a consulta");
            }
        }

       
    }
}