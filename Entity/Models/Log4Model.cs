using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Entity.Models
{
    public class Log4Model
    {
        #region 需要字段再加

        #endregion
        public Log4Model(string exception, string userId, string ip, string message, string menuID, string operaterType, string requesturl)
        {
            Exception = exception;
            UserID = userId;
            IP = ip;
            Message = message;
            MenuID = menuID;
            OperaterType = operaterType;
            RequestUrl = requesturl;
        }
        public string Exception { get; set; }
        public string UserID { get; set; }
        public string IP { get; set; }
        public string Message { get; set; }
        public string MenuID { get; set; }
        public string OperaterType { get; set; }

        public string RequestUrl { get; set; }
    }
}