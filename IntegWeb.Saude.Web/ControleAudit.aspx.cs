using IntegWeb.Entidades.Saude.Auditoria;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL.Auditoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class ControleAudit : BasePage
    {
        #region Atributos
        ControleAuditBLL bll = new ControleAuditBLL();
        EmpAuditBLL empbll = new EmpAuditBLL();
        AuditControle obj = new AuditControle();
        #endregion

        #region Eventos     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaTela();
            }
        
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string msg = ValidaTela();
            if (msg.Equals(""))
            {

                try
                {
                    string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                    string path = Server.MapPath("Spool_Arquivos\\") + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
                    FileUploadControl.SaveAs(path);
                    Session["path"] = path;
                    obj.mesano = drpMes.SelectedValue + "/" + txtAno.Text;
                    obj.id_empaudit = int.Parse(drpEmpresa.SelectedValue);
                    DataTable dt = bll.ListaProcesso(obj);
                    if (dt.Rows.Count == 0)
                    {
                        ImportaDados();
                    }
                    else
                    {
                        lblResponsavel.Text = "Responsável: " + dt.Rows[0]["MATRICULA"].ToString();
                        lblData.Text = "Data: " + DateTime.Parse(dt.Rows[0]["DT_INCLUSAO"].ToString()).ToShortDateString();
                        lblHorario.Text = "Horário: " + String.Format("{0:T}", DateTime.Parse(dt.Rows[0]["DT_INCLUSAO"].ToString()));
                        ModalBox(Page, lnkErro1.ClientID);
                    }
                }
                catch (Exception ex)
                {
                    MostraMensagemTelaUpdatePanel(upAudit, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
                }
                finally
                {
                    FileUploadControl.FileContent.Dispose();
                    FileUploadControl.FileContent.Flush();
                    FileUploadControl.PostedFile.InputStream.Flush();
                    FileUploadControl.PostedFile.InputStream.Close();
                }

            }
            else
            {

                MostraMensagemTelaUpdatePanel(upAudit, "Atenção\\n\\n" + msg);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            ImportaDados();
        }
        #endregion

        #region Métodos

        private void CarregaTela() {

            CarregaDropDowDT(empbll.ListaTodos(new EmpAudit()), drpEmpresa);
        }

        private bool VerificaPadrao(DataTable dt)
        {
            string[] vet = { "TipoServico", "Nome", "Matricula", "Hospital", "DataInternacao", "DataAlta", "Custo", "Glosa", "Cobrado", "PagoFuncesp" };

            string aux = "";
            bool IsOk = false;

            foreach (var item in vet)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName.Equals(item))
                    {

                        IsOk = true;
                        break;
                    }
                }
                if (IsOk)
                    IsOk = false;
                else
                    aux += item + " | ";
            }
            lbColuns.Text = aux;
            return aux.Equals("");
        }

        private void LimpaCampos()
        {

            txtAno.Text = "";
            drpEmpresa.SelectedValue = "0";
            drpMes.SelectedValue = "0";
        }

        private string ValidaTela()
        {

            StringBuilder str = new StringBuilder();
            int ano = 0;
            if (!FileUploadControl.HasFile)
            {

                str.Append("Selecione o Arquivo para importação.\\n");

            }

            if (!FileUploadControl.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")) // formato superior 2003
            {
                str.Append("Carregue apenas arquivos Excel 2007 (.xlsx) ou superior\\nVerifique se o arquivo carregado esta aberto através do Excel\\n");

            }

            if (drpMes.SelectedValue == "0")
            {
                str.Append("Selecione um Mês.\\n");
            }

            if (drpEmpresa.SelectedValue == "0")
            {
                str.Append("Selecione uma Empresa.\\n");
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

        private void ImportaDados()
        {
         if (Session["objUser"] != null || Session["path"]!=null)
           {
               var user = (ConectaAD)Session["objUser"];
                string path = (string)Session["path"];


                //Lê o Excel e converte para DataSet
                DataTable ds = ReadExcelFile(path);

                if (VerificaPadrao(ds))
                {
                    Session["path"] = null;
                    obj.responsavel = user.login;
                    obj.mesano = drpMes.SelectedValue + "/" + txtAno.Text;
                    obj.id_empaudit = int.Parse(drpEmpresa.SelectedValue);
                    bool ret = bll.ImportaDados(ds, obj);
                    if (ret)
                    {
                        LimpaCampos();
                        MostraMensagemTelaUpdatePanel(upAudit, "Dados Importados com Sucesso!");
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upAudit, "Problemas Contate o Administrador de Sistemas!");

                }
                else
                    ModalBox(Page, lnkErro.ClientID);
            }

        }

        #endregion

    }
}