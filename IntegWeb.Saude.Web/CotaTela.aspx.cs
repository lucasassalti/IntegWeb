using Ext.Net;
using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Saude;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpreadsheetLight;

using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;




namespace IntegWeb.Saude.Web
{
    public partial class CotaTela : BasePage
    {
        Cota obj = new Cota();
        CotaBLL objBLL = new CotaBLL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDtCota.Text = DateTime.Today.ToString("dd/MM/yyyy");
                
            }
        }

        private void GerarExcel(int id)
        {
            DataSet ds = objBLL.ListaExcel(id);

            SLDocument sl = new SLDocument();

            SLPageSettings ps = new SLPageSettings();
            ps.Orientation = OrientationValues.Landscape;
            ps.PaperSize = SLPaperSizeValues.A4Paper;

            sl.SetPageSettings(ps);
            string nomeempresa = "";
            int colunaUtilizExecs;

            string dtGeracao = "";

            //Preparação para formatação de borda
            SLStyle bordaAcima = sl.CreateStyle();
            bordaAcima.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

            SLStyle bordaContorno = sl.CreateStyle();
            bordaContorno.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
            bordaContorno.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
            bordaContorno.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
            bordaContorno.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

            SLStyle alinhadoCentro = sl.CreateStyle();
            alinhadoCentro.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            alinhadoCentro.Alignment.Vertical = VerticalAlignmentValues.Center;


            foreach (DataRow linhaEmp in ds.Tables[0].Rows)
            {

                int linhaPlano = int.Parse(linhaEmp["linha_plano"].ToString());
                //Nome da empresa
                nomeempresa = linhaEmp["NOM_ABRVO_EMPRS"].ToString().Replace('/', ' ').Replace('-', ' ');
                sl.AddWorksheet(nomeempresa);
                dtGeracao = linhaEmp["dtGeracao"].ToString();

                DataTable du = ds.Tables[1].Select("codigo =" + linhaEmp["cod_emprs"].ToString()).CopyToDataTable();

                if (du.Rows.Count > 0)
                {
                    //Dados de valores da cota da empresa
                    foreach (DataRow linhaCEmp in du.Rows)
                    {
                        sl.SetCellValue(3, 2, Convert.ToDouble(linhaCEmp["v_amhrateio"]));
                        sl.SetCellValue(4, 2, Convert.ToDouble(linhaCEmp["v_amhexec"]));
                        sl.SetCellValue(7, 2, Convert.ToDouble(linhaCEmp["v_dignarateio"]));
                        sl.SetCellValue(8, 2, Convert.ToDouble(linhaCEmp["v_dignaexec"]));
                        sl.SetCellValue(18, 3, Convert.ToDouble(linhaCEmp["v_conv_deb"]));
                        sl.SetCellValue(18, 4, Convert.ToDouble(linhaCEmp["v_conv_cred"]));
                        sl.SetCellValue(18, 6, Convert.ToDouble(linhaCEmp["v_cota_deb"]));
                        sl.SetCellValue(18, 7, Convert.ToDouble(linhaCEmp["v_cota_cred"]));
                    }
                }
                //Dados de quantidades de participantes da cota da empresa
                DataTable de = ds.Tables[2].Select("codigo =" + linhaEmp["cod_emprs"].ToString()).CopyToDataTable();

                if (de.Rows.Count > 0)
                {
                    foreach (DataRow linhaCeEmp in de.Rows)
                    {
                        linhaPlano++;
                        sl.SetCellValue(linhaPlano, 1, linhaCeEmp["des_plano"].ToString());
                        sl.SetCellValue(linhaPlano, 2, Convert.ToDouble(linhaCeEmp["v_atvtit"]));
                        sl.SetCellValue(linhaPlano, 3, Convert.ToDouble(linhaCeEmp["v_atvagre"]));
                        sl.SetCellValue(linhaPlano, 4, Convert.ToDouble(linhaCeEmp["v_atvdep"]));
                        sl.SetCellValue(linhaPlano, 6, Convert.ToDouble(linhaCeEmp["v_exectit"]));
                        sl.SetCellValue(linhaPlano, 7, Convert.ToDouble(linhaCeEmp["v_execagre"]));
                        sl.SetCellValue(linhaPlano, 8, Convert.ToDouble(linhaCeEmp["v_execdep"]));

                        sl.SetCellValue("E" + linhaPlano, "=SUM(B" + linhaPlano + ":D" + linhaPlano + ")");
                        sl.SetCellValue("I" + linhaPlano, "=SUM(F" + linhaPlano + ":H" + linhaPlano + ")");

                        //Formatação
                        sl.SetCellStyle(linhaPlano, 1, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 2, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 3, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 4, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 5, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 6, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 7, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 8, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 9, bordaContorno);
                    }
                }

                linhaPlano = linhaPlano + 3;
                colunaUtilizExecs = 1;
                //Utilizações de executivos
                var df = ds.Tables[3].Select("codigo =" + linhaEmp["cod_emprs"].ToString());

                if (df.ToList().Count() > 0)
                {
                    foreach (DataRow linhaExec in df.CopyToDataTable().Rows)
                    {
                        colunaUtilizExecs++;
                        sl.SetCellValue(linhaPlano - 1, 1, "Utilizações executivos");
                        sl.SetCellValue(linhaPlano, 1, nomeempresa);
                        sl.SetCellValue(linhaPlano - 1, colunaUtilizExecs, linhaExec["des_plano"].ToString());
                        sl.SetCellValue(linhaPlano, colunaUtilizExecs, Convert.ToDouble(linhaExec["valor"]));

                        //Formatação de borda
                        sl.SetCellStyle(linhaPlano, 1, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 2, bordaContorno);
                        sl.SetCellStyle(linhaPlano, 3, bordaContorno);
                    }
                }

                sl.SetCellValue("B1", nomeempresa);
                sl.SetCellValue("A16", "movimento " + Convert.ToDateTime(txtDtCota.Text).ToString("dd/MM/yyyy") + " gerado em " + dtGeracao);

                sl.MergeWorksheetCells("A16", "B16");
                sl.SetCellValue("A3", "valor AMH para rateio");
                sl.SetCellValue("A4", "valor AMH executivos");
                sl.SetCellValue("A5", "valor total AMH");
                sl.SetCellValue("B5", "=SUM(B3:B4)");

                sl.SetCellValue("A11", "Valor AMH para rateio");
                sl.SetCellValue("A12", "Valor Digna para rateio");
                sl.SetCellValue("A13", "Valor total para rateio");
                sl.SetCellValue("B11", "=B3");
                sl.SetCellValue("B12", "=B7");
                sl.SetCellValue("B13", "=SUM(B11:B12)");

                sl.SetCellValue("A17", "Cotas");
                sl.SetCellValue("A18", nomeempresa);
                sl.SetCellValue("A21", nomeempresa);
                sl.SetCellValue("A24", "Quantidade de participantes");
                sl.SetCellValue("A25", nomeempresa);

                sl.SetCellValue("B17", "participacao");
                sl.SetCellValue("B18", "=B13");
                sl.SetCellValue("C17", "conv_deb");
                sl.SetCellValue("D17", "conv_cred");
                sl.SetCellValue("E17", "despesa");
                sl.SetCellValue("E18", "=(B18-C18+D18)*0.3");
                sl.SetCellValue("F17", "cota_deb");
                sl.SetCellValue("G17", "cota_cred");
                sl.SetCellValue("H17", "a ratear");
                sl.SetCellValue("H18", "=E18-F18+G18");
                sl.SetCellValue("I17", "cota");
                sl.SetCellValue("I18", "=H18/G21");

                sl.SetCellValue("B20", "total das despesas");
                sl.SetCellValue("B21", "=B18-C18+D18");
                sl.SetCellValue("C20", "participação 70%");
                sl.SetCellValue("C21", "=B21*0.7");
                sl.SetCellValue("D20", "participação 30%");
                sl.SetCellValue("D21", "=B21*0.3");
                sl.SetCellValue("E20", "desconto em folha");
                sl.SetCellValue("E21", "=F18-G18");
                sl.SetCellValue("F20", "Liquido a ratear");
                sl.SetCellValue("F21", "=D21-E21");
                sl.SetCellValue("G20", "total de cotas");
                sl.SetCellValue("G21", "=B26+C26");
                sl.SetCellValue("H20", "cota calculada");
                sl.SetCellValue("H21", "=F21/G21");
                
                //Cota cobrada da CESP: 2/3 da cota calculada
                if (linhaEmp["cod_emprs"].ToString() == "1")
                {
                    sl.SetCellValue("I20", "cota cobrada 2/3");
                    sl.SetCellValue("I21", "=H21*2/3");
                }                

                sl.SetCellValue("C24", "Ativos");
                sl.SetCellValue("F24", "Executivos");

                sl.SetCellValue("B25", "Titular");
                sl.SetCellValue("C25", "Agregado");
                sl.SetCellValue("D25", "Dependente");
                sl.SetCellValue("E25", "Total");
                sl.SetCellValue("F25", "Titular");
                sl.SetCellValue("G25", "Agregado");
                sl.SetCellValue("H25", "Dependente");
                sl.SetCellValue("I25", "Total");



                sl.SetCellValue("A7", "Valor Digna Prata II para rateio");
                sl.SetCellValue("A8", "Valor Digna Prata II executivos");
                sl.SetCellValue("A9", "Valor Total Digna Prata II");
                sl.SetCellValue("B9", "=SUM(B7:B8)");


                //Formatação genérica de borda
                sl.SetCellStyle(5, 2, bordaAcima);
                sl.SetCellStyle(9, 2, bordaAcima);
                sl.SetCellStyle(13, 2, bordaAcima);

                sl.SetCellStyle(18, 1, bordaContorno);
                sl.SetCellStyle(18, 2, bordaContorno);
                sl.SetCellStyle(18, 3, bordaContorno);
                sl.SetCellStyle(18, 4, bordaContorno);
                sl.SetCellStyle(18, 5, bordaContorno);
                sl.SetCellStyle(18, 6, bordaContorno);
                sl.SetCellStyle(18, 7, bordaContorno);
                sl.SetCellStyle(18, 8, bordaContorno);
                sl.SetCellStyle(18, 9, bordaContorno);

                sl.SetCellStyle(21, 1, bordaContorno);
                sl.SetCellStyle(21, 2, bordaContorno);
                sl.SetCellStyle(21, 3, bordaContorno);
                sl.SetCellStyle(21, 4, bordaContorno);
                sl.SetCellStyle(21, 5, bordaContorno);
                sl.SetCellStyle(21, 6, bordaContorno);
                sl.SetCellStyle(21, 7, bordaContorno);
                sl.SetCellStyle(21, 8, bordaContorno);
                sl.SetCellStyle(21, 9, bordaContorno);

                //Título do relatório
                sl.InsertRow(1, 2);
                sl.SetCellValue("A1", "Cotas de Rateio");
                sl.SetCellStyle(1, 1, alinhadoCentro);
                sl.MergeWorksheetCells("A1", "I1");

                //AutoFit
                sl.AutoFitColumn(1, 40);

            }


            sl.DeleteWorksheet(SLDocument.DefaultFirstSheetName);

            string caminho = Server.MapPath(@"Spool_Arquivos\\" + "Relatorio_Cotas_" + id + ".xlsx");
            string verificaCaminho = Server.MapPath(@"Spool_Arquivos");
          
            if (Directory.Exists(verificaCaminho))
            {
                sl.SaveAs(caminho);
            }
            else 
            {
                Directory.CreateDirectory(verificaCaminho);
                sl.SaveAs(caminho);
            }
        }
        
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            buscarHist(txtDtCota.Text);
        }

        private void buscarHist(string datMov)
        {

            string valida = ValidaDate(datMov);
            if (string.IsNullOrEmpty(valida))
            {
                grdCota.DataSource = objBLL.ConsultarCota(Convert.ToDateTime(datMov).ToString("dd/MM/yyyy"));
                grdCota.DataBind();
            }
            else
                MostraMensagemTelaUpdatePanel(upCota, valida);
        }

        private string ValidaDate(string dt)
        {

            if (!string.IsNullOrEmpty(dt))

                return "";
            else
                return "Digite a data de movimentação";

        }

        protected void btnGeCota_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {

                    ConectaAD objad = (ConectaAD)Session["objUser"];


                    string valida = ValidaDate(txtDtCota.Text);
                    int ret = 0;
                    //Se tem Relatório sendo gerado
                    if (string.IsNullOrEmpty(valida))
                    {
                        ret = objBLL.GeraCota(objad.login, Convert.ToDateTime(txtDtCota.Text).ToString("dd/MM/yyyy"));
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upCota, valida);

                    if (ret < 1)
                    {
                        MostraMensagemTelaUpdatePanel(upCota, "Já existe uma geração de relatório em andamento. Por favor, aguarde.");
                    }
                    else
                    {
                        GerarExcel(ret);
                        buscarHist(txtDtCota.Text);
                    }
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upCota, "Atenção!\\n\\nNão foi possível concluir a operação.\\n\\nMotivo:\\n\\n" + ex.Message);
            }
            
               
        }

        protected void grdCota_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "GerarCota":
                    int id = int.Parse(e.CommandArgument.ToString());

                    string nomeArquivo = Server.MapPath(@"Spool_Arquivos\\" + "Relatorio_Cotas_" + id + ".xlsx");

                    if (!File.Exists(nomeArquivo))
                    {
                        GerarExcel(id);
                    }
                    UpdateProg1.Visible = false;
                   // Response.Redirect("~/Spool_Arquivos/Relatorio_Cotas_" + id + ".xlsx");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Download", "window.location='Spool_Arquivos/Relatorio_Cotas_" + id + ".xlsx';", true);
                    break;
                default: break;
            }
        }

    }


}