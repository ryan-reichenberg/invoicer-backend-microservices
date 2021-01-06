using System;
using Invoicer.Common.Dispatchers;
using Invoicer.Common.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common.Extensions
{
    public static class QueryHandlerExtensions
    {
        public static void AddQueryHandlers(this IServiceCollection services)
        {
            services.Scan(scan =>  
                scan.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        }
    }
}