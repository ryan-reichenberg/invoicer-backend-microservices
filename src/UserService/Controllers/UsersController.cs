using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Commands;
using UserService.Models;
using UserService.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        public UsersController(IMediator mediator) {
            _commandBus = new CommandBus(mediator);
            _queryBus = new QueryBus(mediator);
        }
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> user = await _queryBus.Query(new GetAllUsersQuery());
            return Ok(user);
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
                    // e.g. confirmation email
                    // 

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
        public async Task<IActionResult> Put(int id, [FromBody]UpdateUserDetailsCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // send command
                   await _commandBus.Send(command);

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
        public async Task<IActionResult> Delete([FromBody]DeleteUserCommand command)
        {
             var result = await _commandBus.Send(command);
             return Ok();
        }
    }
}
