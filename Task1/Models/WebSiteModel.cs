using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Task1.Models
{
    public class WebSiteModel
    {
        static readonly Regex regExprToFindRootUrl = new Regex(@"^(?:https?:\/\/)?(?:[^@\/\n]+@)?(?:www\.)?([^:\/?\n]+)");
        public string URI { get; }
        public int StatusCode { get; set; }
        public List<WebSiteModel> ContaiedLinks { get; }
        public string RootUrl { get; }
        public int Nesting { get; }

        public WebSiteModel(string URI, int Nesting)
        {
            ContaiedLinks = new List<WebSiteModel>();
            StatusCode = -1;
            this.URI = URI;
            this.Nesting = Nesting;
            RootUrl = regExprToFindRootUrl.Match(URI).Value + "/";
        }

        public override string ToString()
        {
            if (URI.Contains(",") || URI.Contains(";"))
            {
                return @$"""{URI}"", {StatusCode}";
            }
            return $"{URI}, {StatusCode}";
        }

        public void AddToContaiedLinks(string uri)
        {
            if (!ContaiedLinks.Exists(x => x.URI == uri))
            {
                ContaiedLinks.Add(new WebSiteModel(uri, Nesting - 1));
            }
        }
    }
}
