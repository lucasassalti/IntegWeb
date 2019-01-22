using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Intranet.Aplicacao.BLL;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;

namespace IntegWeb.Intranet.Web
{
    public partial class GeraMailing : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGerarSaude_Click(object sender, EventArgs e)
        {


            GeraMailingBLL dadosSaude = new GeraMailingBLL();
            DataTable valorDtSaude = dadosSaude.RetornaDtSaude();

            ArquivoDownload adXlsMaling = new ArquivoDownload();
            adXlsMaling.nome_arquivo = "ExportSaude_" + DateTime.Now.ToString("ddmmyyyy") + ".xls";
            adXlsMaling.dados = valorDtSaude;
            Session[ValidaCaracteres(adXlsMaling.nome_arquivo)] = adXlsMaling;
            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsMaling.nome_arquivo);
            AbrirNovaAba(UpdatePanel, fUrl, adXlsMaling.nome_arquivo);

        }

        protected void btnGerarPrevidencia_Click(object sender, EventArgs e)
        {
            GeraMailingBLL dadosPrev = new GeraMailingBLL();
            DataTable valorDtPrevidencia = dadosPrev.RetornaDtPrevidencia();

            ArquivoDownload adXlsMaling = new ArquivoDownload();
            adXlsMaling.nome_arquivo = "ExportPrevidencia_" + DateTime.Now.ToString("ddmmyyyy") + ".xls";
            adXlsMaling.dados = valorDtPrevidencia;
            Session[ValidaCaracteres(adXlsMaling.nome_arquivo)] = adXlsMaling;
            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsMaling.nome_arquivo);
            AbrirNovaAba(UpdatePanel, fUrl, adXlsMaling.nome_arquivo);
        }
    }
}