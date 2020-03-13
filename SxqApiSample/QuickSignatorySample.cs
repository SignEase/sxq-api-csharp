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

            //签约人手机邮箱 选填
            sxqSignatory1.Email = "zjq115097475@qq.com";
            //		sxqSignatory1.Phone = "15123164744";
            //签约方证件号 选填
            //		yclSignatory1.CertNo = "24324342342323234243";
            //填了证件号就必选填证件类型
            //		sxqSignatory1.CertType = QuickSignatory.ID_INSTITUTION_CODE;
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureX = 100.0;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureY = 100.0;
            //签章页 （不填时默认最后一页）
            sxqSignatory1.SignaturePage = 1;
            //签章定位关键词（与X.Y坐标 必须二选一）
            //		sxqSignatory1.Keywords("甲方（签章）");
            //章的用途(签章类型为企业是必填)
            sxqSignatory1.SealPurpose = "合同专用章";



            /** 签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory2.RealName = "李四";
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

            //签约人手机邮箱 选填
            //		yclSignatory2.setEmail("888888@qq.com");
            sxqSignatory2.Phone = "15123164744";
            //签约方证件号 选填
            sxqSignatory2.CertNo = "435534354435354";
            //填了证件号就必选填证件类型
            sxqSignatory2.CertType = QuickSignatory.ID_PERSONAL_CARD;
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureX = 100.0;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureY = 200.0;
            //签章页 （不填时默认最后一页）
            sxqSignatory2.SignaturePage = 1;
            //sxqSignatory2.Keywords = "乙方（签章）";

    
            /** 设置签约方集合 **/
            List<QuickSignatory> sxqSignatorylist = new List<QuickSignatory>();
            sxqSignatorylist.Add(sxqSignatory1);
            sxqSignatorylist.Add(sxqSignatory2);
            quickContract.SignatoryList = sxqSignatorylist;

            /** 快捷签约请求 **/
            Process(client, quickContract);
        }

        //TODO
        /// <summary>
        /// 企业和个人签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public void ConpamyAndPersonSign(SDKClient client)
        {
            QuickContract quickContract = new QuickContract();

            /** 全局掩码控制，如果设置为true，则所有的签约人都会遵守该掩码规则 **/
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //quickContract.CertNoMask = true;
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //quickContract.RealNameMask = true;

            /** 需要签署的合同 **/
            string contractPdfPath = CONTRACT_PATH_PREFIX + "ConpamyAndPerson.pdf";
            string contractName = "ConpamyAndPersonSign" + new Random().Next(100) + ".pdf@";
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

            /** 签约人1 **/
            QuickSignatory sxqSignatory1 = new QuickSignatory();

            /** 签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();

            /** 设置签约方集合 **/
            List<QuickSignatory> sxqSignatorylist = new List<QuickSignatory>();
            sxqSignatorylist.Add(sxqSignatory1);
            sxqSignatorylist.Add(sxqSignatory2);
            quickContract.SignatoryList = sxqSignatorylist;

            /** 快捷签约请求 **/
            Process(client, quickContract);
        }

        //TODO
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
            string storeName = "多人签约";
            // 存证说明
            string transAbs = "多人签约示例";
            // 是否公开可见
            string isPublic = QuickDataStore.ACCESS_PUBLIC;
            QuickDataStore sxqDataStore = new QuickDataStore(storeName, transAbs, isPublic);
            quickContract.DataStore = sxqDataStore;

            /** 签约人1 **/
            QuickSignatory sxqSignatory1 = new QuickSignatory();

            /** 签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();


            /** 设置签约方集合 **/
            List<QuickSignatory> sxqSignatorylist = new List<QuickSignatory>();
            sxqSignatorylist.Add(sxqSignatory1);
            sxqSignatorylist.Add(sxqSignatory2);
            quickContract.SignatoryList = sxqSignatorylist;

            /** 快捷签约请求 **/
            Process(client, quickContract);
        }

        //TODO
        /// <summary>
        /// 多人人签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public void MutiplePeopleSign(SDKClient client)
        {
            QuickContract quickContract = new QuickContract();

            /** 全局掩码控制，如果设置为true，则所有的签约人都会遵守该掩码规则 **/
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //quickContract.CertNoMask = true;
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //quickContract.RealNameMask = true;

            /** 需要签署的合同 **/
            string contractPdfPath = CONTRACT_PATH_PREFIX + "MutiplePeople.pdf";
            string contractName = "MutiplePeopleSign" + new Random().Next(100) + ".pdf@";
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

            /** 签约人1 **/
            QuickSignatory sxqSignatory1 = new QuickSignatory();

            /** 签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();

            /** 签约人3 **/
            QuickSignatory sxqSignatory3 = new QuickSignatory();

            /** 签约人4 **/
            QuickSignatory sxqSignatory4 = new QuickSignatory();


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
        /// 多方签约，每个签约方支持多个签约人
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public void MutiplePartiesSign(SDKClient client)
        {
            QuickContract quickContract = new QuickContract();

            /** 全局掩码控制，如果设置为true，则所有的签约人都会遵守该掩码规则 **/
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //quickContract.CertNoMask = true;
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //quickContract.RealNameMask = true;

            /** 需要签署的合同 **/
            string contractPdfPath = CONTRACT_PATH_PREFIX + "MutipleParties.pdf";
            string contractName = "MutiplePartiesSign" + new Random().Next(100) + ".pdf@";
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
            sxqSignatory1.RealName = "姓名1";
            // 签章类型 必填
            sxqSignatory1.SealType = QuickSignatory.SEAL_ENTERPRISE;
            // 是否自动签约  必填
            sxqSignatory1.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory1.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory1.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //签约方 必填
            sxqSignatory1.Group(QuickSignatory.GROUP_A);

            //签约人手机邮箱 选填
            sxqSignatory1.Email = "zjq115097475@qq.com";
            //		sxqSignatory1.Phone = "15123164744";
            //签约方证件号 选填
            //		yclSignatory1.CertNo = "24324342342323234243";
            //填了证件号就必选填证件类型
            //		sxqSignatory1.CertType = QuickSignatory.ID_INSTITUTION_CODE;
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureX = 100.0;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory1.SignatureY = 100.0;
            //签章页 （不填时默认最后一页）
            sxqSignatory1.SignaturePage = 1;
            //签章定位关键词（与X.Y坐标 必须二选一）
            //		sxqSignatory1.Keywords("甲方（签章）");
            //章的用途(签章类型为企业是必填)
            sxqSignatory1.SealPurpose = "合同专用章";

            /** 单个签约人的掩码规则，优先级最高，设置后会覆盖全局的掩码规则 **/
            //签章姓名是否掩码 （为true时仅显示姓，其余的 * 号代替）
            //		sxqSignatory1.RealNameMask = true;
            //签章证件号是否掩码 （为true时后四位用 * 号代替）
            //		sxqSignatory1.CertNoMask = true;
            //章编号（防伪码）
            //		sxqSignatory1.SealSn = "123456789456123";


            /** 签约方B-签约人2 **/
            QuickSignatory sxqSignatory2 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory2.RealName = "姓名2";
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

            //签约人手机邮箱 选填
            //		yclSignatory2.setEmail("888888@qq.com");
            sxqSignatory2.Phone = "15123164744";
            //签约方证件号 选填
            sxqSignatory2.CertNo = "4355343544353ssss54";
            //填了证件号就必选填证件类型
            sxqSignatory2.CertType = QuickSignatory.ID_PERSONAL_CARD;
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureX = 100.0;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory2.SignatureY = 100.0;
            //签章页 （不填时默认最后一页）
            sxqSignatory2.SignaturePage = 1;
            sxqSignatory2.Keywords = "开户银行";

            /** 签约方B-签约人3 **/
            QuickSignatory sxqSignatory3 = new QuickSignatory();
            // 签约人姓名 必填
            sxqSignatory3.RealName = "姓名2";
            // 签章类型 必填
            sxqSignatory3.SealType = QuickSignatory.SEAL_PERSONAL;
            // 是否自动签约  必填
            sxqSignatory3.SignatoryAuto = QuickSignatory.BOOL_YES;
            // 签约用户类型 必填
            sxqSignatory3.SignatoryUserType = QuickSignatory.USER_PERSONAL;
            // 签约时间 必填
            sxqSignatory3.SignatoryTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // 签约方 必填
            sxqSignatory3.Group(QuickSignatory.GROUP_B);
            //签约人手机邮箱 选填
            //		yclSignatory3.Email = "2222222@qq.com";
            sxqSignatory3.Phone = "15123164744";
            //签约方证件号 选填
            sxqSignatory3.CertNo = "4355343544353";
            //填了证件号就必选填证件类型
            sxqSignatory3.CertType = QuickSignatory.ID_PERSONAL_CARD;
            //签章x坐标 （不填写时系统自动生成）
            sxqSignatory3.SignatureX = 20.0;
            //签章y坐标 （不填写时系统自动生成）
            sxqSignatory3.SignatureY = 20.0;
            //签章页 （不填时默认最后一页）
            sxqSignatory3.SignaturePage = 2;

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
            Console.WriteLine("Store No: {0} , you can fetch the file:\n a) call Program.cs#Fetch method with Store No\n b) access URL: {1}\n",
                signRS.Result.StoreNo, FETCH_URL);
        }
    }
}
