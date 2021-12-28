using System.Threading.Tasks;
using Convey.CQRS.Queries;
using OperationsService.DTO;
using OperationsService.Services;

namespace OperationsService.Messages.Queries.Handlers
{
    public class GetOpertionQueryHandler : IQueryHandler<GetOperationQuery, OperationDto>
    {
        private IOperationsService _operationsService;

        public GetOpertionQueryHandler(IOperationsService operationsService)
        {
            _operationsService = operationsService;
        }
        
        public async Task<OperationDto> HandleAsync(GetOperationQuery query)
        {
            var operation = await _operationsService
                .GetAsync(query.OperationId);
            // Nullable
            return operation;

        }
    }
}