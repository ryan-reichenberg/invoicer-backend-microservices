using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService.Domain;
using AuthenticationService.DTO;
using AuthenticationService.Messages.Commands;

namespace AuthenticationService.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterUserCommand command);
        Task<AuthDto> LoginAsync(LoginCommand command);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}