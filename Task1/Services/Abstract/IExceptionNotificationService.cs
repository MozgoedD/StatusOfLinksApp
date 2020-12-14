using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Services.Abstract
{
    public interface IExceptionNotificationService
    {
        void ExceptionNotify(string exceptionMessage);
    }
}
