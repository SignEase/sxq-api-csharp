namespace SxqCore.Tools
{
    using System;

    public class ResponseUtils
    {
        public static string GetResponse(string responseTxt, string mark)
        {
            if (string.IsNullOrEmpty(responseTxt))
            {
                throw new Exception("服务器返回的相应报文为空!");
            }
            if (GetResponseCode(responseTxt) != 0)
            {
                throw new Exception("远程服务器返回失败，失败原因：" + GetResponseMessage(responseTxt));
            }
            string str = "\"" + mark + "\":\"";
            int index = responseTxt.IndexOf(str);
            if (index < 0)
            {
                throw new Exception("远程服务器返回失败，" + responseTxt);
            }
            index += str.Length;
            int length = responseTxt.IndexOf("\"", index);
            if (length < 0)
            {
                length = responseTxt.Length;
            }
            return responseTxt.Substring(index, length - index);
        }

        public static int GetResponseCode(string responseTxt) => 
            GetResponseNumber(responseTxt, "code");

        public static string GetResponseJsonString(string responseTxt, string mark)
        {
            string str = "\"" + mark + "\":\"";
            int index = responseTxt.IndexOf(str);
            if (index < 0)
            {
                throw new Exception("远程服务器返回失败，" + responseTxt);
            }
            index += str.Length;
            int num2 = responseTxt.LastIndexOf("}\",");
            if (num2 < 0)
            {
                num2 = responseTxt.Length - 1;
            }
            else
            {
                num2++;
            }
            return responseTxt.Substring(index, num2 - index).Replace("\\\"", "\"");
        }

        public static string GetResponseListString(string responseTxt, string mark)
        {
            string str = "\"" + mark + "\":";
            int index = responseTxt.IndexOf(str);
            if (index < 0)
            {
                throw new Exception("远程服务器返回失败，" + responseTxt);
            }
            index += str.Length;
            int num2 = responseTxt.LastIndexOf("]");
            if (num2 < 0)
            {
                num2 = responseTxt.Length - 1;
            }
            else
            {
                num2++;
            }
            return responseTxt.Substring(index, num2 - index);
        }

        public static string GetResponseMessage(string responseTxt) => 
            GetResponseString(responseTxt, "message");

        public static int GetResponseNumber(string responseTxt, string mark)
        {
            string str = "\"" + mark + "\":";
            int index = responseTxt.IndexOf(str);
            if (index < 0)
            {
                throw new Exception("远程服务器返回失败，" + responseTxt);
            }
            int num2 = responseTxt.IndexOf(",", index);
            if (num2 < 0)
            {
                num2 = responseTxt.Length - 1;
            }
            index += str.Length;
            string s = responseTxt.Substring(index, num2 - index);
            int num3 = 0;
            try
            {
                num3 = int.Parse(s);
            }
            catch (Exception exception)
            {
                throw new Exception("远程服务器返回失败，原因：" + exception.Message + " " + responseTxt);
            }
            return num3;
        }

        public static string GetResponseObjString(string responseTxt, string mark)
        {
            string str = "\"" + mark + "\":";
            int index = responseTxt.IndexOf(str);
            if (index < 0)
            {
                return string.Empty;
            }
            index += str.Length;
            int num2 = responseTxt.IndexOf("}", index);
            if (num2 < 0)
            {
                num2 = responseTxt.Length - 1;
            }
            else
            {
                num2++;
            }
            return responseTxt.Substring(index, num2 - index);
        }

        public static string GetResponseOneObjString(string responseTxt, string mark)
        {
            string str = "\"" + mark + "\":";
            int index = responseTxt.IndexOf(str);
            if (index < 0)
            {
                //throw new Exception("远程服务器返回失败，" + responseTxt);
                return "";
            }
            index += str.Length;
            // null judgement
            string nullValue = responseTxt.Substring(index, 4);
            if("null".Equals(nullValue))
            {
                return "";
            }

            int num2 = responseTxt.LastIndexOf("},");
            if (num2 < 0)
            {
                num2 = responseTxt.Length - 1;
            }
            else
            {
                num2++;
            }
            return responseTxt.Substring(index, num2 - index);
        }

        public static string GetResponseString(string responseTxt, string mark)
        {
            string str = "\"" + mark + "\":\"";
            int index = responseTxt.IndexOf(str);
            if (index < 0)
            {
                return string.Empty;
            }
            index += str.Length;
            int num2 = responseTxt.IndexOf("\"", index);
            if (num2 < 0)
            {
                num2 = responseTxt.Length - 1;
            }
            return responseTxt.Substring(index, num2 - index);
        }

        public static bool GetResponseBool(string responseTxt, string mark)
        {
            string str = "\"" + mark + "\":";
            int index = responseTxt.IndexOf(str);
            if (index < 0)
            {
                return false;
            }
            index += str.Length;
            int num2 = responseTxt.IndexOf(",", index);
            if (num2 < 0)
            {
                num2 = responseTxt.Length - 1;
            }
            return Convert.ToBoolean(responseTxt.Substring(index, num2 - index));
        }
    }
}

