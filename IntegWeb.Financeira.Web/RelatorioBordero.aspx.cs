using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IntegWeb.Financeira.Aplicacao.BLL.Tesouraria;
using IntegWeb.Entidades.Framework;
using DocumentFormat.OpenXml.Spreadsheet;
//using Microsoft.Office.Interop.Excel;
using SpreadsheetLight;
using System.Text;

namespace IntegWeb.Financeira.Web
{
    public partial class RelatorioBordero : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGerarRel_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            
            RelatorioBorderoBLL objBLL = new RelatorioBorderoBLL(); 
            //Validações dos campos
            string caminhoSemRateio = Server.MapPath(@"UploadFile\\" + "BorderoPagarSemRateio.xls");

            string caminhoComRateio = Server.MapPath(@"UploadFile\\" + "BorderoPagarComRateio.xls");


            if (rdListPesquisa.SelectedValue == "1")
            {
                if (String.IsNullOrEmpty(txtDtInicial.Text) && String.IsNullOrEmpty(txtDtFinal.Text))
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro: Favor preencher os campos com as datas desejadas");
                    txtDtInicial.Focus();
                }
                else if (String.IsNullOrEmpty(txtDtInicial.Text) && !String.IsNullOrEmpty(txtDtFinal.Text))
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro: Favor preencher o campo 'Data inicial' ");
                    txtDtInicial.Focus();
                }
                else if (!String.IsNullOrEmpty(txtDtInicial.Text) && String.IsNullOrEmpty(txtDtFinal.Text))
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro: Favor preencher o campo 'Data final' ");
                    txtDtFinal.Focus();
                }
                else
                {
                    ds = objBLL.geraRelBorderos(Convert.ToDateTime(txtDtInicial.Text), Convert.ToDateTime(txtDtFinal.Text));
                }

            }
            else if (rdListPesquisa.SelectedValue == "2")
            {
                if (String.IsNullOrEmpty(txtNumBorderoInicial.Text) && String.IsNullOrEmpty(txtNumBorderoFinal.Text))
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro: Favor preencher os campos com o número do borderô ");
                    txtNumBorderoInicial.Focus();
                }
                else if (!String.IsNullOrEmpty(txtNumBorderoInicial.Text) && String.IsNullOrEmpty(txtNumBorderoFinal.Text) && String.IsNullOrEmpty(txtDtFinal.Text) && String.IsNullOrEmpty(txtDtInicial.Text))
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro: Favor preencher o campo 'Nº de borderô final' ");
                    txtNumBorderoFinal.Focus();
                }
                else if (String.IsNullOrEmpty(txtNumBorderoInicial.Text) && !String.IsNullOrEmpty(txtNumBorderoFinal.Text) && String.IsNullOrEmpty(txtDtFinal.Text) && String.IsNullOrEmpty(txtDtInicial.Text))
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro: Favor preencher o campo 'Nº de borderô inicial' ");
                    txtNumBorderoInicial.Focus();
                }
                else
                {
                    string bordInicial = "";
                    string bordFinal = "";

                    if (bordInicial.Length != 6)
                    {
                        bordInicial = formataBordero(txtNumBorderoInicial.Text);
                    }

                    if (bordFinal.Length != 6)
                    {
                        bordFinal = formataBordero(txtNumBorderoFinal.Text);
                    }
                    ds = objBLL.geraRelBorderosNumero(bordInicial, bordFinal);
                }
            }

            if (ds.Tables.Count != 0)
            {
                geraArquivo(ds.Tables[0], 0);
                geraArquivo(ds.Tables[1], 1);

                ArquivoDownload adSemRateio = new ArquivoDownload();
                adSemRateio.nome_arquivo = "BorderoPagarSemRateio.xls";
                adSemRateio.caminho_arquivo = caminhoSemRateio;
                adSemRateio.modo_abertura = System.Net.Mime.DispositionTypeNames.Attachment;
                Session[ValidaCaracteres(adSemRateio.nome_arquivo)] = adSemRateio;
                string fullSemRateio = "WebFile.aspx?dwFile=" + ValidaCaracteres(adSemRateio.nome_arquivo);
                AdicionarAcesso(fullSemRateio);
                AbrirNovaAba(upUpdatePanel, fullSemRateio, adSemRateio.nome_arquivo);

                ArquivoDownload adComRateio = new ArquivoDownload();
                adComRateio.nome_arquivo = "BorderoPagarComRateio.xls";
                adComRateio.caminho_arquivo = caminhoComRateio;
                adComRateio.modo_abertura = System.Net.Mime.DispositionTypeNames.Attachment;
                Session[ValidaCaracteres(adComRateio.nome_arquivo)] = adComRateio;
                string fullComRateio = "WebFile.aspx?dwFile=" + ValidaCaracteres(adComRateio.nome_arquivo);
                AdicionarAcesso(fullComRateio);
                AbrirNovaAba(upUpdatePanel, fullComRateio, adComRateio.nome_arquivo);

                ds.Clear();
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Arquivos gerados com sucesso!");
            }
        
        }
    

        protected String formataBordero(string numBord)
        {
            StringBuilder sbBord = new StringBuilder(numBord);
            for (int i = 0; i < (6 - numBord.Length); i++)
            {
                sbBord.Insert(i, "0");

            }
            return sbBord.ToString();
        }


        protected void geraArquivo(DataTable dt, int tipo)
        {
            SLDocument documento = new SLDocument();
            int linha = 1;
            

            SLPageSettings ps = new SLPageSettings();
            ps.Orientation = OrientationValues.Landscape;
            ps.PaperSize = SLPaperSizeValues.A4Paper;
            documento.SetPageSettings(ps);
            SLStyle row = new SLStyle();
            row.SetFontBold(true);
        
            
            documento.SetCellValue(linha,1,"NUM_BORDERO");
            documento.SetCellValue(linha, 2, "DT_BORDERO");
            documento.SetCellValue(linha, 3, "TIPO");
            documento.SetCellValue(linha, 4, "BANCO_BORD");
            documento.SetCellValue(linha, 5, "AGENCIA_BORD");
            documento.SetCellValue(linha, 6, "NUMCONTA_BORD");
            documento.SetCellValue(linha, 7, "PREFIXO");
            documento.SetCellValue(linha, 8, "NUM_TITULO");
            documento.SetCellValue(linha, 9, "PARCELA");
            documento.SetCellValue(linha, 10, "FORNECEDOR");
            documento.SetCellValue(linha, 11, "NOME");
            documento.SetCellValue(linha, 12, "CNPJ");
            documento.SetCellValue(linha, 13, "DT_EMISSAO");
            documento.SetCellValue(linha, 14, "DT_VENCIMENTO");
            documento.SetCellValue(linha, 15, "VLR_BRUTO");
            documento.SetCellValue(linha, 16, "VLR_ISS");
            documento.SetCellValue(linha, 17, "VLR_IRRF");
            documento.SetCellValue(linha, 18, "VLR_PIS");
            documento.SetCellValue(linha, 19, "VLR_COFINS");
            documento.SetCellValue(linha, 20, "VLR_CSLL");
            documento.SetCellValue(linha, 21, "VLR_LIQ");
            documento.SetCellValue(linha, 22, "COD_NATUREZA");
            documento.SetCellValue(linha, 23, "DESC_NATUREZA");
            documento.SetCellValue(linha, 24, "CONTA_CONTABIL");
            documento.SetCellValue(linha, 25, "CENTRO_CUSTO");
            documento.SetCellValue(linha, 26, "PATROCINADOR");
            documento.SetCellValue(linha, 27, "PLANO");
            documento.SetCellValue(linha, 28, "SUBMASSA");
            documento.SetCellValue(linha, 29, "VALOR_RATEADO");
            documento.SetCellValue(linha, 30, "PERCENTUAL_RATEADO");
            documento.SetCellValue(linha, 31, "COD_FORMA_PAG");
            documento.SetCellValue(linha, 32, "DESC_FORMA_LIQUID");
            documento.SetCellValue(linha, 33, "COD_BARRAS");
            documento.SetCellValue(linha, 34, "PROJETO");

            documento.SetRowStyle(linha, row);

            linha++;

            foreach (DataRow dr in dt.Rows)
            {
                documento.SetCellValue(linha, 1, dr[0].ToString());
                documento.SetCellValue(linha, 2, string.IsNullOrEmpty(dr[1].ToString()) ? "" : Convert.ToDateTime(dr[1]).ToShortDateString());
                documento.SetCellValue(linha, 3, dr[2].ToString());
                documento.SetCellValue(linha, 4, dr[3].ToString());
                documento.SetCellValue(linha, 5, dr[4].ToString());
                documento.SetCellValue(linha, 6, dr[5].ToString());
                documento.SetCellValue(linha, 7, dr[6].ToString());
                documento.SetCellValue(linha, 8, dr[7].ToString());
                documento.SetCellValue(linha, 9, dr[8].ToString());
                documento.SetCellValue(linha, 10, dr[9].ToString());
                documento.SetCellValue(linha, 11, dr[10].ToString());
                documento.SetCellValue(linha, 12, dr[11].ToString());
                documento.SetCellValue(linha, 13, string.IsNullOrEmpty(dr[12].ToString()) ? "" : Convert.ToDateTime(dr[12]).ToShortDateString());
              //  documento.SetCellValue(linha, 14, string.IsNullOrEmpty(dr[13].ToString()) ? "" : Convert.ToDateTime(dr[13]).ToShortDateString());
                documento.SetCellValue(linha,14,Convert.ToDateTime(dr[13].ToString()));
                documento.SetCellValue(linha, 15, dr[14].ToString());
                documento.SetCellValue(linha, 16, dr[15].ToString());
                documento.SetCellValue(linha, 17, dr[16].ToString());
                documento.SetCellValue(linha, 18, dr[17].ToString());
                documento.SetCellValue(linha, 19, dr[18].ToString());
                documento.SetCellValue(linha, 20, dr[19].ToString());
                documento.SetCellValue(linha, 21, dr[20].ToString());
                documento.SetCellValue(linha, 22, dr[21].ToString());
                documento.SetCellValue(linha, 23, dr[22].ToString());
                documento.SetCellValue(linha, 24, dr[23].ToString());
                documento.SetCellValue(linha, 25, dr[24].ToString());
                documento.SetCellValue(linha, 26, dr[25].ToString());
                documento.SetCellValue(linha, 27, dr[26].ToString());
                documento.SetCellValue(linha, 28, dr[27].ToString());
                documento.SetCellValue(linha, 29, dr[28].ToString());
                documento.SetCellValue(linha, 30, dr[29].ToString());
                documento.SetCellValue(linha, 31, dr[30].ToString());
                documento.SetCellValue(linha, 32, dr[31].ToString());
                documento.SetCellValue(linha, 33, dr[32].ToString());
                documento.SetCellValue(linha, 34, dr[33].ToString());
                linha++;
            }

            string caminho = "";

            if (tipo == 0)
                caminho = Server.MapPath(@"UploadFile\\" + "BorderoPagarSemRateio.xls");
            else if (tipo == 1)
                caminho = Server.MapPath(@"UploadFile\\" + "BorderoPagarComRateio.xls");


            documento.SaveAs(caminho);
            //documento.column

        }

        protected void rdListPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdListPesquisa.SelectedValue == "1")
            {
                tbRowData.Visible = true;
                tbRowBord.Visible = false;
            }
            else if (rdListPesquisa.SelectedValue == "2")
            {
                tbRowData.Visible = false;
                tbRowBord.Visible = true;
            }
        }
    }
}