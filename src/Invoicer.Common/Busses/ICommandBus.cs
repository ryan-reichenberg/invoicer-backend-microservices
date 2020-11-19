using System;
using System.Threading.Tasks;

namespace Invoicer.Common.Busses
{
    public interface ICommandBus
    {
        Task<CommandResult> Send<T>(T command) where T : ICommand;
    }
}
