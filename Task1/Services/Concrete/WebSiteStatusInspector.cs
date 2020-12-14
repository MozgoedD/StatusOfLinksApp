using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Task1.Services.Abstract;

namespace Task1.Services.Concrete
{
    class WebSiteStatusInspector : IWebSiteStatusInspector
    {
        public int CheckWebsiteStatus(string URI)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URI);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                return (int)resp.StatusCode;
            }
            catch (WebException e)
            {
                var responce = e.Response as HttpWebResponse;
                if (responce != null)
                {
                    return (int)responce.StatusCode;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(URI+" Exception:");
                Console.WriteLine(e);
                return -1;
            }
        }
    }
}
