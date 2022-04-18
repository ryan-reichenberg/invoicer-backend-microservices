using System;
using System.Text.Json.Serialization;
using Convey.CQRS.Queries;
using InvoicingService.Domain;
using InvoicingService.DTO;

namespace InvoicingService.Messages.Queries
{
    public class GetInvoiceByIdQuery : IQuery<Invoice>
    {
        public Guid Id { get; set; }

        [JsonConstructor]
        public GetInvoiceByIdQuery()
        {
        }
    }
}