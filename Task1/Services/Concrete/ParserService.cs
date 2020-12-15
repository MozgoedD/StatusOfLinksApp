using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Task1.Models;
using Task1.Services.Abstract;

namespace Task1.Services.Concrete
{
    public class ParserService : IParserService
    {
        IExceptionNotificationService exceptionNotificationServiceManager;
        Regex RegExprToParseAllLinks = new Regex(@"<a\s+(?:[^>]*?\s+)?href=""([^""]*)""");
        string Html;

        public ParserService(IExceptionNotificationService exceptionNotificationServiceManager)
        {
            this.exceptionNotificationServiceManager = exceptionNotificationServiceManager;
        }

        public void ParseAllLinksFromWebSite(WebSiteModel baseWebSite)
        {
            try
            {
                var baseUriObj = new Uri(baseWebSite.URI);
                Html = new WebClient().DownloadString(baseUriObj);
            }
            catch (Exception e)
            {
                exceptionNotificationServiceManager.ExceptionNotify(e.ToString());
            }
            foreach (Match match in RegExprToParseAllLinks.Matches(Html))
            {
                var rawLink = match.Groups[1].ToString();
                var containedUriObj = new Uri(rawLink, UriKind.RelativeOrAbsolute);
                if (containedUriObj.IsAbsoluteUri)
                {
                    baseWebSite.AddToContaiedLinks(rawLink);
                }
                else
                {
                    baseWebSite.AddToContaiedLinks(baseWebSite.RootUrl + rawLink);
                }
            }
        }
    }
}
