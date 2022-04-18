using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using InvoicingService.Domain;
using InvoicingService.DTO;
using InvoicingService.Repositories;

namespace InvoicingService.Messages.Commands.Handlers
{
    public class CreateNewInvoiceCommandHandler : ICommandHandler<CreateNewInvoiceCommand>
    {
        private IInvoiceRepository _invoiceRepository;

        public CreateNewInvoiceCommandHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task HandleAsync(CreateNewInvoiceCommand command)
        {
            await _invoiceRepository.SaveAsync(new(command.Id, command.Items?.Select(item => new LineItemDto(item.Description, item.Price, item.Quantity)).ToList(), command.BillFrom, command.BillTo, command.IssuedAt, command.DueDate, command.Recurring, command.TaxRate, command.Tax, command.SubTotal, command.AdditionalNotes, command.ProjectId, command.CreatedAt, command.ModifiedAt, command.Status, command.InvoicedTo, command.InvoicedFrom));
            
        }
    }
}