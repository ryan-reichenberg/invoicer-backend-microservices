using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using Invoicer.Common.RabbitMq.Attributes;
using Microsoft.Extensions.Logging;
using UserService.Repositories;

namespace UserService.Messages.Commands.Handlers
{
    [Message("users")]
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private IUserRepository _repository;
        private ILogger<RegisterUserCommand> _logger;
        public RegisterUserCommandHandler(IUserRepository repository, ILogger<RegisterUserCommand> logger)
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
