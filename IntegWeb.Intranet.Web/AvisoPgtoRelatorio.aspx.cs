using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IntegWeb.Framework;
using Intranet.Aplicacao.BLL;
using IntegWeb.Entidades.Previdencia.Pagamentos;
using IntegWeb.Entidades.Framework;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Data.SqlClient;

namespace IntegWeb.Intranet.Web
{
    public partial class AvisoPgtoRelatorio : BasePage
    {
        string eMailPart;
        string anoMesRef;
        string tipoMesAbono;
        string nome;

        //Variáveis de Session para Controle de Dispose de ReportDocument
        // //////////////////////////////////////////////////////////////////
        private ReportDocument relatorioSession
        {
            get { return (ReportDocument)Session["report"]; }
            set { Session["report"] = value; }
        }

        private List<ReportDocument> listaRelatorioSession
        {
            get { return (List<ReportDocument>)Session["reportList"]; }
            set { Session["reportList"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            ParameterFields parametros = new ParameterFields();

            //1=Visualizar
            //2=Gerar PDF
            string tipo = Request.QueryString["tipo"];


            if (tipo == "1" || tipo == "2")
            {
                //Instancia de Relatório único para PDF
                //Armazenado em Session para posterior Dispose()
                relatorioSession = new ReportDocument();

                anoMesRef = Request.QueryString["hidANO_REFERENCIA"];
                tipoMesAbono = Request.QueryString["hidasabono"];

                parametros = MontaParametros(anoMesRef, tipoMesAbono);

                CrystalReportViewer1.ReportSource = null;

                relatorioSession = MontaCrystal(parametros);

                if (tipo == "1")
                {
                    ExportaCrystalRpt(relatorioSession, parametros, "AvisoPagamentoAbono.rpt");
                }
                else
                {
                    VisualizarPdf(relatorioSession, parametros, "AvisoPagamentoAbono.rpt");
                }

            }
            else if (tipo == "3")
            {
                eMailPart = Request.QueryString["emailPart"];
                anoMesRef = Request.QueryString["hidANO_REFERENCIA"];
                tipoMesAbono = Request.QueryString["hidasabono"];

                string[] vetorAnoMesRef = anoMesRef.Split('|');
                string[] vetorTipoMesAbono = tipoMesAbono.Split('|');

                //Instancia de Lista de Relatórios para envio por E-mail
                //Armazenado em Session para posterior Dispose() de todos os Reports criados
                listaRelatorioSession = new List<ReportDocument>();

                for (int i = 0; i < vetorAnoMesRef.Length; i++)
                {
                    //for (int j = 0; j < vetorTipoMesAbono.Length; j++)
                    //{
                    if (!string.IsNullOrEmpty(vetorAnoMesRef[i]))
                    {
                        parametros = MontaParametros(vetorAnoMesRef[i], vetorTipoMesAbono[i]);

                        // }
                        listaRelatorioSession.Add(MontaCrystal(parametros));
                    }

                }

                //EnviaEmail(relatorios, eMailPart);
                EnviaEmail(listaRelatorioSession, eMailPart);
                Response.Write("<br/><div align=center><h4>Mensagem enviada para  " + eMailPart + " às " + DateTime.Now.ToString() + " com sucesso!</h4></div><br/><br/>");

            }
            else
            {
                Response.Write("Ocorreu um erro!");
            }
        }




        private ParameterFields MontaParametros(string anoMesRef, string tipoMesAbono)
        {
            pagamentos objparametros1 = new pagamentos();

            objparametros1.AVISO_COD_EMPRS = Request.QueryString["hidCOD_EMPRS"];
            objparametros1.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["hidNUM_RGTRO_EMPRG"];
            objparametros1.AVISO_NUM_IDNTF_RPTANT = Request.QueryString["hidNUM_IDNTF_RPTANT"];
            objparametros1.AVISO_NUM_IDNTF_DPDTE = "0";//Request.QueryString["hidNUM_IDNTF_DPDTE"];
            objparametros1.AVISO_ANO_REFERENCIA = anoMesRef;
            objparametros1.Aviso_asabono = tipoMesAbono; //Request.QueryString["hidasabono"];
            objparametros1.Aviso_asquadro = "1";

            Retorno_Aviso_pagto_ms_ab varRetornoAbono = null;

            pagamentosBLL bll = new pagamentosBLL();

            string mensagem = "";


            //List<pagamentos> ListaResultado1 = bll.ConsultarQtde(out mensagem, objparametros);

            varRetornoAbono = bll.ConsultarQtde(out mensagem, objparametros1.AVISO_COD_EMPRS,
                                                objparametros1.AVISO_NUM_RGTRO_EMPRG,
                                                objparametros1.AVISO_NUM_IDNTF_RPTANT,
                                                objparametros1.AVISO_NUM_IDNTF_DPDTE,
                                                objparametros1.AVISO_ANO_REFERENCIA,
                                                objparametros1.Aviso_asabono,
                                                objparametros1.Aviso_asquadro);

            List<pagamentos> ListaResultado1 = varRetornoAbono.pagamentos;

            string nomeAviso = "";

            // Descrição do tipo de Aviso de Pagamento
            if (varRetornoAbono.astipoaviso == "ABONO" && objparametros1.Aviso_asabono != "N")
            {
                nomeAviso = "Aviso de Pagamento - Abono Anual";
            }
            else if (varRetornoAbono.astipoaviso == "ADIANTAMENTO ABONO" && objparametros1.Aviso_asabono != "N")
            {
                nomeAviso = "Aviso de Pagamento - Adiantamento Abono Anual";
            }
            else
            {
                nomeAviso = "Aviso de Pagamento Mensal";
            }



            ParameterFields paramFields = new ParameterFields();
            ParameterField pField;
            ParameterDiscreteValue dcItemYr;

            //AVISO_NOM_EMPRG
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_NOM_EMPRG";
            dcItemYr.Value = ListaResultado1[0].AVISO_NOM_EMPRG;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            this.nome = ListaResultado1[0].AVISO_NOM_EMPRG;
            //relatorio.SetParameterValue(pField.ParameterFieldName, dcItemYr.Value);
            //
            //AVISO_DCR_PLBNF
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_DCR_PLBNF";
            dcItemYr.Value = ListaResultado1[0].AVISO_DCR_PLBNF;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //
            //AVISO_NOM_RZSOC_EMPRS
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_NOM_RZSOC_EMPRS";
            dcItemYr.Value = ListaResultado1[0].AVISO_NOM_RZSOC_EMPRS;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_NOM_RZSOC_BANCO
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_NOM_RZSOC_BANCO";
            dcItemYr.Value = ListaResultado1[0].AVISO_NOM_RZSOC_BANCO;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_NOM_AGBCO
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_NOM_AGBCO";
            dcItemYr.Value = ListaResultado1[0].AVISO_NOM_AGBCO;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_NOM_AGBCO
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_DAT_PAGTO_PCPGBF";
            dcItemYr.Value = ListaResultado1[0].AVISO_DAT_PAGTO_PCPGBF;
            //dcItemYr.Value = Convert.ToDateTime(ListaResultado1[0].AVISO_DAT_PAGTO_PCPGBF);
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //CONTA_TIPO
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "CONTA_TIPO";
            dcItemYr.Value = ListaResultado1[0].AVISO_TIP_CTCOR_HISCAD + "/ " + ListaResultado1[0].AVISO_NUM_CTCOR_HISCAD;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_MES_REFERENCIA
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "ANO_MES_REFERENCIA";
            dcItemYr.Value = ListaResultado1[0].AVISO_MES_REFERENCIA + "/" + ListaResultado1[0].AVISO_ANO_REFERENCIA;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_ADIANT_PREVIST
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_ADIANT_PREVIST";
            dcItemYr.Value = ListaResultado1[0].AVISO_ADIANT_PREVIST;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_TXTFIXO31
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_TXTFIXO31";
            dcItemYr.Value = ListaResultado1[0].AVISO_TXTFIXO31;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_TXTFIXO24
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_TXTFIXO24";
            dcItemYr.Value = ListaResultado1[0].AVISO_TXTFIXO24;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_TXTFIXO25
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_TXTFIXO25";
            dcItemYr.Value = ListaResultado1[0].AVISO_TXTFIXO25;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //


            //AVISO_RODAPE1
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_RODAPE1";
            dcItemYr.Value = ListaResultado1[0].AVISO_RODAPE1;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_RODAPE2
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_RODAPE2";
            dcItemYr.Value = ListaResultado1[0].AVISO_RODAPE2;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //AVISO_RODAPE3
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "AVISO_RODAPE3";
            dcItemYr.Value = ListaResultado1[0].AVISO_RODAPE3;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //


            //Empresa
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "ANCODEMPRS";
            dcItemYr.Value = objparametros1.AVISO_COD_EMPRS;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //Matrícula
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "ANNUMRGTROEMPRG";
            dcItemYr.Value = objparametros1.AVISO_NUM_RGTRO_EMPRG;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //NUM_IDNTF_RPTANT
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "ANNUMIDNTFRPTANT";
            dcItemYr.Value = objparametros1.AVISO_NUM_IDNTF_RPTANT;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //NUM_IDNTF_DPDTE
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "ANNUMIDNTFDPDTE";
            dcItemYr.Value = objparametros1.AVISO_NUM_IDNTF_DPDTE;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //ANO_REFERENCIA
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "ANANOMESREFER";
            dcItemYr.Value = objparametros1.AVISO_ANO_REFERENCIA;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //Abono
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "ASABONO";
            dcItemYr.Value = tipoMesAbono; //Request.QueryString["hidasabono"];
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);
            //

            //Quadro
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "ASQUADRO";
            dcItemYr.Value = "2";
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);

            //Nome tipo de Aviso de Pagamento
            pField = new ParameterField();
            dcItemYr = new ParameterDiscreteValue();
            pField.ParameterFieldName = "NomeTipoAviso";
            dcItemYr.Value = nomeAviso;
            pField.CurrentValues.Add(dcItemYr);
            paramFields.Add(pField);

            //if (tipoMesAbono == "S"){
            //    //Nome tipo de Aviso de Pagamento
            //    pField = new ParameterField();
            //    dcItemYr = new ParameterDiscreteValue();
            //    pField.ParameterFieldName = "NomeTipoAviso";
            //    dcItemYr.Value = "Aviso de Pagamento - Abono Anual";
            //    pField.CurrentValues.Add(dcItemYr);
            //    paramFields.Add(pField);
            //}else
            //        //Nome tipo de Aviso de Pagamento
            //    pField = new ParameterField();
            //    dcItemYr = new ParameterDiscreteValue();
            //    pField.ParameterFieldName = "NomeTipoAviso";
            //    dcItemYr.Value = "Aviso de Pagamento Mensal";
            //    pField.CurrentValues.Add(dcItemYr);
            //    paramFields.Add(pField);
            //}


            pagamentosBloco3 objparametros3 = new pagamentosBloco3();

            objparametros3.AVISO_COD_EMPRS = Request.QueryString["hidCOD_EMPRS"];
            objparametros3.AVISO_NUM_RGTRO_EMPRG = Request.QueryString["hidNUM_RGTRO_EMPRG"];
            objparametros3.AVISO_NUM_IDNTF_RPTANT = Request.QueryString["hidNUM_IDNTF_RPTANT"];
            objparametros3.AVISO_NUM_IDNTF_DPDTE = "0";//Request.QueryString["hidNUM_IDNTF_DPDTE"];
            objparametros3.AVISO_ANO_REFERENCIA = anoMesRef;
            objparametros3.AVISO_asabono = tipoMesAbono; // Request.QueryString["hidasabono"];
            objparametros3.AVISO_asquadro = "3";

            Retorno_Aviso_pagto_ms_ab varRetornoAbono3 = null;
            string mensagem3 = "";

            pagamentosBLL bll3 = new pagamentosBLL();

            varRetornoAbono3 = bll3.ConsultarQtde(out mensagem3, objparametros3.AVISO_COD_EMPRS,
                                                objparametros3.AVISO_NUM_RGTRO_EMPRG,
                                                objparametros3.AVISO_NUM_IDNTF_RPTANT,
                                                objparametros3.AVISO_NUM_IDNTF_DPDTE,
                                                objparametros3.AVISO_ANO_REFERENCIA,
                                                objparametros3.AVISO_asabono,
                                                objparametros3.AVISO_asquadro);

            List<pagamentosBloco3> ListaResultado3 = varRetornoAbono3.pagamentosbloco3;

            //List<pagamentosBloco3> ListaResultado3 = bll.ConsultarBloco3(objparametros3);

            if (ListaResultado3.Count > 0)
            {
                //DCR_VRBFSS
                pField = new ParameterField();
                dcItemYr = new ParameterDiscreteValue();
                pField.ParameterFieldName = "DCR_VRBFSS";
                dcItemYr.Value = ListaResultado3[0].AVISO_DRC_VRBFSS;
                pField.CurrentValues.Add(dcItemYr);
                paramFields.Add(pField);

                pField = new ParameterField();
                dcItemYr = new ParameterDiscreteValue();
                pField.ParameterFieldName = "AVISO_SLD_ANTERIOR";
                dcItemYr.Value = ListaResultado3[0].AVISO_SLD_ANTERIOR;
                pField.CurrentValues.Add(dcItemYr);
                paramFields.Add(pField);

                pField = new ParameterField();
                dcItemYr = new ParameterDiscreteValue();
                pField.ParameterFieldName = "AVISO_MOVTO_MES";
                dcItemYr.Value = ListaResultado3[0].AVISO_MOVTO_MES;
                pField.CurrentValues.Add(dcItemYr);
                paramFields.Add(pField);

                pField = new ParameterField();
                dcItemYr = new ParameterDiscreteValue();
                pField.ParameterFieldName = "AVISO_SLD_ATUAL";
                dcItemYr.Value = ListaResultado3[0].AVISO_SLD_ATUAL;
                pField.CurrentValues.Add(dcItemYr);
                paramFields.Add(pField);
            }
            else
            {
                //DCR_VRBFSS
                pField = new ParameterField();
                dcItemYr = new ParameterDiscreteValue();
                pField.ParameterFieldName = "DCR_VRBFSS";
                dcItemYr.Value = "";
                pField.CurrentValues.Add(dcItemYr);
                paramFields.Add(pField);

                pField = new ParameterField();
                dcItemYr = new ParameterDiscreteValue();
                pField.ParameterFieldName = "AVISO_SLD_ANTERIOR";
                dcItemYr.Value = "0";
                pField.CurrentValues.Add(dcItemYr);
                paramFields.Add(pField);

                pField = new ParameterField();
                dcItemYr = new ParameterDiscreteValue();
                pField.ParameterFieldName = "AVISO_MOVTO_MES";
                dcItemYr.Value = "0";
                pField.CurrentValues.Add(dcItemYr);
                paramFields.Add(pField);

                pField = new ParameterField();
                dcItemYr = new ParameterDiscreteValue();
                pField.ParameterFieldName = "AVISO_SLD_ATUAL";
                dcItemYr.Value = "0";
                pField.CurrentValues.Add(dcItemYr);
                paramFields.Add(pField);
            }

            return paramFields;
        }

        private ReportDocument MontaCrystal(ParameterFields parametros)
        {
            string caminho = Server.MapPath(@"Relatorios/AvisoPagamentoAbono.rpt");

            ReportDocument relatorio = new ReportDocument();
            //relatorio.Load(caminho);
            relatorio.FileName = caminho;

            SqlConnectionStringBuilder conString = new SqlConnectionStringBuilder(ConfigAplication.GetConnectString().ToString().Replace(";Unicode=True", ""));


            TableLogOnInfo tableLogOnInfo = null;


            foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in relatorio.Database.Tables)
            {

                tableLogOnInfo = tbl.LogOnInfo;
                tableLogOnInfo.ConnectionInfo.ServerName = conString.DataSource;
                tableLogOnInfo.ConnectionInfo.DatabaseName = "";
                tableLogOnInfo.ConnectionInfo.UserID = conString.UserID;
                tableLogOnInfo.ConnectionInfo.Password = conString.Password;
                tbl.ApplyLogOnInfo(tableLogOnInfo);

            }



            foreach (ReportDocument sub in relatorio.Subreports)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in sub.Database.Tables)
                {

                    tableLogOnInfo = tbl.LogOnInfo;
                    tableLogOnInfo.ConnectionInfo.ServerName = conString.DataSource;
                    tableLogOnInfo.ConnectionInfo.DatabaseName = "";
                    tableLogOnInfo.ConnectionInfo.UserID = conString.UserID;
                    tableLogOnInfo.ConnectionInfo.Password = conString.Password;
                    tbl.ApplyLogOnInfo(tableLogOnInfo);

                }
            }



            relatorio.SetDatabaseLogon(conString.UserID, conString.Password, conString.DataSource, "");

            //Parâmetros
            foreach (ParameterField pField in parametros)
            {
                relatorio.SetParameterValue(pField.Name, ((ParameterDiscreteValue)(pField.CurrentValues[0])).Value);
            }

            return relatorio;


        }


        private void EnviaEmail(List<ReportDocument> relatorios, string emailPara)
        {
            // DE
            string emailRemetente = "Atendimento Funcesp <atendimento@funcesp.com.br>";

            // ASSUNTO
            string emailAssunto = "Aviso de Pagamento - " + ((ParameterDiscreteValue)(relatorios[0].ParameterFields["AVISO_NOM_EMPRG"].CurrentValues[0])).Value;
            // MENSAGEM


            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            string str_periodo_dia = "";

            if (horarioAtual < periodo_dia)
                str_periodo_dia = "Bom Dia.";
            else
                str_periodo_dia = "Boa Tarde.";

            string emailCorpo = "<p style='font-family:Arial, Helvetica, sans-serif; font-size:12px'>Sr(a). " +
               ((ParameterDiscreteValue)(relatorios[0].ParameterFields["AVISO_NOM_EMPRG"].CurrentValues[0])).Value + ", " +
               str_periodo_dia + "<br/><br/>" +

                "Em resposta a sua solicitação, anexamos a 2ª via do demonstrativo de pagamento." + "<br/><br/>" +
                "Para obter o <strong>demonstrativo de pagamento</strong>, orientamos a acessar na área restrita do portal (www.funcesp.com.br): Previdência / aviso Pagamento de Benefícios." + "<br/><br/>" +
                "Faça seu login com CPF e senha pessoal. Caso tenha esquecido a sua senha e já tenha e-mail cadastrado na Funcesp, clique no botão ‘Recuperar Senha’. Se não tiver e-mail cadastrado, entre em contato com o Disque-Fundação para obter a senha: 11. 3065 3000 ou 0800 012 7173. <br/><br/></p>";


            using (var message = new MailMessage(emailRemetente, emailPara, emailAssunto, emailCorpo))
            {
                //ANEXOS
                foreach (ReportDocument relatorio in relatorios)
                {

                    string nome_abono = "";
                    string ASABONO = ((ParameterDiscreteValue)(relatorio.ParameterFields["ASABONO"].CurrentValues[0])).Value.ToString();

                    if (ASABONO == "S")
                    {
                        nome_abono = "ABONO";
                    }

                    //Exportar para Stream
                    System.IO.Stream pdfMemoria = relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    message.Attachments.Add(new Attachment(pdfMemoria, "DemonstrativoPagto_" + nome_abono + "_" +
                        ((ParameterDiscreteValue)(relatorio.ParameterFields["ANO_MES_REFERENCIA"].CurrentValues[0])).Value + ".pdf"));
                    //emailCorpo = emailCorpo + ((ParameterDiscreteValue)(relatorio.ParameterFields["ANO_MES_REFERENCIA"].CurrentValues[0])).Value + "\n";
                }

                //emailCorpo = emailCorpo.Substring(0, emailCorpo.Length-2);
                //emailCorpo = emailCorpo + "\n\n" + "Atenciosamente," + "\n" + "Atendimento ao Cliente \n\n";
                //----------------------------------------------------------------------
                //---------------------------------------------------------------------
                var contentID = "Image";
                var inlineLogo = new Attachment(Server.MapPath("img/assinatura_email.jpg"));

                //---------------------------------------------------------------------
                // Ambiente de Produção
                //var inlineLogo = new Attachment(@"D:\\avisopagto\img\assinatura_email.jpg");

                //----------------------------------------------------------------------
                //---------------------------------------------------------------------

                inlineLogo.ContentId = contentID;
                inlineLogo.ContentDisposition.Inline = true;
                inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                message.Attachments.Add(inlineLogo);

                emailCorpo = emailCorpo + "<img src=\"cid:" + contentID + "\">";

                //---------------------------------------------------------------------
                //---------------------------------------------------------------------

                message.IsBodyHtml = true;
                message.Body = emailCorpo;

                // ENVIAR COM CÓPIA
                // MailAddress copy = new MailAddress("");
                // message.CC.Add(copy);

                // ENVIAR COM COPIA OCULTA
                // MailAddress bcc = new MailAddress("");
                // message.Bcc.Add(bcc);

                new Email().EnviaEmailMensagem(message);
            }
        }

        public void ExportaCrystalRpt(ReportDocument relatorio, ParameterFields paramFields, string caminho)
        {
            CrystalReportViewer1.ReportSource = relatorio;
            CrystalReportViewer1.ParameterFieldInfo = paramFields;
            //CrystalReportViewer1.ReportSource 
            //CrystalReportSource2.Report.FileName = Server.MapPath(@caminho);

            CrystalReportViewer1.Visible = true;


        }

        public void VisualizarPdf(ReportDocument relatorio, ParameterFields paramFields, string caminho)
        {

            CrystalReportViewer1.ReportSource = relatorio;
            CrystalReportViewer1.ParameterFieldInfo = paramFields;

            ArquivoDownload adExtratoPdf = new ArquivoDownload();
            adExtratoPdf.nome_arquivo = "Aviso_pagto" + DateTime.Now.ToFileTime() + ".pdf";
            adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adExtratoPdf.nome_arquivo;
            adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = adExtratoPdf.caminho_arquivo;
            CrExportOptions = relatorio.ExportOptions;//Report document  object has to be given here
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;

            relatorio.Export();

            Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
            string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
            AdicionarAcesso(fullUrl);
            //AbrirNovaAba(this, fullUrl, adExtratoPdf.nome_arquivo);
            Response.Redirect(fullUrl);
            //((BasePage)this.Page).ResponsePdf(adExtratoPDF.caminho_arquivo, adExtratoPDF.nome_arquivo);
            //ResponsePdf(adExtratoPDF.caminho_arquivo, adExtratoPDF.nome_arquivo);

        }


        protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
        {
            //base.Dispose();

            //Dispose do unico relatório PDF


            if (relatorioSession != null)
            {
                relatorioSession.Close();
                relatorioSession.Dispose();
                relatorioSession = null;
            }

            //Dispose dos N Reports da lista de relatórios enviados por e-mail
            if (listaRelatorioSession != null)
            {
                foreach (ReportDocument relatorio in listaRelatorioSession)
                {
                    relatorio.Close();
                    relatorio.Dispose();
                    //listaRelatorioSession.Remove(relatorio);
                }

                listaRelatorioSession = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}