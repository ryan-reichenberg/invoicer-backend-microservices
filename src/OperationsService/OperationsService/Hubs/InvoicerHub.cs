using System;
using System.Threading.Tasks;
using Convey.Auth;
using Microsoft.AspNetCore.SignalR;
using OperationsService.Extentions;

namespace OperationsService.Hubs
{
    public class InvoicerHub : Hub
    {
        private readonly IJwtHandler _jwtHandler;

        public InvoicerHub(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        public async Task InitializeAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisconnectAsync();
            }
            try
            {
                if (token.Equals("secret"))
                {
                    Console.WriteLine("Connecting");
                    Console.WriteLine(Context.ConnectionId);
                    await Groups.AddToGroupAsync(Context.ConnectionId, "test".ToUserGroup());
                    await ConnectAsync();
                    return;
                }
                Console.WriteLine("Connecting....");
                Console.WriteLine(Context.ConnectionId);
                var payload = _jwtHandler.GetTokenPayload(token);
                if (payload is null)
                {
                    await DisconnectAsync();
                    return;
                }

                var group = Guid.Parse(payload.Subject).ToUserGroup();
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
                await ConnectAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await DisconnectAsync();
            }
        }

        private async Task ConnectAsync()
        {
            Console.WriteLine("Sending message as connected");
            Console.WriteLine(Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("connected");
        }

        private async Task DisconnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
        }
    }
}