using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.ConfigurationTools.AutoFaceRegister.ModuleAutoFace;
using WebAPI.ConfigurationTools.AutoFaceRegister.ModuleAutoFace.LoginModule;

namespace WebAPI.ConfigurationTools.AutoFaceRegister
{
    /// <summary>
    /// 模块注册-统一注册所有ModuleAutoFace模块
    /// </summary>
    public class BaseRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region 业务模块注册
            builder.RegisterModule<UserModule>();
            #endregion



            #region Log4net模块注册
            builder.RegisterModule<Logging4Module>();
            #endregion
            #region AutoMapper模块注入
            builder.RegisterModule(new AutoMapperModule(System.Reflection.Assembly.GetExecutingAssembly()));
            #endregion
            #region 当前登陆人身份信息注册
            builder.RegisterModule<UserInformationsModule>();
            #endregion
        }
    }
}