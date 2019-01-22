using IntegWeb.Saude.Aplicacao.BLL.Processos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;

namespace IntegWeb.Saude.Web
{
    public partial class HistoricoAtendOdonto : BasePage
    {
        #region .: Propriedades :.

        Relatorio relatorio = new Relatorio();
        string relatorio_nome = "RelatorioHistAtendOdonto";
        string relatorio_titulo = "Relatório Historico Atendimento Odonto";
        string relatorio_caminho = @"~/Relatorios/Rel_Historico_Fatura.rpt";

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            string numProtocolo = txtNumProtocolo.Text == "" ? "0" : txtNumProtocolo.Text;
            string numContrato = txtNumContrato.Text == "" ? "0" : txtNumContrato.Text;
            string dtPagto = txtDtPagto.Text;
            string numCrg = txtNumCRG.Text == "" ? "0" : txtNumCRG.Text;



            if (String.IsNullOrEmpty(txtNumProtocolo.Text) && String.IsNullOrEmpty(txtNumContrato.Text) && String.IsNullOrEmpty(txtNumCRG.Text) && String.IsNullOrEmpty(txtDtPagto.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Erro: Favor preencher os campos para consulta");
            }
            else if (!String.IsNullOrEmpty(txtNumProtocolo.Text) && String.IsNullOrEmpty(txtNumContrato.Text) && String.IsNullOrEmpty(txtNumCRG.Text))
            {
                if (InicializaRelatorioHist(numProtocolo, numContrato, numCrg, dtPagto))
                {
                    //Download PDF
                    ArquivoDownload adRelResumoPdf = new ArquivoDownload();
                    adRelResumoPdf.nome_arquivo = relatorio_nome + ".pdf";
                    adRelResumoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelResumoPdf.nome_arquivo;
                    adRelResumoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelResumoPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelResumoPdf.nome_arquivo)] = adRelResumoPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelResumoPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adRelResumoPdf.nome_arquivo);

                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Relatório Gerado com Sucesso");
                }
            }
            else if (!String.IsNullOrEmpty(txtNumContrato.Text) && String.IsNullOrEmpty(txtNumCRG.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Erro: Favor preencher o campo Número CRG");
                txtNumCRG.Focus();
            }
            else if (String.IsNullOrEmpty(txtNumContrato.Text) && !String.IsNullOrEmpty(txtNumCRG.Text))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Erro: Favor preencher o campo Número Contrato");
                txtNumContrato.Focus();
            }
            else
            {
                if (InicializaRelatorioHist(numProtocolo, numContrato, numCrg, dtPagto))
                {
                    //Download PDF
                    ArquivoDownload adRelHistFatOdontPdf = new ArquivoDownload();
                    adRelHistFatOdontPdf.nome_arquivo = relatorio_nome + ".pdf";
                    adRelHistFatOdontPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adRelHistFatOdontPdf.nome_arquivo;
                    adRelHistFatOdontPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adRelHistFatOdontPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adRelHistFatOdontPdf.nome_arquivo)] = adRelHistFatOdontPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelHistFatOdontPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(UpdatePanel, fullUrl, adRelHistFatOdontPdf.nome_arquivo);

                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Relatório Gerado com Sucesso");
                }
            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNumContrato.Text = "";
            txtNumCRG.Text = "";
            txtNumProtocolo.Text = "";
            txtDtPagto.Text = "";

        }

        #endregion

        #region .: Métodos :.

        private bool InicializaRelatorioHist(string numProtocolo, string numContrato, string numCrg, string dtPagto)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_caminho;
            relatorio.parametros.Add(new Parametro() { parametro = "codProt", valor = numProtocolo });
            relatorio.parametros.Add(new Parametro() { parametro = "codPrest", valor = numContrato });
            relatorio.parametros.Add(new Parametro() { parametro = "codCRG", valor = numCrg });
            relatorio.parametros.Add(new Parametro() { parametro = "dataPagto", valor = dtPagto });

            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;

            return true;
        }


        #endregion

    }
}