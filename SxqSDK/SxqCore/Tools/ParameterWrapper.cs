using System;
using System.Collections.Generic;
using SxqClient.Http;
using SxqCore.Bean.Contract;

namespace SxqSDK.SxqCore.Tools
{
    /// <summary>
    /// 请求的参数设值
    /// </summary>
    public class ParameterWrapper
    {
        public ParameterWrapper()
        {
        }

        public static HttpParamers WrapContract(Contract contract)
        {
            HttpParamers parameters = HttpParamers.PostParamers();
            parameters.AddParamer("yclDataStore.storeName", contract.DataStore.StoreName);
            parameters.AddParamer("yclDataStore.isPublic", contract.DataStore.IsPublic);
            parameters.AddParamer("yclDataStore.userBizNumber", contract.DataStore.UserBizNumber);
            parameters.AddParamer("pdfFileBase64", contract.PdfFileBase64);
            parameters.AddParamer("allowPreview", contract.AllowPreview.ToString());
            parameters.AddParamer("allowPwdSetting", contract.AllowPwdSetting.ToString());

            List<Signatory> signatoryList = contract.SignatoryList;
            for (int i = 0; i < signatoryList.Count; i++)
            {
                if (contract.RealNameMask)
                {
                    signatoryList[i].RealNameMask = contract.RealNameMask;
                }
                if (contract.CertNoMask)
                {
                    signatoryList[i].CertNoMask = contract.CertNoMask;
                }

                // 设置必填参数
                parameters.AddParamer("yclSignatoryList[" + i + "].realName", signatoryList[i].RealName);
                parameters.AddParamer("yclSignatoryList[" + i + "].signatoryUserType", signatoryList[i].SignatoryUserType);
                parameters.AddParamer("yclSignatoryList[" + i + "].signatoryTime", signatoryList[i].SignatoryTime);
                parameters.AddParamer("yclSignatoryList[" + i + "].groupName", signatoryList[i].GroupName);
                parameters.AddParamer("yclSignatoryList[" + i + "].groupChar", signatoryList[i].GroupChar);
                // 强制手写签章(针对签约者是个人)
                if(contract.HandWriting && signatoryList[i].IsPersonal())
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].sealType", SxqConst.SEAL_HANDWRITING);
                }
                else
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].sealType", signatoryList[i].SealType);
                }

                // 是否自动签章（授信签约）
                string autoSign = string.IsNullOrEmpty(contract.SignatoryAuto) ? signatoryList[i].SignatoryAuto : contract.SignatoryAuto;
                parameters.AddParamer("yclSignatoryList[" + i + "].signatoryAuto", autoSign);

                // 设置可选参数
                if (!string.IsNullOrEmpty(signatoryList[i].Email))
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].email", signatoryList[i].Email);
                }
                if (!string.IsNullOrEmpty(signatoryList[i].Phone))
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].phone", signatoryList[i].Phone);
                }
                if (!string.IsNullOrEmpty(signatoryList[i].Keywords))
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].keywords", signatoryList[i].Keywords);
                }
                if (signatoryList[i].SignatureX != -1d)
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].signatureX", signatoryList[i].SignatureX.ToString());
                }
                if (signatoryList[i].SignatureY != -1d)
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].signatureY", signatoryList[i].SignatureY.ToString());
                }
                if (signatoryList[i].SignaturePage != -1)
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].signaturePage", signatoryList[i].SignaturePage.ToString());
                }

                if (!string.IsNullOrEmpty(signatoryList[i].CertNo))
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].certNo", signatoryList[i].CertNo);
                    parameters.AddParamer("yclSignatoryList[" + i + "].certType", signatoryList[i].CertType);
                }

                if (!string.IsNullOrEmpty(signatoryList[i].SealPurpose))
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].sealPurpose", signatoryList[i].SealPurpose);
                }

                if (signatoryList[i].RealNameMask)
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].realNameMask", signatoryList[i].RealNameMask + "");
                }

                if (signatoryList[i].CertNoMask)
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].certNoMask", signatoryList[i].CertNoMask + "");
                }

                if (!string.IsNullOrEmpty(signatoryList[i].SealSn))
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].sealSn", signatoryList[i].SealSn);
                }

                // 合同失效时间设置：合同对象上的'失效时间'对所有签约人都生效；如果签约人上有单独设置失效时间的，则使用签约人设置进行覆盖
                if (signatoryList[i].ValidTimeStamp != -1)
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].validTimeStamp", signatoryList[i].ValidTimeStamp.ToString());
                } else if (contract.ValidTimeStamp != -1)
                {
                    parameters.AddParamer("yclSignatoryList[" + i + "].validTimeStamp", contract.ValidTimeStamp.ToString());
                }

            }

            return parameters;
        }
    }
}
