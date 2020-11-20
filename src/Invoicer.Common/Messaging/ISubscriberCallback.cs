using System.Threading.Tasks;

namespace Invoicer.Common.Messaging
{
    public interface ISubscriberCallback
    {
        Task<bool> HandleEventAsync(string message);
    }
}