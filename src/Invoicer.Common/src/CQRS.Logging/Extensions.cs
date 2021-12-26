using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Invoicer.Common.CQRS.Logging.Decorators;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

// Fork of https://github.com/snatch-dev/Convey/tree/master/src/Convey.Logging.CQRS/
namespace Invoicer.Common.CQRS.Logging
{
    public static class Extensions
    {
        public static IConveyBuilder AddCommandHandlersLogging(this IConveyBuilder builder, Assembly assembly = null)
            => builder.AddHandlerLogging(typeof(ICommandHandler<>), typeof(CommandHandlerLoggingDecorator<>), assembly);

        public static IConveyBuilder AddEventHandlersLogging(this IConveyBuilder builder, Assembly assembly = null)
            => builder.AddHandlerLogging(typeof(IEventHandler<>), typeof(EventHandlerLoggingDecorator<>), assembly);
        
        public static IConveyBuilder AddQueryHandlersLogging(this IConveyBuilder builder, Assembly assembly = null)
            => builder.AddHandlerLogging(typeof(IQueryHandler<,>), typeof(QueryHandlerLoggingDecorator<,>), assembly);
        
        private static IConveyBuilder AddHandlerLogging(this IConveyBuilder builder, Type handlerType,
            Type decoratorType, Assembly assembly = null)
        {
            assembly ??= Assembly.GetCallingAssembly();

            var handlers = assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType))
                .ToList();
            
            handlers.ForEach(ch => GetExtensionMethods()
                .FirstOrDefault(mi => !mi.IsGenericMethod && mi.Name == "TryDecorate")?
                .Invoke(builder.Services, new object[]
                {
                    builder.Services,
                    ch.GetInterfaces().FirstOrDefault(),
                    decoratorType.MakeGenericType(ch.GetInterfaces().FirstOrDefault()?.GenericTypeArguments)
                }));

            return builder;
        }

        private static IEnumerable<MethodInfo> GetExtensionMethods()
        {
            var types = typeof(ReplacementBehavior).Assembly.GetTypes();

            var query = from type in types
                where type.IsSealed && !type.IsGenericType && !type.IsNested
                from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                where method.IsDefined(typeof(ExtensionAttribute), false)
                where method.GetParameters()[0].ParameterType == typeof(IServiceCollection)
                select method;
            return query;
        }
    }
}