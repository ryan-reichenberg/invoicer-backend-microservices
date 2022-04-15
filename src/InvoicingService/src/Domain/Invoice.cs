using System;
using System.Collections.Generic;
using System.Linq;
using InvoicingService.DTO;

namespace InvoicingService.Domain
{
    public class Invoice
    {
        public Guid Id;

        public Invoice(Guid id, List<LineItem> items, Contact billTo, Contact billFrom, DateTime issuedAt, DateTime dueDate, bool recurring, double taxRate, decimal tax, decimal subTotal, string additionalNotes, string projectId, DateTime createdAt, DateTime modifiedAt, InvoiceStatus status)
        {
            Id = id;
            Items = items;
            BillTo = billTo;
            BillFrom = billFrom;
            IssuedAt = issuedAt;
            DueDate = dueDate;
            Recurring = recurring;
            TaxRate = taxRate;
            Tax = tax;
            SubTotal = subTotal;
            AdditionalNotes = additionalNotes;
            ProjectId = projectId;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            Status = status;
        }

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
        public InvoiceStatus Status { get; set; }


        // public decimal GetTotalPayableAmount()
        // {
        //     return Items.Aggregate(Decimal.Zero, (sum, x) => sum + x.Price);
        // }

        public void ChangeStatus(InvoiceStatus status)
        {
            Status = status;
        }
    }
}