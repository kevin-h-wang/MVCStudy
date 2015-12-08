using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyUIDemo.Utility;
using System.Configuration;

namespace EasyUIDemo.Tools
{
    public partial class TableImport : Form
    {
        public TableImport()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                txtFilePath.Text = fileName;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string path = txtFilePath.Text.Trim();
            List<TableEntity> lstTable = new List<TableEntity>();
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);

                for (int i = 0; i < xssfworkbook.NumberOfSheets; i++)
                {
                    ISheet sheet = xssfworkbook.GetSheetAt(i);
                    TableEntity tbl = new TableEntity();//实例化一个

                    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                    int rowIndex = 1;
                    int columnIndex = 0;//列
                    int indexIndex = 0;//索引
                    int fkIndex = 0;//外键
                    List<string> lstKeyWords = new List<string>();
                    string strCell0 = string.Empty;
                BigCycle:
                    while (rows.MoveNext())
                    {
                        IRow row = (XSSFRow)rows.Current;
                        strCell0 = row.GetCell(0).ToStringEx();

                        #region 将每行第一个单元格保存下
                        if (!strCell0.IsNullOrEmpty())
                        {
                            if (!lstKeyWords.Contains(strCell0))
                            {
                                lstKeyWords.Add(strCell0);
                            }
                        }
                        #endregion

                        #region 获取开始行
                        if (strCell0 == "表名（英文）：")
                        {
                            tbl.TableName = row.GetCell(2).ToStringEx();
                            tbl.TableDesc = row.GetCell(7).ToStringEx();
                            rowIndex++;
                            continue;
                        }
                        if (strCell0 == "字段名（英文）")
                        {
                            columnIndex = rowIndex + 1;
                            rowIndex++;
                            continue;
                        }

                        if (strCell0 == "索引名称")
                        {
                            indexIndex = rowIndex + 1;
                            rowIndex++;
                            continue;
                        }

                        if (strCell0 == "外键列")
                        {
                            fkIndex = rowIndex + 1;
                            rowIndex++;
                            continue;
                        }
                        #endregion

                        #region 获取列
                        if (columnIndex == rowIndex)
                        {
                            TableColumn col = new TableColumn();
                            for (int j = 0; j < row.LastCellNum; j++)
                            {
                                ICell cell = row.GetCell(j);
                                switch (j)
                                {
                                    case 0:
                                        if (cell.ToStringEx().IsNullOrEmpty() || cell.ToStringEx() == "索引名称")
                                        {
                                            rowIndex++;
                                            goto BigCycle;
                                        }
                                        columnIndex++;
                                        col.ColumnName = cell.ToStringEx();
                                        break;
                                    case 1:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            this.ShowMessage(tbl.TableName, col.ColumnName, "中文名称不能为空!");
                                            return;
                                        }
                                        col.ColumnCnName = cell.ToStringEx();
                                        break;
                                    case 2:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            this.ShowMessage(tbl.TableName, col.ColumnName, "数据类型不能为空!");
                                            return;
                                        }
                                        col.DataType = cell.ToStringEx();
                                        break;
                                    case 4:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            this.ShowMessage(tbl.TableName, col.ColumnName, "主键不能为空!");
                                            return;
                                        }
                                        col.IsPrimaryKey = cell.ToStringEx();
                                        break;
                                    case 5:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            this.ShowMessage(tbl.TableName, col.ColumnName, "允许空不能为空!");
                                            return;
                                        }
                                        col.IsAllowNull = cell.ToStringEx();
                                        break;
                                    case 6:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            this.ShowMessage(tbl.TableName, col.ColumnName, "是否唯一不能为空!");
                                            return;
                                        }
                                        col.IsUnique = cell.ToStringEx();
                                        break;
                                    case 7:
                                        if (!cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            col.DefaultValue = cell.ToStringEx();
                                        }
                                        break;
                                    case 8:
                                        col.ColumnDesc = cell.ToStringEx();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            tbl.Columns.Add(col);
                        }
                        #endregion

                        #region 获取索引
                        if (indexIndex == rowIndex)
                        {
                            TableIndex idx = new TableIndex();
                            for (int j = 0; j < row.LastCellNum; j++)
                            {
                                ICell cell = row.GetCell(j);
                                switch (j)
                                {
                                    case 0:
                                        if (cell.ToStringEx().IsNullOrEmpty() || cell.ToStringEx() == "外键列")
                                        {
                                            rowIndex++;
                                            goto BigCycle;
                                        }
                                        indexIndex++;
                                        idx.IndexName = cell.ToStringEx();
                                        break;
                                    case 1:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            this.ShowMessage(tbl.TableName, idx.IndexName, "索引列不能为空!");
                                            return;
                                        }
                                        idx.IndexColumn = cell.ToStringEx();
                                        break;
                                    case 2:
                                        idx.IndexIncludeColumn = cell.ToStringEx();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            tbl.Indexs.Add(idx);
                        }
                        #endregion

                        #region 获取外键
                        if (fkIndex == rowIndex)
                        {
                            TableForeignKey fk = new TableForeignKey();
                            for (int j = 0; j < row.LastCellNum; j++)
                            {
                                ICell cell = row.GetCell(j);
                                switch (j)
                                {
                                    case 0:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            rowIndex++;
                                            goto BigCycle;
                                        }
                                        fkIndex++;
                                        fk.ForeignKey = cell.ToStringEx();
                                        break;
                                    case 1:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            this.ShowMessage(tbl.TableName, fk.ForeignKey, "主键表不能为空!");
                                            return;
                                        }
                                        fk.ForeignTable = cell.ToStringEx();
                                        break;
                                    case 2:
                                        if (cell.ToStringEx().IsNullOrEmpty())
                                        {
                                            this.ShowMessage(tbl.TableName, fk.ForeignKey, "对应主键表的列不能为空!");
                                            return;
                                        }
                                        fk.ForeignTablePK = cell.ToStringEx();
                                        break;
                                    default:
                                        break;
                                }
                            }
                            tbl.ForeignKeys.Add(fk);
                        }
                        #endregion
                        rowIndex++;
                    }
                    if (!tbl.TableName.IsNullOrEmpty() && tbl.TableName.Contains("_"))
                    {
                        if (!lstKeyWords.Contains("表名（英文）：") || !lstKeyWords.Contains("字段名（英文）")
                            || !lstKeyWords.Contains("索引名称") || !lstKeyWords.Contains("外键列"))
                        {
                            MessageBox.Show("Sheet" + tbl.TableName + "格式有问题，缺少列：表名（英文）：、字段名（英文）、索引名称、外键列");
                            return;
                        }
                        else
                        {
                            lstTable.Add(tbl);
                        }
                    }
                }
            }
            StringBuilder sb = this.GenerateSQL(lstTable);
            string savePath = ConfigurationManager.AppSettings["GenerateTable"];
            if (!string.IsNullOrEmpty(savePath))
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                GenerateFileHelper.GenerateFile(sb.ToStringEx(), savePath + DateTime.Now.ToString("yyyy-MM-dd") + "-GenerateTable.sql");
                MessageBox.Show("生成成功！", "消息");
                System.Diagnostics.Process.Start("Explorer.exe", savePath);
            }
        }

        private void ShowMessage(string sheetName, string columnName, string errorMsg)
        {
            MessageBox.Show(string.Format("Sheet{0}的列：{1}出错！{2}", sheetName, columnName, errorMsg));
        }

        private StringBuilder GenerateSQL(List<TableEntity> tbls)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TableEntity tbl in tbls)
            {
                //删除索引
                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.Append(" /*  删除索引                                                    */ ").AppendLine();
                sb.Append(" /*==============================================================*/ ").AppendLine();
                foreach (TableIndex idx in tbl.Indexs)
                {
                    sb.Append(" if exists (select 1 ").AppendLine();
                    sb.Append("     from  sysindexes ").AppendLine();
                    sb.AppendFormat("    where  id    = object_id('dbo.{0}') ", tbl.TableName).AppendLine();
                    sb.AppendFormat("     and   name  = '{0}' ", idx.IndexName).AppendLine();
                    sb.Append("     and   indid > 0 ").AppendLine();
                    sb.Append("     and   indid < 255) ").AppendLine();
                    sb.AppendFormat(" drop index dbo.{0}.{1} ", tbl.TableName, idx.IndexName).AppendLine();
                    sb.Append(" go ").AppendLine(Environment.NewLine);
                }
                //删除表
                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.Append(" /*  删除表                                                      */").AppendLine();
                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.Append(" if exists (select 1 ").AppendLine();
                sb.Append("     from  sysobjects ").AppendLine();
                sb.AppendFormat("    where  id = object_id('dbo.{0}') ", tbl.TableName).AppendLine();
                sb.AppendFormat("     and   type = 'U') ", tbl.TableName).AppendLine();
                sb.AppendFormat("     drop table dbo.{0} ", tbl.TableName).AppendLine();
                sb.Append(" go ").AppendLine(Environment.NewLine);
                //创建表
                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.Append(" /*  创建表                                                     */ ").AppendLine();
                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.AppendFormat(" create table dbo.{0} ( ", tbl.TableName).AppendLine();
                string primaryKey = string.Empty;
                List<string> uniques = new List<string>();
                foreach (TableColumn col in tbl.Columns)
                {
                    if (col.IsPrimaryKey == "是")
                    {
                        primaryKey += col.ColumnName + ",";
                    }
                    string strNull = col.IsAllowNull == "是" ? "NULL" : "NOT NULL";
                    string strDefault = string.IsNullOrEmpty(col.DefaultValue) ? "" : " DEFAULT " + col.DefaultValue;
                    if (col.IsPrimaryKey != "是" && col.IsUnique == "是" && !uniques.Contains(col.ColumnName))
                    {
                        uniques.Add(col.ColumnName);
                    }
                    sb.AppendFormat("  {0}  {1}  {2}  {3} ,", col.ColumnName, col.DataType, strNull, strDefault).AppendLine();
                }
                sb.Replace(",", ")", sb.Length - 10, 10);
                sb.Append(" go ").AppendLine(Environment.NewLine);
                //添加主键约束
                primaryKey = primaryKey.Remove(primaryKey.Length - 1, 1);
                sb.AppendFormat(" ALTER TABLE {0} ADD CONSTRAINT PK_{0}_{1} PRIMARY KEY ({2}) ", tbl.TableName, primaryKey.Replace(",", ""), primaryKey).AppendLine();
                sb.Append(" go ").AppendLine(Environment.NewLine);
                //添加唯一键约束
                foreach (var key in uniques)
                {
                    sb.AppendFormat(" ALTER TABLE {0} ADD CONSTRAINT {0}_{1} unique ({1}) ", tbl.TableName, key).AppendLine();
                    sb.Append(" go ").AppendLine(Environment.NewLine);
                }
                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.Append(" /*  添加备注                                                     */ ").AppendLine();
                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.Append("  execute sp_addextendedproperty 'MS_Description',  ").AppendLine();
                sb.AppendFormat("   '{0}','user', 'dbo', 'table', '{1}' ", tbl.TableDesc, tbl.TableName).AppendLine();
                sb.Append(" go ").AppendLine(Environment.NewLine);
                foreach (TableColumn col in tbl.Columns)
                {
                    sb.Append("  execute sp_addextendedproperty 'MS_Description',   ").AppendLine();
                    sb.AppendFormat("   '{0}','user', 'dbo', 'table', '{1}', 'column', '{2}' ", col.ColumnCnName, tbl.TableName, col.ColumnName).AppendLine();
                    sb.Append(" go ").AppendLine(Environment.NewLine);
                }
                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.Append(" /*  创建索引                                                     */ ").AppendLine();
                sb.Append(" /*==============================================================*/ ").AppendLine();
                foreach (TableIndex idx in tbl.Indexs)
                {
                    string strIncludeCol = string.IsNullOrEmpty(idx.IndexIncludeColumn) ? "" : string.Format("INCLUDE ({0})", idx.IndexIncludeColumn);
                    sb.AppendFormat("  CREATE INDEX {0}  ", idx.IndexName).AppendLine();
                    sb.AppendFormat("  ON {0} ({1}) {2}", tbl.TableName, idx.IndexColumn, strIncludeCol).AppendLine();
                    sb.Append(" go ").AppendLine(Environment.NewLine);
                }

                sb.Append(" /*==============================================================*/ ").AppendLine();
                sb.Append(" /*  创建外键                                                     */ ").AppendLine();
                sb.Append(" /*==============================================================*/ ").AppendLine();
                foreach (TableForeignKey fk in tbl.ForeignKeys)
                {
                    sb.AppendFormat("  alter table dbo.{0}  ", tbl.TableName).AppendLine();
                    sb.AppendFormat("    add constraint FK_{0}_{1} foreign key ({1}) ", tbl.TableName, fk.ForeignKey).AppendLine();
                    sb.AppendFormat(" references dbo.{0} ({1}) ", fk.ForeignTable, fk.ForeignTablePK).AppendLine();
                    sb.Append(" go ").AppendLine(Environment.NewLine);
                }
            }

            return sb;
        }
    }
}
