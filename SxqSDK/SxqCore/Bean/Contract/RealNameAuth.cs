namespace SxqCore.Bean.Contract
{
    using System;
    using SxqCore.Tools;

    /// <summary>
    /// 快速签约的基础数据对象
    /// </summary>
    public class RealNameAuth
    {
        // 实名类型-个人、机构
        public const string AUTH_TYPE_PERSONAL = "GR";
        public const string AUTH_TYPE_ENTERPRISE = "JG";

        // 个人: 真实姓名, 企业: 法人姓名
        private string realName;
        // 证件号码
        private string certNo;
        // 企业的名称
        private string enterpriseRealName;
        // 企业的社会信用统一号
        private string enterpriseCertNo;
        //  企业证件类型:
        //  组织机构代码证: SxqConst.ID_INSTITUTION_CODE
        //  营业执照:      SxqConst.ID_BUSINESS_LICENCE
        private string enterpriseCertType;
        // 绑定电话
        private string mobile;
        // 绑定邮箱
        private string mail;
        // 用户类型 
        private string type = AUTH_TYPE_PERSONAL;
        

        public RealNameAuth(){}

        public static RealNameAuth PersonRealNameAuth(string realName, string certNo, string mobile) {
            RealNameAuth userRealNameAuth = new RealNameAuth();
            userRealNameAuth.RealName = realName;
            userRealNameAuth.CertNo = certNo;
            userRealNameAuth.Mobile = mobile;
            return userRealNameAuth;
        }

        public static RealNameAuth EnterpriseRealNameAuth(string realName, string certNo, string mobile, string enterpriseCertNo, string enterpriseRealName, string enterpriseCertType) {
            RealNameAuth userRealNameAuth = new RealNameAuth();
            userRealNameAuth.RealName = realName;
            userRealNameAuth.CertNo = certNo;
            userRealNameAuth.Mobile = mobile;
            userRealNameAuth.EnterpriseCertNo = enterpriseCertNo;
            userRealNameAuth.EnterpriseRealName = enterpriseRealName;
            userRealNameAuth.enterpriseCertType = enterpriseCertType;
            return userRealNameAuth;
        }

        public void Check()
        {
            if(string.IsNullOrEmpty(this.mobile) && string.IsNullOrEmpty(this.mail))
            {
                throw new Exception("手机号和邮箱必须填写一个");
            }

            bool isIdcard = IdCardUtil.IsIdCard(this.certNo);
            if(!isIdcard)
            {
                throw new Exception("身份证号格式错误");
            }

            if(!RealNameAuth.AUTH_TYPE_PERSONAL.Equals(this.type)
                && !RealNameAuth.AUTH_TYPE_ENTERPRISE.Equals(this.type))
            {
                throw new Exception("未知的实名认证类型");
            }

            if(RealNameAuth.AUTH_TYPE_PERSONAL.Equals(this.type)
                && string.IsNullOrEmpty(this.realName))
            {
                throw new Exception("姓名不能为空");
            }

            if (RealNameAuth.AUTH_TYPE_ENTERPRISE.Equals(this.type))
            {
                bool notRealName = string.IsNullOrEmpty(this.realName);
                bool notEntRealName = string.IsNullOrEmpty(this.enterpriseRealName);
                bool notEntCertNo = string.IsNullOrEmpty(this.enterpriseCertNo);
                bool notEntCertType = string.IsNullOrEmpty(this.enterpriseCertType);

                if (notRealName || notEntRealName || notEntCertNo || notEntCertType)
                {
                    throw new Exception("企业认证信息不完整，必需包括：法人姓名，企业全称，企业证件号，企业证件类型");
                }
            }

        }

        public string RealName
        {
           
            get
            {
                return this.realName;
            }
            set
            {
                this.realName = value;
            }
        }

        public string CertNo
        {

            get
            {
                return this.certNo;
            }
            set
            {
                this.certNo = value;
            }
        }

        public string EnterpriseRealName
        {

            get
            {
                return this.enterpriseRealName;
            }
            set
            {
                this.enterpriseRealName = value;
            }
        }


        public string EnterpriseCertNo
        {

            get
            {
                return this.enterpriseCertNo;
            }
            set
            {
                this.enterpriseCertNo = value;
            }
        }

        public string EnterpriseCertType
        {

            get
            {
                return this.enterpriseCertType;
            }
            set
            {
                this.enterpriseCertType = value;
                // set the auth type to enterprise 
                this.type = AUTH_TYPE_ENTERPRISE;
            }
  
        }

        public string Mobile
        {

            get
            {
                return this.mobile;
            }
            set
            {
                this.mobile = value;
            }
        }

        public string Mail
        {

            get
            {
                return this.mail;
            }
            set
            {
                this.mail = value;
            }
        }

        public string Type
        {

            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

    }
}
