using System;
using System.Collections.Generic;
using System.Text;
using Task1.Models;
using Task1.Services.Abstract;

namespace Task1.ServiceManagers
{
    public class ReportServiceManager : IReportService
    {
        IReportService _reportService;
        public ReportServiceManager(IReportService reportService)
        {
            _reportService = reportService;
        }
        public void WriteReport(WebSiteModel WebSiteToReport)
        {
            _reportService.WriteReport(WebSiteToReport);
        }
    }
}
