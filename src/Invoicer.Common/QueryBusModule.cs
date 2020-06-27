using System;
using System.Reflection;
using Autofac;
using Invoicer.Common.DataAccess;
using Invoicer.Common.Handlers;

namespace Invoicer.Common
{
    public class QueryBusModule : Autofac.Module
    {
        public Assembly ExecutingAssembly { get; set; }
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(ExecutingAssembly)
                .Where(x => x.IsClosedTypeOf(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces();

      

        }
    }
}
