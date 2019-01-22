using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Renci.SshNet;

namespace IntegWeb.Intranet.Aplicacao
{
    public class EMailMarketingBLL
    {
        //public string SendFTP(string host, string username, string password, List<string> lista_imagens, string nmCampanha)
        public string SendFTP(string host, string username, string password, string caminho_imagem, string nmCampanha)
        {
            EMailMarketingBLL result = new EMailMarketingBLL();
            
            using (var sftp = new SftpClient(host, username, password))
            {

                sftp.Connect();

                sftp.ChangeDirectory(nmCampanha);

                //int cont_pastaI = 0;
                //while (cont_pastaI < lista_imagens.Count)
                //{
                //    foreach (string i in lista_imagens)
                //    {

                using (var fileStream = new FileStream(caminho_imagem, FileMode.Open))
                            {
                                sftp.BufferSize = 4 * 1024; // bypass Payload error large files
                                sftp.UploadFile(fileStream, Path.GetFileName(caminho_imagem));
                            }
                        
                //    }

                //    cont_pastaI++;
                //}
                                
                sftp.Disconnect();
            }

            return result.ToString();
        }

        //public string SendFTPImagem(string host, string username, string password, string lista_imagens, string nmCampanha)
        //{
        //    EMailMarketingBLL result = new EMailMarketingBLL();

        //    using (var sftp = new SftpClient(host, username, password))
        //    {
        //        sftp.Connect();
        //        string nmCampanha_aux = nmCampanha;
        //        int cont = 1;

        //        while (sftp.Exists(nmCampanha_aux))
        //        {
        //            nmCampanha_aux = nmCampanha + "_" + cont;
        //            cont++;
        //        }

        //        nmCampanha = nmCampanha_aux;

        //        if (!sftp.Exists(nmCampanha))
        //        {
        //            sftp.CreateDirectory(nmCampanha);
        //        }

        //        sftp.ChangeDirectory(nmCampanha);

        //        using (var fileStream = new FileStream(lista_imagens, FileMode.Open))
        //        {
        //            sftp.BufferSize = 4 * 1024; // bypass Payload error large files
        //            sftp.UploadFile(fileStream, Path.GetFileName(lista_imagens));
        //        }

        //        sftp.Disconnect();
        //    }

        //    return result.ToString();
        //}

        public string ContCamp(string host, string username, string password, string nmCampanha)
        {

            using (var sftp = new SftpClient(host, username, password))
            {
                sftp.Connect();
                string nmCampanha_aux = nmCampanha;
                int cont = 1;

                while (sftp.Exists(nmCampanha_aux))
                {
                    nmCampanha_aux = nmCampanha + "_" + cont;
                    cont++;
                }

                nmCampanha = nmCampanha_aux;

                if (!sftp.Exists(nmCampanha))
                {
                    sftp.CreateDirectory(nmCampanha);
                }
                sftp.Disconnect();

            }

            return nmCampanha;
        }

    }
}
