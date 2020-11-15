using System;
using MediatR;

namespace Invoicer.Common
{
    public interface ICommand : IRequest<CommandResult>
    {
    }
}
