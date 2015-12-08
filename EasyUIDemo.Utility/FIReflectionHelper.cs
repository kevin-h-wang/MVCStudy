using System;
using System.Reflection;
namespace EasyUIDemo.Utility
{
    public static class FIReflectionHelper
    {

        /// <summary>
        ///     获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static PropertyInfo[] GetProperties(Type type)
        {
            string typeName=type.ToString();
            PropertyInfo[] pis = FICacheHelper.GetCache(typeName) as PropertyInfo[];
            if (pis == null)
            {
                pis = type.GetProperties();
                FICacheHelper.SetCache(typeName, pis);
            }
            return pis;
        }

    }
}