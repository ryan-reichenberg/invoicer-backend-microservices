using System.Threading.Tasks;
using MediatR;

namespace Invoicer.Common.Busses
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator mediator;
        public CommandBus(IMediator mediator)
        {
            this.mediator = mediator;
        }
        

        public async Task<CommandResult> Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            return await mediator.Send(command);
        }
    }
}
