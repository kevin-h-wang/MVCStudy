using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using EasyUIDemo.Utility;

namespace EasyUIDemo.Tools
{
    /// <summary>
    /// 生成文件帮助类
    /// </summary>
    public class GenerateFileHelper
    {
        /// <summary>
        /// 生成实体文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static void GenerateFile(string strXML, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.WriteLine(strXML);
            }
        }
        ///// <summary>
        ///// 生成实体文件
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="fileName"></param>
        //public static void GenerateEntityFile(GenerateInfo generateInfo, DataGridView gridViewMain, bool compositPK)
        //{
        //    if (!compositPK)
        //    {
        //        #region 非联合主键
        //        string entityName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1) + "Entity";
        //        string filePath = string.Format("{0}\\{1}.cs", generateInfo.SavePath, entityName);
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //        }
        //        using (StreamWriter writer = File.CreateText(filePath))
        //        {
        //            writer.WriteLine(@"/*----------------------------------------------------------------");
        //            writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //            writer.WriteLine("// 版权所有。");
        //            writer.WriteLine("// 文   件   名：" + entityName + ".cs");
        //            writer.WriteLine("// 文件功能描述：");
        //            writer.WriteLine("//");
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //            writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 修 改 人：");
        //            writer.WriteLine("// 修改描述：");
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 修改标识：");
        //            writer.WriteLine("//----------------------------------------------------------------*/");
        //            writer.WriteLine("using System;");
        //            writer.WriteLine("using Founder.Framework.Base;");
        //            writer.WriteLine("using Founder.FIS.CMD.Base;");
        //            writer.WriteLine();
        //            writer.WriteLine("namespace " + generateInfo.NameSpace);
        //            writer.WriteLine("{");
        //            //TableDesc
        //            writer.WriteLine("    /// <summary>");
        //            writer.WriteLine(string.Format("    /// {0}", generateInfo.TableDesc));
        //            writer.WriteLine("    /// </summary>");
        //            writer.WriteLine("    [Serializable]  ");

        //            writer.WriteLine(string.Format("    public class {0} : CMDEntityBase", entityName));

        //            writer.WriteLine("    {");
        //            writer.WriteLine("        #region 属性");
        //            writer.WriteLine();
        //            //Properties
        //            foreach (DataGridViewRow row in gridViewMain.Rows)
        //            {
        //                bool isPK = row.Cells["ColumnIsPK"].Value.ToString() == "√" ? true : false;
        //                //bool isNullable = row.Cells["ColumnIsNullable"].Value.ToString() == "√" ? true : false;
        //                if (!isPK)
        //                {
        //                    string isBase = row.Cells["ColumnName"].Value.ToString().ToUpper();
        //                    string type = FormHelper.GetCSharpTypeByDBType(FormHelper.ConvertStrToCMD2DBType(row.Cells["ColumnType"].Value.ToString()));
        //                    if (isBase != "CREATOR" && isBase != "CREATETIME" && isBase != "LASTMODIFIER" && isBase != "LASTMODIFYTIME")
        //                    {
        //                        writer.WriteLine("        /// <summary>");
        //                        writer.WriteLine(string.Format("        /// {0}", row.Cells["ColumnDesc"].Value));
        //                        writer.WriteLine("        /// </summary>");
        //                        writer.WriteLine("        public virtual " + type
        //                            + (FormHelper.IsNullableType(type) ? "?" : "") + " "
        //                            + row.Cells["ColumnName"].Value + "{ get; set; }");
        //                        writer.WriteLine();
        //                    }
        //                }
        //            }
        //            writer.WriteLine("        #endregion");
        //            writer.WriteLine("    }");
        //            writer.WriteLine("}");
        //        }
        //        #endregion
        //    }
        //    else
        //    {
        //        #region 联合主键
        //        string pkEntityName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1) + "PKEntity";
        //        string pkFilePath = string.Format(@"{0}\{1}.cs", generateInfo.SavePath, pkEntityName);
        //        if (File.Exists(pkFilePath))
        //        {
        //            File.Delete(pkFilePath);
        //        }
        //        using (StreamWriter writer = File.CreateText(pkFilePath))
        //        {
        //            writer.WriteLine(@"/*----------------------------------------------------------------");
        //            writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //            writer.WriteLine("// 版权所有。");
        //            writer.WriteLine("// 文   件   名：" + pkEntityName + ".cs");
        //            writer.WriteLine("// 文件功能描述：");
        //            writer.WriteLine("//");
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //            writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 修 改 人：");
        //            writer.WriteLine("// 修改描述：");
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 修改标识：");
        //            writer.WriteLine("//----------------------------------------------------------------*/");
        //            writer.WriteLine("using System;");
        //            writer.WriteLine("using Founder.Framework.Base;");
        //            writer.WriteLine("using Founder.FIS.CMD.Base;");
        //            writer.WriteLine();
        //            writer.WriteLine("namespace " + generateInfo.NameSpace + ".DataPKEntity");
        //            writer.WriteLine("{");
        //            //TableDesc
        //            writer.WriteLine("    /// <summary>");
        //            writer.WriteLine(string.Format("    /// {0}", generateInfo.TableDesc));
        //            writer.WriteLine("    /// </summary>");
        //            writer.WriteLine("    [Serializable]  ");

        //            writer.WriteLine(string.Format("    public class {0} ", pkEntityName));

        //            writer.WriteLine("    {");
        //            writer.WriteLine("        #region 属性");
        //            writer.WriteLine();
        //            //Properties
        //            StringBuilder sb = new StringBuilder();
        //            foreach (DataGridViewRow row in gridViewMain.Rows)
        //            {
        //                bool isPK = row.Cells["ColumnIsPK"].Value.ToString() == "√" ? true : false;
        //                //bool isNullable = row.Cells["ColumnIsNullable"].Value.ToString() == "√" ? true : false;
        //                if (isPK)
        //                {
        //                    string type = FormHelper.GetCSharpTypeByDBType(FormHelper.ConvertStrToCMD2DBType(row.Cells["ColumnType"].Value.ToString()));
        //                    writer.WriteLine("        /// <summary>");
        //                    writer.WriteLine(string.Format("        /// {0}", row.Cells["ColumnDesc"].Value));
        //                    writer.WriteLine("        /// </summary>");
        //                    writer.WriteLine("        public virtual " + type
        //                        + (FormHelper.IsNullableType(type) ? "?" : "") + " "
        //                        + row.Cells["ColumnName"].Value + "{ get; set; }");
        //                    writer.WriteLine();

        //                    sb.AppendFormat("&& this.{0} == pk.{0} ", row.Cells["ColumnName"].Value);
        //                }
        //            }
        //            writer.WriteLine("        #endregion");

        //            writer.WriteLine(@"        /// <summary>");
        //            writer.WriteLine(@"        /// 判断两个对象是否相同，这个方法需要重写");
        //            writer.WriteLine(@"        /// </summary>");
        //            writer.WriteLine("        /// <param name=\"obj\">进行比较的对象</param>");
        //            writer.WriteLine(@"        /// <returns>真true或假false</returns>");
        //            writer.WriteLine("        public override bool Equals(object obj)");
        //            writer.WriteLine("        {");
        //            writer.WriteLine(string.Format("            if (obj is {0})", pkEntityName));
        //            writer.WriteLine("              {");
        //            writer.WriteLine(string.Format("              {0} pk = obj as {0};",pkEntityName));
        //            writer.WriteLine(string.Format("                if ({0})", sb.Remove(0,2).ToString()));
        //            writer.WriteLine("                  {");
        //            writer.WriteLine("                      return true;");
        //            writer.WriteLine("                  }");
        //            writer.WriteLine("              }");
        //            writer.WriteLine("              return false;");
        //            writer.WriteLine("          }");

        //            writer.WriteLine("         public override int GetHashCode()");
        //            writer.WriteLine("         {");
        //            writer.WriteLine("              return base.GetHashCode();");
        //            writer.WriteLine("        }");


        //            writer.WriteLine("    }");
        //            writer.WriteLine("}");
        //        }
        //        #endregion

        //        #region 实体
        //        string entityName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1) + "Entity";
        //        string filePath = string.Format(@"{0}\{1}.cs", generateInfo.SavePath, entityName);
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //        }
        //        using (StreamWriter writer = File.CreateText(filePath))
        //        {
        //            writer.WriteLine(@"/*----------------------------------------------------------------");
        //            writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //            writer.WriteLine("// 版权所有。");
        //            writer.WriteLine("// 文   件   名：" + entityName + ".cs");
        //            writer.WriteLine("// 文件功能描述：");
        //            writer.WriteLine("//");
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //            writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 修 改 人：");
        //            writer.WriteLine("// 修改描述：");
        //            writer.WriteLine("//");
        //            writer.WriteLine("// 修改标识：");
        //            writer.WriteLine("//----------------------------------------------------------------*/");
        //            writer.WriteLine("using System;");
        //            writer.WriteLine("using Founder.Framework.Base;");
        //            writer.WriteLine("using Founder.FIS.CMD.Base;");
        //            writer.WriteLine();
        //            writer.WriteLine("namespace " + generateInfo.NameSpace);
        //            writer.WriteLine("{");
        //            //TableDesc
        //            writer.WriteLine("    /// <summary>");
        //            writer.WriteLine(string.Format("    /// {0}", generateInfo.TableDesc));
        //            writer.WriteLine("    /// </summary>");
        //            writer.WriteLine("    [Serializable]  ");

        //            writer.WriteLine(string.Format("    public class {0} : CMDEntityBase", entityName));

        //            writer.WriteLine("    {");
        //            writer.WriteLine("        #region 属性");
        //            writer.WriteLine();
        //            writer.WriteLine("    /// <summary>");
        //            writer.WriteLine("    /// 联合主键");
        //            writer.WriteLine("    /// </summary>");
        //            writer.WriteLine("    public virtual " + pkEntityName + " PK { get; set; }");
        //            //Properties
        //            foreach (DataGridViewRow row in gridViewMain.Rows)
        //            {
        //                bool isPK = row.Cells["ColumnIsPK"].Value.ToString() == "√" ? true : false;
        //                //bool isNullable = row.Cells["ColumnIsNullable"].Value.ToString() == "√" ? true : false;
        //                if (!isPK)
        //                {
        //                    string isBase = row.Cells["ColumnName"].Value.ToString().ToUpper();
        //                    string type = FormHelper.GetCSharpTypeByDBType(FormHelper.ConvertStrToCMD2DBType(row.Cells["ColumnType"].Value.ToString()));
        //                    if (isBase != "CREATOR" && isBase != "CREATETIME" && isBase != "LASTMODIFIER" && isBase != "LASTMODIFYTIME")
        //                    {
        //                        writer.WriteLine("        /// <summary>");
        //                        writer.WriteLine(string.Format("        /// {0}", row.Cells["ColumnDesc"].Value));
        //                        writer.WriteLine("        /// </summary>");
        //                        writer.WriteLine("        public virtual " + type
        //                            + (FormHelper.IsNullableType(type) ? "?" : "") + " "
        //                            + row.Cells["ColumnName"].Value + "{ get; set; }");
        //                        writer.WriteLine();
        //                    }
        //                }
        //            }
        //            writer.WriteLine("        #endregion");
        //            writer.WriteLine("    }");
        //            writer.WriteLine("}");
        //        }
        //        #endregion
        //    }
        //}

        ///// <summary>
        ///// 生成XML映射文件
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="fileName"></param>
        //public static void GenerateEntityMapFile(GenerateInfo generateInfo, DataGridView gridViewMain, bool compositPK)
        //{
        //    if (!compositPK)
        //    {
        //        string entityName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1) + "Entity";
        //        string filePath = string.Format("{0}\\{1}.hbm.xml", generateInfo.SavePath, entityName);
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //        }
        //        using (StreamWriter writer = File.CreateText(filePath))
        //        {
        //            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        //            writer.WriteLine(string.Format("<hibernate-mapping assembly=\"{0}\" namespace=\"{1}\" xmlns=\"urn:nhibernate-mapping-2.2\">", generateInfo.Assembly, generateInfo.NameSpace));
        //            writer.WriteLine(string.Format("  <class name=\"{0}\" table=\"{1}\" lazy=\"true\" >", entityName, generateInfo.TableName));

        //            //Properties
        //            foreach (DataGridViewRow row in gridViewMain.Rows)
        //            {
        //                string isPK = row.Cells["ColumnIsPK"].Value.ToString();
        //                if (isPK == "√")
        //                {
        //                    writer.WriteLine(string.Format("    <id name=\"ID\" column=\"{0}\" />", row.Cells["ColumnName"].Value.ToString()));
        //                }
        //                else
        //                {
        //                    writer.WriteLine(string.Format("    <property name=\"{0}\">", row.Cells["ColumnName"].Value.ToString()));
        //                    writer.WriteLine(string.Format("      <column name=\"{0}\" sql-type=\"{1}\" not-null=\"{2}\" />", row.Cells["ColumnName"].Value.ToString(),
        //                        row.Cells["ColumnType"].Value.ToString(), row.Cells["ColumnIsNullable"].Value.ToString() == "√" ? "false" : "true"));
        //                    writer.WriteLine("    </property>");
        //                }
        //            }
        //            writer.WriteLine("  </class>");
        //            writer.WriteLine("</hibernate-mapping>");
        //        }
        //    }
        //    else
        //    {
        //        string entityName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1) + "Entity";
        //        string pkEntityName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1)+"PKEntity";
        //        string filePath = string.Format("{0}\\{1}.hbm.xml", generateInfo.SavePath, entityName);
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //        }
        //        using (StreamWriter writer = File.CreateText(filePath))
        //        {
        //            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        //            writer.WriteLine(string.Format("<hibernate-mapping assembly=\"{0}\" namespace=\"{1}\" xmlns=\"urn:nhibernate-mapping-2.2\">", generateInfo.Assembly, generateInfo.NameSpace));
        //            writer.WriteLine(string.Format("  <class name=\"{0}\" table=\"{1}\" lazy=\"true\" >", entityName, generateInfo.TableName));

        //            writer.WriteLine(string.Format("<composite-id name=\"PK\" class=\"{0}.{1}.{2}\">",generateInfo.NameSpace,".DataPKEntity",pkEntityName));
        //            //Properties
        //            foreach (DataGridViewRow row in gridViewMain.Rows)
        //            {
        //                string isPK = row.Cells["ColumnIsPK"].Value.ToString();
        //                if (isPK == "√")
        //                {
        //                    writer.WriteLine(string.Format("     <key-property name=\"{0}\"> ", row.Cells["ColumnName"].Value.ToString()));
        //                    writer.WriteLine(string.Format("        <column name=\"{0}\" sql-type=\"{1}\"/>", row.Cells["ColumnName"].Value.ToString(),row.Cells["ColumnType"].Value.ToString()));
        //                    writer.WriteLine("    </key-property>");
        //                }
        //            }
        //            writer.WriteLine("     </composite-id>");

        //            foreach (DataGridViewRow row in gridViewMain.Rows)
        //            {
        //                string isPK = row.Cells["ColumnIsPK"].Value.ToString();
        //                if(isPK != "√")
        //                {
        //                    writer.WriteLine(string.Format("    <property name=\"{0}\">", row.Cells["ColumnName"].Value.ToString()));
        //                    writer.WriteLine(string.Format("      <column name=\"{0}\" sql-type=\"{1}\" not-null=\"{2}\" />", row.Cells["ColumnName"].Value.ToString(),
        //                        row.Cells["ColumnType"].Value.ToString(), row.Cells["ColumnIsNullable"].Value.ToString() == "√" ? "false" : "true"));
        //                    writer.WriteLine("    </property>");
        //                }
        //            }
        //            writer.WriteLine("  </class>");
        //            writer.WriteLine("</hibernate-mapping>");
        //        }
        //    }
        //}

        ///// <summary>
        ///// 生成编辑页面
        ///// </summary>
        //public static void GenerateEditWebFormFile(GenerateInfo generateInfo, DataGridView gridViewMain)
        //{
        //    List<PageControl> editControls = getPageControlList(gridViewMain, EnumFilterType.编辑页面);
        //    string fileName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1) + "Detail";
        //    #region 生成.aspx页面
        //    //生成编辑页面
        //    string filePath = string.Format("{0}\\{1}.aspx", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    //生成.aspx页面
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(string.Format("<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"{0}.aspx.cs\" Inherits=\"Founder.FIS.CMD.Website.Views{1}{0}\" %>", fileName, generateInfo.Module == "" ? "" : generateInfo.Module + "."));
        //        writer.WriteLine("<!DOCTYPE html>");
        //        writer.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        //        writer.WriteLine("<head runat=\"server\">");
        //        writer.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
        //        writer.WriteLine(string.Format("    <title>{0}</title>", generateInfo.TableDesc + "编辑"));
        //        writer.WriteLine("</head>");
        //        writer.WriteLine("<body>");
        //        writer.WriteLine("    <form id=\"form1\" runat=\"server\">");
        //        writer.WriteLine("        <div id=\"flt\" class=\"flttoolbar\">");
        //        writer.WriteLine("            <Founder:FIButton ID=\"btnSave\" runat=\"server\" Action=\"保存\" OnClick=\"btnSave_Click\" />");
        //        writer.WriteLine("            <Founder:FIButton ID=\"btnCancel\" runat=\"server\" Action=\"取消\" OnClick=\"btnCancel_Click\"/>");
        //        writer.WriteLine("        </div>");
        //        writer.WriteLine("        <div class=\"searchtoolbar\"></div>");
        //        writer.WriteLine("        <div>");
        //        writer.WriteLine("            <table>");
        //        int colCount = generateInfo.EditColumnNumber;
        //        int rowCount = editControls.Count % colCount == 0 ? editControls.Count / colCount : editControls.Count / colCount + 1;
        //        int k = 0;
        //        for (int i = 0; i < rowCount; i++)
        //        {
        //            if (k != editControls.Count)
        //            {
        //                writer.WriteLine("                <tr>");
        //                for (int j = 0; j < colCount; j++)
        //                {
        //                    if (k != editControls.Count)
        //                    {
        //                        writer.WriteLine("                    <th>");
        //                        writer.WriteLine(string.Format("                        <Founder:FILabel ID=\"lbl{0}\" runat=\"server\" Text=\"{1}\" />", editControls[k].ControlName, editControls[k].ControlDesc + "："));
        //                        writer.WriteLine("                    </th>");
        //                        writer.WriteLine("                    <td>");
        //                        string validateHtml = string.Empty;
        //                        if (editControls[k].ControlValidate == EnumValidateType.必填)
        //                        {
        //                            validateHtml = "Required=\"true\"";
        //                        }
        //                        else if (editControls[k].ControlValidate == EnumValidateType.必填数字)
        //                        {
        //                            validateHtml = "Required=\"true\" Type=\"Number\"";
        //                        }
        //                        else if (editControls[k].ControlValidate == EnumValidateType.必填邮箱)
        //                        {
        //                            validateHtml = "Required=\"true\" Type=\"Email\"";
        //                        }
        //                        writer.WriteLine(string.Format("                        <Founder:{1} ID=\"{0}\" runat=\"server\" {2} />", editControls[k].ControlID, editControls[k].ControlType.ToString(), validateHtml));

        //                        writer.WriteLine("                    </td>");
        //                        k++;
        //                    }
        //                }
        //                writer.WriteLine("                </tr>");
        //            }
        //        }
        //        writer.WriteLine("            </table>");
        //        writer.WriteLine("        </div>");
        //        writer.WriteLine("    </form>");
        //        writer.WriteLine("</body>");
        //        writer.WriteLine("</html>");
        //    }
        //    #endregion

        //    #region 生成View.cs文件
        //    //生成View.cs文件
        //    filePath = string.Format("{0}\\I{1}View.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(@"/*----------------------------------------------------------------");
        //        writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //        writer.WriteLine("// 版权所有。");
        //        writer.WriteLine(string.Format("// 文   件   名：I{0}View.cs", fileName));
        //        writer.WriteLine("// 文件功能描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //        writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修 改 人：");
        //        writer.WriteLine("// 修改描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修改标识：");
        //        writer.WriteLine("//----------------------------------------------------------------*/");
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using Founder.FIS.CMD.Base;");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace Founder.FIS.CMD.IView");
        //        writer.WriteLine("{");
        //        writer.WriteLine(string.Format("    public interface I{0}View : ICMDViewBase", fileName));
        //        writer.WriteLine("    {");
        //        writer.WriteLine("        #region 属性");
        //        writer.WriteLine();
        //        foreach (PageControl pageControl in editControls)
        //        {
        //            //Type type = pageControl.ColumnType;
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        /// {0}", pageControl.ControlDesc));
        //            writer.WriteLine("        /// </summary>");
        //            writer.WriteLine("        " + pageControl.ColumnType + (FormHelper.IsNullableType(pageControl.ColumnType) ? "?" : "") + " " + pageControl.ControlName + "{ get; set; }");
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion

        //    #region 生成.cs页面
        //    filePath = string.Format("{0}\\{1}.aspx.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(@"/*----------------------------------------------------------------");
        //        writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //        writer.WriteLine("// 版权所有。");
        //        writer.WriteLine(string.Format("// 文   件   名：{0}.aspx.cs", fileName));
        //        writer.WriteLine("// 文件功能描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //        writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修 改 人：");
        //        writer.WriteLine("// 修改描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修改标识：");
        //        writer.WriteLine("//----------------------------------------------------------------*/");
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Web;");
        //        writer.WriteLine("using Founder.FIS.CMD.Constants.Enum;");
        //        writer.WriteLine("using System.Web.UI;");
        //        writer.WriteLine("using System.Web.UI.WebControls;");
        //        writer.WriteLine("using Founder.BasicModule.Permission.IView;");
        //        writer.WriteLine("using Founder.FIS.CMD.Website.Shared;");
        //        writer.WriteLine("using Founder.Framework.Utility;");
        //        writer.WriteLine();
        //        writer.WriteLine(string.Format("namespace Founder.FIS.CMD.Website.Views{0}", generateInfo.Module == "" ? "" : "." + generateInfo.Module));
        //        writer.WriteLine("{");
        //        writer.WriteLine(string.Format("    public partial class {0} : CMDDetailPageBase, I{0}View", fileName));
        //        writer.WriteLine("    {");
        //        writer.WriteLine("        #region 字段");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 属性");
        //        writer.WriteLine();
        //        writer.WriteLine("        protected override EnumModule Module");
        //        writer.WriteLine("        {");
        //        writer.WriteLine("            get { return EnumModule.用户管理详细; }");
        //        writer.WriteLine("        }");
        //        writer.WriteLine();
        //        writer.WriteLine("        public Guid? ID");
        //        writer.WriteLine("        {");
        //        writer.WriteLine("            get");
        //        writer.WriteLine("            {");
        //        writer.WriteLine("                return ItemID;");
        //        writer.WriteLine("            }");
        //        writer.WriteLine("            set");
        //        writer.WriteLine("            {");
        //        writer.WriteLine("                ItemID = value;");
        //        writer.WriteLine("            }");
        //        writer.WriteLine("        }");
        //        writer.WriteLine();
        //        foreach (PageControl pageControl in editControls)
        //        {
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        /// {0}", pageControl.ControlDesc));
        //            writer.WriteLine("        /// </summary>");
        //            writer.WriteLine(string.Format("        public {1} {0}", pageControl.ControlName, pageControl.ColumnType + (FormHelper.IsNullableType(pageControl.ColumnType) ? "?" : "")));
        //            writer.WriteLine("        {");
        //            writer.WriteLine("            get");
        //            writer.WriteLine("            {");
        //            writer.WriteLine(string.Format("                return {0}.{2}{1};", pageControl.ControlID, FormHelper.GetConvertMethodByType(pageControl.ColumnType), FormHelper.GetControlPropertyValueSetProperty(pageControl.ControlType)));
        //            writer.WriteLine("            }");
        //            writer.WriteLine("            ");
        //            writer.WriteLine("            set");
        //            writer.WriteLine("            {");
        //            writer.WriteLine(string.Format("                {0}.{1} = value.ToStringEx();", pageControl.ControlID, FormHelper.GetControlPropertyValueSetProperty(pageControl.ControlType)));
        //            writer.WriteLine("            }");
        //            writer.WriteLine("        }");
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine();
        //        writer.WriteLine("        #region 事件");
        //        writer.WriteLine("        protected void Page_Load(object sender, EventArgs e){}");
        //        writer.WriteLine("        protected void btnSave_Click(object sender, EventArgs e){}");
        //        writer.WriteLine("        protected void btnCancel_Click(object sender, EventArgs e){}");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 私有方法");
        //        writer.WriteLine("        protected override void InitControl(){}");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 公共方法");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion

        //    #region 生成.aspx.designer.cs页面
        //    filePath = string.Format("{0}\\{1}.aspx.designer.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine("//------------------------------------------------------------------------------");
        //        writer.WriteLine("// <自动生成>");
        //        writer.WriteLine("//     此代码由工具生成。");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//     对此文件的更改可能会导致不正确的行为，并且如果");
        //        writer.WriteLine("//     重新生成代码，这些更改将会丢失。");
        //        writer.WriteLine("// </自动生成>");
        //        writer.WriteLine("//------------------------------------------------------------------------------");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace Founder.FIS.CMD.Website.Views" + (generateInfo.Module == "" ? "" : "." + generateInfo.Module) + "{");
        //        writer.WriteLine();
        //        writer.WriteLine("    public partial class " + fileName + " {");
        //        writer.WriteLine();
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        /// form1 控件。");
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <remarks>");
        //        writer.WriteLine("        /// 自动生成的字段。");
        //        writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //        writer.WriteLine("        /// </remarks>");
        //        writer.WriteLine("        protected global::System.Web.UI.HtmlControls.HtmlForm form1;");
        //        foreach (PageControl pageControl in editControls)
        //        {
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        /// {0} 控件。", pageControl.ControlID));
        //            writer.WriteLine("        /// </summary>");
        //            writer.WriteLine("        /// <remarks>");
        //            writer.WriteLine("        /// 自动生成的字段。");
        //            writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //            writer.WriteLine("        /// </remarks>");
        //            writer.WriteLine(string.Format("         protected global::Founder.Framework.Web.UI.WebControls.{0} {1};", pageControl.ControlType.ToString(), pageControl.ControlID));
        //            writer.WriteLine();
        //            //foreach (ValidateControl validateControl in pageControl.ValidateControlList)
        //            //{
        //            //    writer.WriteLine("        /// <summary>");
        //            //    writer.WriteLine(string.Format("        /// {0} 控件。", validateControl.ControlID));
        //            //    writer.WriteLine("        /// </summary>");
        //            //    writer.WriteLine("        /// <remarks>");
        //            //    writer.WriteLine("        /// 自动生成的字段。");
        //            //    writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //            //    writer.WriteLine("        /// </remarks>");
        //            //    writer.WriteLine(string.Format("        protected global::System.Web.UI.WebControls.{0} {1};", validateControl.ControlName, validateControl.ControlID));
        //            //}
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion
        //}

        ///// <summary>
        ///// 生成Logic文件
        ///// </summary>
        ///// <param name="generateInfo"></param>
        ///// <param name="gridViewMain"></param>
        //public static void GenerateLogicFile(GenerateInfo generateInfo, DataGridView gridViewMain)
        //{
        //    string logicName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_",
        //        StringComparison.Ordinal) + 1) + "Logic";
        //    string filePath = string.Format("{0}\\{1}.cs", generateInfo.SavePath, logicName);
        //    List<PageControl> editControls = getPageControlList(gridViewMain, EnumFilterType.列表页面);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(@"/*----------------------------------------------------------------");
        //        writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //        writer.WriteLine("// 版权所有。");
        //        writer.WriteLine(string.Format("// 文   件   名：{0}.cs", logicName));
        //        writer.WriteLine("// 文件功能描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //        writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修 改 人：");
        //        writer.WriteLine("// 修改描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修改标识：");
        //        writer.WriteLine("//----------------------------------------------------------------*/");
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Data;");
        //        writer.WriteLine("using System.Data.Common;");
        //        writer.WriteLine("using System.Text;");
        //        writer.WriteLine("using System.Collections.Generic;");
        //        writer.WriteLine("using Founder.Framework.Data;");
        //        writer.WriteLine("using Founder.Framework.Core.Base;");
        //        writer.WriteLine("using Founder.FIS.CMD.IView;");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace Founder.FIS.CMD.Logic");
        //        writer.WriteLine("{");
        //        //TableDesc
        //        writer.WriteLine("    /// <summary>");
        //        writer.WriteLine(string.Format("    /// {0}", generateInfo.TableDesc));
        //        writer.WriteLine("    /// </summary>");
        //        //Class
        //        writer.WriteLine(string.Format("    public class {0} ", logicName));

        //        writer.WriteLine("    {");
        //        writer.WriteLine(string.Format("        public static DataTable Serach(I{0}ListView view)",
        //            generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1)));
        //        writer.WriteLine("        {");
        //        writer.WriteLine("            List<DbParameter> listPara = new List<DbParameter>();");
        //        writer.WriteLine("            #region Parameter");
        //        //Add Parameter
        //        foreach (PageControl pageControl in editControls)
        //        {
        //            writer.WriteLine("            DbParameter para=new SqlParameter();");
        //            writer.WriteLine(string.Format("            para.ParameterName=\"@{0}\";", pageControl.ControlName));
        //            writer.WriteLine(string.Format("            para.Value= view.{0};", pageControl.ControlName));
        //            writer.WriteLine("            listPara.Add(para);");
        //        }
        //        writer.WriteLine("            #endregion");
        //        writer.WriteLine("            return new DataTable();");
        //        writer.WriteLine("        }");
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //}

        ///// <summary>
        ///// 从DataGridView中获取页面需要的控件参数
        ///// </summary>
        ///// <param name="gridViewMain">DataGirdView</param>
        ///// <param name="filter">过滤类型，编辑页面控件、查询条件、列表控件</param>
        ///// <returns></returns>
        //private static List<PageControl> getPageControlList(DataGridView gridViewMain, EnumFilterType filter)
        //{
        //    List<PageControl> pageControlList = new List<PageControl>();
        //    foreach (DataGridViewRow row in gridViewMain.Rows)
        //    {
        //        #region 编辑页面
        //        if (filter == EnumFilterType.编辑页面)
        //        {
        //            bool isShowEdit = Convert.ToBoolean(row.Cells["ColumnIsEditControl"].Value);
        //            if (isShowEdit)
        //            {
        //                PageControl pageControl = new PageControl();
        //                pageControl.ControlName = row.Cells["ColumnName"].Value.ToString();
        //                pageControl.ControlDesc = row.Cells["ColumnDesc"].Value.ToString();
        //                pageControl.ColumnType = FormHelper.GetCSharpTypeByDBType(FormHelper.ConvertStrToCMD2DBType(row.Cells["ColumnType"].Value.ToString()));
        //                pageControl.ControlType = row.Cells["ColumnControlType"].Value == null ? EnumControlType.FITextBox
        //                    : (EnumControlType)Enum.Parse(typeof(EnumControlType), row.Cells["ColumnControlType"].Value.ToString());
        //                if (row.Cells["ColumnIsValidate"].Value == null)
        //                {
        //                    if (string.IsNullOrWhiteSpace(row.Cells["ColumnIsNullable"].Value.ToString()))
        //                    {
        //                        pageControl.ControlValidate = EnumValidateType.必填;
        //                    }
        //                    else
        //                    {
        //                        pageControl.ControlValidate = EnumValidateType.无;
        //                    }
        //                }
        //                else
        //                {
        //                    pageControl.ControlValidate = (EnumValidateType)Enum.Parse(typeof(EnumValidateType), row.Cells["ColumnIsValidate"].Value.ToString());
        //                }
        //                pageControlList.Add(pageControl);
        //            }

        //        }
        //        #endregion

        //        #region 列表页面
        //        if (filter == EnumFilterType.列表页面)
        //        {
        //            bool isQueryFilter = Convert.ToBoolean(row.Cells["ColumnIsQueryFilter"].Value);
        //            if (isQueryFilter)
        //            {
        //                PageControl pageControl = new PageControl();
        //                pageControl.ControlName = row.Cells["ColumnName"].Value.ToString();
        //                pageControl.ControlDesc = row.Cells["ColumnDesc"].Value.ToString();
        //                pageControl.ColumnType = FormHelper.GetCSharpTypeByDBType(FormHelper.ConvertStrToCMD2DBType(row.Cells["ColumnType"].Value.ToString()));
        //                pageControl.ControlType = row.Cells["ColumnControlType"].Value == null ? EnumControlType.FITextBox : (EnumControlType)Enum.Parse(typeof(EnumControlType), row.Cells["ColumnControlType"].Value.ToString());
        //                if (row.Cells["ColumnIsValidate"].Value == null)
        //                {
        //                    if (string.IsNullOrWhiteSpace(row.Cells["ColumnIsNullable"].Value.ToString()))
        //                    {
        //                        pageControl.ControlValidate = EnumValidateType.必填;
        //                    }
        //                    else
        //                    {
        //                        pageControl.ControlValidate = EnumValidateType.无;
        //                    }
        //                }
        //                else
        //                {
        //                    pageControl.ControlValidate = (EnumValidateType)Enum.Parse(typeof(EnumValidateType), row.Cells["ColumnIsValidate"].Value.ToString());
        //                }
        //                pageControlList.Add(pageControl);
        //            }

        //        }
        //        #endregion

        //    }
        //    return pageControlList;
        //}

        ///// <summary>
        ///// 获取Grid栏位名称
        ///// </summary>
        ///// <param name="gridViewMain"></param>
        ///// <returns></returns>
        //private static List<PageControl> getGridColumnsList(DataGridView gridViewMain)
        //{
        //    List<PageControl> listItems = new List<PageControl>();
        //    foreach (DataGridViewRow row in gridViewMain.Rows)
        //    {
        //        bool isGridColumn = Convert.ToBoolean(row.Cells["ColumnIsListControl"].Value);

        //        if (isGridColumn)
        //        {
        //            PageControl listItem = new PageControl();
        //            listItem.ControlName = row.Cells["ColumnName"].Value.ToString();
        //            listItem.Sort = Convert.ToBoolean(row.Cells["ColumnIsOrderInListPage"].Value);
        //            listItem.ControlDesc = row.Cells["ColumnDesc"].Value.ToString();
        //            string type = FormHelper.GetCSharpTypeByDBType(FormHelper.ConvertStrToCMD2DBType(row.Cells["ColumnType"].Value.ToString()));
        //            listItem.ColumnType = type;
        //            listItem.DefaultSortExpression = Convert.ToBoolean(row.Cells["ColumnDefaultSortExpression"].Value);
        //            listItems.Add(listItem);
        //        }
        //    }
        //    return listItems;
        //}

        ///// <summary>
        ///// 生成列表页面
        ///// </summary>
        ///// <param name="generateInfo">生成文件信息</param>
        ///// <param name="gridViewMain">Grid信息</param>
        //public static void GenerateListWebFormFile(GenerateInfo generateInfo, DataGridView gridViewMain)
        //{
        //    List<PageControl> listControls = getPageControlList(gridViewMain, EnumFilterType.列表页面);
        //    List<PageControl> gridColumns = getGridColumnsList(gridViewMain);
        //    string fileName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1) + "List";
        //    #region 生成.aspx页面

        //    string filePath = string.Format("{0}\\{1}.aspx", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }

        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(string.Format("<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"{0}.aspx.cs\" Inherits=\"Founder.FIS.CMD.Website.Views{1}{0}\" %>", fileName, generateInfo.Module == "" ? "" : generateInfo.Module + "."));
        //        writer.WriteLine("<!DOCTYPE html>");
        //        writer.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        //        writer.WriteLine("<head runat=\"server\">");
        //        writer.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
        //        writer.WriteLine(string.Format("    <title>{0}</title>", generateInfo.TableDesc + "列表"));
        //        writer.WriteLine("</head>");
        //        writer.WriteLine("<body>");
        //        writer.WriteLine("    <form id=\"form1\" runat=\"server\">");
        //        writer.WriteLine("        <div id=\"flt\" class=\"flttoolbar\">");
        //        writer.WriteLine("            <Founder:FIButton ID=\"btnQuery\" runat=\"server\" Action=\"查询\" OnClick=\"btnQuery_Click\" />");
        //        if (generateInfo.IsCanExport)
        //        {
        //            writer.WriteLine("            <Founder:FIButton ID=\"btnExport\" runat=\"server\" Action=\"导出\" OnClick=\"btnExport_Click\"/>");
        //        }
        //        writer.WriteLine("        </div>");
        //        writer.WriteLine("        <div class=\"searchtoolbar\">");
        //        writer.WriteLine("            <table >");
        //        int colCount = generateInfo.ListColumnNumber;
        //        int rowCount = listControls.Count % colCount == 0 ? listControls.Count / colCount : listControls.Count / colCount + 1;
        //        int k = 0;
        //        #region 生成查询条件区
        //        for (int i = 0; i < rowCount; i++)
        //        {
        //            if (k != listControls.Count)
        //            {
        //                writer.WriteLine("            <tr>");
        //                for (int j = 0; j < colCount; j++)
        //                {
        //                    if (k != listControls.Count)
        //                    {
        //                        writer.WriteLine("                    <th>");
        //                        writer.WriteLine(string.Format("                        <Founder:FILabel ID=\"lbl{0}\" runat=\"server\" Text=\"{1}\" />", listControls[k].ControlName, listControls[k].ControlDesc + "："));
        //                        writer.WriteLine("                    </th>");
        //                        string validateHtml = string.Empty;
        //                        if (listControls[k].ControlValidate == EnumValidateType.必填)
        //                        {
        //                            validateHtml = "Required=\"true\"";
        //                        }
        //                        else if (listControls[k].ControlValidate == EnumValidateType.必填数字)
        //                        {
        //                            validateHtml = "Required=\"true\" Type=\"Number\"";
        //                        }
        //                        else if (listControls[k].ControlValidate == EnumValidateType.必填邮箱)
        //                        {
        //                            validateHtml = "Required=\"true\" Type=\"Email\"";
        //                        }
        //                        writer.WriteLine("                    <td>");
        //                        writer.WriteLine(string.Format("                        <Founder:{1} ID=\"{0}\" runat=\"server\" {2} />", listControls[k].ControlID, listControls[k].ControlType.ToString(), validateHtml));
        //                        writer.WriteLine("                    </td>");
        //                        k++;
        //                    }
        //                }
        //                writer.WriteLine("                </tr>");
        //            }
        //        }
        //        writer.WriteLine("            </table>");
        //        writer.WriteLine("        </div>");
        //        #endregion

        //        #region 生成Grid
        //        writer.WriteLine("        <div id=\"divList\" >");
        //        writer.WriteLine(string.Format("            <Founder:FIGridView DataKeyNames=\"Id\" ID=\"gv{0}\" runat=\"server\" AutoGenerateColumns=\"False\"  ", fileName));
        //        writer.WriteLine(string.Format("                         AllowSorting=\"True\"  AllowPaging=\"True\" OnPageIndexChanging=\"gv{0}_PageIndexChanging\" OnSorting=\"gv{0}_Sorting\">", fileName));
        //        writer.WriteLine("                <Columns>");
        //        writer.WriteLine("                    <Founder:FISerialNumberField />");
        //        foreach (PageControl column in gridColumns)
        //        {
        //            writer.Write(string.Format("                    <asp:TemplateField HeaderText=\"{0}\" ", column.ControlDesc));
        //            if (column.Sort)
        //            {
        //                writer.Write(string.Format("SortExpression=\"{0}\"", column.ControlName));
        //            }
        //            writer.WriteLine(">");
        //            writer.Write("                        <ItemStyle VerticalAlign=\"Middle\" ");
        //            switch (column.ColumnType)
        //            {
        //                case "Int64":
        //                case "Int32":
        //                case "Decimal":
        //                    writer.WriteLine("HorizontalAlign=\"Right\"/>");
        //                    writer.Write("                        <ItemTemplate>");
        //                    writer.Write(string.Format("<%#Eval (\"{0}\") %>", column.ControlName));
        //                    break;
        //                case "DateTime":
        //                    writer.WriteLine("HorizontalAlign=\"Center\"/>");
        //                    writer.Write("                        <ItemTemplate>");
        //                    writer.Write("<%#Eval (\"" + column.ControlName + "\",\"{0:yyyy-MM-dd}\") %>");
        //                    break;
        //                default:
        //                    writer.WriteLine("HorizontalAlign=\"Left\"/>");
        //                    writer.Write("                        <ItemTemplate>");
        //                    writer.Write(string.Format("<%#Eval (\"{0}\") %>", column.ControlName));
        //                    break;
        //            }
        //            writer.WriteLine("</ItemTemplate>");
        //            writer.WriteLine("                    </asp:TemplateField>");
        //        }
        //        writer.WriteLine("                </Columns>");
        //        writer.WriteLine("            </Founder:FIGridView>");
        //        writer.WriteLine("        </div>");
        //        writer.WriteLine("    </form>");
        //        writer.WriteLine("</body>");
        //        writer.WriteLine("</html>");
        //        #endregion

        //    }
        //    #endregion

        //    #region 生成View.cs文件
        //    //生成View.cs文件
        //    filePath = string.Format("{0}\\I{1}View.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(@"/*----------------------------------------------------------------");
        //        writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //        writer.WriteLine("// 版权所有。");
        //        writer.WriteLine(string.Format("// 文   件   名：I{0}View.cs", fileName));
        //        writer.WriteLine("// 文件功能描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //        writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修 改 人：");
        //        writer.WriteLine("// 修改描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修改标识：");
        //        writer.WriteLine("//----------------------------------------------------------------*/");
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using Founder.FIS.CMD.Base;");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace Founder.FIS.CMD.IView");
        //        writer.WriteLine("{");
        //        writer.WriteLine(string.Format("    public interface I{0}View : ICMDViewBase", fileName));
        //        writer.WriteLine("    {");
        //        writer.WriteLine("        #region 属性");
        //        writer.WriteLine();
        //        foreach (PageControl pageControl in listControls)
        //        {
        //            //string type = FormHelper.GetCSharpTypeByDBType(pageControl.ColumnType);
        //            writer.WriteLine("        ///<summary>");
        //            writer.WriteLine(string.Format("        ///{0}", pageControl.ControlDesc));
        //            writer.WriteLine("        ///</summary>");
        //            writer.WriteLine("        " + pageControl.ColumnType + (FormHelper.IsNullableType(pageControl.ColumnType) ? "?" : "") + " " + pageControl.ControlName + "{ get; set; }");
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion

        //    #region 生成.cs页面
        //    string strDefaultSortExpression = string.Empty;
        //    filePath = string.Format("{0}\\{1}.aspx.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(@"/*----------------------------------------------------------------");
        //        writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //        writer.WriteLine("// 版权所有。");
        //        writer.WriteLine(string.Format("// 文   件   名：{0}.aspx.cs", fileName));
        //        writer.WriteLine("// 文件功能描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //        writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修 改 人：");
        //        writer.WriteLine("// 修改描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修改标识：");
        //        writer.WriteLine("//----------------------------------------------------------------*/");
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Collections.Generic;");
        //        writer.WriteLine("using System.Linq;");
        //        writer.WriteLine("using System.Data;");
        //        writer.WriteLine("using System.Web;");
        //        writer.WriteLine("using System.Web.Security;");
        //        writer.WriteLine("using System.Web.UI;");
        //        writer.WriteLine("using System.Web.UI.WebControls;");
        //        writer.WriteLine("using Founder.Framework.UI;");
        //        writer.WriteLine("using Founder.Framework.Web.UI;");
        //        writer.WriteLine("using Founder.Framework.Utility;");
        //        writer.WriteLine("using Founder.Framework.Common;");
        //        writer.WriteLine("using Founder.FIS.CMD.Website.Common;");
        //        writer.WriteLine("using Founder.FIS.CMD.Shared;");
        //        writer.WriteLine("using System.Data.Common;");
        //        writer.WriteLine("using System.Data.SqlClient;");
        //        writer.WriteLine("using Founder.FIS.CMD.Website.Resource.SQLPath;");
        //        writer.WriteLine("using Founder.Framework.Web.UI.WebControls;");
        //        writer.WriteLine("using Founder." + (string.IsNullOrWhiteSpace(generateInfo.Module) ? "" : generateInfo.Module + ".") + "IView;");
        //        writer.WriteLine("using Founder." + (string.IsNullOrWhiteSpace(generateInfo.Module) ? "" : generateInfo.Module + ".") + "BusinessLogic.Controller;");
        //        writer.WriteLine("using Founder." + (string.IsNullOrWhiteSpace(generateInfo.Module) ? "" : generateInfo.Module + ".") + "DataEntity;");
        //        writer.WriteLine("using Founder." + (string.IsNullOrWhiteSpace(generateInfo.Module) ? "" : generateInfo.Module + ".") + "BusinessLogic.DAL;");
        //        writer.WriteLine();
        //        writer.WriteLine(string.Format("namespace Founder.FIS.CMD.Website.Views{0}", generateInfo.Module == "" ? "" : "." + generateInfo.Module));
        //        writer.WriteLine("{");
        //        writer.WriteLine(string.Format("    public partial class {0} : CMDListPageBase, I{0}View", fileName));
        //        writer.WriteLine("    {");
        //        writer.WriteLine("        #region 字段");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 属性");
        //        writer.WriteLine();
        //        writer.WriteLine("        protected override EnumModule Module");
        //        writer.WriteLine("        {");
        //        writer.WriteLine("            get { return EnumModule.用户管理详细; }");
        //        writer.WriteLine("        }");
        //        writer.WriteLine();
        //        writer.WriteLine("        public Guid? ID { get; set; }");
        //        writer.WriteLine();
        //        foreach (PageControl pageControl in listControls)
        //        {
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        /// {0}", pageControl.ControlDesc));
        //            writer.WriteLine("        /// </summary>");
        //            writer.WriteLine(string.Format("        public {1} {0}", pageControl.ControlName, pageControl.ColumnType + (FormHelper.IsNullableType(pageControl.ColumnType) ? "?" : "")));
        //            writer.WriteLine("        {");
        //            writer.WriteLine("            get");
        //            writer.WriteLine("            {");
        //            writer.WriteLine(string.Format("                return {0}.{2}{1};", pageControl.ControlID, FormHelper.GetConvertMethodByType(pageControl.ColumnType), FormHelper.GetControlPropertyValueSetProperty(pageControl.ControlType)));
        //            writer.WriteLine("            }");
        //            writer.WriteLine("            ");
        //            writer.WriteLine("            set");
        //            writer.WriteLine("            {");
        //            writer.WriteLine(string.Format("                {0}.{1} = value.ToStringEx();", pageControl.ControlID, FormHelper.GetControlPropertyValueSetProperty(pageControl.ControlType)));
        //            writer.WriteLine("            }");
        //            writer.WriteLine("        }");
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 事件");
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        /// 页面加载");
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <param name=\"sender\"></param>");
        //        writer.WriteLine("        /// <param name=\"e\"></param>");
        //        writer.WriteLine("        protected void Page_Load(object sender, EventArgs e)");
        //        writer.WriteLine("        {");
        //        writer.WriteLine("            this.gv" + fileName + ".BindGridView += new FIGridView.FIHandler(this.BindGridView);");
        //        writer.WriteLine("            if (!IsPostBack)");
        //        writer.WriteLine("            {");
        //        writer.WriteLine("                //数据初始化");
        //        writer.WriteLine("                DataInit();");
        //        foreach (PageControl column in gridColumns)
        //        {
        //            if (column.DefaultSortExpression)
        //            {
        //                if (string.IsNullOrEmpty(strDefaultSortExpression))
        //                {
        //                    strDefaultSortExpression = column.ControlName;
        //                }
        //                else
        //                {
        //                    strDefaultSortExpression += "," + column.ControlName;
        //                }
        //            }
        //        }
        //        writer.WriteLine("                gv" + fileName + ".DefaultSortExpression = \"" + strDefaultSortExpression + "\";");
        //        writer.WriteLine("                //绑定Grid");
        //        writer.WriteLine("                BindGridView();");
        //        writer.WriteLine("            }");
        //        writer.WriteLine("        }");
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        /// 查询");
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <param name=\"sender\"></param>");
        //        writer.WriteLine("        /// <param name=\"e\"></param>");
        //        writer.WriteLine("        protected void btnQuery_Click(object sender, EventArgs e){}");
        //        if (generateInfo.IsCanExport)
        //        {
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine("        /// 导出");
        //            writer.WriteLine("        /// </summary>");
        //            writer.WriteLine("        /// <param name=\"sender\"></param>");
        //            writer.WriteLine("        /// <param name=\"e\"></param>");
        //            writer.WriteLine("        protected void btnExport_Click(object sender, EventArgs e){}");
        //        }
        //        writer.WriteLine("        protected void gv" + fileName + "_RowDataBound(object sender, GridViewRowEventArgs e){}");
        //        writer.WriteLine("        protected void gv" + fileName + "_RowCommand(object sender, GridViewCommandEventArgs e){}");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 私有方法");
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        /// 数据初始化");
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        private void DataInit()");
        //        writer.WriteLine("        {");
        //        writer.WriteLine("        }");
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        /// 绑定Grid数据");
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        private void BindGridView()");
        //        writer.WriteLine("        {");
        //        writer.WriteLine("            int rowCount, pageCount;");
        //        writer.WriteLine("");
        //        writer.WriteLine("            " + generateInfo.EntityName + " entity = new " + generateInfo.EntityName + "();");
        //        foreach (PageControl pageControl in listControls)
        //        {
        //            writer.WriteLine("            entity." + pageControl.ControlName + "=" + pageControl.ControlName + ";");
        //        }
        //        writer.WriteLine("            DataTable dt = Pagination.GetGridViewData(SystemPath.SQLPath, SystemPath.DictionaryList,");
        //        writer.WriteLine("            gv" + fileName + ".PageIndex, gv" + fileName + ".DefaultSortExpression,entity, out rowCount, out pageCount);");
        //        writer.WriteLine("            GridHelper.BindGrid(rowCount, gv" + fileName + ", dt);");
        //        writer.WriteLine("        }");
        //        writer.WriteLine("        protected override void InitControl(){}");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 公共方法");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion

        //    #region 生成.aspx.designer.cs页面
        //    filePath = string.Format("{0}\\{1}.aspx.designer.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine("//------------------------------------------------------------------------------");
        //        writer.WriteLine("// <自动生成>");
        //        writer.WriteLine("//     此代码由工具生成。");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//     对此文件的更改可能会导致不正确的行为，并且如果");
        //        writer.WriteLine("//     重新生成代码，这些更改将会丢失。");
        //        writer.WriteLine("// </自动生成>");
        //        writer.WriteLine("//------------------------------------------------------------------------------");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace Founder.FIS.CMD.Website" + (generateInfo.Module == "" ? "" : "." + generateInfo.Module) + "{");
        //        writer.WriteLine();
        //        writer.WriteLine("    public partial class " + fileName + " {");
        //        writer.WriteLine();
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        /// form1 控件。");
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <remarks>");
        //        writer.WriteLine("        /// 自动生成的字段。");
        //        writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //        writer.WriteLine("        /// </remarks>");
        //        writer.WriteLine("        protected global::System.Web.UI.HtmlControls.HtmlForm form1;");
        //        foreach (PageControl pageControl in listControls)
        //        {
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        /// {0} 控件。", pageControl.ControlID));
        //            writer.WriteLine("        /// </summary>");
        //            writer.WriteLine("        /// <remarks>");
        //            writer.WriteLine("        /// 自动生成的字段。");
        //            writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //            writer.WriteLine("        /// </remarks>");
        //            writer.WriteLine(string.Format("         protected global::Founder.Framework.Web.UI.WebControls.{0} {1};", pageControl.ControlType.ToString(), pageControl.ControlID));
        //            writer.WriteLine();
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine(string.Format("        /// btnQuery 控件。"));
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <remarks>");
        //        writer.WriteLine("        /// 自动生成的字段。");
        //        writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //        writer.WriteLine("        /// </remarks>");
        //        writer.WriteLine("        protected global::Founder.Framework.Web.UI.WebControls.FIButton btnQuery;");
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine();
        //        writer.WriteLine();
        //        writer.WriteLine(string.Format("        /// gv{0} 控件。", fileName));
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <remarks>");
        //        writer.WriteLine("        /// 自动生成的字段。");
        //        writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //        writer.WriteLine("        /// </remarks>");
        //        writer.WriteLine(string.Format("         protected global::Founder.Framework.Web.UI.WebControls.FIGridView gv{0};", fileName));
        //        writer.WriteLine();
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion
        //}

        ///// <summary>
        ///// 生成通用查询列表页面
        ///// </summary>
        ///// <param name="generateInfo">生成文件信息</param>
        ///// <param name="gridViewMain">Grid信息</param>
        //public static void GenerateCommonQueryListWebFormFile(GenerateInfo generateInfo, DataGridView gridViewMain)
        //{
        //    List<PageControl> listControls = getPageControlList(gridViewMain, EnumFilterType.列表页面);
        //    List<PageControl> gridColumns = getGridColumnsList(gridViewMain);
        //    string fileName = generateInfo.TableName.Substring(generateInfo.TableName.LastIndexOf("_", StringComparison.Ordinal) + 1) + "List";
        //    var entityName = fileName.Replace("List", "");
        //    #region 生成.aspx页面

        //    string filePath = string.Format("{0}\\{1}.aspx", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }

        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(string.Format("<%@ Page Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"{0}.aspx.cs\" Inherits=\"Founder.FIS.CMD.Website.Views{1}{0}\" %>", fileName, generateInfo.Module == "" ? "" : generateInfo.Module + "."));
        //        writer.WriteLine("<%@ Register Src=\"~/UserControls/Common/ConfigurableGrid.ascx\" TagPrefix=\"Founder\" TagName=\"ConfigurableGrid\" %>");
        //        writer.WriteLine("<!DOCTYPE html>");
        //        writer.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        //        writer.WriteLine("<head runat=\"server\">");
        //        writer.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>");
        //        writer.WriteLine(string.Format("    <title>{0}</title>", generateInfo.TableDesc + "列表"));
        //        writer.WriteLine("</head>");
        //        writer.WriteLine("<body>");
        //        writer.WriteLine("    <form id=\"form1\" runat=\"server\">");
        //        writer.WriteLine("        <div id=\"flt\" class=\"flttoolbar\">");
        //        writer.WriteLine("            <Founder:FIButton ID=\"btnQuery\" runat=\"server\" Action=\"查询\" OnClick=\"btnQuery_Click\" />");
        //        if (generateInfo.IsCanExport)
        //        {
        //            writer.WriteLine("            <Founder:FIButton ID=\"btnExport\" runat=\"server\" Action=\"导出\" OnClick=\"btnExport_Click\"/>");
        //        }
        //        writer.WriteLine("        </div>");
        //        writer.WriteLine("        <div class=\"searchtoolbar\">");
        //        writer.WriteLine("            <table >");
        //        int colCount = generateInfo.ListColumnNumber;
        //        int rowCount = listControls.Count % colCount == 0 ? listControls.Count / colCount : listControls.Count / colCount + 1;
        //        int k = 0;
        //        #region 生成查询条件区
        //        for (int i = 0; i < rowCount; i++)
        //        {
        //            if (k != listControls.Count)
        //            {
        //                writer.WriteLine("                <tr>");
        //                for (int j = 0; j < colCount; j++)
        //                {
        //                    if (k != listControls.Count)
        //                    {
        //                        writer.WriteLine("                    <th>");
        //                        writer.WriteLine(string.Format("                        <Founder:FILabel ID=\"lbl{0}\" runat=\"server\" Text=\"{1}\" />", listControls[k].ControlName, listControls[k].ControlDesc + "："));
        //                        writer.WriteLine("                    </th>");
        //                        string validateHtml = string.Empty;
        //                        if (listControls[k].ControlValidate == EnumValidateType.必填)
        //                        {
        //                            validateHtml = "Required=\"true\"";
        //                        }
        //                        else if (listControls[k].ControlValidate == EnumValidateType.必填数字)
        //                        {
        //                            validateHtml = "Required=\"true\" Type=\"Number\"";
        //                        }
        //                        else if (listControls[k].ControlValidate == EnumValidateType.必填邮箱)
        //                        {
        //                            validateHtml = "Required=\"true\" Type=\"Email\"";
        //                        }
        //                        writer.WriteLine("                    <td>");
        //                        writer.WriteLine(string.Format("                        <Founder:{1} ID=\"{0}\" runat=\"server\" {2} />", listControls[k].ControlID, listControls[k].ControlType.ToString(), validateHtml));
        //                        writer.WriteLine("                    </td>");
        //                        k++;
        //                    }
        //                }
        //                writer.WriteLine("                </tr>");
        //            }
        //        }
        //        writer.WriteLine("            </table>");
        //        writer.WriteLine("        </div>");
        //        #endregion

        //        #region 生成Grid
        //        writer.WriteLine("        <div id=\"divList\" >");
        //        writer.WriteLine(string.Format("            <Founder:ConfigurableGrid runat=\"server\" ID=\"cfGrid{0}\" />", entityName));
        //        writer.WriteLine("        </div>");
        //        writer.WriteLine("    </form>");
        //        writer.WriteLine("</body>");
        //        writer.WriteLine("</html>");
        //        #endregion

        //    }
        //    #endregion

        //    #region 生成View.cs文件
        //    //生成View.cs文件
        //    filePath = string.Format("{0}\\I{1}View.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(@"/*----------------------------------------------------------------");
        //        writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //        writer.WriteLine("// 版权所有。");
        //        writer.WriteLine(string.Format("// 文   件   名：I{0}View.cs", fileName));
        //        writer.WriteLine("// 文件功能描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //        writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修 改 人：");
        //        writer.WriteLine("// 修改描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修改标识：");
        //        writer.WriteLine("//----------------------------------------------------------------*/");
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using Founder.FIS.CMD.Base;");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace Founder.FIS.CMD.IView");
        //        writer.WriteLine("{");
        //        writer.WriteLine(string.Format("    public interface I{0}View : ICMDViewBase", fileName));
        //        writer.WriteLine("    {");
        //        writer.WriteLine("        #region 属性");
        //        writer.WriteLine();
        //        foreach (PageControl pageControl in listControls)
        //        {
        //            //string type = FormHelper.GetCSharpTypeByDBType(pageControl.ColumnType);
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        /// {0}", pageControl.ControlDesc));
        //            writer.WriteLine("        /// </summary>");
        //            writer.WriteLine("        " + pageControl.ColumnType + (FormHelper.IsNullableType(pageControl.ColumnType) ? "?" : "") + " " + pageControl.ControlName + "{ get; set; }");
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion

        //    #region 生成.cs页面
        //    string strDefaultSortExpression = string.Empty;
        //    filePath = string.Format("{0}\\{1}.aspx.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine(@"/*----------------------------------------------------------------");
        //        writer.WriteLine("// Copyright (C) 2014方正国际软件有限公司");
        //        writer.WriteLine("// 版权所有。");
        //        writer.WriteLine(string.Format("// 文   件   名：{0}.aspx.cs", fileName));
        //        writer.WriteLine("// 文件功能描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 创 建 人：" + System.Environment.UserName);
        //        writer.WriteLine("// 创建日期：" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修 改 人：");
        //        writer.WriteLine("// 修改描述：");
        //        writer.WriteLine("//");
        //        writer.WriteLine("// 修改标识：");
        //        writer.WriteLine("//----------------------------------------------------------------*/");
        //        writer.WriteLine("using System;");
        //        writer.WriteLine("using System.Web;");
        //        writer.WriteLine("using System.Web.UI;");
        //        writer.WriteLine("using System.Web.UI.WebControls;");
        //        writer.WriteLine("using Founder.FIS.CMD.BasicModule.Controller;");
        //        writer.WriteLine("using Founder.FIS.CMD.BasicModule.Controller.IView;");
        //        writer.WriteLine("using Founder.FIS.CMD.Constants.CommonQueryPath;");
        //        writer.WriteLine("using Founder.FIS.CMD.Constants.Enum;");
        //        writer.WriteLine("using Founder.FIS.CMD.Website.Shared;");
        //        writer.WriteLine("using Founder.FIS.CMD.Website.UserControls.Common;");
        //        writer.WriteLine("using Founder.Framework.Utility;");
        //        writer.WriteLine();
        //        writer.WriteLine(string.Format("namespace Founder.FIS.CMD.Website.Views{0}", generateInfo.Module == "" ? "" : "." + generateInfo.Module));
        //        writer.WriteLine("{");
        //        writer.WriteLine(string.Format("    public partial class {0} : CMDPageBase, I{0}View", fileName));
        //        writer.WriteLine("    {");
        //        writer.WriteLine("        #region 字段");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 属性");
        //        writer.WriteLine();
        //        writer.WriteLine("        protected override EnumModule Module");
        //        writer.WriteLine("        {");
        //        writer.WriteLine("            get { return EnumModule.用户管理详细; }");
        //        writer.WriteLine("        }");
        //        writer.WriteLine();
        //        writer.WriteLine("        public Guid? ID { get; set; }");
        //        writer.WriteLine();
        //        foreach (PageControl pageControl in listControls)
        //        {
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        /// {0}", pageControl.ControlDesc));
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        public {1} {0}", pageControl.ControlName, pageControl.ColumnType + (FormHelper.IsNullableType(pageControl.ColumnType) ? "?" : "")));
        //            writer.WriteLine("        {");
        //            writer.WriteLine("            get");
        //            writer.WriteLine("            {");
        //            writer.WriteLine(string.Format("                return {0}.{2}{1};", pageControl.ControlID, FormHelper.GetConvertMethodByType(pageControl.ColumnType), FormHelper.GetControlPropertyValueSetProperty(pageControl.ControlType)));
        //            writer.WriteLine("            }");
        //            writer.WriteLine("            ");
        //            writer.WriteLine("            set");
        //            writer.WriteLine("            {");
        //            writer.WriteLine(string.Format("                {0}.{1} = value.ToStringEx();", pageControl.ControlID, FormHelper.GetControlPropertyValueSetProperty(pageControl.ControlType)));
        //            writer.WriteLine("            }");
        //            writer.WriteLine("        }");
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 事件");
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        /// 初始化ConfigurableGrid");
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <param name=\"e\"></param>");
        //        writer.WriteLine("        protected override void OnPreInit(EventArgs e)");
        //        writer.WriteLine("        {");
        //        writer.WriteLine("            base.OnPreInit(e);");
        //        writer.WriteLine(string.Format("            cfGrid{0}.Key = \"{0}Grid\";", entityName));
        //        writer.WriteLine(string.Format("            cfGrid{0}.Path = GridPath.ConfigurableGridPath;", entityName));
        //        writer.WriteLine("        }");
        //        writer.WriteLine("        protected void Page_Load(object sender, EventArgs e)");
        //        writer.WriteLine("        {");
        //        writer.WriteLine(string.Format("            var parameters = new {0}ManagerController().GetParameters(this, typeof(I{1}View));", entityName, fileName));
        //        writer.WriteLine(string.Format("            cfGrid{0}.Where = parameters;", entityName));
        //        writer.WriteLine("            if (!IsPostBack){}");
        //        writer.WriteLine("        }");
        //        writer.WriteLine("        protected void btnQuery_Click(object sender, EventArgs e)");
        //        writer.WriteLine("        {");
        //        writer.WriteLine(string.Format("            cfGrid{0}.BindGridView();", entityName));
        //        writer.WriteLine("        }");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 私有方法");
        //        writer.WriteLine("        protected override void InitControl(){}");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("        #region 公共方法");
        //        writer.WriteLine("        #endregion");
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion

        //    #region 生成.aspx.designer.cs页面
        //    filePath = string.Format("{0}\\{1}.aspx.designer.cs", generateInfo.SavePath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine("//------------------------------------------------------------------------------");
        //        writer.WriteLine("// <自动生成>");
        //        writer.WriteLine("//     此代码由工具生成。");
        //        writer.WriteLine("//");
        //        writer.WriteLine("//     对此文件的更改可能会导致不正确的行为，并且如果");
        //        writer.WriteLine("//     重新生成代码，这些更改将会丢失。");
        //        writer.WriteLine("// </自动生成>");
        //        writer.WriteLine("//------------------------------------------------------------------------------");
        //        writer.WriteLine();
        //        writer.WriteLine("namespace Founder.FIS.CMD.Website.Views" + (generateInfo.Module == "" ? "" : "." + generateInfo.Module) + "{");
        //        writer.WriteLine();
        //        writer.WriteLine("    public partial class " + fileName + " {");
        //        writer.WriteLine();
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine("        /// form1 控件。");
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <remarks>");
        //        writer.WriteLine("        /// 自动生成的字段。");
        //        writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //        writer.WriteLine("        /// </remarks>");
        //        writer.WriteLine("        protected global::System.Web.UI.HtmlControls.HtmlForm form1;");
        //        foreach (PageControl pageControl in listControls)
        //        {
        //            writer.WriteLine("        /// <summary>");
        //            writer.WriteLine(string.Format("        /// {0} 控件。", pageControl.ControlID));
        //            writer.WriteLine("        /// </summary>");
        //            writer.WriteLine("        /// <remarks>");
        //            writer.WriteLine("        /// 自动生成的字段。");
        //            writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //            writer.WriteLine("        /// </remarks>");
        //            writer.WriteLine(string.Format("         protected global::Founder.Framework.Web.UI.WebControls.{0} {1};", pageControl.ControlType.ToString(), pageControl.ControlID));
        //            writer.WriteLine();
        //            writer.WriteLine();
        //        }
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine(string.Format("        /// btnQuery 控件。"));
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <remarks>");
        //        writer.WriteLine("        /// 自动生成的字段。");
        //        writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //        writer.WriteLine("        /// </remarks>");
        //        writer.WriteLine("        protected global::Founder.Framework.Web.UI.WebControls.FIButton btnQuery;");
        //        writer.WriteLine();
        //        writer.WriteLine();
        //        writer.WriteLine("        /// <summary>");
        //        writer.WriteLine(string.Format("        /// cfGrid{0} 控件。", entityName));
        //        writer.WriteLine("        /// </summary>");
        //        writer.WriteLine("        /// <remarks>");
        //        writer.WriteLine("        /// 自动生成的字段。");
        //        writer.WriteLine("        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。");
        //        writer.WriteLine("        /// </remarks>");
        //        writer.WriteLine(string.Format("         protected global::Founder.FIS.CMD.Website.UserControls.Common.ConfigurableGrid cfGrid{0};", entityName));
        //        writer.WriteLine();
        //        writer.WriteLine("    }");
        //        writer.WriteLine("}");
        //    }
        //    #endregion

        //    #region 生成GirdView.XML文件
        //    filePath = string.Format("{0}\\{1}GridView.xml", generateInfo.SavePath, entityName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //    using (StreamWriter writer = File.CreateText(filePath))
        //    {
        //        writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        //        writer.WriteLine("<xmlroot>");
        //        foreach (PageControl column in gridColumns)
        //        {
        //            if (column.DefaultSortExpression)
        //            {
        //                if (string.IsNullOrEmpty(strDefaultSortExpression))
        //                {
        //                    strDefaultSortExpression = column.ControlName;
        //                }
        //                else
        //                {
        //                    strDefaultSortExpression += "," + column.ControlName;
        //                }
        //            }
        //        }
        //        writer.WriteLine(string.Format("    <{0}Grid key=\"{0}Grid\" ShowCheckBox=\"True\" EmptyDataText=\"没有数据\" PageSize=\"10\" DataKeyNames=\"ID\" DefaultSort=\"{1}\">", entityName, strDefaultSortExpression));
        //        writer.WriteLine("        <Sql>");
        //        writer.WriteLine("        <![CDATA[");
        //        writer.WriteLine("            " + GetQuerySql(generateInfo.TableName, listControls));
        //        writer.WriteLine("        ]]>");
        //        writer.WriteLine("        </Sql>");
        //        foreach (PageControl pageControl in gridColumns)
        //        {
        //            writer.WriteLine("        <Column>");
        //            writer.WriteLine(string.Format("            <ColumnName>{0}</ColumnName>", pageControl.ControlName));
        //            writer.WriteLine(string.Format("            <HeaderText>{0}</HeaderText>", pageControl.ControlDesc));
        //            writer.WriteLine("            <ForeColor>Black</ForeColor>");
        //            writer.WriteLine("            <Width>80px</Width>");
        //            writer.WriteLine("            <OpenType></OpenType>");
        //            writer.WriteLine("            <OpenTitle></OpenTitle>");
        //            writer.WriteLine("            <OpenWidth></OpenWidth>");
        //            writer.WriteLine("            <OpenHeight></OpenHeight>");
        //            writer.WriteLine("            <URL></URL>");
        //            //writer.WriteLine("            <URL>");
        //            //writer.WriteLine("                 <Address><![CDATA[]]></Address>");
        //            //writer.WriteLine("                 <Parameter></Parameter>");
        //            //writer.WriteLine("            </URL>");
        //            writer.WriteLine(string.Format("            <Aligment>{0}</Aligment>", FormHelper.GetAligmentByType(pageControl.ColumnType)));
        //            writer.WriteLine(string.Format("            <Format>{0}</Format>", pageControl.ColumnType == "DateTime" ? "{0:yyyy-MM-dd}" : ""));
        //            writer.WriteLine("        </Column>");
        //        }
        //        writer.WriteLine("        <CommandAction>");
        //        writer.WriteLine("            <ActionName>编辑</ActionName>");
        //        writer.WriteLine("            <ActionType>Update</ActionType>");
        //        writer.WriteLine("            <ConfirmText></ConfirmText>");
        //        writer.WriteLine("            <OpenType></OpenType>");
        //        writer.WriteLine("            <OpenTitle></OpenTitle>");
        //        writer.WriteLine("            <OpenWidth>800</OpenWidth>");
        //        writer.WriteLine("            <OpenHeight>600</OpenHeight>");
        //        writer.WriteLine("            <URL>");
        //        writer.WriteLine("                <Address><![CDATA[Detail.aspx?ID={0}]]></Address>");
        //        writer.WriteLine("                <Parameter>ID</Parameter>");
        //        writer.WriteLine("            </URL>");
        //        writer.WriteLine("            <OnClientClick></OnClientClick>");
        //        writer.WriteLine("            <SkinID></SkinID>");
        //        writer.WriteLine("            <CommandArgument>");
        //        writer.WriteLine("            <CommandParameter>ID</CommandParameter>");
        //        writer.WriteLine("            </CommandArgument>");
        //        writer.WriteLine("        </CommandAction>");

        //        writer.WriteLine("        <CommandAction>");
        //        writer.WriteLine("            <ActionName>删除</ActionName>");
        //        writer.WriteLine("            <ActionType>Delete</ActionType>");
        //        writer.WriteLine("            <ConfirmText></ConfirmText>");
        //        writer.WriteLine("            <OpenType></OpenType>");
        //        writer.WriteLine("            <OpenTitle></OpenTitle>");
        //        writer.WriteLine("            <OpenWidth></OpenWidth>");
        //        writer.WriteLine("            <OpenHeight></OpenHeight>");
        //        writer.WriteLine("            <URL>");
        //        writer.WriteLine("                <Address><![CDATA[Detail.aspx?ID={0}]]></Address>");
        //        writer.WriteLine("                <Parameter>ID</Parameter>");
        //        writer.WriteLine("            </URL>");
        //        writer.WriteLine("            <OnClientClick>DeleteConfirm()</OnClientClick>");
        //        writer.WriteLine("            <SkinID></SkinID>");
        //        writer.WriteLine("            <CommandArgument>");
        //        writer.WriteLine("            <CommandParameter>ID</CommandParameter>");
        //        writer.WriteLine("            </CommandArgument>");
        //        writer.WriteLine("        </CommandAction>");
        //        writer.WriteLine(string.Format("    </{0}Grid>", entityName));
        //        writer.WriteLine("</xmlroot>");

        //    }
        //    #endregion
        //}

        ///// <summary>
        ///// 将列表查询Sql保存到剪贴板
        ///// </summary>
        ///// <param name="gridViewMain"></param>
        //public static void CopyQuerySqlToClipBoard(string tableName, DataGridView gridViewMain)
        //{

        //    List<ListItem> listItems = new List<ListItem>();
        //    foreach (DataGridViewRow row in gridViewMain.Rows)
        //    {
        //        ListItem listItem = new ListItem();
        //        listItem.Name = row.Cells["ColumnName"].Value.ToString();
        //        listItem.Value = "@" + row.Cells["ColumnName"].Value.ToString();
        //        listItems.Add(listItem);

        //    }

        //    StringBuilder sql = new StringBuilder(@"select * from " + tableName + " where 1=1");
        //    foreach (ListItem listItem in listItems)
        //    {
        //        sql.AppendLine(" and" + string.Format("({0}={1} or {2} is null)", listItem.Name, listItem.Value, listItem.Value));
        //    }

        //    Clipboard.SetDataObject(sql.ToString(), true);

        //}

        ///// <summary>
        ///// 获取通用查询SQL
        ///// </summary>
        ///// <param name="tableName">表名</param>
        ///// <param name="listControls">查询条件SQL</param>
        ///// <returns></returns>
        //private static string GetQuerySql(string tableName, List<PageControl> listControls)
        //{
        //    StringBuilder sql = new StringBuilder(@"select * from " + tableName + " where 1=1");
        //    foreach (PageControl pageControl in listControls)
        //    {
        //        sql.AppendLine(" and" + string.Format("({0}={1} or {1} is null)", pageControl.ControlName, "@" + pageControl.ControlName));
        //    }
        //    return sql.ToString();
        //}
    }
}
