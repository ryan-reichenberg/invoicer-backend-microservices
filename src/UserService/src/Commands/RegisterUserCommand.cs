using System;
using Invoicer.Common;
using Invoicer.Common.RabbitMq.Attributes;
using Newtonsoft.Json;
using UserService.Models;

namespace UserService.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }
        public Address Address{ get; set; }
        
        [JsonConstructor]
        public RegisterUserCommand(string name, string mobileNumber, string emailAddress, Address address)
        {
            Id = new Guid().ToString("N");
            Name = name;
            Address = address;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }
    }
}
