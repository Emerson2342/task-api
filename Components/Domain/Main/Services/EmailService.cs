using System.Net.Mail;
using System.Net;

namespace TaskList.Components.Domain.Main.Services
{
    public class EmailService
    {
        public EmailService()
        {
            
        }
        public bool Send(
           string toName,
           string toEmail,
           string subject,
           string body,
           string fromName = "Nova Web",
           string fromEmail = "emersonr@novaweb.mobi"
           )
        {
            SmtpClient smtpClient = new(Configuration.Email.SmtpServer, Configuration.Email.Port);

            smtpClient.Credentials = new NetworkCredential(Configuration.Email.UserName, Configuration.Email.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            var mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail, toName));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception($"Erro ao enviar email. {ex.Message}");
            }

        }
    }
}
