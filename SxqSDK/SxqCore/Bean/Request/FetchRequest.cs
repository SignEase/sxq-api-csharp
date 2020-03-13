namespace SxqCore.Bean.Request
{
    using SxqClient.Http;
    using SxqClient.Tools;
    using SxqCore.Bean;

    public class FetchRequest : IHttpRequest
    {
        private string storeNo;

        public FetchRequest() {}

        public FetchRequest(string storeNo)
        {
            this.storeNo = storeNo;
        }

        public HttpParamers GetHttpParamers()
        {
            HttpParamers paramers = HttpParamers.GetParamers();
            paramers.AddParamer("storeNo", this.storeNo);
            return paramers;
        }

        public string GetRequestPath() =>
            RequestPathConstant.FETCH;

    }
}



