using IntegWeb.Entidades.Framework;
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
    public partial class AuditEmprPlano : BasePage
    {

        #region .: Propriedades :.

        StringBuilder criticas = new StringBuilder();
        #endregion

        #region .: Eventos :.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {

            if (FileUploadControl.HasFile)
            {
                if (FileUploadControl.PostedFile.ContentType.Equals("application/vnd.ms-excel") || // xls
                   FileUploadControl.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))//xlsx
                {
                    string Excel = "";

                    try
                    {
                        string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                        string UploadFilePath = Server.MapPath("UploadFile\\");
                        Excel = UploadFilePath + name[0] + "." + name[1];

                        if (!Directory.Exists(UploadFilePath))
                        {
                            Directory.CreateDirectory(UploadFilePath);
                        }

                        FileUploadControl.SaveAs(Excel);
                        DataTable dtExcel = ReadExcelFile(Excel);


                        foreach (DataRow drExcelLinha in dtExcel.Rows)
                        {
                            if (!drExcelLinha.Table.Columns.Contains("Matrícula"))
                            {
                                throw new Exception("A Coluna Matrícula não existe, Favor verificar");
                            }
                            if (!drExcelLinha.Table.Columns.Contains("Nome"))
                            {
                                throw new Exception("A Coluna Nome não existe, Favor verificar");
                            }
                        }

                        DataTable dtExtrair = new DataTable();
                        dtExtrair.Columns.Add("CODIGO");
                        dtExtrair.Columns.Add("EMPRESA");
                        dtExtrair.Columns.Add("MATRÍCULA");
                        dtExtrair.Columns.Add("SUB");
                        dtExtrair.Columns.Add("NOME");
                        dtExtrair.Columns.Add("CODIGO PLANO");
                        dtExtrair.Columns.Add("PLANO");

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            AuditEmpPlanoBLL bll = new AuditEmpPlanoBLL();
                            AuditEmpPlanoBLL.AuditEmpPlanoRpt obj = new AuditEmpPlanoBLL.AuditEmpPlanoRpt();


                            string pCarteira = dtExcel.Rows[i]["Matrícula"].ToString();

                            if (pCarteira.Length == 14)
                            {


                                string pEmpresa = dtExcel.Rows[i]["Matrícula"].ToString().Substring(0, 3);
                                string pMatricula = dtExcel.Rows[i]["Matrícula"].ToString().Substring(3, 7);
                                string pSub = dtExcel.Rows[i]["Matrícula"].ToString().Substring(11, 2);
                                string pNome = dtExcel.Rows[i]["Nome"].ToString();


                                obj = bll.GetPlan(pCarteira, pEmpresa, pMatricula, pSub, pNome);

                                if (!obj.Equals(null))
                                {
                                    dtExtrair.Rows.Add(obj.CARTEIRA, obj.COD_EMPRS, obj.MATRICULA, obj.NUM_SUB_MATRIC, obj.NOME, obj.COD_PLANO, obj.PLANO);
                                }

                                if ( dtExcel.Rows[i]["Nome"].ToString() != "")
                                {
                                    string pNomePesq = dtExcel.Rows[i]["Nome"].ToString();

                                  //  obj = bll.GetNome(pCarteira, pNomePesq);

                                    if (!obj.Equals(null))
                                    {
                                        dtExtrair.Rows.Add(obj.CARTEIRA, obj.COD_EMPRS, obj.MATRICULA, obj.NUM_SUB_MATRIC, obj.NOME, obj.COD_PLANO, obj.PLANO);
                                    }
                                    
                                }
                                else
                                {
                                    List<Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica> listcriticas = new List<Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica>();

                                    Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica critica = new Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica("", "", "");
                                    criticas.AppendLine(Environment.NewLine + dtExcel.Rows[i]["Matrícula"].ToString() + " - " + dtExcel.Rows[i]["Nome"].ToString() + " - <span class='alert_inline'>Matrícula não encontrada</span><br/><br/>");
                                    critica = new Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica(dtExcel.Rows[i]["Nome"].ToString(), dtExcel.Rows[i]["Matrícula"].ToString(), "Matrícula não encontrada");
                                    listcriticas.Add(critica);
                                }

                            }
                            else
                            {

                                List<Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica> listcriticas = new List<Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica>();

                                Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica critica = new Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica("", "", "");

                                criticas.AppendLine(Environment.NewLine + dtExcel.Rows[i]["Matrícula"].ToString() + " - " + dtExcel.Rows[i]["Nome"].ToString() + " - <span class='alert_inline'>Matrícula inválida - Não Possui as 14 posições</span><br/><br/>");
                                critica = new Aplicacao.DAL.Auditoria.AuditEmpPlanoDAL.AuditEmpPlanoRptCritica(dtExcel.Rows[i]["Nome"].ToString(), dtExcel.Rows[i]["Matrícula"].ToString(), "Matrícula inválida - Não Possui as 14 posições");
                                listcriticas.Add(critica);

                            }

                        }

                        //download excel
                        ArquivoDownload adXlsDbtConta = new ArquivoDownload();
                        adXlsDbtConta.nome_arquivo = name[0] + "Emp x Plan" + DateTime.Today.ToShortDateString() + ".xls";
                        //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
                        adXlsDbtConta.dados = dtExtrair;
                        Session[ValidaCaracteres(adXlsDbtConta.nome_arquivo)] = adXlsDbtConta;
                        string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsDbtConta.nome_arquivo);
                        //AdicionarAcesso(fUrl);
                        AbrirNovaAba(UpdatePanel, fUrl, adXlsDbtConta.nome_arquivo);
                        lblCriticas.Text = criticas.ToString();


                    }


                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, ex.Message);
                    }

                    finally
                    {
                        FileUploadControl.Dispose();
                    }

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção carrega arquivos xls ou xlsx");
                }

            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção carrega a planilha para continuar");
            }


        }

        #endregion



        #region .: Métodos :.
        #endregion




    }
}
