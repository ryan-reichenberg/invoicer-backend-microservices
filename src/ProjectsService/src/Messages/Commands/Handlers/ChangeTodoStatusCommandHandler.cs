using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using ProjectsService.Messages.Commands.Todos;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class ChangeTodoStatusCommandHandler : ICommandHandler<ChangeTodoStatusCommand>
    {

        private readonly IEventDispatcher _messageBroker;
        private readonly IProjectRepository _projectRepository;

        public ChangeTodoStatusCommandHandler(IEventDispatcher messageBroker, IProjectRepository projectRepository)
        {
            _messageBroker = messageBroker;
            _projectRepository = projectRepository;
        }

        public async Task HandleAsync(ChangeTodoStatusCommand command)
        {
            var todo = await _projectRepository.FindTodoByIdAsync(command.Id.ToString());
            todo.ChangeTodoStatus(command.TodoStatus);
            await _projectRepository.SaveTodoAsync(todo);

        }
    }
}