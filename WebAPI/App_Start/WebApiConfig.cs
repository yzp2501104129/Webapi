using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Base;
using WebAPI.Filter;

namespace WebAPI
{
    public static class WebApiConfig
    {
        // Web API 配置和服务
        public static void Register(HttpConfiguration config)
        {
            #region WebApi默认返回格式为 json  
            var json = config.Formatters.JsonFormatter;

            // 解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 移除XML序列化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //设置序列化方式为驼峰命名法
            var jsonFormatter = config.Formatters.OfType<System.Net.Http.Formatting.JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            #endregion

            #region 是否允许跨域  config.EnableCors(new EnableCorsAttribute("*", "*", "*")) 特性[EnableCors(origins网站域名: "*", headers请求头: "*", methods方法: "*")]
            var allowOrigins = ConfigurationManager.AppSettings["cors_allowOrigins"];
            var allowHeaders = ConfigurationManager.AppSettings["cors_allowHeaders"];
            var allowMethods = ConfigurationManager.AppSettings["cors_allowMethods"];
            var globalCors = new EnableCorsAttribute(allowOrigins, allowHeaders, allowMethods);
            config.EnableCors(globalCors);
            #endregion
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //全局注册身份认证过滤器
            config.Filters.Add(new WebApiAuthenticationFilterAttribute());
            //全局异常过滤器
            config.Filters.Add(new WebApiExceptionFilterAttribute());
            //全局动作过滤器
            config.Filters.Add(new WebApiActionFilterAttribute());
        }
    }
}
