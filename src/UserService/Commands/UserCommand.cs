using Invoicer.Common;
using UserService.Models;

namespace UserService.Commands
{
    public abstract class UserCommand : ICommand
    {
        public string Id;
        public string Name;
        public Address Address;
        public string MobileNumber;
        public string EmailAddress;
    }
}