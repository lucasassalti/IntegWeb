using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Renci.SshNet;

namespace IntegWeb.Saude.Aplicacao
{
    public class UploadSFTPBLL
    {

        public string SendSFTP(string host, string username, string password, string arquivo)
        {
            try
            {
                UploadSFTPBLL result = new UploadSFTPBLL();

                using (var sftp = new SftpClient(host, username, password))
                {
                    sftp.Connect();

                    FileInfo _arquivoInfo = new FileInfo(arquivo);
                    string renameArquivo = _arquivoInfo.Name;

                    using (var fileStream = new FileStream(arquivo, FileMode.Open))
                    {
                        sftp.BufferSize = 4 * 1024; // bypass Payload error large files
                        sftp.UploadFile(fileStream, arquivo);
                        sftp.RenameFile(arquivo, renameArquivo);
                    }

                    sftp.Disconnect();
                }
                return result.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao conectar so SFTP: " + ex.Message);
            }



        }

        public string SendFTP(string ftpIPServidor, string ftpUsuarioID, string ftpSenha, string _nomeArquivo)
        {

            UploadSFTPBLL result = new UploadSFTPBLL();

            FileInfo _arquivoInfo = new FileInfo(_nomeArquivo);
            string uri = "ftp://" + ftpIPServidor + "/" + _arquivoInfo.Name;
            FtpWebRequest requisicaoFTP;

            // Cria um objeto FtpWebRequest a partir da Uri fornecida
            requisicaoFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpIPServidor + "/" + _arquivoInfo.Name));

            // Fornece as credenciais de WebPermission
            requisicaoFTP.Credentials = new NetworkCredential(ftpUsuarioID, ftpSenha);

            // Por padrão KeepAlive é true, 
            requisicaoFTP.KeepAlive = false;

            // Especifica o comando a ser executado
            requisicaoFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Especifica o tipo de dados a ser transferido
            requisicaoFTP.UseBinary = true;

            // Notifica o servidor seobre o tamanho do arquivo a enviar
            requisicaoFTP.ContentLength = _arquivoInfo.Length;

            // Define o tamanho do buffer para 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int _tamanhoConteudo;

            // Abre um stream (System.IO.FileStream) para o arquivo a ser enviado
            FileStream fs = _arquivoInfo.OpenRead();

            try
            {
                // Stream  para o qual o arquivo a ser enviado será escrito
                Stream strm = requisicaoFTP.GetRequestStream();

                // Lê a partir do arquivo stream, 2k por vez
                _tamanhoConteudo = fs.Read(buff, 0, buffLength);

                // ate o conteudo do stream terminar
                while (_tamanhoConteudo != 0)
                {
                    // Escreve o conteudo a partir do arquivo para o stream FTP 
                    strm.Write(buff, 0, _tamanhoConteudo);
                    _tamanhoConteudo = fs.Read(buff, 0, buffLength);
                }

                // Fecha o stream a requisição
                strm.Close();
                fs.Close();

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar so SFTP: " + ex.Message);
            }
        }
    }
}
