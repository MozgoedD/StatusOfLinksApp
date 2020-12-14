using System;
using System.Collections.Generic;
using System.Text;
using Task1.Services.Abstract;

namespace Task1.ServiceManagers
{
    public class WebSiteStatusInspectorManager : IWebSiteStatusInspector
    {
        IWebSiteStatusInspector _webSiteStatusInspector;

        public WebSiteStatusInspectorManager(IWebSiteStatusInspector webSiteStatusInspector)
        {
            _webSiteStatusInspector = webSiteStatusInspector;
        }
        public int CheckWebsiteStatus(string URI)
        {
            return _webSiteStatusInspector.CheckWebsiteStatus(URI);
        }
    }
}
