using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class UpdateTodoCommandHandler : ICommandHandler<UpdateTodoCommand>
    {
        private readonly IEventDispatcher _messageBroker;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<UpdateTodoCommandHandler> _logger;
        

        public UpdateTodoCommandHandler(IEventDispatcher messageBroker, IProjectRepository projectRepository, ILogger<UpdateTodoCommandHandler> logger)
        {
            _messageBroker = messageBroker;
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public async Task HandleAsync(UpdateTodoCommand command)
        {
            _logger.LogDebug($"Updating todo with id: {command.Id}. Payload: {command}");
            await _projectRepository.UpdateTodoAsync(new Todo(command.Id, command.Name, command.Description,
                command.Status, command.Tags.Select(x => x.AsEntity()).ToList()));
        }
    }
}