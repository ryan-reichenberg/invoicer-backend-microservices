using System.Linq;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using Invoicer.Common.Messaging.MessageBroker;
using ProjectsService.Messages.Events.Todos;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Todos.Handlers
{
    public class ChangeTodoStatusCommandHandler : ICommandHandler<ChangeTodoStatusCommand>
    {
        private ITodoRepository _todoRepository;
        private readonly IMessageBroker _messageBroker;

        public ChangeTodoStatusCommandHandler(ITodoRepository todoRepository, IMessageBroker messageBroker)
        {
            _todoRepository = todoRepository;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(ChangeTodoStatusCommand command)
        {
            var todo = await _todoRepository.FindByIdAsync(command.Id);
            var previousStaus = todo.Status;
            todo.ChangeTodoStatus(command.TodoStatus);
            await _todoRepository.UpdateAsync(todo);
            await _messageBroker.PublishAsync(new TodoStatusTransitionEvent(todo.Id, previousStaus, todo.Status));

        }
    }
}