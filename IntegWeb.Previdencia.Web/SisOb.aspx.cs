using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Entidades.Previdencia;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class SisOb : BasePage
    {
        #region Atributos
        ArquivoSisObBLL objbll = new ArquivoSisObBLL();
        ArquivoSisOb obj = new ArquivoSisOb();
#endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
         

        }

        protected void btnUpLoad_Click(object sender, EventArgs e)
        {

            string mesangem = ValidaTela();
            if (mesangem.Equals(""))
            {

                try
                {
                    if (FileUploadControl.PostedFile.ContentType == "text/plain")
                    {
                        {
                            string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                            string path = Server.MapPath("UploadFile\\") + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
                            FileUploadControl.SaveAs(path);
                            List<ArquivoSisOb> list = ImportTxtToList(path);
                            ImportListToDataBase(list);
                        }
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\nCarregue apenas arquivos Texto.");
                }
                catch (Exception ex)
                {
                    MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
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
                MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\n" + mesangem);
            }
        }

     
        #endregion

        #region Métodos
        public List<ArquivoSisOb> ImportTxtToList(string filelocation)
        {
            List<ArquivoSisOb> list = new List<ArquivoSisOb>();
            if (Session["objUser"] != null)
            {
                var user = (ConectaAD)Session["objUser"];

                System.IO.StreamReader file = new System.IO.StreamReader(@filelocation);
                string line;
                string mesano = drpMes.SelectedValue + "/" + txtAno.Text;

                while ((line = file.ReadLine()) != null)
                {
                    ArquivoSisOb obj = new ArquivoSisOb(
                                        line.Substring(0, 6),
                                        line.Substring(6, 5),
                                        line.Substring(11, 10),
                                        line.Substring(21, 8),
                                        line.Substring(29, 10),
                                        line.Substring(39, 76),
                                        line.Substring(115, 32),
                                        line.Substring(147, 8),
                                        line.Substring(155, 8),
                                        line.Substring(163, 11),
                                        line.Substring(174, 11),
                                        line.Substring(185, 1),
                                        line.Substring(186, 14),
                                        line.Substring(200, 10),
                                        mesano,
                                        user.login,
                                        DateTime.Now.ToString()
                                        );
                    list.Add(obj);

                }
            }
            return list;

        }

        private void ImportListToDataBase(List<ArquivoSisOb> list)
        {

            string horainicio = DateTime.Now.ToShortTimeString();
            bool ret = false;
            if (list.Count > 0)
            {
                if (objbll.Deletar(list[0].mesanoref))
                {
                    ret = objbll.Inserir(list);

                    if (ret)
                    {
                        LimpaCampos();
                        MostraMensagemTelaUpdatePanel(upSys, " Arquivo Carregado com sucesso!");
                    }
                }
            }
            else
                MostraMensagemTelaUpdatePanel(upSys, "Arquivo com problema!");

            StatusLabel.Text = "Upload Status: Início " + horainicio + " >>>> Fim    " + DateTime.Now.ToShortTimeString();
            contador.Text = "Número de Registros importados: " + list.Count.ToString();
        }

        private string ValidaTela()
        {

            StringBuilder str = new StringBuilder();
            int ano = 0;
            if (!FileUploadControl.HasFile)
            {

                str.Append("Selecione o Arquivo para importação.\\n");

            }
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

        private void LimpaCampos()
        {

            txtAno.Text = "";
            drpMes.SelectedValue = "0";
        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

     
        #endregion


    }
}