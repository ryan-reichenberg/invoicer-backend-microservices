using System.Threading.Tasks;
using OperationsService.DTO;

namespace OperationsService.Services
{
    public interface IHubService
    {
        Task PublishOperationPendingAsync(OperationDto operation);
        Task PublishOperationCompletedAsync(OperationDto operation);
        Task PublishOperationRejectedAsync(OperationDto operation);
    }
}