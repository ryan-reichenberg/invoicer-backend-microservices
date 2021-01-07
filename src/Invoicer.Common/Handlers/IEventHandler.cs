using System.Threading.Tasks;

namespace Invoicer.Common.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}