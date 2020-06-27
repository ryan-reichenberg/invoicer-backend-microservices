using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Invoicer.Common.Handlers;

namespace Invoicer.Common
{
    public class QueryBus : IQueryBus
    {
        private readonly IServiceProvider _serviceProvider;
        public QueryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Query<TRequest, TResponse>(TRequest query) where TRequest : IQuery<TResponse>
        {
            using (var scope = _serviceProvider.GetAutofacRoot().BeginLifetimeScope())
            {
                var handlers = scope.Resolve<IEnumerable<IQueryHandler<TRequest, TResponse>>>().ToList();
                if (handlers.Count == 1)
                {
                    return await handlers[0].Handle(query);
                }
                else if (handlers.Count == 0)
                {
                    throw new Exception($"Query does not have any handler {query.GetType().Name}");
                }
                else
                {
                    throw new Exception($"Too many registred handlers - {handlers.Count} for query: {query.GetType().Name}");
                }
            }
        }
    }
}
