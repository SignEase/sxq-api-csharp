namespace SxqCore.Bean.Request
{
    using SxqClient.Http;
    using SxqClient.Tools;
    using SxqCore.Bean;

    public class DownloadContractRequest : IHttpRequest
    {
        private string contractId;

        public DownloadContractRequest() {}

        public DownloadContractRequest(string contractId)
        {
            this.contractId = contractId;
        }

        public HttpParamers GetHttpParamers()
        {
            HttpParamers paramers = HttpParamers.GetParamers();
            paramers.AddParamer("contractId", this.contractId);
            return paramers;
        }

        public string GetRequestPath() =>
            RequestPathConstant.DOWNLOAD_CONTRACT;

    }
}



