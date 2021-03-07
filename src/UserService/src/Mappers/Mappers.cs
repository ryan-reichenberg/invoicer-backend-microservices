using System;
using UserService.Commands;
using UserService.Models;

namespace UserService.Mappers
{
    public static class Mappers
    {

        public static User MapToUser(this UserCommand command) => new User
        {
            Id = command.Id ?? Guid.NewGuid().ToString(),
            Name = command.Name ?? "",
            StreetAddress =  command.Address?.StreetAddress ?? "",
            City = command.Address?.City ?? "",
            PostalCode = command.Address?.PostalCode ?? "",
            MobileNumber = command.MobileNumber ?? "",
            EmailAddress = command.EmailAddress ?? ""
        };
    }
}
