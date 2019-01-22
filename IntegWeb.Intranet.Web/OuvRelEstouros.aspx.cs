using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Intranet.Aplicacao;
using Microsoft.Office.Interop.Excel;
using SpreadsheetLight;
using System.IO;
using IntegWeb.Entidades.Framework;

using DocumentFormat.OpenXml.Spreadsheet;



namespace IntegWeb.Intranet.Web
{
    public partial class OuvRelEstouros : BasePage
    {
        OuvRelEstourosBLL objBLL = new OuvRelEstourosBLL();


        protected void Page_Load(object sender, EventArgs e)
        {

        
        }

        protected void btnGeraRelExcel_Click(object sender, EventArgs e)
        {
            string[] mes = new string[13]
            {
                "",
                "Janeiro",
                "Fevereiro",
                "Marco",
                "Abril",
                "Maio",
                "Junho",
                "Julho",
                "Agosto",
                "Setembro",
                "Outubro",
                "Novembro",
                "Dezembro",
            };

            string anoFinal = Convert.ToDateTime(txtDtFinalRelEstouro.Text).ToString("yyyy");

            string mesFinal = mes[Convert.ToInt32(Convert.ToDateTime(txtDtFinalRelEstouro.Text).ToString("MM"))];


            string locationGeral = "window.location='UploadFile/Relatorio_Estouro "
              + mesFinal + " "
              + anoFinal +".xlsx';";

            string locationEstouro = "window.location='UploadFile/aux1-Estouros "
            + mesFinal + " "
            + anoFinal + ".xlsx';";

            string locationResposta = "window.location='UploadFile/aux2-Respostas "
            + mesFinal + " "
            + anoFinal + ".xlsx';";



            string arquivoGeral = Server.MapPath(@"UploadFile\\" + "Relatorio_Estouro "
              + mesFinal +
              " " + anoFinal + ".xlsx");

            string arquivoEstouro = Server.MapPath(@"UploadFile\\" + "aux1-Estouros "
            + mesFinal +
            " " + anoFinal + ".xlsx");

            string arquivoResposta = Server.MapPath(@"UploadFile\\" + "aux2-Respostas "
             + mesFinal +
              " " + anoFinal + ".xlsx");

            try
            {
                if(File.Exists(arquivoGeral))
                {
                    System.IO.File.Delete(arquivoGeral);
                 }
                GeraArquivoGeral(objBLL.GeraRelatorioGeral(Convert.ToDateTime(txtDtInicioRelEstouro.Text),
                                                              Convert.ToDateTime(txtDtFinalRelEstouro.Text)), mesFinal, anoFinal);
                if (File.Exists(arquivoEstouro))
                {
                    System.IO.File.Delete(arquivoEstouro);
                }
                GeraArquivoEstouros(objBLL.GeraEstouro(Convert.ToDateTime(txtDtInicioRelEstouro.Text),
                                                              Convert.ToDateTime(txtDtFinalRelEstouro.Text)), mesFinal, anoFinal);
                if (File.Exists(arquivoResposta))
                {
                    System.IO.File.Delete(arquivoResposta);
                }

                GeraArquivoResposta(objBLL.GeraResposta(Convert.ToDateTime(txtDtInicioRelEstouro.Text),
                                                        Convert.ToDateTime(txtDtFinalRelEstouro.Text)), mesFinal, anoFinal);


                ArquivoDownload arqDownloadGeral = new ArquivoDownload();
                arqDownloadGeral.nome_arquivo = "Relatorio_Estouro " + mesFinal + " " + anoFinal + ".xlsx";
                arqDownloadGeral.caminho_arquivo = arquivoGeral;
                arqDownloadGeral.modo_abertura = System.Net.Mime.DispositionTypeNames.Attachment;
                Session[ValidaCaracteres(arqDownloadGeral.nome_arquivo)] = arqDownloadGeral;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(arqDownloadGeral.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upRelEstouro, fullUrl, arqDownloadGeral.nome_arquivo);



                ArquivoDownload arqDownloadEstouro = new ArquivoDownload();
                arqDownloadEstouro.nome_arquivo = "aux1-Estouros " + mesFinal + " " + anoFinal + ".xlsx";
                arqDownloadEstouro.caminho_arquivo = arquivoEstouro;
                arqDownloadEstouro.modo_abertura = System.Net.Mime.DispositionTypeNames.Attachment;
                Session[ValidaCaracteres(arqDownloadEstouro.nome_arquivo)] = arqDownloadEstouro;
                fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(arqDownloadEstouro.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upRelEstouro, fullUrl, arqDownloadEstouro.nome_arquivo);


                ArquivoDownload arqDownloadResposta = new ArquivoDownload();
                arqDownloadResposta.nome_arquivo = "aux2-Respostas " + mesFinal + " " + anoFinal + ".xlsx";
                arqDownloadResposta.caminho_arquivo = arquivoResposta;
                arqDownloadResposta.modo_abertura = System.Net.Mime.DispositionTypeNames.Attachment;
                Session[ValidaCaracteres(arqDownloadResposta.nome_arquivo)] = arqDownloadResposta;
                fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(arqDownloadResposta.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upRelEstouro, fullUrl, arqDownloadResposta.nome_arquivo);

            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upRelEstouro, "Atenção!\\n\\nNão foi possível concluir a operação.\\n\\nMotivo:\\n\\n" + ex.Message);
            }
        }

        private void GeraArquivoGeral(DataSet ds, string mes, string ano)
        {
            string dtInicio = Convert.ToDateTime(txtDtInicioRelEstouro.Text).ToString("dd-MM-yyyy");
            string dtFinal = Convert.ToDateTime(txtDtFinalRelEstouro.Text).ToString("dd-MM-yyyy");

            int qtdestouro = 0,resperiodo = 0, linha = 6;

            int previsao = objBLL.GeraTotalEstouros(Convert.ToDateTime(dtInicio), Convert.ToDateTime(dtFinal));

            SLDocument sl = new SLDocument();

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


            //Loops de preenchimento do arquivo
            foreach (DataRow LAreaRel in ds.Tables["AREAS_REL"].Rows)
            {

               foreach (DataRow LQtdEstouro in ds.Tables["QTD_ESTOURO"].Rows)
                {
                    
                    if (LAreaRel[0].ToString() == LQtdEstouro[0].ToString())
                    {
                        qtdestouro = Convert.ToInt32(LQtdEstouro[1]);
                        break;
                    }
                    qtdestouro = 0;
                }

               foreach (DataRow LRespPeriodo in ds.Tables["RESP_PERIODO"].Rows)
               {
                   
                   if (LAreaRel[0].ToString() == LRespPeriodo[0].ToString())
                   {
                       resperiodo = Convert.ToInt32(LRespPeriodo[1]);
                       break;
                   }
                   resperiodo = 0;
               }


                    sl.SetCellValue(linha, 1, LAreaRel[0].ToString());
                    sl.SetCellValue(linha, 2, qtdestouro);
                    sl.SetCellValue(linha, 3, resperiodo);

                    sl.SetCellStyle(linha, 1, bordaContorno);
                    sl.SetCellStyle(linha, 2, bordaContorno);
                    sl.SetCellStyle(linha, 3, bordaContorno);
                    sl.SetCellStyle(linha, 4, bordaContorno);

                    if (qtdestouro != 0 && resperiodo != 0)
                            sl.SetCellValue(linha, 4, "=ROUND(((B" + linha + "/C" + linha + ")*100),2)");
                    else
                     sl.SetCellValue(linha, 4, 0);

                    linha++;
               
            }

            //Atribuindo valores fixos e stylo das celulas
            int auxlinha = linha - 1;

            sl.SetCellValue(1, 1, "Manifestações com previsão de encerramento entre " + dtInicio + " e " + dtFinal);
            sl.SetCellValue(1, 2, previsao.ToString());
            sl.SetCellStyle(1, 1, alinhadoCentro);
            sl.SetCellStyle(1, 2, alinhadoCentro);
            sl.SetCellValue(2, 1, "Respostas das Áreas Gerenciadoras:");
            sl.SetCellValue(2, 2, "=C" + linha + @"&"" ou ""&ROUND(B1/C"+ linha + @",4)*100&""%""");
            sl.SetCellValue(3, 1, "Manifestações encerradas após a previsão:");
            sl.SetCellValue(3, 2, "=B" + linha + @"&"" ou ""&ROUND(B" + linha + @"/B1,4)*100&""%""");


            sl.SetCellValue(5, 1, "Áreas Gerenciadoras");
            sl.SetCellValue(5, 2, "Quantidade de estouros dos prazos");
            sl.SetCellValue(5, 3, "Respostas no período");
            sl.SetCellValue(5, 4, "%");
            sl.SetCellStyle(5, 1, resultBold);
            sl.SetCellStyle(5, 2, resultBold);
            sl.SetCellStyle(5, 3, resultBold);
            sl.SetCellStyle(5, 4, resultBold);

            
            sl.SetCellValue(linha, 1, "Total");
            sl.SetCellValue(linha, 2, "=SUM(B6:B"+ auxlinha +")");
            sl.SetCellValue(linha, 3, "=SUM(C6:C"+ auxlinha +")");
            
            sl.SetCellStyle(linha, 1, resultBold);
            sl.SetCellStyle(linha, 2, resultRed);
            sl.SetCellStyle(linha, 3, resultRed);

            sl.SetRowHeight(5, 38);
            sl.SetColumnWidth(1,39);
            sl.SetRowHeight(1, 40);
            sl.AutoFitColumn(2);
            sl.AutoFitColumn(3);
            sl.SetColumnWidth(4,7);

            sl.SetColumnStyle(1, alinhadoCentro);
            sl.SetColumnStyle(2, alinhadoCentro);
            sl.SetColumnStyle(3, alinhadoCentro);
            sl.SetColumnStyle(4, alinhadoCentro);

            string caminho = Server.MapPath(@"UploadFile\\" + "Relatorio_Estouro " 
                + mes +
                " "+ ano + ".xlsx");

            sl.SaveAs(caminho);
            
        }


        private void GeraArquivoEstouros(System.Data.DataTable dt, string mes, string ano)
        {
            SLDocument sl = new SLDocument();


            SLPageSettings ps = new SLPageSettings();
            ps.Orientation = OrientationValues.Landscape;
            ps.PaperSize = SLPaperSizeValues.A4Paper;
            sl.SetPageSettings(ps);


            int linha = 2, count = 1;

            //Set do cabeçalho
            sl.SetCellValue(1, 2, "CHAMADO");
            sl.SetCellValue(1, 3, "CHAMADO");
            sl.SetCellValue(1, 4, "MANI_NR_SEQUENCIA");
            sl.SetCellValue(1, 5, "AREA_DS_AREA");
            sl.SetCellValue(1, 6, "AREA");
            sl.SetCellValue(1, 7, "RECEBEU");
            sl.SetCellValue(1, 8, "REGISTRO");
            sl.SetCellValue(1, 9, "RESPONDEU");
            sl.SetCellValue(1, 10, "PREVISAO");
            sl.SetCellValue(1, 11, "ENCERROU");
            sl.SetCellValue(1, 12, "TIPO_MANIFESTACAO");
            sl.SetCellValue(1, 13, "GRUPO_MANIFESTACAO");

            //Set layout das colunas
            SLStyle ss = colunaStyleAux();
            sl.SetColumnStyle(1, ss);
            sl.SetColumnStyle(2, ss);
            sl.SetColumnStyle(3, ss);
            sl.SetColumnStyle(4, ss);
            sl.SetColumnStyle(5, ss);
            sl.SetColumnStyle(6, ss);
            sl.SetColumnStyle(7, ss);
            sl.SetColumnStyle(8, ss);
            sl.SetColumnStyle(9, ss);
            sl.SetColumnStyle(10, ss);
            sl.SetColumnStyle(11, ss);
            sl.SetColumnStyle(12, ss);
            sl.SetColumnStyle(13, ss);
            sl.SetColumnWidth(2, 11);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 25);
            sl.SetColumnWidth(5, 45);
            sl.SetColumnWidth(6, 36);
            sl.SetColumnWidth(7, 17);
            sl.SetColumnWidth(8, 17);
            sl.SetColumnWidth(9, 17);
            sl.SetColumnWidth(10, 17);
            sl.SetColumnWidth(11, 17);
            sl.SetColumnWidth(12, 30);
            sl.SetColumnWidth(13, 30);

            sl.SetRowStyle(1, cabecalhoStyleAux());

            foreach (DataRow dr in dt.Rows)
            {
                sl.SetCellValue(linha, 1, count);
                sl.SetCellValue(linha, 2, dr[0].ToString());
                sl.SetCellValue(linha, 3, dr[1].ToString());
                sl.SetCellValue(linha, 4, dr[2].ToString());
                sl.SetCellValue(linha, 5, dr[3].ToString());
                sl.SetCellValue(linha, 6, dr[4].ToString());
                sl.SetCellValue(linha, 7, dr[5].ToString());
                sl.SetCellValue(linha, 8, dr[6].ToString());
                sl.SetCellValue(linha, 9, dr[7].ToString());
                sl.SetCellValue(linha, 10, dr[8].ToString());
                sl.SetCellValue(linha, 11, dr[9].ToString());
                sl.SetCellValue(linha, 12, dr[10].ToString());
                sl.SetCellValue(linha, 13, dr[11].ToString());


                linha++;
                count++;
            }




            string caminho = Server.MapPath(@"UploadFile\\" + "aux1-Estouros "
           + mes +
           " " + ano + ".xlsx");

            sl.SaveAs(caminho);
        }

        private void GeraArquivoResposta(System.Data.DataTable dt, string mes, string ano)
        {
            SLDocument sl = new SLDocument();

            SLPageSettings ps = new SLPageSettings();
            ps.Orientation = OrientationValues.Landscape;
            ps.PaperSize = SLPaperSizeValues.A4Paper;
            sl.SetPageSettings(ps);

            int linha = 2, count = 1;

            //Set do cabeçalho
            sl.SetCellValue(1, 2, "CHAMADO");
            sl.SetCellValue(1, 3, "CHAM_DS_PROTOCOLO");
            sl.SetCellValue(1, 4, "MANIFESTAÇÃO");
            sl.SetCellValue(1, 5, "AREA");
            sl.SetCellValue(1, 6, "ENVIO");
            sl.SetCellValue(1, 7, "RESPOSTA");
            sl.SetCellValue(1, 8, "DATA INICIAL");
            sl.SetCellValue(1, 9, "DATA PREVISAO");
            sl.SetCellValue(1, 10, "DATA ENCERRAMENTO");

            //Set layout das colunas
            SLStyle ss = colunaStyleAux();
            sl.SetColumnStyle(1, ss);
            sl.SetColumnStyle(2, ss);
            sl.SetColumnStyle(3, ss);
            sl.SetColumnStyle(4, ss);
            sl.SetColumnStyle(5, ss);
            sl.SetColumnStyle(6, ss);
            sl.SetColumnStyle(7, ss);
            sl.SetColumnStyle(8, ss);
            sl.SetColumnStyle(9, ss);
            sl.SetColumnStyle(10, ss);
            sl.SetColumnWidth(2, 11);
            sl.SetColumnWidth(3, 22);
            sl.SetColumnWidth(4, 16);
            sl.SetColumnWidth(5, 45);
            sl.SetColumnWidth(6, 11);
            sl.SetColumnWidth(7, 12);
            sl.SetColumnWidth(8, 13);
            sl.SetColumnWidth(9, 16);
            sl.SetColumnWidth(10, 25);

            sl.SetRowStyle(1, cabecalhoStyleAux());

            foreach (DataRow dr in dt.Rows)
            {
                sl.SetCellValue(linha, 1, count);
                sl.SetCellValue(linha, 2, dr[0].ToString());
                sl.SetCellValue(linha, 3, dr[1].ToString());
                sl.SetCellValue(linha, 4, dr[2].ToString());
                sl.SetCellValue(linha, 5, dr[3].ToString());
                sl.SetCellValue(linha, 6, string.IsNullOrEmpty(dr[4].ToString())? "" :Convert.ToDateTime(dr[4]).ToShortDateString());
                sl.SetCellValue(linha, 7, string.IsNullOrEmpty(dr[5].ToString()) ? "" : Convert.ToDateTime(dr[5]).ToShortDateString());
                sl.SetCellValue(linha, 8, string.IsNullOrEmpty(dr[6].ToString()) ? "" : Convert.ToDateTime(dr[6]).ToShortDateString());
                sl.SetCellValue(linha, 9, string.IsNullOrEmpty(dr[7].ToString()) ? "" : Convert.ToDateTime(dr[7]).ToShortDateString());
                sl.SetCellValue(linha, 10, string.IsNullOrEmpty(dr[8].ToString()) ? "" : Convert.ToDateTime(dr[8]).ToShortDateString());


                linha++;
                count++;
            }

            string caminho = Server.MapPath(@"UploadFile\\" + "aux2-Respostas "
          + mes +
          " " + ano + ".xlsx");

            sl.SaveAs(caminho);
        }



        private SLStyle colunaStyleAux()
        {
            SLStyle style = new SLStyle();
            style.SetFont("Microsoft Sans Serif", 10);
            style.SetHorizontalAlignment(HorizontalAlignmentValues.Left);

            return style;
        }

        private SLStyle cabecalhoStyleAux()
        {
            SLStyle sr = new SLStyle();
            sr.SetFontBold(true);
            sr.SetFont("Microsoft Sans Serif", 10);
            sr.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            return sr;
        }


    }
}