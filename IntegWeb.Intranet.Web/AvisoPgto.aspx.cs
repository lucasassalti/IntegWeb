using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Intranet.Aplicacao.BLL;
using IntegWeb.Entidades.Previdencia.Pagamentos;
using System.Globalization;

namespace IntegWeb.Intranet.Web
{

    public partial class AvisoPgto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Obter_dataMax("");

                if (Request.QueryString["ParticipanteEmail"] == "undefined")
                {
                    emailPart.Text = "";
                }
                else
                {
                    emailPart.Text = Request.QueryString["ParticipanteEmail"];
                }

            }

        }


        public void Obter_dataMax(String filtro)
        {
            string varRepresentante;
            if (Request.QueryString["nrepr"] == "")
            {
                varRepresentante = "0";
            }
            else
            {
                varRepresentante = Request.QueryString["nrepr"];
            }

            if (String.IsNullOrEmpty(Request.QueryString["nempr"])) return;

            pagamentosBLL bll = new pagamentosBLL();

            //objparametros.Aviso_asquadro = 2;
            //objparametros.Aviso_asabono = S

            string resultado = "0";

            resultado = bll.DataMax(Request.QueryString["nempr"].ToString(), Request.QueryString["nreg"].ToString(), varRepresentante);

            //var resultado = command.ExecuteOracleScalar().ToString();

            if (resultado == "0")
            {
                ClientScript.RegisterStartupScript(
                     Type.GetType("System.String"),
                     "msg",
                     "alert('Nenhum aviso de pagamento foi encontrado.'); fechaTela();",
                     true);

                resAbono.Text = "Nenhum aviso de pagamento foi encontrado.";

                DivErro.Visible = true;
                DivConteudo.Visible = false;
                DivConteudoAbono.Visible = false;
            }
            else
            {
                DivErro.Visible = false;
                DivConteudo.Visible = true;
                DivConteudoAbono.Visible = false;

                dataMaximaRef.Value = resultado;
                ConsultarAbono();
            }

        }

        private void ConsultarAbono()
        {
            string AnoMesAtualRef = dataMaximaRef.Value;
            HidMesAnoRef.Value = AnoMesAtualRef;

            pagamentos objparametros = new pagamentos();
            objparametros.AVISO_COD_EMPRS = Request.QueryString["nempr"];
            objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            objparametros.AVISO_NUM_IDNTF_RPTANT = string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
            objparametros.AVISO_NUM_IDNTF_DPDTE = HidNumDep.Value;
            objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];

            objparametros.Aviso_asabono = string.IsNullOrEmpty(Request.QueryString["hidasabono"]) ? hidasabono.Value : Request.QueryString["hidasabono"];
            objparametros.Aviso_asquadro = hidasquadro.Value;

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

            if (!string.IsNullOrEmpty(mensagem))
            {
                resAbono.Text = mensagem;

                DivErro.Visible = true;
                DivConteudo.Visible = false;
                DivConteudoAbono.Visible = false;
            }


            // nomeAviso.Text = varRetornoAbono.astipoaviso;

            List<pagamentos> LISTA1 = varRetornoAbono.pagamentos;

            if (varRetornoAbono.anqtdeaviso == 2)
            {
                DivConteudo.Visible = true;

                if (LISTA1.Count > 0)
                {
                    resNome.Text = LISTA1[0].AVISO_NOM_EMPRG;
                    rescodEmp.Text = LISTA1[0].AVISO_COD_EMPRS;
                    resnomEmp.Text = LISTA1[0].AVISO_NOM_RZSOC_EMPRS;
                    resNomPlano.Text = LISTA1[0].AVISO_DCR_PLBNF;
                    resNumMatr.Text = LISTA1[0].AVISO_NUM_RGTRO_EMPRG;

                    resMesAno.Text = LISTA1[0].AVISO_MES_REFERENCIA + "/" + LISTA1[0].AVISO_ANO_REFERENCIA;
                    resdataCred.Text = LISTA1[0].AVISO_DAT_PAGTO_PCPGBF;
                    resPagPrev.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LISTA1[0].AVISO_ADIANT_PREVIST);

                    resBanco.Text = LISTA1[0].AVISO_NOM_RZSOC_BANCO;
                    resAgencia.Text = LISTA1[0].AVISO_NOM_AGBCO;
                    resContatipo.Text = LISTA1[0].AVISO_TIP_CTCOR_HISCAD + "/ " + LISTA1[0].AVISO_NUM_CTCOR_HISCAD;

                    resTXTFIXO31.Text = LISTA1[0].AVISO_TXTFIXO31;

                    resTXTFIXO24.Text = LISTA1[0].AVISO_TXTFIXO24;
                    resTXTFIXO25.Text = LISTA1[0].AVISO_TXTFIXO25;

                    resRODAPE1.Text = LISTA1[0].AVISO_RODAPE1;
                    resRODAPE2.Text = LISTA1[0].AVISO_RODAPE2;
                    resRODAPE3.Text = LISTA1[0].AVISO_RODAPE3;

                    mesAtual.Text = LISTA1[0].AVISO_MES_REFERENCIA;


                    // Descrição do tipo de Aviso de Pagamento
                    if (varRetornoAbono.astipoaviso == "ABONO" && objparametros.Aviso_asabono != "N")
                    {
                        nomeAviso.Text = "Aviso de Pagamento - Abono Anual";
                    }
                    else if (varRetornoAbono.astipoaviso == "ADIANTAMENTO ABONO" && objparametros.Aviso_asabono != "N")
                    {
                        nomeAviso.Text = "Aviso de Pagamento - Adiantamento Abono Anual";
                    }
                    else
                    {
                        nomeAviso.Text = "Aviso de Pagamento Mensal";
                    }

                    NomeTipoAviso.Value = nomeAviso.Text;

                    ConsultarAbono2();
                    ConsultarAbono3();
                }
            }
            else
            {
                // quantidade aviso = 1
                if (LISTA1.Count > 0)
                {
                    resNome.Text = LISTA1[0].AVISO_NOM_EMPRG;
                    rescodEmp.Text = LISTA1[0].AVISO_COD_EMPRS;
                    resnomEmp.Text = LISTA1[0].AVISO_NOM_RZSOC_EMPRS;
                    resNomPlano.Text = LISTA1[0].AVISO_DCR_PLBNF;
                    resNumMatr.Text = LISTA1[0].AVISO_NUM_RGTRO_EMPRG;

                    resMesAno.Text = LISTA1[0].AVISO_MES_REFERENCIA + "/" + LISTA1[0].AVISO_ANO_REFERENCIA;
                    resdataCred.Text = LISTA1[0].AVISO_DAT_PAGTO_PCPGBF;
                    resPagPrev.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LISTA1[0].AVISO_ADIANT_PREVIST);

                    resBanco.Text = LISTA1[0].AVISO_NOM_RZSOC_BANCO;
                    resAgencia.Text = LISTA1[0].AVISO_NOM_AGBCO;
                    resContatipo.Text = LISTA1[0].AVISO_TIP_CTCOR_HISCAD + "/ " + LISTA1[0].AVISO_NUM_CTCOR_HISCAD;

                    resTXTFIXO31.Text = LISTA1[0].AVISO_TXTFIXO31;

                    resTXTFIXO24.Text = LISTA1[0].AVISO_TXTFIXO24;
                    resTXTFIXO25.Text = LISTA1[0].AVISO_TXTFIXO25;

                    resRODAPE1.Text = LISTA1[0].AVISO_RODAPE1;
                    resRODAPE2.Text = LISTA1[0].AVISO_RODAPE2;
                    resRODAPE3.Text = LISTA1[0].AVISO_RODAPE3;

                    mesAtual.Text = LISTA1[0].AVISO_MES_REFERENCIA;

                    // Descrição do tipo de Aviso de Pagamento
                    if (varRetornoAbono.astipoaviso == "ABONO")
                    {
                        nomeAviso.Text = "Aviso de Pagamento - Abono Anual";
                        DivMsgAdiantamento.Visible = false;
                    }
                    else if (varRetornoAbono.astipoaviso == "ADIANTAMENTO ABONO")
                    {
                        nomeAviso.Text = "Aviso de Pagamento - Adiantamento Abono Anual";
                        DivMsgAdiantamento.Visible = false;
                    }
                    else
                    {
                        nomeAviso.Text = "Aviso de Pagamento Mensal";
                        DivMsgAdiantamento.Visible = true;
                    }

                    NomeTipoAviso.Value = nomeAviso.Text;

                    ConsultarAbono2();
                    ConsultarAbono3();

                }

                DivConteudoAbono.Visible = false;
                DivConteudo.Visible = true;
            }
        }


        private void ConsultarAbono2()
        {
            string AnoMesAtualRef = dataMaximaRef.Value;
            HidMesAnoRef.Value = AnoMesAtualRef;

            pagamentos objparametros = new pagamentos();
            objparametros.AVISO_COD_EMPRS = Request.QueryString["nempr"];
            objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            objparametros.AVISO_NUM_IDNTF_RPTANT = string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
            objparametros.AVISO_NUM_IDNTF_DPDTE = HidNumDep.Value;
            objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];

            objparametros.Aviso_asabono = string.IsNullOrEmpty(Request.QueryString["hidasabono"]) ? hidasabono.Value : Request.QueryString["hidasabono"];
            objparametros.Aviso_asquadro = hidasquadro2.Value;

            Retorno_Aviso_pagto_ms_ab varRetornoAbono = null;

            string mensagem = "";

            pagamentosBLL bll = new pagamentosBLL();

            varRetornoAbono = bll.ConsultarQtde(out mensagem, objparametros.AVISO_COD_EMPRS,
                                                objparametros.AVISO_NUM_RGTRO_EMPRG,
                                                objparametros.AVISO_NUM_IDNTF_RPTANT,
                                                objparametros.AVISO_NUM_IDNTF_DPDTE,
                                                objparametros.AVISO_ANO_REFERENCIA,
                                                objparametros.Aviso_asabono,
                                                objparametros.Aviso_asquadro);

            // mensagem = bll.ConsultarQtde(out string mensagem, pagamentos objSistema);

            if (!string.IsNullOrEmpty(mensagem))
            {
                resAbono.Text = mensagem;
                DivErro.Visible = true;
                DivConteudo.Visible = false;
                DivConteudoAbono.Visible = false;
            }


            List<pagamentosBloco2> LIST = varRetornoAbono.pagamentosbloco2;

            if (LIST.Count > 0)
            {
                TXTTOTAIS1.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LIST[0].AVISO_TOT_VENCIMENTO);
                TXTTOTAIS2.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LIST[0].AVISO_TOT_DESCONTO);
                TXTTOTAIS3.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LIST[0].AVISO_TOT_LIQUIDO);
                resLiquido.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LIST[0].AVISO_TOT_LIQUIDO);


                grvBloco2.DataSource = LIST;
                grvBloco2.DataBind();


            }
        }



        private void ConsultarAbono3()
        {
            string AnoMesAtualRef = dataMaximaRef.Value;
            HidMesAnoRef.Value = AnoMesAtualRef;

            pagamentos objparametros = new pagamentos();
            objparametros.AVISO_COD_EMPRS = Request.QueryString["nempr"];
            objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            objparametros.AVISO_NUM_IDNTF_RPTANT = string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
            objparametros.AVISO_NUM_IDNTF_DPDTE = HidNumDep.Value;
            objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];

            objparametros.Aviso_asabono = string.IsNullOrEmpty(Request.QueryString["hidasabono"]) ? hidasabono.Value : Request.QueryString["hidasabono"];
            objparametros.Aviso_asquadro = hidasquadro3.Value;

            Retorno_Aviso_pagto_ms_ab varRetornoAbono = null;

            string mensagem = "";

            pagamentosBLL bll = new pagamentosBLL();

            varRetornoAbono = bll.ConsultarQtde(out mensagem, objparametros.AVISO_COD_EMPRS,
                                                objparametros.AVISO_NUM_RGTRO_EMPRG,
                                                objparametros.AVISO_NUM_IDNTF_RPTANT,
                                                objparametros.AVISO_NUM_IDNTF_DPDTE,
                                                objparametros.AVISO_ANO_REFERENCIA,
                                                objparametros.Aviso_asabono,
                                                objparametros.Aviso_asquadro);

            // mensagem = bll.ConsultarQtde(out string mensagem, pagamentos objSistema);

            if (!string.IsNullOrEmpty(mensagem))
            {
                resAbono.Text = mensagem;

                DivErro.Visible = true;
                DivConteudo.Visible = false;
                DivConteudoAbono.Visible = false;
            }

            List<pagamentosBloco3> ListaBloco3 = varRetornoAbono.pagamentosbloco3;

            if (ListaBloco3.Count > 0)
            {
                resHistbloco3.Text = ListaBloco3[0].AVISO_DRC_VRBFSS;

                resSaldobloco3.Text = ListaBloco3[0].AVISO_SLD_ANTERIOR.ToString("G");
                resMovbloco3.Text = String.Format(new CultureInfo("pt-BR"), "{0:G}", ListaBloco3[0].AVISO_MOVTO_MES);
                resSaldoAtualbloco3.Text = String.Format(new CultureInfo("pt-BR"), "{0:G}", ListaBloco3[0].AVISO_SLD_ATUAL);

                TableBloco3.Visible = true;
                refbloco1_3.Visible = true;
                refbloco2_3.Visible = true;
            }
            else
            {
                TableBloco3.Visible = false;
                refbloco1_3.Visible = false;
                refbloco2_3.Visible = false;
            }
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

            hidasabono.Value = "S";

            string AnoMesAtualRef = dataMaximaRef.Value;
            HidMesAnoRef.Value = AnoMesAtualRef;

            pagamentos objparametros = new pagamentos();
            objparametros.AVISO_COD_EMPRS = Request.QueryString["nempr"];
            objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            objparametros.AVISO_NUM_IDNTF_RPTANT = string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
            objparametros.AVISO_NUM_IDNTF_DPDTE = HidNumDep.Value;
            objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];

            objparametros.Aviso_asabono = hidasabono.Value;
            objparametros.Aviso_asquadro = hidasquadro.Value;

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

            if (!string.IsNullOrEmpty(mensagem))
            {
                resAbono.Text = mensagem;
                DivErro.Visible = true;
                DivConteudo.Visible = false;
                DivConteudoAbono.Visible = false;
            }



            List<pagamentos> LISTA1 = varRetornoAbono.pagamentos;

            if (LISTA1.Count > 0)
            {
                resNome.Text = LISTA1[0].AVISO_NOM_EMPRG;
                rescodEmp.Text = LISTA1[0].AVISO_COD_EMPRS;
                resnomEmp.Text = LISTA1[0].AVISO_NOM_RZSOC_EMPRS;
                resNomPlano.Text = LISTA1[0].AVISO_DCR_PLBNF;
                resNumMatr.Text = LISTA1[0].AVISO_NUM_RGTRO_EMPRG;

                resMesAno.Text = LISTA1[0].AVISO_MES_REFERENCIA + "/" + LISTA1[0].AVISO_ANO_REFERENCIA;
                resdataCred.Text = LISTA1[0].AVISO_DAT_PAGTO_PCPGBF;
                resPagPrev.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LISTA1[0].AVISO_ADIANT_PREVIST);

                resBanco.Text = LISTA1[0].AVISO_NOM_RZSOC_BANCO;
                resAgencia.Text = LISTA1[0].AVISO_NOM_AGBCO;
                resContatipo.Text = LISTA1[0].AVISO_TIP_CTCOR_HISCAD + "/ " + LISTA1[0].AVISO_NUM_CTCOR_HISCAD;

                resTXTFIXO31.Text = LISTA1[0].AVISO_TXTFIXO31;

                resTXTFIXO24.Text = LISTA1[0].AVISO_TXTFIXO24;
                resTXTFIXO25.Text = LISTA1[0].AVISO_TXTFIXO25;

                resRODAPE1.Text = LISTA1[0].AVISO_RODAPE1;
                resRODAPE2.Text = LISTA1[0].AVISO_RODAPE2;
                resRODAPE3.Text = LISTA1[0].AVISO_RODAPE3;

                mesAtual.Text = LISTA1[0].AVISO_MES_REFERENCIA;

                // Descrição do tipo de Aviso de Pagamento
                if (varRetornoAbono.astipoaviso == "ABONO" && objparametros.Aviso_asabono != "N")
                {
                    nomeAviso.Text = "Aviso de Pagamento - Abono Anual";
                    DivMsgAdiantamento.Visible = false;
                }
                else if (varRetornoAbono.astipoaviso == "ADIANTAMENTO ABONO" && objparametros.Aviso_asabono != "N")
                {
                    nomeAviso.Text = "Aviso de Pagamento - Adiantamento Abono Anual";
                    DivMsgAdiantamento.Visible = false;
                }
                else
                {
                    nomeAviso.Text = "Aviso de Pagamento Mensal";
                    DivMsgAdiantamento.Visible = true;
                }

                NomeTipoAviso.Value = nomeAviso.Text;

                ConsultarAbono2();
                ConsultarAbono3();
            }

            DivConteudoAbono.Visible = false;
            DivConteudo.Visible = true;

        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            hidasabono.Value = "N";

            string AnoMesAtualRef = dataMaximaRef.Value;
            HidMesAnoRef.Value = AnoMesAtualRef;

            pagamentos objparametros = new pagamentos();
            objparametros.AVISO_COD_EMPRS = Request.QueryString["nempr"];
            objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            objparametros.AVISO_NUM_IDNTF_RPTANT = string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
            objparametros.AVISO_NUM_IDNTF_DPDTE = HidNumDep.Value;
            objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];

            objparametros.Aviso_asabono = hidasabono.Value;
            objparametros.Aviso_asquadro = hidasquadro.Value;

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

            if (!string.IsNullOrEmpty(mensagem))
            {
                resAbono.Text = mensagem;
                DivErro.Visible = true;
                DivConteudo.Visible = false;
            }


            List<pagamentos> LISTA1 = varRetornoAbono.pagamentos;

            if (LISTA1.Count > 0)
            {
                resNome.Text = LISTA1[0].AVISO_NOM_EMPRG;
                rescodEmp.Text = LISTA1[0].AVISO_COD_EMPRS;
                resnomEmp.Text = LISTA1[0].AVISO_NOM_RZSOC_EMPRS;
                resNomPlano.Text = LISTA1[0].AVISO_DCR_PLBNF;
                resNumMatr.Text = LISTA1[0].AVISO_NUM_RGTRO_EMPRG;

                resMesAno.Text = LISTA1[0].AVISO_MES_REFERENCIA + "/" + LISTA1[0].AVISO_ANO_REFERENCIA;
                resdataCred.Text = LISTA1[0].AVISO_DAT_PAGTO_PCPGBF;
                resPagPrev.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LISTA1[0].AVISO_ADIANT_PREVIST);

                resBanco.Text = LISTA1[0].AVISO_NOM_RZSOC_BANCO;
                resAgencia.Text = LISTA1[0].AVISO_NOM_AGBCO;
                resContatipo.Text = LISTA1[0].AVISO_TIP_CTCOR_HISCAD + "/ " + LISTA1[0].AVISO_NUM_CTCOR_HISCAD;

                resTXTFIXO31.Text = LISTA1[0].AVISO_TXTFIXO31;

                resTXTFIXO24.Text = LISTA1[0].AVISO_TXTFIXO24;
                resTXTFIXO25.Text = LISTA1[0].AVISO_TXTFIXO25;

                resRODAPE1.Text = LISTA1[0].AVISO_RODAPE1;
                resRODAPE2.Text = LISTA1[0].AVISO_RODAPE2;
                resRODAPE3.Text = LISTA1[0].AVISO_RODAPE3;

                mesAtual.Text = LISTA1[0].AVISO_MES_REFERENCIA;

                // Descrição do tipo de Aviso de Pagamento
                if (varRetornoAbono.astipoaviso == "ABONO" && objparametros.Aviso_asabono != "N")
                {
                    nomeAviso.Text = "Aviso de Pagamento - Abono Anual";
                }
                else if (varRetornoAbono.astipoaviso == "ADIANTAMENTO ABONO" && objparametros.Aviso_asabono != "N")
                {
                    nomeAviso.Text = "Aviso de Pagamento - Adiantamento Abono Anual";
                }
                else
                {
                    nomeAviso.Text = "Aviso de Pagamento Mensal";
                }

                NomeTipoAviso.Value = nomeAviso.Text;

                ConsultarAbono2();
                ConsultarAbono3();
            }

            DivMsgAdiantamento.Visible = true;
            DivConteudoAbono.Visible = false;
            DivConteudo.Visible = true;

        }





        //private void ConsultarAviso()
        //{
        //    string AnoMesAtualRef = dataMaximaRef.Value;
        //    HidMesAnoRef.Value = AnoMesAtualRef;


        //    pagamentos objparametros = new Entidades.pagamentos();
        //    objparametros.AVISO_COD_EMPRS = Request.QueryString["nempr"];
        //    objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
        //    objparametros.AVISO_NUM_IDNTF_RPTANT = string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
        //    objparametros.AVISO_NUM_IDNTF_DPDTE = HidNumDep.Value;
        //    objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];

        //    objparametros.Aviso_asabono = hidasabono.Value;
        //    objparametros.Aviso_asquadro = hidasquadro.Value;

        //    pagamentosBLL bll = new pagamentosBLL();

        //    List<pagamentos> LISTA1 = bll.Consultar(objparametros);

        //    if (LISTA1.Count > 0)
        //    {
        //        resNome.Text = LISTA1[0].AVISO_NOM_EMPRG;
        //        rescodEmp.Text = LISTA1[0].AVISO_COD_EMPRS;
        //        resnomEmp.Text = LISTA1[0].AVISO_NOM_RZSOC_EMPRS;
        //        resNomPlano.Text = LISTA1[0].AVISO_DCR_PLBNF;
        //        resNumMatr.Text = LISTA1[0].AVISO_NUM_RGTRO_EMPRG;

        //        resMesAno.Text = LISTA1[0].AVISO_MES_REFERENCIA + "/" + LISTA1[0].AVISO_ANO_REFERENCIA;
        //        resdataCred.Text = LISTA1[0].AVISO_DAT_PAGTO_PCPGBF;
        //        resPagPrev.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LISTA1[0].AVISO_ADIANT_PREVIST);

        //        resBanco.Text = LISTA1[0].AVISO_NOM_RZSOC_BANCO;
        //        resAgencia.Text = LISTA1[0].AVISO_NOM_AGBCO;
        //        resContatipo.Text = LISTA1[0].AVISO_TIP_CTCOR_HISCAD + "/ " + LISTA1[0].AVISO_NUM_CTCOR_HISCAD;

        //        resTXTFIXO31.Text = LISTA1[0].AVISO_TXTFIXO31;

        //        resTXTFIXO24.Text = LISTA1[0].AVISO_TXTFIXO24;
        //        resTXTFIXO25.Text = LISTA1[0].AVISO_TXTFIXO25;

        //        resRODAPE1.Text = LISTA1[0].AVISO_RODAPE1;
        //        resRODAPE2.Text = LISTA1[0].AVISO_RODAPE2;
        //        resRODAPE3.Text = LISTA1[0].AVISO_RODAPE3;


        //    }

        //   var s = bll.Consultar(objparametros);

        //    ConsultarAviso2();
        //    ConsultarAviso3();
        //}

        //private void ConsultarAviso2()
        //{


        //    string AnoMesAtualRef = dataMaximaRef.Value;
        //    HidMesAnoRef.Value = AnoMesAtualRef;

        //    pagamentosBloco2 objparametros = new pagamentosBloco2();

        //    objparametros.AVISO_COD_EMPRS = Request.QueryString["nempr"];
        //    objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
        //    objparametros.AVISO_NUM_IDNTF_RPTANT = string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
        //    //objparametros.AVISO_NUM_IDNTF_DPDTE = string.IsNullOrEmpty(Request.QueryString["ndep"]) ? "0" : Request.QueryString["ndep"];
        //    objparametros.AVISO_NUM_IDNTF_DPDTE = HidNumDep.Value;
        //    objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];

        //    objparametros.Aviso_asabono = hidasabono.Value;
        //    objparametros.Aviso_asquadro = hidasquadro2.Value;

        //    pagamentosBLL bll = new pagamentosBLL();

        //    List<pagamentosBloco2> LIST = bll.ConsultarBloco2(objparametros);

        //    if (LIST.Count>0)
        //    {
        //        TXTTOTAIS1.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LIST[0].AVISO_TOT_VENCIMENTO);
        //        TXTTOTAIS2.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LIST[0].AVISO_TOT_DESCONTO);
        //        TXTTOTAIS3.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LIST[0].AVISO_TOT_LIQUIDO);
        //        resLiquido.Text = String.Format(new CultureInfo("pt-BR"), "{0:N}", LIST[0].AVISO_TOT_LIQUIDO);
        //    }

        //    grvBloco2.DataSource = LIST;
        //    grvBloco2.DataBind();


        //    var s = bll.ConsultarBloco2(objparametros);


        //}

        //private void ConsultarAviso3()
        //{
        //    string AnoMesAtualRef = dataMaximaRef.Value;
        //    HidMesAnoRef.Value = AnoMesAtualRef;

        //    pagamentosBloco3 objparametros = new pagamentosBloco3();

        //    objparametros.AVISO_COD_EMPRS = Request.QueryString["nempr"];
        //    objparametros.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
        //    objparametros.AVISO_NUM_IDNTF_RPTANT = string.IsNullOrEmpty(Request.QueryString["nrepr"]) ? "0" : Request.QueryString["nrepr"];
        //    objparametros.AVISO_NUM_IDNTF_DPDTE = HidNumDep.Value;
        //    objparametros.AVISO_ANO_REFERENCIA = string.IsNullOrEmpty(Request.QueryString["hidANO_REFERENCIA"]) ? AnoMesAtualRef : Request.QueryString["hidANO_REFERENCIA"];
        //    objparametros.AVISO_asabono = hidasabono.Value;
        //    objparametros.AVISO_asquadro = hidasquadro3.Value;

        //    pagamentosBLL bll = new pagamentosBLL();

        //    List<pagamentosBloco3> ListaBloco3 = bll.ConsultarBloco3(objparametros);

        //    if (ListaBloco3.Count > 0)
        //    {
        //        resHistbloco3.Text = ListaBloco3[0].AVISO_DRC_VRBFSS;

        //        resSaldobloco3.Text = ListaBloco3[0].AVISO_SLD_ANTERIOR.ToString("G");
        //        resMovbloco3.Text = String.Format(new CultureInfo("pt-BR"), "{0:G}", ListaBloco3[0].AVISO_MOVTO_MES);
        //        resSaldoAtualbloco3.Text = String.Format(new CultureInfo("pt-BR"), "{0:G}", ListaBloco3[0].AVISO_SLD_ATUAL);

        //        TableBloco3.Visible = true;
        //        refbloco1_3.Visible = true;
        //        refbloco2_3.Visible = true;
        //    }
        //    else{
        //        TableBloco3.Visible = false;
        //        refbloco1_3.Visible = false;
        //        refbloco2_3.Visible = false;
        //     }

        //    var s = bll.ConsultarBloco3(objparametros);


        //}

    }
}