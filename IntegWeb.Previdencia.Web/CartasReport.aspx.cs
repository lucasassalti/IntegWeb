using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Previdencia.Aplicacao.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class CartasReport : BasePage
    {
        BasePage objPage = new BasePage();
        RelatorioBLL rel = new RelatorioBLL();
        PropostaAdesaoBLL obj = new PropostaAdesaoBLL();
        SqlConnectionStringBuilder builder;
        List<Relatorio> lstRelOsb = new List<Relatorio>();
        ReportDocument relatorio;

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (Session["relatorio"] != null)
            //{
            //    ReportDocument doc = (ReportDocument)Session["relatorio"];
            //    CrystalReportViewer1.ReportSource = doc;
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Form.DefaultButton = (this.btnRelatorio1.Visible) ? this.btnRelatorio1.UniqueID : this.btnCarregarRelatorios.UniqueID;

            if (String.IsNullOrEmpty(txtDataEmissao.Text))
            {
                txtDataEmissao.Text = DateTime.Now.ToShortDateString();
            }

            if (builder == null)
            {
                builder = new SqlConnectionStringBuilder(ConfigAplication.GetConnectString().ToString().Replace(";Unicode=True", ""));
            }

            if (!IsPostBack)
            {

                string registro = Request.QueryString["registro"];
                string cod_emprs = Request.QueryString["cod_emprs"];
                string tip_relatorio = Request.QueryString["tip_relatorio"];
                if (!String.IsNullOrEmpty(registro))
                {
                    if (!String.IsNullOrEmpty(cod_emprs))
                    {
                        txtMatricula.Text = registro;
                        txtEMpresa.Text = cod_emprs;
                        CarregaRelatorio(!String.IsNullOrEmpty(tip_relatorio) ? int.Parse(tip_relatorio) : 1);
                    }
                }

                string relatorio = Request.QueryString["Relatorio_nome"];
                if (!String.IsNullOrEmpty(relatorio))
                {
                    Relatorio res = rel.Listar(relatorio);
                    Session["ObjRelatorio"] = res;

                    ReportDocument doc1 = (ReportDocument)Session["relatorio"];
                    if (doc1 != null)
                    {
                        if (System.IO.Path.GetFileName(res.arquivo).ToUpper() != System.IO.Path.GetFileName(doc1.FileName).ToUpper()
                            && System.IO.Path.GetFileName(doc1.FileName) != "Relatorio_Tela.rpt"
                            )
                        {

                            string caminho = Server.MapPath(@"Relatorios\Relatorio_Tela.rpt");

                            ReportDocument relatorio2 = new ReportDocument();
                            //relatorio2.Load(caminho);
                            relatorio2.FileName = caminho;
                            CrystalReportViewer1.ReportSource = relatorio2;
                            CrystalReportViewer1.RefreshReport();
                            //CrystalReportSource1.Report.FileName = caminho;
                            //relatorio2.Dispose();
                        }
                    }
                }

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
                if (CtrlID.IndexOf("CrystalReportViewer") > -1 && Session["relOsb"] != null)
                {
                    Relatorio Osb = new Relatorio();
                    Osb = (Relatorio)Session["relOsb"];
                    GeraRelatorio(Osb);
                }
            }

        }

        //protected void GeraRelatorio(Relatorio Objr)
        //{
        //    // Abrir relatorio
        //    ReportDocument relatorio = new ReportDocument();
        //    String Relatorio_caminho = Objr.arquivo;
        //    Relatorio_caminho = Server.MapPath(@Relatorio_caminho);
        //    relatorio.Load(Relatorio_caminho);


        //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigAplication.GetConnectString().ToString().Replace(";Unicode=True", ""));
        //    string usuario = builder.UserID;
        //    string senha = builder.Password;
        //    string banco = builder.DataSource;


        //    TableLogOnInfo tableLogOnInfo = null;

        //    foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in relatorio.Database.Tables)
        //    {

        //        tableLogOnInfo = tbl.LogOnInfo;
        //        tableLogOnInfo.ConnectionInfo.ServerName = banco;
        //        tableLogOnInfo.ConnectionInfo.DatabaseName = "";
        //        tableLogOnInfo.ConnectionInfo.UserID = usuario;
        //        tableLogOnInfo.ConnectionInfo.Password = senha;
        //        tbl.ApplyLogOnInfo(tableLogOnInfo);

        //    }

        //    relatorio.SetDatabaseLogon(usuario, senha, banco, "");
        //    CrystalReportViewer1.ReportSource = relatorio;

        //    // Passar os parametros para o relatorio
        //    CrystalReportViewer1.RefreshReport();
        //    ParameterFields paramFields = new ParameterFields();


        //    ParameterField pfItemYr = new ParameterField();
        //    pfItemYr.ParameterFieldName = "matricula";
        //    ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();
        //    dcItemYr.Value = txtMatricula.Text;
        //    pfItemYr.CurrentValues.Add(dcItemYr);
        //    paramFields.Add(pfItemYr);
        //    CrystalReportViewer1.ParameterFieldInfo = paramFields;


        //    CrystalReportSource1.Report.FileName = Objr.relatorio;
        //    Session["relatorio"] = relatorio;
        //}

        protected void btnCarregarRelatorios_Click(object sender, EventArgs e)
        {

            try
            {
                CarregaRelatorio(1);
                btnRelatorio1.Visible = true;
                btnCarregarRelatorios.Visible = false;
            }
            catch (Exception ex)
            {
                MostraMensagemTela(this.Page, "Atenção \\n\\nOs relatórios não podem ser carregados.\\n\\nMotivo\\n\\n" + ex.Message);
            }
        }

        private bool CarregarOsb(int tipoRelatorio)
        {

            obj.registro = int.Parse(txtMatricula.Text);
            obj.cod_emprs = int.Parse(txtEMpresa.Text);

            CrystalReportViewer1.ParameterFieldInfo = null;

            DataTable dt = obj.ListarRelatorio();
            bool bSemTpAnt = false, bBenefCorrOuSemBenef = false;
          
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Relatorio osb = new Relatorio();
                    osb.arquivo = row["caminho_relatorio"].ToString();
                    osb.relatorio = row["desc_relatorio"].ToString();
                    osb.tipo = int.Parse(row["tipo_relatorio"].ToString());
                    osb.parametros = new List<Parametro>();
                    Parametro u;
                    u = new Parametro();
                    u.parametro = "matricula";
                    u.valor = txtMatricula.Text;
                    osb.parametros.Add(u);

                    u = new Parametro();
                    u.parametro = "empresa";
                    u.valor = txtEMpresa.Text;
                    osb.parametros.Add(u);

                    if (osb.relatorio.Contains("BenefCorreto") ||
                        osb.relatorio.Contains("BenefErrado")  ||
                        osb.relatorio.Contains("Sem_Benef"))
                    {
                        u = new Parametro();
                        u.parametro = "data_emissao";
                        u.valor = txtDataEmissao.Text;
                        osb.parametros.Add(u);
                        if (osb.relatorio.Contains("BenefCorreto") ||
                            osb.relatorio.Contains("Sem_Benef") ||
                            (osb.relatorio.Contains("BenefErrado") && dt.Rows.Count==1)) 
                        {
                            bBenefCorrOuSemBenef = true;
                            u = new Parametro();
                            u.parametro = "imprimir_rodape";
                            u.valor = "false";
                            osb.parametros.Add(u);
                        }
                    }
                    else if (osb.relatorio.Contains("SemTpAnt"))
                    {
                        bSemTpAnt = true;
                    }

                    lstRelOsb.Add(osb);
                }

                if ((bSemTpAnt && bBenefCorrOuSemBenef) || dt.Rows.Count==1) // Se houver somente um relatório, desabilita o btn de segundo relatório.
                {
                    lstRelOsb.Where(x => x.tipo == 1).FirstOrDefault().get_parametro("imprimir_rodape").valor = "true";
                    btnRelatorio2.Visible = false;
                }
                else
                {
                    btnRelatorio2.Visible = true;
                }

                return true;
            }
            else return false;
        }

        protected void btnRelatorio1_Click(object sender, EventArgs e)
        {

            try
            {
                CarregaRelatorio(1);
            }
            catch (Exception ex)
            {
                MostraMensagemTela(this.Page, "Atenção \\n\\nO relatório não pode ser carregado.\\n\\nMotivo\\n\\n" + ex.Message);
            }
        }

        protected void btnRelatorio2_Click(object sender, EventArgs e)
        {
            try
            {
                CarregaRelatorio(2);
            }
            catch (Exception ex)
            {
                MostraMensagemTela(this.Page, "Atenção \\n\\nO relatório não pode ser carregado.\\n\\nMotivo\\n\\n" + ex.Message);
            }
        }

        private string ValidaTela()
        {

            StringBuilder str = new StringBuilder();

            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                str.Append("Digite o número da matricula\\n");
                txtMatricula.Focus();
            }

            if (string.IsNullOrEmpty(txtEMpresa.Text))
            {
                str.Append("Digite o número da empresa\\n");
                txtEMpresa.Focus();

            }

            return str.ToString();

        }

        protected void GeraRelatorio(Relatorio Objr)
        {

            Session["relOsb"] = Objr;

            // Abrir relatorio
            relatorio = new ReportDocument();
            List<Parametro> parametros = Objr.parametros;
            String Relatorio_caminho = Objr.arquivo;
            Relatorio_caminho = Server.MapPath(@Relatorio_caminho);
            //    //relatorio.Load(Relatorio_caminho);
            //    relatorio.FileName = Relatorio_caminho;

            CrystalReportViewer1.ReportSource = null;
            TableLogOnInfo tableLogOnInfo = null;

            //relatorio.Load(Relatorio_caminho);
            relatorio.FileName  = Relatorio_caminho;

            foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in relatorio.Database.Tables)
            {
                tableLogOnInfo = tbl.LogOnInfo;
                tableLogOnInfo.ConnectionInfo.ServerName = builder.DataSource;
                tableLogOnInfo.ConnectionInfo.DatabaseName = "";
                tableLogOnInfo.ConnectionInfo.UserID = builder.UserID;
                tableLogOnInfo.ConnectionInfo.Password = builder.Password;
                tbl.ApplyLogOnInfo(tableLogOnInfo);
            }

            //    relatorio.SetDatabaseLogon(usuario, senha, banco, "");

            relatorio.SetDatabaseLogon(builder.UserID, builder.Password, builder.DataSource, "");

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

            CrystalReportViewer1.ReportSource = relatorio;
        }


        private void CarregaRelatorio(int tipoRelatorio)
        {

            string mensagem = ValidaTela();

            if (mensagem.Equals(""))
            {
                if (CarregarOsb(tipoRelatorio))
                {
                    if (!btnRelatorio2.Visible) tipoRelatorio = 1;

                    GeraRelatorio(lstRelOsb.Where(x => x.tipo == tipoRelatorio).FirstOrDefault());
                }
                else
                {
                    CrystalReportViewer1.ReportSource = null;
                    MostraMensagemTelaUpdatePanel(UpdatePanel1, "Erro ao carregar relatório tipo " + tipoRelatorio + ": \\n\\n" +
                                                                "Dados não localizados para esta matrícula/empresa.");
                }
            }
            else
            {
                CrystalReportViewer1.ReportSource = null;
                MostraMensagemTelaUpdatePanel(UpdatePanel1, "Atenão \\n \\n" + mensagem);
            }


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