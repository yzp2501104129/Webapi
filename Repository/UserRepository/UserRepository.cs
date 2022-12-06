using WebApi.Entity.Models;
using WebApi.Entity.Models.Login;
using WebApi.IRepository.Base;
using WebApi.IRepository.IUserRepository;
using WebApi.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public class UserRepository : BaseRepository<UsersModel>, IUserRepository
    {
        public Task<List<UsersModel>> sss()
        {
            return BaseGetListAsync();
        }
    }
}
