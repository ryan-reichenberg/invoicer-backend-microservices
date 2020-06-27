using System;
using System.Reflection;
using Autofac;

namespace Invoicer.Common
{
    public class InitializeModule : Autofac.Module
    {
        public Assembly assembly { get; set; }
        public InitializeModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<CommandBus>()
                .As<ICommandBus>()
                .SingleInstance();

            builder.RegisterType<QueryBus>()
                .As<IQueryBus>()
                .SingleInstance();

            builder.RegisterModule(new RepositoryModule()
            {
                ExecutingAssembly = assembly
            });
            builder.RegisterModule(new CommandBusModule()
            {
                ExecutingAssembly = assembly
            });
            builder.RegisterModule(new QueryBusModule()
            {
                ExecutingAssembly = assembly
            });

        }
    }
}
