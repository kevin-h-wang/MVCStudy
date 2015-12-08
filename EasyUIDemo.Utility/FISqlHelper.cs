/*----------------------------------------------------------------
// Copyright (C) 2014方正国际软件有限公司
// 版权所有。
// 文   件   名：FISqlHelper
// 文件功能描述：sql帮助类
//
// 
// 创 建 人：刘协雍
// 创建日期：2014/6/5 13:47:22
//
// 修 改 人：
// 修改描述：
//
// 修改标识：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EasyUIDemo.Utility
{
    /// <summary>
    /// SqlHelper类
    /// </summary>
    public class FISqlHelper
    {
        /// 获取sql节点
        /// </summary>
        /// <param name="doc">xml文档</param>
        /// <param name="NodeName">节点名</param>
        /// <returns>sql节点</returns>
        public static XmlNode GetSqlNode(XmlDocument doc, string NodeName)
        {
            //获取节点
            var sqlNode = doc.SelectSingleNode(NodeName);
            //sql语句特殊处理 暂时不做


            return sqlNode;
        }

        /// <summary>
        /// 解析sqlNode
        /// </summary>
        /// <param name="sqlNode">sql节点</param>
        /// <returns>解析后的sql</returns>
        public static string AnalyzeSqlNode(XmlNode sqlNode, List<IDbDataParameter> parmeters)
        {
            return AnalyzeChildSqlNode(sqlNode, parmeters).InnerText;
        }

        /// <summary>
        /// 解析sqlNode
        /// </summary>
        /// <param name="sqlNode">sql节点</param>
        /// <returns>解析后的sql</returns>
        public static XmlNode AnalyzeChildSqlNode(XmlNode sqlNode, List<IDbDataParameter> parmeters)
        {
            /*
             * <isEmpty property="ZTECntNo">d.zte_cnt_no like #ZTECntNo#</isNotEmpty>
             * <isNotEmpty property="ZTECntNo">d.zte_cnt_no like #ZTECntNo#</isNotEmpty>
             * <isEqual  property="IsShowSended" compareValue="0"></isEqual>
             * <isNotEqual property="IsShowSended" compareValue="0"></isNotEqual>
             */
            sqlNode = sqlNode.Clone();
            if (parmeters != null && parmeters.Count > 0)
            {
                //去除isEmpty
                var isEmptyList = sqlNode.SelectNodes("isEmpty");
                foreach (XmlNode node in isEmptyList)
                {
                    string nodeproperty = node.Attributes["property"].Value;
                    var p = (from o in parmeters
                             where o.ParameterName.ToLower() == nodeproperty.ToLower() || o.ParameterName.ToLower() == "@" + nodeproperty.ToLower()
                             select o).FirstOrDefault();
                    //如果没有此属性，那么去除此节点
                    //如果此属性中有值，那么也去除此节点
                    if (p == null)
                    {
                        sqlNode.ReplaceChild(AnalyzeChildSqlNode(node, parmeters), node);
                    }
                    else if (p.Value is DBNull == false)
                    {
                        sqlNode.RemoveChild(node);
                    }
                    else
                    {
                        sqlNode.ReplaceChild(AnalyzeChildSqlNode(node, parmeters), node);
                    }
                }
                //去除isNotEmpty
                var isNotEmptyList = sqlNode.SelectNodes("isNotEmpty");
                foreach (XmlNode node in isNotEmptyList)
                {
                    string nodeproperty = node.Attributes["property"].Value;
                    var p = (from o in parmeters
                             where o.ParameterName.ToLower() == nodeproperty.ToLower() || o.ParameterName.ToLower() == "@" + nodeproperty.ToLower()
                             select o).FirstOrDefault();
                    //如果没有此属性，那么去除此节点
                    //如果此属性中无值，那么也去除此节点
                    if (p == null || p.Value is DBNull)
                    {
                        sqlNode.RemoveChild(node);
                    }
                    else
                    {
                        sqlNode.ReplaceChild(AnalyzeChildSqlNode(node, parmeters), node); 
                    }
                }
                //去除isEqual
                var isEqualList = sqlNode.SelectNodes("isEqual");
                foreach (XmlNode node in isEqualList)
                {
                    string nodeProperty = node.Attributes["property"].Value;
                    string nodeValue = node.Attributes["compareValue"].Value;
                    var p = (from o in parmeters
                             where o.ParameterName.ToLower() == nodeProperty.ToLower() || o.ParameterName.ToLower() == "@" + nodeProperty.ToLower()
                             select o).FirstOrDefault();
                    //如果没有此属性，那么去除此节点
                    //如果此属性中无值，那么也去除此节点
                    if (p == null || p.Value.ToStringEx().ToLower().Equals(nodeValue.ToLower()) == false)
                    {
                        sqlNode.RemoveChild(node);
                    }
                    else
                    { 
                        sqlNode.ReplaceChild(AnalyzeChildSqlNode(node, parmeters), node);
                    }
                }
                //去除isNotEqual
                var isNotEqualList = sqlNode.SelectNodes("isNotEqual");
                foreach (XmlNode node in isNotEqualList)
                {
                    string nodeProperty = node.Attributes["property"].Value;
                    string nodeValue = node.Attributes["compareValue"].Value;
                    var p = (from o in parmeters
                             where o.ParameterName.ToLower() == nodeProperty.ToLower() || o.ParameterName.ToLower() == "@" + nodeProperty.ToLower()
                             select o).FirstOrDefault();
                    //如果没有此属性，那么去除此节点
                    //如果此属性中无值，那么也去除此节点
                    if (p == null || p.Value.ToStringEx().ToLower().Equals(nodeValue.ToLower()))
                    {
                        sqlNode.RemoveChild(node);
                    }
                    else
                    { 
                        sqlNode.ReplaceChild(AnalyzeChildSqlNode(node, parmeters), node);
                    }
                }
            }
            return sqlNode;
        }
    }
}
