using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using InvoicingService.Domain;
using InvoicingService.Repositories;

namespace InvoicingService.Messages.Queries.Handlers
{
    public class GetInvoicesByQueryHandler : IQueryHandler<GetInvoicesByQuery, List<Invoice>>
    {
        private IInvoiceRepository _invoiceRepository;

        public GetInvoicesByQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<List<Invoice>> HandleAsync(GetInvoicesByQuery query)
        {
            var invoices = await _invoiceRepository.GetInvoicesBy(query.Status, query.InvoicedTo, query.InssuedAfter,
                query.InssuedBefore, query.ProjectId);
            return invoices;
        }
    }
}