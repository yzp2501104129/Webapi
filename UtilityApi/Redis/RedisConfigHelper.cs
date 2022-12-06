using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApi.UtilityApi.Tools.Redis
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class RedisConfigHelper
    {
        private static readonly string RedisIp = ConfigurationManager.AppSettings["RedisIp"];
        /// <summary>
        /// 读写服务器
        /// </summary>
        public static string ReadWriteHosts
        {
            get
            {
                return RedisIp;
                //<add key="ReadWriteHosts" value="127.0.0.1:6379" />
            }
        }

        /// <summary>
        /// 只读服务器
        /// </summary>
        /// <returns></returns>
        public static string ReadOnlyHosts
        {
            get
            {
                return "";
                //<add key="ReadOnlyHosts" value="127.0.0.1:6379" />
            }
        }

        /// <summary>
        /// 服务项目验证GUID 的过期时间以秒为单位
        /// </summary>
        public static int GuidPastTime
        {
            get
            {
                return Convert.ToInt32("5000");
                //<add key="GuidPastTime" value="5000" />
            }
        }
    }
}