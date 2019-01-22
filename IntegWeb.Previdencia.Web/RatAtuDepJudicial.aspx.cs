using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Previdencia.Aplicacao.BLL;
using System.IO;
using System.Data;
using IntegWeb.Entidades.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Framework;
using IntegWeb.Entidades;
//using SpreadsheetLight;

namespace IntegWeb.Previdencia.Web
{
    public partial class RatAtuDepJudicial : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region Eventos

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];
            if (FileUploadControl.HasFile)
            {

                if (FileUploadControl.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))//xlsx
                {
                    string nomeArquivo = "";

                    RatAtuDepJudicialBLL objBLL = new RatAtuDepJudicialBLL();

                    try
                    {
                        //string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');

                        string name = Path.GetFileName(FileUploadControl.FileName).ToString();
                        string caminhoArquivo = Server.MapPath("UploadFile\\");

                       //nomeArquivo = caminhoArquivo + name[0] + "." + name[1] + "." + name[2];

                        nomeArquivo = caminhoArquivo + name;

                        if (!Directory.Exists(caminhoArquivo))
                        {
                            Directory.CreateDirectory(caminhoArquivo);
                        }

                        FileUploadControl.SaveAs(nomeArquivo);


                                          

                        //Dataset para guardar todas as abas
                        DataSet ds = new DataSet();
                        ds = ReadExcelFileWork(nomeArquivo);

                        //Adiciona cada aba no Dataset
                        for (int i = 1; i <= ds.Tables.Count; i++)
                        {
                             
                            //Insere cada linha na tabela para calculo
                             foreach (DataRow dtLinha in ds.Tables[i-1].Rows)
                             {

                                 PRE_TBL_JUR_DEP_JUDICIAL obj = new PRE_TBL_JUR_DEP_JUDICIAL();

                                 obj.NRO_PASTA = dtLinha["Pasta"].ToString() == "" ? "" : dtLinha["Pasta"].ToString();
                                 obj.COND_CLIENTE = dtLinha["Condição do Cliente"].ToString() == "" ? "" : dtLinha["Condição do Cliente"].ToString();
                                 obj.DIV_CUSTO = dtLinha["Divisão de Custo"].ToString() == "" ? "" : dtLinha["Divisão de Custo"].ToString();
                                 obj.CATEGORIA = dtLinha["Categoria"].ToString() == "" ? "" : dtLinha["Categoria"].ToString();
                                 obj.ASSUNTO = dtLinha["Assunto"].ToString() == "" ? "" : dtLinha["Assunto"].ToString();
                                 obj.NOM_EMPRG = dtLinha["Participantes"].ToString() == "" ? "" : dtLinha["Participantes"].ToString();
                                 obj.NOM_ABRVO_EMPRS = dtLinha["Empresas"].ToString() == "" ? "" : dtLinha["Empresas"].ToString();
                                 obj.TIPO_DEP_JUD = dtLinha["Tipo"].ToString() == "" ? "" : dtLinha["Tipo"].ToString();
                                 obj.DAT_PAGAMENTO = Convert.ToDateTime(dtLinha["Data"].ToString() == "" ? null : dtLinha["Data"].ToString());
                                 obj.VLR_ORI = Convert.ToDecimal(dtLinha["Valor Original"].ToString() == "" ? "0" : dtLinha["Valor Original"].ToString());
                                 obj.VLR_ATU = Convert.ToDecimal(dtLinha["Valor Atual"].ToString() == "" ? "0" : dtLinha["Valor Atual"].ToString());
                                 obj.VLR_ANT = Convert.ToDecimal(dtLinha["Valor Anterior"].ToString() == "" ? "0" : dtLinha["Valor Anterior"].ToString()) ;
                                 obj.VLR_ATUALIZADO = Convert.ToDecimal(dtLinha["Atualização"].ToString() == "" ? "0" : dtLinha["Atualização"].ToString());
                                 obj.PLANO = null;
                                 obj.VLR_BSPS = null;
                                 obj.VLR_BD = null;
                                 obj.VLR_CV = null;
                                 obj.MOEDA = dtLinha["Moeda"].ToString() == "" ? "" : dtLinha["Moeda"].ToString();
                                 obj.VLR_MOEDA = Convert.ToDecimal(dtLinha["Valor da Moeda"].ToString() == "" ? "0" : dtLinha["Valor da Moeda"].ToString());
                                 obj.COD_VARA_PROC = dtLinha["Juízo"].ToString() == "" ? "" : dtLinha["Juízo"].ToString();
                                 obj.NRO_PROCESSO = dtLinha["Número do Processo"].ToString() == "" ? "" : dtLinha["Número do Processo"].ToString();
                                 obj.USU_GERACAO = user.login;
                                 obj.DAT_GERACAO = DateTime.Now.Date;

                                 Resultado res = objBLL.Inserir(obj); 
                                
                             }


                        }

                        Resultado resProc = objBLL.GeraRateio(DateTime.Now);



                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, ex.Message);
                    }
                    finally
                    {
                       
                        FileUploadControl.PostedFile.InputStream.Flush();
                        FileUploadControl.PostedFile.InputStream.Close();
                        FileUploadControl.FileContent.Dispose();
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Arquivo Processado com Sucesso");
                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção, Favor utilizar arquivos com a extensão xlsx(Excel atual)");

                }
            }
            
        }


        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            RatAtuDepJudicialBLL objBll = new RatAtuDepJudicialBLL();
            System.Data.DataTable dt = objBll.ExportaRelatorio(Convert.ToDateTime(txtDataGeracao.Text));

            GeraSaidaExcel(dt);

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtDataGeracao.Text = "";
        }

 

        #endregion


        #region Métodos

        protected void GeraSaidaExcel(System.Data.DataTable dt)
        {
                        
            ArquivoDownload arqSaida = new ArquivoDownload();
            arqSaida.nome_arquivo = "Depositos_Judiciais-Trabalhistas_Previdenciario_SAIDA.xlsx";
            arqSaida.dados = dt;
            Session[ValidaCaracteres(arqSaida.nome_arquivo)] = arqSaida;
            string caminho = "WebFile.aspx?dwFile=" + ValidaCaracteres(arqSaida.nome_arquivo);
            AdicionarAcesso(caminho);
            AbrirNovaAba(UpdatePanel, caminho, arqSaida.nome_arquivo);


        }

      

        #endregion

    



    }
}