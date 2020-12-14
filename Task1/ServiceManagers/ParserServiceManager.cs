using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models;
using Task1.Services.Abstract;

namespace Task1.ServiceManagers
{
    public class ParserServiceManager : IParserService
    {
        IParserService _parserService;
        public ParserServiceManager(IParserService parserService)
        {
            _parserService = parserService;
        }
        public void ParseAllLinksFromWebSite(WebSiteModel webSite)
        {
            _parserService.ParseAllLinksFromWebSite(webSite);
        }
    }
}
