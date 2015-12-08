using System;
using System.Data;

namespace EasyUIDemo.Utility
{
    public class FIDataTableHelper
    {
        /// <summary>
        ///     部分字段组成的不重复数据集，同时根据其进行默认排序
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        /// <param name="fieldNames">字段名数组</param>
        /// <returns>排序结果</returns>
        public static DataTable SelectDistinct(DataTable sourceTable, params String[] fieldNames)
        {
            if (sourceTable != null && fieldNames != null && fieldNames.Length > 0)
            {
                var view = new DataView(sourceTable) {Sort = String.Join(",", fieldNames)};
                return view.ToTable(true, fieldNames);
            }
            return null;
        }

        /// <summary>
        ///     比较两个DataTable的构架是否一致
        /// </summary>
        /// <param name="dt1">表</param>
        /// <param name="dt2">表</param>
        /// <returns>结果</returns>
        public bool CompareArchitecture(DataTable dt1, DataTable dt2)
        {
            if (dt1 == null || dt2 == null)
            {
                return false;
            }
            if (dt1.Columns.Count != dt2.Columns.Count)
            {
                return false;
            }
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                if (dt1.Columns[i].ColumnName != dt2.Columns[i].ColumnName)
                {
                    return false;
                }
                if (dt1.Columns[i].GetType() != dt2.Columns[i].GetType())
                {
                    return false;
                }
            }
            return true;
        }
    }
}