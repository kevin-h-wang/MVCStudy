using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace EasyUIDemo.Utility
{
    public static class FIDataVerifyHelper
    {
        #region 数据校验

        /// <summary>
        ///     检测是否为整数类型
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsInteger(this String data)
        {
            int d;

            if (int.TryParse(data, out d))
                return true;

            return false;
        }

        /// <summary>
        ///     检测Int范围,看是否越界
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsBigger(this String data)
        {
            decimal d;
            if (decimal.TryParse(data, out d))
            {
                if (d > int.MaxValue || d < int.MinValue)
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     检测是否为浮点类型
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsDecimal(this String data)
        {
            decimal d;

            if (decimal.TryParse(data, out d))
                return true;

            return false;
        }

        /// <summary>
        ///     检测Decimal范围,看是否越界
        /// </summary>
        /// <param name="data">字符串</param>
        /// <param name="intPart">int范围</param>
        /// <param name="decimalPart">decimal范围</param>
        /// <returns>bool结果</returns>
        public static bool IsDecimaloverstep(String data, int intPart, int decimalPart)
        {
            decimal d;
            decimal.TryParse(data, out d);

            decimal intPartValue = 9;
            decimal decimalPartValue = 0;

            if (decimalPart != 0)
                decimalPartValue = (decimal) 0.9;

            for (int i = 1; i < intPart; i++)
            {
                intPartValue = 9 + intPartValue*10;
            }
            for (int i = 1; i < decimalPart; i++)
            {
                decimalPartValue = (decimal) 0.9 + decimalPartValue/10;
            }
            if (Math.Abs(d) > intPartValue + decimalPartValue)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        ///     返回包含中文字符的字符串长度
        ///     C# 的String.Length中中文字只做1位统计,所以要将其转换为2位
        /// </summary>
        /// <param name="strSource">要统计长度的字符串变量</param>
        /// <returns>字符串长度</returns>
        public static int GetLength(this String strSource)
        {
            var regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            int nLength = strSource.Length;

            for (int i = 0; i < strSource.Length; i++)
            {
                if (regex.IsMatch(strSource.Substring(i, 1)))
                {
                    nLength++;
                }
            }

            return nLength;
        }

        /// <summary>
        ///     检测DateTime
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsDateTime(this String data)
        {
            DateTime dt;
            if (DateTime.TryParse(data.Trim(), out dt))
                return true;
            return false;
        }

        /// <summary>
        ///     检测是否为字母类型
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsChar(this String data)
        {
            var regex = new Regex("^[A-Za-z]+$", RegexOptions.Compiled);

            if (regex.IsMatch(data))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     检测是否为字母数字类型
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsNumChar(this String data)
        {
            var regex = new Regex("^[A-Za-z0-9]+$", RegexOptions.Compiled);

            if (regex.IsMatch(data))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     检测是否为邮件类型
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsEmail(this String data)
        {
            var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);
            if (regex.IsMatch(data))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     检测是否为正数
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsSignless(this String data)
        {
            decimal d;

            if (decimal.TryParse(data, out d))
            {
                if (d > 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     检测是否为正整数
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsSignlessInteger(this String data)
        {
            int d;

            if (int.TryParse(data, out d))
            {
                if (d > 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     检测是否为非负整数
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsNonnegativeInteger(this String data)
        {
            int d;
            if (int.TryParse(data, out d))
            {
                if (d >= 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     检测是否为手机号码
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsMobile(this String data)
        {
            var regex = new Regex("^1[3,4,5,8][0-9][0-9]{8}$", RegexOptions.Compiled);
            if (regex.IsMatch(data))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     是否是数字包含排除小数点
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsNumeric(this String s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                if (s.Contains("."))
                {
                    if (s.IndexOf(".") == s.LastIndexOf("."))
                    {
                        char[] str = s.Replace(".", String.Empty).Trim().ToCharArray();
                        return str.All(Char.IsNumber);
                    }
                }
                else
                {
                    char[] str = s.Trim().ToCharArray();
                    return str.All(Char.IsNumber);
                }
            }
            return true;
        }

        /// <summary>
        ///     检测是否为身份证号码
        /// </summary>
        /// <param name="idCard">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsIdCardNo(this String idCard)
        {
            int intLength = idCard.Length;

            String ai = String.Empty;

            if (intLength < 15 || intLength == 16 || intLength == 17 || intLength > 18)
            {
                return false;
            }

            if (intLength == 18)
            {
                ai = idCard.Substring(0, 17);
            }
            else if (intLength == 15)
            {
                ai = idCard;
                ai = String.Format("{0}19{1}", ai.Substring(0, 6), ai.Substring(7, 9));
            }
            if (!IsNumber((ai))) return false;

            int intYear = Convert.ToInt32(ai.Substring(6, 4));
            int intMonth = Convert.ToInt32(ai.Substring(10, 2));
            int intDay = Convert.ToInt32(ai.Substring(12, 2));

            String birthDay = String.Format("{0}-{1}-{2}", intYear, intMonth, intDay);
            if (birthDay.IsDateTime())
            {
                DateTime dateBirthDay = DateTime.Parse(birthDay);
                if (dateBirthDay > DateTime.Now)
                {
                    return false;
                }

                int intYearLength = dateBirthDay.Year - dateBirthDay.Year;
                if (intYearLength < -140)
                {
                    return false;
                }
            }

            if (intMonth > 12 || intDay > 31)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        ///     是否是数字
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsNumber(this String s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                char[] str = s.Trim().ToCharArray();
                return str.All(Char.IsNumber);
            }
            return true;
        }


        /// <summary>
        ///     检测是否为中文
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsChinese(this String data)
        {
            var regex = new Regex("[\u4e00-\u9fa5]", RegexOptions.Compiled);
            if (regex.IsMatch(data))
            {
                return true;
            }
            return false;
        }

        #region IsNullOrEmpty

        /// <summary>
        ///     IsNullOrEmpty封装
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>bool结果</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        ///     通过出生日期，获得当前岁数
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <returns>当前岁数</returns>
        public static int Age(this DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int dateDiff = today.Year - dateOfBirth.Year;
            if (today.Month == dateOfBirth.Month)
            {
                return dateDiff - (today.Day < dateOfBirth.Day ? 1 : 0);
            }
            return dateDiff - (today.Month > dateOfBirth.Month ? 0 : 1);
        }

        #endregion

        #endregion
    }
}