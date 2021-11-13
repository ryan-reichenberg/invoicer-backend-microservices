using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoicingService.Domain
{
    public class Invoice
    {
        public Guid Id;
        public String InvoiceId;
        public List<LineItem> Items { get; set; }
        public Contact BillTo { get; set; }
        public Contact BillFrom { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime DueDate { get; set; }
        public bool Recurring { get; set; }
        public double TaxRate { get; set; }
        public decimal Tax { get; set; }
        public string AdditionalNotes { get; set; }
        public string ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public InvoiceStatus Status { get; set; }


        public decimal GetTotalPayableAmount()
        {
            return Items.Aggregate(Decimal.Zero, (sum, x) => sum + x.Price);
        }

        public void ChangeStatus(InvoiceStatus status)
        {
            Status = status;
        }
    }
}