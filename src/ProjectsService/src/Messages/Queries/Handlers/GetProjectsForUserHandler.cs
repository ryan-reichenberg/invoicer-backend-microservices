using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Queries.Handlers
{
    public class GetProjectsForUserHandler : IQueryHandler<GetProjectsForUserQuery, List<Project>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<GetTodosForProjectHandler> _logger;

        public GetProjectsForUserHandler(IProjectRepository projectRepository, ILogger<GetTodosForProjectHandler> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public async Task<List<Project>> HandleAsync(GetProjectsForUserQuery query)
        {
            List<Project> projects = await _projectRepository.FindAllProjectsByUserIdAsync(query.Id.ToString());
            return projects;
        }
    }
}