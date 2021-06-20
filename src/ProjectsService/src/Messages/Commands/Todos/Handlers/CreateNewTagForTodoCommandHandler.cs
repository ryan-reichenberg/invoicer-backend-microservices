using System.Threading.Tasks;
using Invoicer.Common.Handlers;

namespace ProjectsService.Messages.Commands.Todos.Handlers
{
    public class CreateNewTagForTodoCommandHandler : ICommandHandler<CreateNewTodoCommand>
    {
        public Task HandleAsync(CreateNewTodoCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}