using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.DAL.Faturamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class ExportaRelCI : BasePage
    {
        Relatorio relatorio = new Relatorio();
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string relatorio_nome = "RelatorioCIResumo";
        string relatorio_nomeDet = "RelatorioCIDet";
        string relatorio_titulo = "Relatórios CI";
        string relatorio_simples = @"~/Relatorios/Rel_CI_PorEmpresa.rpt";
        string relatorio_detalhado = @"~/Relatorios/Rel_CI_Detalhado.rpt";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (ReportCrystal != null)
            {
                ReportCrystal.RelatorioID = null;
                ReportCrystal = null;
            }
        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            ExportaRelCiDAL dal = new ExportaRelCiDAL();
            List<short> codEmpresa = dal.GetEmpresa(Convert.ToDateTime(txtDatInicio.Text),Convert.ToDateTime(txtDatFim.Text));

            if (InicializaRelatorioSimples(txtDatInicio.Text,txtDatFim.Text))
            {
                 ArquivoDownload adExtratoPdf = new ArquivoDownload();
                 adExtratoPdf.nome_arquivo = relatorio_nome + ".pdf";
                 adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adExtratoPdf.nome_arquivo;
                 adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                 ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                 Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
                 string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
                 AdicionarAcesso(fullUrl);
                 AbrirNovaAba(UpdatePanel, fullUrl, adExtratoPdf.nome_arquivo);
            }
            
            for (int i = 0; i < codEmpresa.Count; i++)
            {
                if (InicializaRelatorioDet(txtDatInicio.Text, txtDatFim.Text,codEmpresa[i].ToString()))
                {
                    ArquivoDownload adExtratoPdf = new ArquivoDownload();
                    adExtratoPdf.nome_arquivo = relatorio_nomeDet + codEmpresa[i].ToString() + ".pdf";
                    adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adExtratoPdf.nome_arquivo;
                    adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adExtratoPdf.nome_arquivo);
                }
                
            }
           
           
        }

        private bool InicializaRelatorioSimples(string datIni, string datFim)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();
           
                    relatorio.arquivo = relatorio_simples;
                    relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
                    relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });
           
              //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });
              

            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;
            return true;
        }

        private bool InicializaRelatorioDet(string datIni, string datFim, string codEmp)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_detalhado;
            relatorio.parametros.Add(new Parametro() { parametro = "datIni", valor = datIni });
            relatorio.parametros.Add(new Parametro() { parametro = "datFim", valor = datFim });
            relatorio.parametros.Add(new Parametro() { parametro = "codEmp", valor = codEmp });


            //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });


            Session[relatorio_nomeDet] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nomeDet;
            return true;
        }
    }
}