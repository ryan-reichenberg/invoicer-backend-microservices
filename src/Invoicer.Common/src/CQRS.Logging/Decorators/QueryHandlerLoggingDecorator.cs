using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartFormat;

namespace Invoicer.Common.CQRS.Logging.Decorators
{
    [Decorator]
    internal sealed class QueryHandlerLoggingDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _handler;
        private readonly ILogger<QueryHandlerLoggingDecorator<TQuery, TResult>> _logger;
        private readonly IMessageToLogTemplateMapper _mapper;

        public QueryHandlerLoggingDecorator(IQueryHandler<TQuery, TResult> handler,
            ILogger<QueryHandlerLoggingDecorator<TQuery, TResult>> logger, IServiceProvider serviceProvider)
        {
            _handler = handler;
            _logger = logger;
            _mapper = serviceProvider.GetService<IMessageToLogTemplateMapper>() ?? new EmptyMessageToLogTemplateMapper();
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            var template = _mapper.Map(query);
            Stopwatch stopwatch = Stopwatch.StartNew();
            TResult result;
            if (template is null)
            {
                result = await _handler.HandleAsync(query);
                Log(query, null, false, stopwatch.ElapsedMilliseconds);
                return result;
            }

            try
            {
                Log(query, template.Before);
                result = await _handler.HandleAsync(query);
                Log(query, template.After, false, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                var exceptionTemplate = template.GetExceptionTemplate(ex);
                Log(query, exceptionTemplate, isError: true);
                throw;
            }

            return result;
        }

        private void Log(TQuery query, string message, bool isError = false, long executionTime = -1)
        {
            if (string.IsNullOrEmpty(message))
            {
                if (executionTime != -1)
                {
                    _logger.LogInformation(
                        Smart.Format("Executed query: {0} in {1}ms", query.GetType().Name, executionTime,query));
                }

                return;
            }
            if (executionTime != -1)
            {
                message += $" - executed in {executionTime}ms";
            }

            if (isError)
            {
                _logger.LogError(Smart.Format(message, query));
            }
            else
            {
                _logger.LogInformation(Smart.Format(message, query));
            }
        }

        private class EmptyMessageToLogTemplateMapper : IMessageToLogTemplateMapper
        {
            public HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class => null;
        }
    }
}