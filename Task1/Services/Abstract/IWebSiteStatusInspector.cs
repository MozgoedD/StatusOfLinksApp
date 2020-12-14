using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Services.Abstract
{
    public interface IWebSiteStatusInspector
    {
        public int CheckWebsiteStatus(string URI);
    }
}
