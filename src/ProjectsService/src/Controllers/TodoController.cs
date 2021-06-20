using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using ProjectsService.Domain;
using ProjectsService.DTO;
using ProjectsService.Messages.Commands.Todos;
using ProjectsService.Messages.Queries.Todos;

namespace ProjectsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private ICommandDispatcher _commandDispatcher;
        private IQueryDispatcher _queryDispatcher;
        public TodoController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        // GET
        [HttpGet("{Id}", Name = "GetTodosForProject")]
        public async Task<IActionResult> GetTodosForProject(Guid projectId)
        {
            List<TodoDto> todos = await _queryDispatcher.QueryAsync(new GetTodosForProjectQuery() {Id = projectId});
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
        
        [HttpPost]
        public async Task<IActionResult> PostNewTagForTodo(CreateNewTagForTodoCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
        
    }
}