using System;
namespace SxqCore.Bean.Contract
{
    public class SxqConst
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
		/// <summary>
		/// 手写签章
		/// </summary>
		public const string SEAL_HANDWRITING = "Handwriting";

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

		/** 阶段 **/
		/// <summary>
		///未签约
		/// </summary>
		public const string PHASE_ORIGINAL = "ORIGINAL";        
		/// <summary>
		///失效
		/// </summary>
		public const string PHASE_INVALID = "INVALID";          
		/// <summary>
		///已签约
		/// </summary>
		public const string PHASE_SIGNED = "SIGNED";           
		/// <summary>
		///多阶段签-等待履约
		/// </summary>
		public const string MUL_PHASE_WAIT = "WAIT";             
		/// <summary>
		///多阶段签-正在履行
		/// </summary>
		public const string MUL_PHASE_INPROGRESS = "INPROGRESS";

		/** 预览开关 **/
		/// <summary>
		///打开预览
		/// </summary>
		public const int PREVIEW_ON = 1;
		/// <summary>
		///关闭预览
		/// </summary>
		public const int PREVIEW_OFF = 0;

		/** 密码设置开关 **/
		/// <summary>
		///允许密码设置
		/// </summary>
		public const int PWD_SETTING_ON = 1;
		/// <summary>
		///关闭密码设置
		/// </summary>
		public const int PWD_SETTING_OFF = 0;


		public SxqConst() { }

    }
}
