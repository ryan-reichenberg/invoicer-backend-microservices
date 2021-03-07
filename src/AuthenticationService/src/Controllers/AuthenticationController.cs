using System.Threading.Tasks;
using AuthenticationService.Messages.Commands;
using AuthenticationService.Services;
using Invoicer.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
            => Ok(await _authenticationService.LoginAsync(command));


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            await _authenticationService.RegisterAsync(command);

            return NoContent();
        }
    }
}