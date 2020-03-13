namespace SxqCore.Bean.Quick
{
	using SxqCore.Tools;

    /// <summary>
    /// 快速签约的基础数据对象
    /// </summary>
	public class QuickDataStore
    {
		public const string ACCESS_PRIVATE = "PRIVATE";
		public const string ACCESS_PUBLIC = "PUBLIC";
		/** 必备字段 **/
		// 用户业务ID
		private string userBizNumber;
		// 合同名称
		private string storeName;
		// 存证说明
		private string transAbs;
		// 是否公开（PRIVATE，PUBLIC）
		private string isPublic;

		// 使用的合同模板ID （非必填）
		private long contractTemplateId;
		/** ***** **/

		/** 扩展字段 
		private long storeId;
        // 数据拥有者ID
		private long ownerId;
        // 签约人列表，逗号分隔
		private string signatoryNames;
        // 存储来源，本地上传，API，合同
		private string source;
        // 文件类型
		private string fileType;
        // 合同类型
		private string contractStatus;
        // 数据隶属通道
		private string channel;
        // 安全存储数据
		private string storeData;
        // 标签
		private string tags;
        // 商户名称
		private string merchantName;
        // 数据拥有者名字
		private string ownerName;
		// key 
		private string appKey;
		// secret
		private string appSecret;
        //  原始文件名
		private string fileName;
        // 存储文件路径
		private string filePath;
        // 文件url地址
		private string fileUrl;
        // pdf快照图片路径
		private string snapshotPath;
		// pdf快照图片URL
		private string snapshotUrl;
        // hash值（区块链返回）
		private string hashCode;
		private string isBlocked;
        // 数据签名
		private string sign;
        **/

		public QuickDataStore(string storeName, string transAbs, string isPublic)
		{
			this.userBizNumber = QuickSignUtil.GainNo();
			this.storeName = storeName;
			this.transAbs = transAbs;
			this.isPublic = isPublic;
		}

		public string UserBizNumber
		{
			get
			{
				return this.userBizNumber;
			}
			set
			{
				this.userBizNumber = value;
			}
		}

		public string StoreName
		{
			get
			{
				return this.storeName;
			}
			set
			{
				this.storeName = value;
			}
		}

		public string TransAbs
		{
			get
			{
				return this.transAbs;
			}
			set
			{
				this.transAbs = value;
			}
		}

        public string IsPublic
		{
			get
			{
				return this.isPublic;
			}
			set
			{
				this.isPublic = value;
			}
		}
	}
}
