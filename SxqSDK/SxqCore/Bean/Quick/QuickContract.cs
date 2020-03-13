namespace SxqCore.Bean.Quick
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 快速签约的签约合同对象
    /// </summary>
    public class QuickContract
    {
        // pdf文件的base64编码
		private string pdfFileBase64;
		// 所有签章签约人真实姓名打码
		private bool realNameMask = false;
        // 所有签章签约人证件号码打码
		private bool certNoMask = false;
        // 签约人集合
        private List<QuickSignatory> signatoryList;
        // 签约的基础数据对象
        private QuickDataStore dataStore;

        public QuickContract()
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

        public List<QuickSignatory> SignatoryList
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

        public QuickDataStore DataStore
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
