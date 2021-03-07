using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common.Dispatchers;
using Invoicer.Common.Extensions;
using Invoicer.Common.RabbitMq.Publishers;
using Microsoft.AspNetCore.Mvc;
using UserService.Commands;
using UserService.Models;
using UserService.Queries;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IBusPublisher _publisher;
        private readonly IQueryDispatcher _dispatcher;

        public UsersController(IBusPublisher publisher, IQueryDispatcher dispatcher) {
            _publisher = publisher;
            _dispatcher = dispatcher;
        }
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> user = await _dispatcher.QueryAsync(new GetAllUsersQuery());
            return Ok();
        }

        // GET api/users/{guid}
        [HttpGet("{Id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user =  await _dispatcher.QueryAsync(new GetUserByIdQuery() { Id = id }); ;
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _publisher.SendAsync(new RegisterUserCommand("Ryan", "040304352", "fhgdsjkgfdg", new Address("fdhjsf", "fdsdsa", "dsbgfhjdsa")), null);
            return Accepted();
        }

        // PUT api/users/{guid}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UpdateUserDetailsCommand command)
        {
            return Ok();
        }

        // DELETE api/users/{guid}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromBody]DeleteUserCommand command)
        {
          return Ok();
        }
    }
}
