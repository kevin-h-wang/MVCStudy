/*----------------------------------------------------------------
// Copyright (C) 2014方正国际软件有限公司
// 版权所有。
// 文   件   名：FIException
// 文件功能描述：
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyUIDemo.Utility
{
    //todo 扩展用，事务用
    public class FIException : ApplicationException
    {
        #region 字段


        #endregion

        #region 属性

        public string Message { get; set; }

        public EnumFIExceptionType ExceptionType
        {
            get;
            set;
        }
        #endregion

        #region 构造方法

        public FIException(string message, EnumFIExceptionType exceptionType)
        {
            this.ExceptionType = exceptionType;
            this.Message = message;
        }
        #endregion

        #region 私有方法


        #endregion

        #region 公共方法


        #endregion

    }

    /// <summary>
    /// 异常类型
    /// </summary>
    public enum EnumFIExceptionType
    {
        主键冲突异常 = 1,

        操作不成功 = 2,

        未按照要求给定参数 = 3,

        程序配置存在问题 = 4,

        外键冲突异常 = 5
    }
}
