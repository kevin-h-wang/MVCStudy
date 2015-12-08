/* ----------------------------------------------------------------
 * Copyright (C) 2012 方正国际软件有限公司版权所有
 * 
 * 文件名：Utility.cs
 * 文件功能描述：其他实用工具类。
 * 
 * 创建标识：方正 2012-8-14
 * 
 * 修改标识：
 * 修改描述：
 * ----------------------------------------------------------------*/

using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace EasyUIDemo.Utility
{
    //todo: 整理
    /// <summary>
    ///     实用工具类
    /// </summary>
    public static class FIUtility
    {
        #region 文本处理

        private static readonly char[] Upperchar =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        /// <summary>
        ///     截断文本,包含中英文
        /// </summary>
        /// <param name="sourceText">原始文本</param>
        /// <param name="len">截取长度</param>
        /// <returns>截断后文本</returns>
        public static string CutString(string sourceText, int len)
        {
            string result = String.Empty;

            char[] charArr = sourceText.ToCharArray(); //文本字节数组
            int byteLen = Encoding.Default.GetBytes(sourceText).Length; //单字节字符长度
            int charLen = sourceText.Length; //文本长度
            int byteCount = 0; //字节读取进度(中文按双字节计算)
            int pos = 0; //文本截取位置(中文按双字节计算)

            for (int i = 0; i < charLen; i++)
            {
                if (Convert.ToInt32(charArr[i]) > 255)
                {
                    byteCount += 2; //中文加2
                }
                else
                {
                    byteCount += 1;
                }

                if (byteCount >= len)
                {
                    //到达指定长度时,记录位置并停止
                    pos = i;
                    break;
                }
            }

            if (pos > 0)
            {
                result = sourceText.Substring(0, pos) + "...";
            }
            else
            {
                result = sourceText;
            }

            return result;
        }

        /// <summary>
        ///     获取字符串首个大写字母后的字串。
        /// </summary>
        /// <param name="text">要截取的字符串。</param>
        /// <returns>截取后的字串。</returns>
        public static string GetCapitalWord(string text)
        {
            int index = text.IndexOfAny(Upperchar);
            return index > 0 ? text.Substring(index) : text;
        }

        #endregion

        #region Http request

        /// <summary>
        ///     发送Http
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>返回字符串</returns>
        public static string SendUrlGet(string url)
        {
            var req = (HttpWebRequest) WebRequest.Create(url);
            var resp = req.GetResponse() as HttpWebResponse;

            string str;
            using (Stream respStream = resp.GetResponseStream())
            {
                using (var reader = new StreamReader(respStream, Encoding.UTF8))
                {
                    str = reader.ReadLine();
                    return str;
                }
            }
        }

        #endregion

        #region SessionUtility

        /// <summary>
        ///     获取Session值
        /// </summary>
        /// <param name="name">Session名</param>
        /// <returns>Session对象</returns>
        public static object GetSession(string name)
        {
            return HttpContext.Current.Session[name];
        }

        /// <summary>
        ///     设置Session值
        /// </summary>
        /// <param name="name">Session名</param>
        /// <param name="value">Session值</param>
        public static void SetSession(string name, object value)
        {
            HttpContext.Current.Session[name] = value;
        }

        /// <summary>
        ///     移除Session值
        /// </summary>
        /// <param name="name">Session名</param>
        public static void RemoveSession(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        #endregion

        #region SMSUtility

        #endregion

        #region 顺序GUID

        private const int RPC_S_OK = 0;

        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern int UuidCreateSequential(out Guid guid);
        /// <summary>
        /// 生成顺序GUID
        /// </summary>
        /// <returns></returns>
        public static Guid GetSequentialGuid()
        {
            Guid guid;
            int result = UuidCreateSequential(out guid);
            return result == RPC_S_OK ? guid : Guid.NewGuid();
        }

        #endregion

        #region 随即数，随机字母

        /// <summary>
        ///     生成数字随即码
        /// </summary>
        /// <param name="vcodeNum">生成验证码长度</param>
        /// <returns></returns>
        public static String RandomNum(int vcodeNum)
        {
            var rand = new Random();
            int max = "9".PadLeft(vcodeNum, '9').ToInt32().Value;
            int min = "1".PadRight(vcodeNum, '0').ToInt32().Value;
            return rand.Next(min, max).ToStringEx();
        }

        /// <summary>
        ///     生成小写字母验证码
        /// </summary>
        /// <param name="vcodeNum">生成验证码长度</param>
        /// <returns></returns>
        public static String RandomLowerCase(int vcodeNum)
        {
            const String vchar = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            String[] vcArray = vchar.Split(',');
            String vNum = String.Empty;
            int temp = -1;
            var rand = new Random();
            for (int i = 1; i < vcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i*temp*unchecked((int) DateTime.Now.Ticks));
                }

                int t = rand.Next(25);
                if (temp != -1 && temp == t)
                {
                    return RandomLowerCase(vcodeNum);
                }
                temp = t;
                vNum += vcArray[t];
            }
            return vNum;
        }

        #endregion
    }
}