using System.Linq;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using Invoicer.Common.Messaging.MessageBroker;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand>
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<UpdateProjectCommandHandler> _logger;
        

        public UpdateProjectCommandHandler(IMessageBroker messageBroker, IProjectRepository projectRepository, ILogger<UpdateProjectCommandHandler> logger)
        {
            _messageBroker = messageBroker;
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public async Task HandleAsync(UpdateProjectCommand command)
        {
            _logger.LogDebug($"Updating project with id: {command.Id}. Payload: {command}");
            await _projectRepository.UpdateProjectAsync(new Project(command.Id, command.Name,  command.Description,  
                command.Todos.Select(x => x.AsEntity()).ToList(), command.ClientId, command.UserId));
        }
    }
}