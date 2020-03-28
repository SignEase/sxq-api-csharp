namespace SxqCore.Bean.Request
{
    using SxqClient.Http;
    using SxqClient.Tools;
    using SxqCore.Bean;

    public class FetchSignUrlRequest : IHttpRequest
    {
        private string contractId;

        public FetchSignUrlRequest(string contractId)
        {
            this.contractId = contractId;
        }

        public HttpParamers GetHttpParamers()
        {
            HttpParamers paramers = HttpParamers.PostParamers();
            paramers.AddParamer("contractId", this.contractId);
            return HttpParamers.PostParamers();
        }

        public string GetRequestPath() =>
            RequestPathConstant.FETCH_SIGN_URL;

    }
}



