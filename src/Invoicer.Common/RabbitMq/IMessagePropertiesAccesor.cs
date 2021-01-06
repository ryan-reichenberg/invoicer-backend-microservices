namespace Invoicer.Common.RabbitMq
{
    public interface IMessagePropertiesAccessor
    {
        IMessageProperties MessageProperties { get; set; }
    }
}