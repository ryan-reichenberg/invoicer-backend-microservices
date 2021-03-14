using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using Microsoft.Extensions.Logging;

namespace UserService.Events.Handlers
{
    public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
    {
        private ILogger<UserRegisteredEventHandler> _logger;
        public UserRegisteredEventHandler(ILogger<UserRegisteredEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(UserRegisteredEvent @event)
        {
            _logger.LogInformation(@event.ToString());
            return Task.CompletedTask;
        }
    }
}