using IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial;
using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class AdmVerba : BasePage
    {
        #region Atributos
        AcaoJudicialBLL obj = new AcaoJudicialBLL();
        FichaFinanceiraBLL ficha = new FichaFinanceiraBLL();
        string mensagem = "";
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {


            if (FileUploadControl.HasFile)
            {

                if (//FileUploadControl.PostedFile.ContentType.Equals("application/vnd.ms-excel") ||  //formato 2003
                   FileUploadControl.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")) // formato superior 2003
                {

                    try
                    {

                        string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                        string path = Server.MapPath("UploadFile\\") + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                        FileUploadControl.SaveAs(path);
                        //Lê o Excel e converte para DataSet
                        DataSet ds = ReadExcelFileWork(path);

                        List<FichaFinanceira> list = ImportDataTableToList(ds);

                        ficha.InsereVerba(list, out mensagem);

                        MostraMensagemTelaUpdatePanel(upVerba, mensagem);


                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upVerba, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
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

                    MostraMensagemTelaUpdatePanel(upVerba, "Atenção\\n\\nCarregue apenas arquivos Excel 2007 (.xlsx) ou superior!");
                }

            }

            else
            {

                MostraMensagemTelaUpdatePanel(upVerba, "Atenção\\n\\nSelecione um Arquivo para continuar!");
            }

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!txtCodMatricula.Equals("") && !txtCodEmpresa.Equals(""))
            {

                obj.cod_emprs = int.Parse(txtCodEmpresa.Text);
                obj.matricula = int.Parse(txtCodMatricula.Text);

                DataTable dt = obj.CarregaParticipante();

                lblNome.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    lblNome.Text = "Nome do Participante: " + dt.Rows[0]["nom_emprg"].ToString();
                    hdNumPartif.Value = dt.Rows[0]["NUM_MATR_PARTF"].ToString();
                    tabImport.Visible = true;

                }
                else
                {
                    tabImport.Visible = false;

                    lblNome.Text = "Nenhum registro encontrado ";
                    hdNumPartif.Value = "0";
                }

            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {

            txtCodMatricula.Text = "";
            txtCodEmpresa.Text = "";

        }

        protected void btnDeletarVerba_Click(object sender, EventArgs e)
        {
            ficha.num_matr_partf = int.Parse(hdNumPartif.Value);
            ficha.ExcluirVerba(out mensagem);
            MostraMensagemTelaUpdatePanel(upVerba, mensagem);
        }

        #endregion

        #region Métodos
        public List<FichaFinanceira> ImportDataTableToList(DataSet dts)
        {
            List<FichaFinanceira> list = new List<FichaFinanceira>();

            foreach (DataTable table in dts.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {

                    FichaFinanceira obj =
                        new FichaFinanceira(int.Parse(dr["Empresa"].ToString()),
                                            int.Parse(dr["Registro"].ToString()),
                                            int.Parse(table.Columns.Contains("VerbaFixa") ? dr["VerbaFixa"].ToString() : dr["VerbaVariavel"].ToString()),
                                            decimal.Parse(table.Columns.Contains("ValorFixo") ? dr["ValorFixo"].ToString() : dr["ValorVariavel"].ToString()),
                                            int.Parse(dr["Ano"].ToString()),
                                            int.Parse(dr["Mes"].ToString()),
                                            int.Parse(dr["Ano"].ToString()),
                                            int.Parse(dr["Mes"].ToString()),
                                            int.Parse(hdNumPartif.Value),
                                            DateTime.Now);
                    list.Add(obj);


                }
            }


            return list;

        }


        #endregion
    }
}