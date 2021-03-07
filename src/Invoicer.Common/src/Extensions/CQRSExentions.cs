using System;
using Invoicer.Common.Dispatchers;
using Invoicer.Common.Handlers;
using Invoicer.Common.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common.Extensions
{
    public static class QueryHandlerExtensions
    {
        public static IInitializationContainer AddCqrs(this IInitializationContainer container)
        {
            container.Services.Scan(scan =>  
                scan.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                        .WithoutAttribute(typeof(DecoratorAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
                        .WithoutAttribute(typeof(DecoratorAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                        .WithoutAttribute(typeof(DecoratorAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            container.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            return container;

        }
    }
}