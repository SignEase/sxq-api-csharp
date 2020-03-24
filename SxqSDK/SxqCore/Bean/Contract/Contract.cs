namespace SxqCore.Bean.Contract
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 签约合同对象
    /// </summary>
    public class Contract
    {
        // pdf文件的base64编码
		protected string pdfFileBase64;
        // 所有签章签约人真实姓名打码
        protected bool realNameMask = false;
        // 所有签章签约人证件号码打码
        protected bool certNoMask = false;
        // 签约人集合
        protected List<Signatory> signatoryList;
        // 签约的基础数据对象
        protected DataStore dataStore;

        public Contract()
        {
        }

        public string PdfFileBase64
        {
            get
            {
                return this.pdfFileBase64;
            }
            set
            {
                this.pdfFileBase64 = value;
            }
        }

        public bool RealNameMask
        {
            get
            {
                return this.realNameMask;
            }
            set
            {
                this.realNameMask = value;
            }
        }


        public bool CertNoMask
        {
            get
            {
                return this.certNoMask;
            }
            set
            {
                this.certNoMask = value;
            }
        }

        public List<Signatory> SignatoryList
        {
            get
            {
                return this.signatoryList;
            }
            set
            {
                this.signatoryList = value;
            }
        }

        public DataStore DataStore
        {
            get
            {
                return this.dataStore;
            }
            set
            {
                this.dataStore = value;
            }
        }
    }
}
