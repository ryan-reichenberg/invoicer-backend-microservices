using Invoicer.Common;
using Newtonsoft.Json;

namespace AuthenticationService.Messages.Commands
{
    public class LoginCommand : ICommand
    {
        public string Email { get; }
        public string Password { get; }

        [JsonConstructor]
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}