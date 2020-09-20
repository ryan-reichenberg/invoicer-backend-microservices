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
        // GET: api/users/
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> user = await _queryBus.Query(new GetAllUsersQuery());
            return Ok(user);
        }

        // GET api/users/5
        [HttpGet("{Id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var users =  await _queryBus.Query(new GetUserByIDQuery() { Id = id }); ;
            //string id = new Guid().ToString();
            //var users = await _dbContext.DataSet.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(users);
        }

        // POST api/values
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
                    // send confirmation email
                    // 

                    // return result
                   
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateUserDetailsCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // send command
                   await _commandBus.Send(command);

                    // send event
                    //RabbitMQ goes here

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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody]DeleteUserCommand command)
        {
             var result = await _commandBus.Send(command);
             return Ok();
        }
    }
}
