using OfficeOpenXml;
using Intranet.Entidades;
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

namespace IntegWeb.Intranet.Web
{

    public class DadosOperacao
    {
        public string Fundo             { get; set; }
        public string Titulo            { get; set; }
        public string Tipo              { get; set; }
        public string CompraVenda       { get; set; }
        public string Emissor           { get; set; }
        public string Lastro            { get; set; }
        public DateTime dtOperacao      { get; set; }
        public DateTime dtLiquidacao    { get; set; }
        public DateTime dtVencimento    { get; set; }
        public DateTime dtEmissao       { get; set; }
        public string Pu                { get; set; }
        public string Taxa              { get; set; }
        public string Qtd               { get; set; }
        public string Financeiro        { get; set; }
        public string Indexador         { get; set; }
        public string Corretora         { get; set; }
        public string Contato           { get; set; }
        public string Fone              { get; set; }
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
                   fuDistribuicao.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || // formato superior 2003
                   fuDistribuicao.PostedFile.ContentType.Equals("application/vnd.ms-excel.sheet.macroEnabled.12")||
                   fuBaseDados.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || // formato superior 2003
                   fuBaseDados.PostedFile.ContentType.Equals("application/vnd.ms-excel.sheet.macroEnabled.12"))
                {
                    string path_distribuicao = "";
                    string path_basedados = "";
                    try
                    {

                        string[] name = Path.GetFileName(fuDistribuicao.FileName).ToString().Split('.');
                        string UploadFilePath = Server.MapPath("UploadFile\\");
                        path_distribuicao = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                        if (!Directory.Exists(UploadFilePath))
                        {
                            Directory.CreateDirectory(UploadFilePath);
                        }

                        fuDistribuicao.SaveAs(path_distribuicao);
                        DataTable dtExcelPedidos = ReadExcelFile(path_distribuicao, 1, 4);
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
                                    doDados.Emissor     = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("LASTRO FCESP") > -1)
                                    doDados.Lastro = drPedido[1].ToString();

                                if (drPedido[0].ToString().IndexOf("DATA OPERAÇÃO:") > -1)
                                    doDados.dtOperacao = DateTime.Parse(drPedido[1].ToString());

                                if (drPedido[0].ToString().IndexOf("DATA LIQUIDAÇÃO:") > -1)
                                    doDados.dtLiquidacao = DateTime.Parse(drPedido[1].ToString());

                                if (drPedido[0].ToString().IndexOf("DATA VENCIMENTO:") > -1)
                                    doDados.dtVencimento = DateTime.Parse(drPedido[1].ToString());

                                if (drPedido[0].ToString().IndexOf("DATA EMISSÃO:") > -1)
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

                        name = Path.GetFileName(fuBaseDados.FileName).ToString().Split('.');
                        path_basedados = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                        fuBaseDados.SaveAs(path_basedados);
                        DataTable dtExcelBaseDados = ReadExcelFile(path_basedados,"RF");

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

                        GeraRelatorio(dtDistribuicaoFormatada, XlsBaseDados);


                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
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
                        //File.Delete(path_basedados);
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
            string strSelect = "(";
            foreach (string strCondicao in doDados.Titulo.Split(','))
            {
                strSelect += " RFTP_CD = '" + strCondicao + "' OR";
            }
            strSelect = strSelect.Substring(0, strSelect.Length - 3) + ")";
            strSelect += " AND BAINS_CD = '" + doDados.Emissor + "'";
            strSelect += " AND DT_VENCIMENTO = '" + doDados.dtVencimento.ToString("dd/MM/yyyy") + " 00:00:00'";
            

            foreach (DataRow drFundo in dtPedidos.Rows)
            {
                string strSelectFundo = strSelect + " AND NOME = '" + drFundo["FUNDOS / CARTEIRAS"] + "'";
                Decimal qtdDistribuir = Decimal.Parse(drFundo["QUANTIDADE"].ToString());
                DataRow[] aEstoque = dtBaseDados.Select(strSelectFundo,"DT_AQUISICAO");
                if (aEstoque.Length == 0)
                {
                    lblRegistros.Text += "Não foi encontrado ESTOQUE para o seguinte item da BOLETA: " + strSelectFundo + "<BR><BR>";
                }
                foreach (DataRow drItem in aEstoque)
                {
                    Decimal qtdDisponivel = Decimal.Parse(drItem["QT_DISPONIVEL"].ToString());
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

            dtDistribuicaoFinal.Columns.Add("Data da Operação"); //ok
            dtDistribuicaoFinal.Columns.Add("Cód. Cliente"); //ok
            dtDistribuicaoFinal.Columns.Add("Título"); //ok
            dtDistribuicaoFinal.Columns.Add("Emissor");
            dtDistribuicaoFinal.Columns.Add("Cód Lastro");
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
            dtDistribuicaoFinal.Columns.Add("Quantidade"); //ok
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
            dtDistribuicaoFinal.Columns.Add("Clearing");
            dtDistribuicaoFinal.Columns.Add("Local de Custódia");
            dtDistribuicaoFinal.Columns.Add("Prioridade");
            dtDistribuicaoFinal.Columns.Add("Horário de Partida");
            dtDistribuicaoFinal.Columns.Add("Tipo Liq.Financeira");
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
                drNovaLinha["Data da Operação"]         = doDados.dtOperacao.ToString("yyyyMMdd");

                int CLCLI;
                if (drDados["CLCLI_CD"] != null)
                {
                    if (Int32.TryParse(drDados["CLCLI_CD"].ToString(), out CLCLI))
                        drNovaLinha["Cód. Cliente"] = CLCLI.ToString().PadLeft(6, '0');
                    else
                        drNovaLinha["Cód. Cliente"] = drDados["CLCLI_CD"].ToString();
                }
                
                drNovaLinha["Título"]                   = drDados["RFTP_CD"].ToString();
                drNovaLinha["Emissor"]                  = drDados["BAINS_CD"].ToString();
                drNovaLinha["Cód Lastro"]               = drDados["RFLAS_CD"].ToString();
                drNovaLinha["Cód. Indexador"]           = drDados["IDDIR_CD"].ToString();
                drNovaLinha["Taxa Ano"]                 = ""; //Vazia
                drNovaLinha["Apropriação"]              = "D";
                drNovaLinha["% Indexador"]              = "100";
                drNovaLinha["Moeda"]                    = "REAL";
                drNovaLinha["Base"]                     = "2";
                drNovaLinha["Data de Emissão"]          = doDados.dtEmissao.ToString("yyyyMMdd"); //Boleta
                drNovaLinha["Data de Vencimento"]       = doDados.dtVencimento.ToString("yyyyMMdd"); //Boleta
                drNovaLinha["PU de Emissão"]            = ""; //Vazia
                drNovaLinha["Taxa Over"]                = ""; //Vazia
                drNovaLinha["Ativo / Passivo"]          = "A";
                drNovaLinha["Adm / Reserva"]            = "A";
                drNovaLinha["Cetip/Selic"]              = drDados["CD_CETIP"].ToString();
                drNovaLinha["PU – Negociação"]          = doDados.Pu.ToString(); //drDados[""].ToString(); //L da boleta
                drNovaLinha["Quantidade"]               = drDados["QT_DISTRIBUIDA"].ToString();
//                drNovaLinha["Valor Bruto"]              = (Decimal.Parse(doDados.Pu.ToString()) * Decimal.Parse(drDados["QT_DISTRIBUIDA"].ToString())).ToString("n2");//).Replace(".",""); //M das boleta
                drNovaLinha["Valor Bruto"]              = (Decimal.Parse(doDados.Pu.ToString()) * Decimal.Parse(drDados["QT_DISTRIBUIDA"].ToString())).ToString("##0.00");//).Replace(".",""); //M das boleta
                
                drNovaLinha["Market to Market"]         = "F";
                drNovaLinha["% MTM"]                    = "100";
                drNovaLinha["Contraparte"]              = doDados.Corretora.ToString(); //B43 da Boleta
                drNovaLinha["Contato"]                  = ""; //Vazia
                drNovaLinha["IRRF"]                     = ""; //Vazia
                drNovaLinha["IOF"]                      = ""; //Vazia
                drNovaLinha["VL Líquido"]               = ""; //Vazia
//                if (doDados.CompraVenda.ToString() == "VENDA")//B31 da Boleta
//                    drNovaLinha["Compra/Venda"] = "V";
//                else
//                    drNovaLinha["Compra/Venda"] = "C";
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
                drNovaLinha["Operação Vendida"]         = drDados["CD"].ToString();
                drNovaLinha["Clearing"]                 = "04";
                drNovaLinha["Local de Custódia"]        = "01";
                drNovaLinha["Prioridade"]               = ""; //Vazia
                drNovaLinha["Horário de Partida"]       = ""; //Vazia
                drNovaLinha["Tipo Liq.Financeira"]      = "06";
                drNovaLinha["SubSegmento SPC"]          = "RF1";
                drNovaLinha["Número de comando"]        = ""; //Vazia
                drNovaLinha["Cód. Conta Investimento"]  = ""; //Vazia
                if (doDados.dtLiquidacao == doDados.dtOperacao)
                    drNovaLinha["Operação a Termo"] = "V";
                    else
                    drNovaLinha["Operação a Termo"] = "R";
                drNovaLinha["Tipo Leilão"]              = "2";
                drNovaLinha["Informa % Face"]           = "V"; //Vazia
                drNovaLinha["% Valor Face"]             = ""; //Vazia
                drNovaLinha["Data da Liquidação"]       = doDados.dtLiquidacao.ToString("yyyyMMdd"); // drDados[""].ToString(); //B35 da boleta
                drNovaLinha["NegociaçãoVencimento"]     = "N";

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

        public void GeraRelatorio(DataTable dt, ArquivoDownload XlsBaseDados)
        {
            if (dt.Rows.Count > 0)
            {
                //ArquivoUpload dtRelatorio = new ArquivoUpload();
                //var nomeArquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_DISTRIBUICAO_BOLETAS.xls";
                ArquivoDownload XlsDistribuicao = new ArquivoDownload();
                XlsDistribuicao.dados = dt;
                XlsDistribuicao.nome_arquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_DISTRIBUICAO_BOLETAS.xls";
                Session[XlsDistribuicao.nome_arquivo] = XlsDistribuicao;
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx?dwFile=distribuicao', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                string fullUrl = "WebFile.aspx?dwFile=" + XlsDistribuicao.nome_arquivo;
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(this, fullUrl, XlsDistribuicao.nome_arquivo);
                //ArquivoDownload XlsBaseDados = new ArquivoDownload();
                Session[XlsBaseDados.nome_arquivo] = XlsBaseDados;
                fullUrl = "WebFile.aspx?dwFile=" + XlsBaseDados.nome_arquivo;
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx?dwFile=basedados', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(this, fullUrl, XlsBaseDados.nome_arquivo);                
            }

        }
    }
}