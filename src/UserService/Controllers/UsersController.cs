using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Invoicer.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Commands;
using UserService.DataAccess;
using UserService.Mappers;
using UserService.Models;
using UserService.Queries;
using UserService.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserRepository _repo;
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        public UsersController(IUserRepository repo, ICommandBus commandBus, IQueryBus queryBus) {
            _repo = repo;
            _commandBus = commandBus;
            _queryBus = queryBus;
        }
        // GET: api/users/
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            Console.WriteLine("Worked");
            User user = await _repo.DeleteAsync("1");
            return Ok(user);
        }

        // GET api/users/5
        [HttpGet("{Id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var users = await _queryBus.Query<GetUserByIDQuery, User>(new GetUserByIDQuery() { Id = id }); ;
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
                    //User user = command.MapToUser();
                    _commandBus.Send(command);
                    // insert user
                    //_dbContext.Users.Add(user);
                    //await _dbContext.SaveChangesAsync();

                    // send event

                    // return result
                    return Ok(command);
                }
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to persist changes. " +
                    "Try again, and if the problem persists " +
                    "please, see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
