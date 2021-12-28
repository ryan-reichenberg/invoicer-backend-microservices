using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.Messages.Commands;
using ProjectsService.Messages.Commands.Todos;
using ProjectsService.Messages.Queries;

namespace ProjectsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<ProjectsController> _logger;
        // GET
        [HttpPut]
        public async Task<IActionResult> PutUpdateTodo(UpdateTodoCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
        
        [HttpGet("{projectId}/todos", Name = "GetTodosForProject")]
        public async Task<IActionResult> GetTodosForProject(Guid projectId)
        {
            List<Todo> todos = await _queryDispatcher.QueryAsync(new GetTodosForProjectQuery() {Id = projectId.ToString()});
            if (todos == null) return NotFound(projectId);
            return Ok(todos);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostNewTodo(CreateNewTodoCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
        
        [HttpPut]
        public async Task<IActionResult> PutChangeStatus(ChangeTodoStatusCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
        
        [HttpPost("tags")]
        public async Task<IActionResult> PostNewTagForTodo(AddNewTagForTodoCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }

        [HttpDelete("tags")]
        public async Task<IActionResult> DeleteTodoTag(DeleteTodoTagCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteTodo(DeleteTodoCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
    }
}