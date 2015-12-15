using EasyUIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace EasyUIDemo.MVC.Controllers
{
    /// <summary>
    /// 所有需要进行登录控制的控制器基类
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前登录的用户属性
        /// </summary>
        public UserInfo CurrentUserInfo { get; set; }

        /// <summary>
        /// 重新基类在Action执行之前的事情
        /// </summary>
        /// <param name="filterContext">重写方法的参数</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //得到用户登录的信息
            CurrentUserInfo = Session["UserInfo"] as UserInfo;

            //判断用户是否为空
            if (CurrentUserInfo == null)
            {
                Response.Redirect(Url.Content("~/Login/Login"));
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //错误记录
            //WHC.Framework.Commons.LogTextHelper.Error(filterContext.Exception);

            // 当自定义显示错误 mode = On，显示友好错误页面
            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                filterContext.ExceptionHandled = true;
                this.View("Error").ExecuteResult(this.ControllerContext);
            }
        }
    }
}