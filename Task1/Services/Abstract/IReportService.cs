using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models;

namespace Task1.Services.Abstract
{
    public interface IReportService
    {
        public void WriteReport(WebSiteModel WebSiteToReport);
    }
}
