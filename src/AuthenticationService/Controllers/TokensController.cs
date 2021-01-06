using System.Threading.Tasks;
using AuthenticationService.Messages.Commands;
using AuthenticationService.Services;
using Invoicer.Common.Extensions;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationService.Controllers
{
    [ApiController]
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
            => Ok(await _refreshTokenService.CreateAccessTokenAsync(command.RefreshToken));

        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> AccessTokenRevoke(AccessTokenRevokeCommand command)
        {
            await _accessTokenService.DeactivateCurrentAsync(
                command.Bind(c => c.UserId, UserId).UserId.ToString("N"));

            return NoContent();
        }
        [HttpPost("refresh-tokens/revoke")]
        public async Task<IActionResult> RefreshTokenRevoke(RefreshTokenRevokeComamnd command)
        {
            await _refreshTokenService.RevokeTokenAsync(command.RefreshToken, 
                command.Bind(c => c.UserId, UserId).UserId);

            return NoContent();
        }
        
        
    }
}