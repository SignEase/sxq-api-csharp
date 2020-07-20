using SxqClient.Http;
using System;
using SxqCore.Bean.Contract;
using SxqCore.Bean.Response;

namespace SxqApiSample
{
    class Program
    {

        /** -开始联调前，请设置以下参数- **/
        private static int env = ENV_MOCK;
        public static string accessSecret = "";
        public static string accessToken = "";
        // 如果不需要回调，请将以下的callBackUrl设置为null；
        // 如果设置了回调，请参考CallBackServer完成回调处理实现；
        public static string callBackUrl = "http://ip:port/xxx/";
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

            return client = new SDKClient(accessToken, accessSecret, serverUrl, callBackUrl);
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
        /// 下载已签约/存证的合同
        /// </summary>
        /// <param name="contractId">
        /// 创建的签约合同编号
        /// </param>
        static private void Download(long contractId)
        {
            // 存储到本地的文件路径
            string localFilePath = "../../Contract/Signed/ContractFetched.pdf";
            BaseSample baseSample = new BaseSample();
            baseSample.Download(GetOrCreateClient(), contractId.ToString(), localFilePath);
            Console.WriteLine("Download file success, you can find the file at {0}" , localFilePath);
            Console.WriteLine("Call Download api finished");
        }

        /// <summary>
        /// 场景：个人用户实名
        /// </summary>
        const int CASE_AUTH_PERSON = 0;
        /// <summary>
        /// 场景：企业用户实名
        /// </summary>
        const int CASE_AUTH_ENTERPRISE = 1;

        /// <summary>
        /// 个人|企业 实名认证
        /// </summary>
        /// <param name="authCaseType">
        /// see above CASE definition
        /// </param>
        static private void RealName(int authCaseType)
        {
            RealNameAuth realNameAuth = null;
            switch (authCaseType)
            {
                case CASE_AUTH_PERSON:
                    realNameAuth = RealNameAuth.PersonRealNameAuth("试试", "500235199412169110", "18700001111");
                    break;
                case CASE_AUTH_ENTERPRISE:
                    realNameAuth = RealNameAuth.EnterpriseRealNameAuth("慢慢", "500235199412169110", "18711112222", "91500000MA5UCYU7ZY", "慢慢科技", SxqConst.ID_BUSINESS_LICENCE);
                    break;
                default:
                    break;
            }
            BaseSample baseSample = new BaseSample();
            String result = baseSample.RealName(GetOrCreateClient(), realNameAuth);
            Console.WriteLine("Call RealNameAuth api finished: " + result.ToString());
        }


        /// <summary>
        /// 企业实名认证修改
        /// </summary>
        /// <param name="authCaseType">
        /// see above CASE definition
        /// </param>
        static private void EnterpriseReAuth()
        {
            RealNameAuth realNameAuth = realNameAuth = RealNameAuth.EnterpriseRealNameAuth("慢慢", "500235199412169110", "18711112222", "91500000MA5UCYU7ZY", "慢慢科技", SxqConst.ID_BUSINESS_LICENCE);
            BaseSample baseSample = new BaseSample();
            String result = baseSample.EnterpriseReCertification(GetOrCreateClient(), realNameAuth);
            Console.WriteLine("Call enterpriseReCertification api finished: " + result.ToString());
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
        ///  场景：甲乙两人签约，并设置签约的有效期
        /// </summary>
        const int CASE_TWO_PEOPLE_SIGN_AND_SET_EXPIRE_TIME = 5;

        /// <summary>
        /// 快捷签约
        /// </summary>
        /// <param name="signCaseType">
        /// see above CASE definition
        /// </param>
        static private void QuickSignContract(int signCaseType)
        {
            QuickSignatorySample quickSignatorySample = new QuickSignatorySample();

            switch (signCaseType)
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
            SignatorySample signatorySample = new SignatorySample();
            string signUrl = null;
            switch (signType)
            {
                case CASE_TWO_PEOPLE_SIGN:
                    signUrl = signatorySample.TwoPeopleSign(GetOrCreateClient());
                    break;
                case CASE_COMPANY_AND_PERSON_SIGN:
                    signUrl = signatorySample.CompanyAndPersonSign(GetOrCreateClient());
                    break;
                case CASE_TWO_COMPANY_SIGN:
                    signUrl = signatorySample.TwoCompanySign(GetOrCreateClient());
                    break;
                case CASE_MULTIPLE_PEOPLE_SIGN:
                    signUrl = signatorySample.MultiplePeopleSign(GetOrCreateClient());
                    break;
                case CASE_MULTIPLE_PARTIES_SIGN:
                    signUrl = signatorySample.MultiplePartiesSign(GetOrCreateClient());
                    break;
                case CASE_TWO_PEOPLE_SIGN_AND_SET_EXPIRE_TIME:
                    signUrl = signatorySample.TwoPeopleSignAndSetExpireTime(GetOrCreateClient());
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


        /// <summary>
        /// 取回已签约/存证的合同
        /// </summary>
        /// <param name="contractId">
        ///签约合同编号
        /// </param>
        static private void QueryContract(long contractId)
        {
            BaseSample baseSample = new BaseSample();
            SdkResponse<QueryContractResult> rs = baseSample.QueryContract(GetOrCreateClient(), contractId.ToString());
            Console.WriteLine("Query contract result is: {0}", rs.ToString());
            Console.WriteLine("Call QueryContract api finished");
        }


        /// <summary>
        /// 获取继续签约的链接（用于某些特殊场景下，丢失了后续签约的链接）
        /// </summary>
        /// <param name="contractId">
        /// 签约合同编号
        /// </param>
        static private void FetchSignUrl(long contractId)
        {
            BaseSample baseSample = new BaseSample();
            string signUrl = baseSample.FetchSignUrl(GetOrCreateClient(), contractId.ToString());
            if (String.IsNullOrEmpty(signUrl))
            {
                Console.WriteLine("Return URL is NULL, can't continue the signing processing");
            }
            else
            {
                // use the explorer to open the sign url
                System.Diagnostics.Process.Start(signUrl);
            }

            Console.WriteLine("Call FetchSignUrl api finished");
        }

        /// <summary>
        /// 启动本地的回调监听服务
        /// </summary>
        private static void CallBackListener()
        {
            CallBackServer.Inst(callBackUrl);
        }

        static void Main(string[] args)
        {
            //Ping();
            //Download(1046571);
            RealName(CASE_AUTH_PERSON);

            //QuickSignContract(CASE_TWO_PEOPLE_SIGN);
            //QuickSignContract(CASE_COMPANY_AND_PERSON_SIGN);
            //QuickSignContract(CASE_TWO_COMPANY_SIGN);
            //QuickSignContract(CASE_MULTIPLE_PEOPLE_SIGN);
            //QuickSignContract(CASE_MULTIPLE_PARTIES_SIGN);

            //SignContract(CASE_TWO_PEOPLE_SIGN);
            //SignContract(CASE_COMPANY_AND_PERSON_SIGN);
            //SignContract(CASE_TWO_COMPANY_SIGN);
            //SignContract(CASE_MULTIPLE_PEOPLE_SIGN);
            //SignContract(CASE_MULTIPLE_PARTIES_SIGN);
            //SignContract(CASE_TWO_PEOPLE_SIGN_AND_SET_EXPIRE_TIME);

            //FetchSignUrl(1046573);
            //QueryContract(1046573);

            //CallBackListener();
        }

    }
}
