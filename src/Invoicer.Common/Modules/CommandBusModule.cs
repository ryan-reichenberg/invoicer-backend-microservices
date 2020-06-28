using System.Reflection;
using Autofac;
using Invoicer.Common.Handlers;

namespace Invoicer.Common
{
    public class CommandBusModule : Autofac.Module
    {
        public Assembly ExecutingAssembly { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ExecutingAssembly)
                 .Where(x => x.IsAssignableTo<ICommandHandler>())
                 .AsImplementedInterfaces();
        }
    }
}
