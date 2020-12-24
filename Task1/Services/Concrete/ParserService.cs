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
        private IExceptionNotificationService _exceptionNotificationServiceManager;
        private Regex regExprToParseAllLinks = new Regex(@"<a\s+(?:[^>]*?\s+)?href=""([^""]*)""");
        private string html;

        public ParserService(IExceptionNotificationService exceptionNotificationServiceManager)
        {
            _exceptionNotificationServiceManager = exceptionNotificationServiceManager;
        }

        public void ParseAllLinksFromWebSite(WebSiteModel baseWebSite)
        {
            try
            {
                var baseUriObj = new Uri(baseWebSite.URI);
                html = new WebClient().DownloadString(baseUriObj);
            }
            catch (Exception e)
            {
                _exceptionNotificationServiceManager.ExceptionNotify(e.ToString());
            }
            foreach (Match match in regExprToParseAllLinks.Matches(html))
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
