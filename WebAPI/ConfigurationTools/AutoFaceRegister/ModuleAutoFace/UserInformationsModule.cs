using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Tools.UserInformations;

namespace WebAPI.ConfigurationTools.AutoFaceRegister.ModuleAutoFace
{
    public class UserInformationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserInformation>().As<IUserInformations>();
        }
    }
}