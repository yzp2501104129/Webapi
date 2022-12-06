using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.UtilityApi.CustomizeException
{
    /// <summary>
    /// 业务逻辑自定义异常
    /// </summary>
    public class ServiceLogicException : SystemException
    {
        public ServiceLogicException(string Message) : base(Message)
        {

        }
    }
}