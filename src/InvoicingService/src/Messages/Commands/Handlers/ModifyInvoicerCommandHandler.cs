using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace InvoicingService.Messages.Commands.Handlers
{
    public class ModifyInvoicerCommandHandler : ICommandHandler<ModifyInvoiceCommand>
    {
        public Task HandleAsync(ModifyInvoiceCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}