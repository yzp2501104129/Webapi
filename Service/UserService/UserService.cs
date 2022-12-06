using WebApi.Entity.Models;
using WebApi.Entity.Models.Login;
using WebApi.IRepository.Base;
using WebApi.IRepository.IUserRepository;
using WebApi.IService.IUserService;
using WebApi.Service.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Service.UserService
{
    public class UserService : BaseService<UsersModel>, IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository _baseRepository) : base(_baseRepository)
        {
            userRepository = _baseRepository;
        }

        public async Task<List<UsersModel>> sss()
        {
            return await userRepository.sss();
        }
    }
}
