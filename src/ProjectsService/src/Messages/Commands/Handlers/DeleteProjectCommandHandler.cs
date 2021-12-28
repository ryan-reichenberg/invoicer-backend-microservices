using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
    {
        private readonly IEventDispatcher _messageBroker;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<DeleteProjectCommandHandler> _logger;

        public DeleteProjectCommandHandler(IEventDispatcher messageBroker, IProjectRepository projectRepository, ILogger<DeleteProjectCommandHandler> logger)
        {
            _messageBroker = messageBroker;
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public async Task HandleAsync(DeleteProjectCommand command)
        {
            _logger.LogDebug($"Deleting Todo Tag with id: {command.Id}");
            await _projectRepository.DeleteProjectAsync(command.Id.ToString());
        }
    }
}