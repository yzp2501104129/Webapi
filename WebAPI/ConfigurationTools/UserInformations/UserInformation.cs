using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Tools.UserInformations
{
    public class UserInformation : IUserInformations
    {
        public ClaimsIdentity Infomations
        {
            get
            {
                return (ClaimsIdentity)HttpContext.Current.User.Identity;
            }
            set { }
        }
    }
}