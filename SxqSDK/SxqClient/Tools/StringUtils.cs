namespace SxqClient.Tools
{
    using System;
    using System.Text;

    internal class StringUtils
    {
        public static string RANDOM_CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string ByteToString(byte[] InBytes, int len)
        {
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str = str + $"{InBytes[i]:X2}";
            }
            return str;
        }

        public static string Random(int length)
        {
            if (length < 1)
            {
                return null;
            }
            System.Random random = new System.Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(RANDOM_CHARACTERS.Length);
                builder.Append(RANDOM_CHARACTERS.ToCharArray()[index]);
            }
            return builder.ToString();
        }
    }
}

