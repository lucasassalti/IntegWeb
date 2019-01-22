using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Financeira.Aplicacao.BLL;
using IntegWeb.Financeira.Aplicacao.DAL;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System.Collections.Specialized;
using IntegWeb.Entidades;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using IntegWeb.Entidades.Framework;

namespace IntegWeb.Financeira.Web
{
    public partial class EmprestimoDesconto : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagemInicial.Visible = false;

            if (!IsPostBack)
            {
                grdEnvio.Sort("COD_EMPRESTIMO_DESCONTO", SortDirection.Descending);
                ddlAnoRef.SelectedValue = DateTime.Now.Year.ToString();
                ddlMesRef.SelectedValue = DateTime.Now.Month.ToString();
                //CarregaDDLs();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtMesref.Text) &&
                String.IsNullOrWhiteSpace(txtAnoref.Text) &&
                pnlFiltro.Visible == false)
            {
                MostraMensagem(lblMensagemInicial, "Atenção! Preencha pelo menos um campo para pesquisa.");

            }
            else if (String.IsNullOrWhiteSpace(txtPesqCodEmprs.Text) &&
                     String.IsNullOrWhiteSpace(txtPesqMatricula.Text) &&
                     String.IsNullOrWhiteSpace(txtPesqRepres.Text) &&
                     String.IsNullOrWhiteSpace(txtPesqCpf.Text) &&
                     String.IsNullOrWhiteSpace(txtPesqNome.Text) &&
                     pnlFiltro.Visible == true)
            {
                MostraMensagem(lblMensagemInicial, "Atenção! Preencha pelo menos um campo para pesquisa.");

            }
            else

            {
                grdEnvio.EditIndex = -1;
                grdEnvio.PageIndex = 0;
                grdEnvio.DataBind();
            }
        }

        //private bool ValidaPesquisa()
        //{
        //    if (String.IsNullOrWhiteSpace(txtMesref.Text))
        //    {
        //        MostraMensagem(lblMensagemInicial, "Campo Mês é obrigatório");
        //        return false;
        //    }

        //    if (String.IsNullOrWhiteSpace(txtAnoref.Text))
        //    {
        //        MostraMensagem(lblMensagemInicial, "Campo Ano é obrigatório");
        //        return false;
        //    }

        //    return true;
        //}

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtAnoref.Text = "";
            txtMesref.Text = "";
            txtPesqCodEmprs.Text = "";
            txtPesqMatricula.Text = "";
            txtPesqRepres.Text = "";
            txtPesqCpf.Text = "";
            txtPesqNome.Text = "";
            ddlStatus.SelectedValue = "";
            pnlFiltro.Visible = false;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            pnlFiltro.Visible = true;
        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            EmprestimoDescontoBLL bll = new EmprestimoDescontoBLL();
            Resultado res = new Resultado();
            var user = (ConectaAD)Session["objUser"];

            if (ddlMesRef.SelectedValue == "0" ||
                ddlAnoRef.SelectedValue == "0")
            {
                MostraMensagem(lblMensagemInicial, "Atenção! Preencha o campo Mês/Ano para processar.");
            }
            else if (String.IsNullOrWhiteSpace(txtDtComplementados.Text))
            {
                MostraMensagem(lblMensagemInicial, "Atenção! Preencha o campo Dt. Pgto. Complementados para processar.");
            }
            else if (String.IsNullOrWhiteSpace(txtDtSuplementados.Text))
            {
                MostraMensagem(lblMensagemInicial, "Atenção! Preencha o campo Dt. Pgto. Suplementados para processar.");
            } 
            else
            {
                DateTime DtComplementados;
                DateTime.TryParse(txtDtComplementados.Text, out DtComplementados);

                DateTime DtSuplementados;
                DateTime.TryParse(txtDtSuplementados.Text, out DtSuplementados);

                res = bll.Processar(ddlMesRef.SelectedValue, ddlAnoRef.SelectedValue, DtComplementados, DtSuplementados);
                MostraMensagem(lblMensagemInicial, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");

                grdEnvio.DataBind();
            }
        }

        protected void btnSubmit_Gerar_Click(object sender, EventArgs e)
        {
            EmprestimoDescontoBLL bll = new EmprestimoDescontoBLL();
            Resultado res = new Resultado();

            //Download do TXT
            ArquivoDownload adTxtEntidade = new ArquivoDownload();
            adTxtEntidade.nome_arquivo = String.Format("entidade_externa_{0}.txt", ddlMesRef.SelectedValue.PadLeft(2,'0') + ddlAnoRef.SelectedValue);
            adTxtEntidade.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtEntidade.nome_arquivo;
            adTxtEntidade.dados = null;
            Session[ValidaCaracteres(adTxtEntidade.nome_arquivo)] = adTxtEntidade;            

            res = bll.GerarTxt(ddlMesRef.SelectedValue, ddlAnoRef.SelectedValue, adTxtEntidade.caminho_arquivo);
            if (res.Ok)
            {

                string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adTxtEntidade.nome_arquivo);
                AbrirNovaAba(upUpdatePanel, fUrl, adTxtEntidade.nome_arquivo);

                //Download do Excel 
                ArquivoDownload adRelEmprestimo = new ArquivoDownload();
                adRelEmprestimo.nome_arquivo = String.Format("Rel_Margem_Emprestimo_{0}.xls", ddlMesRef.SelectedValue.PadLeft(2, '0') + ddlAnoRef.SelectedValue);
                adRelEmprestimo.caminho_arquivo = null;
                adRelEmprestimo.dados = bll.GerarDataTable(ddlMesRef.SelectedValue, ddlAnoRef.SelectedValue, 4);
                Session[ValidaCaracteres(adRelEmprestimo.nome_arquivo)] = adRelEmprestimo;
                fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelEmprestimo.nome_arquivo);

                AbrirNovaAba(upUpdatePanel, fUrl, adRelEmprestimo.nome_arquivo);
                MostraMensagem(lblMensagemInicial, res.Mensagem, "n_ok");
            }
            else
            {
                MostraMensagem(lblMensagemInicial, res.Mensagem, (res.Alerta ? "n_warning" : "n_error"));
            }            
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                if (!String.IsNullOrWhiteSpace(txtMesref_upload.Text) && !String.IsNullOrWhiteSpace(txtAnoref_upload.Text))
                {
                    if (FileUploadControl.PostedFile.ContentType.Equals("application/vnd.ms-excel") || // xls
                        FileUploadControl.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))//xlsx
                    {
                        string path = "";
                        try
                        {

                            string filename = Path.GetFileName(FileUploadControl.FileName).ToString();
                            string[] name = filename.Split('.');
                            string UploadFilePath = Server.MapPath("UploadFile\\");

                            path = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                            if (!Directory.Exists(UploadFilePath))
                            {
                                Directory.CreateDirectory(UploadFilePath);
                            }

                            FileUploadControl.SaveAs(path);
                            DataTable dt = ReadExcelFile(path, 1);

                            EmprestimoDescontoBLL bll = new EmprestimoDescontoBLL();
                            Resultado res = new Resultado();
                            var user = (ConectaAD)Session["objUser"];
                            //AAT_TBL_RET_DEB_CONTA ja_existe = bll.GetData(filename, "A");

                            //if (ja_existe == null)
                            //{
                            res = bll.DePara(dt, txtMesref_upload.Text, txtAnoref_upload.Text, (user != null) ? user.login : "Desenv");
                            //}
                            //else
                            //{
                            //    lkYes.CommandArgument = filename;
                            //    lkYes.Visible = true;
                            //    MostraMensagem(TbUpload_Mensagem, "Este arquivo já foi importado anteriormente. Tem certeza que deseja importa-lo novamente?");
                            //}

                            //if (res.Ok)
                            //{
                                //res = ConsolidaListaDebitoConta(filename);
                                MostraMensagem(TbUpload_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
                            //}

                        }
                        catch (Exception ex)
                        {
                            MostraMensagem(TbUpload_Mensagem, "Atenção! O arquivo não pôde ser carregado. Motivo:\\n" + ex.Message, "n_error");
                        }
                        finally
                        {
                            FileUploadControl.FileContent.Dispose();
                            FileUploadControl.FileContent.Flush();
                            FileUploadControl.PostedFile.InputStream.Flush();
                            FileUploadControl.PostedFile.InputStream.Close();
                            File.Delete(path);
                        }

                    }
                    else
                    {
                        MostraMensagem(TbUpload_Mensagem, "Atenção! Carregue apenas arquivos de retorno do banco");
                    }
                }
                else
                {
                    MostraMensagem(TbUpload_Mensagem, "Atenção! O campo Mês/Ano de ref. é obrigatório");
                }

            }
            else
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Selecione um Arquivo para continuar");
            }
        
        }

        protected void grdEnvio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int iCOD_EMPRESTIMO_DESCONTO;

            if (e.CommandName != "Sort")
            {
                iCOD_EMPRESTIMO_DESCONTO = Util.String2Int32(e.CommandArgument.ToString()) ?? 0;
                var user = (ConectaAD)Session["objUser"];

                EmprestimoDescontoBLL bll = new EmprestimoDescontoBLL();

                switch (e.CommandName)
                {
                    
                    case "Gravar":

                        Resultado res = new Resultado();

                        string VLR_DIVIDA = ((TextBox)grdEnvio.Rows[grdEnvio.EditIndex].FindControl("txtVlrSaldo")).Text;
                        string VLR_CARGA = ((TextBox)grdEnvio.Rows[grdEnvio.EditIndex].FindControl("txtVlrDivida")).Text;
                        //string VLR_ACERTO = ((TextBox)grdReembolsoAjustes.Rows[grdReembolsoAjustes.EditIndex].FindControl("txtVlrAcerto")).Text;

                        //if (ValidarLancamento(PK, COD_EMPRS, NUM_RGTRO_EMPRG, VLR_LANCAMENTO, COD_VERBA))
                        //{

                            decimal dVLR_DIVIDA;
                            decimal.TryParse(VLR_DIVIDA, out dVLR_DIVIDA);

                            decimal dVLR_CARGA;
                            decimal.TryParse(VLR_CARGA, out dVLR_CARGA);

                            //decimal dVLR_ACERTO;
                            //decimal.TryParse(VLR_ACERTO, out dVLR_ACERTO);                            
                            AAT_TBL_EMPRESTIMO_DESCONTO updLancamentoDesc =
                                bll.GetLancamentoDesconto(iCOD_EMPRESTIMO_DESCONTO);
                            updLancamentoDesc.VLR_DIVIDA = dVLR_DIVIDA;
                            //updLancamentoDesc.VLR_DIVIDA_POSS = dVLR_DIVIDA_POSS;
                            updLancamentoDesc.VLR_CARGA = dVLR_CARGA;

                            //res = Demons.SaveData(updLancamento, dVLR_ACERTO);
                            res = bll.SaveData(updLancamentoDesc);

                            if (res.Ok)
                            {
                                grdEnvio.EditIndex = -1;
                                grdEnvio.DataBind();
                            }
                            else
                            {
                                MostraMensagem(TbUpload_Mensagem, res.Mensagem);
                            }
                        //}

                        break;

                    case "Excluir":

                        res = bll.DeleteData(iCOD_EMPRESTIMO_DESCONTO);
                        if (res.Ok)
                        {
                            //pnlNovoLancamento.Visible = false;
                            grdEnvio.DataBind();
                        }
                        else
                        {
                            MostraMensagem(TbUpload_Mensagem, res.Mensagem);
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        protected void grdDebitoContaRetorno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == 0)
                {
                    Label lstCriticas = (Label)e.Row.FindControl("lstCriticas");
                    AAT_TBL_RET_DEB_CONTA retDebConta = (AAT_TBL_RET_DEB_CONTA)e.Row.DataItem;
                    string strCrit = "";
                    foreach (AAT_TBL_RET_DEB_CONTA_CRITICAS crit in retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS)
                    {
                        strCrit += String.Format("<li>{0} - {1}</li>", crit.COD_CRITICA, crit.DCR_CRITICA);
                    }
                    lstCriticas.Text = String.Format("<ul style='margin: 0px;'>{0}</ul>", strCrit);

                    Label lblCritica = (Label)e.Row.FindControl("lblCritica");
                    retDebConta = (AAT_TBL_RET_DEB_CONTA)e.Row.DataItem;
                    strCrit = "";
                    AAT_TBL_RET_DEB_CONTA_CRITICAS crit2 = retDebConta.AAT_TBL_RET_DEB_CONTA_CRITICAS.FirstOrDefault();
                    if (crit2 != null)
                    {
                        strCrit += String.Format("{0} - {1}", crit2.COD_CRITICA, crit2.DCR_CRITICA);
                        lblCritica.Text = strCrit;
                    }

                }
            }
        }

        protected void btnSalvarEnvio_Click(object sender, EventArgs e)
        {
            //EmprestimoDescontoBLL bll = new EmprestimoDescontoBLL();
            //PRE_TBL_ARQ_ENVIO newEnvio = new PRE_TBL_ARQ_ENVIO();
            //Resultado res = new Resultado();

            //var user = (ConectaAD)Session["objUser"];

            //if (ddlStatusEnvio.SelectedValue == "1" && !String.IsNullOrEmpty(hidCodEnvio.Value))
            //{
            //    PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
            //    envioHist.COD_ARQ_ENVIO = int.Parse(hidCodEnvio.Value);
            //    envioHist.COD_ARQ_STATUS = 2;
            //    envioHist.DTH_INCLUSAO = System.DateTime.Now;
            //    envioHist.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
            //    //envioHist.PRE_TBL_ARQ_ENVIO_STATUS = new PRE_TBL_ARQ_ENVIO_STATUS();
            //    //envioHist.PRE_TBL_ARQ_ENVIO = newEnvio;
            //    bll.InsertHistorico(envioHist);
            //    LimparCampos(2);
            //    grdEnvio.DataBind();
            //    divDetalhesEnvio.Visible = false;
            //    divPesquisa.Visible = true;
            //}

            //if (ddlTipoEnvioEnvio.SelectedValue == "1") //Tipo relatório
            //{
            //    CheckBoxList chklstRel = new CheckBoxList();
            //    if (chklstRelCapJoia.Visible) chklstRel = chklstRelCapJoia;
            //    if (chklstRelCapAutoPatr.Visible) chklstRel = chklstRelCapAutoPatr;
            //    if (chklstRelCapVol.Visible) chklstRel = chklstRelCapVol;
            //    if (chklstRelEmprest.Visible) chklstRel = chklstRelEmprest;
            //    if (chklstRelSaude.Visible) chklstRel = chklstRelSaude;
            //    if (chklstRelSeguro.Visible) chklstRel = chklstRelSeguro;

            //    foreach (ListItem check in chklstRel.Items)
            //    {
            //        if (check.Selected)
            //        {

            //            newEnvio = NovoObjEnvio(0,
            //                                    Convert.ToInt16(ddlTipoEnvioEnvio.SelectedValue),
            //                                    Util.String2Short(txtAnoGerarEnvio.Text),
            //                                    Util.String2Short(txtMesGerarEnvio.Text),
            //                                    Util.String2Short(ddlAreaEnvio.SelectedValue),
            //                                    1,
            //                                    Util.String2Short(ddlGrupoEnvio.SelectedValue),
            //                                    txtReferenciaEnvio.Text,
            //                                    System.DateTime.Now,
            //                                    (user != null) ? user.login : "Desenv");

            //            newEnvio.COD_ARQ_SUB_TIPO = int.Parse(check.Value);

            //            res = bll.SaveData(newEnvio);

            //            if (res.Ok != true)
            //            {
            //                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro! Entre em contato com o administrador \\n\\n Descrição: " + res.Mensagem);
            //            }
            //        }
            //    }

            //    LimparCampos(2);
            //    grdEnvio.DataBind();
            //    divDetalhesEnvio.Visible = false;
            //    divPesquisa.Visible = true;
            //} 
            //else if (ddlTipoEnvioEnvio.SelectedValue == "2") //Tipo Arquivo Repasse
            //{
            //    int contador = 0;

            //    foreach (GridViewRow row in grdRepasse.Rows)
            //    {
            //        if (row.RowType == DataControlRowType.DataRow)
            //        {
            //            CheckBox chkSelect = (row.FindControl("chckRepasse") as CheckBox);
            //            int iCOD_ARQ_ENV_REPASSE = (int)grdRepasse.DataKeys[row.RowIndex].Value;

            //            if (chkSelect.Checked)
            //            {
            //                newEnvio = NovoObjEnvio(0,
            //                                        Convert.ToInt16(ddlTipoEnvioEnvio.SelectedValue),
            //                                        Util.String2Short(txtAnoGerarEnvio.Text),
            //                                        Util.String2Short(txtMesGerarEnvio.Text),
            //                                        Util.String2Short(ddlAreaEnvio.SelectedValue),
            //                                        1,
            //                                        Util.String2Short(ddlGrupoEnvio.SelectedValue),
            //                                        txtReferenciaEnvio.Text,
            //                                        System.DateTime.Now,
            //                                        (user != null) ? user.login : "Desenv");

            //                newEnvio.COD_ARQ_SUB_TIPO = iCOD_ARQ_ENV_REPASSE;

            //                res = bll.SaveData(newEnvio, iCOD_ARQ_ENV_REPASSE); //Util.String2Int32(((Label)row.FindControl("lblCodRepasse")).Text));

            //                if (res.Ok != true)
            //                {
            //                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro! Entre em contato com o administrador \\n\\n Descrição: " + res.Mensagem);
            //                }
            //                contador++;
            //            }
            //        }
            //    }
            //    if (contador == 0)
            //    {
            //        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Selecione um arquivo de repasse antes de salvar.");
            //        grdEnvio.DataBind();
            //    }
            //    else
            //    {
            //        if (res.Ok)
            //        {
            //            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Concluído com sucesso!");
            //            LimparCampos(2);
            //            grdEnvio.DataBind();
            //            divDetalhesEnvio.Visible = false;
            //            divPesquisa.Visible = true;
            //        }
            //        else
            //        {
            //            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao salvar o envio. " + res.Mensagem);
            //        }
            //    }
            //}
            //else if (ddlTipoEnvioEnvio.SelectedValue == "3") //Tipo outros arquivos
            //{
            //    if (FileUploadControl.HasFile)
            //    {
            //        //if (FileUploadControl.PostedFile.ContentType.Equals("text/plain"))
            //        //{

            //            string path = "";

            //            try
            //            {
            //                string filename = Path.GetFileName(FileUploadControl.FileName).ToString();
            //                string[] name = filename.Split('.');
            //                string UploadFilePath = Server.MapPath("UploadFile\\");

            //                path = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

            //                if (!Directory.Exists(UploadFilePath))
            //                {
            //                    Directory.CreateDirectory(UploadFilePath);
            //                }

            //                FileUploadControl.SaveAs(path);
            //                //FileUploadControl.PostedFile.InputStream;
            //                //DataTable dt = ReadTextFile(path);


            //                newEnvio = NovoObjEnvio(0,
            //                                        Convert.ToInt16(ddlTipoEnvioEnvio.SelectedValue),
            //                                        Util.String2Short(txtAnoGerarEnvio.Text),
            //                                        Util.String2Short(txtMesGerarEnvio.Text),
            //                                        Util.String2Short(ddlAreaEnvio.SelectedValue),
            //                                        null,
            //                                        Util.String2Short(ddlGrupoEnvio.SelectedValue),
            //                                        txtReferenciaEnvio.Text,
            //                                        System.DateTime.Now,
            //                                        (user != null) ? user.login : "Desenv");

            //                newEnvio.DAT_ARQUIVO = Util.File2Memory(path);
            //                newEnvio.DCR_CAMINHO_ARQUIVO = name[0];        
            //                newEnvio.DCR_ARQ_EXT = name[1];                            

            //                res = bll.SaveData(newEnvio); //Util.String2Int32(((Label)row.FindControl("lblCodRepasse")).Text));

            //                if (res.Ok)
            //                {
            //                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Concluído com sucesso!");
            //                    LimparCampos(2);
            //                    grdEnvio.DataBind();
            //                    divDetalhesEnvio.Visible = false;
            //                    divPesquisa.Visible = true;
            //                }
            //                else
            //                {
            //                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao salvar o envio. " + res.Mensagem);
            //                }

            //                //string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
            //                //string path = Server.MapPath("UploadFile\\") + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
            //                //FileUploadControl.SaveAs(path);
            //                ////Lê o Excel e converte para DataSet
            //                //DataSet ds = ReadExcelFileWork(path);
            //                //List<FichaFinanceira> list = ImportDataTableToList(ds);
            //                //ficha.InsereVerba(list, out mensagem);
            //                //MostraMensagemTelaUpdatePanel(upVerba, mensagem);
            //            }
            //            catch (Exception ex)
            //            {
            //                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
            //            }
            //            finally
            //            {
            //                FileUploadControl.FileContent.Dispose();
            //                FileUploadControl.FileContent.Flush();
            //                FileUploadControl.PostedFile.InputStream.Flush();
            //                FileUploadControl.PostedFile.InputStream.Close();
            //            }

            //        //}
            //        //else
            //        //{
            //        //    MostraMensagem(lblMensagemImportacao, "Atenção\\n\\nCarregue apenas arquivos texto simples (.txt) ou .CSV!");
            //        //}
            //    }
            //    else if (FileUploadControl.Visible)
            //    {
            //        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção\\n\\nSelecione um Arquivo para continuar!");
            //    }
            //}
        }
    }
}