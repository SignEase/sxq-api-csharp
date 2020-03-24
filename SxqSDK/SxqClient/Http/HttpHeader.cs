namespace SxqClient.Http
{
    using System;

    internal class HttpHeader
    {
        private string contentType;
        private double timestamp;
        private string accessToken;
        private string accessSecret;
        private string signature;
        private string version;

        public HttpHeader(string accessToken,string accessSecret, double timestamp, string signature, string version)
        {
            this.AccessToken = accessToken;
            this.AccessSecret = accessSecret;
            this.Timestamp = timestamp;
            this.Signature = signature;
            this.Version = version;
        }

        public string ContentType
        {
            get
            {
                return this.contentType;
            }
            set
            {
                this.contentType = value;
            }
        }

        public double Timestamp
        {
            get
            {
                return this.timestamp;
            }
            set
            {
                this.timestamp = value;
            }
        }

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

        public string Signature
        {
            get
            {
                return this.signature;
            }
            set
            {
                this.signature = value;
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }
    }
}

