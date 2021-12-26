using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartFormat;

namespace Invoicer.Common.CQRS.Logging.Decorators
{
    [Decorator]
    internal sealed class CommandHandlerLoggingDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly ILogger<CommandHandlerLoggingDecorator<TCommand>> _logger;
        private readonly IMessageToLogTemplateMapper _mapper;

        public CommandHandlerLoggingDecorator(ICommandHandler<TCommand> handler,
            ILogger<CommandHandlerLoggingDecorator<TCommand>> logger, IServiceProvider serviceProvider)
        {
            _handler = handler;
            _logger = logger;
            _mapper = serviceProvider.GetService<IMessageToLogTemplateMapper>() ?? new EmptyMessageToLogTemplateMapper();
        }

        public async Task HandleAsync(TCommand command)
        {
            var template = _mapper.Map(command);

            Stopwatch stopwatch = Stopwatch.StartNew();
            if (template is null)
            {
                await _handler.HandleAsync(command);
                Log(command, null, false, stopwatch.ElapsedMilliseconds);
                return;
            }

            try
            {
                Log(command, template.Before);
                await _handler.HandleAsync(command);
                Log(command, template.After, false, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                var exceptionTemplate = template.GetExceptionTemplate(ex);
                Log(command, exceptionTemplate, isError: true);
                throw;
            }
        }

        private void Log(TCommand command, string message, bool isError = false, long executionTime = -1)
        {
            if (string.IsNullOrEmpty(message))
            {
                if (executionTime != -1)
                {
                    _logger.LogInformation(Smart.Format("Executed command: {0} in {1}ms", command.GetType().Name, executionTime,
                        command));
                }

                return;
            }
            if (executionTime != -1)
            {
                message += $" - executed in {executionTime}ms";
            }

            if (isError)
            {
                _logger.LogError(Smart.Format(message, command));
            }
            else
            {
                _logger.LogInformation(Smart.Format(message, command));
            }
        }

        private class EmptyMessageToLogTemplateMapper : IMessageToLogTemplateMapper
        {
            public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class => null;
        }
    }
}