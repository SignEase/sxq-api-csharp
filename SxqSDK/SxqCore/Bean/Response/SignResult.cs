namespace SxqCore.Bean.Response
{
    using System;

    public class SignResult
    {
        private string signUrl;
        private long contractId;

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

