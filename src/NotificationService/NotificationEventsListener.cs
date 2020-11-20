using System;
using System.Threading;
using System.Threading.Tasks;
using Invoicer.Common.Messaging;
using Microsoft.Extensions.Hosting;

namespace NotificationService
{
    public class NotificationEventsListener : IHostedService, ISubscriberCallback
    {
        private ISubscriber _subscriber;
        public NotificationEventsListener(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber.Start(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.Stop();
            return Task.CompletedTask;
        }

        public Task<bool> HandleEventAsync(string message)
        {
            Console.WriteLine(message);
            return Task.FromResult(true);
        }
    }
}