using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IntegWeb.Entidades.Framework;
using System.Drawing;
using System.IO;

namespace IntegWeb.Previdencia.Web
{
    public partial class CargaProtheus : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                
                var user = (ConectaAD)Session["objUser"];


                //string area = user.departamento.Replace("Gerência - ", "");
             //   string area = "Coordenadoria - Contas e Processos em Saúde";
              //  string area = "Pagamentos Previdenciários";
               // string area = "Coordenadoria - Obrigações Legais e Cadastro";
             //   string area = "Cadastro";
              // string area = "Capitalização";
                 string area = "Tesouraria";
                CadProtheusBLL bll = new CadProtheusBLL();

                if (!IsPostBack)
                {

                    atualizaGrid();
                    ddlProtheus.DataSource = bll.GetCargaProtheusddl(area);
                    ddlProtheus.DataValueField = "COD_CARGA_TIPO";
                    ddlProtheus.DataTextField = "DCR_CARGA_TIPO";
                    ddlProtheus.DataBind();
                    ddlProtheus.Items.Insert(0, new ListItem("---Selecione---", ""));

                    if (area == "Coordenadoria - Obrigações Legais e Cadastro")
                    {
                        atualizaGridClifor();

                        //cblRepasseEmp.DataSource = bll.GetEmpresasRepasse();
                        //lblDatPagaVenc.Text = bll.GetEmpresasRepasse2();

                        cblRepasseEmp.DataSource = bll.GetEmpresasRepasse();
                        cblRepasseEmp.DataValueField = "COD_EMPRS";
                        cblRepasseEmp.DataTextField = "NOM_ABRVO_EMPRS";
                         cblRepasseEmp.DataBind();
                                           
                        
                    }


                    if (DateTime.Now.Hour > 11 && DateTime.Now.Hour < 14)
                    {
                        lblMsgCargaHorario.Visible = true;
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema : " + ex.Message);
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

        }

        public void btnok_Click(object sender, EventArgs e)
        {
            

            try
            {
                CadProtheusBLL obj = new CadProtheusBLL();
                PRE_TBL_CARGA_PROTHEUS_TIPO cpt = obj.GetCargaProtheusTabelaTipo(Int32.Parse(ddlProtheus.SelectedValue.ToString()));
                PRE_TBL_CARGA_PROTHEUS cp = obj.GetCargaProtheusTabelaCarga(Int32.Parse(ddlProtheus.SelectedValue.ToString()));
                PRE_TBL_CARGA_PROTHEUS newobj = new PRE_TBL_CARGA_PROTHEUS();


                    var user = (ConectaAD)Session["objUser"];



                    string area = user.departamento.Replace("Gerência - ", "");

                    if (ddlProtheus.Text == "20")
                    {

                        newobj.COD_CARGA_PROTHEUS = cpt.COD_CARGA_TIPO;
                        newobj.DTH_PAGAMENTO = DateTime.Parse(txtDataComple.Text.ToString());
                        newobj.COD_CARGA_TIPO = cpt.COD_CARGA_TIPO;
                        newobj.COD_CARGA_STATUS = 1;
                        newobj.DTH_EXECUCAO = null;
                        newobj.IND_EXEC_IMEDIATA = "N";
                        newobj.DCR_PARAMETROS = cpt.DCR_CARGA_TIPO;
                        newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                        newobj.DTH_INCLUSAO = DateTime.Now;
                        newobj.DTH_INCIO_PROCESSO = null;
                        newobj.DTH_FIM_PROCESSO = null;
                        newobj.DTH_DOCUMENTO_INICIAL = null;
                        newobj.DTH_DOCUMENTO_FINAL = null;
                        newobj.DTH_COMPLEMENTAR = DateTime.Parse(txtDataComple.Text.ToString());
                        newobj.DTH_SUPLEMENTAR = DateTime.Parse(txtDataSuple.Text.ToString());
                        newobj.COD_ASSOCIADO = Int32.Parse(txtAssociado.Text.ToString());
                        newobj.COD_LOTE = null;

                    }

                    else if (ddlProtheus.Text == "27")
                    {

                        newobj.COD_CARGA_PROTHEUS = cpt.COD_CARGA_TIPO;
                        newobj.DTH_PAGAMENTO = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                        newobj.COD_CARGA_TIPO = cpt.COD_CARGA_TIPO;
                        newobj.COD_CARGA_STATUS = 1;
                        newobj.DTH_EXECUCAO = null;
                        newobj.IND_EXEC_IMEDIATA = "N";
                        newobj.DCR_PARAMETROS = cpt.DCR_CARGA_TIPO;
                        newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                        newobj.DTH_INCLUSAO = DateTime.Now;
                        newobj.DTH_INCIO_PROCESSO = null;
                        newobj.DTH_FIM_PROCESSO = null;
                        newobj.DTH_DOCUMENTO_INICIAL = null;
                        newobj.DTH_DOCUMENTO_FINAL = null;
                        newobj.DTH_COMPLEMENTAR = null;
                        newobj.DTH_SUPLEMENTAR = null;
                        newobj.COD_ASSOCIADO = null;
                        newobj.COD_LOTE = Int32.Parse(txtLote.Text.ToString());

                    }
                    else
                    {
                        newobj.COD_CARGA_PROTHEUS = cpt.COD_CARGA_TIPO;
                        newobj.DTH_PAGAMENTO = DateTime.Parse(txtDatPagaVenc.Text.ToString());
                        newobj.COD_CARGA_TIPO = cpt.COD_CARGA_TIPO;
                        newobj.COD_CARGA_STATUS = 1;
                        newobj.DTH_EXECUCAO = null;
                        newobj.IND_EXEC_IMEDIATA = "N";
                        newobj.DCR_PARAMETROS = cpt.DCR_CARGA_TIPO;
                        newobj.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                        newobj.DTH_INCLUSAO = DateTime.Now;
                        newobj.DTH_INCIO_PROCESSO = null;
                        newobj.DTH_FIM_PROCESSO = null;
                        newobj.DTH_DOCUMENTO_INICIAL = null;
                        newobj.DTH_DOCUMENTO_FINAL = null;
                        newobj.DTH_COMPLEMENTAR = null;
                        newobj.DTH_SUPLEMENTAR = null;
                        newobj.COD_ASSOCIADO = null;
                        newobj.COD_LOTE = null;
                    }

                    Resultado res = obj.Validar(newobj);

                    if (res.Ok)
                    {

                        res = obj.SaveFila(newobj);

                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao salvar dados na MEDCTR");
                    }

                    CadProtheusBLL bll = new CadProtheusBLL();

                    if (String.IsNullOrEmpty(txtMesRef.Text))
                    {
                        if (ddlProtheus.Text == "20")
                        {
                            bll.GetProcessosGerados(cpt.COD_CARGA_TIPO, DateTime.Parse(txtDataComple.Text.ToString()), newobj.DTH_INCLUSAO, 0, 0, 0);
                        }
                        else if(ddlProtheus.Text == "27")
                        {
                            bll.GetProcessosGerados(cpt.COD_CARGA_TIPO, DateTime.Parse(newobj.DTH_PAGAMENTO.ToString()), newobj.DTH_INCLUSAO, 0, 0, 0);
                        }
                        else if (ddlProtheus.Text == "18")
                        {
                            bll.GetProcessosGerados(cpt.COD_CARGA_TIPO, DateTime.Parse(txtDatPagaVenc.Text.ToString()), newobj.DTH_INCLUSAO, 0, 0, 41);
                        }
                        else if (ddlProtheus.Text == "17")
                        {
                            bll.GetProcessosGerados(cpt.COD_CARGA_TIPO, DateTime.Parse(txtDatPagaVenc.Text.ToString()), newobj.DTH_INCLUSAO, 0, 0, 0);
                        }
                        else
                        {
                            bll.GetProcessosGerados(cpt.COD_CARGA_TIPO, DateTime.Parse(txtDatPagaVenc.Text.ToString()), newobj.DTH_INCLUSAO, 0, 0, 0);
                        }
                                        
                    }
                    else
                    {
                           string s = cblRepasseEmp.Items.Cast<ListItem>().Where(item => item.Selected)
                                                                        .Aggregate("", (current, item) => current + (item.Value + ","));

                           bll.InsereEmpresasRepasse(s);

                            bll.GetProcessosGerados(cpt.COD_CARGA_TIPO, DateTime.Parse(txtDatPagaVenc.Text.ToString()), newobj.DTH_INCLUSAO, Convert.ToInt32(txtMesRef.Text), DateTime.Parse(txtDatPagaVenc.Text.ToString()).Year, 0);

                            bll.DeletaEmpresasRepasse();
                        
                    }

                        CadProtheusBLL objLote = new CadProtheusBLL();

                        DataTable dtLote = bll.RetornaNumeroLote(user.login.ToString(), newobj.DTH_INCLUSAO);


                        if (!objLote.ValidaMedctr(Convert.ToInt32(dtLote.Rows[0][0].ToString())))
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao criar processo, movimento não está registrado com as informações solicitadas");
                        }
                
                    atualizaGrid();

            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema : " + ex.Message);
            }

            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Arquivos gravados com sucesso!");
        }

        protected void ddlProtheus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProtheus.Text == "20")
                {
                    lblDataComple.Visible = true;
                    txtDataComple.Visible = true;
                    lblDataSuple.Visible = true;
                    txtDataSuple.Visible = true;
                    lblAssociado.Visible = true;
                    txtAssociado.Visible = true;
                    lblDatPagaVenc.Visible = false;
                    txtDatPagaVenc.Visible = false;
                    FileUploadControl.Visible = false;
                    btnCarregaArq.Visible = false;
                    lblMesRef.Visible = false;
                    txtMesRef.Visible = false;
                    btnok.Visible = true;
                    lblDataInicial.Visible = false;
                    txtDataInicial.Visible = false;
                    lblDataFinal.Visible = false;
                    txtDataFinal.Visible = false;
                    fuTxt.Visible = false;
                    lblTxt.Visible = false;
                    btnCarregaFarm.Visible = false;
                }
                else if (ddlProtheus.Text == "27")
                {
                    lblLote.Visible = true;
                    txtLote.Visible = true;
                    lblDatPagaVenc.Visible = false;
                    txtDatPagaVenc.Visible = false;
                    FileUploadControl.Visible = false;
                    btnCarregaArq.Visible = false;
                    lblMesRef.Visible = false;
                    txtMesRef.Visible = false;
                    btnok.Visible = true;
                    lblDataInicial.Visible = false;
                    txtDataInicial.Visible = false;
                    lblDataFinal.Visible = false;
                    txtDataFinal.Visible = false;
                    fuTxt.Visible = false;
                    lblTxt.Visible = false;
                    btnCarregaFarm.Visible = false;
                }
                else if (ddlProtheus.Text == "31")
                {
                    lblDataInicial.Visible = true;
                    txtDataInicial.Visible = true;
                    lblDataFinal.Visible = true;
                    txtDataFinal.Visible = true;
                    FileUploadControl.Visible = false;
                    btnCarregaArq.Visible = false;
                    lblMesRef.Visible = false;
                    txtMesRef.Visible = false;
                    lblTxt.Visible = true;
                    btnCarregaFarm.Visible = true;
                    fuTxt.Visible = true;
                    btnok.Visible = false;

                }
                else if (ddlProtheus.Text == "99")
                {
                    FileUploadControl.Visible = true;
                    btnCarregaArq.Visible = true;
                    lblMesRef.Visible = false;
                    txtMesRef.Visible = false;
                    btnok.Visible = false;
                    lblDataInicial.Visible = false;
                    txtDataInicial.Visible = false;
                    lblDataFinal.Visible = false;
                    txtDataFinal.Visible = false;
                    fuTxt.Visible = false;
                    lblTxt.Visible = false;
                    btnCarregaFarm.Visible = false;
                }
                else if (ddlProtheus.Text == "14" || ddlProtheus.Text == "15" || ddlProtheus.Text == "16" || ddlProtheus.Text == "30" )
                {
                    lblDataComple.Visible = false;
                    txtDataComple.Visible = false;
                    lblDataSuple.Visible = false;
                    txtDataSuple.Visible = false;
                    lblLote.Visible = false;
                    txtLote.Visible = false;
                    lblDataInicial.Visible = false;
                    txtDataInicial.Visible = false;
                    lblDataFinal.Visible = false;
                    txtDataFinal.Visible = false;
                    lblAssociado.Visible = false;
                    txtAssociado.Visible = false;
                    lblDatPagaVenc.Visible = true;
                    txtDatPagaVenc.Visible = true;
                    FileUploadControl.Visible = false;
                    btnCarregaArq.Visible = false;
                    lblMesRef.Visible = true;
                    txtMesRef.Visible = true;
                    btnok.Visible = true;
                    lblDataInicial.Visible = false;
                    txtDataInicial.Visible = false;
                    lblDataFinal.Visible = false;
                    txtDataFinal.Visible = false;
                    fuTxt.Visible = false;
                    lblTxt.Visible = false;
                    btnCarregaFarm.Visible = false;
                    lblRepasseEmp.Visible = true;
                    //ddlRepasseEmp.Visible = true;
                    cblRepasseEmp.Visible = true;
                }
                else if (ddlProtheus.Text == "17" || ddlProtheus.Text == "18")
                {
                    lblDataComple.Visible = false;
                    txtDataComple.Visible = false;
                    lblDataSuple.Visible = false;
                    txtDataSuple.Visible = false;
                    lblLote.Visible = false;
                    txtLote.Visible = false;
                    lblDataInicial.Visible = false;
                    txtDataInicial.Visible = false;
                    lblDataFinal.Visible = false;
                    txtDataFinal.Visible = false;
                    lblAssociado.Visible = false;
                    txtAssociado.Visible = false;
                    lblDatPagaVenc.Visible = true;
                    txtDatPagaVenc.Visible = true;
                    FileUploadControl.Visible = false;
                    btnCarregaArq.Visible = false;
                    lblMesRef.Visible = true;
                    txtMesRef.Visible = true;
                    btnok.Visible = true;
                    lblDataInicial.Visible = false;
                    txtDataInicial.Visible = false;
                    lblDataFinal.Visible = false;
                    txtDataFinal.Visible = false;
                    fuTxt.Visible = false;
                    lblTxt.Visible = false;
                    btnCarregaFarm.Visible = false;
                    lblRepasseEmp.Visible = false;
                    ddlRepasseEmp.Visible = false;
                    cblRepasseEmp.Visible = false;
                    lblMesRef.Visible = false;
                    txtMesRef.Visible = false;
                }
                else
                {
                    lblDataComple.Visible = false;
                    txtDataComple.Visible = false;
                    lblDataSuple.Visible = false;
                    txtDataSuple.Visible = false;
                    lblLote.Visible = false;
                    txtLote.Visible = false;
                    lblDataInicial.Visible = false;
                    txtDataInicial.Visible = false;
                    lblDataFinal.Visible = false;
                    txtDataFinal.Visible = false;
                    lblAssociado.Visible = false;
                    txtAssociado.Visible = false;
                    lblDatPagaVenc.Visible = true;
                    txtDatPagaVenc.Visible = true;
                    FileUploadControl.Visible = false;
                    btnCarregaArq.Visible = false;
                    lblMesRef.Visible = false;
                    txtMesRef.Visible = false;
                    btnok.Visible = true;
                    fuTxt.Visible = false;
                    lblTxt.Visible = false;
                    btnCarregaFarm.Visible = false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema : " + ex.Message);
            }
        }

        protected void grdFilaProcesso_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void atualizaGrid()
        {
            CadProtheusBLL objBll = new CadProtheusBLL();
            var user = (ConectaAD)Session["objUser"];

            grdFilaProcesso.DataSource = objBll.GeraGridProcesso((user != null) ? user.login.ToString() : "Desenv");
            grdFilaProcesso.DataBind();
        }

        protected void atualizaGridClifor()
        {
            CadProtheusBLL objBll = new CadProtheusBLL();

            DataTable dt = objBll.GeraErroClifor();

            if (dt.Rows.Count > 0)
            {
                grdClifor.DataSource = dt;
                grdClifor.DataBind();
                divClifor.Visible = true;
            }
            else
            {
                divClifor.Visible = false;
            }
     

            
            
        }

        protected void grdFilaProcesso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CadProtheusBLL bll = new CadProtheusBLL();

            var user = (ConectaAD)Session["objUser"];


            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int indexLinha = gvr.RowIndex;

            DateTime dtInclusao = Convert.ToDateTime(((Label)grdFilaProcesso.Rows[indexLinha].FindControl("lblDataInclusao")).Text);
            DataTable dt = bll.RetornaNumeroLote(user.login.ToString(), dtInclusao);

            if (e.CommandName == "Gerar")
            {
                DataSet ds = new DataSet();

                if (((Label)grdFilaProcesso.Rows[indexLinha].FindControl("lblTipo")).Text == "Pagamento a Rede Credenciada")
                {
                    ds = bll.GeraRelCredenc(Convert.ToInt32(dt.Rows[0][0].ToString()));

                    ArquivoDownload adMedAberta = new ArquivoDownload();
                    adMedAberta.nome_arquivo = "MEDCTRAberta.xlsx";
                    adMedAberta.dados = ds.Tables[0];
                    Session[ValidaCaracteres(adMedAberta.nome_arquivo)] = adMedAberta;
                    string fullMedAberta = "WebFile.aspx?dwFile=" + ValidaCaracteres(adMedAberta.nome_arquivo);
                    AdicionarAcesso(fullMedAberta);
                    AbrirNovaAba(upUpdatePanel, fullMedAberta, adMedAberta.nome_arquivo);

                    ArquivoDownload adResPrograma = new ArquivoDownload();
                    adResPrograma.nome_arquivo = "ResumoProdutoRedeCredenciada.xlsx";
                    adResPrograma.dados = ds.Tables[1];
                    Session[ValidaCaracteres(adResPrograma.nome_arquivo)] = adResPrograma;
                    string fullResPrograma = "WebFile.aspx?dwFile=" + ValidaCaracteres(adResPrograma.nome_arquivo);
                    AdicionarAcesso(fullResPrograma);
                    AbrirNovaAba(upUpdatePanel, fullResPrograma, adResPrograma.nome_arquivo);

                    ArquivoDownload adResPatrocinador = new ArquivoDownload();
                    adResPatrocinador.nome_arquivo = "ResumoPatrocinadorRedeCredenciada.xlsx";
                    adResPatrocinador.dados = ds.Tables[2];
                    Session[ValidaCaracteres(adResPatrocinador.nome_arquivo)] = adResPatrocinador;
                    string fullResPatrocinador = "WebFile.aspx?dwFile=" + ValidaCaracteres(adResPatrocinador.nome_arquivo);
                    AdicionarAcesso(fullResPatrocinador);
                    AbrirNovaAba(upUpdatePanel, fullResPatrocinador, adResPatrocinador.nome_arquivo);

                    ArquivoDownload adRelLiquidez = new ArquivoDownload();
                    adRelLiquidez.nome_arquivo = "ResumoLiquidezRedeCredenciada.xlsx";
                    adRelLiquidez.dados = ds.Tables[3];
                    Session[ValidaCaracteres(adRelLiquidez.nome_arquivo)] = adRelLiquidez;
                    string fullLiqui = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelLiquidez.nome_arquivo);
                    AdicionarAcesso(fullLiqui);
                    AbrirNovaAba(upUpdatePanel, fullLiqui, adRelLiquidez.nome_arquivo);


                }
                else
                {

                    ds = bll.GeraRelGerais(Convert.ToInt32(dt.Rows[0][0].ToString()));

                    ArquivoDownload adMedAberta = new ArquivoDownload();
                    adMedAberta.nome_arquivo = "MEDCTRAberta.xlsx";
                    adMedAberta.dados = ds.Tables[0];
                    Session[ValidaCaracteres(adMedAberta.nome_arquivo)] = adMedAberta;
                    string fullMedAberta = "WebFile.aspx?dwFile=" + ValidaCaracteres(adMedAberta.nome_arquivo);
                    AdicionarAcesso(fullMedAberta);
                    AbrirNovaAba(upUpdatePanel, fullMedAberta, adMedAberta.nome_arquivo);

                   // if (user.departamento.Replace("Gerência - ", "") == "Coordenadoria - Obrigações Legais e Cadastro")
                    if (user.departamento.Replace("Gerência - ", "") == "Coordenadoria - Obrigações Legais e Cadastro")
                    {
                        ArquivoDownload adRepasse = new ArquivoDownload();
                        adRepasse.nome_arquivo = "Repasse.xlsx";
                        adRepasse.dados = bll.GetRepasse(Convert.ToInt32(dt.Rows[0][0].ToString()));
                        Session[ValidaCaracteres(adRepasse.nome_arquivo)] = adRepasse;
                        string fullRepasse = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRepasse.nome_arquivo);
                        AdicionarAcesso(fullRepasse);
                        AbrirNovaAba(upUpdatePanel, fullRepasse, adRepasse.nome_arquivo);
                    }
                    else
                    {


                        ArquivoDownload adResPrograma = new ArquivoDownload();
                        adResPrograma.nome_arquivo = "ResumoPrograma.xlsx";
                        adResPrograma.dados = ds.Tables[1];
                        Session[ValidaCaracteres(adResPrograma.nome_arquivo)] = adResPrograma;
                        string fullResPrograma = "WebFile.aspx?dwFile=" + ValidaCaracteres(adResPrograma.nome_arquivo);
                        AdicionarAcesso(fullResPrograma);
                        AbrirNovaAba(upUpdatePanel, fullResPrograma, adResPrograma.nome_arquivo);

                        ArquivoDownload adResPatrocinador = new ArquivoDownload();
                        adResPatrocinador.nome_arquivo = "ResumoPatrocinador.xlsx";
                        adResPatrocinador.dados = ds.Tables[2];
                        Session[ValidaCaracteres(adResPatrocinador.nome_arquivo)] = adResPatrocinador;
                        string fullResPatrocinador = "WebFile.aspx?dwFile=" + ValidaCaracteres(adResPatrocinador.nome_arquivo);
                        AdicionarAcesso(fullResPatrocinador);
                        AbrirNovaAba(upUpdatePanel, fullResPatrocinador, adResPatrocinador.nome_arquivo);

                        ArquivoDownload adRelLiquidez = new ArquivoDownload();
                        adRelLiquidez.nome_arquivo = "ResumoLiquidez.xlsx";
                        adRelLiquidez.dados = ds.Tables[3];
                        Session[ValidaCaracteres(adRelLiquidez.nome_arquivo)] = adRelLiquidez;
                        string fullLiqui = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelLiquidez.nome_arquivo);
                        AdicionarAcesso(fullLiqui);
                        AbrirNovaAba(upUpdatePanel, fullLiqui, adRelLiquidez.nome_arquivo);
                    }
                }

                if (((Label)grdFilaProcesso.Rows[indexLinha].FindControl("lblStatusProcesso")).Text == "Rejeitado")
                {
                    DataTable rjt = bll.GetRejeitada(Convert.ToInt32(((Label)grdFilaProcesso.Rows[indexLinha].FindControl("lblNumLote")).Text));

                    ArquivoDownload rejeitado = new ArquivoDownload();
                    rejeitado.nome_arquivo = "Rejeitados.xlsx";
                    rejeitado.dados = rjt;
                    Session[ValidaCaracteres(rejeitado.nome_arquivo)] = rejeitado;
                    string fullRejeitado = "WebFile.aspx?dwFile=" + ValidaCaracteres(rejeitado.nome_arquivo);
                    AdicionarAcesso(fullRejeitado);
                    AbrirNovaAba(upUpdatePanel, fullRejeitado, rejeitado.nome_arquivo);
                }

            }
            else if (e.CommandName == "Validar")
            {
                if (DateTime.Now.Hour > 11 && DateTime.Now.Hour < 14)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não é possível validar nesse momento, a carga do Protheus está em processamento");

                }
                else if (((Label)grdFilaProcesso.Rows[indexLinha].FindControl("lblTipo")).Text == "Folha Mensal Suplementados")
                {
                    if (DateTime.Now.Hour < 14)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não é possível realizar a validação desse lote no momento devido ao volume de dados, favor efetuar a validação após as 14:00 horas");
                    }
                    else
                    {
                        bll.ValidaLote(Convert.ToInt32(dt.Rows[0][0].ToString()));
                        atualizaGrid();
                    }
                }
                else
                {
                    bll.ValidaLote(Convert.ToInt32(dt.Rows[0][0].ToString()));
                    atualizaGrid();
                }
            }
            else if (e.CommandName == "Excluir")
            {
                bll.ExcluiLote(Convert.ToInt32(dt.Rows[0][0].ToString()));
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Lote excluído");
                atualizaGrid();

            }

        }

        protected void grdFilaProcesso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var status = (Label)e.Row.FindControl("lblStatusProcesso");
                if (status.Text == "Aguardando validação")
                {
                    for (int i = 0; i <= 8; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromArgb(204, 229, 255);

                    }

                }
                else if (status.Text == "Rejeitado")
                {
                    for (int i = 0; i <= 8; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromArgb(255, 229, 204);

                    }
                }
                else
                {
                    for (int i = 0; i <= 8; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromArgb(255, 255, 255);
                    }
                }

            }
        }

        protected void btnCarregaArq_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                if (FileUploadControl.PostedFile.ContentType.Equals("application/vnd.ms-excel") || // xls
                     FileUploadControl.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))//xlsx
                {
                    string Excel = "";

                    if (!String.IsNullOrEmpty(txtDatPagaVenc.Text))
                    {

                        try
                        {
                            string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                            string UploadFilePath = Server.MapPath("UploadFile\\");
                            Excel = UploadFilePath + name[0] + "." + name[1];

                            CadProtheusBLL obj = new CadProtheusBLL();

                            if (!Directory.Exists(UploadFilePath))
                            {
                                Directory.CreateDirectory(UploadFilePath);
                            }

                            FileUploadControl.SaveAs(Excel);

                            DataTable dtMed = ReadExcelFile(Excel);

                            foreach (DataRow dr in dtMed.Rows)
                            {
                                if (String.IsNullOrEmpty(dr[3].ToString()) && String.IsNullOrEmpty(dr[4].ToString()))
                                {
                                    dr.Delete();
                                }
                            }
                            dtMed.AcceptChanges();

                            DataTable dtCLi = new DataTable();

                            DataTable dtProtheus = new DataTable();

                            int num_lote = Convert.ToInt32(obj.retornaMaxLote());

                            bool retorna = false;


                            //       int c = 0;
                            StringBuilder sb = new StringBuilder();

                            foreach (DataRow linha in dtMed.Rows)
                            {
                                dtCLi = obj.GetDadosClifor(Convert.ToInt32(linha["COD_EMPRS"].ToString()), Convert.ToInt32(linha["NUM_RGTRO_EMPRG"].ToString()));

                                if (dtCLi.Rows.Count == 0)
                                {
                                    break;
                                }

                                if (linha["PROGRAMA"].ToString().Length < 4)
                                {
                                    sb.Append(linha["PROGRAMA"].ToString());
                                    sb.Insert(0, "0");
                                    linha["PROGRAMA"] = sb.ToString();
                                    sb.Clear();

                                }

                                if (linha["PATROCINADOR"].ToString().Length < 4)
                                {
                                    sb.Append(linha["PATROCINADOR"].ToString());
                                    sb.Insert(0, "0");
                                    linha["PATROCINADOR"] = sb.ToString();
                                    sb.Clear();
                                }

                                if (linha["SUBMASSA"].ToString().Length < 2)
                                {
                                    sb.Append(linha["SUBMASSA"].ToString());
                                    sb.Insert(0, "0");
                                    linha["SUBMASSA"] = sb.ToString();
                                    sb.Clear();

                                }

                                if (linha["EVENTO"].ToString().Length < 6)
                                {
                                    sb.Append(linha["EVENTO"].ToString());

                                    while (sb.Length < 6)
                                    {
                                        sb.Insert(0, "0");
                                    }
                                    linha["EVENTO"] = sb.ToString();
                                    sb.Clear();
                                }

                                if (linha["XTPLIQ"].ToString().Length < 2)
                                {
                                    sb.Append(linha["XTPLIQ"].ToString());
                                    sb.Insert(0, "0");
                                    linha["XTPLIQ"] = sb.ToString();
                                    sb.Clear();
                                }

                                retorna = obj.InsereProtheus(num_lote, linha["XNUMCT"].ToString(), linha["XTPLIQ"].ToString(), linha["DTREF"].ToString(), linha["DTVENC"].ToString(), linha["PRODUT"].ToString(),
                                                  linha["CCUSTO"].ToString(), linha["NOSSONUMERO"].ToString(), linha["PROGRAMA"].ToString(), linha["PATROCINADOR"].ToString()
                                                   , linha["SUBMASSA"].ToString(), linha["PROJETO"].ToString(), Convert.ToDecimal(linha["VALMED"].ToString()), dtCLi.Rows[0]["MAX(CLI.TIPOPAR)"].ToString(),
                                                  Util.String2Int32(linha["COD_ASSOC"].ToString()), Util.String2Int32(linha["COD_CONVENENTE"].ToString()), Convert.ToInt32(linha["COD_EMPRS"].ToString()), Convert.ToInt32(linha["NUM_RGTRO_EMPRG"].ToString()), Util.String2Int32(linha["NUM_MATR_PARTF"].ToString()), Util.String2Int32(linha["NUM_IDNTF_RPTANT"].ToString()),
                                                  Util.String2Int32(linha["NUM_IDNTF_DPDTE"].ToString())
                                                  , dtCLi.Rows[0]["BANCO"].ToString(), dtCLi.Rows[0]["AGENCIA"].ToString(), dtCLi.Rows[0]["DVAGE"].ToString(), dtCLi.Rows[0]["NUMCON"].ToString(), dtCLi.Rows[0]["DVNUMCON"].ToString(), linha["EVENTO"].ToString()
                                                   );
                            }

                            if (retorna)
                            {

                                //CadProtheusBLL bll = new CadProtheusBLL();

                                obj.InsereMedLiq(num_lote);

                                PRE_TBL_CARGA_PROTHEUS ptc = new PRE_TBL_CARGA_PROTHEUS();

                                var user = (ConectaAD)Session["objUser"];

                                ptc.COD_CARGA_PROTHEUS = 99;
                                ptc.DTH_PAGAMENTO = DateTime.Parse(txtDatPagaVenc.Text.ToString());
                                ptc.COD_CARGA_TIPO = 99;
                                ptc.COD_CARGA_STATUS = 1;
                                ptc.DTH_EXECUCAO = null;
                                ptc.IND_EXEC_IMEDIATA = "N";
                                ptc.DCR_PARAMETROS = "Medição Manual";
                                ptc.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                                ptc.DTH_INCLUSAO = DateTime.Now;
                                ptc.DTH_INCIO_PROCESSO = null;
                                ptc.DTH_FIM_PROCESSO = null;
                                ptc.DTH_DOCUMENTO_INICIAL = null;
                                ptc.DTH_DOCUMENTO_FINAL = null;
                                ptc.DTH_COMPLEMENTAR = null;
                                ptc.DTH_SUPLEMENTAR = null;
                                ptc.COD_ASSOCIADO = null;
                                ptc.COD_LOTE = null;
                                ptc.NUM_LOTE = num_lote;

                                CadProtheusBLL teste = new CadProtheusBLL();

                                Resultado res = new Resultado();
                                res = teste.SaveFila(ptc);

                                atualizaGrid();

                            }


                            //  c++;

                        }
                        catch (Exception ex)
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao criar Medição, favor entrar em contato com o administrador do sistema: " + ex.Message);
                        }
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Favor preencher campo 'Data de Pagamento/Vencimento' ");
                        txtDatPagaVenc.Focus();
                    }


                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Tipo de arquivo inválido, carregue um arquivo do tipo Excel(.xls ou .xlsx)");
                }
            }
        }

        protected void grdClifor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdClifor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            atualizaGridClifor();
            grdClifor.PageIndex = e.NewPageIndex;
            grdClifor.DataBind();
        }

        protected void btnAttClifor_Click(object sender, EventArgs e)
        {
            CadProtheusBLL bll = new CadProtheusBLL();
            bll.AtualizaClifor();
            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Clifor atualizada, favor entrar em contato com a Pagadoria para realizar uma nova carga ou aguardar a rotina automática da meia noite. ");
            atualizaGridClifor();

        }

        protected void btnCarregaFarm_Click(object sender, EventArgs e)
        {

            var user = (ConectaAD)Session["objUser"];

            CadProtheusBLL obj = new CadProtheusBLL();

            PRE_TBL_CARGA_PROTHEUS_TIPO cpt = obj.GetCargaProtheusTabelaTipo(Int32.Parse(ddlProtheus.SelectedValue.ToString()));

            if (fuTxt.HasFile)
                        {

                            try
                            {

                                string path_distribuicao = "";

                                string[] name = Path.GetFileName(fuTxt.FileName).ToString().Split('.');
                                string UploadFilePath = Server.MapPath("UploadFiled\\");
                                path_distribuicao = UploadFilePath + name[0] + "." + name[1];


                                if (!Directory.Exists(UploadFilePath))
                                {
                                    Directory.CreateDirectory(UploadFilePath);
                                }

                                fuTxt.SaveAs(path_distribuicao);

                                DataTable dtExcelFarm = ReadTextFile(path_distribuicao);

                                for (int i = 0; i < dtExcelFarm.Rows.Count; i++)
                                {
                                    string cod_emprs = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(0, 3);
                                    string num_rgtro = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(3, 7);
                                    string dig = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(10, 1);
                                    string num_matr_sub = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(11, 2);
                                    string dig_num_matt = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(13, 1);
                                    string nome = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(14, 41);
                                    string vlr_medcto = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(55, 11);
                                    string vlr_reemb = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(67, 10);
                                    string protocolo = dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(80, 12);
                                    DateTime dt_arq = Convert.ToDateTime(dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(92, 8).Substring(0, 2) + "/" + dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(92, 8).Substring(2, 2) + "/" + dtExcelFarm.Rows[i].ItemArray[0].ToString().Substring(92, 8).Substring(4, 4));

                                    CadProtheusBLL cadbll = new CadProtheusBLL();





                                    if (!cadbll.InsereReembFarm(System.DateTime.Now, user.login, Convert.ToInt32(cod_emprs), Convert.ToInt32(num_rgtro), Convert.ToInt32(dig), Convert.ToInt32(num_matr_sub), Convert.ToInt32(dig_num_matt), nome, Convert.ToDouble(vlr_medcto), Convert.ToDouble(vlr_reemb), Convert.ToInt32(protocolo), dt_arq, Convert.ToDateTime(txtDatPagaVenc.Text)))
                                    {
                                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao Salvar dados do arquivo");
                                    }


                                }



                                PRE_TBL_CARGA_PROTHEUS ptc = new PRE_TBL_CARGA_PROTHEUS();

                                ptc.COD_CARGA_PROTHEUS = cpt.COD_CARGA_TIPO;
                                ptc.DTH_PAGAMENTO = DateTime.Parse(txtDatPagaVenc.Text.ToString());
                                ptc.COD_CARGA_TIPO = cpt.COD_CARGA_TIPO;
                                ptc.COD_CARGA_STATUS = 1;
                                ptc.DTH_EXECUCAO = null;
                                ptc.IND_EXEC_IMEDIATA = "N";
                                ptc.DCR_PARAMETROS = cpt.DCR_CARGA_TIPO;
                                ptc.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                                ptc.DTH_INCLUSAO = Convert.ToDateTime(DateTime.Now.ToString("g"));
                                ptc.DTH_INCIO_PROCESSO = null;
                                ptc.DTH_FIM_PROCESSO = null;
                                ptc.DTH_DOCUMENTO_INICIAL = DateTime.Parse(txtDataInicial.Text.ToString());
                                ptc.DTH_DOCUMENTO_FINAL = DateTime.Parse(txtDataFinal.Text.ToString());
                                ptc.DTH_COMPLEMENTAR = null;
                                ptc.DTH_SUPLEMENTAR = null;
                                ptc.COD_ASSOCIADO = null;
                                ptc.COD_LOTE = null;


                                CadProtheusBLL bll = new CadProtheusBLL();

                                obj.SaveFila(ptc);

                                bll.GetProcessosGerados(cpt.COD_CARGA_TIPO, DateTime.Parse(txtDatPagaVenc.Text.ToString()), ptc.DTH_INCLUSAO, 0, 0, 0);


                                CadProtheusBLL objLote = new CadProtheusBLL();

                                DataTable dtLote = bll.RetornaNumeroLote(user.login.ToString(), ptc.DTH_INCLUSAO);

                                if (!objLote.ValidaMedctr(Convert.ToInt32(dtLote.Rows[0][0].ToString())))
                                {
                                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao criar processo, movimento não está registrado com as informações solicitadas");
                                }
                                else
                                {
                                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Arquivos gravados com sucesso!");
                                }
                            }
                            catch (Exception ex)
                            {
                                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao criar Medição, favor entrar em contato com o administrador do sistema: " + ex.Message);
                            }

                         


                        }


          
            
            atualizaGrid();

        }



    }
}