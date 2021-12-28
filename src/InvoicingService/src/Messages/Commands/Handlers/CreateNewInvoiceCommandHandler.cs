using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace InvoicingService.Messages.Commands.Handlers
{
    public class CreateNewInvoiceCommandHandler : ICommandHandler<CreateNewInvoiceCommand>
    {
        public Task HandleAsync(CreateNewInvoiceCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}