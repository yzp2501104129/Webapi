using WebApi.Entity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using WebApi.UtilityApi.CustomizeException;
using WebApi.UtilityApi.Enum;
using WebAPI.ConfigurationTools.Log4;
using WebAPI.Tools.UserInformations;
using Microsoft.Owin;

namespace WebAPI.Filter
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 重写OnException方法
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            int EnumCode = 0;
            //当前登录人用户名
            string UserName = String.Empty;
            //异常消息
            string ExMessage = actionExecutedContext.Exception.Message;
            //异常
            string StackTrac = actionExecutedContext.Exception.StackTrace;
            StackTrac = StackTrac.Length > 5000 ? StackTrac.Substring(0, 5000) : StackTrac;
            //请求URL
            string RequestUrl = actionExecutedContext.Request.RequestUri.AbsoluteUri;
            //ip地址
            string Ip =  HttpContext.Current.Request.UserHostAddress;

            if (actionExecutedContext.Exception is LoginFailException)
            {
                EnumCode = (int)ErrorEnum.LoginFail;
            }
            else
            {
                UserName = ((ClaimsIdentity)HttpContext.Current.User.Identity).Name;
                if (actionExecutedContext.Exception is ServiceLogicException)
                {
                    EnumCode = (int)ErrorEnum.BusinessCodeError;
                }
                else { EnumCode = (int)ErrorEnum.OtherError; }
            }


            ExceptionFilterResponeModel exception = new ExceptionFilterResponeModel()
            {
                Message = ExMessage,
                ExceptionMsg = StackTrac,
                Code = EnumCode,
            };
            Log4Helper.Error(StackTrac, UserName, Ip, "1", "1", ExMessage, RequestUrl);
            actionExecutedContext.ActionContext.Response = ReturnMsg(exception, HttpStatusCode.InternalServerError);
            base.OnException(actionExecutedContext);
        }

        /// <summary>
        /// Return HttpResponseMessage
        /// </summary>
        /// <param name="respone"></param>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        public static HttpResponseMessage ReturnMsg(ExceptionFilterResponeModel respone, HttpStatusCode httpStatusCode)
        {
            string ResponeContent = JsonConvert.SerializeObject(respone);
            StringContent content = new StringContent(ResponeContent, Encoding.UTF8, "application/json");

            return new HttpResponseMessage()
            {
                StatusCode = httpStatusCode,
                Content = content
            };
        }

    }
}