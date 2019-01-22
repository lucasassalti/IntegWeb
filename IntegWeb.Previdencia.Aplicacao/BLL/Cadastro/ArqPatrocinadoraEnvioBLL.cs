using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.DAL.Cadastro;
using System.Configuration;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class ArqPatrocinadoraEnvioBLL : ArqPatrocinadoraEnvioDAL
    {
        private SqlConnectionStringBuilder builder;
        private ReportDocument ReportDoc;
        private Relatorio relatorio = new Relatorio();
        public string RelatorioID;
        public string caminho_servidor;

        ArquivoDownload adPdf = new ArquivoDownload();
        private DateTime _current_date_time = DateTime.Now;

        public Resultado SaveData(PRE_TBL_ARQ_ENVIO obj, int? iCOD_ARQ_ENV_REPASSE)
        {
            Resultado res = new Resultado();

            res = base.SaveData(obj, 2); //Grava na tabela de envio

            if (res.Ok)
            {
                if (iCOD_ARQ_ENV_REPASSE != null)
                {
                    res = base.SaveRepasse(iCOD_ARQ_ENV_REPASSE, obj.COD_ARQ_ENVIO); //Grava o código de envio na tabela de Repasse
                }

                if (res.Ok)
                {
                    return res;
                }
            }
            return res;
        }

        public PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA PacoteJaLiberado(short? sMes, short? sAno, short? sArea, short? sGrupo)
        {
            PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA ret = new PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA();
            ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();

            List<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA> ls_Areas = new List<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA>();
            ls_Areas = bll.GetDataControle(0, 9999, sGrupo, sAno, sMes, "COD_ARQ_AREA").ToList();

            ret.ANO_REF = sAno;
            ret.MES_REF = sMes;

            //string lista_gerados = "";
            //string lista_liberados = "";
            foreach (PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA a in ls_Areas)
            {

                if (a.QTD_GERADOS > 0)
                {
                    ret.QTD_GERADOS += a.QTD_GERADOS;
                    ret.DCR_ARQ_AREA += a.DCR_ARQ_AREA_SUB_AREA + "," + Environment.NewLine;
                }

                if (a.QTD_ENVIADOS > 0)
                {
                    ret.QTD_ENVIADOS += a.QTD_ENVIADOS;
                    ret.DCR_ARQ_SUB_AREA += a.DCR_ARQ_AREA_SUB_AREA + "," + Environment.NewLine;
                }

                if (a.QTD_PUBLICADOS > 0)
                {
                    ret.QTD_PUBLICADOS += a.QTD_PUBLICADOS;
                    //ret.DCR_ARQ_SUB_AREA += rep.DCR_ARQ_AREA_SUB_AREA + "," + Environment.NewLine;
                }

            }

            //if (!String.IsNullOrEmpty(lista_liberados))
            //{
            //MostraMensagem(lblMensagemNovo, "Atenção! Já existe(m) arquivo(s) de desconto ENVIADO(s) para as seguintes patrocinadoras: " + Environment.NewLine + Environment.NewLine + lista_liberados.Substring(0, lista_liberados.Length - 1) + "<br><br>" + "Impossível gerar novamente", "n_error");
            //ret = "Atenção! Já existe(m) arquivo(s) de desconto LIBERADO(s) das seguintes areas: " + Environment.NewLine + Environment.NewLine + lista_liberados.Substring(0, lista_liberados.Length - 1) + "<br><b>Impossível LIBERAR novamente</b>";
            //ret = "Atenção! O pacote mensal já foi liberado para a Patrocinadora<br><b>Impossível LIBERAR novamente</b>";
            //return false;
            //}
            //else if (!String.IsNullOrEmpty(lista_gerados))
            //{
            //    //ret = "Atenção! Já existe(m) arquivo(s) de desconto gerados(s) das seguintes areas: " + Environment.NewLine + Environment.NewLine + lista_gerados.Substring(0, lista_gerados.Length - 1) + "<br><br>" + "Para gerar novamente estes arquivos clique em <a href='#' onclick='btnReprocessar_click();'>'Reprocessar'</a>";
            //    ret = "Atenção! Já existe(m) arquivo(s) de desconto gerados(s) das seguintes areas: " + Environment.NewLine + Environment.NewLine + lista_gerados.Substring(0, lista_gerados.Length - 1) + "<br>Impossível LIBERAR novamente"; ;
            //    btnGerar.Visible = false;
            //    btnReprocessar.Visible = true;                
            //}

            return ret;
        }

        public List<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA> GetDataControle(int startRowIndex, int maximumRows, short? grupo, short? ano, short? mes, string sortParameter)
        {

            List<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA> ls_ENVIO_CONTROLE = base.GetDataControle2(startRowIndex, maximumRows, grupo, ano, mes, null, sortParameter);

            foreach (PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA env_ctrl in ls_ENVIO_CONTROLE)
            {
                List<PRE_TBL_ARQ_ENV_REPASSE_View> ls_Repasses = new List<PRE_TBL_ARQ_ENV_REPASSE_View>();
                List<PRE_TBL_ARQ_ENVIO_View> ls_Arquivos = new List<PRE_TBL_ARQ_ENVIO_View>();
                //ls_Arquivos = GetWhere(mes, ano, null, null, grupo, 1, env_ctrl.COD_ARQ_AREA, null, null).ToList();
                ls_Repasses = base.GetWhereRepasse(env_ctrl.COD_ARQ_AREA, grupo, mes, ano).ToList();
                env_ctrl.ANO_REF = ano;
                env_ctrl.MES_REF = mes;
                if (ls_Repasses.Count() > 0)
                {
                    env_ctrl.QTD_GERADOS = ls_Repasses.Count();
                }

                //STATUS 2: ENVIADO
                ls_Arquivos = GetWhere(mes, ano, null, null, grupo, 2, env_ctrl.COD_ARQ_AREA, null, null).ToList();
                if (ls_Arquivos.Count() > 0)
                {
                    env_ctrl.QTD_ENVIADOS = ls_Arquivos.Count();
                }

                env_ctrl.lstARQUIVOS_ENVIO = ls_Arquivos;

                //STATUS 3: PUBLICADO
                ls_Arquivos = GetWhere(mes, ano, null, null, grupo, 3, env_ctrl.COD_ARQ_AREA, null, null).ToList();
                if (ls_Arquivos.Count() > 0)
                {
                    env_ctrl.QTD_PUBLICADOS = ls_Arquivos.Count();
                }

                //env_ctrl.lstARQUIVOS_ENVIO = ls_Arquivos;

                //List<PRE_TBL_ARQ_ENVIO_View> ls_ENVIADOS = GetWhere(mes, ano, null, null, grupo, 2, env_ctrl.COD_ARQ_AREA, null, null).ToList();
                //env_ctrl
            }

            return ls_ENVIO_CONTROLE;

        }

        //private int? Processa_Arquivos(List<PRE_TBL_ARQ_ENVIO_View> ls_GERADOS, short? COD_ARQ_AREA)
        //{
        //    int Cont = 0;
        //    switch (COD_ARQ_AREA)
        //    {
        //        //case 3:
        //        //case 5:
        //        default:
        //            Cont = ls_GERADOS.Count();
        //            //Cont = ls_GERADOS.Where(a => a.COD_ARQ_ENVIO_TIPO == 1).Count();
        //            //if (ls_GERADOS.Any(a => a.COD_ARQ_ENVIO_TIPO == 2))
        //            //{
        //            //    Cont++;
        //            //}
        //            break;
        //    }

        //    return Cont;
        //}

        public Resultado Publicar(short? ANO_REF,
                                  short? MES_REF,
                                  short? COD_GRUPO_EMPRS,
                                  string LOG_INCLUSAO)
        {
            Resultado res = new Resultado();

            //enviar_mail_patrocinadora(COD_GRUPO_EMPRS ?? 0, MES_REF ?? 0, ANO_REF ?? 0);
            //return res;

            List<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA> ls_Areas = new List<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA>();
            //ls_Arquivos = GetWhere(MES_REF, ANO_REF, null, null, COD_GRUPO_EMPRS, 2, null, null, null).ToList();
            ls_Areas = GetDataControle(0, 9999, COD_GRUPO_EMPRS, ANO_REF, MES_REF, "COD_ARQ_AREA").ToList();
            //ls_Areas = GetDataControle(0,9999,COD_GRUPO_EMPRS, ANO_REF, MES_REF, 8, "COD_ARQ_AREA").ToList();

            //if (ls_Areas.Any(e => e.QTD_GERADOS < 3))
            //{
            //    res.Erro("Ação não permitida. Uma ou mais areas não disponibilizaram o arquivo para envio.");
            //}

            //foreach (PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA ctrl_area in ls_Areas)
            //{
            // Consolidar areas:
            res = PreparaPacote();
            if (res.Ok)
            {
                string[] param = res.Mensagem.Split(',');
                res = ProcessaPacote(COD_GRUPO_EMPRS, ls_Areas, ANO_REF, MES_REF, param[1], LOG_INCLUSAO);

                if (res.Ok)
                {
                    res = PublicaPacote(param[1], param[2], COD_GRUPO_EMPRS, ANO_REF, MES_REF, LOG_INCLUSAO);
                    if (res.Ok)
                    {
                        foreach (PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA ctrl_area in ls_Areas)
                        {
                            FinalizaArquivoEnviado(ctrl_area.lstARQUIVOS_ENVIO, res.CodigoCriado, _current_date_time, LOG_INCLUSAO);
                        }
                    }
                }

            }
            //List<PRE_TBL_ARQ_ENVIO_View> ls_Arquivos = GetWhere(MES_REF, ANO_REF, null, null, COD_GRUPO_EMPRS, 2, ctrl_area.COD_ARQ_AREA, null, null).ToList();

            //}

            //PRE_TBL_ARQ_ENVIO_View Envio = base.GetLinha(COD_ARQ_AREA);

            return res;
        }

        private void FinalizaArquivoEnviado(List<PRE_TBL_ARQ_ENVIO_View> lstARQUIVOS_ENVIO, long CodigoCriado, DateTime DTH_INCLUSAO, string LOG_INCLUSAO)
        {
            foreach (PRE_TBL_ARQ_ENVIO_View arq in lstARQUIVOS_ENVIO)
            {
                PRE_TBL_ARQ_ENVIO uptEnvio = base.GetARQ_ENVIO(arq.COD_ARQ_ENVIO);
                uptEnvio.COD_ARQ_ENVIO_PAI = int.Parse(CodigoCriado.ToString());
                base.SaveData(uptEnvio, 3);
                PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
                envioHist.COD_ARQ_ENVIO = arq.COD_ARQ_ENVIO;
                envioHist.COD_ARQ_STATUS = 3;
                envioHist.DTH_INCLUSAO = DTH_INCLUSAO;
                envioHist.LOG_INCLUSAO = LOG_INCLUSAO;
                InsertHistorico(envioHist);
            }
        }

        private Resultado PreparaPacote()
        {
            Resultado res = new Resultado();
            string Pasta_Server = caminho_servidor + @"UploadFile";
            string Pkg_id = "pkg__" + System.DateTime.Now.ToFileTime();
            string zip_file = caminho_servidor + @"UploadFile\" + Pkg_id + ".zip";

            Pasta_Server = Pasta_Server + @"\" + Pkg_id;
            if (!Directory.Exists(Pasta_Server))
            {
                Directory.CreateDirectory(Pasta_Server);
            }
            res.Sucesso("Preparação do pacote iniciada," + Pasta_Server + "," + zip_file);
            return res;
        }

        private Resultado ProcessaPacote(short? COD_GRUPO_EMPRS,
                                        List<PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA> ls_Areas,
                                        short? ANO_REF,
                                        short? MES_REF,
                                        string Pasta_Server,
                                        string LOG_INCLUSAO)
        {
            Resultado res = new Resultado();
            //string Pasta_Server = caminho_servidor + @"UploadFile";
            //string Pkg_id = "pkg__" + System.DateTime.Now.ToFileTime();
            //string zip_file = caminho_servidor + @"UploadFile\" + Pkg_id + ".zip";

            //Pasta_Server = Pasta_Server + @"\" + Pkg_id;
            //if (!Directory.Exists(Pasta_Server))
            //{
            //    Directory.CreateDirectory(Pasta_Server);
            //}

            short? nCOD_ARQ_AREA = 0;
            bool bEMITIR_DETALHADO = false;
            bool bEMITIR_RESUMIDO = false;
            bool bEMITIR_MOVFIN = false;
            List<PRE_TBL_ARQ_ENVIO_View> lstATUALIZA_ARQ_ENVIO = new List<PRE_TBL_ARQ_ENVIO_View>();

            foreach (PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA ctrl_area in ls_Areas)
            {
                nCOD_ARQ_AREA = ctrl_area.COD_ARQ_AREA;
                foreach (PRE_TBL_ARQ_ENVIO_View arq in ctrl_area.lstARQUIVOS_ENVIO)
                {
                    switch (arq.COD_ARQ_ENVIO_TIPO)
                    {
                        case 1:
                            if (arq.COD_ARQ_SUB_TIPO == 1)
                            {
                                bEMITIR_DETALHADO = true;
                            }
                            else if (arq.COD_ARQ_SUB_TIPO == 2)
                            {
                                bEMITIR_RESUMIDO = true;
                            }
                            break;
                        case 2:
                            bEMITIR_MOVFIN = true;
                            break;
                        case 3:
                            if (arq.DAT_ARQUIVO == null) arq.DAT_ARQUIVO = new byte[] { };
                            File.WriteAllBytes(Pasta_Server + @"\" + arq.DCR_ARQ_ENVIO + "." + arq.DCR_ARQ_EXT, arq.DAT_ARQUIVO);
                            break;
                    }
                    lstATUALIZA_ARQ_ENVIO.Add(arq);
                }
            }

            if (bEMITIR_DETALHADO)
            {
                // Rel_EnvArq_Detalhe
                InicializaRelatorio(1, COD_GRUPO_EMPRS, ANO_REF, MES_REF);
                ExportarRelatorioPdf(Pasta_Server + @"\" + adPdf.nome_arquivo);
            }

            if (bEMITIR_RESUMIDO)
            {
                // Rel_EnvArq_Resumo
                InicializaRelatorio(2, COD_GRUPO_EMPRS, ANO_REF, MES_REF);
                ExportarRelatorioPdf(Pasta_Server + @"\" + adPdf.nome_arquivo);
            }

            if (bEMITIR_MOVFIN)
            {
                ArqPatrocinadoraRepasseBLL repassBLL = new ArqPatrocinadoraRepasseBLL();
                foreach (string _ext in repassBLL.getSchemaExts(COD_GRUPO_EMPRS))
                {
                    adPdf.caminho_arquivo = "vb_Movfin_{COD_EMPRS}_" + _current_date_time.ToString("ddMMyyyy") + _ext;
                    //vb_Movfin_001_13102016

                    //Condição para emitir os arquivos do Grupo CPFL por área/produto não por empresa
                    //if (COD_GRUPO_EMPRS == 2)
                    //{
                    //    foreach (PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA area in ls_Areas)
                    //    {
                    //        string nom_arquivo = adPdf.caminho_arquivo.Replace("{COD_EMPRS}", area.DCR_ARQ_AREA.ToString());
                    //        repassBLL.GeraArquivoRepasse(Pasta_Server + @"\" + nom_arquivo,
                    //             COD_GRUPO_EMPRS,
                    //             null,
                    //             ANO_REF,
                    //             MES_REF,
                    //             area.COD_ARQ_AREA,
                    //             _current_date_time,
                    //             _ext);
                    //    }
                    //    continue;
                    //}

                    foreach (FCESP_GRUPO_EMP_View EMPRS in base.GetGrupoDdl(COD_GRUPO_EMPRS))
                    {
                        string nom_arquivo = adPdf.caminho_arquivo.Replace("{COD_EMPRS}", EMPRS.EMPRESA.ToString().PadLeft(3, '0'));
                        repassBLL.GeraArquivoRepasse(Pasta_Server + @"\" + nom_arquivo,
                                                     COD_GRUPO_EMPRS,
                                                     Util.TryParseShort(EMPRS.EMPRESA.ToString()),
                                                     ANO_REF,
                                                     MES_REF,
                                                     null,
                            //(ctrl_area.COD_ARQ_AREA == 0) ? null : nCOD_ARQ_AREA,
                                                     _current_date_time,
                                                     _ext);
                    }
                }
            }

            res.Sucesso("Pacote processado.");

            return res;
        }

        private Resultado PublicaPacote(string Pasta_Server,
                                        string zip_file,
                                        short? COD_GRUPO_EMPRS,
                                        short? ANO_REF,
                                        short? MES_REF,
                                        string LOG_INCLUSAO)
        {
            Resultado res = new Resultado();
            if (Directory.GetFiles(Pasta_Server).Length > 0)
            {
                ZipFile.CreateFromDirectory(Pasta_Server, zip_file);
                byte[] data = Util.File2Memory(zip_file);
                PRE_TBL_ARQ_ENVIO newEnvio = new PRE_TBL_ARQ_ENVIO();
                newEnvio.COD_ARQ_ENVIO = 0; // bll.GetMaxPkEnvio();
                newEnvio.MES_REF = MES_REF;
                newEnvio.ANO_REF = ANO_REF;
                newEnvio.COD_ARQ_AREA_ORIG = 1; //Funcesp
                newEnvio.COD_ARQ_AREA_DEST = 2; //Patrocinadora
                newEnvio.COD_GRUPO_EMPRS = COD_GRUPO_EMPRS;
                newEnvio.COD_ARQ_ENVIO_TIPO = 3; //Outros
                newEnvio.DCR_ARQ_ENVIO = String.Format("Mov_Financeiro_Funcesp_{0}_{1}", MES_REF.ToString().PadLeft(2, '0'), ANO_REF.ToString().PadLeft(4, '0'));
                newEnvio.DCR_CAMINHO_ARQUIVO = newEnvio.DCR_ARQ_ENVIO;
                newEnvio.DCR_ARQ_EXT = "zip";
                //obj.COD_ARQ_ENV_REPASSE = Util.String2Int32(((Label)row.FindControl("lblCodRepasse")).Text);
                newEnvio.DAT_ARQUIVO = data;
                newEnvio.DTH_INCLUSAO = System.DateTime.Now;
                newEnvio.LOG_INCLUSAO = LOG_INCLUSAO;
                res = base.SaveData(newEnvio, 3);

                enviar_email_patrocinadora(COD_GRUPO_EMPRS ?? 0, MES_REF ?? 0, ANO_REF ?? 0);

                PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
                envioHist.COD_ARQ_ENVIO = newEnvio.COD_ARQ_ENVIO;
                envioHist.COD_ARQ_STATUS = 3;
                envioHist.DTH_INCLUSAO = newEnvio.DTH_INCLUSAO;
                envioHist.LOG_INCLUSAO = newEnvio.LOG_INCLUSAO;
                InsertHistorico(envioHist);
            }
            else
            {
                res.Erro("Atenção! Não foram gerados arquivos para envio");
            }

            if (Directory.Exists(Pasta_Server))
            {
                Directory.Delete(Pasta_Server, true);
            }

            return res;
        }

        private void enviar_email_patrocinadora(short COD_GRUPO_EMPRS, short MES_REF, short ANO_REF)
        {
            Email mail_util = new Email();
            string corpo_email = Util.carrega_resource("IntegWeb.Previdencia.Aplicacao.MODELOS.email_patrocinadora_mov_mensal.html");
            Stream assinatura = Util.carrega_resource_stream("IntegWeb.Previdencia.Aplicacao.MODELOS.assinatura_email_portal.jpg");

            ArqParametrosDAL paramDAL = new ArqParametrosDAL();
            List<ArqParametrosDAL.PRE_TBL_ARQ_PARAM_view> lstPAram = paramDAL.GetWhere("EMAIL_PATROCINADORA", COD_GRUPO_EMPRS, null, null).ToList();
            string emailDestinatario = ConfigurationSettings.AppSettings["DestArqPatroc"];

            if (lstPAram.FirstOrDefault() != null &&
                ConfigurationSettings.AppSettings["Config"].ToString().Equals("P"))
            {
                emailDestinatario = lstPAram.FirstOrDefault().DCR_PARAM;
            }

            corpo_email = corpo_email.Replace("{BOM_DIA_TARDE}", mail_util.Bom_Dia_Tarde_Noite());
            corpo_email = corpo_email.Replace("{MES_REF}", MES_REF.ToString("00"));
            corpo_email = corpo_email.Replace("{ANO_REF}", ANO_REF.ToString("0000"));

            mail_util.EnviaEmail(emailDestinatario, "Portal Funcesp <atendimento@funcesp.com.br>",
                                 "Portal Funcesp - Novo arquivo disponivel para você", corpo_email, "", assinatura, true);
        }

        public System.IO.Stream ExportarRelatorioPdf()
        {

            InicializaRpt();

            foreach (var p in relatorio.parametros)
            {
                ReportDoc.SetParameterValue(p.parametro, p.valor);
            }

            return ReportDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        }

        public void ExportarOutrosArquivos(string caminho_arquivo)
        {

            InicializaRpt();

            foreach (var p in relatorio.parametros)
            {
                ReportDoc.SetParameterValue(p.parametro, p.valor);
            }

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = caminho_arquivo;
            CrExportOptions = ReportDoc.ExportOptions;//Report document  object has to be given here
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;

            ReportDoc.Export();

            //((BasePage)this.Page).ResponsePdf(adExtratoPDF.caminho_arquivo, adExtratoPDF.nome_arquivo);

            //ResponsePdf(adExtratoPDF.caminho_arquivo, adExtratoPDF.nome_arquivo);

        }

        public void ExportarRelatorioPdf(string caminho_arquivo)
        {

            InicializaRpt();

            foreach (var p in relatorio.parametros)
            {
                ReportDoc.SetParameterValue(p.parametro, p.valor);
            }

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = caminho_arquivo;
            CrExportOptions = ReportDoc.ExportOptions;//Report document  object has to be given here
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;

            ReportDoc.Export();

            //((BasePage)this.Page).ResponsePdf(adExtratoPDF.caminho_arquivo, adExtratoPDF.nome_arquivo);

            //ResponsePdf(adExtratoPDF.caminho_arquivo, adExtratoPDF.nome_arquivo);

        }

        public void InicializaRpt()
        {
            //Session[RelatorioID + "cache"] = _relatorio;

            ReportDoc = new ReportDocument();

            String Relatorio_caminho = relatorio.arquivo;
            //Relatorio_caminho = System.Web.HttpContext.Current.Server.MapPath(@rel_arqenv_detalhe);

            TableLogOnInfo tableLogOnInfo = null;

            ReportDoc.FileName = Relatorio_caminho;

            if (builder == null)
            {
                builder = new SqlConnectionStringBuilder(ConfigAplication.GetConnectString().ToString().Replace(";Unicode=True", ""));
            }

            foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in ReportDoc.Database.Tables)
            {
                tableLogOnInfo = tbl.LogOnInfo;
                tableLogOnInfo.ConnectionInfo.ServerName = builder.DataSource;
                tableLogOnInfo.ConnectionInfo.DatabaseName = "";
                tableLogOnInfo.ConnectionInfo.UserID = builder.UserID;
                tableLogOnInfo.ConnectionInfo.Password = builder.Password;
                tbl.ApplyLogOnInfo(tableLogOnInfo);
            }

            //foreach (ReportDocument rdSub in relatorio.Subreports)
            //{
            //    foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in rdSub.Database.Tables)
            //    {
            //        tableLogOnInfo = tbl.LogOnInfo;
            //        tableLogOnInfo.ConnectionInfo.ServerName = builder.DataSource;
            //        tableLogOnInfo.ConnectionInfo.DatabaseName = "";
            //        tableLogOnInfo.ConnectionInfo.UserID = builder.UserID;
            //        tableLogOnInfo.ConnectionInfo.Password = builder.Password;
            //        tbl.ApplyLogOnInfo(tableLogOnInfo);
            //    }
            //    rdSub.VerifyDatabase();
            //}

            ReportDoc.SetDatabaseLogon(builder.UserID, builder.Password, builder.DataSource, "");

        }

        private bool InicializaRelatorio(short iTipoRelatorio, short? COD_GRUPO_EMPRS, short? ANO_REF, short? MES_REF)
        {

            //if (ValidarCampos())
            //{

            //_relatorio.titulo = rel_arqenv_detalhe;
            //_relatorio.parametros = new List<Parametro>();

            switch (iTipoRelatorio)
            {
                case 1:
                    relatorio.arquivo = caminho_servidor + @"Relatorios\Capitalizacao\Rel_EnvArq_Detalhe.rpt";
                    relatorio.parametros.Add(new Parametro() { parametro = "pMES_ANO", valor = MES_REF.ToString().PadLeft(2, '0') + ANO_REF.ToString().PadLeft(4, '0') });
                    relatorio.parametros.Add(new Parametro() { parametro = "pCOD_GRUPO_EMPRS", valor = COD_GRUPO_EMPRS.ToString() });
                    relatorio.parametros.Add(new Parametro() { parametro = "pCOD_AREA", valor = "0" });
                    relatorio.parametros.Add(new Parametro() { parametro = "pEmpresa", valor = "0" });
                    adPdf.nome_arquivo = "EnvArq_Detalhe_" + _current_date_time.ToString("ddMMyyyy") + ".pdf";
                    adPdf.caminho_arquivo = ""; //@"~/UploadFile/" + adPdf.nome_arquivo; //Server.MapPath(@"UploadFile\") + adPdf.nome_arquivo;
                    break;
                case 2:
                    relatorio.arquivo = caminho_servidor + @"Relatorios\Capitalizacao\Rel_EnvArq_Resumo.rpt";
                    relatorio.parametros.Add(new Parametro() { parametro = "pMES_ANO", valor = MES_REF.ToString().PadLeft(2, '0') + ANO_REF.ToString().PadLeft(4, '0') });
                    relatorio.parametros.Add(new Parametro() { parametro = "pCOD_GRUPO_EMPRS", valor = COD_GRUPO_EMPRS.ToString() });
                    relatorio.parametros.Add(new Parametro() { parametro = "pCOD_AREA", valor = "0" });
                    relatorio.parametros.Add(new Parametro() { parametro = "pEmpresa", valor = "0" });
                    adPdf.nome_arquivo = "EnvArq_Resumo_" + _current_date_time.ToString("ddMMyyyy") + ".pdf";
                    adPdf.caminho_arquivo = ""; //@"~/UploadFile/" + adPdf.nome_arquivo;
                    break;
            }
            //Session[relatorio_nome] = relatorio;
            //ReportCrystal.RelatorioID = relatorio_nome;
            return true;

            //}
            //else return false;


        }

        public new List<PRE_TBL_ARQ_AREA_View> GetAreaDdl(ConectaAD user = null)
        {

            List<PRE_TBL_ARQ_AREA_View> lsArea = base.GetAreaDdl();

            if (user != null)
            {
                //if (user.descricao_status.ToUpper().Contains("SEGURIDADE"))
                if (user.departamento.ToUpper().Contains("CAPITALIZAÇÃO"))
                {
                    lsArea = lsArea.Where(i => (i.COD_ARQ_AREA >= 3 && i.COD_ARQ_AREA <= 6) || i.COD_ARQ_AREA == 9).ToList();
                }
                else if (user.departamento.ToUpper().Contains("SAÚDE"))
                {
                    lsArea = lsArea.Where(i => (i.COD_ARQ_AREA == 8)).ToList();
                }
                if (user.departamento.ToUpper().Contains("FINANCEIRO"))
                {
                    lsArea = lsArea.Where(i => (i.COD_ARQ_AREA == 7)).ToList();
                }
            }

            return lsArea;
        }

        public ArquivoDownload GetArqDownload(int iCOD_ARQ_ENVIO)
        {
            PRE_TBL_ARQ_ENVIO_View ArqEnvio = new PRE_TBL_ARQ_ENVIO_View();
            ArquivoDownload adPdf = new ArquivoDownload();
            ArqEnvio = base.GetLinha(iCOD_ARQ_ENVIO);

            adPdf.nome_arquivo = ArqEnvio.DCR_ARQ_ENVIO + "." + ArqEnvio.DCR_ARQ_EXT;
            adPdf.caminho_arquivo = adPdf.nome_arquivo;
            adPdf.dados = ArqEnvio.DAT_ARQUIVO;
            //adPdf.modo_abertura
            //adPdf.opcao_arquivo

            return adPdf;
        }

        internal Resultado GerarComboArqGrupos(ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View repasse, short? pCOD_ARQ_AREA_ORIG, DateTime pDTH_INCLUSAO, string pLOG_INCLUSAO)
        {
            Resultado res = new Resultado();

            List<PRE_TBL_ARQ_ENVIO_View> lstEnvios =
                GetWhere(repasse.MES_REF, repasse.ANO_REF, null, null, repasse.COD_GRUPO_EMPRS, null, pCOD_ARQ_AREA_ORIG, null, null).ToList();

            int bEMITIR_DETALHADO = 2;
            int bEMITIR_RESUMIDO = 2;
            int bEMITIR_MOVFIN = 2;

            foreach (PRE_TBL_ARQ_ENVIO_View env in lstEnvios)
            {
                switch (env.COD_ARQ_STATUS)
                {
                    case 1:
                        if (env.COD_ARQ_ENVIO_TIPO == 1)
                        {
                            if (env.COD_ARQ_SUB_TIPO == 1)
                            {
                                bEMITIR_DETALHADO = 1;
                            }
                            else if (env.COD_ARQ_SUB_TIPO == 2)
                            {
                                bEMITIR_RESUMIDO = 1;
                            }
                        }
                        else if (env.COD_ARQ_ENVIO_TIPO == 2)
                        {
                            bEMITIR_MOVFIN = 1;
                        }
                        break;
                    case 2:
                        if (env.COD_ARQ_ENVIO_TIPO == 1)
                        {
                            if (env.COD_ARQ_SUB_TIPO == 1)
                            {
                                bEMITIR_DETALHADO = 0;
                            }
                            else if (env.COD_ARQ_SUB_TIPO == 2)
                            {
                                bEMITIR_RESUMIDO = 0;
                            }
                        }
                        else if (env.COD_ARQ_ENVIO_TIPO == 2)
                        {
                            bEMITIR_MOVFIN = 0;
                        }
                        break;
                }
            }

            if (bEMITIR_MOVFIN > 0)
            {

                PRE_TBL_ARQ_ENVIO newEnvio = Repasse2ArqEnvio(repasse,
                                                              2, // Tipo Arq de Repasse
                                                              Convert.ToInt16(repasse.COD_ARQ_ENV_REPASSE), // Sub Tipo = Cód. Arq Repasse
                                                              pCOD_ARQ_AREA_ORIG,
                                                              1, // Para Funcesp
                                                              repasse.DCR_ARQ_ENV_REPASSE,
                                                              pDTH_INCLUSAO,
                                                              pLOG_INCLUSAO);
                if (bEMITIR_MOVFIN > 1)
                {
                    res = SaveData(newEnvio, repasse.COD_ARQ_ENV_REPASSE);
                    //}
                    //else
                    //{
                    //    PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
                    //    envioHist.COD_ARQ_ENVIO = newEnvio.COD_ARQ_ENVIO;
                    //    envioHist.COD_ARQ_STATUS = 2;
                    //    envioHist.DTH_INCLUSAO = newEnvio.DTH_INCLUSAO;
                    //    envioHist.LOG_INCLUSAO = newEnvio.LOG_INCLUSAO;
                    //    InsertHistorico(envioHist);
                }
            }
            else
            {
                //res.Erro("Repasse já foi liberado.");
                res.Erro("Atenção! Já existe um arquivos de desconto Liberado para o grupo " + (repasse.COD_GRUPO_EMPRS ?? 0).ToString());
            }

            //if (bEMITIR_DETALHADO > 0)
            //{

            //    PRE_TBL_ARQ_ENVIO newEnvio = Repasse2ArqEnvio(repasse,
            //                                                  1, // Tipo Relatório
            //                                                  1, // Sub Tipo = Detalhado
            //                                                  pCOD_ARQ_AREA_ORIG,
            //                                                  1, // Para Funcesp
            ////                                                  "rpt_scr_envarq_detalhado_por_empresa_",
            //                                                  "Rel_detalhado_" + repasse.DCR_ARQ_ENV_REPASSE,
            //                                                  pDTH_INCLUSAO,
            //                                                  pLOG_INCLUSAO);
            //    if (bEMITIR_DETALHADO > 1)
            //    {
            //        res = SaveData(newEnvio);
            //    }
            //    else
            //    {
            //        PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
            //        envioHist.COD_ARQ_ENVIO = newEnvio.COD_ARQ_ENVIO;
            //        envioHist.COD_ARQ_STATUS = 2;
            //        envioHist.DTH_INCLUSAO = newEnvio.DTH_INCLUSAO;
            //        envioHist.LOG_INCLUSAO = newEnvio.LOG_INCLUSAO;
            //        InsertHistorico(envioHist);
            //    }
            //}
            //else
            //{
            //    res.Erro("Detalhado já foi liberado.");
            //}

            if (bEMITIR_RESUMIDO > 0)
            {

                PRE_TBL_ARQ_ENVIO newEnvio = Repasse2ArqEnvio(repasse,
                                                              1, // Tipo Relatório
                                                              2, // Sub Tipo = Resumido
                                                              pCOD_ARQ_AREA_ORIG,
                                                              1, // Para Funcesp
                    //"rpt_scr_envarq_resumo_por_empresa_",
                                                              "Rel_resumo_" + repasse.DCR_ARQ_ENV_REPASSE,
                                                              pDTH_INCLUSAO,
                                                              pLOG_INCLUSAO);
                if (bEMITIR_RESUMIDO > 1)
                {
                    res = SaveData(newEnvio, null);
                    //}
                    //else
                    //{
                    //    PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
                    //    envioHist.COD_ARQ_ENVIO = newEnvio.COD_ARQ_ENVIO;
                    //    envioHist.COD_ARQ_STATUS = 2;
                    //    envioHist.DTH_INCLUSAO = newEnvio.DTH_INCLUSAO;
                    //    envioHist.LOG_INCLUSAO = newEnvio.LOG_INCLUSAO;
                    //    InsertHistorico(envioHist);
                }
            }
            else
            {
                //res.Erro("Resumo já foi liberado.");
                res.Erro("Atenção! Já existe um resumo de relatório de desconto Liberado para o grupo " + (repasse.COD_GRUPO_EMPRS ?? 0).ToString());
            }

            return res;
        }

        private PRE_TBL_ARQ_ENVIO Repasse2ArqEnvio(ArqPatrocinadoraRepasseDAL.PRE_TBL_ARQ_ENV_REPASSE_View repasse,
                                                                                                    short COD_ARQ_ENVIO_TIPO,
                                                                                                    short? COD_ARQ_SUB_TIPO,
                                                                                                    short? COD_ARQ_AREA_ORIG,
                                                                                                    short? COD_ARQ_AREA_DEST,
                                                                                                    string DCR_ARQ_ENV_REPASSE,
                                                                                                    DateTime DTH_INCLUSAO,
                                                                                                    string LOG_INCLUSAO)
        {
            PRE_TBL_ARQ_ENVIO obj = new PRE_TBL_ARQ_ENVIO();
            obj.COD_ARQ_ENVIO = 0; // bll.GetMaxPkEnvio();
            obj.MES_REF = repasse.MES_REF;
            obj.ANO_REF = repasse.ANO_REF;
            obj.COD_ARQ_AREA_ORIG = COD_ARQ_AREA_ORIG;
            obj.COD_ARQ_AREA_DEST = COD_ARQ_AREA_DEST;
            obj.COD_GRUPO_EMPRS = repasse.COD_GRUPO_EMPRS;
            obj.COD_ARQ_ENVIO_TIPO = COD_ARQ_ENVIO_TIPO;
            obj.DCR_ARQ_ENVIO = DCR_ARQ_ENV_REPASSE;
            obj.COD_ARQ_SUB_TIPO = COD_ARQ_SUB_TIPO;
            obj.DTH_INCLUSAO = DTH_INCLUSAO;
            obj.LOG_INCLUSAO = LOG_INCLUSAO;
            return obj;
        }

        public Resultado Rejeitar(short? pANO_REF,
                                  short? pMES_REF,
                                  short? pCOD_GRUPO_EMPRS,
                                  short? pCOD_ARQ_AREA,
                                  string pLOG_INCLUSAO)
        {
            Resultado res = new Resultado();

            List<PRE_TBL_ARQ_ENVIO_View> lstEnvios =
                GetWhere(pMES_REF, pANO_REF, null, null, pCOD_GRUPO_EMPRS, 2, pCOD_ARQ_AREA, null, null).ToList();

            foreach (PRE_TBL_ARQ_ENVIO_View env in lstEnvios)
            {
                res = base.ExcluirEnvio(env.COD_ARQ_ENVIO, _current_date_time, pLOG_INCLUSAO, 4); //Rejeitado
            }
            return res;
        }
    }
}
