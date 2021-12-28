using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class CreateNewTagForTodoCommandHandler : ICommandHandler<AddNewTagForTodoCommand>
    {

        private readonly IEventDispatcher _messageBroker;
        private readonly ILogger<CreateNewTagForTodoCommandHandler> _logger;
        private readonly IProjectRepository _projectRepository;

        public CreateNewTagForTodoCommandHandler(IEventDispatcher messageBroker, IProjectRepository projectRepository, ILogger<CreateNewTagForTodoCommandHandler> logger)
        {
            _messageBroker = messageBroker;
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public async Task HandleAsync(AddNewTagForTodoCommand command)
        {
            var todo = await _projectRepository.FindTodoByIdAsync(command.Id.ToString());
            todo.AddNewTag(new TodoTag(command.TodoId, command.Name, command.Color));
            await _projectRepository.SaveTodoAsync(todo);
        }
    }
}