using System.Threading.Tasks;
using AuthenticationService.Messages.Commands;
using Invoicer.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private Services.AuthenticationService _authenticationService;

        // GET
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
            => Ok(await _authenticationService.LoginAsync(command.Email, command.Password));


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            command.BindId(c => c.Id);
            await _authenticationService.RegisterAsync(command.Id, 
                command.Email, command.Password, command.Role);

            return NoContent();
        }
    }
}