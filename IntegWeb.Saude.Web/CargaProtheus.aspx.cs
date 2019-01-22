using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.ENTITY;
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
using System.IO;
using System.Drawing;

namespace IntegWeb.Saude.Web
{
    public partial class CargaProtheus : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                var user = (ConectaAD)Session["objUser"];
                string area = user.departamento.Replace("Gerência - ", "");
                 //string area = "Contas e Processos em Saúde";
                 //string area = "Pagamentos Previdenciários/Seguridade";

                CadProtheusBLL bll = new CadProtheusBLL();

                if (!IsPostBack)
                {

                    atualizaGrid(area);
                    ddlProtheus.DataSource = bll.GetCargaProtheusddl(area);
                    ddlProtheus.DataValueField = "COD_CARGA_TIPO";
                    ddlProtheus.DataTextField = "DCR_CARGA_TIPO";
                    ddlProtheus.DataBind();
                    ddlProtheus.Items.Insert(0, new ListItem("---Selecione---", ""));
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

        }

        public void btnOk_Click(object sender, EventArgs e)
        {

            try
            {
                CadProtheusBLL obj = new CadProtheusBLL();
                PRE_TBL_CARGA_PROTHEUS_TIPO cpt = obj.GetCargaProtheusTabelaTipo(Int32.Parse(ddlProtheus.SelectedValue.ToString()));
                PRE_TBL_CARGA_PROTHEUS cp = obj.GetCargaProtheusTabelaCarga(Int32.Parse(ddlProtheus.SelectedValue.ToString()));
                PRE_TBL_CARGA_PROTHEUS newobj = new PRE_TBL_CARGA_PROTHEUS();

                if (cp == null || cp.COD_CARGA_STATUS == 3 || txtDatPagaVenc.Text != cp.DTH_PAGAMENTO.ToString().Substring(0, 10))
                {

                    var user = (ConectaAD)Session["objUser"];

                    string area = (user != null) ? user.departamento.Replace("Gerência - ", "") : "Desenv";

                    if (ddlProtheus.Text == "20")
                    {

                        newobj.COD_CARGA_PROTHEUS = cpt.COD_CARGA_TIPO;
                        newobj.DTH_PAGAMENTO = null;
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
                        newobj.DTH_PAGAMENTO = null;
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

                    else if (ddlProtheus.Text == "31")
                    {

                        if (fuTxt.HasFile)
                        {
                            string path_distribuicao = "";

                            string[] name = Path.GetFileName(fuTxt.FileName).ToString().Split('.');
                            string UploadFilePath = Server.MapPath("UploadFile\\");
                            path_distribuicao = UploadFilePath + name[0] + "." + name[1]; ;


                            if (!Directory.Exists(UploadFilePath))
                            {
                                Directory.CreateDirectory(UploadFilePath);
                            }


                            fuTxt.SaveAs(path_distribuicao);
                            DataTable dtExcelPedidos = ReadTextFile(path_distribuicao);


                            for (int i = 0; i < dtExcelPedidos.Rows.Count; i++)
                            {
                                string var1 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(0, 3);
                                string var2 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(3, 7);
                                string var3 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(10, 1);
                                string var4 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(11, 2);
                                string var5 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(13, 1);
                                string var6 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(14, 41);
                                string var7 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(55, 11);
                                string var8 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(67, 10);
                                string var9 = dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(80, 12);
                                DateTime var10 = Convert.ToDateTime(dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(92, 8).Substring(0, 2) + "/" + dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(92, 8).Substring(2, 2) + "/" + dtExcelPedidos.Rows[i].ItemArray[0].ToString().Substring(92, 8).Substring(4, 4));

                                REEMB_FRMCIA objTxt = new REEMB_FRMCIA();
                                decimal pk = obj.GetMaxPk();

                                objTxt.HDRDATHOR = System.DateTime.Now.ToString();
                                objTxt.HDRCODUSU = (user != null) ? user.login : "Desenv";
                                objTxt.HDRCODETC = "ETQ-001";
                                objTxt.HDRCODPGR = "CARGA";
                                objTxt.COD_EMPRS = short.Parse(var1);
                                objTxt.NUM_RGTRO_EMPRG = int.Parse(var2);
                                objTxt.DIG = short.Parse(var3);
                                objTxt.NUM_MATR_SUB = short.Parse(var4);
                                objTxt.DIG_MATR_SUB = short.Parse(var5);
                                objTxt.NOME = var6;
                                objTxt.VLR_MEDCTO = decimal.Parse(var7);
                                objTxt.VLR_REEMB = decimal.Parse(var8);
                                objTxt.PROTOCOLO = long.Parse(var9);
                                objTxt.DATA_ARQUIVO = var10;
                                objTxt.MRC_LANC_MEDCTR = "N";
                                objTxt.DATA_CREDITO = null;
                                objTxt.ID_REG = pk;

                                Resultado validar = obj.ValidarTxt(objTxt);

                                if (validar.Ok)
                                {
                                    validar = obj.SaveTxt(objTxt);
                                }
                                else
                                {
                                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao Salvar dados");
                                }

                            }

                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Nenhum arquivo anexado!");
                        }


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
                        newobj.DTH_DOCUMENTO_INICIAL = DateTime.Parse(txtDataInicial.Text.ToString());
                        newobj.DTH_DOCUMENTO_FINAL = DateTime.Parse(txtDataFinal.Text.ToString());
                        newobj.DTH_COMPLEMENTAR = null;
                        newobj.DTH_SUPLEMENTAR = null;
                        newobj.COD_ASSOCIADO = null;
                        newobj.COD_LOTE = null;
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
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao Salvar dados");
                    }

                    CadProtheusBLL bll = new CadProtheusBLL();

                    bll.GetProcessosGerados(cpt.COD_CARGA_TIPO, DateTime.Parse(txtDatPagaVenc.Text.ToString()), newobj.DTH_INCLUSAO);

                    CadProtheusBLL objLote = new CadProtheusBLL();

                    DataTable dtLote = bll.RetornaNumeroLote(user.login.ToString(), newobj.DTH_INCLUSAO);

                    if (!objLote.ValidaMedctr(Convert.ToInt32(dtLote.Rows[0][0].ToString())))
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao criar processo, movimento não está registrado com as informações solicitadas");
                    }

                    atualizaGrid(area);

                }
                else if (cp.COD_CARGA_PROTHEUS == 42)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Arquivos gravados com sucesso!");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "O arquivo já esta na fila para ser gravado!");
                }

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
                    Div21.Visible = false;
                    Div27.Visible = false;
                    Div20.Visible = true;
                    DivData.Visible = false;
                }
                else if (ddlProtheus.Text == "27")
                {
                    Div21.Visible = false;
                    Div20.Visible = false;
                    Div27.Visible = true;
                    DivData.Visible = false;
                }
                else if (ddlProtheus.Text == "31")
                {
                    Div20.Visible = false;
                    Div27.Visible = false;
                    Div21.Visible = true;
                    DivData.Visible = true;
                }

                else
                {
                    Div21.Visible = false;
                    Div20.Visible = false;
                    Div27.Visible = false;
                    DivPrincipal.Visible = true;
                    DivData.Visible = true;
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

        protected void btnGeraRel_Click(object sender, EventArgs e)
        {

        }

        protected void grdFilaProcesso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CadProtheusBLL bll = new CadProtheusBLL();


            var user = (ConectaAD)Session["objUser"];

            string area = (user != null) ? user.departamento.Replace("Gerência - ", "") : "Desenv";

            GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int indexLinha = gvr.RowIndex;
            string tipoProcesso = "";
            

            DateTime dtInclusao = Convert.ToDateTime(((Label)grdFilaProcesso.Rows[indexLinha].FindControl("lblDataInclusao")).Text);
            DataTable dt = bll.RetornaNumeroLote(user.login.ToString(), dtInclusao);

            if (e.CommandName == "Gerar")
            {
                DataSet ds = new DataSet();

              //  tipoProcesso = dt.Rows[0][2].ToString().Trim();


                //if (dt.Rows[0][1].ToString() == "23")
                //{
                //    tipoProcesso = "Devolução da Saúde ao Participante";
                //}
                //else if (dt.Rows[0][1].ToString() == "12")
                //{
                //    tipoProcesso = "Resgate de Cotas";
                //}
                //else if (dt.Rows[0][1].ToString() == "14")
                //{
                //    tipoProcesso = "Repasse Saúde - Ativos";
                //}

                if (dt.Rows[0][1].ToString() != "24")
                {
                    ds = bll.GeraRelGerais(Convert.ToInt32(dt.Rows[0][0].ToString()));

                }
                else
                {
                    ds = bll.GeraRelRedeCredenciada(Convert.ToInt32(dt.Rows[0][0].ToString()));

                }

                //Gera os arquivos sem pontuacao, verificar possiveis nomenclaturas na proxima atualizacao
                ArquivoDownload adMedAberta = new ArquivoDownload();
                adMedAberta.nome_arquivo = "MEDCTRAberta.xlsx";
                adMedAberta.dados = ds.Tables[0];
                Session[ValidaCaracteres(adMedAberta.nome_arquivo)] = adMedAberta;
                string fullMedAberta = "WebFile.aspx?dwFile=" + ValidaCaracteres(adMedAberta.nome_arquivo);
                AdicionarAcesso(fullMedAberta);
                AbrirNovaAba(upUpdatePanel, fullMedAberta, adMedAberta.nome_arquivo);

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
            else if (e.CommandName == "Validar")
            {
                if (DateTime.Now.Hour > 11 && DateTime.Now.Hour < 14)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Não é possível validar nesse momento, a carga do Protheus está em processamento");

                }
                else
                {
                    bll.ValidaLote(Convert.ToInt32(dt.Rows[0][0].ToString()));
                    atualizaGrid(area);
                }



            }
            else if (e.CommandName == "Excluir")
            {
                bll.ExcluiLote(Convert.ToInt32(dt.Rows[0][0].ToString()));
                atualizaGrid(area);
            }
        }

        #region Métodos

        protected void atualizaGrid(string area)
        {
            CadProtheusBLL objBll = new CadProtheusBLL();
            var user = (ConectaAD)Session["objUser"];

            grdFilaProcesso.DataSource = objBll.GeraGridProcesso(area); ;
            grdFilaProcesso.DataBind();




        }

        protected void grdFilaProcesso_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var status = (Label)e.Row.FindControl("lblStatusProcesso");
                if (status.Text == "Aguardando validação")
                {
                    for (int i = 0; i <= 7; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromArgb(204, 229, 255);

                    }

                }
                else
                {
                    for (int i = 0; i <= 7; i++)
                    {
                        e.Row.Cells[i].BackColor = Color.FromArgb(255, 255, 255);
                    }
                }

            }
        }
        #endregion




    }
}