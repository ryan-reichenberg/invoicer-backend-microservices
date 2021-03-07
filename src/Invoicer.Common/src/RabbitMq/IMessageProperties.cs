using System.Collections.Generic;

namespace Invoicer.Common.RabbitMq
{
     public interface IMessageProperties
        {
            string MessageId { get; }
            string CorrelationId { get; }
            long Timestamp { get; }
            IDictionary<string, object> Headers { get; }
        }
}