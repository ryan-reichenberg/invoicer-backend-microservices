using System.Threading.Tasks;
using AuthenticationService.Messages.Commands;
using AuthenticationService.Services;
using Invoicer.Common.Extensions;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokensController : ControllerBase
    {
        private IAccessTokenService _accessTokenService;
        private IRefreshTokenService _refreshTokenService;

        public TokensController(IAccessTokenService accessTokenService,
            IRefreshTokenService refreshTokenService)
        {
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }
        // Refresh
        [HttpPost("access-tokens/refresh")]
        public async Task<IActionResult> AccessTokenRefresh(AccessTokenRefreshCommand command)
            => Ok(await _refreshTokenService.RefreshAccessTokenAsync(command.RefreshToken));

        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> AccessTokenRevoke(AccessTokenRevokeCommand command)
        {
            await _accessTokenService.DeactivateAsync(
                command.Bind(c => c.UserId, UserId).UserId.ToString("N"));

            return NoContent();
        }
        [HttpPost("refresh-tokens/revoke")]
        public async Task<IActionResult> RefreshTokenRevoke(RefreshTokenRevokeComamnd command)
        {
            await _refreshTokenService.RevokeTokenAsync(command.RefreshToken);

            return NoContent();
        }
        
        
    }
}