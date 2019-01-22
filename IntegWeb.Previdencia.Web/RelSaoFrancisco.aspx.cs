using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class RelSaoFrancisco : System.Web.UI.Page
    {
        #region .: Propriedades :.

        Relatorio relatorio = new Relatorio();
        string relatorio_titulo = "Relatório São Francisco";
        string relatorio_inclusao = @"~/Relatorios/Rel_Sao_FranciscoINC.rpt";
        string relatorio_cancelamento = @"~/Relatorios/Rel_Sao_FranciscoCANC.rpt";
        string relatorio_nome = "RelatorioSaoFrancisco";
        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorioInclusao(txtDatInicio.Text, txtDatFim.Text) && ddlTipoRelatorio.SelectedValue == "1")
            {

                ReportCrystal.VisualizaRelatorio();

            }
            else if (InicializaRelatorioCancelamento(txtDatInicio.Text, txtDatFim.Text) && ddlTipoRelatorio.SelectedValue == "2")
            {

                ReportCrystal.VisualizaRelatorio();

            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (ReportCrystal != null)
            {
                ReportCrystal.ID = null;
                ReportCrystal = null;
            }
        }

        #endregion

        #region .: Métodos :.

        private bool InicializaRelatorioInclusao(string datIni, string datFim)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_inclusao;
            relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
            relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });

            //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });


            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;
            return true;
        }

        private bool InicializaRelatorioCancelamento(string datIni, string datFim)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_cancelamento;
            relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
            relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });

            //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });


            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;
            return true;
        }

        #endregion
    }
}