using System;
using System.Reflection;
using Autofac;
using Invoicer.Common.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.Common
{
    public class RepositoryModule : Autofac.Module
    {
        public Assembly ExecutingAssembly { get; set; }
        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ExecutingAssembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ExecutingAssembly)
                .AsClosedTypesOf(typeof(IDbContext<>))
                .AsSelf()
                .InstancePerDependency();

        }
    }
}
