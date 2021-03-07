using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using AuthenticationService.Domain;
using AuthenticationService.DTO;
using AuthenticationService.Exceptions;
using AuthenticationService.Messages.Commands;
using AuthenticationService.Messages.Events;
using AuthenticationService.Providers;
using AuthenticationService.Repositories;
using Invoicer.Common.Messaging.MessageBroker;
using Invoicer.Common.RabbitMq.Publishers;
using Microsoft.Extensions.Logging;

namespace AuthenticationService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenService _tokenService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMessageBroker _messageBroker;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordService _passwordService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly EmailAddressAttribute _emailAddressAttribute = new EmailAddressAttribute();
        public AuthenticationService(IUserRepository userRepository, IAccessTokenService tokenService, 
            ILogger<AuthenticationService> logger, IMessageBroker messageBroker,  IJwtProvider jwtProvider, 
            IPasswordService passwordService, IRefreshTokenService refreshTokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;
            _messageBroker = messageBroker;
            _jwtProvider = jwtProvider;
            _passwordService = passwordService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetUserAsync(id);

            return user is null ? null : new UserDto(user);
        }

        public async Task RegisterAsync(RegisterUserCommand command)
        {
            if (!_emailAddressAttribute.IsValid(command.Email))
            {
                _logger.LogError($"Invalid email: {command.Email}");
                throw new InvalidEmailException(command.Email);
            }

            var user = await _userRepository.GetUserAsync(command.Email);
            if (user is {})
            {
                _logger.LogError($"Email already in use: {command.Email}");
                throw new EmailInUseException(command.Email);
            }

            var role = string.IsNullOrWhiteSpace(command.Role) ? "user" : command.Role.ToLowerInvariant();
            var password = _passwordService.Hash(command.Password);
            user = new User(command.UserId, command.Email, password, role, DateTime.UtcNow, command.Permissions);
            await _userRepository.AddUserAsync(user);

            _logger.LogInformation($"Created an account for the user with id: {user.Id}.");
            await _messageBroker.PublishAsync(new UserRegisteredEvent(command.UserId, command.Name, 
                command.StreetAddress, command.PostalCode, command.City, command.MobileNumber, command.Email));
        }

        public async Task<AuthDto> LoginAsync(LoginCommand command)
        {
            if (!_emailAddressAttribute.IsValid(command.Email))
            {
                _logger.LogError($"Invalid email: {command.Email}");
                throw new InvalidCredentialException(command.Email);
            }

            var user = await _userRepository.GetUserAsync(command.Email);
            if (user is null)
            {
                _logger.LogError($"User with email: {command.Email} was not found.");
                throw new InvalidCredentialsException(command.Email);
            }

            if (!_passwordService.IsValid(user.Password, command.Password))
            {
                _logger.LogError($"Invalid password for user with id: {user.Id.Value}");
                throw new InvalidCredentialsException(command.Email);
            }

            var claims = user.Permissions.Any()
                ? new Dictionary<string, IEnumerable<string>>
                {
                    ["permissions"] = user.Permissions
                }
                : null;
            var auth = _jwtProvider.Create(user.Id, user.Role, claims: claims);
            auth.RefreshToken = await _refreshTokenService.CreateTokenAsync(user.Id);

            _logger.LogInformation($"User with id: {user.Id} has been authenticated.");
            // await _publisher.PublishAsync(new SignedIn(user.Id, user.Role));

            return auth;
        }

        public Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}