using IntegWeb.Previdencia.Aplicacao.BLL.Concessao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class AlteracaoTempoServico : BasePage
    {
        AlteracaoTempoServicoBLL bll = new AlteracaoTempoServicoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPlano.DataSource = bll.GetPlano();
                ddlPlano.DataValueField = "NUM_PLBNF";
                ddlPlano.DataTextField = "DCR_PLBNF";
                ddlPlano.DataBind();
                ddlPlano.Items.Insert(0, new ListItem("---Selecione---", ""));


                hlp_indicadores teste = new hlp_indicadores();
                teste.Indicador = "TS empresa comum";
                teste.Periodo = "04.03.1990 - 04.03.2019";
                teste.Anos = "29";
                teste.Meses = "0";
                teste.Dias = "0";
                List<hlp_indicadores> result = new List<hlp_indicadores>();
                result.Add(teste);
                grdIndicadores.DataSource = result;
                grdIndicadores.DataBind();
            }
        }
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            divPesquisa.Visible = false;
            divForm.Visible = true;
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            divPesquisa.Visible = true;
            divForm.Visible = false;
        }
        protected void rdTempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
    public class hlp_indicadores {
        public string Indicador { get; set; }
        public string Periodo { get; set; }
        public string Anos { get; set; }
        public string Meses { get; set; }
        public string Dias { get; set; }
    }
}