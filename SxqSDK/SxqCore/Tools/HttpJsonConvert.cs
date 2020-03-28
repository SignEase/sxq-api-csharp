namespace SxqCore.Tools
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;
    using SxqCore.Bean.Response;
    using System;

    public class HttpJsonConvert
    {

        public static SdkResponse<T> DeserializeResponse<T>(string value)
        {
            SdkResponse<T> response = (SdkResponse<T>) Activator.CreateInstance(typeof(SdkResponse<T>));
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("反序列化结果为空");
            }
            response.Success = ResponseUtils.GetResponseBool(value, "success");
            if (!response.Success)
            {
                response.Code = ResponseUtils.GetResponseCode(value);
            }
            response.Message = ResponseUtils.GetResponseString(value, "message");
            string responseOneObjString = "";
            try
            {
                responseOneObjString = ResponseUtils.GetResponseOneObjString(value, "result");
                if(string.IsNullOrEmpty(responseOneObjString))
                {
                    responseOneObjString = ResponseUtils.GetResponseOneObjString(value, "data");
                }

                // return data format
                if(string.IsNullOrEmpty(responseOneObjString))
                {
                    responseOneObjString = "{\"storeNo\":\"" + ResponseUtils.GetResponseString(value, "storeNo") + "\"}";
                }
            }
            catch (Exception)
            {
            }
            T local = (T) Activator.CreateInstance(typeof(T));
            if (!string.IsNullOrEmpty(responseOneObjString))
            {
                local = DeserializeObject<T>(responseOneObjString);
            }
            response.Result = local;
            return response;
        }

        public static T DeserializeObject<T>(string value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(value, settings);
        }


        public static string SerializeObject(object value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(value, Formatting.Indented, settings);
        }

        public static string BeanToString<T>(T bean)
        {
            JObject jobject = new JObject();
            jobject = JObject.FromObject(bean);
            return jobject.ToString();
        }
    }
}

