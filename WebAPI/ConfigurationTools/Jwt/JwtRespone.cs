using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Tools.Jwt
{
    public class JwtRespone
    {
        /// <summary>
        /// jwt加密/解密是否成功
        /// </summary>
        public bool _IsSuccess { get; set; }
        /// <summary>
        /// token值
        /// </summary>
        public string _Data { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string _Message { get; set; }
    }
}