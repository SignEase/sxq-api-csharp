namespace SxqClient.Http
{
    using SxqClient.Tools;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Text;

    internal class HttpClient
    {
        public static string CONTENT_FORM = "application/x-www-form-urlencoded;charset=UTF-8";
        public static string CONTENT_MULTIPART = "multipart/form-data;charset=UTF-8; boundary=";
        public static string JSON_CONTENT_FORM = "application/json;charset=UTF-8";
        public static string DEFAULT_CHARSET = "UTF-8";
        public static string CONTENT_ENCODING_GZIP = "gzip";

        private static string BuildGetUrl(string url, string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return url;
            }
            bool flag = url.Contains("?");
            string str = url;
            if (!url.EndsWith("?") && !url.EndsWith("&"))
            {
                if (flag)
                {
                    str = str + "&";
                }
                else
                {
                    str = str + "?";
                }
            }
            return (str + query);
        }

        public static void DoDownload(string url, HttpParamers paramers, HttpHeader header, int connectTimeout, int readTimeout, ref Stream stream)
        {
            try
            {
                string queryString = paramers.GetQueryString();
                HttpWebRequest request = HttpConnection.GetRequest(BuildGetUrl(url, queryString), HttpMethod.GET, header);
                request.ReadWriteTimeout = readTimeout;
                request.Timeout = connectTimeout;
                GetResponseAsOutputStream(request, ref stream);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
        }

        public static string DoGet(string url, HttpParamers paramers, HttpHeader header, int connectTimeout, int readTimeout)
        {
            string responseAsString;
            try
            {
                string queryString = paramers.GetQueryString();
                HttpWebRequest request = HttpConnection.GetRequest(BuildGetUrl(url, queryString), HttpMethod.GET, header);
                request.ReadWriteTimeout = readTimeout;
                request.Timeout = connectTimeout;
                responseAsString = GetResponseAsString(request);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return responseAsString;
        }

        public static string DoPost(string url, HttpParamers paramers, HttpHeader header, int connectTimeout, int readTimeout)
        {
            Stream requestStream = null;
            HttpWebRequest request = null;
            string responseAsString;
            try
            {
                request = HttpConnection.GetRequest(url, HttpMethod.POST, header);
                request.ReadWriteTimeout = readTimeout;
                request.Timeout = connectTimeout;
                if (paramers.IsMultipart())
                {
                    string boundary = "----sdkboundary" + StringUtils.Random(6);
                    request.ContentType = CONTENT_MULTIPART + boundary;
                    Stream stream2 = new MemoryStream();
                    WriteMutiContent(boundary, paramers, ref stream2);
                    request.ContentLength = stream2.Length;
                    stream2.Position = 0L;
                    byte[] buffer = new byte[stream2.Length];
                    stream2.Read(buffer, 0, buffer.Length);
                    stream2.Close();
                    requestStream = request.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Close();
                }
                else if (paramers.IsJsonApplication())
                {
                    string jsonParamer = paramers.JsonParamer;
                    byte[] bytes = Encoding.UTF8.GetBytes(jsonParamer);
                    request.ContentType = JSON_CONTENT_FORM;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                else
                {
                    string queryString = paramers.GetQueryString();
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(queryString);
                        request.ContentType = CONTENT_FORM;
                        requestStream = request.GetRequestStream();
                        requestStream.Write(bytes, 0, bytes.Length);
                        requestStream.Close();
                    }
                }
                responseAsString = GetResponseAsString(request);
            }
            catch (Exception exception1)
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
                throw exception1;
            }
            return responseAsString;
        }

        public static string DoService(string url, HttpParamers paramers, HttpHeader header, int connectTimeout, int readTimeout)
        {
            string str;
            HttpMethod method = paramers.Method;
            try
            {
                if (method != HttpMethod.GET)
                {
                    if (method != HttpMethod.POST)
                    {
                        throw new Exception("不支持的HTTP方法类型");
                    }
                    return DoPost(url, paramers, header, connectTimeout, readTimeout);
                }
                str = DoGet(url, paramers, header, connectTimeout, readTimeout);
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return str;
        }

        private static byte[] GetFileEntry(string fieldName, string fileName, string mimeType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Content-Disposition:form-data;name=\"");
            builder.Append(fieldName);
            builder.Append("\";filename=\"");
            builder.Append(fileName);
            builder.Append("\"\r\nContent-Type:");
            builder.Append(mimeType);
            builder.Append("\r\n\r\n");
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        private static void GetResponseAsOutputStream(WebRequest request, ref Stream outputStream)
        {
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode < HttpStatusCode.BadRequest)
            {
                int num;
                Stream responseStream = response.GetResponseStream();
                byte[] buffer = new byte[0x2000];
                while ((num = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, num);
                }
            }
            else
            {
                response.Close();
                throw new IOException(response.StatusCode.ToString() + " " + response.StatusDescription);
            }
            response.Close();
        }

        public static string GetResponseAsString(HttpWebRequest request)
        {
            string responseCharset = GetResponseCharset(request.ContentType);
            string statusDescription = "";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                if (response.StatusCode < HttpStatusCode.BadRequest)
                {
                    Stream responseStream = response.GetResponseStream();
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                    {
                        responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                    }
                    return new StreamReader(responseStream, Encoding.GetEncoding(responseCharset)).ReadToEnd();
                }
                statusDescription = response.StatusDescription;
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return statusDescription;
        }

        public static string GetResponseCharset(string ctype)
        {
            string str = DEFAULT_CHARSET;
            if (!string.IsNullOrEmpty(ctype))
            {
                char[] separator = new char[] { ';' };
                foreach (string str2 in ctype.Split(separator))
                {
                    if (str2.StartsWith("charset"))
                    {
                        char[] chArray2 = new char[] { '=' };
                        string[] strArray2 = str2.Split(chArray2, 2);
                        if ((strArray2.Length == 2) && !string.IsNullOrEmpty(strArray2[1]))
                        {
                            str = strArray2[1];
                        }
                        return str;
                    }
                }
            }
            return str;
        }

        public static string getStreamAsString(Stream stream, string charset)
        {
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset)))
            {
                return reader.ReadToEnd();
            }
        }

        private static byte[] GetTextEntry(string fieldName, string fieldValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Content-Disposition:form-data;name=\"");
            builder.Append(fieldName);
            builder.Append("\"\r\nContent-Type:text/plain\r\n\r\n");
            builder.Append(fieldValue);
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        private static void WriteMutiContent(string boundary, HttpParamers paramers, ref Stream stream)
        {
            byte[] bytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            foreach (KeyValuePair<string, string> pair in paramers.Paramers)
            {
                byte[] textEntry = GetTextEntry(pair.Key, pair.Value);
                stream.Write(bytes, 0, bytes.Length);
                stream.Write(textEntry, 0, textEntry.Length);
            }
            if (paramers.FileStreams.Count > 0)
            {
                foreach (KeyValuePair<string, IFileItem> pair2 in paramers.FileStreams)
                {
                    IFileItem item = pair2.Value;
                    if (!item.IsValid())
                    {
                        throw new Exception("无效的文件流");
                    }
                    byte[] buffer4 = GetFileEntry(pair2.Key, item.GetFileName(), item.GetMimeType());
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Write(buffer4, 0, buffer4.Length);
                    item.Write(ref stream);
                }
            }
            if (paramers.Files.Count > 0)
            {
                foreach (KeyValuePair<string, List<IFileItem>> pair3 in paramers.Files)
                {
                    foreach (IFileItem item2 in pair3.Value)
                    {
                        if (!item2.IsValid())
                        {
                            throw new Exception("无效的文件流");
                        }
                        byte[] buffer5 = GetFileEntry(pair3.Key, item2.GetFileName(), item2.GetMimeType());
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Write(buffer5, 0, buffer5.Length);
                        item2.Write(ref stream);
                    }
                }
            }
            byte[] buffer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}

