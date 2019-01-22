using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Investimento.Web.Includes
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

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (Session[Request.QueryString["Param"]+"rel"] != null)
            //{
            //    ReportDocument doc = (ReportDocument)Session[Request.QueryString["Param"] + "rel"];
            //    CrystalReportViewer1.ReportSource = doc;
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (Session[RelatorioID] != null)
                //{
                //    _relatorio = (Relatorio)Session["relOsb"];
                //}

                //ReportDocument doc1 = (ReportDocument)Session[Request.QueryString["Param"] + "rel"];
                //if (doc1 != null && objrel != null)
                //{
                //    Page.Title = objrel.titulo;

                //    if (System.IO.Path.GetFileName(objrel.arquivo).ToUpper() != System.IO.Path.GetFileName(doc1.FileName).ToUpper()
                //        && System.IO.Path.GetFileName(doc1.FileName) != "Relatorio_Tela.rpt"
                //        )
                //    {

                //        string caminho = Server.MapPath(@"Relatorios\Relatorio_Tela.rpt");

                //        ReportDocument relatorio2 = new ReportDocument();
                //        //relatorio2.Load(caminho);
                //        relatorio2.FileName = caminho;
                //        CrystalReportViewer1.ReportSource = relatorio2;
                //        CrystalReportViewer1.RefreshReport();
                //        //CrystalReportSource1.Report.FileName = caminho;
                //    }
                //}


            }
            else
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
                    GeraRelatorio();
                }
            }

        }

        public void GeraRelatorio()
        {

            //Session[RelatorioID] = _relatorio;
            Session[RelatorioID + "cache"] = _relatorio;

            // Abrir relatorio
            relatorio = new ReportDocument();
            List<Parametro> parametros = _relatorio.parametros;
            String Relatorio_caminho = _relatorio.arquivo;
            Relatorio_caminho = Server.MapPath(@Relatorio_caminho);
            //relatorio.Load(Relatorio_caminho);
            NomeRelatorio.Text = "Relatorio " + _relatorio.titulo;

            CrystalReportViewer1.ReportSource = null;
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

            relatorio.SetDatabaseLogon(builder.UserID, builder.Password, builder.DataSource, "");
            //CrystalReportViewer1.ReportSource = relatorio;

            // Passar os parametros para o relatorio
            CrystalReportViewer1.RefreshReport();
            ParameterFields paramFields = new ParameterFields();
            foreach (var p in parametros)
            {

                ParameterField pfItemYr = new ParameterField();
                pfItemYr.ParameterFieldName = p.parametro;
                ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();
                dcItemYr.Value = p.valor;
                pfItemYr.CurrentValues.Add(dcItemYr);
                paramFields.Add(pfItemYr);
                CrystalReportViewer1.ParameterFieldInfo = paramFields;

            }
            //CrystalReportSource1.Report.FileName = objrel.relatorio;
            CrystalReportViewer1.ReportSource = relatorio;
            //Session[Request.QueryString["Param"]+"rel"] = relatorio;
        }

        protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
        {
            //ReportDocument rptReportSource = (ReportDocument)CrystalReportViewer1.ReportSource;
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