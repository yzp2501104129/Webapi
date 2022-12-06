using AutoMapper;
using WebApi.Entity.Dto.Users;
using WebApi.Entity.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.ConfigurationTools.AutoMapper.UserProfile
{
    public class UserProfile : Profile, IProfile
    {
        public UserProfile()
        {
            CreateMap<UsersModel,UsersDto>();
        }
    }
}