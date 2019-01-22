
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Faturamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class AdInadiTela : BasePage
    {
        #region Atributos
        AdinResumoBLL obj = new AdinResumoBLL();
        #endregion

        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["relatorio"] != null)
            {
                ReportDocument doc = (ReportDocument)Session["relatorio"];
                CrystalReportViewer2.ReportSource = doc;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument relatorio = new ReportDocument();
                relatorio.Load(Server.MapPath(@"Relatorios\\Relatorio_Tela.rpt"));
                CrystalReportViewer2.ReportSource = relatorio;

                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.matricula = user.login;
                    string message = "";

                    obj.CriarRelatorio(out message);

                    MostraMensagemTelaUpdatePanel(upInaAd, "Atenção\\n\\n" + message);
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upInaAd, "Atenção \\n\\nRegistro não atualizado.\\nMotivo:\\n" + ex.Message);
            }


        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {

                string mesangem = ValidaTela();
                if (mesangem.Equals(""))
                {

                    GeraRelatorio(drpMes.SelectedValue + "/" + txtAno.Text);
                }
                else
                {
                    ReportDocument relatorio = new ReportDocument();
                    relatorio.Load(Server.MapPath(@"Relatorios\\Relatorio_Tela.rpt"));
                    CrystalReportViewer2.ReportSource = relatorio;
                    MostraMensagemTelaUpdatePanel(upInaAd, "Atenção\\n\\n" + mesangem);
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upInaAd,  ex.Message);
            }
        }



        #endregion

        #region Métodos
        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {
            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        private string ValidaTela()
        {

            StringBuilder str = new StringBuilder();
            int ano = 0;

            if (drpMes.SelectedValue == "0")
            {
                str.Append("Selecione um Mês.\\n");
            }


            if (!txtAno.Text.Equals(""))
            {
                if (int.TryParse(txtAno.Text, out ano))
                {
                    if (ano < 1000)
                    {
                        txtAno.Text = "";
                        str.Append("Digite o ano no formato (YYYY).\\n");
                    }
                }
                else
                {
                    txtAno.Text = "";
                    str.Append("Digite apenas números.\\n");
                }
            }
            else
            {

                str.Append("Digite o Ano.\\n");

            }

            return str.ToString();

        }

        protected void GeraRelatorio(string valor)
        {
            // Abrir relatorio
            ReportDocument relatorio = new ReportDocument();
            String Relatorio_caminho = "Relatorios\\Rel_Resumo_InAd.rpt";
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
            CrystalReportViewer2.ReportSource = relatorio;

            // Passar os parametros para o relatorio
            CrystalReportViewer2.RefreshReport();
            ParameterFields paramFields = new ParameterFields();
          

            ParameterField pfItemYr = new ParameterField();
            pfItemYr.ParameterFieldName = "mesano";
            ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();
            dcItemYr.Value =valor;
            pfItemYr.CurrentValues.Add(dcItemYr);
            paramFields.Add(pfItemYr);
            CrystalReportViewer2.ParameterFieldInfo = paramFields;


            CrystalReportSource2.Report.FileName = "Rel_Resumo_InAd.rpt";
            Session["relatorio"] = relatorio;
        }
        #endregion


    }
}