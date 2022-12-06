using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Tools.UserInformations
{
    public interface IUserInformations
    {
        ClaimsIdentity Infomations { get; set; }
    }
}
