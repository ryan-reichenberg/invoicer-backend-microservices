using System.Threading.Tasks;

namespace Invoicer.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}