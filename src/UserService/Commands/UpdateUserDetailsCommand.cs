using System;
using Invoicer.Common;
using UserService.Models;

namespace UserService.Commands
{
    public class UpdateUserDetailsCommand : UserCommand
    {
        public readonly string Id;
        public readonly UserUpdateDetails UpdateDetais;
        public UpdateUserDetailsCommand(string id, UserUpdateDetails updateDetails)
        {
            Id = id;
            UpdateDetais = updateDetails;
        }
    }
}
