
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net.Mime;
using System.Text;
using System.Configuration;

public class BasePage : System.Web.UI.Page
{

    OleDbConnection oledbConn;
    public Singlesignon sso_session;
    public void MostraMensagemTela(System.Web.UI.Page pagina, string mensagem)
    {
        pagina.ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.Page), "Alerta", "alert('" + mensagem.Replace("'", " ").Replace("\n", "").Replace("\r", "") + "');", true);
    }

    public bool IsFileOpen(string filePath)
    {
        bool fileOpened = false;
        try
        {
            System.IO.FileStream fs = System.IO.File.OpenWrite(filePath);
            fs.Close();
        }
        catch (System.IO.IOException ex)
        {
            fileOpened = true;
        }

        return fileOpened;
    }


    public void ModalBox(System.Web.UI.Page pagina, string id)
    {
        pagina.ClientScript.RegisterStartupScript(this.GetType(), "reset", " MostraMsg('" + id + "');", true);
    }

    public void MostraMensagemTelaRedirect(System.Web.UI.Page pagina, String mensagem, String pageRedirect)
    {
        pagina.ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.Page), "Alerta", "alert('" + mensagem.Replace("'", " ").Replace("\n", "").Replace("\r", "") + "'); window.location = \"" + pageRedirect + "\";", true);
    }

    //public void MostraMensagemTelaUpdatePanelRedirectTabPanel(UpdatePanel updatePanel, String mensagem, String pageRedirect)
    //{
    //    ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), "Alerta", "alert('" + mensagem.Replace("'", " ").Replace("\n", "").Replace("\r", "") + "');", true);
    //}

    public void MostraMensagemTelaUpdatePanel(UpdatePanel updatePanel, String mensagem)
    {
        ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), "Alerta", "alert('" + mensagem.Replace("'", " ").Replace("\n", "").Replace("\r", "") + "');", true);
    }
    public void MostraMensagemTelaUpdatePanelRedirect(UpdatePanel updatePanel, String mensagem, String pageRedirect)
    {
        ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), "Alerta", "alert('" + mensagem.Replace("'", " ").Replace("\n", "").Replace("\r", "") + "'); window.location = \"" + pageRedirect + "\";", true);
    }
    public void MostraMensagem(Label msgRodape, String mensagem, String tipo = "n_warning")
    {
        if (msgRodape != null)
        {
            msgRodape.Text = String.Format("<div class='{0}'><p>{1}</p></div>", tipo, mensagem.Replace("\\n", "<br>"));
            msgRodape.Visible = true;
        }
    }
    public void MostraMensagemRodape(String mensagem, String tipo = "n_warning")
    {
        Label msgRodape = (Label)Master.FindControl("MensagemRodape");
        if (msgRodape != null)
        {
            msgRodape.Text = String.Format("<div class='{0}'><p>{1}</p></div>", tipo, mensagem.Replace("\\n", "<br>"));
            msgRodape.Visible = true;
        }
    }

    public static void AbrirNovaAba(System.Web.UI.Page page, string fullUrl, string key)
    {
        AdicionarAcesso(fullUrl);
        // string script = "window.open('" + fullUrl + "', '" + key + "', 'status=1,location=1,menubar=1,resizable=1,toolbar=1,scrollbars=1,titlebar=1');";
        string script = "window.open('" + fullUrl + "', '', 'status=1,location=1,menubar=1,resizable=1,toolbar=1,scrollbars=1,titlebar=1');";
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, script, true);
    }
    public static void AbrirNovaAba(UpdatePanel updatePanel, string fullUrl, string key)
    {
        AdicionarAcesso(fullUrl);
        // string script = "window.open('" + fullUrl + "', '" + key + "', 'status=1,location=1,menubar=1,resizable=1,toolbar=1,scrollbars=1,titlebar=1');";
        string script = "window.open('" + fullUrl + "', '', 'status=1,location=1,menubar=1,resizable=1,toolbar=1,scrollbars=1,titlebar=1');";
        ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), key, script, true);
    }

    public void MostraMensagemConfirmacaoUpdatePanelRedirect(UpdatePanel upControl, String mensagem, String pageYesRedirect, String pageNoRedirect)
    {
        var jsScript = String.Empty;
        jsScript += "var answer=confirm(\'" + mensagem + "\');\n";
        jsScript += "if (answer)";
        jsScript += "{window.location = \"" + pageYesRedirect + "\";}";
        jsScript += "else{window.location = \"" + pageNoRedirect + "\";}";
        ScriptManager.RegisterStartupScript(upControl, GetType(), "script", jsScript, true);
    }

    public void CarregarDdl(DropDownList dropDownList, String textField, String valueField, DataTable dtSource)
    {
        dropDownList.DataTextField = textField;
        dropDownList.DataValueField = valueField;
        dropDownList.DataSource = dtSource;
        dropDownList.DataBind();

        dropDownList.Items.Insert(0, new ListItem { Text = "-", Value = "0" });
    }

    public void CarregarGridView(GridView grid, DataTable dtSource)
    {
        grid.DataSource = dtSource;
        grid.DataBind();
    }

    public void CarregarCkBoxList(CheckBoxList cklist, DataTable dtSource, string name, string value)
    {
        cklist.DataTextField = name;
        cklist.DataValueField = value;
        cklist.DataSource = dtSource;
        cklist.DataBind();
    }

    public bool ChkDigitoTiss(string codigoTiss)
    {
        bool functionReturnValue = false;
        int i = 0;
        int J = 0;
        int DC = 0;
        int k = 0;
        // Checa o Dígito de Controle do Código de Serviço

        for (i = 1; i <= 7; i++)
        {
            int j = 0;
            J = Convert.ToInt32(codigoTiss.Substring(k + j, 1));
            DC = DC + J * (9 - i);
            k++;
        }
        DC = 11 - (DC % 11);

        if (DC > 9)
        {
            DC = 0;
        }
        // Verifica se o dígito bate.
        if (DC == (Convert.ToInt32(codigoTiss.Substring(7, 1))))
        {
            functionReturnValue = true;
        }
        else
        {
            functionReturnValue = false;
        }
        return functionReturnValue;
    }

    public void limparListBox(ListBox lst)
    {
        for (int x = lst.Items.Count - 1; x >= 0; x--)
        {
            if (lst.Items[x].Selected)
            {
                lst.Items[x].Selected = false;
            }
        }
    }

    public void SelecionaTodosLstBox(ListBox lst)
    {
        for (int x = lst.Items.Count - 1; x >= 0; x--)
        {
            if (lst.Items[x].Selected == false)
            {
                lst.Items[x].Selected = true;
            }
        }
    }

    public void RemoverlinhasListBox(ListBox lst)
    {
        lst.Items.Clear();
    }

    public Int32 contarLinhaSelecionada(ListBox lst)
    {
        int count = 0;
        for (int x = lst.Items.Count - 1; x >= 0; x--)
        {
            if (lst.Items[x].Selected)
            {
                count++;
            }
        }
        return count;
    }

    public string FormataString(String valor)
    {
        var htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
        String htmlRetorno = String.Empty;
        try
        {
            htmlRegex.Replace(htmlRetorno, String.Empty);
            htmlRetorno = HttpUtility.HtmlDecode(valor);
            htmlRetorno = htmlRetorno.Replace("amp;", String.Empty);
        }
        catch (Exception ex)
        {
            MostraMensagemTela(Page, "Ocorreu um erro: " + ex);
        }
        return htmlRetorno;
    }

    public void ExibeOcultaDivsUpdatePanel(UpdatePanel up, String divExibir, String divOcultar)
    {
        var jsScript = String.Empty;
        jsScript += "var divOcultar = document.getElementById('" + divOcultar + "');\n";
        jsScript += "divOcultar.style.visibility = 'hidden';\n";
        jsScript += "divOcultar.style.display = 'none';\n";
        jsScript += "var divExibir = document.getElementById('" + divExibir + "');";
        jsScript += "divExibir.style.visibility = 'visible';";
        ScriptManager.RegisterStartupScript(up, GetType(), "script", jsScript, true);
    }

    public void ExibeOcultaDivsUpdatePanel(UpdatePanel up, String divExibir, String divOcultar, String divOcultar2)
    {
        var jsScript = String.Empty;
        jsScript += "var divExibir = document.getElementById('" + divExibir + "');";
        jsScript += "divExibir.style.visibility = 'visible';\n";
        jsScript += "var divOcultar = document.getElementById('" + divOcultar + "');\n";
        jsScript += "divOcultar.style.visibility = 'hidden';\n";
        jsScript += "divOcultar.style.display = 'none';\n";
        jsScript += "var divOcultar2 = document.getElementById('" + divOcultar2 + "');\n";
        jsScript += "divOcultar2.style.visibility = 'hidden';\n";
        jsScript += "divOcultar2.style.display = 'none';";

        ScriptManager.RegisterStartupScript(up, GetType(), "script", jsScript, true);
    }

    public void ExibeOcultaDivs(System.Web.UI.Page pagina, String divExibir, String divOcultar)
    {
        var jsScript = String.Empty;
        jsScript += "var divOcultar = document.getElementById('" + divOcultar + "');\n";
        jsScript += "divOcultar.style.visibility = 'hidden';\n";
        jsScript += "divOcultar.style.display = 'none';\n";
        jsScript += "var divExibir = document.getElementById('" + divExibir + "');";
        jsScript += "divExibir.style.visibility = 'visible';";
        ScriptManager.RegisterStartupScript(pagina, typeof(System.Web.UI.Page), "script", jsScript, true);
    }

    public static String ValidaNomeArquivo(String texto)
    {
        var retorno = texto;
        if (texto != null)
        {
            retorno = retorno.Trim().Replace(" ", "_").Replace("\\", "_").Replace("/", "_").Replace("'", String.Empty)
                      .Replace(".", String.Empty);
            retorno = ValidaCaracteres(retorno);
        }
        else
            retorno = string.Empty;

        return retorno;
    }

    public static String ValidaCaracteres(String texto)
    {
        var retorno = texto;
        if (texto != null)
        {
            retorno = retorno.Replace("ã", "a").Replace("â", "a").Replace("á", "a").Replace("à", "a").Replace("ä", "a");
            retorno = retorno.Replace("Ã", "A").Replace("Â", "A").Replace("À", "A").Replace("Ä", "A").Replace("Á", "A");
            retorno = retorno.Replace("õ", "o").Replace("ô", "o").Replace("ó", "o").Replace("ò", "o").Replace("ö", "o");
            retorno = retorno.Replace("Õ", "O").Replace("Ô", "O").Replace("Ó", "O").Replace("Ò", "O").Replace("Ö", "O");
            retorno = retorno.Replace("ê", "e").Replace("é", "e").Replace("è", "e").Replace("ë", "e");
            retorno = retorno.Replace("î", "i").Replace("í", "i").Replace("ì", "i").Replace("ï", "i");
            retorno = retorno.Replace("û", "u").Replace("ú", "u").Replace("ù", "u").Replace("ü", "u");
            retorno = retorno.Replace("ç", "c").Replace("Ç", "C");
            retorno = retorno.Replace("Ê", "E").Replace("É", "E").Replace("È", "E").Replace("Ë", "E");
            retorno = retorno.Replace("Î", "I").Replace("Í", "I").Replace("Ì", "I").Replace("Ï", "I");
            retorno = retorno.Replace("Û", "U").Replace("Ú", "U").Replace("Ù", "U").Replace("Ü", "U");
            retorno = retorno.Replace("º", "O").Replace("ª", "A");
        }
        else
            retorno = string.Empty;

        return retorno;
    }

    public void LimparControles(System.Web.UI.ControlCollection Controls)
    {
        foreach (System.Web.UI.Control c in Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)(c)).Text = string.Empty;
                }
                else if (c.GetType() == typeof(DropDownList))
                {
                    ((DropDownList)(c)).SelectedIndex = ((DropDownList)(c)).Items.Count > 0 ? 0 : -1;
                }

                else if (c.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)(c)).Checked = false;
                }
                //if (c.HasControls)
                //{
                //    EmptyTextBoxes(c, tb);
                //}
            } 
    }

    public void CarregaDropDowDT(DataTable data, DropDownList list)
    {

        list.DataSource = data;
        list.DataTextField = data.Columns[1].ToString();
        list.DataValueField = data.Columns[0].ToString();

        list.DataBind();
        list.Items.Insert(0, new ListItem("---Selecione---", "0"));


    }

    public void CarregaDropDowList(DropDownList drp, List<object> list, string descricao = "Text", string codigo = "Value")
    {
        drp.DataSource = list;
        drp.DataValueField = codigo;
        drp.DataTextField = descricao;
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("---Selecione---", "0"));
    }

    public void CarregarListBox(ListBox lista, List<object> source, string Nome = "Text", string ID = "Value")
    {
        lista.DataSource = source;
        lista.DataTextField = Nome;
        lista.DataValueField = ID;
        lista.DataBind();
    }


    public void CarregaListBox(DataTable data, ListBox list)
    {

        list.DataSource = data;
        list.DataTextField = data.Columns[1].ToString();
        list.DataValueField = data.Columns[0].ToString();

        list.DataBind();
    }

    public void CloneDropDownList(DropDownList source, DropDownList destiny)
    {
        destiny.Items.Clear();
        foreach (ListItem li in source.Items)
        {
            ListItem newItem = new ListItem(li.Text, li.Value);
            newItem.Enabled = li.Enabled;
            destiny.Items.Add(newItem);
        }
    }

    public void ExportarArquivo(String nomeArquivo, DataTable dt, String opcaoArquivo = null)
    {
        switch (nomeArquivo.Split('.').Last().ToUpper())
        {
            case "TXT":
                ResponseTxt(nomeArquivo, dt, opcaoArquivo);
                break;
            default:
                ExportarExcel(nomeArquivo, dt);
                break;
        }
    }

    public void ExportarArquivo(String nomeArquivo, String dt)
    {
        switch (nomeArquivo.Split('.').Last().ToUpper())
        {
            case "TXT":
                ResponseTxt(nomeArquivo, dt);
                break;
        }
    }

    public void ExportarArquivo(String nomeArquivo, String caminhoArquivo, String modo_abertura)
    {
        switch (caminhoArquivo.Split('.').Last().ToUpper())
        {
            case "TXT":
                ResponseTxt(caminhoArquivo, nomeArquivo);
                break;
            case "PDF":
                ResponsePdf(caminhoArquivo, nomeArquivo, modo_abertura);
                break;
            default:
                ResponseExcel(caminhoArquivo, nomeArquivo);
                break;
        }
    }

    public void ExportarArquivo(String nomeArquivo, XmlDocument xdDocument)
    {
        ResponseXml(nomeArquivo, xdDocument);
    }

    public void ExportarExcel_antigo(GridView grdResult, String nomeArquivo, DataTable dt)
    {
        try
        {
            var oResponse = HttpContext.Current.Response;
            oResponse.Clear();
            oResponse.Charset = "";
            nomeArquivo = nomeArquivo.Replace(" ", "");
            oResponse.AddHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
            oResponse.ContentType = "aplication/vnd.ms-excel";
            var stringWrite = new StringWriter();
            var htmlWrite = new HtmlTextWriter(stringWrite);

            var grdSource = new GridView
            {
                ShowHeader = true,
                DataSource = dt == null ? ConvertGridViewToDataTable(grdResult) : dt
            };
            grdSource.DataBind();
            grdSource.RenderControl(htmlWrite);
            oResponse.Write(stringWrite.ToString());
            oResponse.End();
        }
        catch (Exception ex)
        {

            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }

    }

    public void ExportarExcel(String nomeArquivo, GridView grdResult)
    {
        ExportarExcel(nomeArquivo, ConvertGridViewToDataTable(grdResult));
    }

    public static void ExportarExcel(String nomeArquivo, DataTable dt)
    {
        try
        {
            if (dt == null || dt.Columns.Count == 0)
                throw new Exception("ExportToExcel: Null or empty input table!\n");

            ExcelPackage xlPackage = new ExcelPackage();
            ExcelWorksheet ewsRF = xlPackage.Workbook.Worksheets.Add("Plan1");

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ewsRF.Cells[1, (i + 1)].Value = dt.Columns[i].ColumnName;
            }

            // rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ewsRF.Cells[(i + 2), (j + 1)].Value = dt.Rows[i][j];
                }
            }

            MemoryStream mStream = new MemoryStream(xlPackage.GetAsByteArray());
            byte[] bytes = mStream.ToArray(); 

            var oResponse = HttpContext.Current.Response;
            oResponse.Clear();
            oResponse.Charset = "";
            oResponse.AddHeader("Content-Disposition", "attachment; filename=" + nomeArquivo);
            oResponse.ContentType = "aplication/vnd.ms-excel";
            oResponse.OutputStream.Write(bytes, 0, bytes.Length);
            oResponse.End();

        }
        catch (Exception ex)
        {
            throw new Exception("ExportToExcel: \n" + ex.Message);
        }
    }

    public void ResponseExcel(String XlsPath, String filename)
    {
        try
        {
            var oResponse = HttpContext.Current.Response;
            oResponse.Clear();
            oResponse.Charset = "";
            oResponse.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            oResponse.ContentType = "aplication/vnd.ms-excel";
            oResponse.WriteFile(XlsPath);
            oResponse.End();
        }
        catch (Exception ex)
        {
            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }

    public void ResponsePdf(GridView grdResult, String nomeArquivo)
    {

        try
        {
            var oResponse = HttpContext.Current.Response;
            oResponse.ContentType = "application/pdf";
            oResponse.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo + ".pdf");
            oResponse.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            var grdSource = new GridView
            {
                ShowHeader = true,
                DataSource = ConvertGridViewToDataTable(grdResult)
            };
            grdSource.AllowPaging = false;
            grdSource.DataBind();
            grdSource.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate());
            iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, oResponse.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            oResponse.Write(pdfDoc);
            oResponse.End();

        }
        catch (Exception ex)
        {

            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }

    public void ResponsePdf(String caminho_arquivo, String nomeArquivo, String modo_abertura)
    {
        ResponseFile(caminho_arquivo, nomeArquivo, "application/pdf", modo_abertura);
    }

    public void ResponseFile2(String caminho_arquivo, String nomeArquivo, String ContentType, String ContentDisposition)
    {

        ContentDisposition = ContentDisposition ?? DispositionTypeNames.Inline;

        try
        {
            var oResponse = HttpContext.Current.Response;            
            // INICIA DOWNLOAD
            //oResponse.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo);

            // ABRE NA TELA
            oResponse.ClearHeaders();
            //oResponse.AddHeader("content-disposition", ContentDisposition + ";filename=" + nomeArquivo);            
            oResponse.Cache.SetCacheability(HttpCacheability.NoCache);
            oResponse.Buffer = false;

            oResponse.ContentType = ContentType;

            var stream = new MemoryStream();
            byte[] bytes = Util.File2Memory(@caminho_arquivo);

            oResponse.BinaryWrite(bytes);
            oResponse.Flush();
            oResponse.End();

        }
        catch (Exception ex)
        {

            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }

    public void ResponseFile(String caminho_arquivo, String nomeArquivo, String ContentType, String ContentDisposition)
    {

        ContentDisposition = ContentDisposition ?? DispositionTypeNames.Inline;

        try
        {
            var oResponse = HttpContext.Current.Response;
            //oResponse.ContentType = ContentType;

            // ABRE NA TELA
            //oResponse.AddHeader("content-disposition", ContentDisposition);            
            oResponse.Cache.SetCacheability(HttpCacheability.NoCache);
            oResponse.Buffer = false;
            oResponse.ClearContent(); 

            var stream = new MemoryStream();
            byte[] bytes = Util.File2Memory(@caminho_arquivo);

            // Send it back to the client
            oResponse.ClearHeaders();
            oResponse.ContentType = ContentType;
            oResponse.AddHeader("content-disposition", ContentDisposition + ";filename=" + nomeArquivo);
            oResponse.AddHeader("Content-Length", bytes.Length.ToString());   
            oResponse.BinaryWrite(bytes);
            oResponse.Flush();
            oResponse.End();

        }
        catch (Exception ex)
        {
            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }



    public void ResponseFile(byte[] bytes, String nomeArquivo, String ContentType, String ContentDisposition)
    {

        ContentDisposition = ContentDisposition ?? DispositionTypeNames.Attachment;

        try
        {
            var oResponse = HttpContext.Current.Response;
            //oResponse.ContentType = ContentType;

            // ABRE NA TELA
            //oResponse.AddHeader("content-disposition", ContentDisposition);            
            oResponse.Cache.SetCacheability(HttpCacheability.NoCache);
            oResponse.Buffer = false;
            oResponse.ClearContent();

            // Send it back to the client
            oResponse.ClearHeaders();
            oResponse.ContentType = ContentType;
            oResponse.AddHeader("content-disposition", ContentDisposition + ";filename=" + nomeArquivo);
            oResponse.AddHeader("Content-Length", bytes.Length.ToString());
            oResponse.BinaryWrite(bytes);
            oResponse.Flush();
            oResponse.End();

        }
        catch (Exception ex)
        {
            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }

    public void ResponseWord(GridView grdResult, String nomeArquivo)
    {

        try
        {
            var oResponse = HttpContext.Current.Response;
            oResponse.Clear();
            oResponse.Buffer = true;
            oResponse.AddHeader("content-disposition", "attachment;filename=GridViewExport.doc");
            oResponse.Charset = "";
            oResponse.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            var grdSource = new GridView
            {
                ShowHeader = true,
                DataSource = ConvertGridViewToDataTable(grdResult)
            };
            grdSource.DataBind();
            grdSource.RenderControl(hw);
            oResponse.Output.Write(sw.ToString());
            oResponse.Flush();
            oResponse.End();

        }
        catch (Exception ex)
        {

            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }

    public void ResponseTxt(String nomeArquivo, DataTable dt, String caract_quebra_linha = null)
    {

        try
        {
            var oResponse = HttpContext.Current.Response;
            oResponse.Clear();
            oResponse.Buffer = true;
            oResponse.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo);
            oResponse.Charset = "";
            //oResponse.ContentType = "application/octet-stream";
            oResponse.ContentType = "text/plain";
            StringWriter sw = new StringWriter();
            sw.NewLine = caract_quebra_linha ?? "\r\n";
            foreach (DataRow row in dt.Rows)
            {
                sw.WriteLine(row[0]);
            }
            oResponse.Output.Write(sw);
            oResponse.Flush();
            oResponse.End();

        }
        catch (Exception ex)
        {

            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }

    public void ResponseTxt(String caminho_arquivo, String nomeArquivo)
    {
        ResponseFile(caminho_arquivo, nomeArquivo, "text/plain", DispositionTypeNames.Attachment);
    }

    public void ResponseXml(String nomeArquivo, XmlDocument xdDocument)
    {

        try
        {
            var oResponse = HttpContext.Current.Response;
            oResponse.Clear();
            oResponse.Buffer = true;
            oResponse.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo);
            oResponse.Charset = "";
            oResponse.ContentType = "text/xml";
            oResponse.Output.Write(xdDocument.OuterXml);
            oResponse.Flush();
            oResponse.End();

        }
        catch (Exception ex)
        {

            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }

    public void ResponseHtml(String nomeArquivo, String xdDocument)
    {

        try
        {
            var oResponse = HttpContext.Current.Response;
            oResponse.Clear();
            oResponse.Buffer = true;
            oResponse.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo);
            oResponse.Charset = "";
            oResponse.ContentType = "text/html";
            oResponse.Output.Write(xdDocument);
            oResponse.Flush();
            oResponse.End();
        }
        catch (Exception ex)
        {

            throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
        }
    }

    public DataTable ConvertGridViewToDataTable(GridView dtg)
    {
        DataTable dt = new DataTable();

        if (dtg.HeaderRow != null)
        {

            for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
            {
                if (!dtg.HeaderRow.Cells[i].Text.Equals("&nbsp;"))
                {
                    dt.Columns.Add(Server.HtmlDecode(dtg.HeaderRow.Cells[i].Text));
                }

            }
        }


        foreach (GridViewRow row in dtg.Rows)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (!dtg.HeaderRow.Cells[i].Text.Equals("&nbsp;"))
                {
                    dr[i] = Server.HtmlDecode(row.Cells[i].Text);
                }
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    #region Métodos que lê o Excel sem a necessidade de estar instalado no servidor

    public DataTable ReadExcelFile(string FilePath)
    {
        return (ReadExcelFile(FilePath, 1));
    }

    public DataTable ReadExcelFile(string FilePath, string PlanName)
    {
        return (ReadExcelFile(FilePath, 0, PlanName));
    }

    public DataTable ReadExcelFile(string FilePath, string PlanName, int start_row)
    {
        return (ReadExcelFile(FilePath, 0, PlanName, start_row));
    }

    public DataTable ReadExcelFile(string FilePath, int PlanIndex)
    {
        return (ReadExcelFile(FilePath, PlanIndex, 1));
    }

    public DataTable ReadExcelFile(string FilePath, int PlanIndex, int start_row)
    {
        return (ReadExcelFile(FilePath, PlanIndex, null, start_row));
    }

    public DataTable ReadExcelFile(string FilePath, int PlanIndex, string PlanName = null, int start_row = 1)
    {
        ExcelWorksheets ls_Worksheets = ReadExcelWorksheets(FilePath);
        if (ls_Worksheets.Count > 0)
        {
            ExcelWorksheet wsReadExcel = null;
            wsReadExcel = (PlanName != null) ? ls_Worksheets[PlanName] : ls_Worksheets[PlanIndex];
            if (wsReadExcel != null)
            {
                return ReadWorksSheet(wsReadExcel, start_row);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    //public DataTable ReadExcelFile(string FilePath, int start_row)
    //{
    //    ExcelWorksheets wsReadExcel = ReadExcelWorksheets(FilePath, start_row);
    //    if (wsReadExcel.Count > 0)
    //    {
    //        return ReadWorksSheet(wsReadExcel[0], start_row);
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    public DataSet ReadExcelFileWork(string FilePath)
    {
        ExcelWorksheets wsReadExcel = ReadExcelWorksheets(FilePath);
        DataSet ds = new DataSet(); 
        if (wsReadExcel.Count > 0)
        {
            foreach (ExcelWorksheet item in wsReadExcel)
            {
                ds.Tables.Add(ReadWorksSheet(item));  
            }
            return ds;
        }
        else
        {
            return null;
        }
    }

    public ExcelWorksheets ReadExcelWorksheets(string FilePath)
    {
        DataTable dt = new DataTable();
        FileInfo fi = new FileInfo(FilePath);

        // Check if the file exists
        if (!fi.Exists)
            throw new Exception("File " + FilePath + " Does Not Exists");

        ExcelPackage xlPackage = new ExcelPackage(fi); 

        return xlPackage.Workbook.Worksheets;

    }

    public DataTable ReadWorksSheet(ExcelWorksheet worksheet, int start_row = 1)
    {
        DataTable dt = new DataTable();

        dt = new DataTable();

        if (worksheet.Dimension != null)
        {
            // Fetch the WorkSheet size
            ExcelCellAddress startCell = worksheet.Dimension.Start;
            ExcelCellAddress endCell = worksheet.Dimension.End;

            // place all the data into DataTable
            for (int row = startCell.Row; row <= endCell.Row; row++)
            {
                DataRow dr = dt.NewRow();
                int x = 0;
                for (int col = startCell.Column; col <= endCell.Column; col++)
                {
                    var oValue = worksheet.Cells[row, col].Value;
                    var oText = worksheet.Cells[row, col].Text;
                    //if (oValue != null)
                    //{
                        if (row == start_row)
                        {
                            string sColumnnName = (oValue == null) ? "" : oValue.ToString().Trim();
                            string sColumnnNameOrig = sColumnnName;
                            int cont = 1;
                            while (dt.Columns.IndexOf(sColumnnName) > -1)
                            {
                                sColumnnName = sColumnnNameOrig + cont;
                                cont++;
                            }
                            dt.Columns.Add(sColumnnName);
                        }
                        else if (row > start_row)
                        {
                            //Double NewDouble = 0;
                            //Decimal NewDecimal = 0;
                            //if (double.TryParse(oValue.ToString(), out NewDouble))
                            //{
                            //    dr[x] = (Decimal)NewDouble;
                            //}
                            //else
                            //{
                            //    dr[x] = oValue;
                            //}

                            if (oValue != null)
                            {
                                if (oValue.GetType() == typeof(Double))
                                {
                                    if (oValue.ToString().IndexOf("E") > -1)
                                    {
                                        dr[x] = oText.ToString();                                        
                                    }
                                    else
                                    {
                                        dr[x] = Double.Parse(oValue.ToString());
                                    }
                                } 
                                else if (oValue.GetType() == typeof(Decimal))
                                {
                                    dr[x] = Decimal.Parse(oValue.ToString());
                                }
                                else if (oValue.GetType() == typeof(long))
                                {
                                    dr[x] = long.Parse(oValue.ToString());
                                }
                                else
                                {
                                    dr[x] = oValue;
                                }
                            }
                            x++;
                        }
                }
                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count > 0)
                dt.Rows.RemoveAt(0);
        }

        RemoveLineEmpty(dt);

        return dt;
    }

    public DataTable ReadXmlFile(string FilePath, DataTable dtSchema, string node_parent_key = "", string node_parent_key_2 = "")
    {
        XmlDocument xdSource = new XmlDocument();
        xdSource.Load(FilePath);
        using (XmlNodeList xnlRows = xdSource.SelectNodes("//" + dtSchema.TableName))
        {
            if (xnlRows != null)
            {
                string sXml = "<ROOT>";
                foreach (XmlNode xnRow in xnlRows)
                {
                    if (!String.IsNullOrEmpty(node_parent_key))
                    {
                        XmlNode xnFindKey = null;
                        XmlNode xnFindNext = xnRow;
                        while (xnFindKey == null)
                        {
                            xnFindNext = xnFindNext.ParentNode;
                            xnFindKey = xnFindNext.SelectSingleNode(".//" + node_parent_key);
                        }
                        xnRow.AppendChild(xnFindKey.Clone());
                    }

                    //sXml += xnRow.OuterXml;

                    if (!String.IsNullOrEmpty(node_parent_key_2))
                    {
                        XmlNode xnFindKey2 = null;
                        XmlNode xnFindNext2 = xnRow;
                        while (xnFindKey2 == null)
                        {
                            xnFindNext2 = xnFindNext2.ParentNode;
                            xnFindKey2 = xnFindNext2.SelectSingleNode(".//" + node_parent_key_2);
                        }
                        xnRow.AppendChild(xnFindKey2.Clone());
                    }

                    sXml += xnRow.OuterXml;

                }
                sXml += "</ROOT>";
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Tables.Add(dtSchema);
                    System.IO.StringReader xmlSR = new System.IO.StringReader(sXml);
                    dataSet.ReadXml(xmlSR, XmlReadMode.Auto);
                }
            }
        }

        return dtSchema.Copy();
    }

    public DataTable ReadTextFile(string FilePath, Encoding encoding = null)
    {
        DataTable dt =new DataTable();
        dt.Columns.Add("Data", typeof(String));
        //using (StreamReader sr = File.OpenText(FilePath))
        using (StreamReader sr = new StreamReader(FilePath, encoding ?? Encoding.UTF8))
        {
            string s = String.Empty;
            while ((s = sr.ReadLine()) != null)
            {
                DataRow drNew = dt.Rows.Add();
                drNew["Data"] = s.ToString();
               // dt.Rows.Add(drNew);
            }

            //Tratativa para eliminar linhas em branco no final do arquivo:
            for (int ln = dt.Rows.Count-1; ln > 0; ln--)
            {
                if (String.IsNullOrEmpty(dt.Rows[ln][0].ToString()))
                {
                    dt.Rows.RemoveAt(ln);
                }
                else
                {
                    break;
                }
            }
        }
        return dt;
    }



    #region Ocorreu um erro nesse plugin
    /// <summary>
    ///  Read Data from selected excel file into DataTable
    /// </summary>
    /// <param name="filename">Excel File Path</param>
    /// <returns></returns>
    public DataTable ImportToDataTable(string filename)
    {
        // Initialize an instance of DataTable
        DataTable dt = new DataTable();

        try
        {
            // Use SpreadSheetDocument class of Open XML SDK to open excel file
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filename, false))
            {
                // Get Workbook Part of Spread Sheet Document
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

                // Get all sheets in spread sheet document
                IEnumerable<Sheet> sheetcollection = spreadsheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();

                // Get relationship Id
                string relationshipId = sheetcollection.First().Id.Value;

                // Get sheet1 Part of Spread Sheet Document
                WorksheetPart worksheetPart = (WorksheetPart)spreadsheetDocument.WorkbookPart.GetPartById(relationshipId);

                // Get Data in Excel file
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                IEnumerable<Row> rowcollection = sheetData.Descendants<Row>();

                if (rowcollection.Count() == 0)
                {
                    return dt;
                }

                // Add columns
                foreach (Cell cell in rowcollection.ElementAt(0))
                {
                    dt.Columns.Add(GetValueOfCell(spreadsheetDocument, cell));
                }

                // Add rows into DataTable
                foreach (Row row in rowcollection)
                {
                    DataRow temprow = dt.NewRow();
                    int columnIndex = 0;
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        // Get Cell Column Index
                        int cellColumnIndex = GetColumnIndex(GetColumnName(cell.CellReference));


                        //if (columnIndex < cellColumnIndex)
                        //{
                        //    do
                        //    {
                        //        temprow[columnIndex] = string.Empty;
                        //        columnIndex++;
                        //    }

                        //    while (columnIndex < cellColumnIndex);
                        //}
                        if (columnIndex < row.Descendants<Cell>().Count())
                        {
                            temprow[columnIndex] = GetValueOfCell(spreadsheetDocument, cell);
                            columnIndex++;
                        }

                    }

                    // Add the row to DataTable
                    // the rows include header row
                    dt.Rows.Add(temprow);
                }
            }

            // Here remove header row
            if (dt.Rows.Count > 0)
                dt.Rows.RemoveAt(0);
            return dt;
        }
        catch (IOException ex)
        {
            throw new IOException(ex.Message);
        }
    }

    /// <summary>
    ///  Get Value of Cell
    /// </summary>
    /// <param name="spreadsheetdocument">SpreadSheet Document Object</param>
    /// <param name="cell">Cell Object</param>
    /// <returns>The Value in Cell</returns>
    private static string GetValueOfCell(SpreadsheetDocument spreadsheetdocument, Cell cell)
    {
        // Get value in Cell
        SharedStringTablePart sharedString = spreadsheetdocument.WorkbookPart.SharedStringTablePart;
        if (cell.CellValue == null)
        {
            return string.Empty;
        }

        string cellValue = cell.CellValue.InnerText;

        // The condition that the Cell DataType is SharedString
        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        {
            return sharedString.SharedStringTable.ChildElements[int.Parse(cellValue)].InnerText;
        }
        else
        {
            return cellValue;
        }
    }

    /// <summary>
    /// Get Column Name From given cell name
    /// </summary>
    /// <param name="cellReference">Cell Name(For example,A1)</param>
    /// <returns>Column Name(For example, A)</returns>
    private string GetColumnName(string cellReference)
    {
        // Create a regular expression to match the column name of cell
        Regex regex = new Regex("[A-Za-z]+");
        Match match = regex.Match(cellReference);
        return match.Value;
    }

    /// <summary>
    /// Get Index of Column from given column name
    /// </summary>
    /// <param name="columnName">Column Name(For Example,A or AA)</param>
    /// <returns>Column Index</returns>
    private int GetColumnIndex(string columnName)
    {
        int columnIndex = 0;
        int factor = 1;

        // From right to left
        for (int position = columnName.Length - 1; position >= 0; position--)
        {
            // For letters
            if (Char.IsLetter(columnName[position]))
            {
                columnIndex += factor * ((columnName[position] - 'A') + 1) - 1;
                factor *= 26;
            }
        }

        return columnIndex;
    }

    /// <summary>
    /// Convert DataTable to Xml format
    /// </summary>
    /// <param name="filename">Excel File Path</param>
    /// <returns>Xml format string</returns>
    public string GetXML(string filename)
    {
        using (DataSet ds = new DataSet())
        {
            ds.Tables.Add(this.ImportToDataTable(filename));
            return ds.GetXml();
        }
    }
    #endregion

    #endregion

    #region Método que lê o Excel com a necessidade de estar instalado no servidor

    /// <summary>
    /// Método retorna todos os sheets de uma planilha em um dataset
    /// </summary>
    /// <param name="filelocation"></param>
    /// <returns></returns>
    public DataSet ImportExcelToDataTable(string filelocation)
    {
        DataSet ds = new DataSet();
        try
        {
            // string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=Excel 12.0;HDR=YES";

            oledbConn = new OleDbConnection(@"Data Source=" + filelocation + ";Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;");
            //oledbConn = new OleDbConnection(excelConnectionString);
            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand(); ;
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            DataTable dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            DataTable dtaux;
            string sheetName = string.Empty;
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {

                    dtaux = new DataTable();
                    cmd.Connection = oledbConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [" + row["TABLE_NAME"].ToString() + "]";
                    oleda = new OleDbDataAdapter(cmd);
                    string name = row["TABLE_NAME"].ToString();
                    oleda.Fill(dtaux);
                    dtaux.TableName = name.Replace("$", "");
                    ds.Tables.Add(dtaux);
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao carregar Excel em memória:" + ex.Message);
        }
        finally
        {
            oledbConn.Close();
        }
        return ds;
    }
    #endregion

    public static DataTable ReadAsDataTable(string fileName)
    {
        DataTable dataTable = new DataTable();
        using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
        {
            WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
            IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
            Worksheet workSheet = worksheetPart.Worksheet;
            SheetData sheetData = workSheet.GetFirstChild<SheetData>();
            IEnumerable<Row> rows = sheetData.Descendants<Row>();

            foreach (Cell cell in rows.ElementAt(0))
            {
                dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
            }

            foreach (Row row in rows)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                {
                    dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                }

                dataTable.Rows.Add(dataRow);
            }

        }
        dataTable.Rows.RemoveAt(0);

        return dataTable;
    }

    private static string GetCellValue(SpreadsheetDocument document, Cell cell)
    {
        SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
        string value = cell.CellValue.InnerXml;

        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        {
            return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
        }
        else
        {
            return value;
        }
    }

    public List<string> retornaValorListBox(ListBox lst)
    {
        List<string> codigos = new List<string>();
        string codigo = null;

        for (int x = lst.Items.Count - 1; x >= 0; x--)
        {
            if (lst.Items[x].Selected)
            {
                codigo = lst.Items[x].Value;
                codigos.Add(codigo);
            }
        }
        return codigos;
    }

    public static void RemoveLineEmpty(DataTable dt)
    {
        int count = dt.Columns.Count;

        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            bool allNull = true;
            for (int j = 0; j < count; j++)
            {
                if (dt.Rows[i][j] != DBNull.Value)
                {
                    allNull = false;
                }
            }
            if (allNull)
            {
                dt.Rows[i].Delete();
            }
        }
        dt.AcceptChanges();
    }

    public static void AdicionarAcesso(string fullurl)
    {
        List<string> lista_acessos = (List<string>)HttpContext.Current.Session["Acessos"];

        if (lista_acessos == null)
            lista_acessos = new List<string>();

        if (lista_acessos.IndexOf(fullurl) == -1)
            lista_acessos.Add(fullurl);
        HttpContext.Current.Session["Acessos"] = lista_acessos;
    }

    #region GridView_events

        protected void GridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            GridView gvGridView = (GridView)sender;
            gvGridView.EditIndex = -1;
            gvGridView.PageIndex = 0;
        }

        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView gvGridView = (GridView)sender;
            gvGridView.EditIndex = -1;
            gvGridView.PageIndex = 0;
        }

        protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridView gvGridView = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int sortColumnIndex = GetSortColumnIndex(gvGridView);
                if (sortColumnIndex != -1)
                {
                    AddSortImage(e.Row, gvGridView);
                }
            }
        }

        public int GetSortColumnIndex(GridView gvGridView)
        {
            foreach (DataControlField field in gvGridView.Columns)
            {
            if (gvGridView.SortExpression == field.SortExpression && 
                !String.IsNullOrEmpty(field.HeaderText) &&
                !String.IsNullOrEmpty(field.SortExpression))
                {
                    return gvGridView.Columns.IndexOf(field);
                }
            }
            return -1;
        }

        public void AddSortImage(GridViewRow headerRow, GridView gvGridView)
        {
            int iCol = GetSortColumnIndex(gvGridView);
            if (-1 == iCol)
                return;

            Image sortImage = new Image();
            if (gvGridView.SortDirection == SortDirection.Ascending)
            {
                sortImage.ImageUrl = @"~\img\arrow-up.gif";
                sortImage.AlternateText = "Ascending Order";
            }
            else
            {
                sortImage.ImageUrl = @"~\img\arrow-down.gif";
                sortImage.AlternateText = "Descending Order";
            }
            headerRow.Cells[iCol].Controls.Add(new LiteralControl("&nbsp;"));
            headerRow.Cells[iCol].Controls.Add(sortImage);
        }

        public int GetColumnIndex(GridView gvGridView, string ColumnName)
        {
            foreach (DataControlField Column in gvGridView.Columns)
            {
                if (Column.HeaderText == ColumnName)
                {
                    return gvGridView.Columns.IndexOf(Column);
                }
            }
            return -1;
        }

    #endregion

        public void init_SSO_Session()
        {

            //Singlesignon sso_session = null;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["SingleSignOn"] != null)
            {
                sso_session = (Singlesignon)HttpContext.Current.Session["SingleSignOn"];

                if (sso_session.ListaGrupos == null)
                {
                    Response.Redirect("~/Accessdenied.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Accessdenied.aspx");
            }

        }

        //public void CarregaGrid(string nameView, DataGrid dt, GridView grid)
        //{
        //    ViewState[nameView] = dt;
        //    grid.DataSource = ViewState[nameView]; 
        //    grid.DataBind();
        //}
        
}