using WebApi.Entity.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebAPI.ConfigurationTools.Log4
{
    public static class Log4Helper
    {
        private static Log4Model log4Model = null;
        private static ILog _log4 = null;
        private static readonly string Logger = ConfigurationManager.AppSettings["ADONetAppender"];

        public static void Debug(string Exception, string UserId, string IP, string menuID, string operaterType, string Message, string RequestUrl)
        {
            _log4 = LogManager.GetLogger(Logger);
            if (_log4.IsDebugEnabled)
            {
                log4Model = new Log4Model(Exception, UserId, IP, Message, menuID, operaterType, RequestUrl);
                _log4.Debug(log4Model);
            }
        }
        public static void Error(string Exception, string UserId, string IP, string menuID, string operaterType, string Message,string RequestUrl)
        {
            _log4 = LogManager.GetLogger(Logger);
            if (_log4.IsErrorEnabled)
            {
                log4Model = new Log4Model(Exception, UserId, IP, Message, menuID, operaterType, RequestUrl);
                _log4.Error(log4Model);
            }
        }
        public static void Fatal(string Exception, string UserId, string IP, string menuID, string operaterType, string Message, string RequestUrl)
        {
            _log4 = LogManager.GetLogger(Logger);
            if (_log4.IsFatalEnabled)
            {
                log4Model = new Log4Model(Exception, UserId, IP, Message, menuID, operaterType, RequestUrl);
                _log4.Fatal(log4Model);
            }
        }
        public static void Info(string Exception, string UserId, string IP, string menuID, string operaterType, string Message, string RequestUrl)
        {
            _log4 = LogManager.GetLogger(Logger);
            if (_log4.IsInfoEnabled)
            {
                log4Model = new Log4Model(Exception, UserId, IP, Message, menuID, operaterType, RequestUrl);
                _log4.Info(log4Model);
            }
        }
    }
}