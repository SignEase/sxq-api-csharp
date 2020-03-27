namespace SxqCore.Bean.Response
{
    using System;

    public class QueryContractResult
    {
        //TODO members parsing follow the api resturn values
        private string signUrl;
        private long contractId;
        private string storeName;

        public string SignUrl
        {
            get
            {
                return this.signUrl;
            }
            set
            {
                this.signUrl = value;
            }
        }

        public long ContractId
        {
            get
            {
                return this.contractId;
            }
            set
            {
                this.contractId = value;
            }
        }

    }
}

