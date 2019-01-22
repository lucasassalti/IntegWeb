using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Financeira.Aplicacao.BLL.Tesouraria;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web
{
    public partial class AtualizaDadosBancariosLote : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaConsultaHist();
            }
        }

        #region .: ABA 1 :.

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdHistAtualizacao.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtEmpresa.Text = "";
            txtMatricula.Text = "";
            txtNome.Text = "";
            txtBanco.Text = "";
            txtAgencia.Text = "";
            txtTipConta.Text = "";
            txtNumConta.Text = "";
            txtCpf.Text = "";
            txtRepresentante.Text = "";
            txtProcessamentoIni.Text = "";
            txtProcessamentoFim.Text = "";
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            AtualizaCcLoteBLL bll = new AtualizaCcLoteBLL();

            DataTable dt = bll.ListarDadosParaExcel(Util.String2Short(txtEmpresa.Text), Util.String2Date(txtProcessamentoIni.Text),
                                                    Util.String2Date(txtProcessamentoFim.Text),
                                                    Util.String2Int32(txtMatricula.Text),
                                                    Util.String2Int32(txtRepresentante.Text),
                                                    txtNome.Text,
                                                    Util.String2Int64(txtCpf.Text),
                                                    Util.String2Short(txtBanco.Text),
                                                    Util.String2Int32(txtAgencia.Text),
                                                    txtTipConta.Text,
                                                    txtNumConta.Text);

            //Download do Excel 
            ArquivoDownload adXlsHistConta = new ArquivoDownload();
            adXlsHistConta.nome_arquivo = "Arquivo_Exportado.xls";
            //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
            adXlsHistConta.dados = dt;
            Session[ValidaCaracteres(adXlsHistConta.nome_arquivo)] = adXlsHistConta;
            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsHistConta.nome_arquivo);
            //AdicionarAcesso(fUrl);
            AbrirNovaAba(upUpdatePanel, fUrl, adXlsHistConta.nome_arquivo);

        }

        #endregion

        #region .: ABA 2 :.

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            AtualizaCcLoteBLL bll = new AtualizaCcLoteBLL();
            var user = (ConectaAD)Session["objUser"];

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
                        DataTable dtExcelLote = ReadExcelFile(Excel);


                        foreach (DataRow dr in dtExcelLote.Rows)
                        {
                            if (String.IsNullOrEmpty(dr[0].ToString()) && String.IsNullOrEmpty(dr[1].ToString()))
                            {
                                dr.Delete();
                            }
                        }
                        dtExcelLote.AcceptChanges();

                        dtExcelLote.Columns.Add("Critica");
                        dtExcelLote.Columns.Add("NomeArq");

                        foreach (DataRow drExcelLinha in dtExcelLote.Rows)
                        {
                            if (!drExcelLinha.Table.Columns.Contains("Empresa"))
                            {
                                throw new Exception("A Coluna Empresa não existe, Favor verificar");
                            }
                            else if (!drExcelLinha.Table.Columns.Contains("Matricula"))
                            {
                                throw new Exception("A Coluna Matricula não existe, Favor verificar");
                            }
                            else if (!drExcelLinha.Table.Columns.Contains("Representante"))
                            {
                                throw new Exception("A Coluna Representante não existe, Favor verificar");
                            }
                            else if (!drExcelLinha.Table.Columns.Contains("Nome do Empregado"))
                            {
                                throw new Exception("A Coluna Nome do Empregado não existe, Favor verificar");
                            }
                            else if (!drExcelLinha.Table.Columns.Contains("CPF"))
                            {
                                throw new Exception("A Coluna CPF não existe, Favor verificar");
                            }
                            else if (!drExcelLinha.Table.Columns.Contains("Código do Banco"))
                            {
                                throw new Exception("A Coluna Código do Banco não existe, Favor verificar");
                            }
                            else if (!drExcelLinha.Table.Columns.Contains("Código Agencia"))
                            {
                                throw new Exception("A Coluna Código Agencia não existe, Favor verificar");
                            }
                            else if (!drExcelLinha.Table.Columns.Contains("Tipo de conta corrente"))
                            {
                                throw new Exception("A Coluna Tipo de conta corrente não existe, Favor verificar");
                            }
                            else if (!drExcelLinha.Table.Columns.Contains("Número da conta"))
                            {
                                throw new Exception("A Coluna Número da conta não existe, Favor verificar");
                            }

                        }


                        DataSet ds = bll.Validar(dtExcelLote);

                        DataTable dtInserirHist = ds.Tables[0];

                        DataTable dtInserir = ds.Tables[1];


                        for (int i = 0; i < dtInserir.Rows.Count; i++)
                        {
                            FUN_TBL_ATU_CC obj = new FUN_TBL_ATU_CC();


                            // Inserção tabela Temporaria
                            obj.COD_EMPRS = Convert.ToInt16(dtInserir.Rows[i]["Empresa"].ToString());
                            obj.NUM_RGTRO_EMPRG = Convert.ToInt32(dtInserir.Rows[i]["Matricula"].ToString());
                            obj.NUM_IDNTF_RPTANT = Util.String2Int32(dtInserir.Rows[i]["Representante"].ToString());
                            obj.NUM_CPF_EMPRG = Util.String2Int64(Util.LimparCPF(dtInserir.Rows[i]["CPF"].ToString()));
                            obj.NOME = Util.DataRow2String(dtInserir.Rows[i], "Nome do Empregado").ToUpper();
                            obj.COD_BANCO = Convert.ToInt16(dtInserir.Rows[i]["Código do Banco"].ToString());
                            obj.COD_AGBCO = Util.String2Int32(dtInserir.Rows[i]["Código Agencia"].ToString());
                            obj.TIP_CTCOR_EMPRG = dtInserir.Rows[i]["Tipo de conta corrente"].ToString().Trim();
                            obj.NUM_CTCOR_EMPRG = dtInserir.Rows[i]["Número da conta"].ToString();
                            obj.NOME_ARQ = name[0].ToString();

                            bll.Inserir(obj);


                        }

                        for (int i = 0; i < dtInserirHist.Rows.Count; i++)
                        {
                            FUN_TBL_ATU_CC_HIST objHist = new FUN_TBL_ATU_CC_HIST();
                            //Inserção tabela historico 
                            objHist.COD_EMPRS = Convert.ToInt16(dtInserirHist.Rows[i]["Empresa"].ToString() == "" ? "0" : dtInserirHist.Rows[i]["Empresa"].ToString());
                            objHist.NUM_RGTRO_EMPRG = Convert.ToInt32(dtInserirHist.Rows[i]["Matricula"].ToString() == "" ? "0" : dtInserirHist.Rows[i]["Matricula"].ToString());
                            objHist.NUM_IDNTF_RPTANT = Util.String2Int32(dtInserirHist.Rows[i]["Representante"].ToString());
                            objHist.NOME = Util.DataRow2String(dtInserirHist.Rows[i], "Nome do Empregado").ToUpper();
                            objHist.NUM_CPF_EMPRG = Util.String2Int64(Util.LimparCPF(dtInserirHist.Rows[i]["CPF"].ToString()));
                            objHist.COD_BANCO = Util.String2Short(dtInserirHist.Rows[i]["Código do Banco"].ToString());
                            objHist.COD_AGBCO = Util.String2Int32(dtInserirHist.Rows[i]["Código Agencia"].ToString());
                            objHist.TIP_CTCOR_EMPRG = Util.DataRow2String(dtInserirHist.Rows[i], "Tipo de conta corrente").Trim();
                            objHist.NUM_CTCOR_EMPRG = Util.DataRow2String(dtInserirHist.Rows[i], "Número da conta").ToUpper();
                            objHist.CRITICA = Util.DataRow2String(dtInserirHist.Rows[i], "Critica");
                            objHist.USU_GERACAO = user.login;
                            objHist.NOME_ARQ = name[0].ToString();

                            bll.InserirHist(objHist);
                        }


                        int ii = dtInserir.Rows.Count;
                        int it = dtInserirHist.Rows.Count;
                        int total = ii + it;

                        Resultado res = new Resultado();
                        Resultado resComp = new Resultado();

                        if (total == dtExcelLote.Rows.Count)
                        {

                            res = bll.AtualizaCcLote();
                            resComp = bll.AtualizaCcComplementar();

                            if (res.Ok && resComp.Ok)
                            {
                                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Processado com Sucesso.");

                                ddlAtuContas.Items.Clear();
                                CarregaConsultaHist();

                                string[] arq = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');

                                AtualizaCcLoteBLL Obll = new AtualizaCcLoteBLL();

                                if (ddlAtuContas.Items.Contains(new ListItem(arq[0].ToString())))
                                {
                                    ddlAtuContas.SelectedValue = arq[0].ToString();
                                    lblTotalLinhas.Text = Obll.GetDataCountHist(ddlAtuContas.SelectedValue, Convert.ToBoolean(chkPesqComCritica.Checked)).ToString();
                                    lblQtdErro.Text = Obll.GetCriticasHist(ddlAtuContas.SelectedValue, Convert.ToBoolean(chkPesqComCritica.Checked)).ToString();
                                }
                            }
                            else
                            {
                                if (!res.Ok)
                                {
                                    MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
                                }
                                if (!resComp.Ok)
                                {
                                    MostraMensagemTelaUpdatePanel(upUpdatePanel, resComp.Mensagem);
                                }

                            }
                        }

                        else
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao Atualizar , Favor entrar em Contato com o Administrador do Sistema");
                        }


                        bll.Delete();
                        grdHistAtualizacao.DataBind();
                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, ex.Message);
                    }
                }

                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção carrega arquivos xls ou xlsx");
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção carrega a planilha para continuar");
            }

        
 
           

        }

        #endregion

        protected void ddlAtuContas_TextChanged(object sender, EventArgs e)
        {
            grdConsultaHistorico.PageIndex = 0;
            grdConsultaHistorico.EditIndex = -1;
            lblTotalLinhas.Text = "--";
            lblQtdErro.Text = "--";

            if(ddlAtuContas.SelectedValue != "0" && !String.IsNullOrEmpty(ddlAtuContas.SelectedValue))
            {
                AtualizaCcLoteBLL bll = new AtualizaCcLoteBLL();
                lblTotalLinhas.Text = bll.GetDataCountHist(ddlAtuContas.SelectedValue, Convert.ToBoolean(chkPesqComCritica.Checked)).ToString();
                lblQtdErro.Text = bll.GetCriticasHist(ddlAtuContas.SelectedValue, Convert.ToBoolean(chkPesqComCritica.Checked)).ToString();

            }
        }

        #region Métodos

        public void CarregaConsultaHist()
        {
            AtualizaCcLoteBLL objBLL = new AtualizaCcLoteBLL();

                DataTable dt = objBLL.GetListaDataDT_ATU_CONTAS();

                foreach (DataRow linha in dt.Rows)
                {
                    ddlAtuContas.Items.Add(linha[0].ToString());
                }

                ddlAtuContas.Items.Insert(0, new ListItem("TODOS", "TODOS"));

                lblTotalLinhas.Text = objBLL.GetDataCountHist(ddlAtuContas.SelectedValue, Convert.ToBoolean(chkPesqComCritica.Checked)).ToString();
                lblQtdErro.Text = objBLL.GetCriticasHist(ddlAtuContas.SelectedValue, Convert.ToBoolean(chkPesqComCritica.Checked)).ToString();

                grdConsultaHistorico.DataBind();
             grdHistAtualizacao.Sort("DAT_PROCESSAMENTO",SortDirection.Ascending);

            }

           
        }
     #endregion

    }