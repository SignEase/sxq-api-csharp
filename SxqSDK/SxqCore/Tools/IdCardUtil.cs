namespace SxqCore.Tools
{
    using System;
    using System.Collections.Generic;
    public class IdCardUtil
    {

        static Dictionary<int, string> zoneNum = InitZoneDef();

        private static Dictionary<int, string> InitZoneDef()
        {
            Dictionary<int, string> zoneDef = new Dictionary<int, string>();
            zoneDef.Add(11, "北京");
            zoneDef.Add(12, "天津");
            zoneDef.Add(13, "河北");
            zoneDef.Add(14, "山西");
            zoneDef.Add(15, "内蒙古");
            zoneDef.Add(21, "辽宁");
            zoneDef.Add(22, "吉林");
            zoneDef.Add(23, "黑龙江");
            zoneDef.Add(31, "上海");
            zoneDef.Add(32, "江苏");
            zoneDef.Add(33, "浙江");
            zoneDef.Add(34, "安徽");
            zoneDef.Add(35, "福建");
            zoneDef.Add(36, "江西");
            zoneDef.Add(37, "山东");
            zoneDef.Add(41, "河南");
            zoneDef.Add(42, "湖北");
            zoneDef.Add(43, "湖南");
            zoneDef.Add(44, "广东");
            zoneDef.Add(45, "广西");
            zoneDef.Add(46, "海南");
            zoneDef.Add(50, "重庆");
            zoneDef.Add(51, "四川");
            zoneDef.Add(52, "贵州");
            zoneDef.Add(53, "云南");
            zoneDef.Add(54, "西藏");
            zoneDef.Add(61, "陕西");
            zoneDef.Add(62, "甘肃");
            zoneDef.Add(63, "青海");
            zoneDef.Add(64, "宁夏");
            zoneDef.Add(65, "新疆");
            zoneDef.Add(71, "台湾");
            zoneDef.Add(81, "香港");
            zoneDef.Add(82, "澳门");
            zoneDef.Add(91, "国外");
            return zoneDef;
        }

        static int[] PARITYBIT = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
        static int[] POWER_LIST = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

        /**
         * 身份证号是否基本有效
         *
         * @param s 号码内容
         * @return 是否有效，null和""都是false
         */
        public static bool IsIdCard(String s)
        {
            if (s == null || (s.Length != 15 && s.Length != 18))
                return false;
            
            char[] cs = s.ToUpper().ToCharArray();
            // （1）校验位数
            int power = 0;
            // 循环比正则表达式更快
            for (int i = 0; i < cs.Length; i++)
            {
                if (i == cs.Length - 1 && cs[i] == 'X')
                    break;// 最后一位可以是X或者x
                if (cs[i] < '0' || cs[i] > '9')
                    return false;
                if (i < cs.Length - 1)
                    power += (cs[i] - '0') * POWER_LIST[i];
            }
            // （2）校验区位码
            if (!zoneNum.ContainsKey(int.Parse(s.Substring(0, 2))))
            {
                return false;
            }
            // （3）校验年份
            String year = s.Length == 15 ? "19" + s.Substring(6, 2) : s.Substring(6, 4);
            int iyear = int.Parse(year);
            if (iyear < 1900 || iyear > DateTime.Now.Year)
            {
                return false;// 1900年的PASS，超过今年的PASS
            }
            // （4）校验月份
            String month = s.Length == 15 ? s.Substring(8, 2) : s.Substring(10, 2);
            int imonth = int.Parse(month);
            if (imonth < 1 || imonth > 12)
                return false;
            // （5）校验天数
            String day = s.Length == 15 ? s.Substring(10, 2) : s.Substring(12, 2);
            int iday = int.Parse(day);
            if (iday < 1 || iday > 31)
                return false;
            // （6）校验一个合法的年月日
            if (!Validate(iyear, imonth, iday))
                return false;
            // （7）校验“校验码”
            if (s.Length == 15)
                return true;
            return cs[cs.Length - 1] == PARITYBIT[power % 11];
        }

        static bool Validate(int year, int month, int day)
        {
            // 比如考虑闰月，大小月等
            return true;
        }

        public IdCardUtil(){}

    }
}
