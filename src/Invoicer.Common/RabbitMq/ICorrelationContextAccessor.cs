namespace Invoicer.Common.RabbitMq
{
    public interface ICorrelationContextAccessor
    {
        object CorrelationContext { get; set; }
    }
}