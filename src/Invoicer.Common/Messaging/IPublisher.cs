using System.Threading.Tasks;

namespace Invoicer.Common.Messaging
{
    public interface IPublisher
    {
        Task PublishMessage();
    }
}