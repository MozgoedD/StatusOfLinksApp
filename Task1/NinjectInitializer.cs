using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Task1.Models;
using Task1.Services.Abstract;

namespace Task1
{
    public class NinjectInitializer
    {
        public void InitNinject(string configPath, out Settings userSettings, 
            out IExceptionNotificationService exceptionNotificationServiceManager, out IParserService parserServiceManager,
            out IReportService reportServiceManager, out IWebSiteStatusInspector webSiteStatusInspectorManager)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var userSettingsBuilderManager = kernel.Get<IUserSettignsBuilder>(new ConstructorArgument("configPath", configPath));
            userSettings = userSettingsBuilderManager.GetUserSettings();

            var emailServiceManager = kernel.Get<IEmailService>(
                new ConstructorArgument("emailName", userSettings.EmailFrom),
                new ConstructorArgument("emailPassword", userSettings.EmailFromPassword),
                new ConstructorArgument("smptAddress", userSettings.SmptAddress),
                new ConstructorArgument("smptPort", userSettings.SmptPort));

            exceptionNotificationServiceManager = kernel.Get<IExceptionNotificationService>(
                new ConstructorArgument("emailService", emailServiceManager),
                new ConstructorArgument("recipientAddress", userSettings.EmailTo));

            parserServiceManager = kernel.Get<IParserService>(new ConstructorArgument(
                "exceptionNotificationServiceManager", exceptionNotificationServiceManager));

            reportServiceManager = kernel.Get<IReportService>(
                new ConstructorArgument("fileName", userSettings.FileName),
                new ConstructorArgument("filePath", userSettings.FilePath));

            webSiteStatusInspectorManager = kernel.Get<IWebSiteStatusInspector>(
                new ConstructorArgument("exceptionNotificationServiceManager", exceptionNotificationServiceManager));
        }
    }
}
