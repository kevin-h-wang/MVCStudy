/*----------------------------------------------------------------
// Copyright (C) 2014方正国际软件有限公司
// 版权所有。
// 文   件   名：FICsvHelper.cs
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

using System.Data;
using System.IO;
using System.Text;

namespace EasyUIDemo.Utility
{
    public static class FICsvHelper
    {
        /// <summary>
        ///     导出报表为Csv
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strFilePath">物理路径</param>
        /// <param name="tableheader">表头</param>
        /// <param name="columname">字段标题,逗号分隔</param>
        public static bool DataTable2Csv(DataTable dt, string strFilePath, string tableheader, string columname)
        {
            string strBufferLine = "";
            var strmWriterObj = new StreamWriter(strFilePath, false, Encoding.UTF8);
            strmWriterObj.WriteLine(tableheader);
            strmWriterObj.WriteLine(columname);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strBufferLine = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j > 0)
                        strBufferLine += ",";
                    strBufferLine += dt.Rows[i][j].ToString();
                }
                strmWriterObj.WriteLine(strBufferLine);
            }
            strmWriterObj.Close();
            return true;
        }

        /// <summary>
        ///     将Csv读入DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        public static DataTable Csv2DataTable(string filePath, int n)
        {
            var dt = new DataTable();
            using (var reader = new StreamReader(filePath, Encoding.UTF8, false))
            {
                int i = 0, m = 0;
                reader.Peek();
                while (reader.Peek() > 0)
                {
                    m = m + 1;
                    string str = reader.ReadLine();
                    if (m >= n + 1)
                    {
                        string[] split = str.Split(',');

                        DataRow dr = dt.NewRow();
                        for (i = 0; i < split.Length; i++)
                        {
                            dr[i] = split[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
        }

        /// <summary>
        ///     将CSV格式的流读入table
        /// </summary>
        /// <param name="ms">数据流</param>
        /// <param name="n">整型值</param>
        /// <returns>DataTable</returns>
        public static DataTable Csv2DataTable(MemoryStream ms, int n)
        {
            var dt = new DataTable();
            using (var reader = new StreamReader(ms))
            {
                int i = 0, m = 0;
                reader.Peek();
                while (reader.Peek() > 0)
                {
                    m = m + 1;
                    string str = reader.ReadLine();
                    if (m >= n + 1)
                    {
                        string[] split = str.Split(',');

                        DataRow dr = dt.NewRow();
                        for (i = 0; i < split.Length; i++)
                        {
                            dr[i] = split[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
        }
    }
}