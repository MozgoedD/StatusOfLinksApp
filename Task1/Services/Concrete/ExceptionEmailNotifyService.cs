using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Task1.Services.Abstract;

namespace Task1.Services.Concrete
{
    public class ExceptionEmailNotifyService : IExceptionNotificationService
    {
        string EmailFrom;
        string EmailTo;
        string EmailPassword;
        string SmptAddress;
        int SmptPort;

        public ExceptionEmailNotifyService(string EmailFrom, string EmailTO,
            string EmailPassword, string SmptAddress, int SmptPort)
        {
            this.EmailFrom = EmailFrom;
            this.EmailTo = EmailTO;
            this.EmailPassword = EmailPassword;
            this.SmptAddress = SmptAddress;
            this.SmptPort = SmptPort;
        }

        public void ExceptionNotify(string exceptionMessage)
        {
            MailAddress emailFrom = new MailAddress(EmailFrom, "Console App Mail");
            MailAddress emailTo = new MailAddress(EmailTo);

            MailMessage message = new MailMessage(emailFrom, emailTo);
            message.Subject = $"Expetion Occured";
            message.Body = $"{exceptionMessage}";
            message.IsBodyHtml = false;

            SmtpClient smpt = new SmtpClient(SmptAddress, SmptPort);
            smpt.Credentials = new NetworkCredential(EmailFrom, EmailPassword);

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
