using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.WS_QualiSign;

namespace IntegWeb.Saude.Aplicacao.BLL.Processos
{
    public class WsQualiSignBLL : WsQualiSignDAL
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public class documentoeletronico
        {
            private int codErroField;
            private string msgErroField;
            private string passcodeField;
            private object documentosField;
            /// <remarks/>
            public int CodErro
            {
                get { return this.codErroField; } set { this.codErroField = value; }
            }

            /// <remarks/>
            public string MsgErro
            {
                get { return this.msgErroField; } set { this.msgErroField = value; }
            }

            /// <remarks/>
            public object documentos
            {
                get { return this.documentosField; } set { this.documentosField = value; }
            }

            /// <remarks/>
            public string Passcode
            {
                get
                {
                    return this.passcodeField;
                }
                set
                {
                    this.passcodeField = value;
                }
            }
        }

        public class docEletronico
        {
            public string docto_referencia { get; set; }
            public string docto_status { get; set; }
            public string docto_validade { get; set; }
        }

        List<docEletronico> lstdocumentoeletronico = new List<docEletronico>();
        
        string xml_ListarDocumento = "<?xml version='1.0' encoding='ISO-8859-1' ?><documentoeletronico><sessao>{sessao}</sessao><documento><status>X</status></documento></documentoeletronico>";
        string xml_CadastroDOCUMENTO = "<?xml version='1.0' encoding='ISO-8859-1' ?><documentoeletronico><sessao>{sessao}</sessao><documentos><documento docto_nome_arquivo='{docto_nome_arquivo}' docto_referencia='{docto_referencia}' docto_dtinicio='{docto_dtinicio}' docto_dtfim='{docto_dtfim}' docto_fl_validade='{docto_fl_validade}' docto_cod_tipodoc='{docto_cod_tipodoc}' docto_cod_moeda='{docto_cod_moeda}' docto_sistema_codigo='{docto_sistema_codigo}'>{DOCUMENTO-BASE-64}<politica_assinatura docto_fl_bacen='{docto_fl_bacen}' docto_fl_ass_digital='{docto_fl_ass_digital}' docto_fl_grava_docto='{docto_fl_grava_docto}' docto_fl_ass_portal='{docto_fl_ass_portal}' docto_fl_car_tempo='{docto_fl_car_tempo}'/><seguranca_documento docto_dtmanter_sist='{docto_dtmanter_sist}' docto_cod_tipo_acesso='{docto_cod_tipo_acesso}'/><partes><parte parte_papel_nome='{parte_papel_nome}' parte_empr_cnpj='{parte_empr_cnpj}' parte_empr_ordem='{parte_empr_ordem}'><representantes><representante repr_cont_cpf='{repr_cont_cpf}' repr_cont_ordem='1' repr_somente_acomp='N' /></representantes></parte></partes></documento></documentos></documentoeletronico>";

        //public Resultado ProcessarDocumentos(string sessao,
        //                                    string docto_nome_arquivo,
        //                                    string docto_referencia,
        //                                    DateTime docto_data,
        //                                    string DOC_BASE_64,
        //                                    string ws_ambiente,
        //                                    out SAU_TBL_QUALISIGN qsQualiSign)
        //{

        public Resultado ProcessaDocumentos(List<ArquivoUpload> lst_doctos,
                                            string ws_sessao,
                                            string ws_ambiente,
                                            string CpfRepres,
                                            string pLOG_INCLUSAO,
                                            DateTime pDTH_INCLUSAO)
        {

            Resultado res = new Resultado();

            SAU_TBL_QUALISIGN qsQualiSign = new SAU_TBL_QUALISIGN();
            qsQualiSign.LOG_INCLUSAO = pLOG_INCLUSAO;
            qsQualiSign.DTH_INCLUSAO = pDTH_INCLUSAO;

            foreach (ArquivoUpload au in lst_doctos)
            {

                string DOCUMENTO_BASE_64 = Convert.ToBase64String(Util.File2Memory(au.caminho_arquivo));
                res = AutoCadastroDOCUMENTO(ws_sessao,
                                            au.nome_arquivo,
                                            au.nome_arquivo,
                                            CpfRepres,
                                            pDTH_INCLUSAO,
                                            DOCUMENTO_BASE_64,
                                            ws_ambiente);

                SAU_TBL_QUALISIGN_DOC qsDOC = new SAU_TBL_QUALISIGN_DOC();
                qsDOC.NOM_ARQUIVO = au.nome_arquivo;
                qsDOC.NOM_REF_ARQUIVO = au.nome_arquivo;
                qsDOC.COD_STATUS = "P";

                string[] ret = res.Mensagem.Split(';');
                Int16 CodRetorno = 0;
                if (!Int16.TryParse(ret[0], out CodRetorno))
                {
                    CodRetorno = 999;
                }
                qsDOC.COD_RETORNO = CodRetorno;
                qsDOC.DCR_RETORNO = ret[1];
                qsDOC.DCR_PASSCODE = ret[2];

                qsDOC.COD_STATUS = (qsDOC.COD_RETORNO==0) ? "P" : "R";

                au.processado = true;

                qsQualiSign.SAU_TBL_QUALISIGN_DOC.Add(qsDOC);
                res = base.GravaLoteDocumentos(qsQualiSign);

                //DataTable dt = ReadTextFile(ap._CAMINHO_COMPLETO_ARQUIVO, Encoding.GetEncoding("iso-8859-1"));
                //Resultado resDePara = apBLL.DePara(ap, dt, short.Parse(ddlAnoRef.SelectedValue), short.Parse(ddlMesRef.SelectedValue), (user != null) ? user.login : "DESENV");
            }

            return res;

        }

        public Resultado AutoCadastroDOCUMENTO(string sessao,
                                               string docto_nome_arquivo,
                                               string docto_referencia,
                                               string repr_cont_cpf,
                                               DateTime docto_data,
                                               string DOC_BASE_64,
                                               string ws_ambiente)
        {
            Resultado res = new Resultado();
            documentoeletronico ws_r = new documentoeletronico();

            string dt_docto = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {
                string xml_cd = xml_CadastroDOCUMENTO;
                xml_cd = xml_cd.Replace("{sessao}", sessao);
                xml_cd = xml_cd.Replace("{docto_nome_arquivo}", docto_nome_arquivo);
                xml_cd = xml_cd.Replace("{docto_referencia}", docto_referencia);
                xml_cd = xml_cd.Replace("{docto_dtinicio}", docto_data.ToString("dd/MM/yyyy"));
                xml_cd = xml_cd.Replace("{docto_dtfim}", docto_data.ToString("dd/MM/yyyy"));
                xml_cd = xml_cd.Replace("{docto_fl_validade}", "S");
                xml_cd = xml_cd.Replace("{docto_cod_tipodoc}", "24");
                xml_cd = xml_cd.Replace("{docto_cod_moeda}", "1");
                xml_cd = xml_cd.Replace("{docto_sistema_codigo}", "10");
                xml_cd = xml_cd.Replace("{DOCUMENTO-BASE-64}", DOC_BASE_64);
                xml_cd = xml_cd.Replace("{docto_fl_bacen}", "N");
                xml_cd = xml_cd.Replace("{docto_fl_ass_digital}", "S");
                xml_cd = xml_cd.Replace("{docto_fl_grava_docto}", "S");
                xml_cd = xml_cd.Replace("{docto_fl_ass_portal}", "S");
                xml_cd = xml_cd.Replace("{docto_fl_car_tempo}", "N");
                xml_cd = xml_cd.Replace("{docto_dtmanter_sist}", docto_data.AddYears(1).ToString("dd/MM/yyyy"));
                xml_cd = xml_cd.Replace("{docto_cod_tipo_acesso}", "0"); //0-Privado
                //xml_cd = xml_cd.Replace("{parte_papel_nome}", "Fundação CESP");
                xml_cd = xml_cd.Replace("{parte_papel_nome}", "Autenticador");
                //xml_cd = xml_cd.Replace("{parte_papel_nome}", "Diretoria (Outorgantes Procuração NÃO Eletrônica)");
                xml_cd = xml_cd.Replace("{parte_empr_cnpj}", "62465117000106");
                xml_cd = xml_cd.Replace("{parte_empr_ordem}", "1");
                xml_cd = xml_cd.Replace("{repr_cont_cpf}", repr_cont_cpf);
                qswsdeSoapClient qswsde = new qswsdeSoapClient(ws_ambiente);
                string ret = qswsde.AutoCadastroDOCUMENTO(xml_cd);
                //string ret = "<?xml version='1.0' encoding='ISO-8859-1' ?><documentoeletronico><CodErro>0</CodErro><MsgErro /><Passcode>KBQNPWFI9D0DQHX3FZDH</Passcode></documentoeletronico>";
                //string ret = "<?xml version='1.0' encoding='ISO-8859-1' ?><documentoeletronico><CodErro>65</CodErro><MsgErro>65 - Documento já existe.</MsgErro><Passcode /></documentoeletronico>";
                //documentoeletronico ws_r = ProcessaRetorno(ret);
                ws_r = (documentoeletronico)Util.Object2XML(ret, typeof(documentoeletronico));
            }
            catch (Exception ex)
            {
                ws_r.CodErro = 999;
                ws_r.MsgErro = ex.Message;
            }

            if (ws_r.CodErro == 0)
            {
                res.Sucesso(ws_r.CodErro + ";0 - Arquivo enviado com sucesso;" + ws_r.Passcode);
            }
            else
            {
                //res.Erro(ws_r.CodErro.ToString() + ';' + (ws_r.MsgErro.IndexOf(ws_r.CodErro.ToString()) == -1 ? ws_r.CodErro.ToString() : "") + " - " + ws_r.MsgErro);
                res.Erro(ws_r.CodErro.ToString() + ';' + ws_r.MsgErro + ";");
            }

            return res;

        }

        public Resultado AtualizarStatusDocumentos(string sessao, string ws_ambiente)
        {
            Resultado res = new Resultado();
            documentoeletronico ws_r = new documentoeletronico();
            try
            {
                qswsdeSoapClient qswsde = new qswsdeSoapClient(ws_ambiente);

                xml_ListarDocumento = xml_ListarDocumento.Replace("{sessao}", sessao);
                string ret = qswsde.ListarDocumento(xml_ListarDocumento);
                //string ret = "<?xml version='1.0' encoding='ISO-8859-1' ?><documentoeletronico><CodErro>0</CodErro><MsgErro /><documentos></documentos></documentoeletronico>";
                ws_r = (documentoeletronico)Util.Object2XML(ret, typeof(documentoeletronico));

            }
            catch (Exception ex)
            {
                ws_r.CodErro = 999;
                ws_r.MsgErro = ex.Message;
            }

            if (ws_r.CodErro == 0)
            {

                if (ws_r.documentos != null)
                {
                    XmlNode[] docs = (XmlNode[])ws_r.documentos;
                    if (docs.Length > 0)
                    {
                        foreach (XmlNode xDoc in docs)
                        {
                            lstdocumentoeletronico.Add(new docEletronico()
                            {
                                docto_referencia = xDoc.Attributes["docto_referencia"].InnerText,
                                docto_status = xDoc.Attributes["docto_status"].InnerText,
                                docto_validade = xDoc.Attributes["docto_validade"].InnerText
                            });
                        }
                        res = AtualizarStatusDocumentos(lstdocumentoeletronico);
                    }
                }
                else
                {
                    res.Alert("Nenhum documento para atualizar.");
                }

                if (res.Ok)
                {
                    res.Sucesso("Status dos documentos atualizados com sucesso.");
                }
            }
            else
            {
                res.Erro((ws_r.MsgErro.IndexOf(ws_r.CodErro.ToString()) == -1 ? ws_r.CodErro.ToString() : "") + " - " + ws_r.MsgErro);
            }

            return res;
        }

        public Resultado AtualizarStatusDocumentos(List<docEletronico> lstdocumentoeletronico)
        {
            Resultado res = new Resultado();
            foreach (docEletronico doc in lstdocumentoeletronico)
            {
                res = base.AtualizaStatus(doc.docto_referencia, doc.docto_status);
            }
            return res;
        }
    }
}
