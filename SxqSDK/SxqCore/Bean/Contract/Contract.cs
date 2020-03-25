namespace SxqCore.Bean.Contract
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 签约合同对象
    /// </summary>
    public class Contract
    {
        /// <summary>
        // pdf文件的base64编码
        /// </summary>
        protected string pdfFileBase64;
        /// <summary>
        // 所有签章签约人真实姓名打码
        /// </summary>
        protected bool realNameMask = false;
        /// <summary>
        // 所有签章签约人证件号码打码
        /// </summary>
        protected bool certNoMask = false;
        /// <summary>
        // 签约人集合
        /// </summary>
        protected List<Signatory> signatoryList;
        /// <summary>
        // 签约的基础数据对象
        /// </summary>
        protected DataStore dataStore;
        /// <summary>
        /// 是否自动签章，对该合同下的所有签署人、签署方生效
        /// </summary>
        private string signatoryAuto = null;
        /// <summary>
        /// 是否允许未登录前对合同进行预览
        /// </summary>
        private int allowPreview = SxqConst.PREVIEW_ON;
        /// <summary>
        /// 是否允许新注册账户设置登录密码
        /// </summary>
        private int allowPwdSetting = SxqConst.PWD_SETTING_ON;

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

        public string SignatoryAuto
        {
            get
            {
                return this.signatoryAuto;
            }
            set
            {
                this.signatoryAuto = value;
            }
        }

        public int AllowPreview
        {
            get
            {
                return this.allowPreview;
            }
            set
            {
                this.allowPreview = value;
            }
        }


        public int AllowPwdSetting
        {
            get
            {
                return this.allowPwdSetting;
            }
            set
            {
                this.allowPwdSetting = value;
            }
        }
    }
}
