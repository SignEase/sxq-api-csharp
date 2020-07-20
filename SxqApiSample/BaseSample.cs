using SxqClient.Http;
using SxqCore.Bean.Request;
using SxqCore.Bean.Response;
using SxqCore.Bean.Contract;
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
        /// 下载已签约/已存证文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="contractId">合同编号</param>
        /// <param name="filePath">文件的保存路径</param>
        /// <returns></returns>
        public void Download(SDKClient client, string contractId, string filePath)
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


        /// <summary>
        /// 查询签约详情
        /// </summary>
        /// <param name="client"></param>
        /// <param name="contractId">合同编号</param>
        /// <returns></returns>
        public SdkResponse<QueryContractResult> QueryContract(SDKClient client, string contractId)
        {
            QueryContractRequest request = new QueryContractRequest(contractId);
            string response = null;
            try
            {
                response = client.Service(request);
            }
            catch (Exception e)
            {
                throw new Exception("QueryContract失败,失败原因： " + e.Message);
            }

            SdkResponse<QueryContractResult> sdkResponse = HttpJsonConvert.DeserializeResponse<QueryContractResult>(response);
            if (!sdkResponse.Success)
            {
                throw new Exception("QueryContract失败，失败原因： " + sdkResponse.Message);
            }
            return sdkResponse;
        }

        /// <summary>
        /// 继续签约的链接
        /// </summary>
        /// <param name="client"></param>
        /// <param name="contractId">合同编号</param>
        /// <returns></returns>
        public string FetchSignUrl(SDKClient client, string contractId)
        {
            FetchSignUrlRequest request = new FetchSignUrlRequest(contractId);
            string response = null;
            try
            {
                response = client.Service(request);
            }
            catch (Exception e)
            {
                throw new Exception("FetchSignUrl失败,失败原因： " + e.Message);
            }

            SdkResponse<SignResult> sdkResponse = HttpJsonConvert.DeserializeResponse<SignResult>(response);
            if (!sdkResponse.Success)
            {
                throw new Exception("FetchSignUrl失败，失败原因： " + sdkResponse.Message);
            }
            return sdkResponse.Result.SignUrl;
        }

        /// <summary>
        /// 用户实名认证
        /// </summary>
        /// <param name="client"></param>
        /// <param name="realNameAuth">实名对象</param>
        /// <returns></returns>
        public string RealName(SDKClient client, RealNameAuth realNameAuth)
        {
            string response = null;
            try
            {
                RealNameRequest request = new RealNameRequest(realNameAuth);
                response = client.Service(request);
            }
            catch (Exception e)
            {
                throw new Exception("RealNameAuth失败,失败原因： " + e.Message);
            }

            SdkResponse<NoCustomResult> sdkResponse = HttpJsonConvert.DeserializeResponse<NoCustomResult>(response);
            if (!sdkResponse.Success)
            {
                throw new Exception("RealNameAuth失败，失败原因： " + sdkResponse.Message);
            }
            return sdkResponse.Success.ToString();
        }

        /// <summary>
        /// 企业用户重新认证企业
        /// </summary>
        /// <param name="client"></param>
        /// <param name="realNameAuth">认证对象</param>
        /// <returns></returns>
        public string EnterpriseReCertification(SDKClient client, RealNameAuth realNameAuth)
        {
            if (!RealNameAuth.AUTH_TYPE_ENTERPRISE.Equals(realNameAuth.type))
            {
                throw new Exception("EnterpriseReCertification失败, 失败原因： 用户类型必须为 RealNameAuth.AUTH_TYPE_ENTERPRISE");
            }
            string response = null;
            try
            {
                RecertificationRequest request = new RecertificationRequest(realNameAuth);
                response = client.Service(request);
            }
            catch (Exception e)
            {
                throw new Exception("EnterpriseReCertification失败,失败原因： " + e.Message);
            }

            SdkResponse<NoCustomResult> sdkResponse = HttpJsonConvert.DeserializeResponse<NoCustomResult>(response);
            if (!sdkResponse.Success)
            {
                throw new Exception("EnterpriseReCertification失败，失败原因： " + sdkResponse.Message);
            }
            return sdkResponse.Success.ToString();
        }


        public BaseSample() { }
    }
}
