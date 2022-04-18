using System;
using Convey.CQRS.Commands;
using Convey.MessageBrokers;

namespace UserService.Messages.Commands
{
    [Message("users")]
    public class RegisterUserCommand : ICommand
    {
        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        
        public RegisterUserCommand(string name, string mobileNumber, string emailAddress, String streetAddress, string postalCode, string city)
        {
            Id = new Guid().ToString("N");
            Name = name;
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            City = city;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }
    }
}
