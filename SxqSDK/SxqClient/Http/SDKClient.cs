namespace SxqClient.Http
{
    using SxqClient.Tools;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class SDKClient
    {
        private string VERSION;
        private string accessToken;
        private string accessSecret;
        private string serverUrl;
        private int connectTimeout;
        private int readTimeout;

        public SDKClient(string accessToken, string accessSecret, string serverUrl)
        {
            this.VERSION = "C#-3.0.2";
            this.connectTimeout = 0x3a98;
            this.readTimeout = 0x7530;
            this.accessToken = accessToken.Trim();
            this.accessSecret = accessSecret.Trim();
            this.serverUrl = serverUrl.Trim();
        }

        public SDKClient(string accessToken, string accessSecret, string serverUrl, int connectTimeout, int readTimeout) : this(accessToken, accessSecret, serverUrl)
        {
            this.connectTimeout = connectTimeout;
            this.readTimeout = readTimeout;
        }

        private void PolyfixOfOldApi(ref HttpParamers paramers)
        {
            // old sxqian api polly fill
            paramers.AddParamer("yclDataStore.appKey", this.AccessToken);
            paramers.AddParamer("yclDataStore.appSecret", this.AccessSecret);
        }

        private void PolyfixOfOldDownloadApi(ref HttpParamers paramers)
        {
            // old sxqian api polly fill
            paramers.AddParamer("appKey", this.AccessToken);
            paramers.AddParamer("appSecret", this.AccessSecret);
        }

        public void Download(IHttpRequest request, ref Stream outputStream)
        {
            this.HttpDownload(request.GetRequestPath(), request.GetHttpParamers(), ref outputStream);
        }

        public void HttpDownload(string serviceUrl, HttpParamers paramers, ref Stream outputStream)
        {
            string url = this.ServerUrl + serviceUrl;
            double totalMilliseconds = DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
            byte[] inBytes = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(this.AccessToken + this.AccessSecret + totalMilliseconds.ToString()));
            string signature = StringUtils.ByteToString(inBytes, inBytes.Length);
            HttpHeader header = new HttpHeader(this.AccessToken, totalMilliseconds, signature, this.VERSION);
            PolyfixOfOldDownloadApi(ref paramers);
            try
            {
                HttpClient.DoDownload(url, paramers, header, this.connectTimeout, this.readTimeout, ref outputStream);
            }
            catch (Exception exception1)
            {
                throw new Exception(exception1.Message);
            }
        }

        public string HttpService(string serviceUrl, HttpParamers paramers)
        {
            string str3;
            string url = this.ServerUrl + serviceUrl;
            double totalMilliseconds = DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
            byte[] inBytes = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(this.AccessToken + this.AccessSecret + totalMilliseconds.ToString()));
            string signature = StringUtils.ByteToString(inBytes, inBytes.Length);
            HttpHeader header = new HttpHeader(this.AccessToken, totalMilliseconds, signature, this.VERSION);
            PolyfixOfOldApi(ref paramers);
            try
            {
                str3 = HttpClient.DoService(url, paramers, header, this.connectTimeout, this.readTimeout);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return str3;
        }

            public string Service(IHttpRequest request) => 
            this.HttpService(request.GetRequestPath(), request.GetHttpParamers());

        public string AccessToken
        {
            get
            {
                return this.accessToken;
            }
            set
            {
                this.accessToken = value;
            }
        }

        public string AccessSecret
        {
            get
            {
                return this.accessSecret;
            }
            set
            {
                this.accessSecret = value;
            }
        }

        public string ServerUrl
        {
            get
            {
                return this.serverUrl;
            }
            set
            {
                this.serverUrl = value;
            }
        }
    }
}

