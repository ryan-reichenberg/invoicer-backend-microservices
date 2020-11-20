using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Invoicer.Common.Messaging
{
    public interface IPublisher
    {
        Task PublishMessageAsync(string message, string routingKey);
    }
}