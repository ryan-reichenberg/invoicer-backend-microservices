using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Messages.Commands;
using UserService.Queries;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public UsersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserDto> users = await _queryDispatcher.QueryAsync(new GetAllUsersQuery());
            return Ok(users);
        }

        // GET api/users/{guid}
        [HttpGet("{Id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user =  await _queryDispatcher.QueryAsync(new GetUserByIdQuery() { Id = id }); ;
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post(RegisterUserCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted($"api/users/{command.Id}", null);
        }

        // PUT api/users/{guid}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateUserDetailsCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted($"api/users/{command.Id}", null);
        }

        // DELETE api/users/{guid}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteUserCommand command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
    }
}
