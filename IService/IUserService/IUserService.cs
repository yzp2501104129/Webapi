using WebApi.Entity.Models.Login;
using WebApi.IService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.IService.IUserService
{
    public interface IUserService: IBaseService<UsersModel>
    {
        Task<List<UsersModel>> sss();
    }
}
