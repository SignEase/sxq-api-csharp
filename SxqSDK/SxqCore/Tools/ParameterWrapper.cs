using System;
using System.Collections.Generic;
using SxqClient.Http;
using SxqCore.Bean.Contract;

namespace SxqSDK.SxqCore.Tools
{
    public class ParameterWrapper
    {
        public ParameterWrapper()
        {
        }

        public static HttpParamers WrapContract(Contract contract)
        {
            HttpParamers paramers = HttpParamers.PostParamers();
            paramers.AddParamer("yclDataStore.storeName", contract.DataStore.StoreName);
            paramers.AddParamer("yclDataStore.isPublic", contract.DataStore.IsPublic);
            paramers.AddParamer("yclDataStore.userBizNumber", contract.DataStore.UserBizNumber);
            paramers.AddParamer("pdfFileBase64", contract.PdfFileBase64);
            paramers.AddParamer("allowPreview", contract.AllowPreview.ToString());
            paramers.AddParamer("allowPwdSetting", contract.AllowPwdSetting.ToString());

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
                paramers.AddParamer("yclSignatoryList[" + i + "].realName", signatoryList[i].RealName);
                paramers.AddParamer("yclSignatoryList[" + i + "].sealType", signatoryList[i].SealType);
                paramers.AddParamer("yclSignatoryList[" + i + "].signatoryUserType", signatoryList[i].SignatoryUserType);
                paramers.AddParamer("yclSignatoryList[" + i + "].signatoryTime", signatoryList[i].SignatoryTime);
                paramers.AddParamer("yclSignatoryList[" + i + "].groupName", signatoryList[i].GroupName);
                paramers.AddParamer("yclSignatoryList[" + i + "].groupChar", signatoryList[i].GroupChar);

                // 是否自动签章（授信签约）
                string autoSign = string.IsNullOrEmpty(contract.SignatoryAuto) ? signatoryList[i].SignatoryAuto : contract.SignatoryAuto;
                paramers.AddParamer("yclSignatoryList[" + i + "].signatoryAuto", autoSign);

                // 设置可选参数
                if (!string.IsNullOrEmpty(signatoryList[i].Email))
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].email", signatoryList[i].Email);
                }
                if (!string.IsNullOrEmpty(signatoryList[i].Phone))
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].phone", signatoryList[i].Phone);
                }
                if (!string.IsNullOrEmpty(signatoryList[i].Keywords))
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].keywords", signatoryList[i].Keywords);
                }
                if (signatoryList[i].SignatureX != -1d)
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].signatureX", signatoryList[i].SignatureX.ToString());
                }
                if (signatoryList[i].SignatureY != -1d)
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].signatureY", signatoryList[i].SignatureY.ToString());
                }
                if (signatoryList[i].SignaturePage != -1)
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].signaturePage", signatoryList[i].SignaturePage.ToString());
                }

                if (!string.IsNullOrEmpty(signatoryList[i].CertNo))
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].certNo", signatoryList[i].CertNo);
                    paramers.AddParamer("yclSignatoryList[" + i + "].certType", signatoryList[i].CertType);
                }

                if (!string.IsNullOrEmpty(signatoryList[i].SealPurpose))
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].sealPurpose", signatoryList[i].SealPurpose);
                }

                if (signatoryList[i].RealNameMask)
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].realNameMask", signatoryList[i].RealNameMask + "");
                }

                if (signatoryList[i].CertNoMask)
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].certNoMask", signatoryList[i].CertNoMask + "");
                }

                if (!string.IsNullOrEmpty(signatoryList[i].SealSn))
                {
                    paramers.AddParamer("yclSignatoryList[" + i + "].sealSn", signatoryList[i].SealSn);
                }

            }

            return paramers;
        }
    }
}
