using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.Repositories;

namespace ProjectsService.Messages.Queries.Handlers
{
    public class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQuery, Project>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<GetProjectByIdQueryHandler> _logger;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository, ILogger<GetProjectByIdQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public Task<Project> HandleAsync(GetProjectByIdQuery query)
        {
            var project = _projectRepository.FindByIdAsync(query.Id.ToString());
            if (project == null) return null;
            return project;
        }
    }
}