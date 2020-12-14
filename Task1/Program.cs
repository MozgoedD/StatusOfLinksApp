using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Task1.Models;
using Task1.Classes;
using Task1.Services.Concrete;
using Task1.ServiceManagers;
using Task1.Services.Abstract;

namespace Task1
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please enter a link to the config file and a valid url to the website");
                return 0;
            }
            var configPath = args[0];
            var Uri = args[1];

            var userSettingsBuilder = new UserSettingsFromJsonBuilder(configPath);
            var userSettingsBuilderManager = new UserSettingsBuilderManager(userSettingsBuilder);
            var userSettings = userSettingsBuilderManager.GetUserSettings();

            var parserService = new ParserService();
            var parserServiceManager = new ParserServiceManager(parserService);

            var reportService = new CsvReportWriter(userSettings.FileName, userSettings.FilePath);
            var reportServiceManager = new ReportServiceManager(reportService);

            var webSiteStatusInspector = new WebSiteStatusInspector();
            var webSiteStatusInspectorManager = new WebSiteStatusInspectorManager(webSiteStatusInspector);

            var exceptionNotificationService = new ExceptionEmailNotifyService(userSettings.EmailFrom, userSettings.EmailTo,
                userSettings.EmailFromPassword, userSettings.SmptAddress, userSettings.SmptPort);
            var exceptionNotificationServiceManager = new ExceptionNotificationServiceManager(exceptionNotificationService);

            var mainLink = new WebSiteModel(Uri, userSettings.Nesting);
            mainLink.StatusCode = webSiteStatusInspectorManager.CheckWebsiteStatus(mainLink.URI);

            Console.WriteLine(userSettings.FilePath + userSettings.FileName);

            if (mainLink.StatusCode.ToString().StartsWith("2") || mainLink.StatusCode.ToString().StartsWith("3"))
            {
                Console.WriteLine($"Program In Progress...");
                var eachLinkProcedure = new EachLinkProcedure(parserServiceManager,
                    webSiteStatusInspectorManager, reportServiceManager);
                try
                {
                    eachLinkProcedure.StartMainProcedure(mainLink);
                }
                catch(Exception e)
                {
                    exceptionNotificationServiceManager.ExceptionNotify(e.ToString());
                }
            }
            else
            {
                Console.WriteLine($"Main link is not avialable");
                return 0;
            }
            Console.WriteLine("DONE");
            return 1;
        }
    }
}
