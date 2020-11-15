using System;
using System.Threading.Tasks;
using MediatR;

namespace Invoicer.Common.Handlers
{
    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {
    }
}
