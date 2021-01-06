using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Invoicer.Common.RabbitMq.Plugins
{
    public interface IRabbitMqPluginAccessor
    {
        void SetSuccessor(Func<object, object, BasicDeliverEventArgs, Task> successor);
    }
}