using System;
using Invoicer.Common;
using Newtonsoft.Json;

namespace AuthenticationService.Messages.Commands
{
    public class RegisterCommand : ICommand
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }
        public string Role { get; }

        [JsonConstructor]
        public RegisterCommand(Guid id, string email, string password, string role)
        {
            Id = id;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}