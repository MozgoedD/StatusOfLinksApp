using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Task1.Services.Abstract;

namespace Task1.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private string emailName;
        private string emailPassword;
        private string smptAddress;
        private int smptPort;
        public EmailService(string emailName, string emailPassword,
            string smptAddress, int smptPort)
        {
            this.emailName = emailName;
            this.emailPassword = emailPassword;
            this.smptAddress = smptAddress;
            this.smptPort = smptPort;
        }

        public void sendEmail(string subject, string messageText, string recipientAddress)
        {
            using (var smpt = new SmtpClient(smptAddress, smptPort))
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(emailName, "Console App Mail");
                message.Bcc.Add(new MailAddress(recipientAddress));
                message.Subject = subject;
                message.Body = messageText;
                message.IsBodyHtml = true;

                smpt.Credentials = new NetworkCredential(emailName, emailPassword);
                smpt.EnableSsl = true;
                try
                {
                    smpt.Send(message);
                    Console.WriteLine($"Exception message was sent:\n{message.Body} was sent");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception message was not sent:\n{message.Body}  \n\nEmail Exception: {e.Message}");
                }
            }
        }
    }
}
