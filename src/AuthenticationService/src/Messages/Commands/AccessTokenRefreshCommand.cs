using Invoicer.Common;
using Newtonsoft.Json;

namespace AuthenticationService.Messages.Commands
{
    public class AccessTokenRefreshCommand : ICommand
    {
        public string RefreshToken { get; }

        [JsonConstructor]
        public AccessTokenRefreshCommand(string token, string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}