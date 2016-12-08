using System;
using System.Net;

namespace NGitHubdater
{
    class CustomWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest webRequest = base.GetWebRequest(address);

            /*if (webRequest is HttpWebRequest)
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)webRequest;
                httpWebRequest.KeepAlive = false;
            }*/

            return webRequest;
        }
    }
}
