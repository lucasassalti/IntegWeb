using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Intranet.Aplicacao;
using System.Data;
using IntegWeb.Entidades.Framework;
using System.IO;
using System.Net;


namespace IntegWeb.Intranet.Web
{
    public partial class ListaEnvioEmail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {

            ListaEnvioEmailBLL bll = new ListaEnvioEmailBLL();

           //DataTable dtSau = bll.GeraBoletoSaude();
           
            DataTable dtSau = bll.Gera();

           ArquivoDownload adBoleto = new ArquivoDownload();
           adBoleto.nome_arquivo = "Lista_Email_Boletos_Saude.xlsx";
           adBoleto.dados = dtSau;
           Session[ValidaCaracteres(adBoleto.nome_arquivo)] = adBoleto;
           string fullBoleto = "WebFile.aspx?dwFile=" + ValidaCaracteres(adBoleto.nome_arquivo);
           AdicionarAcesso(fullBoleto);
           AbrirNovaAba(this.Page, fullBoleto, adBoleto.nome_arquivo);
            
            
        }

    }
}