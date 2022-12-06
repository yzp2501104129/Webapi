using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.UtilityApi.Attributes.SwaggerAttribute
{
    /// <summary>
    /// 当控制器中的一些接口不希望暴露到swagger上 标记当前这个特性【限制只能标记方法】
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HiddenApiAttribute:Attribute
    {
         
    }
}