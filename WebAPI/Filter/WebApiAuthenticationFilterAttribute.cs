using WebApi.Entity.Models.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPI.Tools;
using WebAPI.Tools.Jwt;
using WebAPI.Tools.UserInformations;

namespace WebAPI.Filter
{
    /// <summary>
    /// 身份校验过滤器 jwt
    /// </summary>
    public class WebApiAuthenticationFilterAttribute : IAuthorizationFilter
    {
        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (((ReflectedHttpActionDescriptor)actionContext.ActionDescriptor).MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                actionContext.ActionDescriptor.ControllerDescriptor.ControllerType.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return continuation();
            }
            if (actionContext.Request.Headers.TryGetValues("Authentization", out IEnumerable<string> Header))
            {
                string Token = Header.First();
                JwtRespone jwtRespone = JwtTools.CheckJwtDecode(Token, out UsersModel usersInfomations);
                if (jwtRespone._IsSuccess)
                {
                    (actionContext.ControllerContext.Controller as ApiController).User = new ClaimsPrincipal(usersInfomations);
                    return continuation();
                }
                else
                {
                    return Task.FromResult(new AuthenticationFaileResult(actionContext.Request, jwtRespone._Message).Execute());
                }
            }
            return Task.FromResult(new AuthenticationFaileResult(actionContext.Request, "你没有权限访问该接口").Execute());
        }
    }
}