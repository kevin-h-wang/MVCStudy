using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyUIDemo.Tools
{
    public class TableEntity
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }
        /// <summary>
        /// 表备注
        /// </summary>
        public string TableDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 包含的列
        /// </summary>
        private List<TableColumn> _column;
        public List<TableColumn> Columns
        {
            get
            {
                if (_column == null)
                {
                    _column = new List<TableColumn>();
                }
                return _column;
            }
            set
            {
                _column = value;
            }
        }

        /// <summary>
        /// 外键
        /// </summary>
        private List<TableForeignKey> _foreignKeys;
        public List<TableForeignKey> ForeignKeys
        {
            get
            {
                if (_foreignKeys == null)
                {
                    _foreignKeys = new List<TableForeignKey>();
                }
                return _foreignKeys;
            }
            set
            {
                _foreignKeys = value;
            }
        }

        /// <summary>
        /// 索引
        /// </summary>
        private List<TableIndex> _Indexs;
        public List<TableIndex> Indexs
        {
            get
            {
                if (_Indexs == null)
                {
                    _Indexs = new List<TableIndex>();
                }
                return _Indexs;
            }
            set
            {
                _Indexs = value;
            }
        }
    }
}
