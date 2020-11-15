using System;
using MediatR;

namespace Invoicer.Common
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
