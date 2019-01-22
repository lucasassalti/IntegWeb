using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.BLL.Cobranca;

namespace IntegWeb.Saude.Web
{
    public partial class BoletoCobrancaSaude : BasePage
    {
        BoletoCobrancaSaudeBLL bll = new BoletoCobrancaSaudeBLL();


        #region .:Eventos:.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlTiposTxt.Items.Insert(0, new ListItem("Selecione o arquivo TXT", ""));
                ddlTipoPdf.Items.Insert(0, new ListItem("Selecione o arquivo PDF", ""));
                ddlTipoRel.Items.Insert(0, new ListItem("Selecione o Relatório", ""));
                ddlTipoRotinaIna.Items.Insert(0, new ListItem("Selecione a Rotina que deseja processar", ""));
            }
        }

       

  

        #region .:Botões:.

        protected void btnBoletos_Click(object sender, EventArgs e)
        {

            try 
            {
                decimal lbl;
                bll.ProcessarCobrancaSaude(Convert.ToDateTime(txtDtVenc.Text), Convert.ToDateTime(txtDtTol.Text), Convert.ToDecimal(txtNumLote.Text), out lbl );

                lblAba1.Text = "Número do Lote: " + lbl;

                DivSucessoAba1.Visible = true;
                divPrincipalAba1.Visible = false;
            }
            catch(Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel,ex.Message);

            }

           


        }

        protected void btnInadimplentes_Click(object sender, EventArgs e)
        {

            try
            {
                DivSucessoAba3.Visible = false;

                bll.ProcessarInadimplentes(Convert.ToDecimal(ddlTipoRotinaIna.SelectedValue), Convert.ToDateTime(txtDtVencAba3.Text), Convert.ToDecimal(txtNumLoteAba3.Text));

                if (ddlTipoRotinaIna.SelectedValue == "2")
                {
                    lbl2Aba3.Text = "Próxima Rotina a ser processada - Aba 4º Inclusão Aviso Cancelamento ";

                    DivSucessoAba3.Visible = true;
                    divPrincipalAba3.Visible = false;
                }

                DivSucessoAba3.Visible = true;
                //divPrincipalAba3.Visible = false;

               
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, ex.Message);
            }

           
        }

        protected void btnRel_Click(object sender, EventArgs e)
        {
            lblCount.Visible = false;
            try
            {
                DataTable dt = bll.ProcessarRelatorios(Convert.ToDecimal(ddlTipoRel.SelectedValue), Convert.ToDateTime(txtDtVencAba5.Text), Convert.ToDecimal(txtNumLoteAba5.Text));
                lblCount.Text = "Quantidade de registros na Planilha: " + dt.Rows.Count.ToString();
                lblCount.Visible = true;
              
                if (dt.Rows.Count > 0)
                {
                    Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                    var nomeArquivo = Convert.ToString(DateTime.Today.Day) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Year) + "_" + ddlTipoRel.SelectedItem + ".xls";
                    dtRelatorio.Add(nomeArquivo, dt);
                    Session["DtRelatorio"] = dtRelatorio;
                    BasePage.AbrirNovaAba(upUpdatePanel, "WebFile.aspx", "OPEN_WINDOW");
                }
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, ex.Message);
            }


            
        }

        protected void btnFlags_Click(object sender, EventArgs e)
        {

            try
            {
                bll.ProcessarFlagInsucesso(Convert.ToDecimal(txtNumLoteAba2.Text));

                DivSucessoAba2.Visible = true;
                divPrincipalAba2.Visible = false;
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, ex.Message);
            }

            
        }

        protected void btnAviso_Click(object sender, EventArgs e)
        {

            try
            {
                bll.ProcessarAvisoDeCancelamento(Convert.ToDateTime(txtDtVencAba4.Text), Convert.ToDecimal(txtNumLoteAba4.Text));

                DivSucessoAba5.Visible = true;
                divPrincipalAba5.Visible = false;
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, ex.Message);
            }

           
        }

        protected void btnGerarTxt_Click(object sender, EventArgs e)
        {
            try
            {
                string nomeArquivo  ="";

                string host = "";
                string usuario = "";
                string senha = "";

                List<string> listaTemp = new List<string>();
                List<string> listaArq = new List<string>();

                Stream ms;

                bll.ProcessarArquivosTxts(Convert.ToDecimal(ddlTiposTxt.SelectedValue), Convert.ToDecimal(txtNumLoteAba6.Text), out nomeArquivo);

                VerificarAmbienteFTP(out host, out usuario, out senha);

                ms = DownloadArquivoFTP(host, usuario, senha, nomeArquivo);

                GeraSaidaArquivo(ms, nomeArquivo);

                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Arquivo gerado com Sucesso");

              

            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, ex.Message);
            }

         

        }

        //protected void btnPesquisarTxt_Click(object sender, EventArgs e)
        //{
        //    DivArquivos.Visible = true;
        //    try
        //    {
        //        string host = "";
        //        string usuario = "";
        //        string senha = "";
        //        List<string> listaTemp = new List<string>();
        //        List<string> listaArq = new List<string>();


        //        VerificarAmbienteFTP(out host, out usuario, out senha);

        //        ListarArquivosFTP(1, host, usuario, senha, ddlTipoPdf.SelectedValue, out listaTemp);

        //        VerificarTipoArquivoTXT(listaTemp, ddlTiposTxt2.SelectedValue, out listaArq);

        //        gridViewTxt.DataSource = listaArq;
        //        gridViewTxt.DataBind();

        //        DivArquivos.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MostraMensagemTelaUpdatePanel(upUpdatePanel, ex.Message);
        //    }


            
        //}

        protected void btnCobEJ_Click(object sender, EventArgs e)
        {
            DivSucessoAba4.Visible = false;

            try
            {
                decimal contador = 0;

                bll.ProcessarExtraJudicial(txtCodEmpresaAbaEJ.Text, txtNumMatriculaAbaEJ.Text, Convert.ToDecimal(txtNumRepres.Text), Convert.ToDecimal(txtNumLoteAbaEJ.Text), Convert.ToDateTime(txtDtVencAbaEJ.Text), Convert.ToDateTime(txtDtVencAntAbaEJ.Text),out contador);

                

                if (contador > 0)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro inserido com sucesso!");

                    txtCodEmpresaAbaEJ.Text = "";
                    txtNumMatriculaAbaEJ.Text = "";
                    txtNumLoteAbaEJ.Text = "";
                    txtDtVencAbaEJ.Text = "";
                    txtDtVencAntAbaEJ.Text = "";

                    DivSucessoAba4.Visible = true;
                }
                else 
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Participante não foi encontrado na tabela de inadimplentes");
                    DivSucessoAba4.Visible = false;
                }

                
               
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, ex.Message);
            }

           
        }

        protected void BtnDownloadAba8_Click(object sender, EventArgs e)
        {
            string host = "sistema.mcm.srv.br";
            string usuario = "cesp";
            string senha = "F2017ce14S07";

            List<string> listaArq = new List<string>();

            string caminho = "";

            ListarArquivosFTP(txtNumLoteAba8.Text, host, usuario, senha, ddlTipoPdf.SelectedValue, out listaArq, out caminho);

            gridViewMcm.DataSource = listaArq;
            gridViewMcm.DataBind();

            DivPesquisaAba8.Visible = true;
        }

        #endregion

        #region .:GridViews:.
        //protected void gridViewTxt_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Download")
        //    {
        //        string host = "";
        //        string usuario = "";
        //        string senha = "";

        //        Stream ms;

        //        GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //        int RowIndex = gvr.RowIndex;

        //        List<string> listaTemp = new List<string>();
        //        List<string> listaArq = new List<string>();

        //        VerificarAmbienteFTP(out host, out usuario, out senha);

        //        ListarArquivosFTP(2, host, usuario, senha, ddlTipoPdf.SelectedValue, out listaTemp);

        //        ms = DownloadArquivoFTP(host, usuario, senha, gridViewTxt.Rows[RowIndex].Cells[1].Text);

        //        GeraSaidaArquivo(ms, gridViewTxt.Rows[RowIndex].Cells[1].Text);


        //    }
        //}

        protected void gridViewMcm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {

                string host = "sistema.mcm.srv.br";
                string usuario = "cesp";
                string senha = "F2017ce14S07";

                string caminho = "";

                Stream ms;

                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;


                List<string> listaArq = new List<string>();

                ListarArquivosFTP(txtNumLoteAba8.Text, host, usuario, senha, ddlTipoPdf.SelectedValue, out listaArq, out caminho);

                ms = DownloadArquivoFTP(caminho, usuario, senha, gridViewMcm.Rows[RowIndex].Cells[1].Text);

                GeraSaidaArquivo(ms, gridViewMcm.Rows[RowIndex].Cells[1].Text);
            }


        }

        #endregion

        #endregion


        #region .:Métodos:.

        protected void ListarArquivosFTP(string numLoteMcm, string host, string usuario, string senha, string tipoArquivo, out List<string> listaArq, out string caminho)
        {
            listaArq = new List<string>();
            caminho = "";
            try
            {

         
                if (tipoArquivo == "1")
                {
                    caminho = "ftp://" + host + "/2018/COBSAUD/COBSAUDC_BOLETO/" + numLoteMcm + "/";
                }
                else if (tipoArquivo == "2")
                {
                    caminho = "ftp://" + host + "/2018/COBSAUD/COBSAUDAR_BOLETO/" + numLoteMcm + "/";
                }
                else if (tipoArquivo == "3")
                {
                    caminho = "ftp://" + host + "/2018/COBSAUD/CIDSAUDATV_CARTA_E_BOLETO/" + numLoteMcm + "/"; ;
                }
                else if (tipoArquivo == "4")
                {
                    caminho = "ftp://" + host + "/2018/COBSAUD/CAISAUD_CARTA_E_BOLETO/" + numLoteMcm + "/"; ;
                }
                else if (tipoArquivo == "5")
                {
                    caminho = "ftp://" + host + "/2018/COBSAUD/CACSAUD_CARTA_E_BOLETO/" + numLoteMcm + "/"; ;
                }


              

                FtpWebRequest requisicaoFTP;

                // Cria um objeto FtpWebRequest a partir do endereço fornecido

                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(caminho);

                // Fornece as credenciais de WebPermission
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);

                // Especifica o comando a ser executado
                requisicaoFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                // Especifica o tipo de dados a ser transferido
                requisicaoFTP.UseBinary = true;

                using (FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        listaArq = reader.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();

                    }
                }

            }
            catch(WebException)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, " O Lote digitado não foi encontrado");
            }

        }

        public DataTable ConverterListParaDataTable<T>(IList<T> lista)
        {
            PropertyDescriptorCollection propriedades = TypeDescriptor.GetProperties(typeof(T));

            DataTable dt = new DataTable();

            foreach (PropertyDescriptor prop in propriedades)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in lista)
            {
                DataRow linha = dt.NewRow();
                foreach (PropertyDescriptor prop in propriedades)
                {
                    linha[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                }

                dt.Rows.Add(linha);
            }

            return dt;
        }

        protected void VerificarTipoArquivoTXT(List<string> lista, string tipoTXT, out List<string> listatemp)
        {
            string data = "";

            listatemp = new List<string>();

            foreach (string linha in lista)
            {
                switch (tipoTXT)
                {   //Boletos de Cobrança
                    case "1":

                        if (linha.Contains("COBSAUDC") || linha.Contains("COBSAUDT"))
                        {

                            listatemp.Add(linha);
                        }

                        break;

                    //Boletos Judiciais
                    case "2":

                        if (linha.Contains("COBSAUDAR"))
                        {
                            listatemp.Add(linha);
                        }
                        break;

                    //Cartas de Aviso de Cancelamento
                    case "3":

                        if (linha.Contains("CACSAUDC") || linha.Contains("CACSAUDT"))
                        {
                            listatemp.Add(linha);
                        }
                        break;

                    //Cartas de Aviso de Inadimplência
                    case "4":

                        if (linha.Contains("CAISAUDC") || linha.Contains("CAISAUDT"))
                        {
                            listatemp.Add(linha);
                        }
                        break;

                    //Cartas de Aviso de Inadimplência - Digna
                    case "5":

                        if (linha.Contains("CIDSAUDATV") || linha.Contains("CIDSAUDT"))
                        {
                            listatemp.Add(linha);
                        }
                        break;

                    default: throw new Exception
                            ("Txt não encontrado");

                }
            }
        }

        protected void VerificarAmbienteFTP(out string host, out string username, out string password)
        {


            //verifica o ambiente para conexão do ftp
            if (ConfigurationManager.AppSettings["Config"] == "P")
            {
                host = "ftp://" + "fcespracp004/";
                username = "integ";
                password = "modelo00";
            }
            else if (ConfigurationManager.AppSettings["Config"] == "T")
            {
                host = "ftp://" + "fcesporah001/";
                username = "integ";
                password = "newtst";
            }
            else
            {
                host = "ftp://" + "fcesporad001/";
                username = "integ";
                password = "newdev";

                
            }


        }

        protected Stream DownloadArquivoFTP(string host, string usuario, string senha, string nomeArq)
        {
            MemoryStream ms = new MemoryStream();

            FileInfo _arquivoInfo = new FileInfo(nomeArq);
            string uri = host + _arquivoInfo.Name;
            FtpWebRequest requisicaoFTP;

            try
            {
                // Cria um objeto FtpWebRequest a partir da Uri fornecida
                requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(uri);

                // Fornece as credenciais de WebPermission
                requisicaoFTP.Credentials = new NetworkCredential(usuario, senha);

                // Por padrão KeepAlive é true, 
                requisicaoFTP.KeepAlive = false;

                // Especifica o comando a ser executado
                requisicaoFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                // Especifica o tipo de dados a ser transferido
                requisicaoFTP.UseBinary = true;

                FtpWebResponse response = (FtpWebResponse)requisicaoFTP.GetResponse();


                Stream responseStream = response.GetResponseStream();



                response.GetResponseStream().CopyTo(ms);

                ms.Seek(0, System.IO.SeekOrigin.Begin);

                response.Dispose();
                response.Close();


            }
            catch (OutOfMemoryException e)
            {
                // MostraMensagemTelaUpdatePanel(upUpdatePanel, "Verificar se o download foi realizado, desbloquear pop-up caso não tenha sido iniciado");
            }
            catch (Exception ex)
            {
                  MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro: " + ex.Message);
            }


            return ms;
        }

        public void GeraSaidaArquivo(Stream arquivo, string extensao_anexo)
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

        #endregion

    }
}