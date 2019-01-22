using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using System.Threading;
using System.Data;
using IntegWeb.Entidades.Framework;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Net;
using IntegWeb.Framework;
using System.Configuration;



namespace IntegWeb.Saude.Web
{


    public partial class RegistrosOficiais : BasePage
    {
        #region .:Propriedades:.
        #endregion

        #region .:Eventos:.

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                RegistrosOficiaisBLL bll = new RegistrosOficiaisBLL();

                dllMesAno.DataSource = bll.GetDados();
                dllMesAno.DataTextField = "DATA_DESC";
                dllMesAno.DataValueField = "DATA_DESC";
                dllMesAno.DataBind();
                dllMesAno.Items.Insert(0, new ListItem("---Selecione---", ""));

                //Timer.Enabled = false;
            }
            else
            {

            }
        }

        #region .:ABA 1:.

        protected void btnGerar_Click(object sender, EventArgs e)
        {

            ProcessarRo();

        }

        //protected void Timer_Tick1(object sender, EventArgs e)
        //{
        //    CarregarStatus();
        //}

        #endregion

        #region .:ABA 2:.

        protected void dllMesAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dllMesAno.SelectedIndex == 0)
            {
                gridRel.Visible = false;
            }
            else
            {
                gridRel.Visible = true;

                RegistrosOficiaisBLL bll = new RegistrosOficiaisBLL();

                string[] dllvalor = dllMesAno.SelectedValue.Split('/');

                decimal mes = Convert.ToDecimal(dllvalor[0]);
                decimal ano = Convert.ToDecimal(dllvalor[1]);

                gridRel.DataSource = bll.GetRel(mes, ano);
                gridRel.DataBind();
            }
        }

        protected void gridRel_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            string host = "";
            string username = "";
            string password = "";

            //verifica o ambiente para conexão do ftp
            if (ConfigurationManager.AppSettings["Config"] == "P")
            {
                 host = "fcespracp004";
                 username = "integ";
                 password = "modelo00";
            }
            else if(ConfigurationManager.AppSettings["Config"] == "T")
            {
                 host = "fcesporah001";
                 username = "integ";
                 password = "newtst";

            }
            else
            {
                 host = "fcesporad001";
                 username = "integ";
                 password = "newdev";
            }



            //pega o codigo do relatório
            int id = Convert.ToInt32(gridRel.Rows[gridRel.SelectedIndex].Cells[0].Text);

            //pega o mes
            string[] data = dllMesAno.SelectedValue.Split('/');

            // pega o nº do mes e converte em nome
            string mes = VerificarMes(Convert.ToInt32(data[0]));

           //nome para procurar no ftp
            string nomeRelatorio = "REL-RO_PESL_" + Convert.ToString(id) + ".csv";

            //gerar o nome do arquivo csv
            string nomeSaida = "RO_PESL_" + mes + "_" + Convert.ToString(id) + ".csv";

            
            ////verifica se o arquivo existe no ftp
            bool verifica = VerificaArquivoFTP(host, username, password, nomeRelatorio);

            

            if (verifica == true)
            {

                Stream stream = DownloadArqFTP(host, username, password, nomeRelatorio);

                GeraSaidaCsv(stream, nomeSaida);

                stream.Dispose();
                stream.Close();

                MostraMensagemTelaUpdatePanel(upUpdatePanel, " Download Realizado com sucesso !!" + "\t" + " desbloquear pop-up caso não tenha iniciado o download");

                // Session.Clear();

                dllMesAno.SelectedIndex = 0;
                gridRel.DataBind();

            }
            else
            {

                RegistrosOficiaisBLL bll = new RegistrosOficiaisBLL();

                bll.ProcessarRel(id);

                Stream stream = DownloadArqFTP(host, username, password, nomeRelatorio);

                GeraSaidaCsv(stream, nomeSaida);

                stream.Dispose();
                stream.Close();

                MostraMensagemTelaUpdatePanel(upUpdatePanel, " Download Realizado com sucesso !!" + "\n" +" desbloquear pop-up caso não tenha iniciado o download");

                dllMesAno.SelectedIndex = 0;
                gridRel.DataBind();
            }
            

        }

        protected void TabContainer_ActiveTabChanged(object sender, EventArgs e)
        {
            RegistrosOficiaisBLL bll = new RegistrosOficiaisBLL();

            dllMesAno.DataSource = bll.GetDados();
            dllMesAno.DataBind();
            dllMesAno.Items.Insert(0, new ListItem("---Selecione---", ""));
            // dllMesAno.SelectedIndex = 0;

            gridRel.Visible = false;

        }
       
        #endregion

        #endregion

        #region .:Métodos:.

        #region .:ABA 1:.

        //protected void CarregarStatus()
        //{
        //    RegistrosOficiaisBLL bll = new RegistrosOficiaisBLL();

        //    Label LabelStatuss = (Label)UpdateProg.FindControl("LabelStatus");

        //    LabelStatuss.Text = "...";



        //    //if (LabelStatus.Text == "REMOÇÃO DE DADOS AUXILIARES FINALIZADA...")
        //    //{
        //    //    Timer.Enabled = false;
        //    //    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Dados Gerados com Sucesso !");
        //    //    txtMes.Text = "";
        //    //    txtAno.Text = "";
        //    //    LabelStatus.Visible = false;
        //    //}


        //}

        protected void ProcessarRo()
        {
            RegistrosOficiaisBLL bll = new RegistrosOficiaisBLL();
            int mes = Convert.ToInt32(txtMes.Text);
            int ano = Convert.ToInt32(txtAno.Text);

            if (mes == 12)
            {
                bll.ProcessarRO( 1 , ano + 1);
            }
            else 
            {
                bll.ProcessarRO(mes + 1, ano);
            }

            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Dados do RO Referente ao mês " + (mes) + "/" + ano + " foram gerados com sucesso!");

            txtMes.Text = "";
            txtAno.Text = "";

        }

        #endregion

        #region .:ABA 2:.

        public bool VerificaArquivoFTP(string host, string username, string password, string nameArq)
        {
            bool existe = false;

            FileInfo _arquivoInfo = new FileInfo(nameArq);
            string uri = "ftp://" + host + "/" + _arquivoInfo.Name;
            FtpWebRequest requisicaoFTP;

            requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + host + "/" + _arquivoInfo.Name));
            requisicaoFTP.Credentials = new NetworkCredential(username, password);
            requisicaoFTP.Method = WebRequestMethods.Ftp.GetFileSize;
            try
            {
                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();
                existe = true;
                response.Close();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
            }
            return existe;


        }

        public Stream DownloadArqFTP(string host, string username, string password, string nameArq)
        {
            MemoryStream ms = new MemoryStream();



            FileInfo _arquivoInfo = new FileInfo(nameArq);
            string uri = "ftp://" + host + "/" + _arquivoInfo.Name;
            FtpWebRequest requisicaoFTP;

            try
            {
                // Cria um objeto FtpWebRequest a partir da Uri fornecida
                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(uri);

                // Fornece as credenciais de WebPermission
                requisicaoFTP.Credentials = new NetworkCredential(username, password);

                // Por padrão KeepAlive é true, 
                requisicaoFTP.KeepAlive = false;

                // Especifica o comando a ser executado
                requisicaoFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                // Especifica o tipo de dados a ser transferido
                requisicaoFTP.UseBinary = true;

                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();


                response.GetResponseStream().CopyTo(ms);

                ms.Seek(0, System.IO.SeekOrigin.Begin);

                response.Dispose();
                response.Close();

            }
            catch (OutOfMemoryException e)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Verificar se o download foi realizado, desbloquear pop-up caso não tenha sido iniciado");
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro: " + ex.Message);
            }
          

            return ms;
        }

        public void GeraSaidaCsv(Stream arquivo, string extensao_anexo)
        {
            ArquivoDownload preparaDados = new ArquivoDownload();

            preparaDados.dados = Util.ToByteArray(arquivo);
            preparaDados.nome_arquivo = extensao_anexo;
            Session[preparaDados.nome_arquivo] = preparaDados;
            string fullUrl = "WebFile.aspx?dwFile=" + preparaDados.nome_arquivo;
            AdicionarAcesso(fullUrl);
            AbrirNovaAba(upUpdatePanel, fullUrl, preparaDados.nome_arquivo);

            arquivo.Close();
        }

        public string VerificarMes(int mes) 
        {
            string msg = "";

            switch(mes)
            {
                case 1: return msg= "JANEIRO";

                break;

                case 2: return msg = "FEVEREIRO";

                break;
                    
                case 3: return msg = "MARÇO";

                break;

                case 4 : return msg = "ABRIL";

                break;

                case 5: return msg = "MAIO";
                break;

                case 6 : return msg = "JUNHO";

                break;

                case 7 : return msg = "JULHO";

                break;
                case 8 : return msg = "AGOSTO";

                break;
                
                case 9 : return msg = "SETEMBRO";

                break;
                
                case 10 :return msg = "OUTUBRO";

                break;

                case 11 : return msg = "NOVEMBRO";

                break;

                case 12 : return msg = "DEZEMBRO";

                break;
            }

            return msg;
        }
        
        //public void CreateCSVFile(DataTable dtDataTablesList)
        //{
        //    string strFilePath = @"D:\Users\e40059\Desktop\lindo\myCSVfile.csv";

        //    // Create the CSV file to which grid data will be exported.

        //    StreamWriter sw = new StreamWriter(strFilePath, false);

        //    //First we will write the headers.

        //    int iColCount = dtDataTablesList.Columns.Count;

        //    for (int i = 0; i < iColCount; i++)
        //    {
        //        sw.Write(dtDataTablesList.Columns[i]);
        //        if (i < iColCount - 1)
        //        {
        //            sw.Write(",");
        //        }
        //    }
        //    sw.Write(sw.NewLine);

        //    // Now write all the rows.

        //    foreach (DataRow dr in dtDataTablesList.Rows)
        //    {
        //        for (int i = 0; i < iColCount; i++)
        //        {
        //            if (!Convert.IsDBNull(dr[i]))
        //            {
        //                sw.Write(dr[i].ToString());
        //            }
        //            if (i < iColCount - 1)
        //            {
        //                sw.Write(",");
        //            }
        //        }
        //        sw.Write(sw.NewLine);
        //    }
        //    sw.Close();
        //}

        #endregion

        #endregion






    }
}