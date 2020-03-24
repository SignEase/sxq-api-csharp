namespace SxqCore.Bean.Request
{
    using SxqClient.Http;
    using SxqClient.Tools;
    using SxqCore.Bean.Contract;
    using SxqSDK.SxqCore.Tools;

    public class DraftContractRequest : IHttpRequest
    {
        private Contract contract;

        public DraftContractRequest(Contract contract)
        {
            this.contract = contract;
        }

        public HttpParamers GetHttpParamers()
        {
            return ParameterWrapper.WrapContract(this.contract);
        }

        public string GetRequestPath() =>
            RequestPathConstant.DRAFT_CONTRACT;

    }
}



