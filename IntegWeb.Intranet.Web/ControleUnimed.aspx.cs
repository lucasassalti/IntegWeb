using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Intranet.Aplicacao;
using System.Data;


namespace IntegWeb.Intranet.Web
{
    public partial class ControleUnimed : BasePage
    {
        string empresa;
        string matricula;
        string sub;
        string nrepr;
        string cpart;
        string cpf;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //teste
                //Teste
                //empresa = "40";
                //matricula = "413593";
                //nrepr = "936666";

                ////Parametro
                empresa = Request.QueryString["nempr"];
                matricula = Request.QueryString["nreg"];
                sub = Request.QueryString["sub"];
                nrepr = Request.QueryString["nrepr"];
                cpart = Request.QueryString["cpart"];
                cpf = Request.QueryString["pessDsCpf"];

                verificaConsultaUnimedPlanoSaude();
            }

        }

        void verificaConsultaUnimedPlanoSaude()
        {
            DataTable dt = new DataTable();
            ControleUnimedCrmBLL unimedCrmBLL = new ControleUnimedCrmBLL();

            if (string.IsNullOrEmpty(nrepr))
            {
                if (string.IsNullOrEmpty(empresa) || string.IsNullOrEmpty(matricula))
                {
                    dt = null;
                }
                else
                {
                    dt = unimedCrmBLL.selectUnimedPlanoSaudeTitular(empresa, matricula);
                }

            }
            else
            {
                dt = unimedCrmBLL.selectUnimedPlanoSaudeRepres(empresa, matricula, nrepr);
            }

            grdControleUnimed.DataSource = dt;
            grdControleUnimed.DataBind();
        }

    }
}