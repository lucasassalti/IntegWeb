//using OfficeOpenXml;
using IntegWeb.Entidades.Saude;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace IntegWeb.Saude.Web
{

    public partial class GerarXmlANS : BasePage
    {

        string xml_registroANS = "<?xml version=\"1.0\" encoding=\"utf-8\"?><operadora xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><registroANS xsi:type=\"xsd:string\">315478</registroANS><cnpjOperadora xsi:type=\"xsd:string\">62465117000106</cnpjOperadora><solicitacao>{SOLICITACOES}</solicitacao></operadora>";
        string xml_nossoNumero_isencaoOnus = "<nossoNumero xsi:type=\"xsd:string\"></nossoNumero> <isencaoOnus xsi:type=\"xsd:string\">S</isencaoOnus>";
        string rpi_nome = "_INCLUSAO_ANS.RPI";
        string rpa_nome = "_ALTERACAO_ANS.RPA";
        string rpv_nome = "_VICULACAO_ANS.RPV";
        //string rpv_nome = ".RPV";
        string rpe_nome = "_EXCLUSAO_ANS.RPE";
        string xml_vinculacaoPrestador = "<vinculacaoPrestadorRede><prestador><cnpjCpf xsi:type=\"xsd:string\">{CNPJ}</cnpjCpf><cnes xsi:type=\"xsd:string\">{CNES}</cnes> <codigoMunicipioIBGE xsi:type=\"xsd:string\">{CODIGOMUNICIPIOIBGE}</codigoMunicipioIBGE></prestador><vinculacao>{VINCULACAO}</vinculacao></vinculacaoPrestadorRede>";
        string xsd_vinculacaoPrestador = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"><xs:element name=\"prestador\"><xs:complexType><xs:sequence><xs:element name=\"cnpjCpf\"></xs:element><xs:element name=\"cnes\"></xs:element><xs:element name=\"codigoMunicipioIBGE\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"vinculacao\"><xs:complexType><xs:sequence><xs:element name=\"numeroRegistroPlanoVinculacao\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element><xs:element name=\"codigoPlanoOperadoraVinculacao\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"vinculacaoPrestadorRede\"><xs:complexType><xs:sequence><xs:element ref=\"prestador\"></xs:element><xs:element ref=\"vinculacao\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"solicitacao\"><xs:complexType><xs:sequence><xs:element name=\"nossoNumero\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element><xs:element name=\"isencaoOnus\"></xs:element><xs:element ref=\"vinculacaoPrestadorRede\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"operadora\"><xs:complexType><xs:sequence><xs:element name=\"registroANS\"></xs:element><xs:element name=\"cnpjOperadora\"></xs:element><xs:element ref=\"solicitacao\"></xs:element></xs:sequence></xs:complexType></xs:element></xs:schema>";
        string xml_alteracaoPrestador  = "<alteracaoPrestador><identificacao><cnpjCpf xsi:type=\"xsd:string\">{IDENTIFICACAO_CNPJ}</cnpjCpf> <cnes xsi:type=\"xsd:string\">{IDENTIFICACAO_CNES}</cnes> <codigoMunicipioIBGE xsi:type=\"xsd:string\">{IDENTIFICACAO_CODIBGE}</codigoMunicipioIBGE> </identificacao> <alterarDados> <classificacao xsi:type=\"xsd:string\">{CLASSIFICACAO}</classificacao> <cnpjCpf xsi:type=\"xsd:string\">{CNPJ}</cnpjCpf> <cnes xsi:type=\"xsd:string\">{CNES}</cnes> <uf xsi:type=\"xsd:string\">{UF}</uf> <codigoMunicipioIBGE xsi:type=\"xsd:string\">{CODIGOMUNICIPIOIBGE}</codigoMunicipioIBGE> <razaoSocial xsi:type=\"xsd:string\">{RAZAOSOCIAL}</razaoSocial> <relacaoOperadora xsi:type=\"xsd:string\">{RELACAOOPERADORA}</relacaoOperadora> <tipoContratualizacao xsi:type=\"xsd:string\">{TIPOCONTRATUALIZACAO}</tipoContratualizacao> <registroANSOperadoraIntermediaria xsi:type=\"xsd:string\">{REGISTROANSOPERADORAINTERMEDIARIA}</registroANSOperadoraIntermediaria> <dataContratualizacao xsi:type=\"xsd:string\">{DATACONTRATUALIZACAO}</dataContratualizacao> <dataInicioPrestacaoServico xsi:type=\"xsd:string\">{DATAINICIOPRESTACAOSERVICO}</dataInicioPrestacaoServico> <disponibilidadeServico xsi:type=\"xsd:string\">{DISPONIBILIDADESERVICO}</disponibilidadeServico> <urgenciaEmergencia xsi:type=\"xsd:string\">{URGENCIAEMERGENCIA}</urgenciaEmergencia> <vinculacao> <numeroRegistroPlanoVinculacao xsi:type=\"xsd:string\"></numeroRegistroPlanoVinculacao> <codigoPlanoOperadoraVinculacao xsi:type=\"xsd:string\"></codigoPlanoOperadoraVinculacao> </vinculacao> </alterarDados> </alteracaoPrestador>";
        string xsd_alteracaoPrestador  = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"><xs:element name=\"identificacao\"><xs:complexType><xs:sequence><xs:element name=\"cnpjCpf\"></xs:element><xs:element name=\"cnes\"></xs:element><xs:element name=\"codigoMunicipioIBGE\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"vinculacao\"><xs:complexType><xs:sequence><xs:element name=\"numeroRegistroPlanoVinculacao\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element><xs:element name=\"codigoPlanoOperadoraVinculacao\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"alterarDados\"><xs:complexType><xs:sequence><xs:element name=\"classificacao\"></xs:element><xs:element name=\"cnpjCpf\"></xs:element><xs:element name=\"cnes\"></xs:element><xs:element name=\"uf\"></xs:element><xs:element name=\"codigoMunicipioIBGE\"></xs:element><xs:element name=\"razaoSocial\"></xs:element><xs:element name=\"relacaoOperadora\"></xs:element><xs:element name=\"tipoContratualizacao\"></xs:element><xs:element name=\"registroANSOperadoraIntermediaria\"></xs:element><xs:element name=\"dataContratualizacao\"></xs:element><xs:element name=\"dataInicioPrestacaoServico\"></xs:element><xs:element name=\"disponibilidadeServico\"></xs:element><xs:element name=\"urgenciaEmergencia\"></xs:element><xs:element ref=\"vinculacao\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"alteracaoPrestador\"><xs:complexType><xs:sequence><xs:element ref=\"identificacao\"></xs:element><xs:element ref=\"alterarDados\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"solicitacao\"><xs:complexType><xs:sequence><xs:element name=\"nossoNumero\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element><xs:element name=\"isencaoOnus\"></xs:element><xs:element ref=\"alteracaoPrestador\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element></xs:sequence></xs:complexType></xs:element><xs:element name=\"operadora\"><xs:complexType><xs:sequence><xs:element name=\"registroANS\"></xs:element><xs:element name=\"cnpjOperadora\"></xs:element><xs:element ref=\"solicitacao\"></xs:element></xs:sequence></xs:complexType></xs:element></xs:schema>";
        string xml_inclusaoPrestador   = "<inclusaoPrestador><classificacao xsi:type=\"xsd:string\">{CLASSIFICACAO}</classificacao> <cnpjCpf xsi:type=\"xsd:string\">{CNPJ}</cnpjCpf> <cnes xsi:type=\"xsd:string\">{CNES}</cnes><uf xsi:type=\"xsd:string\">{UF}</uf> <codigoMunicipioIBGE xsi:type=\"xsd:string\">{CODIGOMUNICIPIOIBGE}</codigoMunicipioIBGE> <razaoSocial xsi:type=\"xsd:string\">{RAZAOSOCIAL}</razaoSocial> <relacaoOperadora xsi:type=\"xsd:string\">{RELACAOOPERADORA}</relacaoOperadora> <tipoContratualizacao xsi:type=\"xsd:string\">{TIPOCONTRATUALIZACAO}</tipoContratualizacao> <registroANSOperadoraIntermediaria xsi:type=\"xsd:string\">{REGISTROANSOPERADORAINTERMEDIARIA}</registroANSOperadoraIntermediaria> <dataContratualizacao xsi:type=\"xsd:string\">{DATACONTRATUALIZACAO}</dataContratualizacao> <dataInicioPrestacaoServico xsi:type=\"xsd:string\">{DATAINICIOPRESTACAOSERVICO}</dataInicioPrestacaoServico> <disponibilidadeServico xsi:type=\"xsd:string\">{DISPONIBILIDADESERVICO}</disponibilidadeServico> <urgenciaEmergencia xsi:type=\"xsd:string\">{URGENCIAEMERGENCIA}</urgenciaEmergencia><vinculacao>{VINCULACAO}</vinculacao></inclusaoPrestador>";
        string xml_exclusaoPrestador   = "<exclusaoPrestador><identificacao><cnpjCpf xsi:type=\"xsd:string\">{CNPJ}</cnpjCpf><cnes xsi:type=\"xsd:string\">{CNES}</cnes><codigoMunicipioIBGE xsi:type=\"xsd:string\">{CodIBGE}</codigoMunicipioIBGE></identificacao></exclusaoPrestador>";
        string xsd_exclusaoPrestador   = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"> <xs:element name=\"identificacao\"> <xs:complexType> <xs:sequence> <xs:element name=\"cnpjCpf\"></xs:element> <xs:element name=\"cnes\"></xs:element> <xs:element name=\"codigoMunicipioIBGE\"></xs:element> </xs:sequence> </xs:complexType> </xs:element> <xs:element name=\"exclusaoPrestador\"> <xs:complexType> <xs:sequence> <xs:element ref=\"identificacao\"></xs:element> </xs:sequence> </xs:complexType> </xs:element> <xs:element name=\"solicitacao\"> <xs:complexType> <xs:sequence> <xs:element ref=\"exclusaoPrestador\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element> </xs:sequence> </xs:complexType> </xs:element> <xs:element name=\"operadora\"> <xs:complexType> <xs:sequence> <xs:element name=\"registroANS\"></xs:element> <xs:element name=\"cnpjOperadora\"></xs:element> <xs:element ref=\"solicitacao\"></xs:element> </xs:sequence> </xs:complexType> </xs:element> </xs:schema>";
        string tag_NR                  = "<numeroRegistroPlanoVinculacao xsi:type=\"xsd:string\">{NUMEROREGISTROPLANOVINCULACAO}</numeroRegistroPlanoVinculacao>";
        string tag_CP                  = "<codigoPlanoOperadoraVinculacao xsi:type=\"xsd:string\">{CODIGOPLANOOPERADORAVINCULACAO}</codigoPlanoOperadoraVinculacao>";

        protected void btnProcessar_Click(object sender, EventArgs e)
        {

            if (ValidarTela())
            {
                string path_listagem = "";
                string path_prestadores = "";
                try
                {

                    string[] name = Path.GetFileName(fuListagem.FileName).ToString().Split('.');
                    string UploadFilePath = Server.MapPath("UploadFile\\");
                    path_listagem = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                    if (!Directory.Exists(UploadFilePath))
                    {
                        Directory.CreateDirectory(UploadFilePath);
                    }

                    fuListagem.SaveAs(path_listagem);
                    DataTable dtExcel = ReadExcelFile(path_listagem,1,1);

                    DataTable dtExcelPrestadores = null;
                    DataTable dtPrestadores = null;

                    if (!String.IsNullOrEmpty(fuPrestadores.FileName)) { 
                        name = Path.GetFileName(fuPrestadores.FileName).ToString().Split('.');
                        path_prestadores = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
                        fuPrestadores.SaveAs(path_prestadores);
                        dtExcelPrestadores = ReadExcelFile(path_prestadores, 1, 7);
                        dtPrestadores = PreparaPrestadores(dtExcelPrestadores);
                    }

                    string sXml = "";
                    string extensao_anexo = ".xml";
                    int cReg = 0;
                    Resultado res = new Resultado();                    

                    switch (ddlTipo.SelectedValue)
                    {
                        case "RPI":
                            res = GeraXmlInclusao(dtExcel, out sXml, out cReg);
                            extensao_anexo = rpi_nome;
                            break;
                        case "RPA":
                            dtExcel = ReadExcelFile(path_listagem, 1, 2);
                            res = GeraXmlAlteracao(dtExcel, out sXml, out cReg);
                            extensao_anexo = rpa_nome;
                            break;
                        case "RPV":
                            res = GeraXmlVinculacao(dtExcel, out sXml, out cReg);
                            extensao_anexo = rpv_nome;
                            break;
                            //foreach (DataRow vinculos in dtExcel.Rows)
                            //{
                            //    sXml = "";
                            //    res = GeraXmlVinculacao(vinculos, out sXml);
                            //    if (res.Ok)
                            //    {
                            //        extensao_anexo = String.Format("_{0}_{1}_{2}{3}", 
                            //                            vinculos["CONTRATO"],
                            //                            Util.LimparCNPJ(vinculos["NUMERO_REGISTRO_PLANO_VINCULACAO"].ToString()),
                            //                            vinculos["CODIGO_PLANO_OPERADORA_VINCULACAO"].ToString(),
                            //                            rpv_nome);
                            //        GeraSaidaXml(sXml, DateTime.Today.ToString("MM_dd") + extensao_anexo);
                            //    }
                            //    cReg++;
                            //}                            
                            break;
                        case "RPE":
                            res = GeraXmlExclusao(dtExcel, dtPrestadores, out sXml, out cReg);
                            extensao_anexo = rpe_nome;
                            break;
                    }

                    //if (ddlTipo.SelectedValue != "RPV")
                    //{
                        GeraSaidaXml(sXml, DateTime.Today.ToString("yyyy_MM_dd") + extensao_anexo);
                    //}

                    if (res.Ok)
                    {
                        MostraMensagemRodape(String.Format("{0} registros gerados<br/><br/>", cReg), "n_ok");
                    }
                    else
                    {
                        MostraMensagemRodape(String.Format("{0} registros gerados<br/><br/>Atenção: " + res.Mensagem.Replace("\\n", "<br/>"), cReg));
                    }

                }
                catch (Exception ex)
                {
                    //MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);                     
                    MostraMensagemRodape("Atenção! O arquivo não pôde ser carregado.\\nMotivo: " + ex.Message, "n_error");
                }
                finally
                {
                    fuListagem.FileContent.Dispose();
                    fuListagem.FileContent.Flush();
                    fuListagem.PostedFile.InputStream.Flush();
                    fuListagem.PostedFile.InputStream.Close();
                    File.Delete(path_listagem);                    
                    fuPrestadores.FileContent.Dispose();
                    fuPrestadores.FileContent.Flush();
                    if (fuPrestadores.PostedFile!=null)
                    {
                        fuPrestadores.PostedFile.InputStream.Flush();
                        fuPrestadores.PostedFile.InputStream.Close();
                    }
                    if (File.Exists(path_prestadores)) { 
                        File.Delete(path_prestadores);
                    }
                }
            }
        }

        private bool ValidarTela()
        {

            if (String.IsNullOrEmpty(ddlTipo.SelectedValue)) 
            {
                MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nSelecione um tipo de arquivo para exportar");
                return false;
            }

            switch (ddlTipo.SelectedValue)
            {
                case "RPI":
                case "RPA":
                case "RPV":
                    if (!fuListagem.HasFile)
                    {
                        MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nSelecione a planilha com a lista para continuar");
                        return false;
                    }

                    if (
                        !fuListagem.PostedFile.ContentType.Contains("officedocument.spreadsheetml.sheet") && // formato superior 2003
                        !fuListagem.PostedFile.ContentType.Contains("vnd.ms-excel.sheet"))
                    {
                        MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nApenas planilhas do tipo Excel 2003 (.xls/.xlsx) ou superior são permitidas");
                        return false;
                    }
                    break;
                case "RPE":
                    if (!fuListagem.HasFile || !fuPrestadores.HasFile)
                    {
                        MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nSelecione as duas planilhas para continuar");
                        return false;
                    }

                    if (
                        !fuListagem.PostedFile.ContentType.Contains("officedocument.spreadsheetml.sheet") && // formato superior 2003
                        !fuListagem.PostedFile.ContentType.Contains("vnd.ms-excel.sheet") &&
                        !fuPrestadores.PostedFile.ContentType.Contains("officedocument.spreadsheetml.sheet") && // formato superior 2003
                        !fuPrestadores.PostedFile.ContentType.Contains("vnd.ms-excel.sheet"))
                    {
                        MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nApenas planilhas do tipo Excel 2003 (.xls/.xlsx) ou superior são permitidas");
                        return false;
                    }
                    break;
            }

            return true;

        }

        private DataTable PreparaPrestadores(DataTable dtExcelPrestadores)
        {
            dtExcelPrestadores.Columns.Add("CNPJ/CPF_Number", typeof(Int64));
            dtExcelPrestadores.Columns.Add("PJ_14_PF_11", typeof(int));
            DataTable dtPrestadores = dtExcelPrestadores.Clone();            

            foreach (DataRow drItem in dtExcelPrestadores.Rows)
            {
                if (drItem["CNPJ/CPF"]!=null && !String.IsNullOrEmpty(drItem["CNPJ/CPF"].ToString()))
                    drItem["CNPJ/CPF_Number"] = Int64.Parse(Util.LimparCNPJ(drItem["CNPJ/CPF"].ToString()));

                //Tipo de pessoa: PJ=14  -  PF=11
                drItem["PJ_14_PF_11"] = (drItem["CNPJ/CPF"].ToString().IndexOf("/") > -1) ? "14" : "11";

                if (drItem["Município"].ToString().ToUpper().IndexOf("MOJI ") > -1) // Correção Moji
                {
                    drItem["Município"] = drItem["Município"].ToString().ToUpper().Replace("MOJI", "MOGI");
                } else {
                    drItem["Município"] = drItem["Município"].ToString().ToUpper().Replace("ANHANGUERA", "ANHANGÜERA");
                    drItem["Município"] = drItem["Município"].ToString().ToUpper().Replace("BIRIGUI", "BIRIGÜI");
                    drItem["Município"] = drItem["Município"].ToString().ToUpper().Replace("LAURO MULLER", "LAURO MÜLLER");
                    drItem["Município"] = drItem["Município"].ToString().ToUpper().Replace("SANT'ANA DO LIVRAMENTO", "SANTANA DO LIVRAMENTO");
                }

                dtPrestadores.ImportRow(drItem);
            }

            dtPrestadores.Columns.Remove("CNPJ/CPF");
            dtPrestadores.Columns["CNPJ/CPF_Number"].ColumnName = "CNPJ_CPF";

            return dtPrestadores;
        }

        private string ProcessaDadosPrestador(string tipo_xml, string xml_base, Prestador oPrestador, ref string criticas)
        {

            ConvenenteBLL ConvenenteBLL = new ConvenenteBLL();
            CidadesBLL CidadesBLL = new CidadesBLL();
            TB_CONVENENTE Convenente = ConvenenteBLL.Consultar(oPrestador.COD_CONVENENTE);

            if (Convenente != null)
            {
                TB_END_CONVEN endConvenente = Convenente.TB_END_CONVEN.FirstOrDefault(end => end.IDC_END_CORRESP == "S" && end.IDC_DESATIVADO == "N");
                TB_END_CONVEN CNES_Convenente_ANS = Convenente.TB_END_CONVEN.FirstOrDefault(end => end.COD_MUNICI == (Convenente.COD_MUNICI_IBGE ?? 0).ToString() && end.IDC_DESATIVADO == "N");

                Cidade cidade =
                    (Convenente.COD_MUNICI_IBGE != null) ?
                        CidadesBLL.ConsultarPorIBGE(Decimal.ToInt32((Decimal)Convenente.COD_MUNICI_IBGE)) :
                        CidadesBLL.ConsultarPorCodigo(endConvenente.COD_MUNICI, endConvenente.COD_ESTADO);

                xml_base = xml_base.Replace("{CLASSIFICACAO}", Convenente.TIP_CLASSIF_ESTAB ?? "1");

                //xml_base = xml_base.Replace("{CNPJ}", oPrestador.CNPJ.PadLeft((oPrestador.PJ) ? 14 : 11, '0'));
                xml_base = xml_base.Replace("{CNPJ}", Convenente.NUM_CGC_CPF.Trim().PadLeft((Convenente.TIP_PESSOA == "J") ? 14 : 11, '0'));

                //xml_base = xml_base.Replace("{CNES}", Convenente.NUM_CNES ?? "");
                if (CNES_Convenente_ANS != null || endConvenente != null)
                {
                    xml_base = xml_base.Replace("{CNES}", (CNES_Convenente_ANS ?? endConvenente).NUM_CNES);
                }
                else
                {
                    xml_base = xml_base.Replace("{CNES}", "");
                }

                xml_base = xml_base.Replace("{UF}", cidade.estado_sigla);
                xml_base = xml_base.Replace("{CODIGOMUNICIPIOIBGE}", cidade.cod_ibge.ToString());
                xml_base = xml_base.Replace("{RAZAOSOCIAL}", Util.UTF8_XML_DECODE(Convenente.NOM_RAZAO_SOCIAL ?? Convenente.NOM_CONVENENTE));
                xml_base = xml_base.Replace("{RELACAOOPERADORA}", "C"); //contratualizado
                xml_base = xml_base.Replace("{TIPOCONTRATUALIZACAO}", Convenente.TIP_CONTRATACAO ?? "D"); // D - direto
                if (Convenente.TIP_CONTRATACAO == "D")
                {
                    xml_base = xml_base.Replace("{REGISTROANSOPERADORAINTERMEDIARIA}", "");
                    xml_base = xml_base.Replace("{DATACONTRATUALIZACAO}", DateTime.Parse(Convenente.DAT_INI_CONTRATO.ToString()).ToString("dd/MM/yyyy"));                  
                }
                else
                {
                    if (Convenente.TIP_CONTRATACAO == "I" && Convenente.NUM_REG_ANS_OPE_INT == null)
                    {
                        criticas += String.Format("Não foi localizado o código de registro na ANS da operadora intermediária: CNPJ {0} - CNES: {1}\\n" +
                                    "       {2} Município: {3} ({4})\\n\\n",
                                    Convenente.NUM_CGC_CPF,
                                    Convenente.NUM_CNES,
                                    (Convenente.NOM_RAZAO_SOCIAL ?? Convenente.NOM_CONVENENTE),
                                    cidade.nome,
                                    cidade.estado_sigla);
                    }
                    xml_base = xml_base.Replace("{REGISTROANSOPERADORAINTERMEDIARIA}", Convenente.NUM_REG_ANS_OPE_INT.ToString());
                    xml_base = xml_base.Replace("{DATACONTRATUALIZACAO}", "");
                }
                xml_base = xml_base.Replace("{DATAINICIOPRESTACAOSERVICO}", DateTime.Parse(Convenente.DAT_INI_CONTRATO.ToString()).ToString("dd/MM/yyyy"));
                xml_base = xml_base.Replace("{DISPONIBILIDADESERVICO}", Convenente.IDC_DISP_SERV); //P - parcial
                xml_base = xml_base.Replace("{URGENCIAEMERGENCIA}", Convenente.IDC_PRONTO_SOCORRO); //N - não

                if (cidade.cod_ibge == 0)
                {

                    if (Convenente.COD_MUNICI_IBGE == null)
                    {
                        criticas += String.Format("Cód. IBGE não encontrado para o município {0} ({1})\\n" +
                                                    "CNPJ {2} - CNES: {3} - Razão Social: {4}\\n\\n",
                                                    cidade.nome,
                                                    cidade.estado_sigla,
                                                    Convenente.NUM_CGC_CPF,
                            //Convenente.NUM_CNES,
                                                    (String.IsNullOrEmpty((CNES_Convenente_ANS ?? endConvenente).NUM_CNES) ? "VAZIO" : (CNES_Convenente_ANS ?? endConvenente).NUM_CNES),
                                                    Convenente.NOM_RAZAO_SOCIAL);
                    }
                    else
                    {
                        criticas += String.Format("Cód. IBGE Município para ANS não encontrado: {0} \\n" +
                                                    "CNPJ {1} - CNES: {2} - Razão Social: {3}\\n\\n",
                                                    Convenente.COD_MUNICI_IBGE,
                                                    Convenente.NUM_CGC_CPF,
                                                    Convenente.NUM_CNES,
                                                    (Convenente.NOM_RAZAO_SOCIAL ?? Convenente.NOM_CONVENENTE));
                    }
                }

                if (Convenente.SIT_CONVENENTE == "3")
                {
                    criticas += String.Format("Prestador Excluído (Situação=3): CNPJ {0} - CNES: {1}\\n" +
                                "       {2} Município: {3} ({4})\\n\\n",
                                Convenente.NUM_CGC_CPF,
                                Convenente.NUM_CNES,
                                (Convenente.NOM_RAZAO_SOCIAL ?? Convenente.NOM_CONVENENTE),
                                cidade.nome,
                                cidade.estado_sigla);
                }
            }
            else
            {
                criticas += String.Format("Prestador não localizado: CNPJ {0} - CNES: {1}\\n" +
                            "       Cód. Convenente (contrato) {2} - {3} \\n\\n",
                            oPrestador.CNPJ,
                            oPrestador.CNES,
                            oPrestador.COD_CONVENENTE,
                            oPrestador.Razao_Social);
                xml_base = "";
            }

            return xml_base;        
        }

        private string ProcessaDadosPrestador2(string tipo_xml, string xml_base, Prestador oPrestador, ref string criticas)
        {

            ConvenenteBLL ConvenenteBLL = new ConvenenteBLL();
            CidadesBLL CidadesBLL = new CidadesBLL();
            TB_CONVENENTE Convenente = ConvenenteBLL.Consultar(oPrestador.COD_CONVENENTE);

            if (Convenente != null)
            {
                TB_END_CONVEN endConvenente = Convenente.TB_END_CONVEN.FirstOrDefault(end => end.IDC_END_CORRESP == "S" && end.IDC_DESATIVADO == "N");
                TB_END_CONVEN CNES_Convenente_ANS = Convenente.TB_END_CONVEN.FirstOrDefault(end => end.COD_MUNICI == (Convenente.COD_MUNICI_IBGE ?? 0).ToString() && end.IDC_DESATIVADO == "N");

                xml_base = xml_base.Replace("{CLASSIFICACAO}", "");

                //xml_base = xml_base.Replace("{CNPJ}", oPrestador.CNPJ.PadLeft((oPrestador.PJ) ? 14 : 11, '0'));
                xml_base = xml_base.Replace("{CNPJ}", Convenente.NUM_CGC_CPF.Trim().PadLeft((Convenente.TIP_PESSOA == "J") ? 14 : 11, '0'));

                //xml_base = xml_base.Replace("{CNES}", Convenente.NUM_CNES ?? "");
                xml_base = xml_base.Replace("{CNES}", oPrestador.CNES);

                //if (Convenente.COD_MUNICI_IBGE != null)

                Cidade cidade =
                    (Convenente.COD_MUNICI_IBGE != null) ?
                        CidadesBLL.ConsultarPorIBGE(Decimal.ToInt32((Decimal)Convenente.COD_MUNICI_IBGE)) :
                        CidadesBLL.ConsultarPorCodigo(endConvenente.COD_MUNICI, endConvenente.COD_ESTADO);

                if (!String.IsNullOrEmpty(oPrestador.Municipio) && (Convenente.COD_MUNICI_IBGE != null))
                {
                    xml_base = xml_base.Replace("{UF}", cidade.estado_sigla);
                }
                else
                {
                    xml_base = xml_base.Replace("{UF}", "");
                }

                if (!String.IsNullOrEmpty(oPrestador.UF) && (Convenente.COD_MUNICI_IBGE != null))
                {
                    if (String.IsNullOrEmpty(oPrestador.UF))
                    {
                        xml_base = xml_base.Replace("{UF}", cidade.estado_sigla);
                    }
                }
                else
                {
                    xml_base = xml_base.Replace("{CODIGOMUNICIPIOIBGE}", "");
                }

                //xml_base = xml_base.Replace("{CLASSIFICACAO}", "");
                //xml_base = xml_base.Replace("{CNPJ}", "");
                //xml_base = xml_base.Replace("{CNES}", "");
                //xml_base = xml_base.Replace("{UF}", "");
                //xml_base = xml_base.Replace("{CODIGOMUNICIPIOIBGE}", "");
                xml_base = xml_base.Replace("{RAZAOSOCIAL}", oPrestador.Razao_Social);
                xml_base = xml_base.Replace("{RELACAOOPERADORA}", oPrestador.RELACAO_OPERADORA);
                xml_base = xml_base.Replace("{TIPOCONTRATUALIZACAO}", oPrestador.TIPO_CONTRATUALIZACAO);
                xml_base = xml_base.Replace("{REGISTROANSOPERADORAINTERMEDIARIA}", oPrestador.REGISTRO_ANS_OPERADORA_INTERMEDIARIA);
                if (!String.IsNullOrEmpty(oPrestador.DATA_CONTRATUALIZACAO))
                {
                    xml_base = xml_base.Replace("{DATACONTRATUALIZACAO}", DateTime.Parse(oPrestador.DATA_CONTRATUALIZACAO).ToString("dd/MM/yyyy"));
                }
                else
                {
                    xml_base = xml_base.Replace("{DATACONTRATUALIZACAO}", "");
                }
                xml_base = xml_base.Replace("{DATAINICIOPRESTACAOSERVICO}", "");
                xml_base = xml_base.Replace("{DISPONIBILIDADESERVICO}", "");
                xml_base = xml_base.Replace("{URGENCIAEMERGENCIA}", ""); 

                if (cidade.cod_ibge == 0)
                {

                    if (Convenente.COD_MUNICI_IBGE == null)
                    {
                        criticas += String.Format("Cód. IBGE não encontrado para o município {0} ({1})\\n" +
                                                    "CNPJ {2} - CNES: {3} - Razão Social: {4}\\n\\n",
                                                    cidade.nome,
                                                    cidade.estado_sigla,
                                                    Convenente.NUM_CGC_CPF,
                            //Convenente.NUM_CNES,
                                                    (String.IsNullOrEmpty((CNES_Convenente_ANS ?? endConvenente).NUM_CNES) ? "VAZIO" : (CNES_Convenente_ANS ?? endConvenente).NUM_CNES),
                                                    Convenente.NOM_RAZAO_SOCIAL);
                    }
                    else
                    {
                        criticas += String.Format("Cód. IBGE Município para ANS não encontrado: {0} \\n" +
                                                    "CNPJ {1} - CNES: {2} - Razão Social: {3}\\n\\n",
                                                    Convenente.COD_MUNICI_IBGE,
                                                    Convenente.NUM_CGC_CPF,
                                                    Convenente.NUM_CNES,
                                                    (Convenente.NOM_RAZAO_SOCIAL ?? Convenente.NOM_CONVENENTE));
                    }
                }

                if (Convenente.SIT_CONVENENTE == "3")
                {
                    criticas += String.Format("Prestador Excluído (Situação=3): CNPJ {0} - CNES: {1}\\n" +
                                "       {2} Município: {3} ({4})\\n\\n",
                                Convenente.NUM_CGC_CPF,
                                Convenente.NUM_CNES,
                                (Convenente.NOM_RAZAO_SOCIAL ?? Convenente.NOM_CONVENENTE),
                                cidade.nome,
                                cidade.estado_sigla);
                }
            }
            else
            {
                criticas += String.Format("Prestador não localizado: CNPJ {0} - CNES: {1}\\n" +
                            "       Cód. Convenente (contrato) {2} - {3} \\n\\n",
                            oPrestador.CNPJ,
                            oPrestador.CNES,
                            oPrestador.COD_CONVENENTE,
                            oPrestador.Razao_Social);
                xml_base = "";
            }

            return xml_base;   

            //xml_base = xml_base.Replace("{CLASSIFICACAO}", "");
            //xml_base = xml_base.Replace("{DATACONTRATUALIZACAO}", "");  
            //return xml_base;
        }


        private string SubstituiDadosPrestador(string xml_base, Prestador oPrestador)
        {            
            //TB_CONVENENTE Convenente = ConvenenteBLL.Consultar(oPrestador.COD_CONVENENTE);
            ConvenenteBLL ConvenenteBLL = new ConvenenteBLL();
            CidadesBLL CidadesBLL = new CidadesBLL();
            TB_CONVENENTE Convenente = ConvenenteBLL.Consultar(oPrestador.COD_CONVENENTE);

            if (Convenente != null)
            {
                TB_END_CONVEN endConvenente = Convenente.TB_END_CONVEN.FirstOrDefault(end => end.IDC_END_CORRESP == "S" && end.IDC_DESATIVADO == "N");
                TB_END_CONVEN CNES_Convenente_ANS = Convenente.TB_END_CONVEN.FirstOrDefault(end => end.COD_MUNICI == (Convenente.COD_MUNICI_IBGE ?? 0).ToString() && end.IDC_DESATIVADO == "N");

                Cidade cidade =
                    (Convenente.COD_MUNICI_IBGE != null) ?
                        CidadesBLL.ConsultarPorIBGE(Decimal.ToInt32((Decimal)Convenente.COD_MUNICI_IBGE)) :
                        CidadesBLL.ConsultarPorCodigo(endConvenente.COD_MUNICI, endConvenente.COD_ESTADO);

                //xml_base = xml_base.Replace("{IDENTIFICACAO_CNPJ}", Convenente.NUM_CGC_CPF.Trim().PadLeft((Convenente.TIP_PESSOA == "J") ? 14 : 11, '0'));

                if (!String.IsNullOrEmpty(oPrestador.CHAVE_ALTERACAO.CNPJ.Trim()))
                {
                    xml_base = xml_base.Replace("{IDENTIFICACAO_CNPJ}", oPrestador.CHAVE_ALTERACAO.CNPJ.Trim().PadLeft((Convenente.TIP_PESSOA == "J") ? 14 : 11, '0'));
                }
                else
                {
                    //CNPJ ANTIGO:
                    xml_base = xml_base.Replace("{IDENTIFICACAO_CNPJ}", Convenente.NUM_CGC_CPF.Trim().PadLeft((Convenente.TIP_PESSOA == "J") ? 14 : 11, '0'));
                }

                xml_base = xml_base.Replace("{IDENTIFICACAO_CNES}", oPrestador.CHAVE_ALTERACAO.CNES);
                //if (!String.IsNullOrEmpty(oPrestador.CHAVE_ALTERACAO.CNES.Trim()))
                //{
                //    xml_base = xml_base.Replace("{IDENTIFICACAO_CNES}", oPrestador.CHAVE_ALTERACAO.CNES);
                //}
                //else
                //{
                //    //CNES ANTIGO:
                //    if (CNES_Convenente_ANS != null || endConvenente != null) {
                //        xml_base = xml_base.Replace("{IDENTIFICACAO_CNES}", (CNES_Convenente_ANS ?? endConvenente).NUM_CNES);
                //    } else {
                //        xml_base = xml_base.Replace("{IDENTIFICACAO_CNES}", "");
                //    }
                //}

                if (!String.IsNullOrEmpty(oPrestador.CHAVE_ALTERACAO.Municipio.Trim()))
                {
                    xml_base = xml_base.Replace("{IDENTIFICACAO_CODIBGE}", oPrestador.CHAVE_ALTERACAO.Municipio.PadLeft(6, '0').Substring(0, 6));
                }
                else
                {
                    //CODIBGE ANTIGO:
                    xml_base = xml_base.Replace("{IDENTIFICACAO_CODIBGE}", cidade.cod_ibge.ToString());
                }
                
            }
            else
            {
                xml_base = xml_base.Replace("{IDENTIFICACAO_CNPJ}", oPrestador.CNPJ.Trim().PadLeft((oPrestador.PJ) ? 14 : 11, '0'));
                xml_base = xml_base.Replace("{IDENTIFICACAO_CNES}", oPrestador.CNES);
                xml_base = xml_base.Replace("{IDENTIFICACAO_CODIBGE}", oPrestador.Municipio.PadLeft(6, '0').Substring(0, 6));
            }

            //{IDENTIFICACAO_CNPJ}
            //{IDENTIFICACAO_CNES}
            //{IDENTIFICACAO_CODIBGE}
            //{CLASSIFICACAO}
            //{CNPJ}
            //{CNES}
            //{UF}
            //{CODIGOMUNICIPIOIBGE}
            //{RAZAOSOCIAL}
            //{RELACAOOPERADORA}
            //{TIPOCONTRATUALIZACAO}
            //{REGISTROANSOPERADORAINTERMEDIARIA}
            //{DATACONTRATUALIZACAO}
            //{DATAINICIOPRESTACAOSERVICO}
            //{DISPONIBILIDADESERVICO}
            //{URGENCIAEMERGENCIA}

            return xml_base;
        }

        private Resultado GeraXmlInclusao(DataTable dtIncluidos, out string sXml, out int cReg)
        {
            sXml = "";
            cReg = 0;
            string criticas = "";            
            Resultado res = new Resultado();            
            PrestadorBLL Prestador = new PrestadorBLL();            
            Prestador.LoadData(dtIncluidos);
            foreach (Prestador oPrestador in Prestador.Prestadores)
            {
                string strInclusaoPrestador = ProcessaDadosPrestador("I", xml_inclusaoPrestador, oPrestador, ref criticas);                
                string strVINCULACAO = ProcessaPlanos(oPrestador, ref criticas);
                strInclusaoPrestador = strInclusaoPrestador.Replace("{VINCULACAO}", strVINCULACAO).Trim();                
                sXml += strInclusaoPrestador;
                cReg++;
            }

            sXml = xml_registroANS.Replace("{SOLICITACOES}", xml_nossoNumero_isencaoOnus + sXml);
            sXml = sXml.Replace("&", "&amp;");
            //Resultado resXVal = Util.XmlValidator(sXml, xsd_exclusaoPrestador);

            if (String.IsNullOrEmpty(criticas))
            {
                res.Sucesso("");
            }
            else
            {
                res.Erro(criticas);
            }

            return res;

        }

        private string ProcessaPlanos(Prestador oPrestador, ref string criticas)
        {
            string xml_Planos = "";
            string[] numRegistros = Util.LimparCNPJ(oPrestador.REGISTRO_PLANO).Split(',');
            string[] codPlanos = Util.LimparCNPJ(oPrestador.CODIGO_PLANO_OPERADORA).Split(',');

            foreach(string rp in numRegistros)
            {
                xml_Planos += tag_NR.Replace("{NUMEROREGISTROPLANOVINCULACAO}", rp);
            }

            foreach(string cp in codPlanos)
            {
                xml_Planos += tag_CP.Replace("{CODIGOPLANOOPERADORAVINCULACAO}", cp);
            }

            return xml_Planos;
        }

        private Resultado GeraXmlAlteracao(DataTable dtIncluidos, out string sXml, out int cReg)
        {
            sXml = "";
            cReg = 0;
            string criticas = "";            
            Resultado res = new Resultado();
            PrestadorBLL Prestador = new PrestadorBLL();
            Prestador.LoadData(dtIncluidos);
            foreach (Prestador oPrestador in Prestador.Prestadores)
            {
                string strAlteracaoPrestador = ProcessaDadosPrestador2("A", xml_alteracaoPrestador, oPrestador, ref criticas);
                strAlteracaoPrestador = SubstituiDadosPrestador(strAlteracaoPrestador, oPrestador);
                sXml += strAlteracaoPrestador;
                cReg++;
            }

            sXml = xml_registroANS.Replace("{SOLICITACOES}", xml_nossoNumero_isencaoOnus + sXml);
            sXml = sXml.Replace("&", "&amp;");
            Resultado resXVal = Util.XmlValidator(sXml, xsd_alteracaoPrestador);

            if (String.IsNullOrEmpty(criticas))
            {
                res.Sucesso("");
            }
            else
            {
                res.Erro(criticas);
            }

            return res;

        }

        private Resultado GeraXmlVinculacao(DataTable drVinculado, out string sXml, out int cReg)
        {
            sXml = "";
            cReg = 0;
            string criticas = "";
            Resultado res = new Resultado();
            PrestadorBLL Prestador = new PrestadorBLL();
            Prestador.LoadData(drVinculado);
            //Prestador oPrestador = Prestador.DataRow2Pretador(drVinculado);
            foreach (Prestador oPrestador in Prestador.Prestadores)
            {
                string strVinculacaoPrestador = ProcessaDadosPrestador("V", xml_vinculacaoPrestador, oPrestador, ref criticas);
                string strVINCULACAO = ProcessaPlanos(oPrestador, ref criticas);
                strVinculacaoPrestador = strVinculacaoPrestador.Replace("{VINCULACAO}", strVINCULACAO).Trim();      
                sXml += strVinculacaoPrestador;
                cReg++;
            }

            sXml = xml_registroANS.Replace("{SOLICITACOES}", xml_nossoNumero_isencaoOnus + sXml);
            sXml = sXml.Replace("&", "&amp;");
            Resultado resXVal = Util.XmlValidator(sXml, xsd_vinculacaoPrestador);

            if (String.IsNullOrEmpty(criticas))
            {
                res.Sucesso("");
            }
            else
            {
                res.Erro(criticas);
            }

            return res;

        }

        private Resultado GeraXmlExclusao(DataTable dtExcluidos, DataTable dtPrestadores, out string sXml, out int cReg)
        {
            sXml = "";
            cReg = 0;
            bool duplicados = false;
            string criticas = "";
            Resultado res = new Resultado();
            CidadesBLL CidadesBLL = new CidadesBLL();
            foreach (DataRow drCanc in dtExcluidos.Rows)
            {
                string strExclusaoPrestador = xml_exclusaoPrestador;
                string Razao_Social = "";

                if (drCanc["Razão Social"] != DBNull.Value)
                {
                    Razao_Social = drCanc["Razão Social"].ToString().Replace("'", "''");
                }

                DataRow[] draPrestador = new DataRow[] { };

                string CNES_test = "AND CNES='{1}'";

                if (drCanc["CNES"] == DBNull.Value)
                {
                    CNES_test = "AND CNES IS NULL";
                }

                if (drCanc["CNPJ/CPF_NUM"] != DBNull.Value && drCanc["CNES"] != DBNull.Value) // Busca pelo CNPJ
                {
                    draPrestador = dtPrestadores.Select(String.Format("CNPJ_CPF={0} " + CNES_test, drCanc["CNPJ/CPF_NUM"], drCanc["CNES"]));
                }
                else if (drCanc["Razão Social"] != DBNull.Value) // Busca pela Razão Social
                {
                    draPrestador = dtPrestadores.Select(String.Format("[Razão Social]='{0}' " + CNES_test, Razao_Social, drCanc["CNES"]));
                }

                if (draPrestador.Length > 1)
                {
                    draPrestador = dtPrestadores.Select(String.Format("CNPJ_CPF={0} " + CNES_test + " AND [Razão Social]='{2}'", drCanc["CNPJ/CPF_NUM"], drCanc["CNES"], Razao_Social));
                    duplicados = true;
                }

                if (draPrestador.Length > 1)
                {
                    draPrestador = dtPrestadores.Select(String.Format("CNPJ_CPF={0} " + CNES_test + " AND [Razão Social]='{2}' AND [Município]='{3}'", drCanc["CNPJ/CPF_NUM"], drCanc["CNES"], Razao_Social, drCanc["Município"]));
                    duplicados = true;
                }

                //if (draPrestador.Length > 1 && criticas.IndexOf(draPrestador[0]["CNPJ_CPF"].ToString()) == -1)
                //{

                //    criticas += String.Format("Razão Social duplicada vinculada ao CNPJ {0} - CNES: {1}\\n" +
                //                              "       {2} Município: {3} ({4})\\n",
                //                              draPrestador[0]["CNPJ_CPF"],
                //                              (String.IsNullOrEmpty(draPrestador[0]["CNES"].ToString()) ? "VAZIO" : draPrestador[0]["CNES"].ToString()),
                //                              draPrestador[0]["Razão Social"],
                //                              draPrestador[0]["Município"],
                //                              draPrestador[0]["UF"]);

                //    criticas += String.Format("       {0} Município: {1} ({2})\\n\\n",
                //                              draPrestador[1]["Razão Social"],
                //                              draPrestador[1]["Município"],
                //                              draPrestador[1]["UF"]);

                //}

                if (draPrestador.Length == 0 && criticas.IndexOf(drCanc["CNPJ/CPF_NUM"].ToString()) == -1)
                {
                    if (duplicados){
                        criticas += String.Format("Dados divergentes e/ou duplicados para o seguinte CNPJ {0} - CNES: {1}\\n" +
                                                  "       {2} Município: {3} ({4})\\n\\n",
                                                  drCanc["CNPJ/CPF_NUM"],
                                                  (String.IsNullOrEmpty(drCanc["CNES"].ToString()) ? "VAZIO" : drCanc["CNES"].ToString()),
                                                  drCanc["Razão Social"],
                                                  drCanc["Município"],
                                                  drCanc["UF"]);
                    }
                    else
                    {
                        criticas += String.Format("Não foram encontrados dados para o CNPJ {0} - CNES: {1}\\n" +
                                                  "       {2} Município: {3} ({4})\\n\\n",
                                                  drCanc["CNPJ/CPF_NUM"],
                                                  (String.IsNullOrEmpty(drCanc["CNES"].ToString()) ? "VAZIO" : drCanc["CNES"].ToString()),
                                                  drCanc["Razão Social"],
                                                  drCanc["Município"],
                                                  drCanc["UF"]);
                    } 
                }

                if (draPrestador.Length>0)
                {
                    DataRow drPrestador = draPrestador[0];
                    //foreach (DataRow drPrestador in dtPrestadores.Select(String.Format("CNPJ_CPF={0} AND UF='{1}' AND Município='{2}'", drCanc["CNPJ/CPF_NUM"], drCanc["UF"], drCanc["Município"])))
                    //{
                    Cidade cidade = CidadesBLL.Consultar(drPrestador["Município"].ToString().ToUpper(), ValidaCaracteres(drPrestador["UF"].ToString().ToUpper()));
                    //Cidade cidade = CidadesBLL.Consultar(drCanc["Município"].ToString().ToUpper(), ValidaCaracteres(drCanc["UF"].ToString().ToUpper()));

                    strExclusaoPrestador = strExclusaoPrestador.Replace("{CNPJ}", drPrestador["CNPJ_CPF"].ToString().PadLeft((int)drPrestador["PJ_14_PF_11"], '0'));
                    strExclusaoPrestador = strExclusaoPrestador.Replace("{CNES}", drPrestador["CNES"].ToString());
                    strExclusaoPrestador = strExclusaoPrestador.Replace("{CodIBGE}", cidade.cod_ibge.ToString());
                    if (cidade.cod_ibge == 0)
                    {

                        criticas += String.Format("Cód. IBGE não encontrado para o município {0} ({1})\\n" +
                                                  "CNPJ {2} - CNES: {3} - Razão Social: {4}\\n\\n",
                                                  draPrestador[0]["Município"],
                                                  draPrestador[0]["UF"],
                                                  draPrestador[0]["CNPJ_CPF"],
                                                  (String.IsNullOrEmpty(draPrestador[0]["CNES"].ToString()) ? "VAZIO" : draPrestador[0]["CNES"].ToString()),
                                                  draPrestador[0]["Razão Social"]);
                    }
                    sXml += strExclusaoPrestador;
                    cReg++;
                }
                //break;
                //}
            }

            sXml = xml_registroANS.Replace("{SOLICITACOES}", sXml);
            sXml = sXml.Replace("&", "&amp;");
            Resultado resXVal = Util.XmlValidator(sXml, xsd_exclusaoPrestador);

            if (resXVal.Ok && String.IsNullOrEmpty(criticas))
            {
                res.Sucesso(resXVal.Mensagem);
            }
            else
            {
                res.Erro(criticas + resXVal.Mensagem);
            }

            return res;

        }
       
        public void GeraSaidaXml(string sXML_ANS, string extensao_anexo)
        {
            ArquivoDownload adXML_ANS = new ArquivoDownload();
            XmlDocument xdANS = new XmlDocument();
            xdANS.LoadXml(sXML_ANS);
            adXML_ANS.dados = xdANS;
            adXML_ANS.nome_arquivo = extensao_anexo;
            Session[adXML_ANS.nome_arquivo] = adXML_ANS;
            string fullUrl = "WebFile.aspx?dwFile=" + adXML_ANS.nome_arquivo;
            AdicionarAcesso(fullUrl);
            AbrirNovaAba(this, fullUrl, adXML_ANS.nome_arquivo);
        }
    }
}