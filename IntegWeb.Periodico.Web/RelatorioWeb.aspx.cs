using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using IntegWeb.Framework;
using System.Data.SqlClient;

using System.Net.Mail;
using System.Net;
using System.Text;
using IntegWeb.Entidades.Relatorio;
using System.Web.UI.HtmlControls;
using IntegWeb.Framework.Aplicacao;


namespace IntegWeb.Periodico.Web
{
    public partial class RelatorioWeb : System.Web.UI.Page
    {
        #region Atributos
        BasePage objPage = new BasePage();
        RelatorioBLL rel = new RelatorioBLL();
        #endregion

        #region Enventos
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["relatorio"] != null)
            {
                ReportDocument doc = (ReportDocument)Session["relatorio"];
                CrystalReportViewer1.ReportSource = doc;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string relatorio = Request.QueryString["Relatorio_nome"];
                if (!String.IsNullOrEmpty(relatorio))
                {
                    Relatorio res = rel.Listar(relatorio);
                    Session["ObjRelatorio"] = res;
                    GeraHtml(res);

                    ReportDocument doc1 = (ReportDocument)Session["relatorio"];
                    if (doc1 != null)
                    {
                        if (System.IO.Path.GetFileName(res.arquivo).ToUpper() != System.IO.Path.GetFileName(doc1.FileName).ToUpper()
                            && System.IO.Path.GetFileName(doc1.FileName) != "Relatorio_Tela.rpt"
                            )
                        {

                            string caminho = Server.MapPath(@"Relatorios\Relatorio_Tela.rpt");

                            ReportDocument relatorio2 = new ReportDocument();
                            relatorio2.Load(caminho);
                            CrystalReportViewer1.ReportSource = relatorio2;
                            CrystalReportViewer1.RefreshReport();
                            CrystalReportSource1.Report.FileName = caminho;
                        }
                    }
                }
                else
                   Response.Redirect("index.aspx");

            }

        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            ValidaCampos();
        }
        #endregion

        #region Métodos
        public void ValidaCampos()
        {
            if (Session["ObjRelatorio"] != null)
            {

                Relatorio obj = (Relatorio)Session["ObjRelatorio"];
                List<Parametro> listP = obj.parametros;

                int contador = 0;
                List<Object> list = new List<Object>();
                foreach (String cc in Request.Form.Keys)
                {
                    if (cc.IndexOf("Param_") > -1)
                    {
                        list.Add(Request.Form[cc]);
                        listP[contador].valor = Request.Form[cc];
                        contador++;
                    }
                }

                foreach (var par in listP)
                {
                    if (par.permite_null == "N" && par.valor == "")
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + "O campo " + par.descricao + " não pode estar em branco" + "');", true);
                        return;
                    }
                }

                try
                {
                    GeraRelatorio(obj);

                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }

            }
        }

        protected void GeraHtml(Relatorio relatorio)
        {
            List<Parametro> parametros = relatorio.parametros;
            NomeRelatorio.Text = relatorio.titulo;

            if (parametros.Count > 0)
            {
                string tamanho = "200px";
                System.Text.StringBuilder scriptAjax = new System.Text.StringBuilder();

                foreach (var par in parametros)
                {
                    HtmlTableRow row = new HtmlTableRow();

                    Label label1 = new Label();
                    label1.ID = "lbl" + par.parametro;
                    label1.Text = par.descricao;
                    label1.Visible = par.visivel == "S";

                    HtmlTableCell cell = new HtmlTableCell();
                    cell.Controls.Add(label1); ;
                    row.Cells.Add(cell);

                    if (par.componente_web == "DropDownList")
                    {
                        DropDownList DropDownList1 = new DropDownList();
                        DropDownList1.ID = "Param_" + par.parametro;
                        DropDownList1.Visible = par.visivel == "S";
                        DropDownList1.Enabled = par.habilitado == "S";
                        DropDownList1.Style.Add("width", tamanho);

                        if (par.dropdowlist_consulta.Length > 0)
                            objPage.CarregaDropDowDT(rel.ListarDrop(par.dropdowlist_consulta), DropDownList1);

                        HtmlTableCell cell1 = new HtmlTableCell();
                        cell1.Controls.Add(DropDownList1); ;
                        row.Cells.Add(cell1);

                    }

                    if (par.componente_web == "TextBox")
                    {
                        TextBox TextBox1 = new TextBox();
                        TextBox1.ID = "Param_" + par.descricao;
                        TextBox1.Visible = par.visivel == "S";
                        TextBox1.Style.Add("width", tamanho);
                        TextBox1.Enabled = true;

                        if (par.tipo == "DateField")
                        {
                            TextBox1.CssClass = "date";
                            TextBox1.Attributes.Add("onkeypress", "javascript:return mascara(this, data);");
                            TextBox1.Attributes.Add("MaxLength", "10");
                        }


                        if (par.valor_inicial.Length > 0)
                        {
                            TextBox1.Text = par.valor_inicial;
                        }
                        HtmlTableCell cell2 = new HtmlTableCell();
                        cell2.Controls.Add(TextBox1); ;
                        row.Cells.Add(cell2);
                    }



                    table.Rows.Add(row);

                }

            }
        }

        private void BindCrystal(Relatorio relatorio)
        {

            List<Parametro> list = relatorio.parametros;
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath(relatorio.arquivo));
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigAplication.GetConnectString().Replace(";Unicode=True", ""));
            string usuario = builder.UserID;
            string senha = builder.Password;
            string banco = builder.DataSource;

            crystalReport.SetDatabaseLogon(usuario, senha, "", banco);




            if (relatorio.parametros.Count > 0)
            {
                foreach (var p in list)
                {

                    crystalReport.SetParameterValue("num_matricula", "806684");
                }


            }
            CrystalReportViewer1.ReportSource = crystalReport;

            Session["ObjRelatorio"] = crystalReport;

        }

        protected void GeraRelatorio(Relatorio Objr)
        {
            // Abrir relatorio
            ReportDocument relatorio = new ReportDocument();
            List<Parametro> parametros = Objr.parametros;
            String Relatorio_caminho = Objr.arquivo;
            Relatorio_caminho = Server.MapPath(@Relatorio_caminho);
            relatorio.Load(Relatorio_caminho);


            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigAplication.GetConnectString().ToString().Replace(";Unicode=True", ""));
            string usuario = builder.UserID;
            string senha = builder.Password;
            string banco = builder.DataSource;


            TableLogOnInfo tableLogOnInfo = null;

            foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in relatorio.Database.Tables)
            {

                tableLogOnInfo = tbl.LogOnInfo;
                tableLogOnInfo.ConnectionInfo.ServerName = banco;
                tableLogOnInfo.ConnectionInfo.DatabaseName = "";
                tableLogOnInfo.ConnectionInfo.UserID = usuario;
                tableLogOnInfo.ConnectionInfo.Password = senha;
                tbl.ApplyLogOnInfo(tableLogOnInfo);

            }

            relatorio.SetDatabaseLogon(usuario, senha, banco, "");
            CrystalReportViewer1.ReportSource = relatorio;

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
            CrystalReportSource1.Report.FileName = Objr.relatorio;
            Session["relatorio"] = relatorio;
        }
        #endregion

    }
}
