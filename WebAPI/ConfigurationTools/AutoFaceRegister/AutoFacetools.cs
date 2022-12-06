using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using WebAPI.ConfigurationTools.AutoFaceRegister;
using WebAPI.ConfigurationTools.AutoMapper;

namespace WebAPI.ConfigurationTools.AutoFaceRegister
{
    public static class AutoFacetools
    {
        public static void LoadAufaceConfig()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //注册Api容器需要使用HttPConfiguration对象
            HttpConfiguration config = GlobalConfiguration.Configuration;

            //注册所有的ApiControllers
            builder.RegisterApiControllers(Assembly.Load("WebAPI"))
                                        .Where(x => x.Name.EndsWith("Controller"));

            //模块注册
            builder.RegisterModule<BaseRegister>();

            var container = builder.Build();

            // api的控制器对象由autofac来创建
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}