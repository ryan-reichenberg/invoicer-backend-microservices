using System;
using Invoicer.Common;
using UserService.Models;

namespace UserService.Commands
{
    public class RegisterUserCommand : UserCommand
    {
        public RegisterUserCommand(string id, string name, string mobileNumber, string emailAddress, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }
    }
}
