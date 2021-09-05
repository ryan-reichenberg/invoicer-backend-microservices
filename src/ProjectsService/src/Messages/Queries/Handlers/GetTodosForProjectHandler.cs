using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Queries.Handlers
{
    public class GetTodosForProjectHandler : IQueryHandler<GetTodosForProjectQuery, List<Todo>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<GetTodosForProjectHandler> _logger;

        public GetTodosForProjectHandler(IProjectRepository projectRepository, ILogger<GetTodosForProjectHandler> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        public async Task<List<Todo>> HandleAsync(GetTodosForProjectQuery query)
        {
            Project project = await _projectRepository.FindByIdAsync(query.Id);
            _logger.LogDebug("Found todos: '{Todos}' for project {Id}", project?.Todos, project?.Id);
            return project?.Todos;
        }
    }
}