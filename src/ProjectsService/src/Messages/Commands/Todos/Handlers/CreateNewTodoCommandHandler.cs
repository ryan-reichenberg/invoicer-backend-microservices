using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using Invoicer.Common.Messaging.MessageBroker;
using ProjectsService.Domain;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Todos.Handlers
{
    public class CreateNewTodoCommandHandler : ICommandHandler<CreateNewTodoCommand>
    {
        private ITodoRepository _todoRepository;
        private readonly IMessageBroker _messageBroker;

        public CreateNewTodoCommandHandler(ITodoRepository todoRepository, IMessageBroker messageBroker)
        {
            _todoRepository = todoRepository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(CreateNewTodoCommand command)
        {
            await _todoRepository.SaveAsync(new Todo(command.TodoId, command.Name, command.Description,
                command.ProjectId, command.Status, command.Tags));
        }
    }
}