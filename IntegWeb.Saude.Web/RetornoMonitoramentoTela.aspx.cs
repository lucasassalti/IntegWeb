using IntegWeb.Saude.Aplicacao.BLL.ExigenciasLegais;
using IntegWeb.Entidades.Saude.ExigenciasLegais.MonitoramentoTISS;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Reflection;
using System.Xml;
using System.Collections;

namespace IntegWeb.Saude.Web
{
    public partial class RetornoMonitoramentoTela : BasePage
    {
        #region Atributos
        mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento obj = new mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento();
        #endregion

        #region Enventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpLoad_Click(object sender, EventArgs e)
        {
            using (FileUploadControl)
            {
                try
                {
                    if (FileUploadControl.HasFile)
                    {
                        if (FileUploadControl.PostedFile.ContentType == "application/octet-stream")
                        {
                            string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                            string caminho = Server.MapPath("Spool_Arquivos\\") + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
                            FileUploadControl.SaveAs(caminho);

                            if (Inserir(caminho))
                            {
                                MostraMensagemTelaUpdatePanel(upSys, "Arquivo " + Path.GetFileName(FileUploadControl.FileName).ToString() + " inserido com sucesso.");
                            }
                        }
                        else
                            MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\nCarregue apenas arquivos com extensão XTE.");
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\nSelecione o Arquivo para importação.");
                    }
                }
                catch (Exception ex)
                {
                    MostraMensagemTelaUpdatePanel(upSys, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
                }
            }

        }
        #endregion

        #region Métodos

        private bool Inserir(string caminhoArquivo)
        {
            if (Session["objUser"] != null)
            {
                string horainicio = DateTime.Now.ToShortTimeString();
                ConectaAD objad = (ConectaAD)Session["objUser"];

                //MENSAGEMENVIOANS
                mensagemEnvioANS msgenvio = null;
                try
                {
                    msgenvio = MapearObjetoMensagemEnvioANS(objad.login, caminhoArquivo);
                }
                catch (Exception e)
                {
                    MostraMensagemTelaUpdatePanel(upSys, "Erro ao criar MENSAGEMENVIOANS: " + e.Message);
                    return false;
                }
                RetornoMonitoramentoBLL msgEnvioBLL = new RetornoMonitoramentoBLL();
                Resultado resultado = msgEnvioBLL.Inserir(msgenvio);
                if (resultado.Ok)
                {
                    msgenvio.cod_retmonitiss = int.Parse(resultado.CodigoCriado.ToString());
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                //CRIA O DATASET COM O XML
                DataSet ds = null;
                try
                {
                    ds = CriarXmlDataSet(caminhoArquivo);
                }
                catch (Exception e)
                {
                    MostraMensagemTelaUpdatePanel(upSys, "Erro ao criar DataSet: " + e.Message);
                    return false;
                }

                //CRIA COLUNA COD_RETMONITISS
                for (int i = 0; i < ds.Tables.Count - 1; i++)
                {
                    ds.Tables[i].Columns.Add(new DataColumn("COD_RETMONITISS"));
                    for (int j = 0; j < ds.Tables[i].Rows.Count - 1; j++)
                    {
                        ds.Tables[i].Rows[j]["COD_RETMONITISS"] = msgenvio.cod_retmonitiss;
                    }
                }

                //DATATABLES
                DataTable dtCabecalho = ds.Tables["cabecalho"];
                DataTable dtIdentificacaoTransacao = ds.Tables["identificacaoTransacao"];
                DataTable dtMensagem = ds.Tables["mensagem"];
                DataTable dtAnsParaOperadora = ds.Tables["ansParaOperadora"];
                DataTable dtResumoProcessamento = ds.Tables["resumoProcessamento"];
                DataTable dtRegistrosRejeitados = ds.Tables["registrosRejeitados"];
                DataTable dtContratadoExecutante = ds.Tables["contratadoExecutante"];
                DataTable dtErrosGuia = ds.Tables["errosGuia"];
                DataTable dtErrosItensGuia = ds.Tables["errosItensGuia"];
                DataTable dtIdentProcedimento = ds.Tables["identProcedimento"];
                DataTable dtProcedimento = ds.Tables["procedimento"];
                DataTable dtRelacaoErros = ds.Tables["relacaoErros"];
                DataTable dtResumoProcessamentoTotais = ds.Tables["resumoProcessamentoTotais"];
                DataTable dtEpilogo = ds.Tables["epilogo"];
            

                #region INSERÇÃO VIA BULK COPY
                //CABEÇALHO
                resultado = new RetornoMonitoramentoCabecalhoBLL().Inserir(dtCabecalho);
                if (!resultado.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                //MENSAGEM
                resultado = new RetornoMonitoramentoMensagemBLL().Inserir(dtMensagem);
                if (!resultado.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                //ANS PARA OPERADORA
                resultado = new RetornoMonitoramentoAnsParaOperadoraBLL().Inserir(dtAnsParaOperadora);
                if (!resultado.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                //RESUMO PROCESSAMENTO
                resultado = new RetornoMonitoramentoResumoProcessamentoBLL().Inserir(dtResumoProcessamento);
                if (!resultado.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                //RESUMO PROCESSAMENTO TOTAIS
                resultado = new RetornoMonitoramentoResumoProcessamentoTotaisBLL().Inserir(dtResumoProcessamentoTotais);
                if (!resultado.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                //EPÍLOGO
                resultado = new RetornoMonitoramentoEpilogoBLL().Inserir(dtEpilogo);
                if (!resultado.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                //dt = ds.Tables["epilogo"];
                //epilogo epilogo = TrataEpilogo(dt, msgenvio);
                //resultado = new RetornoMonitoramentoBLL().Inserir(epilogo);
                //if (!resultado.Ok)
                //{
                //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                //    return false;
                //}

                #endregion

                #region INSERÇÃO VIA PROCEDURE
                //IDENTIFICAÇÃO DA TRANSAÇÃO
                resultado = new RetornoMonitoramentoIdentificacaoTransacaoBLL().Inserir(dtIdentificacaoTransacao);
                if (!resultado.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                //REGISTROS REJEITADOS
                //dt = ds.Tables["registrosRejeitados"];
                //mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados registrosRejeitados = TrataRegistrosRejeitados(dt, resumoProcessamento);
                //resultado = new RetornoMonitoramentoBLL().Inserir(registrosRejeitados);
                //if (!resultado.Ok)
                //{
                //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                //    return false;
                //}

                //CONTRATADO EXECUTANTE
                resultado = new IntegWeb.Saude.Aplicacao.BLL.ExigenciasLegais.RetornoMonitoramentoContratadoExecutanteBLL().Inserir(dtContratadoExecutante);
                if (!resultado.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                    return false;
                }

                #endregion
                //////CONTRATADO EXECUTANTE
                //dt = ds.Tables["contratadoExecutante"];
                //ct_monitoramentoGuiaDadosContratadoExecutante contratadoExecutante = TrataContratadoExecutante(dt, registrosRejeitados);
                //resultado = new RetornoMonitoramentoBLL().Inserir(contratadoExecutante);
                //if (!resultado.Ok)
                //{
                //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                //    return false;
                //}

                //////ERROS GUIA
                //dt = ds.Tables["errosGuia"];
                //mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosGuia errosGuia = TrataErrosGuia(dt, registrosRejeitados);
                //resultado = new RetornoMonitoramentoBLL().Inserir(errosGuia);
                //if (!resultado.Ok)
                //{
                //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                //    return false;
                //}

                //////ERROS ITENS GUIA
                //dt = ds.Tables["errosItensGuia"];
                //mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuia errosItensGuia = TrataErrosItensGuia(dt, registrosRejeitados);
                //resultado = new RetornoMonitoramentoBLL().Inserir(errosItensGuia);
                //if (!resultado.Ok)
                //{
                //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                //    return false;
                //}

                //////IDENTIFICAÇÃO PROCEDIMENTO
                //dt = ds.Tables["identProcedimento"];
                //ct_monitoramentoGuiaProcedimentosIdentProcedimento identProcedimento = TrataIdentProcedimento(dt, errosItensGuia);
                //resultado = new RetornoMonitoramentoBLL().Inserir(identProcedimento);
                //if (!resultado.Ok)
                //{
                //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                //    return false;
                //}

                //////PROCEDIMENTO
                //dt = ds.Tables["procedimento"];
                //ct_monitoramentoGuiaProcedimentosIdentProcedimentoProcedimento procedimento = TrataProcedimento(dt, identProcedimento);
                //resultado = new RetornoMonitoramentoBLL().Inserir(procedimento);
                //if (!resultado.Ok)
                //{
                //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                //    return false;
                //}

                //////RELAÇÃO ERROS
                //dt = ds.Tables["relacaoErros"];
                //mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuiaRelacaoErros relacaoErros = TrataRelacaoErros(dt, errosItensGuia);
                //resultado = new RetornoMonitoramentoBLL().Inserir(relacaoErros);
                //if (!resultado.Ok)
                //{
                //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
                //    return false;
                //}

            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            return true;
        }

        private DataSet CriarXmlDataSet(string caminho)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(caminho);

            return ds;
        }
        #endregion

        #region Mapeamento de objetos
        private mensagemEnvioANS MapearObjetoMensagemEnvioANS(string p_login, string caminho)
        {
            mensagemEnvioANS msgenvio = new mensagemEnvioANS();
            msgenvio.username = p_login;
            //XElement xmlpuro = XElement.Load(caminho);
            //msgenvio.xml = xmlpuro.Value;
            msgenvio.xml = caminho;

            return msgenvio;
        }

        private cabecalhoTransacao MapearObjetoCabecalho(DataSet ds, mensagemEnvioANS msgEnvioANS)
        {
            DataTable dt = ds.Tables["cabecalho"];

            cabecalhoTransacao cabecalho = new cabecalhoTransacao();
            cabecalho.cabecalho_Id = Convert.ToInt32(dt.Rows[0]["cabecalho_Id"].ToString());
            cabecalho.registroANS = dt.Rows[0]["registroans"].ToString();
            cabecalho.versaoPadrao = dt.Rows[0]["versaopadrao"] == "3.02.00" ? dm_versao_monitor.Item30200 : dm_versao_monitor.Item30201;
            cabecalho.COD_RETMONITISS = msgEnvioANS.cod_retmonitiss;

            
            cabecalhoTransacaoIdentificacaoTransacao identificacaoTransacao = MapearObjetoIdentificacaoTransacao(ds, cabecalho);

            cabecalho.identificacaoTransacao = identificacaoTransacao;

            return cabecalho;
        }

        private cabecalhoTransacaoIdentificacaoTransacao MapearObjetoIdentificacaoTransacao(DataSet ds, cabecalhoTransacao cabecalho)
        {
            DataTable dt = ds.Tables["identificacaoTransacao"];

            cabecalhoTransacaoIdentificacaoTransacao identificacaoTransacao = new cabecalhoTransacaoIdentificacaoTransacao();
            identificacaoTransacao.tipoTransacao = dm_tipoTransacaoANS.MONITORAMENTO;
            identificacaoTransacao.numeroLote = dt.Rows[0]["numeroLote"].ToString();
            identificacaoTransacao.competenciaLote = dt.Rows[0]["competenciaLote"].ToString();
            identificacaoTransacao.dataRegistroTransacao = DateTime.ParseExact(dt.Rows[0]["dataRegistroTransacao"].ToString(), "yyyy-MM-dd", null);
            identificacaoTransacao.horaRegistroTransacao = DateTime.ParseExact(dt.Rows[0]["horaRegistroTransacao"].ToString(), "HH:mm:ss", null);
            identificacaoTransacao.cabecalho_Id = cabecalho.cabecalho_Id;
            identificacaoTransacao.COD_RETMONITISS = cabecalho.COD_RETMONITISS;

            return identificacaoTransacao;
        }

        private mensagemEnvioANSMensagem MapearObjetoMensagem(DataSet ds, mensagemEnvioANS msgenvio)
        {
            DataTable dt = ds.Tables["mensagem"];
            mensagemEnvioANSMensagem mensagem = new mensagemEnvioANSMensagem();
            mensagem.mensagem_Id = Convert.ToInt32(dt.Rows[0]["mensagem_Id"].ToString());
            mensagem.COD_RETMONITISS = msgenvio.cod_retmonitiss;

            //ANS PARA OPERADORA
            mensagemEnvioANSMensagemAnsParaOperadora ansParaOperadora = MapearObjetoAnsParaOperadora(ds, mensagem);
            mensagem.Item = ansParaOperadora;

            return mensagem;
        }

        private mensagemEnvioANSMensagemAnsParaOperadora MapearObjetoAnsParaOperadora(DataSet ds, mensagemEnvioANSMensagem mensagem)
        {
            DataTable dt = ds.Tables["ansParaOperadora"];
            mensagemEnvioANSMensagemAnsParaOperadora ansParaOperadora = new mensagemEnvioANSMensagemAnsParaOperadora();

            ansParaOperadora.ansParaOperadora_Id = Convert.ToInt32(dt.Rows[0]["ansParaOperadora_Id"].ToString());
            ansParaOperadora.mensagem_Id = mensagem.mensagem_Id;
            ansParaOperadora.COD_RETMONITISS = mensagem.COD_RETMONITISS;

            //RESUMO PROCESSAMENTO
            mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento resumoProcessamento = MapearObjetoResumoProcessamento(ds, ansParaOperadora);
            //dt = ds.Tables["resumoProcessamento"];
            //mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento resumoProcessamento = TrataResumoProcessamento(dt, ansParaOperadora);
            //resultado = new RetornoMonitoramentoBLL().Inserir(resumoProcessamento);
            //if (!resultado.Ok)
            //{
            //    MostraMensagemTelaUpdatePanel(upSys, resultado.Mensagem);
            //    return false;
            //}

            return ansParaOperadora;
        }

        private mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento MapearObjetoResumoProcessamento(DataSet ds, mensagemEnvioANSMensagemAnsParaOperadora ansParaOperadora)
        {
            DataTable dt = ds.Tables["resumoProcessamento"];
            mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento resumoProcessamento = new mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento();

            resumoProcessamento.nomeArquivo = dt.Rows[0]["nomeArquivo"].ToString();
            resumoProcessamento.resumoProcessamento_Id = Convert.ToInt32(dt.Rows[0]["resumoProcessamento_Id"].ToString());
            // resumoProcessamento.arquivoProcessadoPelaANS = dt.Rows[0]["arquivoProcessadoPelaANS"].ToString();
            //resumoProcessamento.ansParaOperadora_Id = ansParaOperadora.ansParaOperadora_Id;

            List<mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados> registrosRejeitados =
                new List<mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados>();

            return resumoProcessamento;
        }

        private mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados TrataRegistrosRejeitados(DataTable dt, mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento resumoProcessamento)
        {
            throw new NotImplementedException();
        }

        private ct_monitoramentoGuiaDadosContratadoExecutante TrataContratadoExecutante(DataTable dt, mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados registrosRejeitados)
        {
            throw new NotImplementedException();
        }

        private mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosGuia TrataErrosGuia(DataTable dt, mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados registrosRejeitados)
        {
            throw new NotImplementedException();
        }

        private mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuia TrataErrosItensGuia(DataTable dt, mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados registrosRejeitados)
        {
            throw new NotImplementedException();
        }

        private ct_monitoramentoGuiaProcedimentosIdentProcedimento TrataIdentProcedimento(DataTable dt, mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuia errosItensGuia)
        {
            throw new NotImplementedException();
        }

        private ct_monitoramentoGuiaProcedimentosIdentProcedimentoProcedimento TrataProcedimento(DataTable dt, ct_monitoramentoGuiaProcedimentosIdentProcedimento identProcedimento)
        {
            throw new NotImplementedException();
        }

        private mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuiaRelacaoErros TrataRelacaoErros(DataTable dt, mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuia errosItensGuia)
        {
            throw new NotImplementedException();
        }

        private mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoResumoProcessamentoTotais TrataResumoProcessamentoTotais(DataTable dt, mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento resumoProcessamento)
        {
            throw new NotImplementedException();
        }

        private epilogo TrataEpilogo(DataTable dt, mensagemEnvioANS msgEnvioANS)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}