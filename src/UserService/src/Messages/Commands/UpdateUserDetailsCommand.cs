using System;
using Invoicer.Common;
using Newtonsoft.Json;

namespace UserService.Messages.Commands
{
    public class UpdateUserDetailsCommand : ICommand
    {
        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string Name { get; set; }

        public Guid Id { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        [JsonConstructor]
        public UpdateUserDetailsCommand(Guid id, string name, string mobileNumber, string emailAddress, String streetAddress, string postalCode, string city)
        {
            Id = id;
            Name = name;
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            City = city;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }
    }
}
