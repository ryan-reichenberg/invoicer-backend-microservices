namespace Invoicer.Common.Messaging
{
    public interface ISubscriber
    {
        void Start();
        void Stop();
    }
}