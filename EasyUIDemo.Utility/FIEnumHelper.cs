using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;

namespace EasyUIDemo.Utility
{
    public class FIEnumHelper
    {
        /// <summary>
        ///     通过枚举类型的值获取文本
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="value">值</param>
        /// <returns>枚举值对应的文本</returns>
        public static string GetValueViaEnum(Type enumType, int value)
        {
            Array values = Enum.GetValues(enumType);
            var listItems = new List<ListItem>();
            for (int i = 0; i < values.Length; i++)
            {
                string text = values.GetValue(i).ToString();
                if (value == (int) values.GetValue(i))
                    return text;
            }
            return string.Empty;
        }

        /// <summary>
        ///     通过枚举类型的值获取文本
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="value">值</param>
        /// <returns>枚举值对应的文本</returns>
        public static string GetValueViaEnum(Type enumType, string value)
        {
            Array values = Enum.GetValues(enumType);
            var listItems = new List<ListItem>();
            for (int i = 0; i < values.Length; i++)
            {
                string text = values.GetValue(i).ToString();
                if (value.ToInt32() == (int) values.GetValue(i))
                    return text;
            }
            return string.Empty;
        }


        /// <summary>
        ///     枚举填充到 ListItem
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>ListItem集合</returns>
        public static List<ListItem> GetListViaEnum(Type enumType)
        {
            Array values = Enum.GetValues(enumType);
            var listItems = new List<ListItem>();
            for (int i = 0; i < values.Length; i++)
            {
                string text = values.GetValue(i).ToString();
                var value = (int) values.GetValue(i);
                listItems.Add(new ListItem(text, value.ToString(CultureInfo.InvariantCulture)));
            }
            return listItems;
        }

        /// <summary>
        /// 枚举的描述填充到ListItem
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<ListItem> GetDescriptionListViaEnum(Type enumType)
        {
            Array values = Enum.GetValues(enumType);
            var listItems = new List<ListItem>();
            for (int i = 0; i < values.Length; i++)
            {
                string text = ((Enum)values.GetValue(i)).ToEnumDescription();
                var value = (int)values.GetValue(i);
                listItems.Add(new ListItem(text, value.ToString(CultureInfo.InvariantCulture)));
            }
            return listItems;
        }

    }
}