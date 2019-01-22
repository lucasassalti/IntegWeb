using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.BLL.Cadastro;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Entidades.Framework;
using System.IO.Compression;
using System.Text;

namespace IntegWeb.Saude.Web
{
    public partial class ExtratorCI : BasePage
    {

        #region .: Propriedades :.

        Relatorio relatorio = new Relatorio();
        string relatorio_titulo = "Relatório CI Emitido";
        string relatorio_caminho = @"~/Relatorios/Rel_CI_Emitido.rpt";
        string relatorio_nome = "RelatorioCiEmitido";
        DateTime? dataProcessamento;

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExtratorCiBLL bll = new ExtratorCiBLL();
                dataProcessamento = bll.GetDataProcessamento();
                Processo_Mensagem.Text = "Último Processamento: " + dataProcessamento;
            }
        }

        #region .: Aba 1 :.

        protected void btnProcessarCI_Click(object sender, EventArgs e)
        {
            ExtratorCiBLL bll = new ExtratorCiBLL();
            FUN_TBL_CONTROLE_PROCESSACI obj = new FUN_TBL_CONTROLE_PROCESSACI();
            var user = (ConectaAD)Session["objUser"];

            Resultado res = bll.ProcessaCI();
            obj.DESC_PROCESSAMENTO = res.Mensagem;
            obj.USU_GERACAO = user.login;
            bll.Inserir(obj);
            MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
            dataProcessamento = bll.GetDataProcessamento();
            Processo_Mensagem.Text = "Último Processamento: " + dataProcessamento;

        }

        #endregion

        #region .: Aba 2 :.

        protected void btnGerarCI_Click(object sender, EventArgs e)
        {

            string caminho_servidor = Server.MapPath(@"~/");
            string Pasta_Server = caminho_servidor + @"\UploadFile";
            string Pasta_Server_Download = caminho_servidor + @"\UploadFile";

            Pasta_Server = Pasta_Server + @"\" + "CARTOES";
            Pasta_Server_Download = Pasta_Server_Download + @"\" + "EXTRAIR";

            if (Directory.Exists(Pasta_Server_Download))
            {
                Directory.Delete(Pasta_Server_Download, true);
            }

            Directory.CreateDirectory(Pasta_Server);
            Directory.CreateDirectory(Pasta_Server_Download);

            if (ddlTipoCartao.SelectedValue == "1")
            {
                //TODOS 

                ExtArqAdesaoPesCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoPesEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaPesCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaPesEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoPesAzulCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoPesAzulEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoAmhEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaPesAzulCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaPesAzulEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaAmhEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqCiPrataCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                //ExtArqCiPrataEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);

                ExtArqAdesaoExtensCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaExtensCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);

                ExtArqAdesaoDigOuroCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigOuroEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigOuroCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigOuroEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigCrstCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigCrstEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigCrstCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigCrstEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigBrnzCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigBrnzEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigBrnzCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigBrnzEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigCespCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigCespEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigCespCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigCespEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);

                ExtArqAdesaoNossoAmpCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoAmpEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoAmpCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoAmpEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoConcCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoConcEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoConcCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoConcEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoConfCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoConfEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoConfCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoConfEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoIntCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoIntEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoIntCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoIntEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoTotCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoTotEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoTotCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoTotEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoRegeCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoRegeEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoRegeCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoRegeEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoRegaCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoRegaEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoRegaCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoRegaEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoPerfeCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoPerfeEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoPerfeCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoPerfeEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoPerfaCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoPerfaEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoPerfaCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoPerfaEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);

                if (Directory.GetFiles(Pasta_Server).Length > 0)
                {
                    ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip");

                    //Download do arquivo
                    ArquivoDownload adXlsCarteirinha = new ArquivoDownload();
                    adXlsCarteirinha.nome_arquivo = "CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    adXlsCarteirinha.caminho_arquivo = Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    Session[ValidaCaracteres(adXlsCarteirinha.nome_arquivo)] = adXlsCarteirinha;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsCarteirinha.nome_arquivo);
                    AbrirNovaAba(upUpdatePanel, fUrl, adXlsCarteirinha.nome_arquivo);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não Há Arquivos para Serem Gerados");
                }

                if (Directory.Exists(Pasta_Server))
                {
                    Directory.Delete(Pasta_Server, true);

                }

                txtData.Text = "";
            }

            else if (ddlTipoCartao.SelectedValue == "2")
            {
                // PES 
                ExtArqAdesaoPesCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoPesEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaPesCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaPesEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoPesAzulCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoPesAzulEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoAmhEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaPesAzulCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaPesAzulEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaAmhEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);

                if (Directory.GetFiles(Pasta_Server).Length > 0)
                {
                    ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip");

                    //Download do arquivo
                    ArquivoDownload adXlsCarteirinha = new ArquivoDownload();
                    adXlsCarteirinha.nome_arquivo = "CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    adXlsCarteirinha.caminho_arquivo = Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    Session[ValidaCaracteres(adXlsCarteirinha.nome_arquivo)] = adXlsCarteirinha;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsCarteirinha.nome_arquivo);
                    AbrirNovaAba(upUpdatePanel, fUrl, adXlsCarteirinha.nome_arquivo);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não Há Arquivos PES para Gerados");
                }

                if (Directory.Exists(Pasta_Server))
                {
                    Directory.Delete(Pasta_Server, true);
                }

                txtData.Text = "";
            }

            else if (ddlTipoCartao.SelectedValue == "3")
            {
                ExtArqCiPrataCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                //ExtArqCiPrataEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);

                if (Directory.GetFiles(Pasta_Server).Length > 0)
                {
                    ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip");

                    //Download do arquivo
                    ArquivoDownload adXlsCarteirinha = new ArquivoDownload();
                    adXlsCarteirinha.nome_arquivo = "CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    adXlsCarteirinha.caminho_arquivo = Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    Session[ValidaCaracteres(adXlsCarteirinha.nome_arquivo)] = adXlsCarteirinha;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsCarteirinha.nome_arquivo);
                    AbrirNovaAba(upUpdatePanel, fUrl, adXlsCarteirinha.nome_arquivo);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não Há Arquivos AUX.MEDICAMENTO para Gerados");
                }

                if (Directory.Exists(Pasta_Server))
                {
                    Directory.Delete(Pasta_Server, true);
                }

                txtData.Text = "";
            }

            else if (ddlTipoCartao.SelectedValue == "4")
            {
                //EXTENSIVE 
                ExtArqAdesaoExtensCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaExtensCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);


                if (Directory.GetFiles(Pasta_Server).Length > 0)
                {
                    ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip");

                    //Download do arquivo
                    ArquivoDownload adXlsCarteirinha = new ArquivoDownload();
                    adXlsCarteirinha.nome_arquivo = "CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    adXlsCarteirinha.caminho_arquivo = Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    Session[ValidaCaracteres(adXlsCarteirinha.nome_arquivo)] = adXlsCarteirinha;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsCarteirinha.nome_arquivo);
                    AbrirNovaAba(upUpdatePanel, fUrl, adXlsCarteirinha.nome_arquivo);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não Há Arquivos EXTENSIVE para Serem Gerados");
                }

                if (Directory.Exists(Pasta_Server))
                {
                    Directory.Delete(Pasta_Server, true);
                }
                txtData.Text = "";
            }

            else if (ddlTipoCartao.SelectedValue == "5")
            {
                //DIGNA 

                ExtArqAdesaoDigOuroCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigOuroEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigOuroCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigOuroEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigCrstCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigCrstEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigCrstCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigCrstEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigBrnzCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigBrnzEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigBrnzCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigBrnzEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigCespCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoDigCespEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigCespCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaDigCespEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);

                if (Directory.GetFiles(Pasta_Server).Length > 0)
                {
                    ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip");

                    //Download do arquivo
                    ArquivoDownload adXlsCarteirinha = new ArquivoDownload();
                    adXlsCarteirinha.nome_arquivo = "CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    adXlsCarteirinha.caminho_arquivo = Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    Session[ValidaCaracteres(adXlsCarteirinha.nome_arquivo)] = adXlsCarteirinha;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsCarteirinha.nome_arquivo);
                    AbrirNovaAba(upUpdatePanel, fUrl, adXlsCarteirinha.nome_arquivo);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não Há Arquivos DIGNA para Serem Gerados");
                }

                if (Directory.Exists(Pasta_Server))
                {
                    Directory.Delete(Pasta_Server, true);
                }
                txtData.Text = "";
            }

            else if (ddlTipoCartao.SelectedValue == "6")
            {
                // NOSSO

                ExtArqAdesaoNossoAmpCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoAmpEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoAmpCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoAmpEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoConcCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoConcEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoConcCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoConcEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoConfCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoConfEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoConfCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoConfEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoIntCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoIntEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoIntCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoIntEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoTotCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoTotEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoTotCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoTotEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoRegeCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoRegeEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoRegeCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoRegeEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoRegaCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoRegaEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoRegaCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoRegaEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoPerfeCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoPerfeEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoPerfeCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoPerfeEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoPerfaCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqAdesaoNossoPerfaEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoPerfaCorreio(Convert.ToDateTime(txtData.Text), Pasta_Server);
                ExtArqSegViaNossoPerfaEmpresa(Convert.ToDateTime(txtData.Text), Pasta_Server);

                if (Directory.GetFiles(Pasta_Server).Length > 0)
                {
                    ZipFile.CreateFromDirectory(Pasta_Server, Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip");

                    //Download do arquivo
                    ArquivoDownload adXlsCarteirinha = new ArquivoDownload();
                    adXlsCarteirinha.nome_arquivo = "CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    adXlsCarteirinha.caminho_arquivo = Pasta_Server_Download + "\\CARTOES" + Convert.ToDateTime(txtData.Text).ToString("ddMMyyyy") + ".zip";
                    Session[ValidaCaracteres(adXlsCarteirinha.nome_arquivo)] = adXlsCarteirinha;
                    string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsCarteirinha.nome_arquivo);
                    AbrirNovaAba(upUpdatePanel, fUrl, adXlsCarteirinha.nome_arquivo);

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não Há Arquivos NOSSO para Serem Gerados");
                }


                if (Directory.Exists(Pasta_Server))
                {
                    Directory.Delete(Pasta_Server, true);
                }
                txtData.Text = "";
            }

        }

        #endregion

        #region .: Aba 3 :.

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorio(txtEmpresa.Text, txtMatricula.Text, txtSubMatricula.Text))
            {
                ArquivoDownload adExtratoPdf = new ArquivoDownload();
                adExtratoPdf.nome_arquivo = relatorio_nome + ".pdf";
                adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adExtratoPdf.nome_arquivo;
                adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upUpdatePanel, fullUrl, adExtratoPdf.nome_arquivo);

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

        #endregion

        #region .: Métodos :.

        #region .: PES :.

        public void ExtArqAdesaoPesCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Vermelho_PES_Correio_Adesão " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Vermelho_PES_Correio_Adesão " + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoPesCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //  StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoPesEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Vermelho_PES_Empresa_Adesao" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Vermelho_PES_Empresa_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoPesEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaPesCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Vermelho_PES_Correio_2_via " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Vermelho_PES_Correio_2_via " + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaPesCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaPesEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Vermelho_PES_Empresa_2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Vermelho_PES_Empresa_2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaPesEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoPesAzulCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Azul_PES_Correio_Adesao" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Azul_PES_Correio_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoPesAzulCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoPesAzulEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Azul_PES_Empresa_Adesao " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Azul_PES_Empresa_Adesao " + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoPesAzulEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoAmhEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Azul_AMH_Empresa_Adesao" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Azul_AMH_Empresa_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoAmhEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaPesAzulCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Azul_PES_Correio_2_via " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Azul_PES_Correio_2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaPesAzulCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaPesAzulEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Azul_PES_Empresa_2_via " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Azul_PES_Empresa_2_via " + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaPesAzulEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaAmhEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Azul_AMH_Empresa_2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Azul_AMH_Empresa_2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaAmhEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqCiPrataCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\EMISSAO_CI_PRATA_CORR" + DateTime.Now.ToString("ddMM")+ ".txt";

            string strFilePath = caminho + "\\EMISSAO_CI_PRATA_CORR" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqCiPrataCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        //public void ExtArqCiPrataEmpresa(DateTime data, string caminho)
        //{
        //    // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\EMISSAO_CI_PRATA_EMPRESA" + DateTime.Now.ToString("ddMM")+ ".txt";

        //    string strFilePath = caminho + "\\EMISSAO_CI_PRATA_EMPRESA" + data.ToString("ddMM") + ".txt";

        //    ExtratorCiBLL bll = new ExtratorCiBLL();

        //    DataTable dt = bll.ArqCiPrataEmpresa(data);

        //    if (dt.Rows.Count > 0)
        //    {
        //        StreamWriter sw = new StreamWriter(strFilePath, false);

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            sw.WriteLine(dr["linha"].ToString());
        //        }

        //        sw.Flush();
        //        sw.Close();
        //        sw.Dispose();
        //    }
        //}


        #endregion

        #region .: EXTENSIVE :.

        public void ExtArqAdesaoExtensCorreio(DateTime data, string caminho)
        {
            //  string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Extensive_Correio_Adesao" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Extensive_Correio_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoExtensCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaExtensCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Extensive_Correio_2VIA" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Extensive_Correio_2VIA" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaExtensCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        #endregion

        #region .: DIGNA OURO/CRISTAL/BRONZE/CESP :.

        public void ExtArqAdesaoDigOuroCorreio(DateTime data, string caminho)
        {

            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_OURO_PRATA_correio_Adesao " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_OURO_PRATA_correio_Adesao " + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoDigOuroCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoDigOuroEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_OURO_PRATA_Empresa_Adesao" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_OURO_PRATA_Empresa_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoDigOuroEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaDigOuroCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_OURO_PRATA_correio2_via  " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_OURO_PRATA_correio2_via  " + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaDigOuroCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaDigOuroEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_OURO_PRATA_Empresa_2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_OURO_PRATA_Empresa_2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaDigOuroEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));


                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoDigCrstCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_CRISTAL_correio_Adesao " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_CRISTAL_correio_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoDigCrstCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));


                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoDigCrstEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_CRISTAL_Empresa_Adesao" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_CRISTAL_Empresa_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoDigCrstEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));


                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaDigCrstCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_CRISTAL_correio2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_CRISTAL_correio2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaDigCrstCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaDigCrstEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_CRISTAL_Empresa_2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_CRISTAL_Empresa_2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaDigCrstEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoDigBrnzCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy")  + "\\CARTOES_PADRAO\\Digna_BRONZE_correio_Adesao " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_BRONZE_correio_Adesao " + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoDigBrnzCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoDigBrnzEmpresa(DateTime data, string caminho)
        {
            //  string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_BRONZE_Empresa_Adesao" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_BRONZE_Empresa_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoDigBrnzEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);
                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaDigBrnzCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_BRONZE_correio2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_BRONZE_correio2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaDigBrnzCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaDigBrnzEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_BRONZE_Empresa_2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_BRONZE_Empresa_2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaDigBrnzEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoDigCespCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_CESP_correio_Adesao " + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_CESP_correio_Adesao " + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoDigCespCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoDigCespEmpresa(DateTime data, string caminho)
        {
            //  string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_CESP_Empresa_Adesao" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_CESP_Empresa_Adesao" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoDigCespEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaDigCespCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_CESP_correio2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_CESP_correio2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaDigCespCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaDigCespEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\Digna_CESP_Empresa_2_via" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\Digna_CESP_Empresa_2_via" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaDigCespEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //  StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        #endregion

        #region .: ARQUIVOS TODOS OS PLANO NOSSO :.

        public void ExtArqAdesaoNossoAmpCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_AMPLIADO_ADESAO_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_AMPLIADO_ADESAO_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoAmpCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoAmpEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_AMPLIADO_ADESAO_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_AMPLIADO_ADESAO_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoAmpEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();

            }
        }

        public void ExtArqSegViaNossoAmpCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_AMPLIADO_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_AMPLIADO_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoAmpCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();

            }
        }

        public void ExtArqSegViaNossoAmpEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_AMPLIADO_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_AMPLIADO_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoAmpEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoConcCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_CONCEITO_ADESAO_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_CONCEITO_ADESAO_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoConcCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();

            }
        }

        public void ExtArqAdesaoNossoConcEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_CONCEITO_ADESAO_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_CONCEITO_ADESAO_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoConcEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();

            }
        }

        public void ExtArqSegViaNossoConcCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_CONCEITO_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_CONCEITO_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoConcCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoConcEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_CONCEITO_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_CONCEITO_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoConcEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoConfCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_CONFORTO_ADESAO_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_CONFORTO_ADESAO_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoConfCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoConfEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_CONFORTO_ADESAO_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_CONFORTO_ADESAO_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoConfEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoConfCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") +  "\\CARTOES_PADRAO\\NOSSO_CONFORTO_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_CONFORTO_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoConfCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoConfEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_CONFORTO_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_CONFORTO_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoConfEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoIntCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_INTEGRAL_ADESAO_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_INTEGRAL_ADESAO_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoIntCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoIntEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_INTEGRAL_ADESAO_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_INTEGRAL_ADESAO_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoIntEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();

            }
        }

        public void ExtArqSegViaNossoIntCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_INTEGRAL_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_INTEGRAL_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoIntCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoIntEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_INTEGRAL_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_INTEGRAL_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoIntEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoTotCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_TOTAL_ADESAO_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_TOTAL_ADESAO_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoTotCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();

            }
        }

        public void ExtArqAdesaoNossoTotEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_TOTAL_ADESAO_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_TOTAL_ADESAO_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoTotEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoTotCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_TOTAL_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_TOTAL_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoTotCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoTotEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_TOTAL_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_TOTAL_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoTotEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoRegeCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_ENFERMARIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_ENFERMARIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoRegeCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoRegeEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_ENFERMARIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_ENFERMARIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoRegeEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoRegeCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_ENFERMARIA_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_ENFERMARIA_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoRegeCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }

        }

        public void ExtArqSegViaNossoRegeEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_ENFERMARIA_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_ENFERMARIA_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoRegeEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoRegaCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_APTO_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_APTO_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoRegaCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoRegaEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_APTO_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_APTO_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoRegaEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoRegaCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_APTO_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_APTO_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoRegaCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoRegaEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_APTO_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_APTO_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoRegaEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }



        public void ExtArqAdesaoNossoPerfeCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_ENFERMARIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_PERFIL_ENFERMARIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoPerfeCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));
                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoPerfeEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_ENFERMARIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_PERFIL_ENFERMARIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoPerfeEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoPerfeCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_ENFERMARIA_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_PERFIL_ENFERMARIA_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoPerfeCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }

        }

        public void ExtArqSegViaNossoPerfeEmpresa(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_ENFERMARIA_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_REGIONAL_SP_ENFERMARIA_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoPerfeEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoPerfaCorreio(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_APTO_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_PERFIL_APTO_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoPerfaCorreio(data);

            if (dt.Rows.Count > 0)
            {
                //StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqAdesaoNossoPerfaEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_APTO_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_PERFIL_APTO_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqAdesaoNossoPerfaEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoPerfaCorreio(DateTime data, string caminho)
        {
            //string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_APTO_2A_VIA_CORR_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_PERFIL_APTO_2A_VIA_CORR_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoPerfaCorreio(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        public void ExtArqSegViaNossoPerfaEmpresa(DateTime data, string caminho)
        {
            // string strFilePath = @"\\Fcespwkpp001\DIRETORIOS\Divisao_Tecnologia_Informacao\Cadastro\CARTOES_" + DateTime.Now.ToString("yyyy") + "\\CARTOES_PADRAO\\NOSSO_REGIONAL_APTO_2A_VIA_EMP_" + DateTime.Now.ToString("ddMM") + ".txt";

            string strFilePath = caminho + "\\NOSSO_PERFIL_APTO_2A_VIA_EMP_" + data.ToString("ddMM") + ".txt";

            ExtratorCiBLL bll = new ExtratorCiBLL();

            DataTable dt = bll.ArqSegViaNossoPerfaEmpresa(data);

            if (dt.Rows.Count > 0)
            {
                // StreamWriter sw = new StreamWriter(strFilePath, false);

                StreamWriter sw = new StreamWriter(strFilePath, true, Encoding.GetEncoding(1252));

                foreach (DataRow dr in dt.Rows)
                {
                    sw.WriteLine(dr["linha"].ToString());
                }
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        #endregion

        private bool InicializaRelatorio(string codEmprs, string matr, string sub)
        {

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = relatorio_caminho;
            relatorio.parametros.Add(new Parametro() { parametro = "Empresa", valor = codEmprs });
            relatorio.parametros.Add(new Parametro() { parametro = "Matricula", valor = matr });
            relatorio.parametros.Add(new Parametro() { parametro = "Subm", valor = sub });


            //      relatorio.parametros.Add(new Parametro() { parametro = "ANDTA_REF", valor = DateTime.Parse(DataBase).ToShortDateString() });


            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;
            return true;
        }

        #endregion


    }
}