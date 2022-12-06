using WebApi.Entity.Models.Login;
using WebApi.IRepository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.IRepository.IUserRepository
{
    public interface IUserRepository : IBaseRepository<UsersModel>
    {
        Task<List<UsersModel>> sss();
    }
}
