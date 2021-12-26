using UserService.Domain;
using UserService.DTO;

namespace UserService.Repositories
{
     public static class Extensions
    {
        public static User AsEntity(this UserDto userDto)
            => new User(userDto.Id, userDto.Name, userDto.StreetAddress, userDto.PostalCode, userDto.City,
                userDto.MobileNumber, userDto.EmailAddress);
        
        public static UserDto AsDto(this User user)
            => new UserDto(user.Id, user.Name, user.StreetAddress, user.PostalCode, user.City,
                user.MobileNumber, user.EmailAddress);

    }
}