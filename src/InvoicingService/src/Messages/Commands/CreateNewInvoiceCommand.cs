using System;
using System.Collections.Generic;
using Convey.CQRS.Commands;
using InvoicingService.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InvoicingService.Messages.Commands
{
    public class CreateNewInvoiceCommand : ICommand
    {
        public Guid Id;
        public Guid InvoicedFrom { get; set; }
        public Guid InvoicedTo { get; set; }
        public List<LineItem> Items { get; set; }
        public Contact BillTo { get; set; }
        public Contact BillFrom { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime DueDate { get; set; }
        public bool Recurring { get; set; }
        public double TaxRate { get; set; }
        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public string AdditionalNotes { get; set; }
        public string ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public InvoiceStatus Status { get; set; }
    
        [JsonConstructor]
        public CreateNewInvoiceCommand()
        {
        }
    }
}