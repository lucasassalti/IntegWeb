using IntegWeb.Entidades.Relatorio;
using IntegWeb.Entidades.Financeira.Tesouraria;
using IntegWeb.Framework;
using IntegWeb.Financeira.Aplicacao.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web
{
    public partial class MovDiarioTela : BasePage
    {

        #region Propriedades 

        Relatorio relatorio = new Relatorio();
        string relatorio_titulo = "Relatório de Leitura MOV_DIARIA";
        string relatorio_inclusao = @"~/Relatorios/Rel_Mov_Diaria.rpt";
        string relatorio_nome = "Rel_Mov_Diaria.rpt";
        #endregion

        #region Atributos
        MovDiario obj = new MovDiario();
        #endregion

        #region Eventos
        protected void grdImportacao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                switch (e.CommandName)
                {
                    case "Exportar":
                        obj.dt_inclusao = e.CommandArgument.ToString();
                        DataTable dt = new MovDiarioBLL().BuscaDetalheImportacao(obj);

                        if (dt.Rows.Count > 0)
                        {

                            var nomeArquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_ControleCRC" + ".xls";

                            Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                            dtRelatorio.Add(nomeArquivo, dt);
                            Session["DtRelatorio"] = dtRelatorio;
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                            BasePage.AbrirNovaAba(this, "WebFile.aspx", "OPEN_WINDOW");


                        }
                        else
                            MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\n" + "A consulta não retornou dados");

                        break;
                    //case "Deletar":
                    //    obj.dt_inclusao = e.CommandArgument.ToString();
                    //    bool ret = new MovDiarioBLL().Deletar(obj);
                    //    if (ret)
                    //    {
                    //        if (ViewState["Parametros"] != null)
                    //        {
                    //            string[] par = ViewState["Parametros"].ToString().Split(char.Parse("|"));
                    //            CarregaGrid("grdImportacao", new MovDiarioBLL().BuscaImportacao(par[0], par[1]), grdImportacao);
                    //            MostraMensagemTelaUpdatePanel(upSys, "Importação Deletada com Sucesso!");
                    //        }

                    //    }
                    //    else
                    //        MostraMensagemTelaUpdatePanel(upSys, "Problemas contate o administrador de sistemas!");
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\nA importação não pôde ser deletada.\\nMotivo:\\n" + ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
              Session[relatorio_nome] = relatorio;
              ReportCrystal.DataBind();
            }
            if (!IsPostBack)
            {
                Session[relatorio_nome] = relatorio;
            }
        }

        protected void btnUpLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUploadControl.HasFile)
                {


                    if (FileUploadControl.PostedFile.ContentType == "text/plain")
                    {
                        {
                            string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                            string path = Server.MapPath("UploadFile\\") + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
                            FileUploadControl.SaveAs(path);
                            List<MovDiario> list = ImportTxtToList(path);
                            ImportListToDataBase(list);
                        }
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\nCarregue apenas arquivos Texto.");

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\n" + "Selecione o Arquivo para importação.");
                }
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

        //protected void btnConsultar_Click(object sender, EventArgs e)
        //{
        //    string message = ValidaCampos();
        //    if (string.IsNullOrEmpty(message))
        //    {
        //        ViewState["Parametros"] = txtdtIni.Text + "|" + txtdtFin.Text;
        //        CarregaGrid("grdImportacao", new MovDiarioBLL().BuscaImportacao(txtdtIni.Text, txtdtFin.Text), grdImportacao);
        //    }
        //    else
        //        MostraMensagemTelaUpdatePanel(upSys, "Atenção!\\n" + message);
        //}

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            if (InicializaRelatorio())
            {
                ReportCrystal.VisualizaRelatorio();
                //bool alerta = false;
                //Session[relatorio_nome] = relatorio;
                //AbrirNovaAba(this.Page, "RelatorioWeb.aspx?Relatorio_nome=" + relatorio_nome + "&PromptParam=false&Popup=true" + (alerta ? "&Alert=true" : ""), relatorio_nome);           
            }
        }

        #endregion

        #region Métodos
        private void ImportListToDataBase(List<MovDiario> list)
        {

            string horainicio = DateTime.Now.ToShortTimeString();
            bool ret = false;
            if (list.Count > 0)
            {
                //if (new MovDiarioBLL().Deletar(list[0].mesano))
                //{
                ret = new MovDiarioBLL().Inserir(list);

                if (ret)
                {
                    //LimpaCampos();
                    MostraMensagemTelaUpdatePanel(upSys, " Arquivo Carregado com sucesso!");
                }
                // }
            }
            else
                MostraMensagemTelaUpdatePanel(upSys, "Arquivo com problema!");

            StatusLabel.Text = "Upload Status: Início " + horainicio + " >>>> Fim    " + DateTime.Now.ToShortTimeString();
            contador.Text = "Número de Registros importados: " + list.Count.ToString();
        }

        //public string ValidaCampos()
        //{

        //    StringBuilder str = new StringBuilder();
        //    if (string.IsNullOrEmpty(txtdtIni.Text))

        //        str.Append("\\nDigite a data Inicial");


        //    if (string.IsNullOrEmpty(txtdtFin.Text))
        //        str.Append("\\nDigite a data Final");

        //    return str.ToString();
        //}

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        public List<MovDiario> ImportTxtToList(string filelocation)
        {
            List<MovDiario> list = new List<MovDiario>();
            if (Session["objUser"] != null)
            {
                var user = (ConectaAD)Session["objUser"];

                System.IO.StreamReader file = new System.IO.StreamReader(@filelocation);
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    MovDiario obj = new MovDiario(
                                        line.Substring(0, 1),
                                        line.Substring(1, 3),
                                        line.Substring(4, 20),
                                        line.Substring(24, 10),
                                        line.Substring(34, 6),
                                        line.Substring(40, 6),
                                        line.Substring(46, 11),
                                        line.Substring(57, 35),
                                        line.Substring(101, 10),
                                        line.Substring(92, 9),
                                        line.Substring(111, 10),
                                        line.Substring(117, 4),
                                        line.Substring(121, 1),
                                        line.Substring(122, 3),
                                        line.Substring(125, 20),
                                        line.Substring(145, 10),
                                        line.Substring(111, 10),
                                        line.Substring(125, 9),
                                        line.Substring(155, 6),
                                        user.login,
                                        DateTime.Now.ToString()
                                        );
                    list.Add(obj);

                }
                //Se PRIMEIRA  linha estiver nula excluí 
                if (string.IsNullOrWhiteSpace(list.First().registro) && string.IsNullOrWhiteSpace(list.First().matricula) && string.IsNullOrWhiteSpace(list.First().representante) && string.IsNullOrWhiteSpace(list.First().cpf) && string.IsNullOrWhiteSpace(list.First().nome))
                    list.Remove(list.First());
                //Se ÚLTIMA  linha estiver nula excluí 
                if (string.IsNullOrWhiteSpace(list.Last().registro) && string.IsNullOrWhiteSpace(list.Last().matricula) && string.IsNullOrWhiteSpace(list.Last().representante) && string.IsNullOrWhiteSpace(list.Last().cpf) && string.IsNullOrWhiteSpace(list.Last().nome))
                    list.Remove(list.Last());
            }
            else
                Response.Redirect("Login.aspx");

            return list;

        }
        
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (ReportCrystal != null)
            {
                ReportCrystal.ID = null;
                ReportCrystal = null;
            }
        }

        private bool InicializaRelatorio()
        {
            relatorio.titulo = relatorio_titulo;
            relatorio.arquivo = relatorio_inclusao;
            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;
            return true;
        }

        #endregion 
    }
}