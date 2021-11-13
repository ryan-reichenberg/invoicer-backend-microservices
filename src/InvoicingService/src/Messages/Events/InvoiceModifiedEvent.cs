using System;
using InvoicingService.Domain;

namespace InvoicingService.Messages.Events
{
    public class InvoiceModifiedEvent
    {
        public Invoice Invoice { get; set; }
    }
}