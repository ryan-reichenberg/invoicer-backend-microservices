using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Invoicer.Common.RabbitMq.Plugins
{
    internal interface IRabbitMqPluginAccessor
    {
        void SetSuccessor(Func<object, object, BasicDeliverEventArgs, Task> successor);
    }
}