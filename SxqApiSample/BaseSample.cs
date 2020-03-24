using SxqClient.Http;
using SxqCore.Bean;
using SxqCore.Bean.Request;
using SxqCore.Bean.Response;
using SxqCore.Tools;
using System;
using System.IO;

namespace SxqApiSample
{
    class BaseSample
    {
        /// <summary>
        /// Ping服务器是否联通
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public SdkResponse<PingResult> Ping(SDKClient client)
        {
            PingRequest request = new PingRequest();
            string response = null;
            try
            {
                response = client.Service(request);
            }
            catch (Exception e)
            {
                throw new Exception("Ping服务器失败,失败原因： " + e.Message);
            }

            SdkResponse<PingResult> sdkResponse = HttpJsonConvert.DeserializeResponse<PingResult>(response);
            if (!sdkResponse.Success)
            {
                throw new Exception("Ping服务器失败，失败原因： " + sdkResponse.Message);
            }
            return sdkResponse;
        }

        /// <summary>
        /// 取回已签约/已存证文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="contractId">合同编号</param>
        /// <param name="filePath">文件的保存路径</param>
        /// <returns></returns>
        public void Fetch(SDKClient client, string contractId, string filePath)
        {
            DownloadContractRequest request = new DownloadContractRequest(contractId);
            try
            {
                Stream outputStream = new MemoryStream();
                client.Download(request,ref outputStream);

                MemoryStream memoryStream = (MemoryStream) outputStream;
                FileStream fs = new FileStream(filePath, FileMode.Create);
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(memoryStream.ToArray());
                fs.Close();
                memoryStream.Close();
            }
            catch (Exception e)
            {
                throw new Exception("取回文件失败,失败原因： " + e.Message);
            }
        }

        public BaseSample() { }
    }
}
