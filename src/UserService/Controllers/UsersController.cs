using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common;
using Invoicer.Common.Busses;
using Invoicer.Common.Messaging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Commands;
using UserService.Models;
using UserService.Queries;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IPublisher _publisher;
        public UsersController(IMediator mediator, IPublisher publisher) {
            _publisher = publisher;
        }
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> user = await _queryBus.Query(new GetAllUsersQuery());
            return Ok();
        }

        // GET api/users/{guid}
        [HttpGet("{Id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user =  await _queryBus.Query(new GetUserByIdQuery() { Id = id }); ;
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterUserCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // send command
                    CommandResult result = await _commandBus.Send(command);
                    if (!result.Ok)
                    {
                        Failed failure = (Failed) result;
                        return StatusCode((int)failure.ResponseCode, failure.Reasons);
                    }

                    // send events
                    await _publisher.PublishMessageAsync("Hello World", "");
                    // return result
                    return Ok();

                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to persist changes. " +
                    "Try again, and if the problem persists " +
                    "Please see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        // PUT api/users/{guid}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]UpdateUserDetailsCommand command)
        {
            try
            {
                command.Id = id;
                if (ModelState.IsValid)
                {
                    // send command
                   var result = await _commandBus.Send(command);
                   if (!result.Ok)
                   {
                       Failed failure = (Failed) result;
                       return StatusCode((int)failure.ResponseCode, failure.Reasons);
                   }

                    // send event(s)

                    // return result
                    return Ok();
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to persist changes. " +
                                             "Try again, and if the problem persists " +
                                             "please, see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        // DELETE api/users/{guid}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromBody]DeleteUserCommand command)
        {
            if (ModelState.IsValid)
            {
                // Gross
                command.Id = id;
                var result = await _commandBus.Send(command);
                // send events -- Important
                return Ok();
            }

            return BadRequest();
        }
    }
}
