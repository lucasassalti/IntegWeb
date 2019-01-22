using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using IntegWeb.Entidades.Framework;
using IntegWeb.Intranet.Aplicacao;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;

namespace IntegWeb.Intranet.Web
{
    public partial class RelatorioFestaAposentados : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                    ListItem forma = new ListItem();
                    ListItem forma2 = new ListItem();
                    ListItem forma3 = new ListItem();
                    forma.Text = "Boleto";
                    forma.Value = "boleto";

                    ddlTipoRel.Items.Clear();
                    ddlTipoRel.Items.Add(forma);

                    forma2.Text = "Folha de pagamento";
                    forma2.Value = "folha";
                    ddlTipoRel.Items.Add(forma2);

                    forma3.Text = "Todos";
                    forma3.Value = "todos";
                    ddlTipoRel.Items.Add(forma3);
            }
            
        }

           

            protected void btnGerarRel_Click(object sender, EventArgs e)
            {
                try
                {
                    string tipo = ddlTipoRel.SelectedItem.Value.ToString();
                    if (String.IsNullOrEmpty(txtDataInicial.Text) && !String.IsNullOrEmpty(txtDataFinal.Text))
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Campo de data inicial vazio, favor preencher");
                        txtDataInicial.Focus();
                    }
                    else if (!String.IsNullOrEmpty(txtDataInicial.Text) && String.IsNullOrEmpty(txtDataFinal.Text))
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Campo de data final vazio, favor preencher");
                        txtDataFinal.Focus();
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(txtDataFinal.Text) && !String.IsNullOrEmpty(txtDataInicial.Text))
                        {
                            string dataInicial = txtDataInicial.Text.Replace("/", "-");
                            string dataFinal = txtDataFinal.Text.Replace("/", "-");
                            RelatorioFestaAposentadosBLL objBLL = new RelatorioFestaAposentadosBLL();


                            string location = "window.location='UploadFile/Relatorio_Festa_Aposentados "
                              + dataInicial + " á "
                              + dataFinal + ".xlsx';";

                            string arquivo = Server.MapPath(@"UploadFile\\" + "Relatorio_Festa_Aposentados "
                              + dataInicial +
                              " á " + dataFinal + ".xlsx");

                            GeraArquivo(objBLL.geraRelatorio(Convert.ToDateTime(txtDataInicial.Text), Convert.ToDateTime(txtDataFinal.Text), tipo), dataInicial, dataFinal);

                            ArquivoDownload arqDownloadGeral = new ArquivoDownload();
                            arqDownloadGeral.nome_arquivo = "Relatorio_Festa_Aposentados " + dataInicial + " á " + dataFinal + ".xlsx";
                            arqDownloadGeral.caminho_arquivo = arquivo;
                            arqDownloadGeral.modo_abertura = System.Net.Mime.DispositionTypeNames.Attachment;
                            Session[ValidaCaracteres(arqDownloadGeral.nome_arquivo)] = arqDownloadGeral;
                            string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(arqDownloadGeral.nome_arquivo);
                            AdicionarAcesso(fullUrl);
                            AbrirNovaAba(UpdatePanel, fullUrl, arqDownloadGeral.nome_arquivo);
                        }
                        else
                        {
                            string dataInicial = "";
                            string dataFinal = "";
                            RelatorioFestaAposentadosBLL objBLL = new RelatorioFestaAposentadosBLL();


                            string location = "window.location='UploadFile/Relatorio_Festa_Aposentados_Geral.xlsx';";

                            string arquivo = Server.MapPath(@"UploadFile\\" + "Relatorio_Festa_Aposentados_Geral.xlsx");

                            GeraArquivo(objBLL.geraRelatorioGeral(tipo), dataInicial, dataFinal);

                            ArquivoDownload arqDownloadGeral = new ArquivoDownload();
                            arqDownloadGeral.nome_arquivo = "Relatorio_Festa_Aposentados_Geral.xlsx";
                            arqDownloadGeral.caminho_arquivo = arquivo;
                            arqDownloadGeral.modo_abertura = System.Net.Mime.DispositionTypeNames.Attachment;
                            Session[ValidaCaracteres(arqDownloadGeral.nome_arquivo)] = arqDownloadGeral;
                            string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(arqDownloadGeral.nome_arquivo);
                            AdicionarAcesso(fullUrl);
                            AbrirNovaAba(UpdatePanel, fullUrl, arqDownloadGeral.nome_arquivo);
                        }
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }


            private void GeraArquivo(System.Data.DataTable dt, string dataInicial, string dataFinal)
            {
                SLDocument sl = new SLDocument();

                int linha = 1;

                //Set das configurações do arquivos e variaveis de style
                SLPageSettings ps = new SLPageSettings();
                ps.Orientation = OrientationValues.Landscape;
                ps.PaperSize = SLPaperSizeValues.A4Paper;
                

                sl.SetPageSettings(ps);

                SLStyle bordaAcima = sl.CreateStyle();
                bordaAcima.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

                SLStyle resultBold = sl.CreateStyle();
                resultBold.SetFontBold(true);
                resultBold.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                resultBold.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                resultBold.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                resultBold.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

                SLStyle resultRed = sl.CreateStyle();
                resultRed.SetFontBold(true);
                resultRed.SetFontColor(System.Drawing.Color.Red);
                resultRed.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                resultRed.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                resultRed.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                resultRed.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

                SLStyle bordaContorno = sl.CreateStyle();
                bordaContorno.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                bordaContorno.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                bordaContorno.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                bordaContorno.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

                SLStyle alinhadoCentro = sl.CreateStyle();
                alinhadoCentro.Alignment.Horizontal = HorizontalAlignmentValues.Center;
                alinhadoCentro.Alignment.Vertical = VerticalAlignmentValues.Center;
                alinhadoCentro.SetWrapText(true);

                sl.SetCellValue(linha, 1, "Código Empresa");
                sl.SetCellValue(linha, 2, "Número do registro do empregado");
                sl.SetCellValue(linha, 3, "Nome");
                sl.SetCellValue(linha, 4, "E-mail");
                sl.SetCellValue(linha, 5, "Data da compra");
                sl.SetCellValue(linha, 6, "Valor da compra");
                sl.SetCellValue(linha, 7, "Quantidade de ingressos");
                sl.SetCellValue(linha, 8, "Parcelas");
                sl.SetCellValue(linha, 9, "Forma de pagamento");
                sl.SetCellValue(linha, 10, "Data de vencimento");

                sl.SetCellStyle(linha, 1, bordaContorno);
                sl.SetCellStyle(linha, 2, bordaContorno);
                sl.SetCellStyle(linha, 3, bordaContorno);
                sl.SetCellStyle(linha, 4, bordaContorno);
                sl.SetCellStyle(linha, 5, bordaContorno);
                sl.SetCellStyle(linha, 6, bordaContorno);
                sl.SetCellStyle(linha, 7, bordaContorno);
                sl.SetCellStyle(linha, 8, bordaContorno);
                sl.SetCellStyle(linha, 9, bordaContorno);
                sl.SetCellStyle(linha, 10, bordaContorno);

                sl.SetCellStyle(linha, 1, resultBold);
                sl.SetCellStyle(linha, 2, resultBold);
                sl.SetCellStyle(linha, 3, resultBold);
                sl.SetCellStyle(linha, 4, resultBold);
                sl.SetCellStyle(linha, 5, resultBold);
                sl.SetCellStyle(linha, 6, resultBold);
                sl.SetCellStyle(linha, 7, resultBold);
                sl.SetCellStyle(linha, 8, resultBold);
                sl.SetCellStyle(linha, 9, resultBold);
                sl.SetCellStyle(linha, 10, resultBold);

                linha++;

                foreach (DataRow dr in dt.Rows)
                {
                    sl.SetCellValue(linha, 1, dr[0].ToString());
                    sl.SetCellValue(linha, 2, dr[1].ToString());
                    sl.SetCellValue(linha, 3, dr[2].ToString());
                    sl.SetCellValue(linha, 4, dr[3].ToString());
                    sl.SetCellValue(linha, 5, dr[4].ToString());
                    sl.SetCellValue(linha, 6, dr[5].ToString());
                    sl.SetCellValue(linha, 7, dr[6].ToString());
                    sl.SetCellValue(linha, 8, dr[7].ToString());
                    sl.SetCellValue(linha, 9, dr[8].ToString());
                    sl.SetCellValue(linha, 10, dr[9].ToString());

                    sl.SetCellStyle(linha, 1, bordaContorno);
                    sl.SetCellStyle(linha, 2, bordaContorno);
                    sl.SetCellStyle(linha, 3, bordaContorno);
                    sl.SetCellStyle(linha, 4, bordaContorno);
                    sl.SetCellStyle(linha, 5, bordaContorno);
                    sl.SetCellStyle(linha, 6, bordaContorno);
                    sl.SetCellStyle(linha, 7, bordaContorno);
                    sl.SetCellStyle(linha, 8, bordaContorno);
                    sl.SetCellStyle(linha, 9, bordaContorno);
                    sl.SetCellStyle(linha, 10, bordaContorno);
                    linha++;
                    
                }
                sl.AutoFitColumn(1);
                sl.AutoFitColumn(2);
                sl.AutoFitColumn(3);
                sl.AutoFitColumn(4);
                sl.AutoFitColumn(5);
                sl.AutoFitColumn(6);
                sl.AutoFitColumn(7);
                sl.AutoFitColumn(8);
                sl.AutoFitColumn(9);
                sl.AutoFitColumn(10);

                string arquivo;

                if (!String.IsNullOrEmpty(dataInicial) && !String.IsNullOrEmpty(dataFinal))
                {
                    arquivo = Server.MapPath(@"UploadFile\\" + "Relatorio_Festa_Aposentados "
                      + dataInicial +
                      " á " + dataFinal + ".xlsx");
                }
                else
                {
                    arquivo = Server.MapPath(@"UploadFile\\" + "Relatorio_Festa_Aposentados_Geral.xlsx");
                }

                sl.SaveAs(arquivo);
            
            }
        
    }
}