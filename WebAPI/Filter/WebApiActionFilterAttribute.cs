using Entity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApi.UtilityApi.EncryptionDecryption;
using WebAPI.Base;

namespace WebAPI.Filter
{
    /// <summary>
    /// WebApi动作过滤器
    /// </summary>
    public class WebApiActionFilterAttribute : ActionFilterAttribute
    {
        public static readonly string SecretKey = ConfigurationManager.AppSettings["DesSecretKey"].ToString();

        /// <summary>
        ///为防止Sql注入，过滤器开始拦截
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //获取参数描述
            var ParametersDescriptor = actionContext.ActionDescriptor.GetParameters();
            //获取参数
            IDictionary<string, object> ParametersList = actionContext.ActionArguments;
            foreach (var p in ParametersDescriptor)
            {
                var value = ParametersList[p.ParameterName];

                var pType = p.ParameterType;

                if (value == null)
                {
                    continue;
                }
                //如果不是值类型或接口，不需要过滤
                if (!pType.IsClass) continue;

                if (value is string)
                {
                    //对string类型过滤
                    ParametersList[p.ParameterName] = AntiSqlInject.Instance.GetSafetySql(value.ToString());
                }
                else
                {
                    //是一个class，对class的属性中，string类型的属性进行过滤
                    var properties = pType.GetProperties();
                    foreach (var pp in properties)
                    {
                        var temp = pp.GetValue(value);
                        if (temp == null)
                        {
                            continue;
                        }
                        pp.SetValue(value, temp is string ? AntiSqlInject.Instance.GetSafetySql(temp.ToString()) : temp);
                    }
                }
            }
            base.OnActionExecuting(actionContext);
        }


        /// <summary>
        /// 统一DES加密Json字符串
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                string Result = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
                DataSoureResponeModel dataSoureRespone = new DataSoureResponeModel()
                {
                    Code = (int)System.Net.HttpStatusCode.OK,
                    Data = String.IsNullOrWhiteSpace(Result) == true ? null : DESEncrypt.Encrypt(Result, SecretKey),
                    Message = String.Empty
                };
                actionExecutedContext.Response = new System.Net.Http.HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(dataSoureRespone), Encoding.UTF8, "application/json"),
                };
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}