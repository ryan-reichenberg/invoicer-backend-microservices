using System;
using System.Threading.Tasks;

namespace Invoicer.Common
{
    public interface IQueryBus
    {
        Task<TResponse> Query<TRequest, TResponse>(TRequest query) where TRequest : IQuery<TResponse>;
    }
}
