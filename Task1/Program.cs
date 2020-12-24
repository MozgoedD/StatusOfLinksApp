using System;
using Task1.Models;
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

            var ninjectInitializer = new NinjectInitializer();

            ninjectInitializer.InitNinject(configPath,
                out Settings userSettings,
                out IExceptionNotificationService exceptionNotificationServiceManager,
                out IParserService parserServiceManager,
                out IReportService reportServiceManager,
                out IWebSiteStatusInspector webSiteStatusInspectorManager);

            var mainLink = new WebSiteModel(Uri, userSettings.Nesting);
            mainLink.StatusCode = webSiteStatusInspectorManager.CheckWebsiteStatus(mainLink.URI);

            if (mainLink.StatusCode.ToString().StartsWith("2") || mainLink.StatusCode.ToString().StartsWith("3"))
            {
                Console.WriteLine($"Program In Progress...");
                var eachLinkProcedure = new MainProcedure(parserServiceManager,
                    webSiteStatusInspectorManager, reportServiceManager);
                try
                {
                    eachLinkProcedure.StartMainProcedure(mainLink);
                }
                catch (Exception e)
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
