namespace SxqCore.Bean.Request
{
    using SxqClient.Http;
    using SxqClient.Tools;
    using SxqCore.Bean.Contract;
    using SxqSDK.SxqCore.Tools;

    public class QuickSignRequest : IHttpRequest
    {
        private Contract quickContract;

        public QuickSignRequest(Contract quickContract)
        {
            this.quickContract = quickContract;
        }

        public HttpParamers GetHttpParamers()
        {
            HttpParamers paramers = ParameterWrapper.WrapContract(this.quickContract);
            return paramers;
        }

        public string GetRequestPath() =>
            RequestPathConstant.QUICK_SIGNATORY;

    }
}