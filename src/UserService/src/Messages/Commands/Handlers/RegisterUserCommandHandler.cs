using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Microsoft.Extensions.Logging;
using UserService.Repositories;

namespace UserService.Messages.Commands.Handlers
{
    // [Message("users")]
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private IUserRepository _repository;
        private ILogger<RegisterUserCommandHandler> _logger;
        public RegisterUserCommandHandler(IUserRepository repository, ILogger<RegisterUserCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public Task HandleAsync(RegisterUserCommand command)
        {
            _logger.LogInformation("Received command");
            return Task.CompletedTask;
        }
    }
}
