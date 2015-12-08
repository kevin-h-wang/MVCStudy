using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyUIDemo.Tools
{
    public class TableForeignKey
    {
        /// <summary>
        /// 外键
        /// </summary>
        public string ForeignKey
        {
            get;
            set;
        }

        /// <summary>
        /// 外键表
        /// </summary>
        public string ForeignTable
        {
            get;
            set;
        }

        /// <summary>
        /// 外键表的主键
        /// </summary>
        public string ForeignTablePK
        {
            get;
            set;
        }

    }
}
