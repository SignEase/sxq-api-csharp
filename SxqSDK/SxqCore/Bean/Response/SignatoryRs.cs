namespace SxqCore.Bean.Contract
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// 返回的签约人结果对象
	/// </summary>
	public class SignatoryRs
	{
	
		// 签约人ID
		private long signatoryId;
		// 证件号码
		private string realName;
		// 是否已实名认证
		private bool sxqVerified = false;
		// 签约时间
		private string signatoryTime;
		// 签约分组字符编号
		private string groupChar;
		// 签约分组名称
		private string groupName;
		// 签约人邮箱
		private string status;
		// 签约人电话
		private string phone;
        // 签约人邮箱
		private string email;
		// 真实姓名
		private string certNo;
		// 证件类型
		private string certType;
        // 签约人类型
        private string signatoryUserType;
		// 签约相关变量
		private string signatureVariable;


        public SignatoryRs() { }

        public bool IsPersonal()
        {
			return SxqConst.USER_PERSONAL.Equals(this.signatoryUserType);
        }


		public long SignatoryId
		{
			get
			{
				return this.signatoryId;
			}
			set
			{
				this.signatoryId = value;
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

		public bool SxqVerified
		{
			get
			{
				return this.sxqVerified;
			}
			set
			{
				this.sxqVerified = value;
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

		public string Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
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

		public string SignatureVariable
		{
			get
			{
				return this.signatureVariable;
			}
			set
			{
				this.signatureVariable = value;
			}
		}


	}
}
