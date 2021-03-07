using System;
using System.Collections.Generic;
using Invoicer.Common;
using Newtonsoft.Json;

namespace AuthenticationService.Messages.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Password { get; }
        public string Role { get; }

        public string Name { get; }
        public string StreetAddress { get; }
        public string PostalCode { get; }
        public string City { get; }
        public string MobileNumber { get; }
        public IEnumerable<string> Permissions { get; }

        public RegisterUserCommand(Guid userId, string email, string password, string role, string name, 
            string streetAddress, string postalCode, string city, string mobileNumber, 
            IEnumerable<string> permissions)
        {
            UserId = userId == Guid.Empty ? Guid.NewGuid() : userId;
            Email = email;
            Password = password;
            Name = name;
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            City = city;
            MobileNumber = mobileNumber;
            Role = role;
            Permissions = permissions;
        }
    }
}