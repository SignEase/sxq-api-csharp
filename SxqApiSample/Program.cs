using SxqClient.Http;
using System;
using SxqCore.Bean.Response;

namespace SxqApiSample
{
    class Program
    {

        /** -开始联调前，请设置以下参数- **/
        private static int env = ENV_MOCK; 
        public static string accessSecret = "";
        public static string accessToken = "";
        /** ------------------------  **/

        private static SDKClient client = null;
        private static SDKClient GetOrCreateClient()
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
            string serverUrl = ParseServer(env);
            
            return client = new SDKClient(accessToken, accessSecret, serverUrl); 
        }

        /// <summary>
        /// 开发环境
        /// </summary>
        private const int ENV_DEV = 1;
        /// <summary>
        /// 联调环境
        /// </summary>
        private const int ENV_MOCK = 2;
        /// <summary>
        /// 正式环境
        /// </summary>
        private const int ENV_ONLINE = 3;

        private static string ParseServer(int env)
        {
            switch (env)
            {
                case ENV_DEV:
                    return "http://127.0.0.1:7878";
                case ENV_MOCK:
                    return "https://mock.sxqian.com";
                case ENV_ONLINE:
                    return "https://sxqian.com";
                default:
                    return "";
            }
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
        static private void Download()
        {
            // 合同签约后返回的合同存证编号
            //string storeNo = "YC0001046440";
            string storeNo = "YC0001046455";
            // 存储到本地的文件路径
            string localFilePath = "../../Contract/Signed/ContractFetched.pdf";
            BaseSample baseSample = new BaseSample();
            baseSample.Fetch(GetOrCreateClient(), storeNo, localFilePath);
            Console.WriteLine("Download file success, you can find the file at {0}" , localFilePath);
            Console.WriteLine("Call Download api finished");
        }

        /// <summary>
        /// 场景：甲乙两人签约
        /// </summary>
        const int CASE_TWO_PEOPLE_SIGN = 0;
        /// <summary>
        ///  场景：企业和个人签约
        /// </summary>
        const int CASE_COMPANY_AND_PERSON_SIGN = 1;
        /// <summary>
        /// 场景：企业和企业签约
        /// </summary>
        const int CASE_TWO_COMPANY_SIGN = 2;
        /// <summary>
        ///  场景：多签约人签约
        /// </summary>
        const int CASE_MULTIPLE_PEOPLE_SIGN = 3;
        /// <summary>
        ///  场景：多方签约，每个签约方支持多个签约人
        /// </summary>
        const int CASE_MULTIPLE_PARTIES_SIGN = 4;

        /// <summary>
        /// 快捷签约
        /// </summary>
        /// <param name="caseType">
        /// see above CASE definition
        /// </param>
        static private void QuickSignContract(int signType)
        {
            QuickSignatorySample quickSignatorySample = new QuickSignatorySample();

            switch (signType)
            {
                case CASE_TWO_PEOPLE_SIGN:
                    quickSignatorySample.TwoPeopleSign(GetOrCreateClient());
                    break;
                case CASE_COMPANY_AND_PERSON_SIGN:
                    quickSignatorySample.CompanyAndPersonSign(GetOrCreateClient());
                    break;
                case CASE_TWO_COMPANY_SIGN:
                    quickSignatorySample.TwoCompanySign(GetOrCreateClient());
                    break;
                case CASE_MULTIPLE_PEOPLE_SIGN:
                    quickSignatorySample.MultiplePeopleSign(GetOrCreateClient());
                    break;
                case CASE_MULTIPLE_PARTIES_SIGN:
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
        /// see above CASE definition
        /// </param>
        static private void SignContract(int signType)
        {
            SignatorySample sgnatorySample = new SignatorySample();
            string signUrl = null;
            switch (signType)
            {
                case CASE_TWO_PEOPLE_SIGN:
                    signUrl = sgnatorySample.TwoPeopleSign(GetOrCreateClient());
                    break;
                case CASE_COMPANY_AND_PERSON_SIGN:
                    signUrl = sgnatorySample.CompanyAndPersonSign(GetOrCreateClient());
                    break;
                case CASE_TWO_COMPANY_SIGN:
                    signUrl = sgnatorySample.TwoCompanySign(GetOrCreateClient());
                    break;
                case CASE_MULTIPLE_PEOPLE_SIGN:
                    signUrl = sgnatorySample.MultiplePeopleSign(GetOrCreateClient());
                    break;
                case CASE_MULTIPLE_PARTIES_SIGN:
                    signUrl = sgnatorySample.MultiplePartiesSign(GetOrCreateClient());
                    break;
                default:
                    break;
            }

            if(String.IsNullOrEmpty(signUrl))
            {
                Console.WriteLine("Return URL is NULL, can't continue the signing processing");
            }
            else
            {
                // use the explorer to open the sign url
                System.Diagnostics.Process.Start(signUrl);
            }
            Console.WriteLine("Call SignContract api finished");

        }

        static void Main(string[] args)
        {
            //Ping();
            //Fetch();
            //QuickSignContract(CASE_TWO_PEOPLE_SIGN);
            //QuickSignContract(CASE_COMPANY_AND_PERSON_SIGN);
            //QuickSignContract(CASE_TWO_COMPANY_SIGN);
            //QuickSignContract(CASE_MULTIPLE_PEOPLE_SIGN);
            //QuickSignContract(CASE_MULTIPLE_PARTIES_SIGN);

            SignContract(CASE_TWO_PEOPLE_SIGN);
            //SignContract(CASE_COMPANY_AND_PERSON_SIGN);
            //SignContract(CASE_TWO_COMPANY_SIGN);
            //SignContract(CASE_MULTIPLE_PEOPLE_SIGN);
            //SignContract(CASE_MULTIPLE_PARTIES_SIGN);
        }
        
    }
}
