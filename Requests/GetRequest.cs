using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Requests
{
    public class GetRequest
    {
        string Address;
        HttpWebRequest Request;

        public Dictionary<string, string> Headers { get; set; }

        //стандартные заголовки HTTP запроса: Accept, Host, Data, ContentType, Referer, Useragent
        public string Response { get; set; }   
        public string Host { get; set; }
        public string Accept { get; set; }
        public string Referer { get; set; }
        public string Useragent { get; set; }
        public WebProxy Proxy { get; set; }

        public GetRequest(string adress)
        {
            Address = adress;
            Headers = new Dictionary<string, string>();   
        }

        //Получаем ответ
        public void Run(CookieContainer cookieContainer)
        {
            Request = (HttpWebRequest)WebRequest.Create(Address);
            Request.Method = "Get";
            Request.CookieContainer = cookieContainer;
            Request.Proxy = Proxy;
            Request.Accept = Accept;
            Request.Host = Host;
            Request.Referer = Referer;
            Request.UserAgent = Useragent;

            foreach(var pair in Headers)
            {
                Request.Headers.Add(pair.Key, pair.Value);  
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    Response = new StreamReader(stream).ReadToEnd();
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
