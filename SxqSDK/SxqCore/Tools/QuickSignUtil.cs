namespace SxqCore.Tools
{
    using System;
    using System.IO;

    public class QuickSignUtil
    {
        public QuickSignUtil()
        {
        }

        /// <summary>
        /// 生成业务编号
        /// </summary>
        /// <returns>生成的业务编号</returns>
		public static string GainNo()
		{
			string number = "";
            number += DateTime.Now.ToString("yyyyMMddHHmmssSSS");
            Random rd = new Random();
            int a = rd.Next() * 1000;
            if (a < 10 && a > 0)
			{
				a = a * 100;
			}
			else if (a >= 10 && a < 100)
			{
				a = a * 10;
			}
            number += (a == 0) ? "000" : a.ToString();
			return number;
		}

        /// <summary>
        /// 将文件转码成BASE64的字符串
        /// </summary>
        /// <param name="filePath">文件路路径</param>
        /// <returns>文件对应的BASE64字符串</returns>
        public static string FileToBase64Str(string filePath)
        {
            string base64Str = string.Empty;
            try
            {
                using (FileStream filestream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] bt = new byte[filestream.Length];

                    filestream.Read(bt, 0, bt.Length);
                    base64Str = Convert.ToBase64String(bt);
                    filestream.Close();
                }

                return base64Str;
            }
            catch (Exception e)
            {
                return base64Str;
            }
        }

    }

}
