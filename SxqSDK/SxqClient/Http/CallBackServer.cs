namespace SxqClient.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// 回调监听和付处理服务，需自行实现CallBackServer.ProcessBiz方法里的逻辑
    /// </summary>
    public class CallBackServer
    {
        const string LISTEN_URL = "http://127.0.0.1:7777/sxq-callback/";

        public CallBackServer() { }

        /// <summary>
        /// Post请求中的参数值
        /// </summary>
        public class PostParameter
        {
            const int TYPE_PARAMETER = 0;  // 参数
            const int TYPE_FILE = 1;   // 文件

            public int type = TYPE_PARAMETER;
            public string name;
            public byte[] datas;

            public bool matchName(string name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    return false;
                }

                return name.Equals(this.name);
            }

            public string getValue()
            {
                if (this.datas == null || this.datas.Length <= 0)
                {
                    return null;
                }
                //return System.Text.Encoding.Default.GetString(this.datas).Replace("\n","").Replace("\r","");
                return System.Text.Encoding.Default.GetString(this.datas);
            }
        }

        private static bool CompareBytes(byte[] source, byte[] comparison)
        {
            try
            {
                int count = source.Length;
                if (source.Length != comparison.Length)
                    return false;
                for (int i = 0; i < count; i++)
                    if (source[i] != comparison[i])
                        return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static byte[] ReadLineAsBytes(Stream SourceStream)
        {
            var resultStream = new MemoryStream();
            while (true)
            {
                int data = SourceStream.ReadByte();
                resultStream.WriteByte((byte)data);
                if (data == 10)
                    break;
            }
            resultStream.Position = 0;
            byte[] dataBytes = new byte[resultStream.Length];
            resultStream.Read(dataBytes, 0, dataBytes.Length);
            return dataBytes;
        }

        /// <summary>
        /// 获取Post过来的参数和数据
        /// </summary>
        /// <returns></returns>
        private static List<PostParameter> ProcessPostRequest(HttpListenerContext context)
        {
            List<PostParameter> postValueList = new List<PostParameter>();
            try
            {
                if (context.Request.ContentType.Length > 20 && string.Compare(context.Request.ContentType.Substring(0, 20), "multipart/form-data;", true) == 0)
                {
                    //string[] HttpListenerPostValue = context.Request.ContentType.Split(';').Skip(1).ToArray();
                    //string boundary = string.Join(";", HttpListenerPostValue).Replace("boundary=", "").Trim();
                    int splitStart = context.Request.ContentType.IndexOf(";");
                    string contentRemain = context.Request.ContentType.Substring(splitStart + 1);
                    string boundary = contentRemain.Replace("boundary=", "").Trim();

                    byte[] ChunkBoundary = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
                    byte[] EndBoundary = Encoding.UTF8.GetBytes("--" + boundary + "--\r\n");
                    Stream SourceStream = context.Request.InputStream;
                    var resultStream = new MemoryStream();
                    bool CanMoveNext = true;
                    PostParameter data = null;
                    while (CanMoveNext)
                    {
                        byte[] currentChunk = ReadLineAsBytes(SourceStream);
                        if (!Encoding.UTF8.GetString(currentChunk).Equals("\r\n"))
                            resultStream.Write(currentChunk, 0, currentChunk.Length);
                        if (CompareBytes(ChunkBoundary, currentChunk))
                        {
                            byte[] result = new byte[resultStream.Length - ChunkBoundary.Length];
                            resultStream.Position = 0;
                            resultStream.Read(result, 0, result.Length);
                            CanMoveNext = true;
                            if (result.Length > 0)
                                data.datas = result;
                            data = new PostParameter();
                            postValueList.Add(data);
                            resultStream.Dispose();
                            resultStream = new MemoryStream();

                        }
                        else if (Encoding.UTF8.GetString(currentChunk).Contains("Content-Disposition"))
                        {
                            byte[] result = new byte[resultStream.Length - 2];
                            resultStream.Position = 0;
                            resultStream.Read(result, 0, result.Length);
                            CanMoveNext = true;
                            data.name = Encoding.UTF8.GetString(result).Replace("Content-Disposition: form-data; name=\"", "").Replace("\"", "").Split(';')[0];
                            resultStream.Dispose();
                            resultStream = new MemoryStream();
                        }
                        else if (Encoding.UTF8.GetString(currentChunk).Contains("Content-Type"))
                        {
                            CanMoveNext = true;
                            data.type = 1;
                            resultStream.Dispose();
                            resultStream = new MemoryStream();
                        }
                        else if (CompareBytes(EndBoundary, currentChunk))
                        {
                            byte[] result = new byte[resultStream.Length - EndBoundary.Length - 2];
                            resultStream.Position = 0;
                            resultStream.Read(result, 0, result.Length);
                            data.datas = result;
                            resultStream.Dispose();
                            CanMoveNext = false;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Process faild: " + e.Message);
            }

            return postValueList;
        }

        /// <summary>
        /// 初始化并启动回调监听
        /// </summary>
        /// <param name="listenUrl">监听的URL</param>
        public static void Inst(string listenUrl)
        {
            try
            {
                HttpListener listerner = new HttpListener();
                {
                    while (true)
                    {
                        try
                        {
                            listerner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//Anonymous匿名访问
                            listerner.Prefixes.Add(string.IsNullOrEmpty(listenUrl) ? LISTEN_URL : listenUrl); //监听端口
                            listerner.Start();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Init the callback server failed: " +  e.Message);
                            continue;
                        }
                        break;
                    }
                    Console.WriteLine("Callback server starup....");
                    while (true)
                    {
                        //等待请求连接(阻塞状态)
                        HttpListenerContext ctx = listerner.GetContext();
                        ThreadPool.QueueUserWorkItem(new WaitCallback(TaskProc), ctx);
                    }
                    //con.Close();
                    //listerner.Stop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Process faild: " + e.Message);
                //Console.Write("Press any key to continue . . . ");
                //Console.ReadKey();
            }

        }

        /// <summary>
        /// http请求处理并返回客户端处理结果：
        /// - 支持GET和POST请求，读取2个变量：contractId和notaryUrl
        /// - 处理成功：status code=200；处理失败/异常：status code=500
        /// </summary>
        /// <param name="httpContext">http请求体</param>
        private static void TaskProc(object httpContext)
        {
            HttpListenerContext context = (HttpListenerContext)httpContext;
            string msg = "";
            try
            {
                Console.WriteLine("Start to process " + context.Request.HttpMethod + " request from " + context.Request.RemoteEndPoint);
                long contractId = -1;
                string notaryUrl = "";
                if ("POST".Equals(context.Request.HttpMethod))
                {
                    List<PostParameter> parameters = ProcessPostRequest(context);
                    foreach (PostParameter param in parameters)
                    {
                        if (param.matchName("contractId"))
                        {
                            contractId = long.Parse(param.getValue());
                        }
                        else if (param.matchName("notaryUrl"))
                        {
                            notaryUrl = param.getValue();
                        }
                    }
                }
                else if ("GET".Equals(context.Request.HttpMethod))
                {
                    contractId = long.Parse(context.Request.QueryString["contractId"]);
                    notaryUrl = context.Request.QueryString["notaryUrl"];

                    //string filename = Path.GetFileName(ctx.Request.RawUrl);
                    //string userName = HttpUtility.ParseQueryString(filename).Get("userName");//避免中文乱码
                }

                Console.WriteLine("Receive the contract id is: " + contractId + ", notary url is: " + notaryUrl);
                if (contractId == -1)
                {
                    throw new Exception("没有获取到contract id，无法完成请求处理");
                }

                msg = "请求处理成功[contract id = " + contractId + "]";
                // 状态返回码设置为200，表示处理成功
                context.Response.StatusCode = 200;
                context.Response.StatusDescription = msg;
            }
            catch (Exception e)
            {
                // 状态返回码设置为500，表示处理失败
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = e.Message;
                Console.WriteLine("Process faild: " + e.Message);
                msg = "请求处理失败: " + e.Message;
            }

            // 使用Writer输出http响应代码
            using (StreamWriter responseWriter = new StreamWriter(context.Response.OutputStream))
            {
                responseWriter.Write(context.Request.HttpMethod + msg);
                responseWriter.Close();
                context.Response.Close();
            }
        }



        /// <summary>
        /// 回调处理的实现，请自行编码
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="notaryUrl"></param>
        private void ProcessBiz(long contractId, string notaryUrl)
        {
            //TODO 处理逻辑，如：notaryUrl入库，更新本地对应数据的状态等

        }


    }
}

