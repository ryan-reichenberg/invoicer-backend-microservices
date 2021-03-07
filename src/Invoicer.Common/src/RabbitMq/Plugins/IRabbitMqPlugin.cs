using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Invoicer.Common.RabbitMq.Plugins
{
    public interface IRabbitMqPlugin
    {
        Task HandleAsync(object message, object correlationContext, BasicDeliverEventArgs args);
    }
}