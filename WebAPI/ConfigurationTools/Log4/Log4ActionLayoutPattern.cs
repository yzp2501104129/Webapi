using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.ConfigurationTools.Log4
{
    public class Log4ActionLayoutPattern : PatternLayout
    {
        public Log4ActionLayoutPattern()
        {
            this.AddConverter("Log4ActionInfo", typeof(Log4ActionConverter));
        }
    }
}