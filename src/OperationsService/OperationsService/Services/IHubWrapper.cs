using System.Threading.Tasks;

namespace OperationsService.Services
{
    public interface IHubWrapper
    {
        Task PublishToUserAsync(string userId, string message, object data);
        Task PublishToAllAsync(string message, object data);
    }
}