namespace SxqClient.Http
{
    using System;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    internal class HttpConnection
    {
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => 
            true;

        public static HttpWebRequest GetRequest(string url, HttpMethod method, HttpHeader header)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpConnection.CheckValidationResult);
            HttpWebRequest request1 = (HttpWebRequest) WebRequest.Create(url);
            request1.Method = method.ToString();
            request1.Accept = "text/plain,application/json";
            request1.UserAgent = "sxq-csharp-sdk";
            request1.Headers.Set("Accept-Encoding", "gzip,deflate");
            request1.Headers.Add("x-sxq-open-timestamp", header.Timestamp.ToString());
            request1.Headers.Add("x-sxq-open-signature", header.Signature.ToLower());
            request1.Headers.Add("x-sxq-open-accesstoken", header.AccessToken);
            request1.Headers.Add("x-sxq-open-accesssecret", header.AccessSecret);
            request1.Headers.Add("version", header.Version);
            return request1;
        }
    }
}

