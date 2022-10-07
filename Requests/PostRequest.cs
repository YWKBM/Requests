using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Requests
{
    public class PostRequest
    {
        string Address;
        HttpWebRequest Request;

        public Dictionary<string, string> Headers { get; set; }

        //Для пост запроса дополнительно используются Data, ContentType
        public string Response { get; set; }   
        public string Data { get; set; }
        public string ContentType { get; set; }
        public string Host { get; set; }
        public string Accept { get; set; }
        public string Referer { get; set; }
        public string Useragent { get; set; }
        public WebProxy Proxy { get; set; }

        public PostRequest(string adress)
        {
            Address = adress;
            Headers = new Dictionary<string, string>();   
        }

        public void Run(CookieContainer cookieContainer)
        {
            Request = (HttpWebRequest)WebRequest.Create(Address);
            Request.Method = "POST";
            Request.CookieContainer = cookieContainer;
            Request.Proxy = Proxy;
            Request.Accept = Accept;
            Request.Host = Host;
            Request.Referer = Referer;
            Request.UserAgent = Useragent;
            Request.ContentType = ContentType;

            byte[] sentData = Encoding.UTF8.GetBytes(Data);
            Request.ContentLength = sentData.Length;
            Stream sendStream = Request.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close(); 

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
