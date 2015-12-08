using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyUIDemo.Tools
{
    public class TableColumn
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 列中文名
        /// </summary>
        public string ColumnCnName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public string IsPrimaryKey
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public string IsAllowNull
        {
            get;
            set;
        }

        /// <summary>
        /// 是否唯一
        /// </summary>
        public string IsUnique
        {
            get;
            set;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string ColumnDesc
        {
            get;
            set;
        }
    }
}
