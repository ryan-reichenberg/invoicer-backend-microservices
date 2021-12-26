namespace Invoicer.Common.CQRS.Logging
{
    public interface IMessageToLogTemplateMapper
    {
        HandlerLogTemplate Map<TMessage>(TMessage message) where TMessage : class;
    }
}