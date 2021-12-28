using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OperationsService.Extentions;
using OperationsService.Hubs;

namespace OperationsService.Services
{
    public class HubWrapper : IHubWrapper
    {
        private readonly IHubContext<InvoicerHub> _hubContext;

        public HubWrapper(IHubContext<InvoicerHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task PublishToUserAsync(string userId, string message, object data)
            => await _hubContext.Clients.Group(userId.ToUserGroup()).SendAsync(message, data);

        public async Task PublishToAllAsync(string message, object data)
            => await _hubContext.Clients.All.SendAsync(message, data);
    }
}