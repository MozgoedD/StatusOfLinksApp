using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Task1.Classes;

namespace Task1.Models
{
    class Link
    {
        string URI;
        string RootURL;
        int Nesting;
        Settings settings;
        List<Link> Links = new List<Link>();

        public Link(string URI, int Nesting, Settings settings)
        {
            this.URI = URI;
            this.Nesting = Nesting;
            this.settings = settings;

            Regex regExprToFindURL1 = new Regex(@"^(?:https?:\/\/)?(?:[^@\/\n]+@)?(?:www\.)?([^:\/?\n]+)");
            RootURL = regExprToFindURL1.Match(URI).Value + "/";
        }

        void AddLinkToList(string uri, int nesting)
        {
            if (!Links.Exists(x => x.URI == uri))
            {
                Links.Add(LinkFactory.CreateLink(uri, nesting - 1, settings));
            }
        }

        public void ExtractAllUrls()
        {
            var csv = new StringBuilder();
            Regex regExprToFindLinks = new Regex(@"<a\s+(?:[^>]*?\s+)?href=""([^""]*)""");
            string html = "";


            try
            {
                Uri uriObj = new Uri(URI);
                html = new WebClient().DownloadString(uriObj);
            }
            catch (UriFormatException e)
            {
                EmailUtils.ExceptionSend(settings, $"User entered not valid link: {URI}: {e.Message}");
            }
            catch (Exception e)
            {
                EmailUtils.ExceptionSend(settings, $"An Exception occured while Extracting urls from {URI}: {e.Message}");
            }

            foreach (Match match in regExprToFindLinks.Matches(html))
            {
                string rawLink = match.Groups[1].ToString();
                if (rawLink.StartsWith("http"))
                {
                    AddLinkToList(rawLink, Nesting);
                }
                else if (rawLink.StartsWith("//"))
                {
                    rawLink = rawLink.Insert(0, "http:");
                    AddLinkToList(rawLink, Nesting);
                }
                else if (rawLink.StartsWith("./"))
                {
                    rawLink = rawLink.Remove(0, 2);
                    rawLink = RootURL + rawLink;
                    AddLinkToList(rawLink, Nesting);
                }
                else if (rawLink.StartsWith("/"))
                {
                    rawLink = rawLink.Remove(0, 1);
                    rawLink = RootURL + rawLink;
                    AddLinkToList(rawLink, Nesting);
                }
                else
                {
                    rawLink = URI + rawLink;
                    AddLinkToList(rawLink, Nesting);
                }
            }
        }

        public string CheckWebsiteStatus()
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URI);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                return $"{(int)resp.StatusCode}";
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    var responce = e.Response as HttpWebResponse;
                    if (responce != null)
                    {
                        return $"{(int)responce.StatusCode}";
                    }
                    else
                    {
                        return "No status code avialable";
                    }
                }
                else
                {
                    return $"No status code avialable {e.Status}";
                }
            }

            catch (Exception e)
            {
                EmailUtils.ExceptionSend(settings, $"An Exception occured: {e.Message} in processing {URI}");
                return $"No status code avialable {e}";
            }

        }

        public void WriteReport()
        {
            var csv = new StringBuilder();

            foreach (Link link in Links)
            {
                string status = CheckWebsiteStatus();
                string uri = link.URI;

                if (uri.Contains(",") || uri.Contains(";"))
                {
                    uri = @$"""{uri}""";
                }

                string line = $"{uri}, {status}";

                Console.WriteLine(line);
                csv.AppendLine(line);

                if (link.Nesting >= 1)
                {
                    link.ExtractAllUrls();
                    link.WriteReport();
                }
            }
            CsvWriter.WriteCSVToFile(settings, csv);
        }

    }
}
