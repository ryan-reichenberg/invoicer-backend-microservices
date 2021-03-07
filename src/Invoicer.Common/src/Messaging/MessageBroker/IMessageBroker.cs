using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoicer.Common.Messaging.MessageBroker
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IEvent[] events);
        Task PublishAsync(IEnumerable<IEvent> events);
    }
}