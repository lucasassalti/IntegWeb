using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Entidades.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Text;
using IntegWeb.Saude.Aplicacao;
using IntegWeb.Framework;
using System.Xml.Serialization;

namespace IntegWeb.Saude.Web
{
    public partial class PortalMobile : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            ProcPortalDAL obj = new ProcPortalDAL();

            DataTable dt = new DataTable();
            dt = obj.JOB_SAU_CLASSE_CONV();
            string dataHoraSegundos = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");


            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        var demoFile = archive.CreateEntry(@"gm_ms_tprede_" + dataHoraSegundos + ".csv");

                        var entryStream = demoFile.Open();
                        using (var streamWriter = new StreamWriter(entryStream, Encoding.UTF8))
                        {

                            streamWriter.WriteLine("\"classe\";\"descricao\";\"Versao_layout\";\"Empresa_id\"");

                            foreach (DataRow row in dt.Rows)
                            {

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {

                                    streamWriter.WriteLine(row[i].ToString());
                                }
                            }
                        }

                    }
                                        
                    using (var fileStream = new FileStream(@"C:\ArquivosMobileSaude\gm_ms_tprede_" + dataHoraSegundos + ".zip", FileMode.Create))
                    //using (var fileStream = new FileStream(@"D:\Users\F02581\Desktop\gm_ms_tprede_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);

                    }

                    //Enviar arquivo para o SFTP (Servidor Linux) Zipado
                    UploadSFTPBLL Upload = new UploadSFTPBLL();

                    //string host = "ms2.mobilesaude.com.br";
                    //string username = "funcesp";
                    //string password = "funcesp123";
                    //string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_tprede_" + dataHoraSegundos + ".zip";
                    ////string nomeArquivo = @"D:\Users\F02581\Desktop\gm_ms_tprede_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";


                    //Upload.SendSFTP(host, username, password, nomeArquivo);


                    //Servidor Windows homologação

                    string ftpIPServidor = "fcesporah001";
                    string ftpUsuarioID = "integ";
                    string ftpSenha = "newtst";
                    string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_tprede_" + dataHoraSegundos + ".zip";
                 //   string nomeArquivo = @"D:\Users\F02581\Desktop\gm_ms_tprede_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";

                 //   Upload.SendFTP(ftpIPServidor, ftpUsuarioID, ftpSenha, nomeArquivo);


                }

            }



            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message); ;
            }

            DataTable dtPLano = new DataTable();
            dtPLano = obj.JOB_SAU_CONV_PLANO();

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        var demoFile = archive.CreateEntry(@"gm_ms_Planos_" + dataHoraSegundos + ".csv");

                        var entryStream = demoFile.Open();
                        using (var streamWriter = new StreamWriter(entryStream, Encoding.UTF8))
                        {
                            streamWriter.WriteLine("\"codigo_legado\";\"descricao\";\"eletivo\";\"emergencia\" ;\"Versao_layout\";\"registro_plano_ans\";\"clas_p_fins_comerc\";\"situacao_plano_comerc\";\"Empresa_id\"");

                            foreach (DataRow row in dtPLano.Rows)
                            {

                                for (int i = 0; i < dtPLano.Columns.Count; i++)
                                {

                                    streamWriter.WriteLine(row[i].ToString());
                                }
                            }
                        }

                    }

                    using (var fileStream = new FileStream(@"C:\ArquivosMobileSaude\gm_ms_Planos_" + dataHoraSegundos + ".zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }

                    //Enviar arquivo para o FTP Zipado
                    UploadSFTPBLL Upload = new UploadSFTPBLL();

                    //string host = "ms2.mobilesaude.com.br";
                    //string username = "funcesp";
                    //string password = "funcesp123";
                    //string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_Planos_" + dataHoraSegundos + ".zip";
                    ////string nomeArquivo = @"D:\Users\F02581\Desktop\gm_ms_Planos_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";


                    //Upload.SendSFTP(host, username, password, nomeArquivo);

                    //Servidor Windows homologação

                    string ftpIPServidor = "fcesporah001";
                    string ftpUsuarioID = "integ";
                    string ftpSenha = "newtst";
                    string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_Planos_" + dataHoraSegundos + ".zip";

                 //   Upload.SendFTP(ftpIPServidor, ftpUsuarioID, ftpSenha, nomeArquivo);
                }

            }



            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message); ;
            }

            DataTable dtEspecCred = new DataTable();
            dtEspecCred = obj.JOB_SAU_ESPC_RED_CRED();

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        var demoFile = archive.CreateEntry(@"gm_ms_espec_" + dataHoraSegundos + ".csv");

                        var entryStream = demoFile.Open();
                        using (var streamWriter = new StreamWriter(entryStream, Encoding.UTF8))
                        {
                            streamWriter.WriteLine("\"codigo_CBO\";\"descricao\";\"Versao_layout\";\"Empresa_id\"");

                            foreach (DataRow row in dtEspecCred.Rows)
                            {

                                for (int i = 0; i < dtEspecCred.Columns.Count; i++)
                                {

                                    streamWriter.WriteLine(row[i].ToString());
                                }
                            }
                        }

                    }

                    using (var fileStream = new FileStream(@"C:\ArquivosMobileSaude\gm_ms_espec_" + dataHoraSegundos+ ".zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }

                    //Enviar arquivo para o FTP Zipado
                    UploadSFTPBLL Upload = new UploadSFTPBLL();

                    //string host = "ms2.mobilesaude.com.br";
                    //string username = "funcesp";
                    //string password = "funcesp123";
                    //string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_espec_" + dataHoraSegundos + ".zip";
                    ////string nomeArquivo = @"D:\Users\F02581\Desktop\gm_ms_espec_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";


                    //Upload.SendSFTP(host, username, password, nomeArquivo);

                    //Servidor Windows homologação

                    string ftpIPServidor = "fcesporah001";
                    string ftpUsuarioID = "integ";
                    string ftpSenha = "newtst";
                    string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_espec_" + dataHoraSegundos + ".zip";

                //    Upload.SendFTP(ftpIPServidor, ftpUsuarioID, ftpSenha, nomeArquivo);
                }

            }



            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message); ;
            }

            DataTable dtQualifCred = new DataTable();
            dtQualifCred = obj.JOB_SAU_QUALIF_REDCRED();

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        var demoFile = archive.CreateEntry(@"gm_ms_qualificacao_" + dataHoraSegundos + ".csv");

                        var entryStream = demoFile.Open();
                        using (var streamWriter = new StreamWriter(entryStream, Encoding.UTF8))
                        {
                            streamWriter.WriteLine("\"CPF_CNPJ\";\"CODIGO_CBO\";\"SEQUENCIAL_ENDERECO\";\"CODIGO_ACREDITACAO\" ;\"RESUMO_QUALIFICACAO\";\"DETALHE_QUALIFICACAO\";\"DATA_INICIAL\";\"DATA_FINAL\";\"OPERACAO\";\"Versao_layout\";\"Empresa_id\"");

                            foreach (DataRow row in dtQualifCred.Rows)
                            {

                                for (int i = 0; i < dtQualifCred.Columns.Count; i++)
                                {

                                    streamWriter.WriteLine(row[i].ToString());
                                }
                            }
                        }

                    }

                    using (var fileStream = new FileStream(@"C:\ArquivosMobileSaude\gm_ms_qualificacao_" + dataHoraSegundos + ".zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }

                    //Enviar arquivo para o FTP Zipado
                    UploadSFTPBLL Upload = new UploadSFTPBLL();

                    string host = "ms2.mobilesaude.com.br";
                    string username = "funcesp";
                    string password = "funcesp123";
                    string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_qualificacao_" + dataHoraSegundos + ".zip";
                 //   string nomeArquivo = @"D:\Users\F02581\Desktop\gm_ms_qualificacao_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";


                    Upload.SendSFTP(host, username, password, nomeArquivo);

                    //Servidor Windows homologação

                    //string ftpIPServidor = "fcesporah001";
                    //string ftpUsuarioID = "integ";
                    //string ftpSenha = "newtst";
                    //string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_qualificacao_" + dataHoraSegundos + ".zip";

                   //Upload.SendFTP(ftpIPServidor, ftpUsuarioID, ftpSenha, nomeArquivo);
                }

            }



            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message);
            }

            DataTable dtCredenciada = new DataTable();
            dtCredenciada = obj.JOB_SAU_REDE_CREDENCIADA();

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        var demoFile = archive.CreateEntry(@"gm_ms_redecred_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");

                        var entryStream = demoFile.Open();
                        using (var streamWriter = new StreamWriter(entryStream, Encoding.UTF8))
                        {
                            streamWriter.WriteLine("\"codigo_cbo\";\"codigo_plano\";\"classe_prestador\";\"codigo_legado\";\"nome_prestador\";\"sexo\";\"endereco\";\"numero\";\"complemento\";\"bairro\";\"codigo_municipio\";\"codigo_uf\";\"cep\";\"telefone_primario\";\"telefone_secundario\";\"email\";\"site_url\";\"nome_logomarca\";\"cpf_cnpj\";\"prioridade\";\"latitude\";\"longitude\";\"sequencial_endereco\";\"data_bloqueio\";\"motivo_bloqueio\";\"operacao\";\"versao_layout\";\"razao_social\";\"sigla_conselho_regional\";\"uf_conselho_regional\";\"numero_conselho_regional\";\"nome_responsavel_tecnico\";\"facebook\";\"twitter\";\"observacoes\";\"acessibilidade\";\"detalhe_acessibilidade\";\"atend_24_horas\";\"link_agenda_online\";\"secao_resultado\";\"regime_atendimento\";\"cpf_cnpj_subst\";\"codigo_CBO_subst\";\"classe_prestador_subst\";\"sequencial_endereco_subst\";\"empresa_id\";\"reservado1\";\"dt_inicio_atend\"");

                            foreach (DataRow row in dtCredenciada.Rows)
                            {

                                for (int i = 0; i < dtCredenciada.Columns.Count; i++)
                                {

                                    streamWriter.WriteLine(row[i].ToString());
                                }
                            }
                        }
                        
                    }

                    using (var fileStream = new FileStream(@"C:\ArquivosMobileSaude\gm_ms_redecred_" + dataHoraSegundos + ".zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }

                    //Enviar arquivo para o FTP Zipado
                    UploadSFTPBLL Upload = new UploadSFTPBLL();

                    //string host = "ms2.mobilesaude.com.br";
                    //string username = "funcesp";
                    //string password = "funcesp123";
                    //string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_redecred_" + dataHoraSegundos + ".zip";
                    ////string nomeArquivo = @"D:\Users\F02581\Desktop\gm_ms_redecred_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";


                    //Upload.SendSFTP(host, username, password, nomeArquivo);

                    //Servidor Windows homologação

                    string ftpIPServidor = "fcesporah001";
                    string ftpUsuarioID = "integ";
                    string ftpSenha = "newtst";
                    string nomeArquivo = @"C:\ArquivosMobileSaude\gm_ms_redecred_" + dataHoraSegundos + ".zip";

                 //   Upload.SendFTP(ftpIPServidor, ftpUsuarioID, ftpSenha, nomeArquivo);
                }

            }



            catch (Exception ex)
            {

                throw new Exception("Problemas contate o administrador do sistema [ProcPortalDAL]: //n" + ex.Message); ;
            }

            Response.Write("<script LANGUAGE='JavaScript' >alert('Arquivos gerados com sucesso!')</script>");
        }

    }
}