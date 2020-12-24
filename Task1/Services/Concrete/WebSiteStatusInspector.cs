using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Task1.Services.Abstract;

namespace Task1.Services.Concrete
{
    public class WebSiteStatusInspector : IWebSiteStatusInspector
    {
        private IExceptionNotificationService _exceptionNotificationServiceManager;

        public WebSiteStatusInspector(IExceptionNotificationService exceptionNotificationServiceManager)
        {
            _exceptionNotificationServiceManager = exceptionNotificationServiceManager;
        }

        public int CheckWebsiteStatus(string uri)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(uri);
                using (var resp = (HttpWebResponse)req.GetResponse())
                {
                    return (int)resp.StatusCode;
                }
            }
            catch (WebException e)
            {
                using (var resp = e.Response as HttpWebResponse)
                {
                    if (resp != null)
                    {
                        return (int)resp.StatusCode;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                _exceptionNotificationServiceManager.ExceptionNotify(e.ToString());
                return -1;
            }
        }
    }
}
