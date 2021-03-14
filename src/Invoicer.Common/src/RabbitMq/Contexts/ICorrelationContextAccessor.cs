namespace Invoicer.Common.RabbitMq.Contexts
{
    public interface ICorrelationContextAccessor
    {
        object CorrelationContext { get; set; }
    }
}