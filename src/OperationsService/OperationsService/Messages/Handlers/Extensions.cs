using System.Text;
using Convey.MessageBrokers;
using Newtonsoft.Json;
using OperationsService.Types;

namespace OperationsService.Messages.Handlers
{
    public static class Extensions
    {
        
        public static CorrelationContext GetCorrelationContext(this ICorrelationContextAccessor accessor)
        {
            if (accessor.CorrelationContext is null)
            {
                return null;
            }

            var payload = JsonConvert.SerializeObject(accessor.CorrelationContext);

            return string.IsNullOrWhiteSpace(payload)
                ? null
                : JsonConvert.DeserializeObject<CorrelationContext>(payload);
        }

        public static OperationState? GetSagaState(this IMessageProperties messageProperties)
            {
                const string sagaHeader = "Saga";
                if (messageProperties?.Headers is null || !messageProperties.Headers.TryGetValue(sagaHeader, out var saga))
                {
                    return null;
                }

                return saga is byte[] sagaBytes
                    ? Encoding.UTF8.GetString(sagaBytes).ToLowerInvariant() switch
                    {
                        "pending" => OperationState.Pending,
                        "completed" => OperationState.Completed,
                        "rejected" => OperationState.Rejected,
                        _ => (OperationState?) null
                    }
                    : null;
            }
    }
}