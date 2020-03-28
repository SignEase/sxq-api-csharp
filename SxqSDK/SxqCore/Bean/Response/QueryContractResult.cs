namespace SxqCore.Bean.Response
{
    using System;
    using System.Collections.Generic;
    using SxqCore.Bean.Contract;
    using SxqCore.Tools;

    public class QueryContractResult
    {
        // 签约合同ID
        private long contractId;
        // 签约名称
        private string contractName;
        // 签约链接
        private string signUrl;
        // 签约合同的状态
        // 参见SxqCore.Bean.Contract.SxqConst中定义的合同状态
        private string contractStatus;
        // 存证过程链接
        private string notaryUrl;
        // 快照访问链接
        private string snapshotUrl;
        // 创建时间
        private long rowAddTimeMS;
        // 格式化后的创建时间
        private string createTime;
        //// 签约人对象
        private List<SignatoryRs> signatories;


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

        public string ContractName
        {
            get
            {
                return this.contractName;
            }
            set
            {
                this.contractName = value;
            }
        }

        public string ContractStatus
        {
            get
            {
                return this.contractStatus;
            }
            set
            {
                this.contractStatus = value;
            }
        }

        public string NotaryUrl
        {
            get
            {
                return this.notaryUrl;
            }
            set
            {
                this.notaryUrl = value;
            }
        }

        public string SnapshotUrl
        {
            get
            {
                return this.snapshotUrl;
            }
            set
            {
                this.snapshotUrl = value;
            }
        }

        public long RowAddTimeMS
        {
            get
            {
                return this.rowAddTimeMS;
            }
            set
            {
                try
                {
                    this.rowAddTimeMS = value;
                    this.createTime = SignUtil.ParseTimeMS(this.rowAddTimeMS);
                }
                catch
                {
                    // do nothing
                }

            }
        }

        public string CreateTime
        {
            get
            {
                return this.createTime;
            }
        }

        public List<SignatoryRs> Signatories
        {
            get
            {
                return this.signatories;
            }
            set
            {
                this.signatories = value;
            }
        }

    }
}

