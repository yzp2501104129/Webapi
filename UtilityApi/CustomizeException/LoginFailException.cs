using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.UtilityApi.CustomizeException
{
    /// <summary>
    /// 登录自定义异常
    /// </summary>
    public class LoginFailException : SystemException
    {
        public LoginFailException(string Message) : base(Message)
        {



        }
    }
}