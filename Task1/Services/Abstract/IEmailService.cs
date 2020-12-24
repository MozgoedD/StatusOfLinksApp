using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Services.Abstract
{
    public interface IEmailService
    {
        void sendEmail(string subject, string messageText, string recipientAddress);
    }
}
