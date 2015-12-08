using System;

namespace EasyUIDemo.Utility
{
    /// <summary>
    ///     结果类，存放方法返回值
    /// </summary>
    [Serializable]
    public class FIReturnInfo
    {

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="isSucceed"></param>
        public FIReturnInfo(bool isSucceed = true)
        {
            IsSucceed = isSucceed;
        }


        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="isSucceed">是否成功</param>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        /// <param name="description">描述</param>
        public FIReturnInfo(bool isSucceed, string message, Exception exception = null, string description = null)
        {
            IsSucceed = isSucceed;
            Message = message;
            Exception = exception;
            Description = description;
        }

        /// <summary>
        ///     成功
        /// </summary>
        public static FIReturnInfo TRUE
        {
            get
            {
                //if (_true == null)
                //{
                //    _true = new FIReturnInfo(true);
                //}
                return new FIReturnInfo(true);
            }
        }

        /// <summary>
        ///     失败
        /// </summary>
        public static FIReturnInfo FALSE
        {
            get
            {
                //if (_false == null)
                //{
                //    _false = new FIReturnInfo(false);
                //}
                return new FIReturnInfo(false);
            }
        }

        /// <summary>
        ///     是否成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        ///     信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     携带异常
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///     详细描述
        /// </summary>
        public string Description { get; set; }
    }
}