using System.Threading.Tasks;
using Invoicer.Common.Dispatchers;
using Invoicer.Common.Handlers;
using Invoicer.Common.Messaging.MessageBroker;
using Microsoft.Extensions.Logging;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class DeleteTodoTagCommandHandler : ICommandHandler<DeleteTodoTagCommand>
    {
        private readonly IEventDispatcher _messageBroker;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<DeleteTodoTagCommandHandler> _logger;
        

        public DeleteTodoTagCommandHandler(IEventDispatcher messageBroker, IProjectRepository projectRepository, ILogger<DeleteTodoTagCommandHandler> logger)
        {
            _messageBroker = messageBroker;
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public async Task HandleAsync(DeleteTodoTagCommand command)
        {
            _logger.LogDebug($"Deleting Todo Tag with id: {command.Id}");
            await _projectRepository.DeleteTodoTag(command.Id.ToString());
        }
    }
}