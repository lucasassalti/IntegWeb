using OfficeOpenXml;
//using IntegWeb.Framework;
//using IntegWeb.Framework.Aplicacao;
//using IntegWeb.Entidades.Carga;
using IntegWeb.Entidades.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace IntegWeb.Investimento.Web
{

    public class DadosOperacao
    {
        public string Fundo { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string CompraVenda { get; set; }
        public string Emissor { get; set; }
        public string Lastro { get; set; }
        public DateTime dtOperacao { get; set; }
        public DateTime dtLiquidacao { get; set; }
        public DateTime dtVencimento { get; set; }
        public Nullable<DateTime> dtEmissao { get; set; }
        public string Pu { get; set; }
        public string Taxa { get; set; }
        public string Qtd { get; set; }
        public string Financeiro { get; set; }
        public string Indexador { get; set; }
        public string Corretora { get; set; }
        public string Contato { get; set; }
        public string Fone { get; set; }
    }


    public partial class DistribuirBoleta : BasePage
    {

        DadosOperacao doDados = new DadosOperacao();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDistribuir_Click(object sender, EventArgs e)
        {
            lblRegistros.Text = "";

            if (fuDistribuicao.HasFile && fuBaseDados.HasFile)
            {
                if (
                   fuDistribuicao.PostedFile.ContentType.Equals("application/vnd.ms-excel") || // formato superior 2003
                   fuDistribuicao.PostedFile.ContentType.Equals("application/vnd.ms-excel.sheet.macroEnabled.12") ||
                   fuBaseDados.PostedFile.ContentType.Equals("application/vnd.ms-excel") || // formato superior 2003
                   fuBaseDados.PostedFile.ContentType.Equals("application/vnd.ms-excel.sheet.macroEnabled.12") ||

                   fuDistribuicao.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || // formato superior 2003
                   fuDistribuicao.PostedFile.ContentType.Equals("application/vnd.ms-excel.sheet.macroEnabled.12") ||
                   fuBaseDados.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || // formato superior 2003
                   fuBaseDados.PostedFile.ContentType.Equals("application/vnd.ms-excel.sheet.macroEnabled.12"))
                {
                    string path_distribuicao = "";
                    string path_basedados = "";
                    string path_txt = "";
                    try
                    {

                        string[] name = Path.GetFileName(fuDistribuicao.FileName).ToString().Split('.');
                        string UploadFilePath = Server.MapPath("UploadFile\\");
                        //path_distribuicao = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
                        path_distribuicao = UploadFilePath + name[0] + "-" + System.DateTime.Now.ToString("yyyy-MM-dd--HH-mm") + "." + name[1];


                        if (!Directory.Exists(UploadFilePath))
                        {
                            Directory.CreateDirectory(UploadFilePath);
                        }


                        fuDistribuicao.SaveAs(path_distribuicao);
                        DataTable dtExcelPedidos = ReadExcelFile(path_distribuicao, 1, 4);
                       
                        //Verificar se o Campo de Código esta nulo 29/08/2016
                        foreach (DataRow drExcelPedidos in dtExcelPedidos.Rows)
                        {


                            if (!drExcelPedidos.Table.Columns.Contains("Código Bradesco"))
                            {
                                throw new Exception("A Coluna do Codigo Bradesco não existe, favor verificar");
                            }
               
                        }

                        DataTable dtPedidos = dtExcelPedidos.Clone();
                        bool blnDADOS = false;
                        foreach (DataRow drPedido in dtExcelPedidos.Rows)
                        {
                            if (!blnDADOS)
                            {
                                if (drPedido[0].ToString().IndexOf("DADOS DA OPERAÇÃO") > -1)
                                {
                                    blnDADOS = true;
                                }
                                else
                                {
                                    if (!String.IsNullOrEmpty(drPedido[0].ToString()))
                                    {
                                        dtPedidos.ImportRow(drPedido);
                                    }
                                }
                            }
                            else
                            {
                                if (drPedido[0].ToString().IndexOf("TÍTULO:") > -1)
                                    doDados.Titulo = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("TIPO:") > -1)
                                    doDados.Tipo = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("COMPRA / VENDA") > -1)
                                    doDados.CompraVenda = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("EMISSOR") > -1)
                                    doDados.Emissor = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("LASTRO FCESP") > -1)
                                    doDados.Lastro = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("DATA OPERAÇÃO:") > -1)
                                    doDados.dtOperacao = DateTime.Parse(drPedido[1].ToString());

                                if (drPedido[0].ToString().IndexOf("DATA LIQUIDAÇÃO:") > -1)
                                    doDados.dtLiquidacao = DateTime.Parse(drPedido[1].ToString());

                                if (drPedido[0].ToString().IndexOf("DATA VENCIMENTO:") > -1)
                                    doDados.dtVencimento = DateTime.Parse(drPedido[1].ToString());

                                if ((drPedido[0].ToString().IndexOf("DATA EMISSÃO:") > -1) &&
                                    (!String.IsNullOrEmpty(drPedido[1].ToString())))
                                    doDados.dtEmissao = DateTime.Parse(drPedido[1].ToString());

                                if (drPedido[0].ToString().IndexOf("PU:") > -1)
                                    doDados.Pu = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("TAXA:") > -1)
                                    doDados.Taxa = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("QUANTIDADE:") > -1)
                                    doDados.Qtd = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("FINANCEIRO:") > -1)
                                    doDados.Financeiro = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("INDEXADOR:") > -1)
                                    doDados.Indexador = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("CORRETORA:") > -1)
                                    doDados.Corretora = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("CONTATO:") > -1)
                                    doDados.Contato = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("FONE:") > -1)
                                    doDados.Fone = drPedido[1].ToString();

                            }
                        }

                        if (doDados.dtEmissao == null)
                        {
                            throw new Exception("Atenção\\n\\nA data emissao esta vazia.\\nMotivo:\\n");
                        }

                        name = Path.GetFileName(fuBaseDados.FileName).ToString().Split('.');
                        //path_basedados = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
                        path_basedados = UploadFilePath + name[0] + "-" + System.DateTime.Now.ToString("yyyy-MM-dd--HH-mm") + "." + name[1];


                        fuBaseDados.SaveAs(path_basedados);

                        DataTable dtExcelBaseDados = ReadExcelFile(path_basedados, "RF");

                        if (dtExcelBaseDados == null)
                        {
                            throw new Exception("Não foi possivel carragar os dados da planiha [ RF ] no arquivo: " + fuBaseDados.FileName);
                        }

                        DataTable dtBaseDados = null;
                        dtBaseDados = PreparaBaseDados(dtExcelBaseDados);

                        DataTable dtDistribuicao = null;
                        dtDistribuicao = ProcessaQuantidades(dtPedidos, dtBaseDados);

                        DataTable dtDistribuicaoFormatada = null;
                        dtDistribuicaoFormatada = FormataQuantidades(dtDistribuicao);

                        AtualizaBaseDados(path_basedados, dtDistribuicao);

                        ArquivoDownload XlsBaseDados = new ArquivoDownload();
                        XlsBaseDados.dados = null;
                        XlsBaseDados.caminho_arquivo = path_basedados;
                        XlsBaseDados.nome_arquivo = fuBaseDados.FileName;

                        path_txt = UploadFilePath + "BOLETA_RF-" + System.DateTime.Now.ToString("yyyy-MM-dd--HH-mm") + ".txt";
                        GeraRelatorioTxt(dtDistribuicaoFormatada, path_txt);

                        ArquivoDownload TxtDistribuido = new ArquivoDownload();
                        TxtDistribuido.dados = null;
                        TxtDistribuido.caminho_arquivo = path_txt;
                        TxtDistribuido.nome_arquivo = Path.GetFileName(path_txt);


                        GeraRelatorio(dtDistribuicaoFormatada, XlsBaseDados, TxtDistribuido);


                    }
                    catch (Exception ex)
                    {

                        if (ex.Message == "Can not open the package. Package is an OLE compound document. If this is an encrypted package, please supply the password")
                        {
                            MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nO formato da planilha excel não é compativel, favor abrir a planilha e salvar como xlsx.\\nMotivo:\\n" + ex.Message);
                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
                        }

                    }
                    finally
                    {
                        fuDistribuicao.FileContent.Dispose();
                        fuDistribuicao.FileContent.Flush();
                        fuDistribuicao.PostedFile.InputStream.Flush();
                        fuDistribuicao.PostedFile.InputStream.Close();
                        File.Delete(path_distribuicao);
                        fuBaseDados.FileContent.Dispose();
                        fuBaseDados.FileContent.Flush();
                        fuBaseDados.PostedFile.InputStream.Flush();
                        fuBaseDados.PostedFile.InputStream.Close();
                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nCarregue apenas arquivos Excel 2003 (.xls/.xlsx) ou superior!");
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nSelecione as duas planilhas para continuar!");
            }

        }

        private DataTable PreparaBaseDados(DataTable dtExcelBaseDados)
        {
            DataTable dtBaseDados = dtExcelBaseDados.Clone();
            dtBaseDados.Columns["DT_AQUISICAO"].DataType = typeof(DateTime);
            dtBaseDados.Columns.Add("QT_DISTRIBUIDA", typeof(Decimal));
            dtBaseDados.Columns.Add("NOVA_QT_DISPONIVEL", typeof(Decimal));
            dtBaseDados.Merge(dtExcelBaseDados, true, MissingSchemaAction.Ignore);
            return dtBaseDados;
        }

        private DataTable ProcessaQuantidades(DataTable dtPedidos, DataTable dtBaseDados)
        {
            Decimal qtd_total = 0;
            DataTable dtDistribuicao = dtBaseDados.Clone();

            //comentado 26/08/2016
            //string strSelect = "(";
            //foreach (string strCondicao in doDados.Titulo.Split(','))
            //{
            //    //strSelect += " RFTP_CD = '" + strCondicao;
            //    strSelect += " RFTP_CD = '" + strCondicao + "' OR";
            //}
            //strSelect = strSelect.Substring(0, strSelect.Length - 3) + ")";
            //strSelect += " AND BAINS_CD = '" + doDados.Emissor + "'";
            //strSelect += " AND DT_VENCIMENTO = '" + doDados.dtVencimento.ToString("dd/MM/yyyy") + " 00:00:00'";


            foreach (DataRow drFundo in dtPedidos.Rows)
            {
                //comentado 26/08/2016
                //string strSelectFundo = strSelect + " AND NOME = '" + drFundo["FUNDOS / CARTEIRAS"] + "'";
                string strSelectFundo = "(" + "CLCLI_CD = '" + drFundo["CÓDIGO BRADESCO"] + "' AND DT_VENCIMENTO = '" + drFundo["VENCIMENTO"] + "')";
                Decimal qtdDistribuir = Decimal.Parse(drFundo["QUANTIDADE"].ToString());
                DataRow[] aEstoque = dtBaseDados.Select(strSelectFundo, "DT_AQUISICAO");
                //DataRow[] aEstoque = dtBaseDados.Select(strSelect, "DT_AQUISICAO");
                if (aEstoque.Length == 0)
                {
                    lblRegistros.Text += "Não foi encontrado ESTOQUE para o seguinte item da BOLETA: " + strSelectFundo + "<BR><BR>";
                }
                foreach (DataRow drItem in aEstoque)
                {
                    Decimal qtdDisponivel = Decimal.Parse(drItem["QT_DISPONIVEL"].ToString());

                    if (qtdDisponivel == 0) continue;

                    if (qtdDisponivel < qtdDistribuir)
                    {
                        drItem["QT_DISTRIBUIDA"] = qtdDisponivel;
                        drItem["NOVA_QT_DISPONIVEL"] = 0;
                        qtdDistribuir -= qtdDisponivel;
                        qtd_total = qtd_total + qtdDisponivel; //<-----
                    }
                    else
                    {
                        drItem["QT_DISTRIBUIDA"] = qtdDistribuir;
                        drItem["NOVA_QT_DISPONIVEL"] = qtdDisponivel - qtdDistribuir;
                        qtd_total = qtd_total + qtdDistribuir; //<-----
                        qtdDistribuir = 0;
                    }

                    dtDistribuicao.ImportRow(drItem);
                    if (qtdDistribuir == 0) break;
                }


            }

            lblRegistros.Text += "Foram distribuidas " + qtd_total.ToString() + " quantidades.<BR><BR>";// de " + Qtd.ToString() + " .";

            lblRegistros.Visible = true;

            return dtDistribuicao;
        }

        private DataTable FormataQuantidades(DataTable dtDistribuicao)
        {
            DataTable dtDistribuicaoFinal = new DataTable();

            dtDistribuicaoFinal.Columns.Add("Data da Operação");
            dtDistribuicaoFinal.Columns.Add("Cód. Cliente", typeof(string));
            dtDistribuicaoFinal.Columns.Add("Título");
            dtDistribuicaoFinal.Columns.Add("Emissor");
            dtDistribuicaoFinal.Columns.Add("Cód Lastro", typeof(string));
            dtDistribuicaoFinal.Columns.Add("Cód. Indexador");
            dtDistribuicaoFinal.Columns.Add("Taxa Ano");
            dtDistribuicaoFinal.Columns.Add("Apropriação");
            dtDistribuicaoFinal.Columns.Add("% Indexador");
            dtDistribuicaoFinal.Columns.Add("Moeda");
            dtDistribuicaoFinal.Columns.Add("Base");
            dtDistribuicaoFinal.Columns.Add("Data de Emissão");
            dtDistribuicaoFinal.Columns.Add("Data de Vencimento");
            dtDistribuicaoFinal.Columns.Add("PU de Emissão");
            dtDistribuicaoFinal.Columns.Add("Taxa Over");
            dtDistribuicaoFinal.Columns.Add("Ativo / Passivo");
            dtDistribuicaoFinal.Columns.Add("Adm / Reserva");
            dtDistribuicaoFinal.Columns.Add("Cetip/Selic");
            dtDistribuicaoFinal.Columns.Add("PU – Negociação");
            dtDistribuicaoFinal.Columns.Add("Quantidade");
            dtDistribuicaoFinal.Columns.Add("Valor Bruto");
            dtDistribuicaoFinal.Columns.Add("Market to Market");
            dtDistribuicaoFinal.Columns.Add("% MTM");
            dtDistribuicaoFinal.Columns.Add("Contraparte");
            dtDistribuicaoFinal.Columns.Add("Contato");
            dtDistribuicaoFinal.Columns.Add("IRRF");
            dtDistribuicaoFinal.Columns.Add("IOF");
            dtDistribuicaoFinal.Columns.Add("VL Líquido");
            dtDistribuicaoFinal.Columns.Add("Compra/Venda");
            dtDistribuicaoFinal.Columns.Add("Operação Vendida");
            dtDistribuicaoFinal.Columns.Add("Clearing", typeof(string));
            dtDistribuicaoFinal.Columns.Add("Local de Custódia", typeof(string));
            dtDistribuicaoFinal.Columns.Add("Prioridade");
            dtDistribuicaoFinal.Columns.Add("Horário de Partida");
            dtDistribuicaoFinal.Columns.Add("Tipo Liq.Financeira", typeof(string));
            dtDistribuicaoFinal.Columns.Add("SubSegmento SPC");
            dtDistribuicaoFinal.Columns.Add("Número de comando");
            dtDistribuicaoFinal.Columns.Add("Cód. Conta Investimento");
            dtDistribuicaoFinal.Columns.Add("Operação a Termo");
            dtDistribuicaoFinal.Columns.Add("Tipo Leilão");
            dtDistribuicaoFinal.Columns.Add("Informa % Face");
            dtDistribuicaoFinal.Columns.Add("% Valor Face");
            dtDistribuicaoFinal.Columns.Add("Data da Liquidação");
            dtDistribuicaoFinal.Columns.Add("NegociaçãoVencimento");

            foreach (DataRow drDados in dtDistribuicao.Rows)
            {
                DataRow drNovaLinha = dtDistribuicaoFinal.NewRow();
                drNovaLinha["Data da Operação"] = doDados.dtOperacao.ToString("yyyyMMdd");

                int CLCLI;
                if (drDados["CLCLI_CD"] != null)
                {
                    if (Int32.TryParse(drDados["CLCLI_CD"].ToString(), out CLCLI))
                        drNovaLinha["Cód. Cliente"] = CLCLI.ToString().PadLeft(6, '0'); //<----
                    else
                        drNovaLinha["Cód. Cliente"] = drDados["CLCLI_CD"].ToString();
                }
                drNovaLinha["Título"] = drDados["RFTP_CD"].ToString();
                drNovaLinha["Emissor"] = drDados["BAINS_CD"].ToString();
                drNovaLinha["Cód Lastro"] = drDados["RFLAS_CD"].ToString(); //<----
                drNovaLinha["Cód. Indexador"] = drDados["IDDIR_CD"].ToString();
                drNovaLinha["Taxa Ano"] = ""; //Vazia
                drNovaLinha["Apropriação"] = "D";
                drNovaLinha["% Indexador"] = "100";
                drNovaLinha["Moeda"] = "REAL";
                drNovaLinha["Base"] = "2";
                drNovaLinha["Data de Emissão"] = doDados.dtEmissao.Value.ToString("yyyyMMdd"); //Boleta
                drNovaLinha["Data de Vencimento"] = doDados.dtVencimento.ToString("yyyyMMdd"); //Boleta
                drNovaLinha["PU de Emissão"] = ""; //Vazia
                drNovaLinha["Taxa Over"] = ""; //Vazia
                drNovaLinha["Ativo / Passivo"] = "A";
                drNovaLinha["Adm / Reserva"] = "A";
                drNovaLinha["Cetip/Selic"] = drDados["CD_CETIP_SELIC"].ToString(); //<----14/09/2015
                drNovaLinha["PU – Negociação"] = doDados.Pu.ToString(); //drDados[""].ToString(); //L da boleta
                drNovaLinha["Quantidade"] = drDados["QT_DISTRIBUIDA"].ToString();
                drNovaLinha["Valor Bruto"] = (Decimal.Parse(doDados.Pu.ToString()) * Decimal.Parse(drDados["QT_DISTRIBUIDA"].ToString())).ToString("##0.00");//).Replace(".",""); //M das boleta             
                drNovaLinha["Market to Market"] = "F";
                drNovaLinha["% MTM"] = "100";
                drNovaLinha["Contraparte"] = doDados.Corretora.ToString(); //B43 da Boleta
                drNovaLinha["Contato"] = ""; //Vazia
                drNovaLinha["IRRF"] = ""; //Vazia
                drNovaLinha["IOF"] = ""; //Vazia
                drNovaLinha["VL Líquido"] = ""; //Vazia
                string caseSwitch = doDados.CompraVenda.ToString();
                switch (caseSwitch)
                {
                    case "VENDA":
                        drNovaLinha["Compra/Venda"] = "V";
                        break;
                    case "COMPRA":
                        drNovaLinha["Compra/Venda"] = "C";
                        break;
                    case "TROCA":
                        drNovaLinha["Compra/Venda"] = "T";
                        break;
                }
                drNovaLinha["Operação Vendida"] = drDados["CD"].ToString(); //<----14/09/2015
                drNovaLinha["Clearing"] = "04"; //<----
                drNovaLinha["Local de Custódia"] = "01"; //<----
                drNovaLinha["Prioridade"] = ""; //Vazia
                drNovaLinha["Horário de Partida"] = ""; //Vazia
                drNovaLinha["Tipo Liq.Financeira"] = "06"; //<----
                drNovaLinha["SubSegmento SPC"] = "RF1";
                drNovaLinha["Número de comando"] = ""; //Vazia
                drNovaLinha["Cód. Conta Investimento"] = ""; //Vazia
                if (doDados.dtLiquidacao == doDados.dtOperacao)
                    drNovaLinha["Operação a Termo"] = "V";
                else
                    drNovaLinha["Operação a Termo"] = "R";
                drNovaLinha["Tipo Leilão"] = "2";
                drNovaLinha["Informa % Face"] = "V"; //Vazia
                drNovaLinha["% Valor Face"] = ""; //Vazia
                drNovaLinha["Data da Liquidação"] = doDados.dtLiquidacao.ToString("yyyyMMdd"); // drDados[""].ToString(); //B35 da boleta
                drNovaLinha["NegociaçãoVencimento"] = "N";

                dtDistribuicaoFinal.Rows.Add(drNovaLinha);

            }

            return dtDistribuicaoFinal;

        }

        public void AtualizaBaseDados(string FilePath, DataTable dtAtualizacoes)
        {
            DataTable dt = new DataTable();
            FileInfo fi = new FileInfo(FilePath);

            // Check if the file exists
            if (!fi.Exists)
                throw new Exception("File " + FilePath + " Does Not Exists");

            ExcelPackage xlPackage = new ExcelPackage(fi);
            ExcelWorksheet ewsRF = xlPackage.Workbook.Worksheets["RF"];

            int colCD = dtAtualizacoes.Columns.IndexOf("CD");
            int colQT_DISPONIVEL = dtAtualizacoes.Columns.IndexOf("QT_DISPONIVEL");
            int start = ewsRF.Dimension.Start.Row;
            int end = ewsRF.Dimension.End.Row;

            foreach (DataRow drAtualiza in dtAtualizacoes.Rows)
            {
                for (int iRow = start; iRow < end; iRow++)
                {
                    if (ewsRF.Cells[iRow, colCD + 1].Value.ToString() == drAtualiza[colCD].ToString())
                    {
                        ewsRF.Cells[iRow, colQT_DISPONIVEL + 1].Value = drAtualiza["NOVA_QT_DISPONIVEL"].ToString();
                        ewsRF.Cells[iRow, colQT_DISPONIVEL + 1].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                    }
                }
            }

            xlPackage.Save();
            xlPackage.Dispose();

        }

        public string GeraRelatorioTxt(DataTable dt, string outputFilePath)
        {
            using (StreamWriter sw = new StreamWriter(outputFilePath, false))
            {
                sw.Write("0#RF\n"); //Cabeçalho

                foreach (DataRow row in dt.Rows)
                {
                    sw.Write("#");//<----14/09/2015
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (!row.IsNull(i))
                        {
                            sw.Write(row[i].ToString() + "#");
                        }
                        else
                        {
                            sw.Write(row[i].ToString() + "=");
                        }
                    }
                    sw.WriteLine();
                }

                sw.Write("99#RF\n"); //Rodapé
                //sw.Close();

                using (MemoryStream ms = new MemoryStream())
                {
                    StreamWriter swt = new StreamWriter(ms);
                    swt = sw;
                    sw.Flush();
                    ms.Position = 0;
                    StreamReader sr = new StreamReader(ms);
                    string myStr = sr.ReadToEnd();

                    return myStr;
                }
            }
        }

        public void GeraRelatorio(DataTable dt, ArquivoDownload XlsBaseDados, ArquivoDownload TxtDistribuido)
        {
            if (dt.Rows.Count > 0)
            {
                ArquivoDownload XlsDistribuicao = new ArquivoDownload();
                XlsDistribuicao.dados = dt;
                XlsDistribuicao.nome_arquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_DISTRIBUICAO_BOLETAS.xls";
                string fullUrl = "";

                Session[XlsDistribuicao.nome_arquivo] = XlsDistribuicao;
                fullUrl = "WebFile.aspx?dwFile=" + XlsDistribuicao.nome_arquivo;
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(this, fullUrl, XlsDistribuicao.nome_arquivo);

                Session[XlsBaseDados.nome_arquivo] = XlsBaseDados;
                fullUrl = "WebFile.aspx?dwFile=" + XlsBaseDados.nome_arquivo;
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(this, fullUrl, XlsBaseDados.nome_arquivo);

                Session[TxtDistribuido.nome_arquivo] = TxtDistribuido;
                fullUrl = "WebFile.aspx?dwFile=" + TxtDistribuido.nome_arquivo;
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(this, fullUrl, TxtDistribuido.nome_arquivo);
            }

        }


    }
}