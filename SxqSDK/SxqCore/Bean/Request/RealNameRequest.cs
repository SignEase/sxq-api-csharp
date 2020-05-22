namespace SxqCore.Bean.Request
{
    using SxqClient.Http;
    using SxqClient.Tools;
    using SxqCore.Bean.Contract;
    using SxqSDK.SxqCore.Tools;

    public class RealNameRequest : IHttpRequest
    {
        private RealNameAuth realNameAuth;

        public RealNameRequest(RealNameAuth realNameAuth)
        {
            this.realNameAuth = realNameAuth;
        }

        public HttpParamers GetHttpParamers()
        {
            return ParameterWrapper.WrapRealNameAuth(this.realNameAuth);
        }

        public string GetRequestPath() =>
            RequestPathConstant.REALNAME_AUTH;

    }
}



