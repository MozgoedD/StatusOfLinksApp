using System;
using System.Collections.Generic;
using System.Text;
using Task1.Services.Abstract;

namespace Task1.ServiceManagers
{
    public class ExceptionNotificationServiceManager : IExceptionNotificationService
    {
        IExceptionNotificationService _exceptionNotificationService;

        public ExceptionNotificationServiceManager(IExceptionNotificationService exceptionNotificationService)
        {
            _exceptionNotificationService = exceptionNotificationService;
        }
        public void ExceptionNotify(string exceptionMessage)
        {
            _exceptionNotificationService.ExceptionNotify(exceptionMessage);
        }
    }
}
