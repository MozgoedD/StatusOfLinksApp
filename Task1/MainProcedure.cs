using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models;
using Task1.Services.Abstract;

namespace Task1
{
    public class MainProcedure
    {
        private IParserService _parserServiceManager;
        private IWebSiteStatusInspector _webSiteStatusInspectorManager;
        private IReportService _reportServiceManager;

        public MainProcedure(IParserService parserServiceManager,
            IWebSiteStatusInspector webSiteStatusInspectorManager, IReportService reportServiceManager)
        {
            _parserServiceManager = parserServiceManager;
            _webSiteStatusInspectorManager = webSiteStatusInspectorManager;
            _reportServiceManager = reportServiceManager;
        }

        public void StartMainProcedure(WebSiteModel baseWebSite)
        {
            _parserServiceManager.ParseAllLinksFromWebSite(baseWebSite);
            Console.WriteLine($"Working with {baseWebSite.URI}");
            foreach (var containedLink in baseWebSite.ContaiedLinks)
            {
                containedLink.StatusCode = _webSiteStatusInspectorManager.CheckWebsiteStatus(containedLink.URI);
            }
            _reportServiceManager.WriteReport(baseWebSite);
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
