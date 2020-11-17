using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a valid url to the website");
                return 0;
            }

            string Uri = args[0];

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config.json")
                .Build();
            var settings = new Settings();
            configuration.Bind(settings);

            int nesting = Int16.Parse(settings.Nesting);


            Link websiteToTest = new Link(Uri, nesting, settings);

            string mainLinkStatus = websiteToTest.CheckWebsiteStatus();
            if (mainLinkStatus.StartsWith("2") || mainLinkStatus.StartsWith("3"))
            {
                Console.WriteLine($"Main link status: {mainLinkStatus}");
                websiteToTest.ExtractAllUrls();
                websiteToTest.WriteReport();
                Console.WriteLine("DONE");
            }
            else
            {
                Console.WriteLine($"Main link is not avialable: {mainLinkStatus}");
            }

            return 1;
        }
    }
}
