using System;
using System.Threading.Tasks;
using MediatR;

namespace Invoicer.Common.Handlers
{
    public interface ICommandHandler { }
    public interface ICommandHandler<in T> : IRequestHandler<T, CommandResult> where T : ICommand
    {
    }
}
