namespace SxqCore.Bean.Request
{
    using SxqClient.Http;
    using SxqClient.Tools;
    using SxqCore.Bean;

    public class QueryContractRequest : IHttpRequest
    {
        private string contractId;

        public QueryContractRequest() {}

        public QueryContractRequest(string contractId)
        {
            this.contractId = contractId;
        }

        public HttpParamers GetHttpParamers()
        {
            HttpParamers paramers = HttpParamers.PostParamers();
            paramers.AddParamer("contractId", this.contractId);
            return paramers;
        }

        public string GetRequestPath() =>
            RequestPathConstant.QUERY_CONTRACT;

    }
}


