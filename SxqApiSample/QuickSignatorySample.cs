using SxqClient.Http;
using SxqCore.Bean.Request;
using SxqCore.Bean.Response;
using SxqCore.Tools;
using System;
using SxqCore.Bean;
using SxqCore.Bean.Contract;
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
    class QuickSignatorySample : SignatorySample
    {

        /// <summary>
        /// 甲乙两人签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public new void TwoPeopleSign(SDKClient client)
        {
            Contract contract = ContractOfTwoPeople();
            contract.SignatoryAuto = SxqConst.BOOL_YES;

            /** 快捷签约请求 **/
            Process(client, contract);
        }

        /// <summary>
        /// 企业和个人签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public new void CompanyAndPersonSign(SDKClient client)
        {
            Contract contract = ContractOfCompanyAndPerson();
            contract.SignatoryAuto = SxqConst.BOOL_YES;

            /** 快捷签约请求 **/
            Process(client, contract);
        }

        /// <summary>
        /// 企业和企业签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public new void TwoCompanySign(SDKClient client)
        {
            Contract contract = ContractOfTwoCompany();
            contract.SignatoryAuto = SxqConst.BOOL_YES;

            /** 快捷签约请求 **/
            Process(client, contract);
        }

        /// <summary>
        /// 多人人签约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public new void MultiplePeopleSign(SDKClient client)
        {
            Contract contract = ContractOfMultiplePeople();
            contract.SignatoryAuto = SxqConst.BOOL_YES;

            /** 快捷签约请求 **/
            Process(client, contract);
        }

        /// <summary>
        /// 多方签约，每个签约方支持多个签约人
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public new void MultiplePartiesSign(SDKClient client)
        {
            Contract contract = ContractOfMultipleParties();
            contract.SignatoryAuto = SxqConst.BOOL_YES;

            /** 快捷签约请求 **/
            Process(client, contract);
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

            SdkResponse<SignResult> signRS = HttpJsonConvert.DeserializeResponse<SignResult>(response);
            if (!signRS.Success)
            {
                throw new Exception("快捷签约合同失败，失败原因： " + signRS.Message);
            }
            string DOWNLOAD_URL = client.ServerUrl + RequestPathConstant.DOWNLOAD_CONTRACT + "?appKey=" + client.AccessToken
                + "&appSecret=" + client.AccessSecret + "&storeNo=" + signRS.Result.ContractId;
            Console.WriteLine("Contract No: {0} , you can download the file:\n a) call Program.cs#Fetch method with Store No {1}\n b) access URL in the Explorer: {2}\n",
                signRS.Result.ContractId, signRS.Result.ContractId, DOWNLOAD_URL);
        }
    }
}
