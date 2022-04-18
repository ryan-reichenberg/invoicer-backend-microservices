using System;
using System.Collections.Generic;
using Convey.CQRS.Queries;
using InvoicingService.Domain;
using Newtonsoft.Json;

namespace InvoicingService.Messages.Queries
{
    public class GetInvoicesByQuery : IQuery<List<Invoice>>
    {
        public InvoiceStatus? Status { get; set; }
        public DateTime? InssuedAfter { get; set; }
        public DateTime? InssuedBefore { get; set; }
        public Guid? InvoicedTo { get; set; }
        public Guid? ProjectId { get; set; }
        
        [JsonConstructor]
        public GetInvoicesByQuery()
        {
        }
    }
}