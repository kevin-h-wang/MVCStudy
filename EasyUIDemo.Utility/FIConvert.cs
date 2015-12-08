/*----------------------------------------------------------------
// Copyright (C) 2014方正国际软件有限公司
// 版权所有。
// 文   件   名：FIConvert
// 文件功能描述：
//
// 
// 创 建 人：王洪福
// 创建日期：2014/6/13
//
// 修 改 人：
// 修改描述：
//
// 修改标识：
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace EasyUIDemo.Utility
{
    public static class FIConvert
    {
        #region 类型转换

        /// <summary>
        ///     Linq中IEnumerable转换成DataTable
        /// </summary>
        /// <param name="list">list数据</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable(this IEnumerable list)
        {
            var dataTable = new DataTable();
            bool schemaIsBuild = false;
            PropertyInfo[] props = null;
            foreach (object item in list)
            {
                if (!schemaIsBuild)
                {
                    props =
                        item.GetType()
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
                    foreach (PropertyInfo pi in props)
                    {
                        dataTable.Columns.Add(new DataColumn(pi.Name, pi.PropertyType));
                    }
                    schemaIsBuild = true;
                }
                DataRow row = dataTable.NewRow();
                foreach (PropertyInfo pi in props)
                {
                    row[pi.Name] = pi.GetValue(item, null);
                }
                dataTable.Rows.Add(row);
            }
            dataTable.AcceptChanges();
            return dataTable;
        }

        /// <summary>
        ///     DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns>List 集合</returns>
        public static List<TResult> ToList<TResult>(this DataTable dataTable) where TResult : class, new()
        {
            //创建一个属性的列表
            var prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            Type t = typeof(TResult);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach(t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty),
                p => { if (dataTable.Columns.Contains(p.Name)) prlist.Add(p); });
            //创建返回的集合
            var oblist = new List<TResult>();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    //创建TResult的实例
                    var ob = new TResult();
                    //找到对应的数据  并赋值
                    DataRow row1 = row;
                    prlist.ForEach(p => { if (row1[p.Name] != DBNull.Value) p.SetValue(ob, row1[p.Name], null); });
                    //放入到返回的集合中.
                    oblist.Add(ob);
                }
            }
            return oblist;
        }

        /// <summary>
        ///     值为1或true返回true，否则返回false
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Bool值</returns>
        public static Boolean? ToBoolean(this String value)
        {
            if (value == null || String.IsNullOrEmpty(value.Trim()) || Equals(value.Trim().ToLower(), "system.object"))
                return null;
            if (Equals(value.Trim(), "1"))
                return true;
            if (Equals(value.Trim().ToLower(), "true"))
                return true;
            if (Equals(value.Trim(), "0"))
                return false;
            if (Equals(value.Trim().ToLower(), "false"))
                return false;
            return null;
        }

        /// <summary>
        ///     值为1返回true其他返回false
        /// </summary>
        /// <param name="value">整型值</param>
        /// <returns>Bool值</returns>
        public static Boolean? ToBoolean(this int value)
        {
            if (value == 1)
                return true;
            return ToBoolean(value.ToString());
        }

        /// <summary>
        ///     值为1或true返回true，否则返回false
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>Bool值</returns>
        public static Boolean? ToBoolean(this object value)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return null;
            return ToBoolean(value.ToString());
        }

        /// <summary>
        /// 值为1或true返回true，否则返回false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean ToBooleanNull(this object value)
        {
            if (value == null || String.IsNullOrEmpty(value.ToString().Trim()))
                return false;
            if (Equals(value.ToString().Trim(), "1"))
                return true;
            if (Equals(value.ToString().Trim().ToLower(), "true"))
                return true;
            return false;
        }

        /// <summary>
        ///     object类型转化为string
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串值</returns>
        public static String ToStringEx(this object obj)
        {
            if (obj == null || Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return String.Empty;

            return obj.ToString().Trim();
        }

        /// <summary>
        ///     截取strFormer中长度为subLength的字符串，如果strFormer本身的长度小于subLength,返回strFormer
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="subLength">长度</param>
        /// <returns>字符串</returns>
        public static string ToSubString(this object obj, int subLength)
        {
            if (obj == null || Equals(obj.ToString().ToLower().Trim(), "system.object"))
                return String.Empty;

            if (obj.ToString().Trim().Length <= subLength)
                return obj.ToString().Trim();
            return obj.ToString().Trim().Substring(0, subLength) + "..";
            // return obj.ToString().Trim();
        }

        /// <summary>
        ///     转换为整型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this object obj)
        {
            if (obj == null)
                return null;
            return ToInt32(obj.ToString());
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">UInt32对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this bool? obj)
        {
            switch (obj)
            {
                case true:
                    return 1;
                case false:
                    return 0;

            }
            return null;
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this String obj)
        {
            int i;
            if (Int32.TryParse(obj, out i))
            {
                return i;
            }
            return null;
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">float对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this float obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">double对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this double obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">UInt32对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this UInt32 obj)
        {
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">long对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this long obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        ///   转换为整型
        /// </summary>
        /// <param name="obj">decimal对象</param>
        /// <returns>可空整型</returns>
        public static int? ToInt32(this decimal obj)
        {
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///     转换为长整型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this object obj)
        {
            if (obj == null)
                return null;

            return ToInt64(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this String obj)
        {
            if (obj == null)
                return null;

            long i;
            if (Int64.TryParse(obj, out i))
            {
                return i;
            }
            return null;
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">int对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this int obj)
        {
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">float对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this float obj)
        {
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">double对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this double obj)
        {
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj">UInt32对象</param>
        /// <returns>可空long</returns>
        public static long? ToInt64(this UInt32 obj)
        {
            return Convert.ToInt64(obj);
        }


        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空decimal</returns>
        public static decimal? ToDecimal(this object obj)
        {
            if (obj == null)
                return null;
            return ToDecimal(obj.ToString());
        }


        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空decimal</returns>
        public static decimal? ToDecimal(this String obj)
        {
            if (obj == null || obj == "")
                return null;
            decimal i;
            if (decimal.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空decimal</returns>
        public static decimal ToDecimalNullDefaultZero(this String obj)
        {
            if (obj == null || obj == "")
                return 0;
            decimal i;
            if (decimal.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>执行结果</returns>
        public static decimal ToDecimalRoundOffDefaultZero(this String obj)
        {
            if (obj == null || obj == "")
                return 0;

            decimal i;
            if (obj.LastIndexOf('.') > 0)
            {
                obj = obj.Substring(0, obj.LastIndexOf('.') + 2);
                if (decimal.TryParse(obj, out i))
                {
                    return Math.Round((i * 10 + decimal.Parse("0.5")) / 10, 0, MidpointRounding.AwayFromZero);
                }
            }
            else
            {
                if (decimal.TryParse(obj, out i))
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>费空decimal</returns>
        public static decimal ToDecimalNotNull(this String obj)
        {
            decimal i;
            if (!string.IsNullOrEmpty(obj) && decimal.TryParse(obj, out i))
            {
                return i;
            }
            return 0;
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>费空decimal</returns>
        public static decimal? ToDecimalZeroDefultNull(this String obj)
        {
            if (obj == null || obj == "")
                return null;
            decimal i;
            if (decimal.TryParse(obj, out i))
            {
                if (i == 0)
                {
                    return null;
                }
                else
                {
                    return i;
                }
            }
            return null;
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>费空decimal</returns>
        public static decimal? ToDecimalZeroDefultNull(this Decimal obj)
        {
            if (obj == 0)
            {
                return null;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        ///     转换为高精度型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>费空decimal</returns>
        public static int? ToDecimalZeroDefultNull(this int obj)
        {
            if (obj == 0)
            {
                return null;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">Int对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this int obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">float对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this float obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">double对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this double obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">UInt32对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this UInt32 obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 转换为3位用逗号隔开的方式
        /// </summary>
        /// <param name="amount">金额额</param>
        /// <returns>显示值</returns>
        public static string ToAmount(this object amount)
        {
            string strAmount = amount.ToStringEx();
            if (strAmount.IsNullOrEmpty())
            {
                return string.Empty;
            }
            //转换
            double doubleAmount;
            bool isDouble = double.TryParse(strAmount, out doubleAmount);
            if (isDouble == false)
            {
                return string.Empty;
            }
            return string.Format("{0:N}", doubleAmount);
        }

        /// <summary>
        /// 转换为百分比格式
        /// </summary>
        /// <param name="percent">值</param>
        /// <returns>显示值</returns>
        public static string ToPercent(this object percent)
        {
            string strAmount = percent.ToStringEx();
            if (strAmount.IsNullOrEmpty())
            {
                return string.Empty;
            }
            //转换
            double doublePercent;
            bool isDouble = double.TryParse(strAmount, out doublePercent);
            if (isDouble == false)
            {
                return string.Empty;
            }
            return string.Format("{0:P}", doublePercent);
        }
        /// <summary>
        /// 转换为百分比格式四位小数
        /// </summary>
        /// <param name="percent">值</param>
        /// <returns>显示值</returns>
        public static string ToPercent4(this object percent)
        {
            string strAmount = percent.ToStringEx();
            if (strAmount.IsNullOrEmpty())
            {
                return string.Empty;
            }
            //转换
            double doublePercent;
            bool isDouble = double.TryParse(strAmount, out doublePercent);
            if (isDouble == false)
            {
                return string.Empty;
            }
            return string.Format("{0:P4}", doublePercent);
        }
        /// <summary>
        /// 转换为3位用逗号隔开的方式
        /// </summary>
        /// <param name="amount">金额额</param>
        /// <returns>显示值</returns>
        public static string ToAmountNotShowZero(this object amount)
        {
            string strAmount = amount.ToStringEx();
            if (strAmount.IsNullOrEmpty())
            {
                return string.Empty;
            }
            //转换
            double doubleAmount;
            bool isDouble = double.TryParse(strAmount, out doubleAmount);
            if (isDouble == false)
            {
                return string.Empty;
            }
            //不显示0
            if (doubleAmount == 0)
            {
                return string.Empty;
            }
            return string.Format("{0:N}", doubleAmount);
        }

        /// <summary>
        /// 转换为高精度型
        /// </summary>
        /// <param name="obj">long对象</param>
        /// <returns>decimal值</returns>
        public static decimal ToDecimal(this long obj)
        {
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        ///     日期转字符
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回yyyy-MM-dd格式日期字符串</returns>
        public static String ToDateString(this object obj)
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                return date.Value.ToString("yyyy-MM-dd");
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回yyyy-MM-dd格式日期字符串</returns>
        public static String ToDateStringEx(this object obj)
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                return date.Value.ToString("yyyyMMdd");
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符带时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>
        ///     如果日期是1753/01/01 00:00:00（数据库最小时间）则返回空字符串；
        ///     其他情况返回yyyy-MM-dd HH:mm:ss格式日期字符串
        /// </returns>
        public static String ToDateTimeString(this object obj)
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                return date.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符带时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>
        ///     如果日期是1753/01/01 00:00:00（数据库最小时间）则返回空字符串；
        ///     其他情况返回yyyy-MM-dd HH:mm:ss格式日期字符串
        /// </returns>
        public static String ToDateTimeStringEx(this object obj)
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                return date.Value.ToString("yyyyMMddHHmmss");
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符
        /// </summary>
        /// <param name="obj">DateTime对象</param>
        /// <returns>返回yyyy-MM-dd格式日期字符串</returns>
        public static String ToDateString(this DateTime obj)
        {
            return obj.ToString("yyyy-MM-dd");
        }

        /// <summary>
        ///     日期转字符带时间
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回yyyy-MM-dd HH:mm:ss格式日期字符串</returns>
        public static String ToYearMonthDateString(this object obj)
        {
            DateTime? date = obj.ToDateTime();
            if (date.HasValue)
            {
                return date.Value.ToString("yyyy-MM");
            }
            return string.Empty;
        }

        /// <summary>
        ///     日期转字符
        /// </summary>
        /// <param name="obj">DateTime对象</param>
        /// <returns>返回yyyy-MM-dd格式日期字符串</returns>
        public static String ToYearMonthDateString(this DateTime obj)
        {
            return obj.ToString("yyyy-MM");
        }

        /// <summary>
        ///     转换为日期型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空DateTime</returns>
        public static DateTime? ToDateTime(this object obj)
        {
            if (obj == null)
                return null;
            return ToDateTime(obj.ToString());
        }

        /// <summary>
        ///     转换为日期型
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空DateTime</returns>
        public static DateTime? ToDateTime(this String obj)
        {
            if (obj == null)
                return null;
            DateTime i;
            if (DateTime.TryParse(obj, out i))
            {
                return i;
            }
            return null;
        }

        /// <summary>
        ///     保留小数
        /// </summary>
        /// <param name="dblData">decimal值</param>
        /// <param name="precision">整型小数位</param>
        /// <returns>字符串</returns>
        public static string ToFormartNumerData(decimal dblData, int precision = 2, bool IsAmountFormat = false)
        {
            if (IsAmountFormat)
            {
                return String.Format("{0:N" + precision + "}", dblData);
            }
            else
            {
                return String.Format("{0:F" + precision + "}", dblData);
            }
        }

        /// <summary>
        ///     保留小数
        /// </summary>
        /// <param name="dblData">decimal值</param>
        /// <param name="precision">整型小数位</param>
        /// <returns>字符串</returns>
        public static string ToFormartNumerData(this decimal? obj)
        {
            return String.Format("{0:F2}", obj);
        }
        /// <summary>
        ///     保留小数
        /// </summary>
        /// <param name="dblData">decimal值</param>
        /// <param name="precision">整型小数位</param>
        /// <returns>字符串</returns>
        public static string ToFormartNumerDataNotShowZero(this decimal? obj)
        {
            if (obj == 0)
            {
                return string.Empty;
            }
            else
            {
                return String.Format("{0:F2}", obj);
            }
        }
        /// <summary>
        ///  
        /// </summary>      
        /// <returns>字符串</returns>
        public static string ToFormartNumerDataNotShowZero(this int? obj)
        {
            if (obj == 0)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToStringEx();
            }
        }

        /// <summary>
        ///     将char数组转换为String类型
        /// </summary>
        /// <param name="inputValue">char数组</param>
        /// <returns>String类型值</returns>
        public static string ToStringEx(this char[] inputValue)
        {
            try
            {
                var str = new StringBuilder();
                foreach (char c in inputValue)
                {
                    str.Append(c.ToString());
                }
                return str.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        ///     转换为Guid
        /// </summary>
        /// <param name="obj">String对象</param>
        /// <returns>可空Guid</returns>
        public static Guid? ToGuid(this String obj)
        {
            Guid guid;
            if (obj == null || obj == "" || Equals(obj.ToLower().Trim(), "system.object"))
                return null;
            if (Guid.TryParse(obj, out guid))
            {
                return guid;
            }
            return null;
        }

        /// <summary>
        ///     转换为Guid
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>可空Guid</returns>
        public static Guid? ToGuid(this Object obj)
        {
            if (obj == null)
                return null;
            return ToGuid(obj.ToString());
        }

        #region 枚举
        /// <summary>
        ///     转换成枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举</typeparam>
        /// <param name="value">字符串</param>
        /// <returns>枚举值</returns>
        public static TEnum ToEnum<TEnum>(this object value) where TEnum : struct
        {
            TEnum tEnum;
            Enum.TryParse(value.ToStringEx(), out tEnum);
            return tEnum;
        }
        /// <summary>
        ///     转换成枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举</typeparam>
        /// <param name="value">字符串</param>
        /// <returns>枚举值</returns>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            TEnum tEnum;
            Enum.TryParse(value, out tEnum);
            return tEnum;
        }

        /// <summary>
        /// 转换成枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举</typeparam>
        /// <param name="value">整型值</param>
        /// <returns>枚举值</returns>
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : struct
        {
            TEnum tEnum;
            Enum.TryParse(value.ToStringEx(), out tEnum);
            return tEnum;
        }

        /// <summary>
        /// 转换成枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举</typeparam>
        /// <param name="value">整型值</param>
        /// <returns>枚举值</returns>
        public static TEnum? ToEnum<TEnum>(this int? value) where TEnum : struct
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                TEnum tEnum;
                Enum.TryParse(value.ToStringEx(), out tEnum);
                return tEnum;
            }
        }

        /// <summary>
        /// 转换成枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举</typeparam>
        /// <param name="value">可空Bool值</param>
        /// <returns>枚举值</returns>
        public static TEnum? ToEnum<TEnum>(this bool? value) where TEnum : struct
        {
            return ToEnum<TEnum>(value.ToInt32());
        }

        /// <summary>
        ///     获取枚举值
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>枚举值</returns>
        public static int ToEnumValue<T>(this string name)
        {
            return (int)Enum.Parse(typeof(T), name, true);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="value"></param>
        /// <returns>如果没有取得枚举的返回值，则返回int的最小值</returns>
        public static int ToEnumValue(this Enum value)
        {
            if (value == null)
            {
                return int.MinValue;
            }
            string name = Enum.GetName(value.GetType(), value);
            Type type = value.GetType();
            int returnValue = name == null ? int.Parse(value.ToString()) : int.MinValue;
            try
            {
                returnValue = (int)Enum.Parse(type, name, true);
            }
            catch (Exception)
            {

            }
            return returnValue;
        }

        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEnumDescription(this Enum value)
        {
            if (value.ToEnumValue() == 0)
                return string.Empty;

            Type type = value.GetType();
            string name = Enum.GetName(value.GetType(), value);
            object[] customAttributes = type.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (customAttributes.Length <= 0)
            {
                throw new NullReferenceException("请设置DescriptAttribute!");
            }
            return ((DescriptionAttribute)customAttributes[0]).Description;
        }

        /// <summary>
        /// 将数字转化为中文数字
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="isComplexFont">是否要繁体表示</param>
        /// <returns>中文数字</returns>
        public static string ToChineseNum(this int value, bool isComplexFont = false)
        {
            string[] zh = isComplexFont == false ? new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" } : new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] unit = isComplexFont == false ? new string[] { "", "十", "百", "千", "万", "十", "百", "千", "亿", "十" } : new string[] { "", "拾", "佰", "仟", "萬", "拾", "佰", "仟", "億", "拾" };
            string str = value.ToString();
            int len = str.Length;
            int i;
            string tmpstr, rstr;
            rstr = "";
            for (i = 1; i <= len; i++)
            {
                tmpstr = str.Substring(len - i, 1);
                rstr = string.Concat(zh[Int32.Parse(tmpstr)] + unit[i - 1], rstr);
            }
            rstr = rstr.Replace("一十", "十");
            rstr = rstr.Replace("拾零", "拾");
            rstr = rstr.Replace("十零", "十");
            rstr = rstr.Replace("零拾", "零");
            rstr = rstr.Replace("零十", "零");
            rstr = rstr.Replace("零佰", "零");
            rstr = rstr.Replace("零百", "零");
            rstr = rstr.Replace("零仟", "零");
            rstr = rstr.Replace("零千", "零");
            rstr = rstr.Replace("零萬", "零");
            rstr = rstr.Replace("零万", "零");
            rstr = rstr.Replace("零零", "零");
            rstr = rstr.Replace("零亿", "零");
            rstr = rstr.Replace("零億", "零");
            return rstr;
        }

        /// <summary>
        /// 将数字转化为中文数字
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="isComplexFont">是否要繁体表示</param>
        /// <returns>中文数字</returns>
        public static string ToChineseNum(this decimal value, bool isComplexFont = false)
        {
            string[] zh = isComplexFont == false ? new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" } : new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] unit = isComplexFont == false ? new string[] { "", "十", "百", "千", "万", "十", "百", "千", "亿", "十" } : new string[] { "", "拾", "佰", "仟", "萬", "拾", "佰", "仟", "億", "拾" };
            string str = value.ToString();
            int len = str.Length;
            int i;
            string tmpstr, rstr;
            rstr = "";
            for (i = 1; i <= len; i++)
            {
                tmpstr = str.Substring(len - i, 1);
                rstr = string.Concat(zh[Int32.Parse(tmpstr)] + unit[i - 1], rstr);
            }
            rstr = rstr.Replace("一十", "十");
            rstr = rstr.Replace("拾零", "拾");
            rstr = rstr.Replace("十零", "十");
            rstr = rstr.Replace("零拾", "零");
            rstr = rstr.Replace("零十", "零");
            rstr = rstr.Replace("零佰", "零");
            rstr = rstr.Replace("零百", "零");
            rstr = rstr.Replace("零仟", "零");
            rstr = rstr.Replace("零千", "零");
            rstr = rstr.Replace("零萬", "零");
            rstr = rstr.Replace("零万", "零");
            rstr = rstr.Replace("零零", "零");
            rstr = rstr.Replace("零亿", "零");
            rstr = rstr.Replace("零億", "零");
            return rstr;
        }

        public static string MoneyToChinese(this string value)
        {
            #region 转换方法
            string LowerMoney = value;
            string functionReturnValue = null;
            bool IsNegative = false; // 是否是负数
            if (LowerMoney.Trim().Substring(0, 1) == "-")
            {
                // 是负数则先转为正数
                LowerMoney = LowerMoney.Trim().Remove(0, 1);
                IsNegative = true;
            }
            string strLower = null;
            string strUpart = null;
            string strUpper = null;
            int iTemp = 0;
            // 保留两位小数 123.489→123.49　　123.4→123.4
            LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
            if (LowerMoney.IndexOf(".") > 0)
            {
                if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
                {
                    LowerMoney = LowerMoney + "0";
                }
            }
            else
            {
                LowerMoney = LowerMoney + ".00";
            }
            strLower = LowerMoney;
            iTemp = 1;
            strUpper = "";
            while (iTemp <= strLower.Length)
            {
                switch (strLower.Substring(strLower.Length - iTemp, 1))
                {
                    case ".":
                        strUpart = "圆";
                        break;
                    case "0":
                        strUpart = "零";
                        break;
                    case "1":
                        strUpart = "壹";
                        break;
                    case "2":
                        strUpart = "贰";
                        break;
                    case "3":
                        strUpart = "叁";
                        break;
                    case "4":
                        strUpart = "肆";
                        break;
                    case "5":
                        strUpart = "伍";
                        break;
                    case "6":
                        strUpart = "陆";
                        break;
                    case "7":
                        strUpart = "柒";
                        break;
                    case "8":
                        strUpart = "捌";
                        break;
                    case "9":
                        strUpart = "玖";
                        break;
                }

                switch (iTemp)
                {
                    case 1:
                        strUpart = strUpart + "分";
                        break;
                    case 2:
                        strUpart = strUpart + "角";
                        break;
                    case 3:
                        strUpart = strUpart + "";
                        break;
                    case 4:
                        strUpart = strUpart + "";
                        break;
                    case 5:
                        strUpart = strUpart + "拾";
                        break;
                    case 6:
                        strUpart = strUpart + "佰";
                        break;
                    case 7:
                        strUpart = strUpart + "仟";
                        break;
                    case 8:
                        strUpart = strUpart + "万";
                        break;
                    case 9:
                        strUpart = strUpart + "拾";
                        break;
                    case 10:
                        strUpart = strUpart + "佰";
                        break;
                    case 11:
                        strUpart = strUpart + "仟";
                        break;
                    case 12:
                        strUpart = strUpart + "亿";
                        break;
                    case 13:
                        strUpart = strUpart + "拾";
                        break;
                    case 14:
                        strUpart = strUpart + "佰";
                        break;
                    case 15:
                        strUpart = strUpart + "仟";
                        break;
                    case 16:
                        strUpart = strUpart + "万";
                        break;
                    default:
                        strUpart = strUpart + "";
                        break;
                }

                strUpper = strUpart + strUpper;
                iTemp = iTemp + 1;
            }

            strUpper = strUpper.Replace("零拾", "零");
            strUpper = strUpper.Replace("零佰", "零");
            strUpper = strUpper.Replace("零仟", "零");
            strUpper = strUpper.Replace("零零零", "零");
            strUpper = strUpper.Replace("零零", "零");
            strUpper = strUpper.Replace("零角零分", "整");
            strUpper = strUpper.Replace("零分", "整");
            strUpper = strUpper.Replace("零角", "零");
            strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("亿零万零圆", "亿圆");
            strUpper = strUpper.Replace("零亿零万", "亿");
            strUpper = strUpper.Replace("零万零圆", "万圆");
            strUpper = strUpper.Replace("零亿", "亿");
            strUpper = strUpper.Replace("零万", "万");
            strUpper = strUpper.Replace("零圆", "圆");
            strUpper = strUpper.Replace("零零", "零");

            // 对壹圆以下的金额的处理
            if (strUpper.Substring(0, 1) == "圆")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "零")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "角")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "分")
            {
                strUpper = strUpper.Substring(1, strUpper.Length - 1);
            }
            if (strUpper.Substring(0, 1) == "整")
            {
                strUpper = "零圆整";
            }
            functionReturnValue = strUpper;

            if (IsNegative == true)
            {
                return "负" + functionReturnValue;
            }
            else
            {
                return functionReturnValue;
            }

            #endregion
        }

        #endregion


        #endregion
    }
}