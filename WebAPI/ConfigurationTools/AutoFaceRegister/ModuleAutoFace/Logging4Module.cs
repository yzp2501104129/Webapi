﻿using Autofac.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebAPI.ConfigurationTools.AutoFaceRegister.ModuleAutoFace
{
    /// <summary>
    /// 注册log4net  这段代码我也看不懂 只知道一个大概   代码是autofac官网提供的  我只是一个搬运工
    /// </summary>
    public class Logging4Module : Autofac.Module
    {
        private static void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(ILog) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, LogManager.GetLogger(instanceType), null);
            }
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(
              new[]
              {
                new ResolvedParameter(
                    (p, i) => p.ParameterType == typeof(ILog),
                    (p, i) => LogManager.GetLogger(p.Member.DeclaringType)
                ),
              });
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;

            // Handle properties.
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }
    }
}