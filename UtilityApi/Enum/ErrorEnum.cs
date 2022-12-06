using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.UtilityApi.Enum
{
    public enum ErrorEnum
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        LoginSuccess = 1,

        /// <summary>
        /// 登录失败
        /// </summary>
        LoginFail = -1,

        /// <summary>
        /// 退出
        /// </summary>
        LoginOut = -2,

        /// <summary>
        /// 业务逻辑代码错误
        /// </summary>
        BusinessCodeError = -3,

        /// <summary>
        /// 身份校验失败
        /// </summary>
        Unauthorized = -4,

        /// <summary>
        /// 其他错误
        /// </summary>
        OtherError = -5,
    }
}
