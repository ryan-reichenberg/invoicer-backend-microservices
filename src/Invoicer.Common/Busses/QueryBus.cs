using System;
using System.Threading.Tasks;
using MediatR;

namespace Invoicer.Common
{
    public class QueryBus : IQueryBus
    {
        private readonly IMediator mediator;
        public QueryBus(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public async Task<TResponse> Query<TResponse>(IQuery<TResponse> query)
        {
            return await mediator.Send(query);
        }
    }
}
