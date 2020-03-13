using SxqClient.Http;
using SxqCore.Bean.Request;
using SxqCore.Bean.Response;
using SxqCore.Tools;
using System;
using SxqCore.Bean;
using SxqCore.Bean.Quick;
using System.Collections.Generic;

namespace SxqApiSample
{

    /********************************** 注意 ***************************************/
    /** #1 需要注意如果签约人设置了手机号或邮箱的，会作为账户的唯一标识：
 	/** a）如果该标识找到已存在账户的，会直接使用已存在账户下的信息进行签约，如：实名信息。
 	/** b）如果该标识没有查找到已存在账户的，则会用设置的信息创建新的账户信息，并完成签合约。
 	/**
 	/** #2 区块链身份证功能在测试环境无法正常扫码查看，会跳转到线上环境。需要在线上环境才能正常使用。
 	/** 
 	/** #3 签约的合同底板位于Contract/BaseBoard，Fetch方法取回的合同位于Contract/Signed
	/*******************************************************************************/

    /// <summary>
    /// <para> # 快捷签署合同，目前支持：</para>
    /// <para> - 多人签约：多个个体（企业或个人）之间进行签约 </para>
    /// <para> - 多方签约：多个签约方之间进行签约，每个签约方可以包含多名签署人 </para>
    /// </summary>
    class QuickSignatorySample : BaseSample
    {

        private const string CONTRACT_PATH_PREFIX = "../../Contract/BaseBoard/";

        /// <summary>
        /// 甲乙两人签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public void TwoPeopleSign(SDKClient client)
        {
            QuickContract quickContract = new QuickContract();

            /** 全局掩码控制，如果设置为true，则所有的签约人都会遵守该掩码规则 **/
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //quickContract.CertNoMask = true;
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //quickContract.RealNameMask = true;

            /** 需要签署的合同 **/
            string contractPdfPath = CONTRACT_PATH_PREFIX + "TwoPeople.pdf";
            string contractName = "TwoPeopleSign" + new Random().Next(100) + ".pdf@";
            string contractBase64Str = QuickSignUtil.FileToBase64Str(contractPdfPath);
            quickContract.PdfFileBase64 = contractName + contractBase64Str;

            /** 合同基本信息 **/
            // 签署名称
            string storeName = "两人签约";
            // 存证说明
            string transAbs = "两人签约示例";
            // 是否公开可见
            string isPublic = QuickDataStore.ACCESS_PUBLIC;
            QuickDataStore sxqDataStore = new QuickDataStore(storeName, transAbs, isPublic);
            quickContract.DataStore = sxqDataStore;

            /** 签约人1 **/
            QuickSignatory sxqSignatory1 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory1.RealName = "张三";
            //签约方证件号 选填
            sxqSignatory1.CertNo = "430511198702173333";
            //填了证件号就必选填证件类型
            sxqSignatory1.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory1.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory1.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory1.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory1.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // 签约方 必填
            sxqSignatory1.Group(QuickSignatory.GROUP_A);
            // 签约人邮箱 选填
            //sxqSignatory1.Email = "888888@qq.com";
            // 签约人手机 选填
            sxqSignatory1.Phone = "15923641111";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureX = 106d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureY = 544d;
            //签章页 （不填时默认最后一页）
            sxqSignatory1.SignaturePage = 1;


            /** 签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory2.RealName = "李四";
            //签约方证件号 选填
            sxqSignatory2.CertNo = "430511198702173444";
            //填了证件号就必选填证件类型
            sxqSignatory2.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory2.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约  必填
            sxqSignatory2.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory2.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory2.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory2.Group(QuickSignatory.GROUP_B);
            //签约人手机或邮箱 选填
            //		sxqSignatory2.Email = "888888@qq.com";
            sxqSignatory2.Phone = "15923642222";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureX = 377d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureY = 544d;
            //签章页 （不填时默认最后一页）
            sxqSignatory2.SignaturePage = 1;
    
            /** 设置签约方集合 **/
            List<QuickSignatory> sxqSignatorylist = new List<QuickSignatory>();
            sxqSignatorylist.Add(sxqSignatory1);
            sxqSignatorylist.Add(sxqSignatory2);
            quickContract.SignatoryList = sxqSignatorylist;

            /** 快捷签约请求 **/
            Process(client, quickContract);
        }

        /// <summary>
        /// 企业和个人签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public void CompanyAndPersonSign(SDKClient client)
        {
            QuickContract quickContract = new QuickContract();

            /** 全局掩码控制，如果设置为true，则所有的签约人都会遵守该掩码规则 **/
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //quickContract.CertNoMask = true;
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //quickContract.RealNameMask = true;

            /** 需要签署的合同 **/
            string contractPdfPath = CONTRACT_PATH_PREFIX + "CompanyAndPerson.pdf";
            string contractName = "ConpamyAndPersonSign" + new Random().Next(100) + ".pdf@";
            string contractBase64Str = QuickSignUtil.FileToBase64Str(contractPdfPath);
            quickContract.PdfFileBase64 = contractName + contractBase64Str;

            /** 合同基本信息 **/
            // 签署名称
            string storeName = "企业和个人签约";
            // 存证说明
            string transAbs = "企业和个人签约示例";
            // 是否公开可见
            string isPublic = QuickDataStore.ACCESS_PUBLIC;
            QuickDataStore sxqDataStore = new QuickDataStore(storeName, transAbs, isPublic);
            quickContract.DataStore = sxqDataStore;

            /** 签约人1 **/
            QuickSignatory sxqSignatory1 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory1.RealName = "张三";
            //签约方证件号 选填
            sxqSignatory1.CertNo = "430511198702173516";
            //填了证件号就必选填证件类型
            sxqSignatory1.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory1.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory1.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory1.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory1.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // 签约方 必填
            sxqSignatory1.Group(QuickSignatory.GROUP_A);
            // 签约人邮箱 选填
            //sxqSignatory1.Email = "zjq115097475@qq.com";
            // 签约人手机 选填
            sxqSignatory1.Phone = "15923641111";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureX = 106d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureY = 544d;
            //签章页 （不填时默认最后一页）
            sxqSignatory1.SignaturePage = 1;


            /** 签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory2.RealName = "省心签科技";
            // 签章类型 必填
            sxqSignatory2.SealType = QuickSignatory.SEAL_ENTERPRISE;
            // 是否自动签约  必填
            sxqSignatory2.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory2.SignatoryUserType = QuickSignatory.USER_ENTERPRISE;
            // 签约时间 必填
            sxqSignatory2.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory2.Group(QuickSignatory.GROUP_B);
            //签约人手机或邮箱 选填
            //		sxqSignatory2.Email = "888888@qq.com";
            sxqSignatory2.Phone = "15123144444";
            //签约方证件号 选填
            sxqSignatory2.CertNo = "91500000MA5UCYU7DD";
            //填了证件号就必选填证件类型
            sxqSignatory2.CertType = QuickSignatory.ID_INSTITUTION_CODE;
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureX = 377d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureY = 544d;
            //签章页 （不填时默认最后一页）
            sxqSignatory2.SignaturePage = 1;

            /** 设置签约方集合 **/
            List<QuickSignatory> sxqSignatorylist = new List<QuickSignatory>();
            sxqSignatorylist.Add(sxqSignatory1);
            sxqSignatorylist.Add(sxqSignatory2);
            quickContract.SignatoryList = sxqSignatorylist;

            /** 快捷签约请求 **/
            Process(client, quickContract);
        }

        /// <summary>
        /// 企业和企业签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public void TwoCompanySign(SDKClient client)
        {
            QuickContract quickContract = new QuickContract();

            /** 全局掩码控制，如果设置为true，则所有的签约人都会遵守该掩码规则 **/
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //quickContract.CertNoMask = true;
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //quickContract.RealNameMask = true;

            /** 需要签署的合同 **/
            string contractPdfPath = CONTRACT_PATH_PREFIX + "TwoCompany.pdf";
            string contractName = "TwoCompanySign" + new Random().Next(100) + ".pdf@";
            string contractBase64Str = QuickSignUtil.FileToBase64Str(contractPdfPath);
            quickContract.PdfFileBase64 = contractName + contractBase64Str;

            /** 合同基本信息 **/
            // 签署名称
            string storeName = "企业和企业签约";
            // 存证说明
            string transAbs = "企业和企业签约示例";
            // 是否公开可见
            string isPublic = QuickDataStore.ACCESS_PUBLIC;
            QuickDataStore sxqDataStore = new QuickDataStore(storeName, transAbs, isPublic);
            quickContract.DataStore = sxqDataStore;

            /** 签约人1 **/
            QuickSignatory sxqSignatory1 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory1.RealName = "甲方公司";
            //签约方证件号 选填
            sxqSignatory1.CertNo = "91500000MA5UCYU7AA";
            //填了证件号就必选填证件类型
            sxqSignatory1.CertType = QuickSignatory.ID_INSTITUTION_CODE;
            // 签章类型 必填
            sxqSignatory1.SealType = QuickSignatory.SEAL_ENTERPRISE;
            // 是否自动签约 必填
            sxqSignatory1.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory1.SignatoryUserType = QuickSignatory.USER_ENTERPRISE;
            // 签约时间 必填
            sxqSignatory1.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // 签约方 必填
            sxqSignatory1.Group(QuickSignatory.GROUP_A);
            // 签约人邮箱 选填
            //sxqSignatory1.Email = "zjq115097475@qq.com";
            // 签约人手机 选填
            sxqSignatory1.Phone = "15923645555";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureX = 106d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureY = 544d;
            //签章页 （不填时默认最后一页）
            sxqSignatory1.SignaturePage = 1;


            /** 签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory2.RealName = "乙方公司";
            //签约方证件号 选填
            sxqSignatory2.CertNo = "91500000MA5UCYU7BB";
            //填了证件号就必选填证件类型
            sxqSignatory2.CertType = QuickSignatory.ID_INSTITUTION_CODE;
            // 签章类型 必填
            sxqSignatory2.SealType = QuickSignatory.SEAL_ENTERPRISE;
            // 是否自动签约  必填
            sxqSignatory2.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory2.SignatoryUserType = QuickSignatory.USER_ENTERPRISE;
            // 签约时间 必填
            sxqSignatory2.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory2.Group(QuickSignatory.GROUP_B);
            //签约人手机或邮箱 选填
            //		sxqSignatory2.Email = "888888@qq.com";
            sxqSignatory2.Phone = "15123166666";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureX = 377d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureY = 545d;
            //签章页 （不填时默认最后一页）
            sxqSignatory2.SignaturePage = 1;


            /** 设置签约方集合 **/
            List<QuickSignatory> sxqSignatorylist = new List<QuickSignatory>();
            sxqSignatorylist.Add(sxqSignatory1);
            sxqSignatorylist.Add(sxqSignatory2);
            quickContract.SignatoryList = sxqSignatorylist;

            /** 快捷签约请求 **/
            Process(client, quickContract);
        }

        /// <summary>
        /// 多人人签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public void MultiplePeopleSign(SDKClient client)
        {
            QuickContract quickContract = new QuickContract();

            /** 全局掩码控制，如果设置为true，则所有的签约人都会遵守该掩码规则 **/
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //quickContract.CertNoMask = true;
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //quickContract.RealNameMask = true;

            /** 需要签署的合同 **/
            string contractPdfPath = CONTRACT_PATH_PREFIX + "MultiplePeople.pdf";
            string contractName = "MultiplePeopleSign" + new Random().Next(100) + ".pdf@";
            string contractBase64Str = QuickSignUtil.FileToBase64Str(contractPdfPath);
            quickContract.PdfFileBase64 = contractName + contractBase64Str;

            /** 合同基本信息 **/
            // 签署名称
            string storeName = "多人签约";
            // 存证说明
            string transAbs = "多人签约示例";
            // 是否公开可见
            string isPublic = QuickDataStore.ACCESS_PUBLIC;
            QuickDataStore sxqDataStore = new QuickDataStore(storeName, transAbs, isPublic);
            quickContract.DataStore = sxqDataStore;

            /**签约人1 * */
            QuickSignatory sxqSignatory1 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory1.RealName = "张三";
            //签约方证件号 选填
            sxqSignatory1.CertNo = "430511198702173516";
            //填了证件号就必选填证件类型
            sxqSignatory1.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory1.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory1.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory1.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory1.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // 签约方 必填
            sxqSignatory1.Group(QuickSignatory.GROUP_A);
            // 签约人邮箱 选填
            //sxqSignatory1.Email = "zjq115097475@qq.com";
            // 签约人手机 选填
            sxqSignatory1.Phone = "15923641111";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureX = 106d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureY = 545d;
            //签章页 （不填时默认最后一页）
            sxqSignatory1.SignaturePage = 1;


            /** 签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory2.RealName = "乙方-李四";
            //签约方证件号 选填
            sxqSignatory2.CertNo = "430511198702171222";
            //填了证件号就必选填证件类型
            sxqSignatory2.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory2.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory2.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory2.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory2.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory2.Group(QuickSignatory.GROUP_B);
            //签约人手机或邮箱 选填
            //		sxqSignatory2.Email = "888888@qq.com";
            sxqSignatory2.Phone = "15923641222";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureX = 300d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureY = 545d;
            //签章页 （不填时默认最后一页）
            sxqSignatory2.SignaturePage = 1;

            /** 签约人3 **/
            QuickSignatory sxqSignatory3 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory3.RealName = "乙方-王五";
            //签约方证件号 选填
            sxqSignatory3.CertNo = "430511198702171333";
            //填了证件号就必选填证件类型
            sxqSignatory3.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory3.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory3.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory3.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory3.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory3.Group(QuickSignatory.GROUP_B);
            //签约人手机或邮箱 选填
            //		sxqSignatory2.Email = "888888@qq.com";
            sxqSignatory3.Phone = "15923641333";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory3.SignatureX = 440d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory3.SignatureY = 545d;
            //签章页 （不填时默认最后一页）
            sxqSignatory3.SignaturePage = 1;

            /** 设置签约方集合 **/
            List<QuickSignatory> sxqSignatorylist = new List<QuickSignatory>();
            sxqSignatorylist.Add(sxqSignatory1);
            sxqSignatorylist.Add(sxqSignatory2);
            sxqSignatorylist.Add(sxqSignatory3);
            quickContract.SignatoryList = sxqSignatorylist;

            /** 快捷签约请求 **/
            Process(client, quickContract);
        }

        /// <summary>
        /// 多方签约，每个签约方支持多个签约人
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public void MultiplePartiesSign(SDKClient client)
        {
            QuickContract quickContract = new QuickContract();

            /** 全局掩码控制，如果设置为true，则所有的签约人都会遵守该掩码规则 **/
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //quickContract.CertNoMask = true;
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //quickContract.RealNameMask = true;

            /** 需要签署的合同 **/
            string contractPdfPath = CONTRACT_PATH_PREFIX + "MultipleParties.pdf";
            string contractName = "MultiplePartiesSign" + new Random().Next(100) + ".pdf@";
            string contractBase64Str = QuickSignUtil.FileToBase64Str(contractPdfPath);
            quickContract.PdfFileBase64 = contractName + contractBase64Str;

            /** 合同基本信息 **/
            // 签署名称
            string storeName = "多方签约";
            // 存证说明
            string transAbs = "多方签约示例（每方可以有多个签约人）";
            // 是否公开可见
            string isPublic = QuickDataStore.ACCESS_PUBLIC;
            QuickDataStore sxqDataStore = new QuickDataStore(storeName, transAbs, isPublic);
            quickContract.DataStore = sxqDataStore;

            /** 签约方A-签约人1 **/
            QuickSignatory sxqSignatory1 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory1.RealName = "甲方";
            //签约方证件号 选填
            sxqSignatory1.CertNo = "430511198702173516";
            //填了证件号就必选填证件类型
            sxqSignatory1.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory1.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory1.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory1.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory1.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // 签约方 必填
            sxqSignatory1.Group(QuickSignatory.GROUP_A);
            // 签约人邮箱 选填
            //sxqSignatory1.Email = "zjq115097475@qq.com";
            // 签约人手机 选填
            sxqSignatory1.Phone = "15923641267";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureX = 106d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureY = 545d;
            //签章页 （不填时默认最后一页）
            sxqSignatory1.SignaturePage = 1;


            /** 签约方B-签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory2.RealName = "乙方-李四";
            //签约方证件号 选填
            sxqSignatory2.CertNo = "430511198702173111";
            //填了证件号就必选填证件类型
            sxqSignatory2.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory2.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory2.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory2.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory2.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory2.Group(QuickSignatory.GROUP_B);
            //签约人手机或邮箱 选填
            //		sxqSignatory2.Email = "888888@qq.com";
            sxqSignatory2.Phone = "15923641222";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureX = 300d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureY = 545d;
            //签章页 （不填时默认最后一页）
            sxqSignatory2.SignaturePage = 1;

            /** 签约方B-签约人3 **/
            QuickSignatory sxqSignatory3 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory3.RealName = "乙方-王五";
            //签约方证件号 选填
            sxqSignatory3.CertNo = "430511198702173222";
            //填了证件号就必选填证件类型
            sxqSignatory3.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory3.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory3.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory3.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory3.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory3.Group(QuickSignatory.GROUP_B);
            //签约人手机或邮箱 选填
            //		sxqSignatory2.Email = "888888@qq.com";
            sxqSignatory3.Phone = "15923641333";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory3.SignatureX = 440d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory3.SignatureY = 545d;
            //签章页 （不填时默认最后一页）
            sxqSignatory3.SignaturePage = 1;

            /** 签约方C-签约人4 **/
            QuickSignatory sxqSignatory4 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory4.RealName = "丙方";
            //签约方证件号 选填
            sxqSignatory4.CertNo = "430511198702173333";
            //填了证件号就必选填证件类型
            sxqSignatory4.CertType = QuickSignatory.ID_PERSONAL_CARD;
            // 签章类型 必填
            sxqSignatory4.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约 必填
            sxqSignatory4.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory4.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory4.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory4.Group(QuickSignatory.GROUP_C);
            //签约人手机或邮箱 选填
            //		sxqSignatory2.Email = "888888@qq.com";
            sxqSignatory4.Phone = "15923641262";
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory4.SignatureX = 106d;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory4.SignatureY = 670d;
            //签章页 （不填时默认最后一页）
            sxqSignatory4.SignaturePage = 1;

            /** 设置签约方集合 **/
            List<QuickSignatory> sxqSignatorylist = new List<QuickSignatory>();
            sxqSignatorylist.Add(sxqSignatory1);
            sxqSignatorylist.Add(sxqSignatory2);
            sxqSignatorylist.Add(sxqSignatory3);
            sxqSignatorylist.Add(sxqSignatory4);
            quickContract.SignatoryList = sxqSignatorylist;

            /** 快捷签约请求 **/
            Process(client, quickContract);
        }

        
        /// <summary>
        /// 发送并处理快捷签约请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="quickContract">签约的合同对象</param>
        private void Process(SDKClient client, QuickContract quickContract)
        {
            QuickSignRequest request = new QuickSignRequest(quickContract);
            string response = null;
            try
            {
                response = client.Service(request);
            }
            catch (Exception e)
            {
                throw new Exception("快捷签约合同失败,失败原因： " + e.Message);
            }

            SdkResponse<QuickSignResult> signRS = HttpJsonConvert.DeserializeResponse<QuickSignResult>(response);
            if (!signRS.Success)
            {
                throw new Exception("快捷签约合同失败，失败原因： " + signRS.Message);
            }
            string FETCH_URL = client.ServerUrl + RequestPathConstant.FETCH + "?appKey=" + client.AccessToken
                + "&appSecret=" + client.AccessSecret + "&storeNo=" + signRS.Result.StoreNo;
            Console.WriteLine("Store No: {0} , you can fetch the file:\n a) call Program.cs#Fetch method with Store No {1}\n b) access URL in the Explorer: {2}\n",
                signRS.Result.StoreNo, signRS.Result.StoreNo, FETCH_URL);
        }
    }
}
