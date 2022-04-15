using System;
using System.Collections.Generic;
using InvoicingService.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InvoicingService.DTO
{
    public class InvoiceDto
    {
        public InvoiceDto()
        {
        }

        public InvoiceDto(Guid id, DateTime issuedAt, DateTime dueDate, bool recurring, double taxRate, decimal tax, decimal subTotal, string additionalNotes, string projectId, DateTime createdAt, DateTime modifiedAt, InvoiceStatus status)
        {
            Id = id;
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
        public InvoiceDto(Guid id, List<LineItemDto>? items, Contact? billTo, Contact? billFrom, DateTime issuedAt, DateTime dueDate, bool recurring, double taxRate, decimal tax, decimal subTotal, string additionalNotes, string projectId, DateTime createdAt, DateTime modifiedAt, InvoiceStatus status)
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
        
        public Guid Id { get; set; }
        public List<LineItemDto> Items { get; set; }
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
    }
}