using SxqClient.Http;
using System;
using SxqCore.Bean.Quick;
using SxqCore.Bean.Response;
using SxqCore.Tools;
using System.Collections.Generic;

namespace SxqApiSample
{
    class Program
    {
        /** -开始联调前，请设置以下参数- **/
        private static int env = 0; // 0-测试&联调环境; 1-线上环境
        public static string accessSecret = "";
        public static string accessToken = "";
        public static string serverUrl = "";
        /** ------------------------  **/

        private static SDKClient client = null;
        static private SDKClient GetOrCreateClient()
        {
            if(client != null)
            {
                return client;
            }
            // 请使用你注册账户的Secrect; 否则会使用以下测试环境默认Secret
            accessSecret = string.IsNullOrEmpty(accessSecret) ? "3daca3b13ef04e7f8a751d74c8318a1f" : accessSecret;
            // 请使用你注册账户的Token; 否则会使用以下测试环境默认Token
            accessToken = string.IsNullOrEmpty(accessToken) ? "20200303093507658157" : accessToken; 
            // 指定访问的服务器
            serverUrl = (env == 0) ? "https://mock.sxqian.com" : "https://sxqian.com";
            
            return client = new SDKClient(accessToken, accessSecret, serverUrl); 
        }

        /// <summary>
        /// 测试服务器是否能联通
        /// </summary>
        static private void Ping()
        {
            BaseSample baseSample = new BaseSample();
            SdkResponse<PingResult> pingRS = baseSample.Ping(GetOrCreateClient());
            Console.WriteLine("Service time: {0} , Local time: {1}", pingRS.Result.ServiceTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine("Call Ping api finished");
        }

        /// <summary>
        /// 取回已签约/存证的合同
        /// </summary>
        static private void Fetch()
        {
            // 合同签约后返回的合同存证编号
            //string storeNo = "YC0001046440";
            string storeNo = "YC0001046455";
            // 存储到本地的文件路径
            string localFilePath = "../../Contract/Signed/ContractFetched.pdf";
            BaseSample baseSample = new BaseSample();
            baseSample.Fetch(GetOrCreateClient(), storeNo, localFilePath);
            Console.WriteLine("Fetch file success, you can find the file at {0}" , localFilePath);
            Console.WriteLine("Call Fetch api finished");
        }

        /// <summary>
        /// 快捷签约
        /// </summary>
        /// <param name="caseType">
        /// 0: TwoPeopleSign;
        /// 1: CompanyAndPersonSign;
        /// 2: TwoCompanySign;
        /// 3: MultiplePeopleSign;
        /// 4: MultiplePartiesSign
        /// </param>
        static private void QuickSignContract(int signType)
        {
            QuickSignatorySample quickSignatorySample = new QuickSignatorySample();

            switch (signType)
            {
                case 0:
                    quickSignatorySample.TwoPeopleSign(GetOrCreateClient());
                    break;
                case 1:
                    quickSignatorySample.CompanyAndPersonSign(GetOrCreateClient());
                    break;
                case 2:
                    quickSignatorySample.TwoCompanySign(GetOrCreateClient());
                    break;
                case 3:
                    quickSignatorySample.MultiplePeopleSign(GetOrCreateClient());
                    break;
                case 4:
                    quickSignatorySample.MultiplePartiesSign(GetOrCreateClient());
                    break;
                default:
                    break;
            }
            Console.WriteLine("Call QucikSign api finished");
        }


        /// <summary>
        /// 普通签约
        /// </summary>
        /// <param name="caseType">
        /// 
        /// </param>
        static private void SignContract(int signType)
        {
            QuickSignatorySample quickSignatorySample = new QuickSignatorySample();

            switch (signType)
            {
                case 0:
                    quickSignatorySample.TwoPeopleSign(GetOrCreateClient());
                    break;
                case 1:
                    quickSignatorySample.CompanyAndPersonSign(GetOrCreateClient());
                    break;
                case 2:
                    quickSignatorySample.TwoCompanySign(GetOrCreateClient());
                    break;
                case 3:
                    quickSignatorySample.MultiplePeopleSign(GetOrCreateClient());
                    break;
                case 4:
                    quickSignatorySample.MultiplePartiesSign(GetOrCreateClient());
                    break;
                default:
                    break;
            }
            Console.WriteLine("Call QucikSign api finished");
        }

        static void Main(string[] args)
        {
            //Ping();
            //Fetch();
            //QuickSignContract(0);
            //QuickSignContract(1);
            //QuickSignContract(2);
            //QuickSignContract(3);
            QuickSignContract(4);
        }
        
    }
}
