using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.Messages.Commands;
using ProjectsService.Messages.Queries;

namespace ProjectsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<ProjectsController> _logger;
        public ProjectsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ILogger<ProjectsController> logger)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _logger = logger;

        }
        // GET
        [HttpGet("{userId}", Name = "GetAllProjectsForUser")]
        public async Task<IActionResult> GetAllProjectsForUser(Guid userId)
        {
            List<Project> projects = await _queryDispatcher.QueryAsync(new GetProjectsForUserQuery() {Id = userId});
            return Ok(projects);
        }
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            Project project = await _queryDispatcher.QueryAsync(new GetProjectByIdQuery() {Id = projectId});
            return Ok(project);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostNewProject(CreateNewProjectCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
        
        [HttpPut]
        public async Task<IActionResult> PutUpdateProject(UpdateProjectCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(DeleteProjectCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
        
       
    }
}