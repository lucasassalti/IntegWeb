using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using IntegWeb.Entidades;

namespace IntegWeb.Framework
{
    public class Email
    {
        public Resultado EnviaEmail(string emailDestinatario, string emailRemetente, string emailAssunto, string emailCorpo, string emailNomeAnexo, Stream StreamAssinatura = null, bool assinar = false)
        {
            Resultado res = new Resultado();
            using (var message = new MailMessage(emailRemetente, Util.PreparaEmail(emailDestinatario), emailAssunto, emailCorpo))
            {
                try
                {
                    if (assinar && (StreamAssinatura != null))
                    {
                        string contentID = "assinatura_funcesp";
                        //Attachment inlineLogo = new Attachment(System.Web.HttpContext.Current.Server.MapPath("img/assinatura_email.jpg"));
                        ContentType ct = new ContentType(MediaTypeNames.Image.Jpeg);
                        Attachment inlineLogo = new Attachment(StreamAssinatura, ct);
                        inlineLogo.ContentId = contentID;
                        inlineLogo.ContentDisposition.Inline = true;
                        inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                        message.Attachments.Add(inlineLogo);
                        //emailCorpo = emailCorpo + "<img src=\"cid:" + contentID + "\">";
                        emailCorpo = emailCorpo.Replace("{ASSINATURA_FUNCESP}", "<img src=\"cid:" + contentID + "\">");
                    }

                    emailCorpo = emailCorpo.Replace("{ASSINATURA_FUNCESP}", "");

                    message.IsBodyHtml = true;
                    message.Body = emailCorpo;
                    
                    EnviaEmailMensagem(message);
                    res.Sucesso("E-Mail enviado com sucesso!");
                }
                catch (Exception ex)
                {
                    res.Erro("Erro ao enviar o e-mail: " + ex.Message);
                }
            }
            return res;
        }

        public SmtpClient EnviaEmailMensagem(MailMessage message)
        {
            // SMTP INTERNO:
            //var mailClient = new SmtpClient("smtp.funcesp.com.br");
            //mailClient.Credentials = new System.Net.NetworkCredential("atendimento@funcesp.com.br", "WebCrm#14");
            //mailClient.Send(message);

            //Novo SMTP Office 365:
            SmtpClient mailClient = new SmtpClient("smtp.office365.com", 587);
            mailClient.EnableSsl = true;
            mailClient.TargetName = "STARTTLS/smtp.office365.com";
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //mailClient.Credentials = new System.Net.NetworkCredential("aniversariantes@funcesp.com.br", "Pr#jet@Ani");

            // Produção
            mailClient.Credentials = new System.Net.NetworkCredential("atendimento@funcesp.com.br", "WebCrm#14");

            // Homologação
            //mailClient.Credentials = new System.Net.NetworkCredential("homol_atendimento@funcesp.com.br", "hoatend2014");

            //mailClient.Credentials = new System.Net.NetworkCredential("integweb@funcesp.com.br", "Integ@Web#06");
            mailClient.Send(message);

            return mailClient;
        }

        public string Bom_Dia_Tarde_Noite()
        {
            string ret = "";
            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);
            TimeSpan periodo_tarde = new TimeSpan(18, 0, 0);
            if (horarioAtual < periodo_dia)
            {
                ret = "Bom Dia";
            }
            else if (horarioAtual < periodo_tarde)
            {
                ret = "Boa Tarde";
            }
            else
            {
                ret = "Boa Noite";
            }
            return ret;
        }
    }
}
