using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Servico_envio_email_aniversarios
{
    class Envio_email_aniversariante
    {
        public static OracleConnection conn ;
        private static string Server_Email = Properties.Settings.Default.Server_Email;
        private static string Server_Email_Login = Properties.Settings.Default.Server_Email_Login;
        private static string Server_Email_Key = Properties.Settings.Default.Server_Email_Key;
        private static string Email_Notificao = Properties.Settings.Default.Email_Notificacao;
        private static string Email_Copia1 = Properties.Settings.Default.Email_Com_Copia1;
        private static string Email_Copia2 = Properties.Settings.Default.Email_Com_Copia2;
        public static List<String[]> birthdays;

        public Envio_email_aniversariante()
        {
            conn = new OracleConnection();
            conn.ConnectionString = Properties.Settings.Default.ConnectionBD;
            conn.Open();
        }

        ~Envio_email_aniversariante()
        {
            conn.Close();
        }

        public void Executar()
        {
            // while (1 == 1)
            try
            {
                gravar_log("Incio da verificação");
                List<String[]> aniversariantes = Lista_Funcionarios(Properties.Settings.Default.Select_Aniversariantes);              
                if (aniversariantes.Count > 0 && enviado_hoje() == false)
                {
                    birthdays = aniversariantes;
                    gravar_log("Aniversariante encontrados - Iniciando envio de e-mails.");
                    //

                    List<String[]> destinatarios = Lista_Funcionarios(Properties.Settings.Default.Select_Destinatarios);
                    for (int i = 0; i < destinatarios.Count; i++)
                    {
                        String texto_notificacao = gerar_texto_notificacao(aniversariantes, -9999, destinatarios[i][1]);
                        try
                        {
                            if (enviado_hoje("Enviando e-mail notificacao para  " + destinatarios[i][2] + " com sucesso ") == false)
                            {
                                gravar_log("Enviando e-mail notificacao para  " + destinatarios[i][2]);
                                if (conn.ConnectionString.ToUpper().IndexOf("NEWDEV") == -1)
                                {
                                    //enviar_email_notificacao(destinatarios[i][2], "Olha quem faz aniversario!", texto_notificacao);
                                    enviar_email_notificacao_aniversario(aniversariantes[i][2], "Olha quem faz aniversario!", texto_notificacao);
                                    gravar_log("Enviando e-mail notificacao para  " + destinatarios[i][2] + " com sucesso ");
                                }
                                else
                                {
                                    gravar_log("** AMBIENTE DESENVOLVIMENTO ** Envio e-mail notificação ignorado:  " + destinatarios[i][2]);
                                }
                            }
                        }
                        catch (Exception ee)
                        {
                            gravar_log("Erro: " + ee.Message.ToString());
                        }
                    }


                    for (int i = 0; i < aniversariantes.Count; i++)
                    {
                        String texto_notificacao_personalizado = gerar_texto_notificacao(aniversariantes, i, aniversariantes[i][1]);
                        if (texto_notificacao_personalizado != null)
                        {
                            try
                            {
                                if (enviado_hoje("Enviando e-mail notificacao2 para  " + destinatarios[i][2] + " com sucesso ") == false)
                                {
                                    gravar_log("Enviando e-mail notificacao2 para  " + destinatarios[i][2]);
                                    if (conn.ConnectionString.ToUpper().IndexOf("NEWDEV") == -1)
                                    {
                                        //enviar_email_notificacao(aniversariantes[i][2], "Olha quem faz aniversario!", texto_notificacao_personalizado);
                                        enviar_email_notificacao_aniversario(aniversariantes[i][2], "Olha quem faz aniversario!", texto_notificacao_personalizado);
                                        gravar_log("Enviando e-mail notificacao2 para  " + destinatarios[i][2] + " com sucesso ");
                                    }
                                    else
                                    {
                                        gravar_log("** AMBIENTE DESENVOLVIMENTO ** Envio e-mail notificação2 ignorado:  " + destinatarios[i][2]);
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                gravar_log("Erro: " + ee.Message.ToString());
                            }
                        }
                    }


                    for (int i = 0; i < aniversariantes.Count; i++)
                    {
                        String texto_parabenizacao = gerar_texto_parabenizacao(aniversariantes, i);
                        try
                        {
                            if (enviado_hoje("Enviando e-mail parabenizacao para  " + aniversariantes[i][2] + " com sucesso ") == false)
                            {
                                gravar_log("Enviando e-mail parabenizacao para  " + aniversariantes[i][2]);
                                if (conn.ConnectionString.ToUpper().IndexOf("NEWDEV") == -1)
                                {
                                    enviar_email_aniversariante(aniversariantes[i][2], "Feliz aniversário!", texto_parabenizacao);
                                    gravar_log("Enviando e-mail parabenizacao para  " + aniversariantes[i][2] + " com sucesso ");
                                }
                                else
                                {
                                    gravar_log("** AMBIENTE DESENVOLVIMENTO ** Envio e-mail parabenização ignorado:  " + destinatarios[i][2]);
                                }
                            }
                        }
                        catch (Exception ee)
                        {
                            gravar_log("Erro: " + ee.Message.ToString());
                        }
                    }
                    gravar_log("Processo realizado com sucesso.");

                }
                else
                {
                    if (aniversariantes.Count == 0)
                    {
                        gravar_log("Nenhum aniversáriante encontrado.");
                    }
                    if (enviado_hoje() == true)
                    {
                        gravar_log("E-mail já enviados hoje.");
                    }
                }

                // System.Threading.Thread.Sleep(Tempo_de_espera);
            }
            catch (Exception ee)
            {
                gravar_log("Erro: " + ee.Message.ToString());
            }

        }

        static OracleDataReader Select(String SQL)
        {                         
            OracleCommand command = conn.CreateCommand();            
            command.CommandText = SQL;
            OracleDataReader reader = command.ExecuteReader();
            return reader;
        }

        static List<String[]> Lista_Funcionarios(String pSQL)
        {
            List<String[]> lista_aniversariante = new List<String[]>();
            int contador = -1;
            OracleDataReader reader = Select(pSQL);
            while (reader.Read())
            {
                contador++;
                String[] aniversariante = new String[4];
                aniversariante[0] = Convert.ToString(reader["NUM_RGTRO_EMPRG"]);
                aniversariante[1] = (String)reader["nom_emprg"];
                aniversariante[2] = (String)reader["cod_email_emprg"];
                aniversariante[3] = (String)reader["area"];
                lista_aniversariante.Add(aniversariante);
            }
            return lista_aniversariante;
        }

        public static String gerar_texto_notificacao(List<String[]> aniversariantes, int ignorar, String nome_destinatario)
        {
            String texto_individual = null;
            String texto_geral = null;
            for (int i = 0; i < aniversariantes.Count; i++)
            {
                if (i != ignorar)
                {
                    texto_individual = Properties.Settings.Default.Notificacao_Corpo.Replace("X_ANIVERSARIANTE_X", aniversariantes[i][1]).Replace("X_AREA_X", aniversariantes[i][3]);
                    texto_individual = texto_individual.Replace("X_FOTO_X", aniversariantes[i][0]);
                    texto_individual = texto_individual.Replace("X_MATRICULA_X", aniversariantes[i][0]);
                    texto_geral = texto_geral + texto_individual;
                }
            }
            if (texto_geral != null)
            {
                texto_geral = Properties.Settings.Default.Notificacao_Cabecalho.Replace("X_DESTINATARIO_X", nome_destinatario) +
                              texto_geral +
                              Properties.Settings.Default.Notificacao_Rodape;

            }
            return texto_geral;
        }

        static String gerar_texto_parabenizacao(List<String[]> aniversariantes, int id_aniversariante)
        {
            String texto_individual = null;
            String texto_geral = null;
            texto_individual = Properties.Settings.Default.Parabenizacao_Corpo.Replace("X_ANIVERSARIANTE_X", aniversariantes[id_aniversariante][1]).Replace("X_AREA_X", aniversariantes[id_aniversariante][3]);
            texto_individual = texto_individual.Replace("X_FOTO_X", aniversariantes[id_aniversariante][0]);
            texto_individual = texto_individual.Replace("X_MATRICULA_X", aniversariantes[id_aniversariante][0]);
            
            texto_geral = Properties.Settings.Default.Parabenizacao_Cabecalho +
                           texto_individual +
                          Properties.Settings.Default.Parabenizacao_Rodape;

            //texto_geral = texto_individual;

            return texto_geral;
        }

        public bool enviado_hoje()
        {
            bool resultado = false;
            string[] lines = File.ReadAllLines(Properties.Settings.Default.Log_Path + "/Servico_Envio_Email_Aniversariante.log");
            Match match;
            foreach (string line in lines)
            {
                match = Regex.Match(line, DateTime.Now.ToString().Substring(0, 10) + "(.*?)Processo realizado com sucesso.(.*?)", RegexOptions.None);
                if (match.Success)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public bool enviado_hoje(String texto)
        {
            bool resultado = false;
            string[] lines = File.ReadAllLines(Properties.Settings.Default.Log_Path + "/Servico_Envio_Email_Aniversariante.log");
            Match match;
            foreach (string line in lines)
            {
                match = Regex.Match(line, DateTime.Now.ToString().Substring(0, 10) + "(.*?)" + texto + "(.*?)", RegexOptions.None);
                if (match.Success)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public static void gravar_log(String texto)
        {            
            TextWriter tw = new StreamWriter(Properties.Settings.Default.Log_Path + "\\Servico_Envio_Email_Aniversariante.log", true);
            tw.WriteLine(DateTime.Now.ToString() + " " + texto);
            tw.Close();
        }

        public static void enviar_email_notificacao(String Empregado_Email, String Titulo, String corpo_email)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(Email_Notificao);
            mailMessage.To.Add(Empregado_Email);
            mailMessage.Subject = Titulo ;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = corpo_email;

            send_mail(mailMessage);
        }
        
        public static void enviar_email_aniversariante(String Empregado_Email, String Titulo, String corpo_email)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(Email_Notificao);
            mailMessage.To.Add(Empregado_Email);
            if (!String.IsNullOrEmpty(Email_Copia1)) { mailMessage.Bcc.Add(new MailAddress(Email_Copia1)); }
            if (!String.IsNullOrEmpty(Email_Copia2)) { mailMessage.Bcc.Add(new MailAddress(Email_Copia2)); }
            mailMessage.Subject = Titulo;
            mailMessage.Body = corpo_email;
            mailMessage.IsBodyHtml = true;

            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_01.jpg", "X_SERVERIMG00_X", " width='800' height='156' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_02.jpg", "X_SERVERIMG01_X", " width='128' height='202' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_04.jpg", "X_SERVERIMG02_X", " width='84' height='202' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_05.jpg", "X_SERVERIMG03_X", " width='127' height='214' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_07.jpg", "X_SERVERIMG04_X", " width='91' height='214' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_08.jpg", "X_SERVERIMG05_X", " width='800' height='248' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_09.jpg", "X_SERVERIMG06_X", " width='800' height='278' ");

            #region oldway
            /*//MAIL_IMGS - HEADER
            Attachment attHeader = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_01.jpg", MediaTypeNames.Image.Jpeg);
            attHeader.ContentDisposition.Inline = true;
            attHeader.ContentId = Guid.NewGuid().ToString();

            //MAIL_IMGS - BODY
            Attachment att = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_02.jpg", MediaTypeNames.Image.Jpeg);
            att.ContentId = Guid.NewGuid().ToString();
            att.ContentDisposition.Inline = true;
            Attachment att2 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_04.jpg", MediaTypeNames.Image.Jpeg);
            att2.ContentDisposition.Inline = true;
            att2.ContentId = Guid.NewGuid().ToString();
            Attachment att3 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_05.jpg", MediaTypeNames.Image.Jpeg);
            att3.ContentDisposition.Inline = true;
            att3.ContentId = Guid.NewGuid().ToString();
            Attachment att4 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_07.jpg", MediaTypeNames.Image.Jpeg);
            att4.ContentDisposition.Inline = true;
            att4.ContentId = Guid.NewGuid().ToString();

            //MAIL_IMGS - FOOTER
            Attachment attFooter = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_08.jpg", MediaTypeNames.Image.Jpeg);
            attFooter.ContentDisposition.Inline = true;
            attFooter.ContentId = Guid.NewGuid().ToString();
            Attachment attFooter2 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/aniversario_funcesp_09.jpg", MediaTypeNames.Image.Jpeg);
            attFooter2.ContentDisposition.Inline = true;
            attFooter2.ContentId = Guid.NewGuid().ToString();

            List<String[]> aniversariantes = Lista_Funcionarios(Properties.Settings.Default.Select_Aniversariantes.ToString());
            mailMessage.Body = gerar_texto_parabenizacao(aniversariantes, 0);
            
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG00_X", @"<img src='cid:" + attHeader.ContentId + @"'  width='800' height='156'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG01_X", @"<img src='cid:" + att.ContentId + @"' width='128' height='202' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG02_X", @"<img src='cid:" + att2.ContentId + @"' width='84' height='202' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG03_X", @"<img src='cid:" + att3.ContentId + @"' width='127' height='214' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG04_X", @"<img src='cid:" + att4.ContentId + @"' width='91' height='214' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG05_X", @"<img src='cid:" + attFooter.ContentId + @"'  width='800' height='248'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG06_X", @"<img src='cid:" + attFooter2.ContentId + @"' width='800' height='278' />");

            mailMessage.Attachments.Add(attHeader);
            mailMessage.Attachments.Add(att);
            mailMessage.Attachments.Add(att2);
            mailMessage.Attachments.Add(att3);
            mailMessage.Attachments.Add(att4);
            mailMessage.Attachments.Add(attFooter);
            mailMessage.Attachments.Add(attFooter2);*/
            #endregion

            send_mail(mailMessage);
        }

        public static void enviar_email_notificacao_aniversario(String Empregado_Email, String Titulo, String corpo_email)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(Email_Notificao);
            mailMessage.To.Add(Empregado_Email);
            if (!String.IsNullOrEmpty(Email_Copia1)) { mailMessage.Bcc.Add(new MailAddress(Email_Copia1)); }
            if (!String.IsNullOrEmpty(Email_Copia2)) { mailMessage.Bcc.Add(new MailAddress(Email_Copia2)); }
            mailMessage.Subject = Titulo;
            mailMessage.Body = corpo_email;
            mailMessage.IsBodyHtml = true;

            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_02.jpg", "X_SERVERIMG00_X", "  width='104' height='178'  ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_03.jpg", "X_SERVERIMG01_X", " width='434' height='178' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_04.jpg", "X_SERVERIMG02_X", " width='262' height='178' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_05.jpg", "X_SERVERIMG03_X", " width='104' height='54' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_07.jpg", "X_SERVERIMG04_X", " width='262' height='54' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_08.jpg", "X_SERVERIMG05_X", " width='104' height='93' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_10.jpg", "X_SERVERIMG06_X", " width='84' height='93' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_11.jpg", "X_SERVERIMG07_X", " width='104' height='161' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/" + birthdays[0][0] + ".jpg", "X_SERVERIMG08_X", " height='130' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/index_16.jpg", "X_SERVERIMG09_X", " width='84' height='161' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/spacer.jpg", "X_SERVERIMG10_X", " width='104' height='1' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/spacer2.jpg", "X_SERVERIMG11_X", " width='191' height='1' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/spacer3.jpg", "X_SERVERIMG12_X", " width='243' height='1' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/spacer4.jpg", "X_SERVERIMG13_X", " width='178' height='1' ");
            build_attachment(ref mailMessage, @"http://fcespwebp001/Fotos_Aniversariantes/spacer5.jpg", "X_SERVERIMG14_X", " width='84' height='1' ");
            
            #region old_way
            /*
            //MAIL_IMGS - HEADER
            Attachment attHeader = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_02.jpg", MediaTypeNames.Image.Jpeg);
            attHeader.ContentDisposition.Inline = true;
            attHeader.ContentId = Guid.NewGuid().ToString();
            Attachment attHeader2 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_03.jpg", MediaTypeNames.Image.Jpeg);
            attHeader2.ContentDisposition.Inline = true;
            attHeader2.ContentId = Guid.NewGuid().ToString();
            Attachment attHeader3 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_04.jpg", MediaTypeNames.Image.Jpeg);
            attHeader3.ContentDisposition.Inline = true;
            attHeader3.ContentId = Guid.NewGuid().ToString();
            Attachment attHeader4 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_05.jpg", MediaTypeNames.Image.Jpeg);
            attHeader4.ContentDisposition.Inline = true;
            attHeader4.ContentId = Guid.NewGuid().ToString();
            Attachment attHeader5 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_07.jpg", MediaTypeNames.Image.Jpeg);
            attHeader5.ContentDisposition.Inline = true;
            attHeader5.ContentId = Guid.NewGuid().ToString();
            Attachment attHeader6 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_08.jpg", MediaTypeNames.Image.Jpeg);
            attHeader6.ContentDisposition.Inline = true;
            attHeader6.ContentId = Guid.NewGuid().ToString();
            Attachment attHeader7 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_10.jpg", MediaTypeNames.Image.Jpeg);
            attHeader7.ContentDisposition.Inline = true;
            attHeader7.ContentId = Guid.NewGuid().ToString();

            //MAIL_IMGS - BODY
            Attachment att = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_11.jpg", MediaTypeNames.Image.Jpeg);
            att.ContentId = Guid.NewGuid().ToString();
            att.ContentDisposition.Inline = true;
            Attachment att2 = new Attachment(@"D:\\2563.jpg", MediaTypeNames.Image.Jpeg);
            att2.ContentDisposition.Inline = true;
            att2.ContentId = Guid.NewGuid().ToString();
            Attachment att3 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_14.jpg", MediaTypeNames.Image.Jpeg);
            att3.ContentDisposition.Inline = true;
            att3.ContentId = Guid.NewGuid().ToString();

            //MAIL_IMGS - FOOTER
            Attachment attFooter = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/index_16.jpg", MediaTypeNames.Image.Jpeg);
            attFooter.ContentDisposition.Inline = true;
            attFooter.ContentId = Guid.NewGuid().ToString();
            Attachment attFooter2 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/spacer.jpg", MediaTypeNames.Image.Jpeg);
            attFooter2.ContentDisposition.Inline = true;
            attFooter2.ContentId = Guid.NewGuid().ToString();
            Attachment attFooter3 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/spacer2.jpg", MediaTypeNames.Image.Jpeg);
            attFooter3.ContentDisposition.Inline = true;
            attFooter3.ContentId = Guid.NewGuid().ToString();
            Attachment attFooter4 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/spacer3.jpg", MediaTypeNames.Image.Jpeg);
            attFooter4.ContentDisposition.Inline = true;
            attFooter4.ContentId = Guid.NewGuid().ToString();
            Attachment attFooter5 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/spacer4.jpg", MediaTypeNames.Image.Jpeg);
            attFooter5.ContentDisposition.Inline = true;
            attFooter5.ContentId = Guid.NewGuid().ToString();
            Attachment attFooter6 = new Attachment(@"http://fcespwebp001/Fotos_Aniversariantes/imagens/spacer5.jpg", MediaTypeNames.Image.Jpeg);
            attFooter6.ContentDisposition.Inline = true;
            attFooter6.ContentId = Guid.NewGuid().ToString();

            List<String[]> aniversariantes = Lista_Funcionarios(Properties.Settings.Default.Select_Destinatarios.ToString());
            mailMessage.Body = gerar_texto_notificacao(aniversariantes, -9999, destinatarios[i][1]);

            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG00_X", @"<img src='cid:" + attHeader.ContentId + @"'  width='104' height='178'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG01_X", @"<img src='cid:" + attHeader2.ContentId + @"'  width='434' height='178'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG02_X", @"<img src='cid:" + attHeader3.ContentId + @"'  width='262' height='178'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG03_X", @"<img src='cid:" + attHeader4.ContentId + @"'  width='104' height='54'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG04_X", @"<img src='cid:" + attHeader5.ContentId + @"'  width='262' height='54'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG05_X", @"<img src='cid:" + attHeader6.ContentId + @"'  width='104' height='93'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG06_X", @"<img src='cid:" + attHeader7.ContentId + @"'  width='84' height='93'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG07_X", @"<img src='cid:" + att.ContentId + @"' width='104' height='161' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG08_X", @"<img src='cid:" + att2.ContentId + @"' height='130' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG09_X", @"<img src='cid:" + att3.ContentId + @"' width='84' height='161' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG10_X", @"<img src='cid:" + attFooter.ContentId + @"'  width='800' height='232'  />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG11_X", @"<img src='cid:" + attFooter2.ContentId + @"' width='104' height='1' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG12_X", @"<img src='cid:" + attFooter3.ContentId + @"' width='191' height='1' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG13_X", @"<img src='cid:" + attFooter4.ContentId + @"' width='243' height='1' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG14_X", @"<img src='cid:" + attFooter5.ContentId + @"' width='178' height='1' />");
            mailMessage.Body = mailMessage.Body.Replace("X_SERVERIMG15_X", @"<img src='cid:" + attFooter6.ContentId + @"' width='84' height='1' />");

            mailMessage.Attachments.Add(attHeader);
            mailMessage.Attachments.Add(attHeader2);
            mailMessage.Attachments.Add(attHeader3);
            mailMessage.Attachments.Add(attHeader4);
            mailMessage.Attachments.Add(attHeader5);
            mailMessage.Attachments.Add(attHeader6);
            mailMessage.Attachments.Add(attHeader7);
            mailMessage.Attachments.Add(att);
            mailMessage.Attachments.Add(att2);
            mailMessage.Attachments.Add(att3);
            mailMessage.Attachments.Add(attFooter);
            mailMessage.Attachments.Add(attFooter2);
            mailMessage.Attachments.Add(attFooter3);
            mailMessage.Attachments.Add(attFooter4);
            mailMessage.Attachments.Add(attFooter5);
            mailMessage.Attachments.Add(attFooter6);*/
            #endregion

            send_mail(mailMessage);
        }

        static void send_mail(MailMessage p_mail)
        {
           

            SmtpClient smtpClient = new SmtpClient(Server_Email, 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(Server_Email_Login, Server_Email_Key);

            smtpClient.Send(p_mail);
        }

        static void build_attachment(ref MailMessage p_mail, string p_imgpath, string p_imgtag, string p_imgstyle = "")
        {
            Attachment att = new Attachment(p_imgpath, MediaTypeNames.Image.Jpeg);
            att.ContentDisposition.Inline = true;
            att.ContentId = Guid.NewGuid().ToString();

            p_mail.Body = p_mail.Body.Replace(p_imgtag, @"<img src='cid:" + att.ContentId + @"'  " + p_imgstyle + "  />");
            p_mail.Attachments.Add(att);
        }



    }

    
}
