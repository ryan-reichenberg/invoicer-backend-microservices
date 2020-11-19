using System;
using System.Threading.Tasks;

namespace Invoicer.Common.Busses
{
    public interface IQueryBus
    {
        Task<TResponse> Query<TResponse>(IQuery<TResponse> query);
    }
}
