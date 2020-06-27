using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Invoicer.Common.Handlers;

namespace Invoicer.Common
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;  
        }
        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            using (var scope = _serviceProvider.GetAutofacRoot(). BeginLifetimeScope())
            { 
                var handlers = scope.Resolve<IEnumerable<ICommandHandler<TCommand>>>().ToList();
                if (handlers.Count == 1)
                {
                    handlers[0].Handle(command);
                }
                else if (handlers.Count == 0)
                {
                    throw new Exception($"Command does not have any handler {command.GetType().Name}");
                }
                else
                {
                    throw new Exception($"Too many registred handlers - {handlers.Count} for command {command.GetType().Name}");
                }
            }
        }
    }
}
