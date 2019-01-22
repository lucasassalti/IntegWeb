using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Entidades.Framework;

namespace IntegWeb.Saude.Web
{
    public partial class CargaCredReembolso1 : BasePage
    {

        Relatorio relatorio = new Relatorio();
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string relatorio_nome = "Rel_Carga_Credito_Reembolso";
        string relatorio_titulo = "Relatório - Informações da Carga de Crédito";
        string relatorio_simples = @"~/Relatorios/Rel_Carga_Credito_Reembolso.rpt";
        string nome_anexo = "CargaCreditoReembolso";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtVerifica.Visible = false;
            }
        }

        protected void btngerar_Click(object sender, EventArgs e)
        {
            Obter_dataMax();
        }



        protected void btnconsultar_Click(object sender, EventArgs e)
        {
            CreditoReembolsoBLL CredReemb = new CreditoReembolsoBLL();

            DateTime DatIni, DatFim;
            DateTime.TryParse(hiddataini1.Value, out DatIni);
            DateTime.TryParse(hiddataini2.Value, out DatFim);


            CargaCreditoReembolsoTotal totalizador = CredReemb.getBuscaTotalizadorReembolso(DatIni.ToString("dd/MM/yyyy"), DatFim.ToString("dd/MM/yyyy"));

            if (totalizador != null)
            {
                if (!String.IsNullOrEmpty(totalizador.quantidadeUsuario.ToString()) || !String.IsNullOrEmpty(totalizador.quantidadeUsuario.ToString()))
                {
                    txtVerifica.Visible = true;

                    btnconsultar.Visible = true;
                    txtVerifica.Text = @"<div class='n_ok'>
                                                       <h5>Informações::</h5><p>
                                                            <br> ° Quantidade de usuários:  " + totalizador.quantidadeUsuario +
                                                         "  <br> ° Valor total: " + String.Format("{0:C}", totalizador.valorTotal) +
                                                         "  <br> ° Quantidade de Protocolos: " + totalizador.quantidadeProtocolo +
                                             " </p>  </div>";
                }
            }

            else
            {
                txtVerifica.Text = @"<div class='n_warning'>
                                                      <h5>Atenção::</h5>
                                                      <p> Não foi possível encontrar as informações solicitadas. Por favor, valide a data inicial/ final e tente novamente!</p> 
                                    </div>";
            }
        }



        public void Obter_dataMax()
        {
            CreditoReembolsoBLL CredReemb = new CreditoReembolsoBLL();

            DateTime DatIni, DatFim;
            DateTime.TryParse(hiddataini1.Value, out DatIni);
            DateTime.TryParse(hiddataini2.Value, out DatFim);

            int resultado = CredReemb.ConsultarQtdRegistrosCarga(DatIni, DatFim);

            if (resultado == 0)
            {

                txtVerifica.Visible = true;
                btngerar.Visible = false;
                txtVerifica.Text = "<h2>Processando. Aguarde...</h2>";

                CredReemb.ExecutarCargaCredito(DatIni, DatFim);

                resultado = CredReemb.ConsultarQtdRegistrosCarga(DatIni, DatFim);

                if (resultado == 0)
                {
                    txtVerifica.Visible = true;
                    btngerar.Visible = true;
                    txtVerifica.Text = "<div class='n_error'><p>A carga não está disponível no período informado!</p></div>";
                }

                else
                {
                    txtVerifica.Visible = true;
                    txtVerifica.Text = "<div class='n_ok'><p><strong>Carga efetuada com sucesso!</strong> <br> Foram inseridos: " + resultado + " registros no período informado!</p></div> ";

                    geraRelatorio(DatIni.ToString("dd/MM/yyyy"), DatFim.ToString("dd/MM/yyyy"));
                }
            }
            else
            {
                txtVerifica.Visible = true;
                txtVerifica.Text = "<div class='n_warning'><p><strong>Carga já realizada!</strong> <br> Já existem registros gerados para o periodo especificado. Total de registros encontrados: " + resultado + "</p></div> ";
            }

        }

        private void geraRelatorio(string dataInicial, string dataFinal)
        {
            relatorio.titulo = relatorio_titulo;

            relatorio.parametros = new List<Parametro>();
            relatorio.parametros.Add(new Parametro() { parametro = "pDtInicial", valor = dataInicial });
            relatorio.parametros.Add(new Parametro() { parametro = "pDtFinal", valor = dataFinal });

            relatorio.arquivo = relatorio_simples;

            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;

            ArquivoDownload adExtratoPdf = new ArquivoDownload();
            adExtratoPdf.nome_arquivo = nome_anexo + ".pdf";
            adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adExtratoPdf.nome_arquivo;
            ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

            Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
            string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
            AdicionarAcesso(fullUrl);
            AbrirNovaAba(upCadSus, fullUrl, adExtratoPdf.nome_arquivo);
        }


    }
}