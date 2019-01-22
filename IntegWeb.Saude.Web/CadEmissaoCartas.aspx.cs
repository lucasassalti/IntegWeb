using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Saude.Aplicacao.BLL.Cadastro;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class CadEmissaoCartas : BasePage
    {

        #region .: Propriedades :.

        Relatorio relatorio = new Relatorio();

        //adesao.rpt
        string relatorio_nome_adesao = "Adesao";
        string relatorio_titulo_adesao = "Carta Adesão";
        string relatorio_adesao = @"~/Relatorios/Cadastro/ADESAO.rpt";

        //etq.rpt
        string relatorio_nome_etq = "Etiqueta";
        string relatorio_titulo_etq = "Etiqueta";
        string relatorio_etq = @"~/Relatorios/Cadastro/ETQ.rpt";

        //qtde_cartas.rpt
        string relatorio_nome_qtde_cartas = "Quantidade_Cartas";
        string relatorio_titulo_qtde_cartas = "Quantidade Cartas";
        string relatorio_qtde_cartas = @"~/Relatorios/Cadastro/QTDE_CARTAS.rpt";

        //cartas_rel.rpt
        string relatorio_nome_cartas_rel = "Relatorio_Cartas";
        string relatorio_titulo_cartas_rel = "Relatorio Cartas";
        string relatorio_cartas_rel = @"~/Relatorios/Cadastro/CARTAS_REL.rpt";

        //CANCELAMENTO_CONGENERE.rpt
        string relatorio_nome_canc_congenere = "Cancelamento_Congenere";
        string relatorio_titulo_canc_congenere = "Relatorio Cancelamento Congenere";
        string relatorio_canc_congenere = @"~/Relatorios/Cadastro/CANCELAMENTO_CONGENERE.rpt";


        //ETQ_CONGENERE.rpt
        string relatorio_nome_etq_congenere = "Etiqueta_Congenere";
        string relatorio_titulo_etq_congenere = "Etiqueta Congenere";
        string relatorio_etq_congenere = @"~/Relatorios/Cadastro/ETQ_CONGENERE.rpt";


        //QTDE_CARTAS_CONGENERE.rpt
        string relatorio_nome_qtde_carta_congenere = "Qtde_Carta_Congenere";
        string relatorio_titulo_qtde_carta_congenere = "Quantidade Carta Congenere";
        string relatorio_qtde_carta_congenere = @"~/Relatorios/Cadastro/QTDE_CARTAS_CONGENERE.rpt";

        //CARTAS_REL_CONGENERE.rpt
        string relatorio_nome_carta_congenere = "Carta_Congenere";
        string relatorio_titulo_carta_congenere = "Carta Congenere";
        string relatorio_carta_congenere = @"~/Relatorios/Cadastro/CARTAS_REL_CONGENERE.rpt";

        //CANCELAMENTO.rpt
        string relatorio_nome_cancelamento = "Cancelamento";
        string relatorio_titulo_cancelamento = "Cancelamento";
        string relatorio_cancelamento = @"~/Relatorios/Cadastro/CANCELAMENTO.rpt";

        //ETQ_CANCELAMENTO.rpt
        string relatorio_nome_etq_cancelamento = "Etiqueta_Cancelamento";
        string relatorio_titulo_etq_cancelamento = "Etiqueta Cancelamento";
        string relatorio_etq_cancelamento = @"~/Relatorios/Cadastro/ETQ_CANCELAMENTO.rpt";

        //CANCELAMENTO_412.rpt
        string relatorio_nome_cancelamento412 = "Cancelamento_RN412";
        string relatorio_titulo_cancelamento412 = "Cancelamento RN412";
        string relatorio_cancelamento412 = @"~/Relatorios/Cadastro/CANCELAMENTO_412.rpt";

        //cancelamento_24.rpt
        string relatorio_nome_cancelamento24 = "Cancelamento24";
        string relatorio_titulo_cancelamento24 = "Cancelamento 24 anos";
        string relatorio_cancelamento24 = @"~/Relatorios/Cadastro/CANCELAMENTO_24.rpt";

        //REV_APOSENTADO.rpt
        string relatorio_nome_rev_aposentado = "Rev_Aposentado";
        string relatorio_titulo_rev_aposentado = "Rev Aposentado";
        string relatorio_rev_aposentado = @"~/Relatorios/Cadastro/REV_APOSENTADO.rpt";

        //QTDE_CARTAS_ATIVOS_1.rpt
        string relatorio_nome_qtde_carta_ativo = "Quantidade_Carta_Ativo";
        string relatorio_titulo_qtde_carta_ativo = "Quantidade Carta Ativo";
        string relatorio_qtde_carta_ativo = @"~/Relatorios/Cadastro/QTDE_CARTAS_ATIVOS_1.rpt";

        //QTDE_CARTAS_ATIVOS_2.rpt - cteep elektro cpfl
        string relatorio_nome_qtde_carta_ativo2 = "Quantidade_Carta_Ativo2";
        string relatorio_titulo_qtde_carta_ativo2 = "Quantidade Carta Ativo 2";
        string relatorio_qtde_carta_ativo2 = @"~/Relatorios/Cadastro/QTDE_CARTAS_ATIVOS_2.rpt";


        //REV_ATIVO.rpt
        string relatorio_nome_rev_ativo = "Rev_Ativo";
        string relatorio_titulo_rev_ativo = "Rev Ativo";
        string relatorio_rev_ativo = @"~/Relatorios/Cadastro/REV_ATIVO.rpt";

        //REV_ATIVO_CTEEP.rpt
        string relatorio_nome_rev_ativo_cteep = "Rev_Ativo_Cteep";
        string relatorio_titulo_rev_ativo_cteep = "Rev Ativo Cteep";
        string relatorio_rev_ativo_cteep = @"~/Relatorios/Cadastro/REV_ATIVO_CTEEP.rpt";

        //REV_ATIVO_ELEKTRO.rpt
        string relatorio_nome_rev_ativo_elektro = "Rev_Ativo_Elektro";
        string relatorio_titulo_rev_ativo_elektro = "Rev Ativo Elektro";
        string relatorio_rev_ativo_elektro = @"~/Relatorios/Cadastro/REV_ATIVO_ELEKTRO.rpt";

        //REV_ATIVO_CPFL.rpt
        string relatorio_nome_rev_ativo_cpfl = "Rev_Ativo_Cpfl";
        string relatorio_titulo_rev_ativo_cpfl = "Rev Ativo Cpfl";
        string relatorio_rev_ativo_cpfl = @"~/Relatorios/Cadastro/REV_ATIVO_CPFL.rpt";

        //ETQ_ATIVO.rpt
        string relatorio_nome_etq_ativo = "Etiqueta_Ativo";
        string relatorio_titulo_etq_ativo = "Etiqueta Ativo";
        string relatorio_etq_ativo = @"~/Relatorios/Cadastro/ETQ_ATIVO.rpt";

        //ETQ_ATIVO_CTEEP.rpt
        string relatorio_nome_etq_ativo_cteep = "Etiqueta_Ativo_Cteep";
        string relatorio_titulo_etq_ativo_cteep = "Etiqueta Ativo Cteep";
        string relatorio_etq_ativo_cteep = @"~/Relatorios/Cadastro/ETQ_ATIVO_CTEEP.rpt";

        //ETQ_ATIVO_ELEKTRO.rpt
        string relatorio_nome_etq_ativo_elektro = "Etiqueta_Ativo_Elektro";
        string relatorio_titulo_etq_ativo_elektro = "Etiqueta Ativo Elektro";
        string relatorio_etq_ativo_elektro = @"~/Relatorios/Cadastro/ETQ_ATIVO_ELEKTRO.rpt";

        //ETQ_ATIVO_CPFL.rpt
        string relatorio_nome_etq_ativo_cpfl = "Etiqueta_Ativo_Cpfl";
        string relatorio_titulo_etq_ativo_cpfl = "Etiqueta Ativo Cpfl";
        string relatorio_etq_ativo_cpfl = @"~/Relatorios/Cadastro/ETQ_ATIVO_CPFL.rpt";

        //CARTAS_REL_1.rpt
        string relatorio_nome_cartas_rel1 = "Cartas1";
        string relatorio_titulo_cartas_rel1 = "Cartas 1";
        string relatorio_cartas_rel1 = @"~/Relatorios/Cadastro/CARTAS_REL_1.rpt";

        //CARTAS_REL_2.rpt - cteep elektro cpfl
        string relatorio_nome_cartas_rel2 = "Cartas2";
        string relatorio_titulo_cartas_rel2 = "Cartas 2";
        string relatorio_cartas_rel2 = @"~/Relatorios/Cadastro/CARTAS_REL_2.rpt";

        //TROCA.rpt
        string relatorio_nome_troca = "Rel_Troca";
        string relatorio_titulo_troca = "Troca";
        string relatorio_troca = @"~/Relatorios/Cadastro/TROCA.rpt";

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtDatFim.Text = "";
            txtDatIni.Text = "";
            ddlTipoRelatorio.SelectedIndex = 0;
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            string caminho_servidor = Server.MapPath(@"~/");
            string Pasta_Server = caminho_servidor + @"\UploadFile";
            string Pasta_Server_Download = caminho_servidor + @"\UploadFile";

            Pasta_Server = Pasta_Server + @"\" + "CARTAS" + "\\";
            Pasta_Server_Download = Pasta_Server_Download + @"\" + "EXTRAIR";

            if (Directory.Exists(Pasta_Server_Download))
            {
                Directory.Delete(Pasta_Server_Download, true);
            }

            Directory.CreateDirectory(Pasta_Server);
            Directory.CreateDirectory(Pasta_Server_Download);

            CadEmissaoCartasBLL bll = new CadEmissaoCartasBLL();
            Resultado res = new Resultado();

            Resultado resDelete = bll.DeleteGeracao(ddlTipoRelatorio.SelectedValue);
            res = bll.GerarCartas(Convert.ToDateTime(txtDatIni.Text), Convert.ToDateTime(txtDatFim.Text), ddlTipoRelatorio.SelectedValue);

            if (resDelete.Ok && res.Ok)
            {

                #region .: Adesão :.

                if (ddlTipoRelatorio.SelectedValue == "ADESAO")
                {
                    //adesao.rpt
                    if (InicializaRelatorioAdesao(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adAdesaoPdf = new ArquivoDownload();
                        adAdesaoPdf.nome_arquivo = relatorio_nome_adesao + ".pdf";
                        adAdesaoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adAdesaoPdf.nome_arquivo;
                        adAdesaoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adAdesaoPdf.caminho_arquivo);

                    }

                    //etq.rpt
                    if (InicializaRelatorioEtiqueta(ddlTipoRelatorio.SelectedValue, DateTime.Now.ToShortDateString()))
                    {
                        ArquivoDownload adExtratoPdf = new ArquivoDownload();
                        adExtratoPdf.nome_arquivo = relatorio_nome_etq + ".pdf";
                        adExtratoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adExtratoPdf.nome_arquivo;
                        adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);
                    }

                    //qtde_cartas.rpt
                    if (InicializaRelatorioQtdCartas(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adQtdCartasPdf = new ArquivoDownload();
                        adQtdCartasPdf.nome_arquivo = relatorio_nome_qtde_cartas + ".pdf";
                        adQtdCartasPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdCartasPdf.nome_arquivo;
                        adQtdCartasPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdCartasPdf.caminho_arquivo);
                    }

                    //cartas_rel.rpt
                    if (InicializaRelatorioCartasRel(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCartasRelPdf = new ArquivoDownload();
                        adCartasRelPdf.nome_arquivo = relatorio_nome_cartas_rel + ".pdf";
                        adCartasRelPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRelPdf.nome_arquivo;
                        adCartasRelPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRelPdf.caminho_arquivo);

                    }

                    if (Directory.GetFiles(Pasta_Server).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip");

                        //Download do arquivo
                        ArquivoDownload adZipCartas = new ArquivoDownload();
                        adZipCartas.nome_arquivo = "CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip";
                        adZipCartas.caminho_arquivo = Pasta_Server_Download + "\\" + adZipCartas.nome_arquivo;
                        Session[ValidaCaracteres(adZipCartas.nome_arquivo)] = adZipCartas;
                        string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipCartas.nome_arquivo);
                        AbrirNovaAba(UpdatePanel, fUrl, adZipCartas.nome_arquivo);
                    }

                    if (Directory.Exists(Pasta_Server))
                    {
                        Directory.Delete(Pasta_Server, true);

                    }
                }

                #endregion

                #region .: Cancelamento :.

                else if (ddlTipoRelatorio.SelectedValue == "CANCELAMENTO")
                {
                    //CANCELAMENTO_CONGENERE.rpt
                    if (InicializaRelatorioCancCongenere(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCancCongenerePdf = new ArquivoDownload();
                        adCancCongenerePdf.nome_arquivo = relatorio_nome_canc_congenere + ".pdf";
                        adCancCongenerePdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCancCongenerePdf.nome_arquivo;
                        adCancCongenerePdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCancCongenerePdf.caminho_arquivo);
                    }

                    //ETQ_CONGENERE.rpt
                    if (InicializaRelatorioEtiquetaCongenere(ddlTipoRelatorio.SelectedValue, DateTime.Now.ToShortDateString()))
                    {
                        ArquivoDownload adEtqCongenerePdf = new ArquivoDownload();
                        adEtqCongenerePdf.nome_arquivo = relatorio_nome_etq_congenere + ".pdf";
                        adEtqCongenerePdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adEtqCongenerePdf.nome_arquivo;
                        adEtqCongenerePdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adEtqCongenerePdf.caminho_arquivo);
                    }


                    //QTDE_CARTAS_CONGENERE.rpt
                    if (InicializaRelatorioQtdCartasCongenere(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adQtdCartCongenerePdf = new ArquivoDownload();
                        adQtdCartCongenerePdf.nome_arquivo = relatorio_nome_qtde_carta_congenere + ".pdf";
                        adQtdCartCongenerePdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdCartCongenerePdf.nome_arquivo;
                        adQtdCartCongenerePdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdCartCongenerePdf.caminho_arquivo);
                    }

                    //CARTAS_REL_CONGENERE.rpt
                    if (InicializaRelatorioCartasRelCongenere(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCartRelCongenerePdf = new ArquivoDownload();
                        adCartRelCongenerePdf.nome_arquivo = relatorio_nome_carta_congenere + ".pdf";
                        adCartRelCongenerePdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartRelCongenerePdf.nome_arquivo;
                        adCartRelCongenerePdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartRelCongenerePdf.caminho_arquivo);
                    }

                    //CANCELAMENTO.rpt
                    if (InicializaRelatorioCancelamento(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCancelamentoPdf = new ArquivoDownload();
                        adCancelamentoPdf.nome_arquivo = relatorio_nome_cancelamento + ".pdf";
                        adCancelamentoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCancelamentoPdf.nome_arquivo;
                        adCancelamentoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCancelamentoPdf.caminho_arquivo);
                    }

                    //ETQ_CANCELAMENTO.rpt
                    if (InicializaRelatorioEtiquetaCancelamento(ddlTipoRelatorio.SelectedValue, DateTime.Now.ToShortDateString()))
                    {
                        ArquivoDownload adEtqCancelamentoPdf = new ArquivoDownload();
                        adEtqCancelamentoPdf.nome_arquivo = relatorio_nome_etq_cancelamento + ".pdf";
                        adEtqCancelamentoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adEtqCancelamentoPdf.nome_arquivo;
                        adEtqCancelamentoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adEtqCancelamentoPdf.caminho_arquivo);
                    }

                    //QTDE_CARTAS.rpt
                    if (InicializaRelatorioQtdCartas(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adQtdCartasPdf = new ArquivoDownload();
                        adQtdCartasPdf.nome_arquivo = relatorio_nome_qtde_cartas + ".pdf";
                        adQtdCartasPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdCartasPdf.nome_arquivo;
                        adQtdCartasPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdCartasPdf.caminho_arquivo);
                    }

                    //CARTAS_REL.rpt
                    if (InicializaRelatorioCartasRel(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCartasRelPdf = new ArquivoDownload();
                        adCartasRelPdf.nome_arquivo = relatorio_nome_cartas_rel + ".pdf";
                        adCartasRelPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRelPdf.nome_arquivo;
                        adCartasRelPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRelPdf.caminho_arquivo);

                    }

                    if (Directory.GetFiles(Pasta_Server).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip");

                        //Download do arquivo
                        ArquivoDownload adZipCartas = new ArquivoDownload();
                        adZipCartas.nome_arquivo = "CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip";
                        adZipCartas.caminho_arquivo = Pasta_Server_Download + "\\" + adZipCartas.nome_arquivo;
                        Session[ValidaCaracteres(adZipCartas.nome_arquivo)] = adZipCartas;
                        string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipCartas.nome_arquivo);
                        AbrirNovaAba(UpdatePanel, fUrl, adZipCartas.nome_arquivo);
                    }

                    if (Directory.Exists(Pasta_Server))
                    {
                        Directory.Delete(Pasta_Server, true);

                    }

                }

                #endregion

                #region .: Cancelamento RN 412 :.

                else if (ddlTipoRelatorio.SelectedValue == "CANCELAMENTO_412")
                {
                    //CANCELAMENTO_412.rpt
                    if (InicializaRelatorioCancelamento412(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCancelamento412Pdf = new ArquivoDownload();
                        adCancelamento412Pdf.nome_arquivo = relatorio_nome_cancelamento412 + ".pdf";
                        adCancelamento412Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCancelamento412Pdf.nome_arquivo;
                        adCancelamento412Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCancelamento412Pdf.caminho_arquivo);
                    }

                    //ETQ_CANCELAMENTO.rpt
                    if (InicializaRelatorioEtiquetaCancelamento(ddlTipoRelatorio.SelectedValue, DateTime.Now.ToShortDateString()))
                    {
                        ArquivoDownload adEtqCancelamentoPdf = new ArquivoDownload();
                        adEtqCancelamentoPdf.nome_arquivo = relatorio_nome_etq_cancelamento + ".pdf";
                        adEtqCancelamentoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adEtqCancelamentoPdf.nome_arquivo;
                        adEtqCancelamentoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adEtqCancelamentoPdf.caminho_arquivo);
                    }

                    //QTDE_CARTAS.rpt
                    if (InicializaRelatorioQtdCartas(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adQtdCartasPdf = new ArquivoDownload();
                        adQtdCartasPdf.nome_arquivo = relatorio_nome_qtde_cartas + ".pdf";
                        adQtdCartasPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdCartasPdf.nome_arquivo;
                        adQtdCartasPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdCartasPdf.caminho_arquivo);
                    }

                    //CARTAS_REL.rpt
                    if (InicializaRelatorioCartasRel(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCartasRelPdf = new ArquivoDownload();
                        adCartasRelPdf.nome_arquivo = relatorio_nome_cartas_rel + ".pdf";
                        adCartasRelPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRelPdf.nome_arquivo;
                        adCartasRelPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRelPdf.caminho_arquivo);

                    }

                    if (Directory.GetFiles(Pasta_Server).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip");

                        //Download do arquivo
                        ArquivoDownload adZipCartas = new ArquivoDownload();
                        adZipCartas.nome_arquivo = "CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip";
                        adZipCartas.caminho_arquivo = Pasta_Server_Download + "\\" + adZipCartas.nome_arquivo;
                        Session[ValidaCaracteres(adZipCartas.nome_arquivo)] = adZipCartas;
                        string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipCartas.nome_arquivo);
                        AbrirNovaAba(UpdatePanel, fUrl, adZipCartas.nome_arquivo);
                    }

                    if (Directory.Exists(Pasta_Server))
                    {
                        Directory.Delete(Pasta_Server, true);

                    }
                }

                #endregion

                #region .: Cancelamento 24 anos :.

                else if (ddlTipoRelatorio.SelectedValue == "CANCELAMENTO_24")
                {
                    //cancelamento_24.rpt
                    if (InicializaRelatorioCancelamento24())
                    {
                        ArquivoDownload adCancelamento24Pdf = new ArquivoDownload();
                        adCancelamento24Pdf.nome_arquivo = relatorio_nome_cancelamento24 + ".pdf";
                        adCancelamento24Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCancelamento24Pdf.nome_arquivo;
                        adCancelamento24Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCancelamento24Pdf.caminho_arquivo);
                    }

                    //etq.rpt
                    if (InicializaRelatorioEtiqueta(ddlTipoRelatorio.SelectedValue, DateTime.Now.ToShortDateString()))
                    {
                        ArquivoDownload adExtratoPdf = new ArquivoDownload();
                        adExtratoPdf.nome_arquivo = relatorio_nome_etq + ".pdf";
                        adExtratoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adExtratoPdf.nome_arquivo;
                        adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);
                    }

                    //qtde_cartas.rpt
                    if (InicializaRelatorioQtdCartas(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adQtdCartasPdf = new ArquivoDownload();
                        adQtdCartasPdf.nome_arquivo = relatorio_nome_qtde_cartas + ".pdf";
                        adQtdCartasPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdCartasPdf.nome_arquivo;
                        adQtdCartasPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdCartasPdf.caminho_arquivo);
                    }

                    //cartas_rel.rpt
                    if (InicializaRelatorioCartasRel(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCartasRelPdf = new ArquivoDownload();
                        adCartasRelPdf.nome_arquivo = relatorio_nome_cartas_rel + ".pdf";
                        adCartasRelPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRelPdf.nome_arquivo;
                        adCartasRelPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRelPdf.caminho_arquivo);

                    }

                    if (Directory.GetFiles(Pasta_Server).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip");

                        //Download do arquivo
                        ArquivoDownload adZipCartas = new ArquivoDownload();
                        adZipCartas.nome_arquivo = "CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip";
                        adZipCartas.caminho_arquivo = Pasta_Server_Download + "\\" + adZipCartas.nome_arquivo;
                        Session[ValidaCaracteres(adZipCartas.nome_arquivo)] = adZipCartas;
                        string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipCartas.nome_arquivo);
                        AbrirNovaAba(UpdatePanel, fUrl, adZipCartas.nome_arquivo);
                    }

                    if (Directory.Exists(Pasta_Server))
                    {
                        Directory.Delete(Pasta_Server, true);

                    }

                }

                #endregion

                #region .: Rev Aposentado :.

                else if (ddlTipoRelatorio.SelectedValue == "REV_APOSENTADO")
                {
                    //REV_APOSENTADO.rpt
                    if (InicializaRelatorioRevAposentado(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adRevAposentadoPdf = new ArquivoDownload();
                        adRevAposentadoPdf.nome_arquivo = relatorio_nome_rev_aposentado + ".pdf";
                        adRevAposentadoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevAposentadoPdf.nome_arquivo;
                        adRevAposentadoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevAposentadoPdf.caminho_arquivo);
                    }

                    //etq.rpt
                    if (InicializaRelatorioEtiqueta(ddlTipoRelatorio.SelectedValue, DateTime.Now.ToShortDateString()))
                    {
                        ArquivoDownload adExtratoPdf = new ArquivoDownload();
                        adExtratoPdf.nome_arquivo = relatorio_nome_etq + ".pdf";
                        adExtratoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adExtratoPdf.nome_arquivo;
                        adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                    }

                    //qtde_cartas.rpt
                    if (InicializaRelatorioQtdCartas(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adQtdCartasPdf = new ArquivoDownload();
                        adQtdCartasPdf.nome_arquivo = relatorio_nome_qtde_cartas + ".pdf";
                        adQtdCartasPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdCartasPdf.nome_arquivo;
                        adQtdCartasPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdCartasPdf.caminho_arquivo);
                    }

                    //cartas_rel.rpt
                    if (InicializaRelatorioCartasRel(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCartasRelPdf = new ArquivoDownload();
                        adCartasRelPdf.nome_arquivo = relatorio_nome_cartas_rel + ".pdf";
                        adCartasRelPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRelPdf.nome_arquivo;
                        adCartasRelPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRelPdf.caminho_arquivo);

                    }

                    if (Directory.GetFiles(Pasta_Server).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip");

                        //Download do arquivo
                        ArquivoDownload adZipCartas = new ArquivoDownload();
                        adZipCartas.nome_arquivo = "CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip";
                        adZipCartas.caminho_arquivo = Pasta_Server_Download + "\\" + adZipCartas.nome_arquivo;
                        Session[ValidaCaracteres(adZipCartas.nome_arquivo)] = adZipCartas;
                        string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipCartas.nome_arquivo);
                        AbrirNovaAba(UpdatePanel, fUrl, adZipCartas.nome_arquivo);
                    }

                    if (Directory.Exists(Pasta_Server))
                    {
                        Directory.Delete(Pasta_Server, true);

                    }

                }

                #endregion

                #region .: Rev Ativo :.

                else if (ddlTipoRelatorio.SelectedValue == "REV_ATIVO")
                {
                    //QTDE_CARTAS_ATIVOS_1.rpt 2,62,66,71,72,79,80,81,82,83,84,85,86,87,89,91,92,95,97,98,99,100,43,50
                    if (InicializaRelatorioQtdeCartaAtivos(ddlTipoRelatorio.SelectedValue, "2,62,66,71,72,79,80,81,82,83,84,85,86,87,89,91,92,95,97,98,99,100,43,50"))
                    {
                        ArquivoDownload adQtdeCartaAtivosPdf = new ArquivoDownload();
                        adQtdeCartaAtivosPdf.nome_arquivo = relatorio_nome_qtde_carta_ativo + ".pdf";
                        adQtdeCartaAtivosPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdeCartaAtivosPdf.nome_arquivo;
                        adQtdeCartaAtivosPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdeCartaAtivosPdf.caminho_arquivo);
                    }
                    //QTDE_CARTAS_ATIVOS_2.rpt - cteep 43
                    if (InicializaRelatorioQtdeCartaAtivos2(ddlTipoRelatorio.SelectedValue, "43"))
                    {
                        ArquivoDownload adQtdeCartaAtivos2Pdf = new ArquivoDownload();
                        adQtdeCartaAtivos2Pdf.nome_arquivo = relatorio_nome_qtde_carta_ativo2 + "cteep" + ".pdf";
                        adQtdeCartaAtivos2Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdeCartaAtivos2Pdf.nome_arquivo;
                        adQtdeCartaAtivos2Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdeCartaAtivos2Pdf.caminho_arquivo);
                    }
                    //QTDE_CARTAS_ATIVOS_2.rpt - elektro 50
                    if (InicializaRelatorioQtdeCartaAtivos2(ddlTipoRelatorio.SelectedValue, "50"))
                    {
                        ArquivoDownload adQtdeCartaAtivos2Pdf = new ArquivoDownload();
                        adQtdeCartaAtivos2Pdf.nome_arquivo = relatorio_nome_qtde_carta_ativo2 + "elektro" +".pdf";
                        adQtdeCartaAtivos2Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdeCartaAtivos2Pdf.nome_arquivo;
                        adQtdeCartaAtivos2Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdeCartaAtivos2Pdf.caminho_arquivo);
                    }
                    //QTDE_CARTAS_ATIVOS_2.rpt - cpfl 2,62,66,71,72,79,80,81,82,83,84,85,86,87,89,91,92,95,97,98,99,100
                    if (InicializaRelatorioQtdeCartaAtivos2(ddlTipoRelatorio.SelectedValue, "2,62,66,71,72,79,80,81,82,83,84,85,86,87,89,91,92,95,97,98,99,100"))
                    {
                        ArquivoDownload adQtdeCartaAtivos2Pdf = new ArquivoDownload();
                        adQtdeCartaAtivos2Pdf.nome_arquivo = relatorio_nome_qtde_carta_ativo2 + "cpfl" + ".pdf";
                        adQtdeCartaAtivos2Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdeCartaAtivos2Pdf.nome_arquivo;
                        adQtdeCartaAtivos2Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdeCartaAtivos2Pdf.caminho_arquivo);
                    }

                    //REV_ATIVO.rpt
                    if (InicializaRelatorioRevAtivo(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adRevAtivoPdf = new ArquivoDownload();
                        adRevAtivoPdf.nome_arquivo = relatorio_nome_rev_ativo + ".pdf";
                        adRevAtivoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevAtivoPdf.nome_arquivo;
                        adRevAtivoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevAtivoPdf.caminho_arquivo);

                    }

                    //REV_ATIVO_CTEEP.rpt
                    if (InicializaRelatorioRevAtivoCteep(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adRevAtivoCteepPdf = new ArquivoDownload();
                        adRevAtivoCteepPdf.nome_arquivo = relatorio_nome_rev_ativo_cteep + ".pdf";
                        adRevAtivoCteepPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevAtivoCteepPdf.nome_arquivo;
                        adRevAtivoCteepPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevAtivoCteepPdf.caminho_arquivo);

                    }


                    //REV_ATIVO_ELEKTRO.rpt
                    if (InicializaRelatorioRevAtivoElektro(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adRevAtivoElektroPdf = new ArquivoDownload();
                        adRevAtivoElektroPdf.nome_arquivo = relatorio_nome_rev_ativo_elektro + ".pdf";
                        adRevAtivoElektroPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevAtivoElektroPdf.nome_arquivo;
                        adRevAtivoElektroPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevAtivoElektroPdf.caminho_arquivo);
                    }
                    //REV_ATIVO_CPFL.rpt
                    if (InicializaRelatorioRevAtivoCpfl())
                    {
                        ArquivoDownload adRevAtivoCpflPdf = new ArquivoDownload();
                        adRevAtivoCpflPdf.nome_arquivo = relatorio_nome_rev_ativo_cpfl + ".pdf";
                        adRevAtivoCpflPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevAtivoCpflPdf.nome_arquivo;
                        adRevAtivoCpflPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevAtivoCpflPdf.caminho_arquivo);

                    }
                    //ETQ_ATIVO.rpt
                    if (InicializaRelatorioEtiquetaAtivo(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adRevEtqAtivoPdf = new ArquivoDownload();
                        adRevEtqAtivoPdf.nome_arquivo = relatorio_nome_etq_ativo + ".pdf";
                        adRevEtqAtivoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevEtqAtivoPdf.nome_arquivo;
                        adRevEtqAtivoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevEtqAtivoPdf.caminho_arquivo);
                    }
                    //ETQ_ATIVO_CTEEP.rpt
                    if (InicializaRelatorioEtiquetaAtivoCteep(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adRevEtqAtivoCteepPdf = new ArquivoDownload();
                        adRevEtqAtivoCteepPdf.nome_arquivo = relatorio_nome_etq_ativo_cteep + ".pdf";
                        adRevEtqAtivoCteepPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevEtqAtivoCteepPdf.nome_arquivo;
                        adRevEtqAtivoCteepPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevEtqAtivoCteepPdf.caminho_arquivo);
                    }
                    //ETQ_ATIVO_ELEKTRO.rpt
                    if (InicializaRelatorioEtiquetaAtivoElektro(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adRevEtqAtivoElektroPdf = new ArquivoDownload();
                        adRevEtqAtivoElektroPdf.nome_arquivo = relatorio_nome_etq_ativo_elektro + ".pdf";
                        adRevEtqAtivoElektroPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevEtqAtivoElektroPdf.nome_arquivo;
                        adRevEtqAtivoElektroPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevEtqAtivoElektroPdf.caminho_arquivo);
                    }
                    //ETQ_ATIVO_CPFL.rpt
                    if (InicializaRelatorioEtiquetaAtivoCpfl(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adRevEtqAtivoCpflPdf = new ArquivoDownload();
                        adRevEtqAtivoCpflPdf.nome_arquivo = relatorio_nome_etq_ativo_cpfl + ".pdf";
                        adRevEtqAtivoCpflPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adRevEtqAtivoCpflPdf.nome_arquivo;
                        adRevEtqAtivoCpflPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adRevEtqAtivoCpflPdf.caminho_arquivo);
                    }
                    //CARTAS_REL_1.rpt 2,62,66,71,72,79,80,81,82,83,84,85,86,87,89,91,92,95,97,98,99,100,43,50
                    if (InicializaRelatorioCartasRel1(ddlTipoRelatorio.SelectedValue, "2,62,66,71,72,79,80,81,82,83,84,85,86,87,89,91,92,95,97,98,99,100,43,50"))
                    {
                        ArquivoDownload adCartasRel1Pdf = new ArquivoDownload();
                        adCartasRel1Pdf.nome_arquivo = relatorio_nome_cartas_rel1 + ".pdf";
                        adCartasRel1Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRel1Pdf.nome_arquivo;
                        adCartasRel1Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRel1Pdf.caminho_arquivo);
                    }
                    //CARTAS_REL_2.rpt - cteep 43
                    if (InicializaRelatorioCartasRel2(ddlTipoRelatorio.SelectedValue, "43"))
                    {
                        ArquivoDownload adCartasRel2Pdf = new ArquivoDownload();
                        adCartasRel2Pdf.nome_arquivo = relatorio_nome_cartas_rel2 + "cteep" + ".pdf";
                        adCartasRel2Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRel2Pdf.nome_arquivo;
                        adCartasRel2Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRel2Pdf.caminho_arquivo);

                    }
                    //CARTAS_REL_2.rpt - elektro 50
                    if (InicializaRelatorioCartasRel2(ddlTipoRelatorio.SelectedValue, "50"))
                    {
                        ArquivoDownload adCartasRel2Pdf = new ArquivoDownload();
                        adCartasRel2Pdf.nome_arquivo = relatorio_nome_cartas_rel2 + "elektro" + ".pdf";
                        adCartasRel2Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRel2Pdf.nome_arquivo;
                        adCartasRel2Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRel2Pdf.caminho_arquivo);
                    }
                    //CARTAS_REL_2.rpt - cpfl 2,62,66,71,72,79,80,81,82,83,84,85,86,87,89,91,92,95,97,98,99,100
                    if (InicializaRelatorioCartasRel2(ddlTipoRelatorio.SelectedValue, "2,62,66,71,72,79,80,81,82,83,84,85,86,87,89,91,92,95,97,98,99,100"))
                    {
                        ArquivoDownload adCartasRel2Pdf = new ArquivoDownload();
                        adCartasRel2Pdf.nome_arquivo = relatorio_nome_cartas_rel2 + "cpfl" + ".pdf";
                        adCartasRel2Pdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRel2Pdf.nome_arquivo;
                        adCartasRel2Pdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRel2Pdf.caminho_arquivo);
                    }

                    if (Directory.GetFiles(Pasta_Server).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip");

                        //Download do arquivo
                        ArquivoDownload adZipCartas = new ArquivoDownload();
                        adZipCartas.nome_arquivo = "CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip";
                        adZipCartas.caminho_arquivo = Pasta_Server_Download + "\\" + adZipCartas.nome_arquivo;
                        Session[ValidaCaracteres(adZipCartas.nome_arquivo)] = adZipCartas;
                        string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipCartas.nome_arquivo);
                        AbrirNovaAba(UpdatePanel, fUrl, adZipCartas.nome_arquivo);
                    }

                    if (Directory.Exists(Pasta_Server))
                    {
                        Directory.Delete(Pasta_Server, true);

                    }
                }

                #endregion

                #region .: Troca :.

                else if (ddlTipoRelatorio.SelectedValue == "TROCA")
                {
                    //TROCA.rpt
                    if (InicializaRelatorioTroca())
                    {
                        ArquivoDownload adTrocaPdf = new ArquivoDownload();
                        adTrocaPdf.nome_arquivo = relatorio_nome_troca + ".pdf";
                        adTrocaPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adTrocaPdf.nome_arquivo;
                        adTrocaPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adTrocaPdf.caminho_arquivo);
                    }
                    //etq.rpt
                    if (InicializaRelatorioEtiqueta(ddlTipoRelatorio.SelectedValue, DateTime.Now.ToShortDateString()))
                    {
                        ArquivoDownload adExtratoPdf = new ArquivoDownload();
                        adExtratoPdf.nome_arquivo = relatorio_nome_etq + ".pdf";
                        adExtratoPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adExtratoPdf.nome_arquivo;
                        adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                    }

                    //qtde_cartas.rpt
                    if (InicializaRelatorioQtdCartas(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adQtdCartasPdf = new ArquivoDownload();
                        adQtdCartasPdf.nome_arquivo = relatorio_nome_qtde_cartas + ".pdf";
                        adQtdCartasPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adQtdCartasPdf.nome_arquivo;
                        adQtdCartasPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adQtdCartasPdf.caminho_arquivo);
                    }

                    //cartas_rel.rpt
                    if (InicializaRelatorioCartasRel(ddlTipoRelatorio.SelectedValue))
                    {
                        ArquivoDownload adCartasRelPdf = new ArquivoDownload();
                        adCartasRelPdf.nome_arquivo = relatorio_nome_cartas_rel + ".pdf";
                        adCartasRelPdf.caminho_arquivo = Pasta_Server + DateTime.Now.ToString("ddMMyyyy") + "_" + adCartasRelPdf.nome_arquivo;
                        adCartasRelPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adCartasRelPdf.caminho_arquivo);

                    }

                    if (Directory.GetFiles(Pasta_Server).Length > 0)
                    {
                        ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip");

                        //Download do arquivo
                        ArquivoDownload adZipCartas = new ArquivoDownload();
                        adZipCartas.nome_arquivo = "CARTAS" + ddlTipoRelatorio.SelectedValue + DateTime.Now.ToString("ddMMyyyy") + ".zip";
                        adZipCartas.caminho_arquivo = Pasta_Server_Download + "\\" + adZipCartas.nome_arquivo;
                        Session[ValidaCaracteres(adZipCartas.nome_arquivo)] = adZipCartas;
                        string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adZipCartas.nome_arquivo);
                        AbrirNovaAba(UpdatePanel, fUrl, adZipCartas.nome_arquivo);
                    }

                    if (Directory.Exists(Pasta_Server))
                    {
                        Directory.Delete(Pasta_Server, true);

                    }
                }

                #endregion

            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);

            }
        }


        #endregion

        #region .: Métodos :.

        //adesao.rpt
        private bool InicializaRelatorioAdesao(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_adesao;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_adesao;
            relatorio.parametros.Add(new Parametro() { parametro = "tipo_carta", valor = tipoRelatorio });

            Session[relatorio_nome_adesao] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_adesao;
            return true;
        }
        //etq.rpt
        private bool InicializaRelatorioEtiqueta(string tipoRelatorio, string dat_emissao)
        {

            relatorio.titulo = relatorio_titulo_etq;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_etq;
            relatorio.parametros.Add(new Parametro() { parametro = "Carta", valor = tipoRelatorio });
            relatorio.parametros.Add(new Parametro() { parametro = "Data_emissao", valor = dat_emissao });

            Session[relatorio_nome_etq] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_etq;
            return true;
        }
        //qtde_cartas.rpt
        private bool InicializaRelatorioQtdCartas(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_qtde_cartas;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_qtde_cartas;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });

            Session[relatorio_nome_qtde_cartas] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_qtde_cartas;
            return true;
        }
        //cartas_rel.rpt
        private bool InicializaRelatorioCartasRel(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_cartas_rel;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_cartas_rel;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });

            Session[relatorio_nome_cartas_rel] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_cartas_rel;
            return true;
        }
        //CANCELAMENTO_CONGENERE.rpt
        private bool InicializaRelatorioCancCongenere(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_canc_congenere;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_canc_congenere;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });

            Session[relatorio_nome_canc_congenere] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_canc_congenere;
            return true;
        }
        //ETQ_CONGENERE.rpt
        private bool InicializaRelatorioEtiquetaCongenere(string tipoRelatorio, string dat_emissao)
        {

            relatorio.titulo = relatorio_titulo_etq_congenere;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_etq_congenere;
            relatorio.parametros.Add(new Parametro() { parametro = "Carta", valor = tipoRelatorio });
            relatorio.parametros.Add(new Parametro() { parametro = "Data_emissao", valor = dat_emissao });

            Session[relatorio_nome_etq_congenere] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_etq_congenere;
            return true;
        }
        //QTDE_CARTAS_CONGENERE.rpt
        private bool InicializaRelatorioQtdCartasCongenere(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_qtde_carta_congenere;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_qtde_carta_congenere;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });

            Session[relatorio_nome_qtde_carta_congenere] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_qtde_carta_congenere;
            return true;
        }
        //CARTAS_REL_CONGENERE.rpt
        private bool InicializaRelatorioCartasRelCongenere(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_carta_congenere;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_carta_congenere;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });

            Session[relatorio_nome_carta_congenere] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_carta_congenere;
            return true;
        }
        //CANCELAMENTO.rpt
        private bool InicializaRelatorioCancelamento(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_cancelamento;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_cancelamento;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });

            Session[relatorio_nome_cancelamento] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_cancelamento;
            return true;
        }
        //ETQ_CANCELAMENTO.rpt
        private bool InicializaRelatorioEtiquetaCancelamento(string tipoRelatorio, string dat_emissao)
        {

            relatorio.titulo = relatorio_titulo_etq_cancelamento;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_etq_cancelamento;
            relatorio.parametros.Add(new Parametro() { parametro = "Carta", valor = tipoRelatorio });
            relatorio.parametros.Add(new Parametro() { parametro = "Data_emissao", valor = dat_emissao });
             
            Session[relatorio_nome_etq_cancelamento] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_etq_cancelamento;
            return true;
        }
        //CANCELAMENTO_412.rpt
        private bool InicializaRelatorioCancelamento412(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_cancelamento412;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_cancelamento412;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = "CANCELAMENTO" });

            Session[relatorio_nome_cancelamento412] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_cancelamento412;
            return true;
        }
        //cancelamento_24.rpt
        private bool InicializaRelatorioCancelamento24()
        {

            relatorio.titulo = relatorio_titulo_cancelamento24;
             relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_cancelamento24;
      
            Session[relatorio_nome_cancelamento24] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_cancelamento24;
            return true;
        }
        //REV_APOSENTADO.rpt
        private bool InicializaRelatorioRevAposentado(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_rev_aposentado;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_rev_aposentado;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });

            Session[relatorio_nome_rev_aposentado] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_rev_aposentado;
            return true;
        }

        //QTDE_CARTAS_ATIVOS_1.rpt
        private bool InicializaRelatorioQtdeCartaAtivos(string tipoRelatorio, string empresa)
        {

            relatorio.titulo = relatorio_titulo_qtde_carta_ativo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_qtde_carta_ativo;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });
            relatorio.parametros.Add(new Parametro() { parametro = "COD_EMPRS", valor = empresa });
       
            Session[relatorio_nome_qtde_carta_ativo] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_qtde_carta_ativo;
            return true;
        }
        //QTDE_CARTAS_ATIVOS_2.rpt
        private bool InicializaRelatorioQtdeCartaAtivos2(string tipoRelatorio, string empresa)
        {

            relatorio.titulo = relatorio_titulo_qtde_carta_ativo2;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_qtde_carta_ativo2;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });
            relatorio.parametros.Add(new Parametro() { parametro = "COD_EMPRS", valor = empresa });
         
            Session[relatorio_nome_qtde_carta_ativo2] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_qtde_carta_ativo2;
            return true;
        }
        //REV_ATIVO.rpt
        private bool InicializaRelatorioRevAtivo(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_rev_ativo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_rev_ativo;
            relatorio.parametros.Add(new Parametro() { parametro = "TIPO", valor = tipoRelatorio });
                 
            Session[relatorio_nome_rev_ativo] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_rev_ativo;
            return true;
        }
        //REV_ATIVO_CTEEP.rpt
        private bool InicializaRelatorioRevAtivoCteep(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_rev_ativo_cteep;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_rev_ativo_cteep;
            relatorio.parametros.Add(new Parametro() { parametro = "TIPO", valor = tipoRelatorio });

            Session[relatorio_nome_rev_ativo_cteep] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_rev_ativo_cteep;
            return true;
        }
        //REV_ATIVO_ELEKTRO.rpt
        private bool InicializaRelatorioRevAtivoElektro(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_rev_ativo_elektro;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_rev_ativo_elektro;
            relatorio.parametros.Add(new Parametro() { parametro = "TIPO", valor = tipoRelatorio });

            Session[relatorio_nome_rev_ativo_elektro] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_rev_ativo_elektro;
            return true;
        }
        //REV_ATIVO_CPFL.rpt
        private bool InicializaRelatorioRevAtivoCpfl()
        {

            relatorio.titulo = relatorio_titulo_rev_ativo_cpfl;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_rev_ativo_cpfl;

            Session[relatorio_nome_rev_ativo_cpfl] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_rev_ativo_cpfl;
            return true;
        }
        //ETQ_ATIVO.rpt
        private bool InicializaRelatorioEtiquetaAtivo(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_etq_ativo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_etq_ativo;
            relatorio.parametros.Add(new Parametro() { parametro = "TIPO", valor = tipoRelatorio });

            Session[relatorio_nome_etq_ativo] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_etq_ativo;
            return true;
        }
        //ETQ_ATIVO_CTEEP.rpt
        private bool InicializaRelatorioEtiquetaAtivoCteep(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_etq_ativo_cteep;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_etq_ativo_cteep;
            relatorio.parametros.Add(new Parametro() { parametro = "TIPO", valor = tipoRelatorio });

            Session[relatorio_nome_etq_ativo_cteep] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_etq_ativo_cteep;
            return true;
        }
        //ETQ_ATIVO_ELEKTRO.rpt
        private bool InicializaRelatorioEtiquetaAtivoElektro(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_etq_ativo_elektro;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_etq_ativo_elektro;
            relatorio.parametros.Add(new Parametro() { parametro = "TIPO", valor = tipoRelatorio });

            Session[relatorio_nome_etq_ativo_elektro] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_etq_ativo_elektro;
            return true;
        }
        //ETQ_ATIVO_CPFL.rpt
        private bool InicializaRelatorioEtiquetaAtivoCpfl(string tipoRelatorio)
        {

            relatorio.titulo = relatorio_titulo_etq_ativo_cpfl;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_etq_ativo_cpfl;
            relatorio.parametros.Add(new Parametro() { parametro = "TIPO", valor = tipoRelatorio });

            Session[relatorio_nome_etq_ativo_cpfl] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_etq_ativo_cpfl;
            return true;
        }
        //CARTAS_REL_1.rpt 
        private bool InicializaRelatorioCartasRel1(string tipoRelatorio, string empresa)
        {

            relatorio.titulo = relatorio_titulo_cartas_rel1;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_cartas_rel1;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });
            relatorio.parametros.Add(new Parametro() { parametro = "COD_EMPRS", valor = empresa });

            Session[relatorio_nome_cartas_rel1] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_cartas_rel1;
            return true;
        }
        //CARTAS_REL_2.rpt
        private bool InicializaRelatorioCartasRel2(string tipoRelatorio, string empresa)
        {

            relatorio.titulo = relatorio_titulo_cartas_rel2;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_cartas_rel2;
            relatorio.parametros.Add(new Parametro() { parametro = "CARTA", valor = tipoRelatorio });
            relatorio.parametros.Add(new Parametro() { parametro = "COD_EMPRS", valor = empresa });

            Session[relatorio_nome_cartas_rel2] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_cartas_rel2;
            return true;
        }
        //TROCA.rpt
        private bool InicializaRelatorioTroca()
        {

            relatorio.titulo = relatorio_titulo_troca;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_troca;
            Session[relatorio_nome_troca] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_troca;
            return true;
        }

        #endregion
    }
}