using System.Collections.Generic;
using Invoicer.Common.RabbitMq.Conventions;

namespace Invoicer.Common.RabbitMq
{
    public interface IRabbitMqClient
    {
        void Send(object message, IConventions conventions, string messageId = null, string correlationId = null,
            string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null);
    }
}