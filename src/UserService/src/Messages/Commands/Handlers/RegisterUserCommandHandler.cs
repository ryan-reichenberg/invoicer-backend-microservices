using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Microsoft.Extensions.Logging;
using UserService.Events;
using UserService.Repositories;

namespace UserService.Messages.Commands.Handlers
{
    // [Message("users")]
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private IUserRepository _repository;
        private IBusPublisher _busPublisher;
        private ILogger<RegisterUserCommandHandler> _logger;
        public RegisterUserCommandHandler(IUserRepository repository, ILogger<RegisterUserCommandHandler> logger, IBusPublisher busPublisher)
        {
            _repository = repository;
            _logger = logger;
            _busPublisher = busPublisher;
        }


        public Task HandleAsync(RegisterUserCommand command)
        {
            _logger.LogInformation("Received command");
            // _busPublisher.PublishAsync(new UserRegisteredEvent(command));
            return Task.CompletedTask;
        }
    }
}
