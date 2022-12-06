using Autofac;
using WebApi.IRepository.IUserRepository;
using WebApi.IService.IUserService;
using Repository.UserRepository;
using WebApi.Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.ConfigurationTools.AutoFaceRegister.ModuleAutoFace.LoginModule
{
    public class UserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
        }
    }
}