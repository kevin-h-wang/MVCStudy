using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyUIDemo.Tools
{
    public class TableIndex
    {
        /// <summary>
        /// 索引名称
        /// </summary>
        public string IndexName
        {
            get;
            set;
        }

        /// <summary>
        /// 索引列
        /// </summary>
        public string IndexColumn
        {
            get;
            set;
        }

        /// <summary>
        /// 索引包含列
        /// </summary>
        public string IndexIncludeColumn
        {
            get;
            set;
        }

    }
}
