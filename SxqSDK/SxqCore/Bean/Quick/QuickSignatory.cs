namespace SxqCore.Bean.Quick
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// 快速签约的签约人对象
	/// </summary>
	public class QuickSignatory
	{
		/** 签章类型 **/
		/// <summary>
		/// 私章
		/// </summary>
		public const string SEAL_PERSONAL = "PERSONAL";
		/// <summary>
		/// 公章
		/// </summary>
		public const string SEAL_ENTERPRISE = "OFFICIAL";

		/** 用户类型 **/
		/// <summary>
		/// 个人用户
		/// </summary>
		public const string USER_PERSONAL = "PERSONAL";
		/// <summary>
		/// 企业用户
		/// </summary>
		public const string USER_ENTERPRISE = "ENTERPRISE";

		/** 证件类型 **/
		/// <summary>
		/// 身份证
		/// </summary>
		public const string ID_PERSONAL_CARD = "ID";
		/// <summary>
		/// 组织机构代码证
		/// </summary>
		public const string ID_INSTITUTION_CODE = "INSTITUTION_CODE";
		/// <summary>
		/// 营业执照
		/// </summary>
		public const string ID_BUSINESS_LICENCE = "BUSINESS_LICENCE";

		/** 布尔值 **/
		public const string BOOL_YES = "YES"; //是
		public const string BOOL_IS = "IS"; //是
		public const string BOOL_NO = "NO"; //否

		/** 签约组 **/
		public const string GROUP_A = "甲方";
		public const string GROUP_B = "乙方";
		public const string GROUP_C = "丙方";
		public const string GROUP_D = "丁方";
		public const string GROUP_E = "戊方";
		public const string GROUP_F = "己方";
		public const string GROUP_G = "庚方";
		public const string GROUP_H = "辛方";
		public const string GROUP_I = "壬方";
		public const string GROUP_J = "癸方";
		public const string GROUP_K = "子方";
		public static Dictionary<string, string> GROUP_DEF = InitGroupDef();

		/** 必备字段 **/
		// 签约人真实姓名
		private string realName;
		// 图章类型(公章: official, 私章: personal)
		private string sealType;
		// 是否自动签章
		private string signatoryAuto;
		// 签约人类型(personal , enterprise)
		private string signatoryUserType;
		// 签约时间
		private string signatoryTime;
		// 签约分组字符编号
		private string groupChar;
		// 签约分组名称
		private string groupName;
		// 签约人邮箱
		private string email;
		// 签约人电话
		private string phone;
		// 证件号码
		private string certNo;
		// 证件类型
		private string certType;
		//  签章x坐标
		private double signatureX = -1d;
		// 签章y坐标
		private double signatureY = -1d;
		// 签章页
		private int signaturePage = -1;
		// 定位定位关键词
		private string keywords;
		// 印章用途
		private string sealPurpose;
		// 签章真实姓名打码
		private bool realNameMask = false;
		// 签章证件号码打码
		private bool certNoMask = false;
		// 章编号（防伪码）
		private string sealSn;
		/** ***** **/

        /** 扩展字段 
		private long signatoryId;
		// 待签约文件存储ID
		private long storeId;
		// 签约人ID
		private long signatoryUserId;
		//  未签，已签
		private string status;
		// 是否系统用户
		private string sysUser;
		// 分组顺序
		private int groupOrder;
		//  签章顺序
		private int signatoryOrder;
		// 签约人备注
		private string remark;
        **/
	
		private static Dictionary<string, string> InitGroupDef()
		{
			Dictionary<string, string> groupDef = new Dictionary<string, string>();
			groupDef.Add("甲方", "a");
			groupDef.Add("乙方", "b");
			groupDef.Add("丙方", "c");
			groupDef.Add("丁方", "d");
			groupDef.Add("戊方", "e");
			groupDef.Add("己方", "f");
			groupDef.Add("庚方", "g");
			groupDef.Add("辛方", "h");
			groupDef.Add("壬方", "i");
			groupDef.Add("癸方", "j");
			groupDef.Add("子方", "k");
			return groupDef;
		}

        public QuickSignatory() { }

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

		public string SealType
		{
			get
			{
				return this.sealType;
			}
			set
			{
				this.sealType = value;
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
		public string SignatoryUserType
		{
			get
			{
				return this.signatoryUserType;
			}
			set
			{
				this.signatoryUserType = value;
			}
		}
        public string SignatoryTime
		{
			get
			{
				return this.signatoryTime;
			}
			set
			{
				this.signatoryTime = value;
			}
		}
		public string GroupChar
		{
			get
			{
				return this.groupChar;
			}
			set
			{
				this.groupChar = value;
			}
		}
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
			set
			{
				this.groupName = value;
			}
		}
		public void Group(string groupName)
		{
            if(GROUP_DEF.ContainsKey(groupName))
            {
				this.groupName = groupName;
				this.groupChar = GROUP_DEF[groupName];
			}
		}
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}
		public string Phone
		{
			get
			{
				return this.phone;
			}
			set
			{
				this.phone = value;
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
		public string CertType
		{
			get
			{
				return this.certType;
			}
			set
			{
				this.certType = value;
			}
		}
		public double SignatureX
		{
			get
			{
				return this.signatureX;
			}
			set
			{
				this.signatureX = value;
			}
		}
		public double SignatureY
		{
			get
			{
				return this.signatureY;
			}
			set
			{
				this.signatureY = value;
			}
		}
		public int SignaturePage
		{
			get
			{
				return this.signaturePage;
			}
			set
			{
				this.signaturePage = value;
			}
		}
		public string Keywords
		{
			get
			{
				return this.keywords;
			}
			set
			{
				this.keywords = value;
			}
		}
		public string SealPurpose
		{
			get
			{
				return this.sealPurpose;
			}
			set
			{
				this.sealPurpose = value;
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
		public string SealSn
		{
			get
			{
				return this.sealSn;
			}
			set
			{
				this.sealSn = value;
			}
		}


	}
}
