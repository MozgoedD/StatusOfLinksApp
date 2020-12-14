using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models;
using Task1.Services.Abstract;

namespace Task1
{
    public class MainProcedure
    {
        IParserService parserServiceManager;
        IWebSiteStatusInspector webSiteStatusInspectorManager;
        IReportService reportServiceManager;

        public MainProcedure(IParserService parserServiceManager,
            IWebSiteStatusInspector webSiteStatusInspectorManager, IReportService reportServiceManager)
        {
            this.parserServiceManager = parserServiceManager;
            this.webSiteStatusInspectorManager = webSiteStatusInspectorManager;
            this.reportServiceManager = reportServiceManager;
        }

        public void StartMainProcedure(WebSiteModel baseWebSite)
        {
            parserServiceManager.ParseAllLinksFromWebSite(baseWebSite);
            Console.WriteLine($"Working with {baseWebSite.URI}");
            foreach (var containedLink in baseWebSite.ContaiedLinks)
            {
                containedLink.StatusCode = webSiteStatusInspectorManager.CheckWebsiteStatus(containedLink.URI);
            }
            reportServiceManager.WriteReport(baseWebSite);
            if (baseWebSite.Nesting >= 2)
            {
                foreach (var containedLink in baseWebSite.ContaiedLinks)
                {
                    if (containedLink.StatusCode.ToString().StartsWith("2") || containedLink.StatusCode.ToString().StartsWith("3"))
                    {
                        StartMainProcedure(containedLink);
                    }
                }
            }
        }
    }
}
