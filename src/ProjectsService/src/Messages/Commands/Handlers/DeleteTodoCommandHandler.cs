using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class DeleteTodoCommandHandler : ICommandHandler<DeleteTodoCommand>
    {
        private readonly IEventDispatcher _messageBroker;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<DeleteTodoCommandHandler> _logger;

        public DeleteTodoCommandHandler(IEventDispatcher messageBroker, IProjectRepository projectRepository, ILogger<DeleteTodoCommandHandler> logger)
        {
            _messageBroker = messageBroker;
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public async Task HandleAsync(DeleteTodoCommand command)
        {
            _logger.LogDebug($"Deleting Todo Tag with id: {command.Id}");
            await _projectRepository.DeleteTodoAsync(command.Id.ToString());
        }
    }
}