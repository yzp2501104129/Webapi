using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WebAPI.ConfigurationTools.AutoMapper;

namespace WebAPI.ConfigurationTools.AutoFaceRegister.ModuleAutoFace
{
    /// <summary>
    /// AutoMapper 官网https://docs.automapper.org/en/stable/Dependency-injection.html 集成autofac注入代码
    /// </summary>
    public class AutoMapperModule : Autofac.Module
    {
        private readonly IEnumerable<Assembly> assembliesToScan;

        public AutoMapperModule(IEnumerable<Assembly> assembliesToScan) => this.assembliesToScan = assembliesToScan;

        public AutoMapperModule(params Assembly[] assembliesToScan) : this((IEnumerable<Assembly>)assembliesToScan) { }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            /**********************
             * 程序集 Assembly：一个类库里面的所有程序，比如 WebAPI, Entity，IRepository，IService，Repository，Service，UtilityApi都是程序集
             * 程序集里面存在着：你在里面定义的所有class Attributes interface Filed Properties...可以通过反射拿到
             * 
             * 
             * 
             * 
             */

            var assembliesToScan = this.assembliesToScan as Assembly[] ?? this.assembliesToScan.ToArray();

            var allTypes = assembliesToScan
                          .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                          .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                          .SelectMany(a => a.DefinedTypes)
                          .Where(w => w.IsAssignableFrom(typeof(IProfile)))//默认继承IProfile,排除不需要configuration的实例
                          .ToArray();


            var openTypes = new[] {
                        typeof(IValueResolver<,,>),
                        typeof(IMemberValueResolver<,,,>),
                        typeof(ITypeConverter<,>),
                        typeof(IValueConverter<,>),
                        typeof(IMappingAction<,>)
            };

            foreach (var type in openTypes.SelectMany(openType =>
                 allTypes.Where(t => t.IsClass && !t.IsAbstract && ImplementsGenericInterface(t.AsType(), openType))))
            {
                builder.RegisterType(type.AsType()).InstancePerDependency();
            }


            //configuration配置
            builder.Register<IConfigurationProvider>(ctx =>
                new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(assembliesToScan);
                    cfg.AllowNullCollections = true;//允许空集合
                })
            );

            builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();
        }

        private static bool ImplementsGenericInterface(Type type, Type interfaceType)
                  => IsGenericType(type, interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => IsGenericType(@interface, interfaceType));

        private static bool IsGenericType(Type type, Type genericType)
                   => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}