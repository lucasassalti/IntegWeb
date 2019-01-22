using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;

namespace IntegWeb.Web.Includes
{
    public partial class ucReport : System.Web.UI.UserControl
    {
        private SqlConnectionStringBuilder builder;
        //private Relatorio osb = new Relatorio();
        private ReportDocument relatorio;
        private Relatorio _relatorio = new Relatorio();
        public string RelatorioID
        {
            get
            {
                return _relatorio.relatorio;
            }
            set
            {
                _relatorio = (Relatorio)Session[value];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session[Request.QueryString["Param"]+"rel"] != null)
            //{
            //    ReportDocument doc = (ReportDocument)Session[Request.QueryString["Param"] + "rel"];
            //    CrystalReportViewer1.ReportSource = doc;
            //}
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string CtrlID = string.Empty;
                if (Request.Form["__EVENTTARGET"] != null &&
                    Request.Form["__EVENTTARGET"] != string.Empty)
                {
                    CtrlID = Request.Form["__EVENTTARGET"];
                }

                // Carrega o relatório quando algum comando do CrystalViewr é solicitado (Ex. Exportar para PDF):
                if (CtrlID.IndexOf("CrystalReportViewer") > -1 && Session[RelatorioID + "cache"] != null)
                {
                    _relatorio = (Relatorio)Session[RelatorioID + "cache"];
                    VisualizaRelatorio();
                }
            }
        }

        public void InicializaRpt()
        {
            Session[RelatorioID + "cache"] = _relatorio;

            relatorio = new ReportDocument();

            String Relatorio_caminho = _relatorio.arquivo;
            Relatorio_caminho = Server.MapPath(@Relatorio_caminho);

            TableLogOnInfo tableLogOnInfo = null;

            relatorio.FileName = Relatorio_caminho;

            if (builder == null)
            {
                builder = new SqlConnectionStringBuilder(ConfigAplication.GetConnectString().ToString().Replace(";Unicode=True", ""));
            }

            foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in relatorio.Database.Tables)
            {
                tableLogOnInfo = tbl.LogOnInfo;
                tableLogOnInfo.ConnectionInfo.ServerName = builder.DataSource;
                tableLogOnInfo.ConnectionInfo.DatabaseName = "";
                tableLogOnInfo.ConnectionInfo.UserID = builder.UserID;
                tableLogOnInfo.ConnectionInfo.Password = builder.Password;
                tbl.ApplyLogOnInfo(tableLogOnInfo);
            }

            //foreach (ReportDocument rdSub in relatorio.Subreports)
            //{
            //    foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in rdSub.Database.Tables)
            //    {
            //        tableLogOnInfo = tbl.LogOnInfo;
            //        tableLogOnInfo.ConnectionInfo.ServerName = builder.DataSource;
            //        tableLogOnInfo.ConnectionInfo.DatabaseName = "";
            //        tableLogOnInfo.ConnectionInfo.UserID = builder.UserID;
            //        tableLogOnInfo.ConnectionInfo.Password = builder.Password;
            //        tbl.ApplyLogOnInfo(tableLogOnInfo);
            //    }
            //    rdSub.VerifyDatabase();
            //}

            relatorio.SetDatabaseLogon(builder.UserID, builder.Password, builder.DataSource, "");

        }

        public void VisualizaRelatorio()
        {

            CrystalReportViewer1.ReportSource = null;
            InicializaRpt();
            CrystalReportViewer1.RefreshReport();
            ParameterFields paramFields = new ParameterFields();
            foreach (var p in _relatorio.parametros)
            {
                ParameterField pfItemYr = new ParameterField();
                pfItemYr.ParameterFieldName = p.parametro;
                ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();
                dcItemYr.Value = p.valor;
                pfItemYr.CurrentValues.Add(dcItemYr);
                paramFields.Add(pfItemYr);
                CrystalReportViewer1.ParameterFieldInfo = paramFields;
            }

            relatorio.Refresh();
            //CrystalReportViewer1.ReuseParameterValuesOnRefresh = true; 
            CrystalReportViewer1.ReportSource = relatorio;
        }

        public void ExportarRelatorioPdf(string caminho_arquivo)
        {

            InicializaRpt();

            foreach (var p in _relatorio.parametros)
            {
                relatorio.SetParameterValue(p.parametro, p.valor);
            }

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = caminho_arquivo;
            CrExportOptions = relatorio.ExportOptions;//Report document  object has to be given here
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;

            relatorio.Export();

            //((BasePage)this.Page).ResponsePdf(adExtratoPDF.caminho_arquivo, adExtratoPDF.nome_arquivo);

            //ResponsePdf(adExtratoPDF.caminho_arquivo, adExtratoPDF.nome_arquivo);

        }

        public System.IO.Stream ExportarRelatorioPdf()
        {

            InicializaRpt();

            foreach (var p in _relatorio.parametros)
            {
                relatorio.SetParameterValue(p.parametro, p.valor);
            }

            return relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        }

        public void ExportarRelatorioHtml(string nome_arquivo)
        {

            InicializaRpt();

            ArquivoDownload adExtratoPDF = new ArquivoDownload();
            adExtratoPDF.nome_arquivo = nome_arquivo;
            adExtratoPDF.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + adExtratoPDF.nome_arquivo;

            foreach (var p in _relatorio.parametros)
            {
                relatorio.SetParameterValue(p.parametro, p.valor);
            }

            //relatorio.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, false, ""); 
            relatorio.ExportToHttpResponse(ExportFormatType.HTML40, HttpContext.Current.Response, false, "");

        }

        protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
        {
            if (relatorio != null)
            {
                relatorio.Close();
                relatorio.Dispose();
                relatorio = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }

}