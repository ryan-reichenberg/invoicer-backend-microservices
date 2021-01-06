using System;
using System.Threading.Tasks;

namespace Invoicer.Common.RabbitMq.Subscribers
{
    public interface IBusSubscriber
    {
        IBusSubscriber Subscribe<T>(Func<IServiceProvider, T, object, Task> handle) where T : class;
    }
}