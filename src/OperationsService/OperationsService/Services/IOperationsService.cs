using System;
using System.Threading.Tasks;
using OperationsService.DTO;
using OperationsService.Types;

namespace OperationsService.Services
{
    public interface IOperationsService
    {
        event EventHandler<OperationUpdatedEventArgs> OperationUpdated;
        Task<OperationDto> GetAsync(string id);

        Task<(bool updated, OperationDto operation)> TrySetAsync(string id, string userId, string name,
            OperationState state, string code = null, string reason = null);
    }
}