namespace SxqClient.Http
{
    using SxqClient.Tools;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web;

    public class HttpParamers
    {
        private string jsonParamer;
        private Dictionary<string, string> paramers = new Dictionary<string, string>();
        private Dictionary<string, IFileItem> fileStreams = new Dictionary<string, IFileItem>();
        private Dictionary<string, List<IFileItem>> files = new Dictionary<string, List<IFileItem>>();

        public HttpParamers(HttpMethod method)
        {
            this.Method = method;
        }

        public HttpParamers AddFile(string key, IFileItem stream)
        {
            this.FileStreams.Add(key, stream);
            return this;
        }

        public HttpParamers AddFiles(string key, List<IFileItem> items)
        {
            this.Files.Add(key, items);
            return this;
        }

        public HttpParamers AddParamer(string key, string value)
        {
            this.Paramers.Add(key, value);
            return this;
        }

        public static HttpParamers GetParamers() => 
            new HttpParamers(HttpMethod.GET);

        public string GetQueryString()
        {
            if ((this.Paramers == null) || (this.Paramers.Count == 0))
            {
                return null;
            }
            string str = "";
            foreach (KeyValuePair<string, string> pair in this.Paramers)
            {
                string key = pair.Key;
                string str3 = pair.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(str3))
                {
                    str = str + "&";
                    str = str + key;
                    str = str + "=";
                    str = str + HttpUtility.UrlEncode(str3, Encoding.UTF8);
                }
            }
            return str.Substring(1);
        }

        public bool IsJsonApplication()
        {
            if (this.JsonParamer == null)
            {
                return false;
            }
            return this.JsonParamer.Length > 0;
        }

        public bool IsMultipart()
        {
            if (this.FileStreams.Count == 0)
            {
                return (this.Files.Count != 0);
            }
            return true;
        }

        public static HttpParamers PostJsonParamers(string json) => 
            new HttpParamers(HttpMethod.POST) { JsonParamer = json };

        public static HttpParamers PostParamers() => 
            new HttpParamers(HttpMethod.POST);
       
        internal HttpMethod Method { get; set; }

        public Dictionary<string, string> Paramers
        {
            get
            {
                return this.paramers;
            }
            set
            {
                this.paramers = value;
            }
        }

        internal Dictionary<string, IFileItem> FileStreams
        {
            get
            {
                return this.fileStreams;
            }
            set
            {
                this.fileStreams = value;
            }
        }

        internal Dictionary<string, List<IFileItem>> Files
        {
            get
            {
                return this.files;
            }
            set
            {
                this.files = value;
            }
        }

        public string JsonParamer
        {
            get
            {
                return this.jsonParamer;
            }
            set
            {
                this.jsonParamer = value;
            }
        }
    }
}

