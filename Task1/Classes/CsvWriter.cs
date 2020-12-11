using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task1.Classes
{
    static class CsvWriter
    {
        static public void WriteCSVToFile(Settings settings, StringBuilder csv)
        {
            try
            {
                File.AppendAllText($"{settings.FileName}", csv.ToString());
            }
            catch (Exception e)
            {
                EmailUtils.ExceptionSend(settings, $"An Exception occured: {e.Message} while writing to .csv file");
            }
        }
    }
}
