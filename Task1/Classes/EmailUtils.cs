using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Task1.Classes
{
    static class EmailUtils
    {
        public static void ExceptionSend(Settings settings, string text)
        {
            MailAddress emailFrom = new MailAddress(settings.EmailFrom, "Islamov Azat LinksConsoleApp");
            MailAddress emailTo = new MailAddress(settings.EmailTo);

            MailMessage message = new MailMessage(emailFrom, emailTo);
            message.Subject = $"Expetion Occured";
            message.Body = $"{text}";
            message.IsBodyHtml = false;

            SmtpClient smpt = new SmtpClient(settings.SmptAddress, Int16.Parse(settings.SmptPort));
            smpt.Credentials = new NetworkCredential(settings.EmailFrom, settings.EmailFromPassword);

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
