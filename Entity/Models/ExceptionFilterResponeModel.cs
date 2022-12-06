using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Entity.Models
{
    public class ExceptionFilterResponeModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ExceptionMsg { get; set; }

        /// <summary>
        /// 异常编码
        /// </summary>
        public int Code { get; set; }
    }
}