using RabbitMQ.Client;

namespace Invoicer.Common.Messaging
{
    public interface ISubscriber
    {
        void Start(ISubscriberCallback callback);
        void Stop();
    }
}