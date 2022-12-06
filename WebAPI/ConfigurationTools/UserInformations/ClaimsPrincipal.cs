using WebApi.Entity.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebAPI.Tools.UserInformations
{
    /// <summary>
    /// 授权
    /// </summary>
    public class ClaimsPrincipal : IPrincipal
    {
        public ClaimsPrincipal(UsersModel usersModel)
        {
            Identity = new ClaimsIdentity(usersModel);
        }
        public IIdentity Identity { get; }


        /// <summary>
        /// 校验权限
        /// </summary>
        /// <param name="role">身份类型</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                string AuthenticationType = ((ClaimsIdentity)HttpContext.Current.User.Identity).AuthenticationType;
                string[] UsersType = AuthenticationType.Split(',');
                string[] RoleType = role.Split(',');
                foreach (string item in UsersType)
                {
                    if (RoleType.Contains(item))
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