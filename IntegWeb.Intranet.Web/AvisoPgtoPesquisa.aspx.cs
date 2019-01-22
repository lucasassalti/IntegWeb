using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Intranet.Aplicacao.BLL;
using IntegWeb.Entidades.Previdencia.Pagamentos;
using System.Data;
using System.Data.SqlClient;

namespace IntegWeb.Intranet.Web
{
    public partial class AvisoPgtoPesquisa : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                mesInicio.Items.Insert(0, new ListItem("---Selecione---", ""));
                mesFim.Items.Insert(0, new ListItem("---Selecione---", ""));
                anoInicio.Items.Insert(0, new ListItem("---Selecione---", ""));
                anoFim.Items.Insert(0, new ListItem("---Selecione---", ""));

                if (Request.QueryString["emailPart"] == "undefined")
                {
                    campoEmailPart.Text = "";
                }
                else
                {
                    campoEmailPart.Text = Request.QueryString["emailPart"];
                }

                /////////////////////////////////////////////////////// 
                //DataMax filtro = new DataMax();
                Obter_dataMax();


            }
        }

        public void Obter_dataMax()
        {

            //string anoAtual = DateTime.Now.Year;

            string varRepresentante;
            if (Request.QueryString["hidNUM_IDNTF_RPTANT"] == "")
            {
                varRepresentante = "0";
            }
            else
            {
                varRepresentante = Request.QueryString["hidNUM_IDNTF_RPTANT"];
            }

            if (String.IsNullOrEmpty(Request.QueryString["hidCOD_EMPRS"])) return;

            //String de conexao

            var resultado = Request.QueryString["hidANO_REFERENCIA"];

            //var mesMaximoAnt = "12";//resultado.Substring(4, 2);
            var anoMaximoAnt = DateTime.Now.Year.ToString();//resultado.Substring(0, 4);

            // DataTable dtPagamentos = bll.RetornarPgtos(Request.QueryString["hidCOD_EMPRS"].ToString(), Request.QueryString["hidNUM_RGTRO_EMPRG"].ToString(), varRepresentante);
            //DataView view = new DataView(dtPagamentos);
            //DataTable dtPagamentosdistinct = view.ToTable(true, "ano_referencia");

            pagamentosBLL bll = new pagamentosBLL();
            DataTable dtPagamentos = bll.RetornarPgtos(Request.QueryString["hidCOD_EMPRS"].ToString(), Request.QueryString["hidNUM_RGTRO_EMPRG"].ToString(), varRepresentante, "01", "12", anoMaximoAnt, anoMaximoAnt);

            var asQuery = (from m in dtPagamentos.AsEnumerable()
                           orderby m.Field<decimal>("ano_referencia") ascending
                           select new
                           {
                               mesAnoref = m.Field<string>("mesAnoref") + " " + m.Field<decimal>("ano_referencia").ToString(),
                               REFERENCIA = m.Field<string>("REFERENCIA") 
                               //ANO_REFERENCIA = m.Field<decimal>("ano_referencia").ToString()
                           });

            grdResultadoTr.DataSourceID = null;
            grdResultadoTr.DataSource = asQuery.ToList();
            grdResultadoTr.DataBind();

            anoInicio.SelectedValue = DateTime.Now.Year.ToString(); // resultado.Substring(0, 4);
            mesInicio.SelectedValue = "01";//resultado.Substring(4, 2);
            anoFim.SelectedValue = DateTime.Now.Year.ToString();//resultado.Substring(0, 4);
            mesFim.SelectedValue = "12";//resultado.Substring(4, 2);

            DataTable dt = new DataTable();

            dt = dtPagamentos;

            foreach (DataRow dr in dt.Rows)
            {
                DataCompleta.Value = dr["referencia"].ToString();

            }
            //mesFim.SelectedIndex = mesFim.Items.Count - 1;

            //var resultado = Request.QueryString["hidANO_REFERENCIA"];

            dataMaxima.Value = resultado.Substring(4, 2);
            anoMaximo.Value = resultado.Substring(0, 4);

            ConsultarAbono();
            //connection.Close();

        }

        private void ConsultarAbono()
        {
            string AnoMesAtualRef = DataCompleta.Value;

            pagamentos objparametros = new pagamentos();

            objparametros.AVISO_COD_EMPRS = Request.QueryString["hidCOD_EMPRS"];
            objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["hidNUM_RGTRO_EMPRG"];
            objparametros.AVISO_NUM_IDNTF_RPTANT = Request.QueryString["hidNUM_IDNTF_RPTANT"];
            objparametros.AVISO_NUM_IDNTF_DPDTE = "0";
            objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];

            objparametros.Aviso_asabono = Request.QueryString["hidasabono"];
            objparametros.Aviso_asquadro = Request.QueryString["hidasquadro"];

            Retorno_Aviso_pagto_ms_ab varRetornoAbono = null;

            string mensagem = "";

            pagamentosBLL bll = new pagamentosBLL();

            //objparametros.Aviso_asquadro = 2;
            //objparametros.Aviso_asabono = S

            varRetornoAbono = bll.ConsultarQtde(out mensagem,
                                                objparametros.AVISO_COD_EMPRS,
                                                objparametros.AVISO_NUM_RGTRO_EMPRG,
                                                objparametros.AVISO_NUM_IDNTF_RPTANT,
                                                objparametros.AVISO_NUM_IDNTF_DPDTE,
                                                objparametros.AVISO_ANO_REFERENCIA,
                                                objparametros.Aviso_asabono,
                                                objparametros.Aviso_asquadro);

            // nomeAviso.Text = varRetornoAbono.astipoaviso;

            List<pagamentos> LISTA1 = varRetornoAbono.pagamentos;

            if (LISTA1.Count > 0)
            {
                resNome.Text = LISTA1[0].AVISO_NOM_EMPRG;
                rescodEmp.Text = LISTA1[0].AVISO_COD_EMPRS;
                resnomEmp.Text = LISTA1[0].AVISO_NOM_RZSOC_EMPRS;
                resNomPlano.Text = LISTA1[0].AVISO_DCR_PLBNF;
                resNumMatr.Text = LISTA1[0].AVISO_NUM_RGTRO_EMPRG;
            }
        }

        /// <summary>
        /// Valida as data antes de pesquisar o conteudo
        /// </summary>
        string validarDatas()
        {
            if (mesInicio.SelectedIndex == 0 || anoInicio.SelectedIndex == 0 || mesFim.SelectedIndex == 0 || anoFim.SelectedIndex == 0)
            {
                MostraMensagemTela(this.Page, "Favor preencher o Filtro do período");
                return "erro";
            }

            if (Convert.ToInt16(anoFim.SelectedValue) < Convert.ToInt16(anoInicio.SelectedValue))
            {
                MostraMensagemTela(this.Page, "A data ano fim não pode ser menor que a data ano Inicio!");
                return "erro";
            }

            //Validação de ano final menor que inicial
            if (Convert.ToInt16(mesFim.SelectedValue) < Convert.ToInt16(mesInicio.SelectedValue))
            {
                if (! (Convert.ToInt16(anoFim.SelectedValue) > Convert.ToInt16(anoInicio.SelectedValue) ) )
                {
                    MostraMensagemTela(this.Page, "O mes fim não pode ser menor que o mes Inicio, favor verificar o filtro selecionado!");
                    return "erro";
                }
            }

            return "ok";
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string varRepresentante;
            if (Request.QueryString["hidNUM_IDNTF_RPTANT"] == "")
            {
                varRepresentante = "0";
            }
            else
            {
                varRepresentante = Request.QueryString["hidNUM_IDNTF_RPTANT"];
            }

            if (String.IsNullOrEmpty(Request.QueryString["hidCOD_EMPRS"])) return;


            var erroM = validarDatas();
            if (erroM != "erro")
            {
                pagamentosBLL bll = new pagamentosBLL();
                DataTable dtPagamentos = bll.RetornarPgtos(Request.QueryString["hidCOD_EMPRS"].ToString(), Request.QueryString["hidNUM_RGTRO_EMPRG"].ToString(), varRepresentante, mesInicio.SelectedValue, mesFim.SelectedValue, anoInicio.SelectedValue, anoFim.SelectedValue);

                DataTable dt = new DataTable();

                dt = dtPagamentos;

                var asQuery = (from m in dtPagamentos.AsEnumerable()
                               orderby m.Field<decimal>("ano_referencia") ascending
                               select new
                               {
                                   mesAnoref = m.Field<string>("mesAnoref") + " " + m.Field<decimal>("ano_referencia").ToString(),
                                   REFERENCIA = m.Field<string>("REFERENCIA")
                                   
                               });

                grdResultadoTr.DataSource = null;
                //grdResultadoTr.DataSource = dt;
                grdResultadoTr.DataSource = asQuery.ToList();
                grdResultadoTr.DataBind();

                foreach (DataRow dr in dt.Rows)
                {
                    DataCompleta.Value = dr["referencia"].ToString();
                }
                //mesFim.SelectedIndex = mesFim.Items.Count - 1;

                var resultado = Request.QueryString["hidANO_REFERENCIA"];

                dataMaxima.Value = resultado.Substring(4, 2);
                anoMaximo.Value = resultado.Substring(0, 4);

                //mesInicio.SelectedValue = resultado + Request.QueryString["hidasabono"].ToString();
                ConsultarAbono();
            }
        }

        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(campoEmailPart.Text.ToString()) || string.IsNullOrWhiteSpace(campoEmailPart.Text.ToString()))
            {
                MostraMensagemTela(this.Page, "Campo e-mail é obrigatório!");
                return;
            }
            string vetorAnoMesRef = "";
            string vetortipoMesAbono = "";

            foreach (GridViewRow row in grdResultadoTr.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelect = (row.FindControl("chkSelect") as CheckBox);

                    if (chkSelect.Checked == true)
                    {
                        var hf = row.FindControl("hfReferencia") as HiddenField;
                        var tipoMesAbono = "N";

                        if (hf.Value.Contains("N"))
                        {
                            //MENSAL
                            tipoMesAbono = "N";
                        }
                        else
                        {
                            //ABONO
                            tipoMesAbono = "S";
                        }

                        var anoMesReferencia = hf.Value.Replace("N", "").Replace("S", "");

                        vetorAnoMesRef += String.Concat("|", anoMesReferencia);
                        vetortipoMesAbono += String.Concat("|", tipoMesAbono.ToString());
                    }
                }
            }
            var EndEmailPart = campoEmailPart.Text;
            var tipo = "3";
            
            if (string.IsNullOrEmpty(vetorAnoMesRef))
            {
                MostraMensagemTela(this.Page, "Favor selecionar um periodo para que seja enviado");
                return;
            }

            var urlString = "~/AvisoPgtoRelatorio.aspx?hidCOD_EMPRS=" + Request.QueryString["hidCOD_EMPRS"] +
                            "&hidNUM_RGTRO_EMPRG=" + Request.QueryString["hidNUM_RGTRO_EMPRG"] +
                            "&hidNUM_IDNTF_RPTANT=" + Request.QueryString["hidNUM_IDNTF_RPTANT"] +
                            "&hidNUM_IDNTF_DPDTE=" + Request.QueryString["hidNUM_IDNTF_DPDTE"] +
                            "&hidANO_REFERENCIA=" + vetorAnoMesRef + "&hidasabono=" + vetortipoMesAbono + "&hidasquadro=" + Request.QueryString["hidasquadro"] +
                            "&tipo=" + tipo + "&emailPart=" + EndEmailPart + "&NomeTipoAviso=" + Request.QueryString["NomeTipoAviso"];

            Response.Redirect(urlString);
        }

        protected void grdResultadoTr_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Visualizar")
            {
                var selecionado = e.CommandArgument.ToString();
                var selecionadoAno = e.CommandArgument.ToString();

                var AnoRefSelecionado = selecionadoAno.Replace("N", "").Replace("S", "");
                var EndEmailPart = campoEmailPart.Text;
                var tipoPgto = "";

                if (selecionado.Contains("N"))
                {
                    //MENSAL
                    tipoPgto = "N";
                }
                else
                {
                    //ABONO
                    tipoPgto = "S";
                }

                var urlString = "~/AvisoPgto.aspx?nempr=" + Request.QueryString["hidCOD_EMPRS"] + "&nreg=" + Request.QueryString["hidNUM_RGTRO_EMPRG"] +
                    "&nrepr=" + Request.QueryString["hidNUM_IDNTF_RPTANT"] + "&ndep=" + Request.QueryString["hidNUM_IDNTF_DPDTE"] +
                    "&hidasabono=" + tipoPgto + "&hidasquadro=" + Request.QueryString["hidasquadro"] +
                    "&ParticipanteEmail=" + EndEmailPart + "&hidANO_REFERENCIA=" + AnoRefSelecionado + "&NomeTipoAviso=" + Request.QueryString["NomeTipoAviso"];

                Response.Redirect(urlString);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {

            anoInicio.SelectedIndex = 0;
            mesInicio.SelectedIndex = 0;
            anoFim.SelectedIndex = 0;
            mesFim.SelectedIndex = 0;

            grdResultadoTr.DataSource = null;
            grdResultadoTr.DataSourceID = null;
            grdResultadoTr.DataBind();
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdResultadoTr.HeaderRow.FindControl("chkSelectAll");

            foreach (GridViewRow row in grdResultadoTr.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSelect");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        //private void ConsultarAviso()
        //{
        //    string AnoMesAtualRef = DataCompleta.Value;


        //    pagamentos objparametros = new Entidades.pagamentos();

        //    objparametros.AVISO_COD_EMPRS = Request.QueryString["hidCOD_EMPRS"];
        //    objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["hidNUM_RGTRO_EMPRG"];
        //    objparametros.AVISO_NUM_IDNTF_RPTANT = Request.QueryString["hidNUM_IDNTF_RPTANT"];
        //    objparametros.AVISO_NUM_IDNTF_DPDTE = "0";//Request.QueryString["hidNUM_IDNTF_DPDTE"];
        //   // objparametros.AVISO_ANO_REFERENCIA = Request.QueryString["hidANO_REFERENCIA"];
        //    objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];


        //    objparametros.Aviso_asabono = Request.QueryString["hidasabono"];
        //    objparametros.Aviso_asquadro = Request.QueryString["hidasquadro"];

        //    pagamentosBLL bll = new pagamentosBLL();

        //    List<pagamentos> LISTA1 = bll.Consultar(objparametros);

        //    if (LISTA1.Count > 0)
        //    {
        //        resNome.Text = LISTA1[0].AVISO_NOM_EMPRG;
        //        rescodEmp.Text = LISTA1[0].AVISO_COD_EMPRS;
        //        resnomEmp.Text = LISTA1[0].AVISO_NOM_RZSOC_EMPRS;
        //        resNomPlano.Text = LISTA1[0].AVISO_DCR_PLBNF;
        //        resNumMatr.Text = LISTA1[0].AVISO_NUM_RGTRO_EMPRG;

        //    }

        //    var s = bll.Consultar(objparametros);
        //}

    }
}