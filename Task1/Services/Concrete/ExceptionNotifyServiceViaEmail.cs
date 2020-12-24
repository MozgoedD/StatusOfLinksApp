using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Task1.Services.Abstract;

namespace Task1.Services.Concrete
{
    public class ExceptionNotifyServiceViaEmail : IExceptionNotificationService
    {
        private IEmailService _emailService;
        private string recipientAddress;

        public ExceptionNotifyServiceViaEmail(IEmailService emailService, string recipientAddress)
        {
            _emailService = emailService;
            this.recipientAddress = recipientAddress;
        }

        public void ExceptionNotify(string exceptionMessage)
        {
            _emailService.sendEmail("Exception Occured", exceptionMessage, recipientAddress);
        }
    }
}
