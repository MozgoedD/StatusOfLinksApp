using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Task1.Services.Abstract;
using Task1.Services.Concrete;

namespace Task1
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IExceptionNotificationService>().To<ExceptionNotifyServiceViaEmail>();
            Bind<IParserService>().To<ParserService>();
            Bind<IReportService>().To<ReportServiceCsvWriter>();
            Bind<IUserSettignsBuilder>().To<UserSettingsBuilderFromJson>();
            Bind<IWebSiteStatusInspector>().To<WebSiteStatusInspector>();
            Bind<IEmailService>().To<EmailService>();
        }
    }
}
