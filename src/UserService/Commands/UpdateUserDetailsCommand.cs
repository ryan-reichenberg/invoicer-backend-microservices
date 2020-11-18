using System;
using Invoicer.Common;
using UserService.Models;

namespace UserService.Commands
{
    public class UpdateUserDetailsCommand : UserCommand
    {
        private readonly string Id;
        public UpdateUserDetailsCommand(string id, string name, string mobileNumber, string emailAddress, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }
    }
}
