using Convey.CQRS.Queries;
using OperationsService.DTO;

namespace OperationsService.Messages.Queries
{
    public class GetOperationQuery : IQuery<OperationDto>
    {
        public string OperationId { get; set; }
    }
}