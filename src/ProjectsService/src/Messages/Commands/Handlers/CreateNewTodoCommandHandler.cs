using System.Threading.Tasks;
using Invoicer.Common.Dispatchers;
using Invoicer.Common.Handlers;
using Invoicer.Common.Messaging.MessageBroker;
using ProjectsService.Domain;
using ProjectsService.Messages.Commands.Todos;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Commands.Handlers
{
    public class CreateNewTodoCommandHandler : ICommandHandler<CreateNewTodoCommand>
    {
        private readonly IEventDispatcher _messageBroker;
        private readonly IProjectRepository _projectRepository;

        public CreateNewTodoCommandHandler(IEventDispatcher messageBroker, IProjectRepository projectRepository)
        {
            _messageBroker = messageBroker;
            _projectRepository = projectRepository;
        }
        public async Task HandleAsync(CreateNewTodoCommand command)
        {
            var project = await _projectRepository.FindByIdAsync(command.ProjectId);
            project.AddNewTodo(new Todo(command.TodoId, command.Name, command.Description, command.Status, command.Tags));
            await _projectRepository.SaveProjectAsync(project);
        }
    }
}