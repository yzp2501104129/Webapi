using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebAPI.Tools.Jwt;

namespace WebAPI.ConfigurationTools.UserInformations.RoleAttributes
{
    /// <summary>
    /// 接口访问角色授权特性
    /// </summary>
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 角色类型
        /// </summary>
        public string Roles;
        /// <summary>
        /// 用户
        /// </summary>
        public string Users;

        /// <summary>
        /// 开始授权
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            IPrincipal principal = actionContext.ControllerContext.RequestContext.Principal;
            if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    return principal.IsInRole(Roles);
                }
                if (!string.IsNullOrEmpty(Users))
                {
                    return IsInUsers(Users);
                }
            }
            return false;
        }

        /// <summary>
        /// 授权失败统一返回
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = RoleFailResponse();
            //base.HandleUnauthorizedRequest(actionContext);
        }

        /// <summary>
        ///  Http响应统一处理
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage RoleFailResponse()
        {
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Content = new StringContent(JsonConvert.SerializeObject(new JwtRespone()
                {
                    _Data = null,
                    _IsSuccess = false,
                    _Message = "授权失败"
                }), Encoding.UTF8),
            };
        }

        /// <summary>
        ///  用户信息授权
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        private static bool IsInUsers(string Users)
        {
            if (!string.IsNullOrEmpty(Users))
            {
                string Name = ((ClaimsIdentity)HttpContext.Current.User.Identity).Name;
                string[] RoleType = Users.Split(',');
                foreach (string item in RoleType)
                {
                    if (item.Equals(Name))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}