using IntegWeb.Entidades.Framework;
using IntegWeb.Saude.Aplicacao.BLL;
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

    public partial class GerarPlanAtendeSUS : BasePage
    {

        string xml_registroANS = "<?xml version=\"1.0\" encoding=\"utf-8\"?><operadora xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><registroANS xsi:type=\"xsd:string\">315478</registroANS><cnpjOperadora xsi:type=\"xsd:string\">62465117000106</cnpjOperadora><solicitacao>{SOLICITACOES}</solicitacao></operadora>";
        string xml_exclusaoPrestador = "<exclusaoPrestador><identificacao><cnpjCpf xsi:type=\"xsd:string\">{CNPJ}</cnpjCpf><cnes xsi:type=\"xsd:string\">{CNES}</cnes><codigoMunicipioIBGE xsi:type=\"xsd:string\">{CodIBGE}</codigoMunicipioIBGE></identificacao></exclusaoPrestador>";
        string xsd_exclusaoPrestador = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"> <xs:element name=\"identificacao\"> <xs:complexType> <xs:sequence> <xs:element name=\"cnpjCpf\"></xs:element> <xs:element name=\"cnes\"></xs:element> <xs:element name=\"codigoMunicipioIBGE\"></xs:element> </xs:sequence> </xs:complexType> </xs:element> <xs:element name=\"exclusaoPrestador\"> <xs:complexType> <xs:sequence> <xs:element ref=\"identificacao\"></xs:element> </xs:sequence> </xs:complexType> </xs:element> <xs:element name=\"solicitacao\"> <xs:complexType> <xs:sequence> <xs:element ref=\"exclusaoPrestador\" minOccurs=\"1\" maxOccurs=\"unbounded\"></xs:element> </xs:sequence> </xs:complexType> </xs:element> <xs:element name=\"operadora\"> <xs:complexType> <xs:sequence> <xs:element name=\"registroANS\"></xs:element> <xs:element name=\"cnpjOperadora\"></xs:element> <xs:element ref=\"solicitacao\"></xs:element> </xs:sequence> </xs:complexType> </xs:element> </xs:schema>";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            lblRegistros.Text = "";
            int i = 0;

            //Deleta Registros da Tabela Antes do Processamento
            ParticipantesBLL bll = new ParticipantesBLL();
            bll.DeletaRegistros();

            if (fuCancelados.HasFile)
            {
                if (
                   fuCancelados.PostedFile.ContentType.Equals("text/xml"))
                {
                    string path_xml = "";
                    try
                    {


                        string[] name = Path.GetFileName(fuCancelados.FileName).ToString().Split('.');
                        string UploadFilePath = Server.MapPath("UploadFile\\");
                        path_xml = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                        if (!Directory.Exists(UploadFilePath))
                        {
                            Directory.CreateDirectory(UploadFilePath);
                        }

                        fuCancelados.SaveAs(path_xml);
                        DataSet dsXML = ImportarXML(path_xml);

                        ParticipantesBLL pParticipantes = new ParticipantesBLL();

                        DataTable dtAtendimentos = PreparaPrestadores(dsXML.Tables["atendimento"]);

                        foreach (DataRow dtAtendimento in dtAtendimentos.Rows)
                        {
                            i++;

                            if (dtAtendimento["codigoBeneficiario"] != null)
                            {
                                SAU_VW_CONSULTA_USUARIO_SUS part =
                                pParticipantes.ListarPorParticipante(dtAtendimento["codigoBeneficiario"].ToString(),
                                                                     null,
                                                                     dtAtendimento["dataInicioAtendimento"].ToString());

                                if (part == null)
                                {
                                    part =
                                    pParticipantes.ListarPorParticipante(null,
                                                                         dtAtendimento["codigoBeneficiario"].ToString(),
                                                                         dtAtendimento["dataInicioAtendimento"].ToString());
                                }

                                if (part == null)
                                {
                                      part =
                                    pParticipantes.ListarPorParticipante(null,
                                                                         dtAtendimento["codigoBeneficiario"].ToString(),
                                                                         null);
                                }

                                if (part == null)
                                {
                                      part =
                                    pParticipantes.ListarPorParticipante(dtAtendimento["codigoBeneficiario"].ToString(),
                                                                         null,
                                                                         null);
                                }

                                if (part != null)
                                {

                                    dtAtendimento["COD_EMPRS"] = part.COD_EMPRS;
                                    dtAtendimento["NUM_MATRICULA"] = part.NUM_RGTRO_EMPRG;
                                    dtAtendimento["SUB_MATRICULA"] = part.NUM_SUB_MATRIC;
                                    dtAtendimento["Nomeparticipante"] = part.NOM_PARTICIP;
                                    dtAtendimento["DDD"] = part.COD_DDD_TEL_TITULAR;
                                    dtAtendimento["TELEFONE"] = part.TELEFONE_TITULAR;
                                    dtAtendimento["DDD_CEL"] = part.COD_DDD_CEL_TITULAR;
                                    dtAtendimento["CELULAR"] = part.CELULAR_TITULAR;
                                    dtAtendimento["COD_PLANO"] = part.COD_PLANO_PERIODO;
                                    dtAtendimento["DES_PLANO"] = part.DES_PLANO_PERIODO;
                                    dtAtendimento["DAT_ADESAO"] = part.DAT_ADESAO;
                                    dtAtendimento["dataNascBeneficiario"] = part.DAT_NASCIMENTO;

                                }
                                else
                                {

                                    part = pParticipantes.ListarPorParticipanteSemPlano(dtAtendimento["codigoBeneficiario"].ToString());

                                    if (part == null)
                                    {
                                        
                                    }


                                    dtAtendimento["COD_EMPRS"] = part.COD_EMPRS;
                                    dtAtendimento["NUM_MATRICULA"] = part.NUM_RGTRO_EMPRG;
                                    dtAtendimento["SUB_MATRICULA"] = part.NUM_SUB_MATRIC;
                                    dtAtendimento["Nomeparticipante"] = part.NOM_PARTICIP;
                                    dtAtendimento["DDD"] = part.COD_DDD_TEL_TITULAR;
                                    dtAtendimento["TELEFONE"] = part.TELEFONE_TITULAR;
                                    dtAtendimento["DDD_CEL"] = part.COD_DDD_CEL_TITULAR;
                                    dtAtendimento["CELULAR"] = part.CELULAR_TITULAR;
                                    dtAtendimento["COD_PLANO"] = "0";
                                    dtAtendimento["DES_PLANO"] = "SEM PLANO NO PERÍODO";
                                    dtAtendimento["DAT_ADESAO"] = part.DAT_ADESAO;
                                    dtAtendimento["dataNascBeneficiario"] = part.DAT_NASCIMENTO;

                                }

                            }

                        }


                        DataTable dtSUS = new DataTable();
                        dtSUS.Columns.Add("numeroOficio", typeof(string));
                        dtSUS.Columns.Add("numeroProcesso", typeof(string));
                        dtSUS.Columns.Add("numeroABI", typeof(string));
                        dtSUS.Columns.Add("numero", typeof(string));
                        dtSUS.Columns.Add("tipo", typeof(string));
                        dtSUS.Columns.Add("codigoBeneficiario", typeof(string));
                        dtSUS.Columns.Add("codigoCCO", typeof(string));
                        dtSUS.Columns.Add("competencia", typeof(string));
                        dtSUS.Columns.Add("dataInicioAtendimento", typeof(string));
                        dtSUS.Columns.Add("dataFimAtendimento", typeof(string));
                        dtSUS.Columns.Add("dataNascBeneficiario", typeof(string));
                        dtSUS.Columns.Add("Cod_Emp", typeof(string));
                        dtSUS.Columns.Add("matricula", typeof(string));
                        dtSUS.Columns.Add("Sub_Matricula", typeof(string));
                        dtSUS.Columns.Add("nomebeneficiario", typeof(string));
                        dtSUS.Columns.Add("DDD", typeof(string));
                        dtSUS.Columns.Add("Tel_Benef", typeof(string));
                        dtSUS.Columns.Add("DDD_Celular", typeof(string));
                        dtSUS.Columns.Add("Tel_Celular_Benef", typeof(string));
                        dtSUS.Columns.Add("Data_Cancelamento", typeof(string));
                        dtSUS.Columns.Add("Cod_Plano", typeof(string));
                        dtSUS.Columns.Add("Desc_Plano", typeof(string));
                        dtSUS.Columns.Add("DAT_ADESAO", typeof(string));
                        dtSUS.Columns.Add("nomeUPS", typeof(string));
                        dtSUS.Columns.Add("municipio", typeof(string));
                        dtSUS.Columns.Add("codigoUF", typeof(string));
                        dtSUS.Columns.Add("codigoProcedimento", typeof(string));
                        dtSUS.Columns.Add("descricaoProcedimento", typeof(string));
                        dtSUS.Columns.Add("valorProcedimento", typeof(string));
                        dtSUS.Columns.Add("ValorTotal", typeof(string));
                        dtSUS.Columns.Add("GLOSA", typeof(string));
                        dtSUS.Columns.Add("Chamado", typeof(string));
                        dtSUS.Columns.Add("Resposta", typeof(string));
                        dtSUS.Columns.Add("%Limitador", typeof(string));
                        dtSUS.Columns.Add("Co_participação", typeof(string));
                        dtSUS.Columns.Add("A_deduzir", typeof(string));
                        dtSUS.Columns.Add("PagamentoSUS", typeof(string));
                        dtSUS.Columns.Add("Impugnação", typeof(string));
                        dtSUS.Columns.Add("S_Impug_motiv", typeof(string));
                        dtSUS.Columns.Add("Mensalidade_salário", typeof(string));




                        DataTable dtProcedimentos = dsXML.Tables["procedimento"];
                        DataTable dtPrestadores = dsXML.Tables["prestador"];
                        DataTable dtABI = dsXML.Tables["ABI"];
                        foreach (DataRow drProcedimento in dtProcedimentos.Rows)
                        {

                            DataRow[] drAtendimento = dtAtendimentos.Select("numero = " + drProcedimento["numero"] + " and competencia = " + drProcedimento["competencia"]);
                            DataRow[] drABI = dtABI.Select();

                            if (drAtendimento.Length > 0)
                            {
                                foreach (DataRow dr in drAtendimento)
                                {

                                    //if (dr["numero"].ToString() == "3514119619087")
                                    //{

                                    DataRow drNew = dtSUS.NewRow();

                                    drNew["numeroOficio"] = drABI[0]["numeroOficio"].ToString();
                                    //verifa o tamanho do campo para não estourar na tabela
                                    drNew["numeroProcesso"] = CortaTexto(drABI[0]["numeroProcesso"].ToString(), 30);
                                    drNew["numeroABI"] = CortaTexto(drABI[0]["numeroABI"].ToString(), 5);

                                    if (dr["numero"].ToString().IndexOf("3514204415392") > -1)
                                    {
                                        drNew["numero"] = dr["numero"].ToString();
                                    }

                                    drNew["numero"] = CortaTexto(dr["numero"].ToString(), 20);
                                    drNew["tipo"] = (dr["tipo"] != null ? CortaTexto(dr["tipo"].ToString().Trim(), 20) : "");
                                    drNew["codigobeneficiario"] = (dr["codigoBeneficiario"] != null ? dr["codigoBeneficiario"].ToString().Trim() : "");
                                    drNew["codigoCCO"] = (dr["codigoCCO"] != null ? dr["codigoCCO"].ToString().Trim() : "");
                                    drNew["competencia"] = (dr["competencia"] != null ? CortaTexto(dr["competencia"].ToString().Trim(), 6) : "");
                                    drNew["dataInicioAtendimento"] = (dr["dataInicioAtendimento"] != null ? dr["dataInicioAtendimento"].ToString().Trim() : "");
                                    drNew["dataFimAtendimento"] = (dr["dataFimAtendimento"] != null ? dr["dataFimAtendimento"].ToString().Trim() : "");
                                    drNew["dataNascBeneficiario"] = (dr["dataNascBeneficiario"] != null ? dr["dataNascBeneficiario"].ToString().Trim() : "");
                                    drNew["Cod_Emp"] = (dr["COD_EMPRS"] != null ? CortaTexto(dr["COD_EMPRS"].ToString().Trim(), 3) : "");
                                    drNew["matricula"] = (dr["NUM_MATRICULA"] != null ? CortaTexto(dr["NUM_MATRICULA"].ToString().Trim(), 15) : "");
                                    drNew["Sub_Matricula"] = (dr["SUB_MATRICULA"] != null ? CortaTexto(dr["SUB_MATRICULA"].ToString().Trim(), 2) : "");
                                    drNew["nomebeneficiario"] = (dr["Nomeparticipante"] != null ? CortaTexto(dr["Nomeparticipante"].ToString().Trim(), 150) : "");
                                    drNew["DDD"] = (dr["DDD"] != null ? CortaTexto(dr["DDD"].ToString().Trim(), 3) : "");
                                    drNew["Tel_Benef"] = (dr["TELEFONE"] != null ? CortaTexto(dr["TELEFONE"].ToString().Trim(), 10) : "");
                                    drNew["DDD_Celular"] = (dr["DDD_CEL"] != null ? CortaTexto(dr["DDD_CEL"].ToString().Trim(), 3) : "");
                                    drNew["Tel_Celular_Benef"] = (dr["CELULAR"] != null ? CortaTexto(dr["CELULAR"].ToString().Trim(), 10) : "");
                                    drNew["Cod_Plano"] = (dr["COD_PLANO"] != null ? CortaTexto(dr["COD_PLANO"].ToString().Trim(), 3) : "");
                                    drNew["Desc_Plano"] = (dr["DES_PLANO"] != null ? CortaTexto(dr["DES_PLANO"].ToString().Trim(), 150) : "");
                                    drNew["DAT_ADESAO"] = (dr["DAT_ADESAO"] != null ? dr["DAT_ADESAO"].ToString().Trim() : "");
                                    drNew["codigoProcedimento"] = (drProcedimento["codigoProcedimento"] != null ? CortaTexto(drProcedimento["codigoProcedimento"].ToString().Trim(), 10) : "");
                                    drNew["descricaoProcedimento"] = (drProcedimento["descricaoProcedimento"] != null ? CortaTexto(drProcedimento["descricaoProcedimento"].ToString().Trim(), 150) : "");
                                    drNew["valorProcedimento"] = (drProcedimento["valorProcedimento"] != null ? drProcedimento["valorProcedimento"].ToString().Trim().Replace('.', ',') : "");


                                    DataRow[] drPrestador = dtPrestadores.Select("CNES = " + dr["CNES"]);

                                    if (drPrestador.Length > 0)
                                    {
                                        drNew["nomeUPS"] = (drPrestador[0]["nomeUPS"] != null ? CortaTexto(drPrestador[0]["nomeUPS"].ToString().Trim(), 80) : "");
                                        drNew["municipio"] = (drPrestador[0]["municipio"] != null ? CortaTexto(drPrestador[0]["municipio"].ToString().Trim(), 80) : "");
                                        drNew["codigoUF"] = (drPrestador[0]["codigoUF"] != null ? CortaTexto(drPrestador[0]["codigoUF"].ToString().Trim(), 2) : "");
                                    }


                                    dtSUS.Rows.Add(drNew);
                                    //    }

                                }
                            }
                            else
                            {
                                lblRegistros.Visible = true;
                                lblRegistros.Text = " Erro no Processamento.";
                            }


                        }

                        //dtPrestadores = PreparaPrestadores(dtExcelPrestadores);

                        //DataTable dtDistribuicao = null;
                        string sXml = "";
                        int cReg = 0;

                        //if (res.Ok)
                        //{
                        string consultaABI;


                        Gravar(dtSUS);
                        PreparaDownloadXls(dtSUS);
                        lblRegistros.Text = String.Format("Fim do Processamento.");
                        lblRegistros.ForeColor = System.Drawing.Color.Green;
                        lblRegistros.Visible = true;
                        //}
                        //else
                        //{
                        //    //MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção:\\n" + res.Mensagem);
                        //    lblRegistros.Visible = true;
                        //    lblRegistros.Text = String.Format("{0} registros gerados<br/><br/>", cReg);
                        //    lblRegistros.Text += "Atenção:<br/><br/>" + res.Mensagem.Replace("\\n", "<br/>");
                        //}

                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
                    }
                    finally
                    {
                        fuCancelados.FileContent.Dispose();
                        fuCancelados.FileContent.Flush();
                        fuCancelados.PostedFile.InputStream.Flush();
                        fuCancelados.PostedFile.InputStream.Close();
                        File.Delete(path_xml);
                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nCarregue apenas arquivos Excel 2003 (.xls/.xlsx) ou superior!");
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upSimulacao, "Atenção\\n\\nSelecione as duas planilhas para continuar!");
            }

        }

        private string CortaTexto(string texto, int tamanho)
        {
            string ret = texto;
            if (texto.Length > tamanho)
            {
                texto = texto.Substring(0, tamanho);
            }

            return ret;


        }

        private DataSet ImportarXML(string path_xml)
        {
            DataSet dsRet = new DataSet();

            DataTable dtABI = new DataTable("ABI");
            dtABI.Columns.Add("numeroOficio", typeof(string));
            dtABI.Columns.Add("numeroProcesso", typeof(string));
            dtABI.Columns.Add("numeroABI", typeof(string));

            dsRet.Tables.Add(ReadXmlFile(path_xml, dtABI));

            //DataColumn[] dcColunas = new DataColumn[11];
            DataTable dtAtendimento = new DataTable("atendimento");
            dtAtendimento.Columns.Add("competencia", typeof(string));
            dtAtendimento.Columns.Add("tipo", typeof(string));
            dtAtendimento.Columns.Add("numero", typeof(string));
            dtAtendimento.Columns.Add("dataInicioAtendimento", typeof(string));
            dtAtendimento.Columns.Add("dataFimAtendimento", typeof(string));
            dtAtendimento.Columns.Add("caraterAtendimento", typeof(string));
            dtAtendimento.Columns.Add("codigoBeneficiario", typeof(string));
            dtAtendimento.Columns.Add("codigoCCO", typeof(string));
            dtAtendimento.Columns.Add("dataNascBeneficiario", typeof(string));
            dtAtendimento.Columns.Add("valorTotal", typeof(string));
            dtAtendimento.Columns.Add("CNES", typeof(string));
            // CAMPOS QUE SERÃO PREENCHUIDOS PELO RESULTADO DA VW

            dtAtendimento.Columns.Add("COD_EMPRS", typeof(string));
            dtAtendimento.Columns.Add("NUM_MATRICULA", typeof(string));
            dtAtendimento.Columns.Add("SUB_MATRICULA", typeof(string));
            dtAtendimento.Columns.Add("Nomeparticipante", typeof(string));
            dtAtendimento.Columns.Add("DDD", typeof(string));
            dtAtendimento.Columns.Add("TELEFONE", typeof(string));
            dtAtendimento.Columns.Add("DDD_CEL", typeof(string));
            dtAtendimento.Columns.Add("CELULAR", typeof(string));
            dtAtendimento.Columns.Add("COD_PLANO", typeof(string));
            dtAtendimento.Columns.Add("DES_PLANO", typeof(string));
            dtAtendimento.Columns.Add("DAT_ADESAO", typeof(string));


            dsRet.Tables.Add(ReadXmlFile(path_xml, dtAtendimento, "CNES"));

            DataTable dtProcedimento = new DataTable("procedimento");
            dtProcedimento.Columns.Add("tipoProcedimento", typeof(string));
            dtProcedimento.Columns.Add("codigoProcedimento", typeof(string));
            dtProcedimento.Columns.Add("descricaoProcedimento", typeof(string));
            dtProcedimento.Columns.Add("quantidadeProcedimento", typeof(string));
            dtProcedimento.Columns.Add("valorProcedimento", typeof(string));
            dtProcedimento.Columns.Add("numero", typeof(string));
            dtProcedimento.Columns.Add("competencia", typeof(string));

            dsRet.Tables.Add(ReadXmlFile(path_xml, dtProcedimento, "numero", "competencia"));

            DataTable dtPrestador = new DataTable("prestador");
            dtPrestador.Columns.Add("CNES", typeof(string));
            dtPrestador.Columns.Add("nomeUPS", typeof(string));
            dtPrestador.Columns.Add("naturezaUPS", typeof(string));
            dtPrestador.Columns.Add("endereco", typeof(string));
            dtPrestador.Columns.Add("municipio", typeof(string));
            dtPrestador.Columns.Add("codigoUF", typeof(string));
            dtPrestador.Columns.Add("cep", typeof(string));
            dtPrestador.Columns.Add("quantidadePrestador", typeof(string));
            dtPrestador.Columns.Add("dataNascBeneficiario", typeof(string));
            dtPrestador.Columns.Add("valorTotalPrestador", typeof(string));

            dsRet.Tables.Add(ReadXmlFile(path_xml, dtPrestador));



            return dsRet;

        }

        private DataTable PreparaPrestadores(DataTable dtPrestadores)
        {
            //dtExcelPrestadores.Columns.Add("CNPJ/CPF_Number", typeof(Int64));
            //dtExcelPrestadores.Columns.Add("PJ_14_PF_11", typeof(int));
            //DataTable dtPrestadores = dtExcelPrestadores.Clone();            

            foreach (DataRow drItem in dtPrestadores.Rows)
            {
                int pos = drItem["codigoBeneficiario"].ToString().IndexOf('_');
                if (pos > -1)
                {
                    drItem["codigoBeneficiario"] = drItem["codigoBeneficiario"].ToString().Substring(0, pos);
                }
                //dtPrestadores.ImportRow(drItem);
            }

            return dtPrestadores;
        }

        public void Gravar(DataTable dt)
        {
            CadAnaliseSuSBLL bll = new CadAnaliseSuSBLL();
            bll.GravaDados(dt);

        }

        public void PreparaDownloadXls(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                ArquivoDownload XlsDistribuicao = new ArquivoDownload();
                XlsDistribuicao.dados = dt;
                XlsDistribuicao.nome_arquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_RELACAO_SUS.xls";
                Session[XlsDistribuicao.nome_arquivo] = XlsDistribuicao;
                AbrirNovaAba(this, "WebFile.aspx?dwFile=" + XlsDistribuicao.nome_arquivo, XlsDistribuicao.nome_arquivo);
            }

        }

        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            ParticipantesBLL bll = new ParticipantesBLL();

            bll.DeletaRegistros();
        }

        //public string VerificaABI(string ABI)
        //{
        //    ParticipantesBLL bll = new ParticipantesBLL();

        //    return bll.VerificaABI(ABI);
        //}

    }

}