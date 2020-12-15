using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Task1.Models;
using Task1.Services.Abstract;

namespace Task1.Services.Concrete
{
    public class ReportServiceCsvWriter : IReportService
    {
        string ReportPath;
        public ReportServiceCsvWriter(string fileName, string filePath)
        {
            ReportPath = $"{filePath}{fileName}.csv";
        }
        public void WriteReport(WebSiteModel WebSiteToReport)
        {
            var csv = new StringBuilder();
            csv.AppendLine(WebSiteToReport.ToString());
            foreach (var containedLink in WebSiteToReport.ContaiedLinks)
            {
                csv.AppendLine(containedLink.ToString());
            }
            csv.AppendLine("NEXT WEBSITE LINKS,  NEXT WEBSITE LINKS");
            File.AppendAllText(ReportPath, csv.ToString());
        }
    }
}
