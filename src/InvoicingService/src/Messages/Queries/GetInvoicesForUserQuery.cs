using System.Collections.Generic;
using Convey.CQRS.Queries;
using InvoicingService.Domain;

namespace InvoicingService.Messages.Queries
{
    public class GetInvoicesForUserQuery : IQuery<List<Invoice>>
    {
    }
}