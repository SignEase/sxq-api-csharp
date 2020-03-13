namespace SxqCore.Bean.Request
{
    using SxqClient.Http;
    using SxqClient.Tools;
    using SxqCore.Bean;

    public class PingRequest : IHttpRequest
    {
        public PingRequest()
        {
        }

        public HttpParamers GetHttpParamers()
        {
            return HttpParamers.PostParamers();
        }

        public string GetRequestPath() =>
            RequestPathConstant.PING;

    }
}



