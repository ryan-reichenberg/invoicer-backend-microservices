using System.Linq;
using InvoicingService.Domain;
using InvoicingService.DTO;

namespace InvoicingService.Repositories
{
    public static class Extensions
    {
        public static Invoice AsEntity(this InvoiceDto invoiceDto)
            => new(invoiceDto.Id, invoiceDto.Items?.Select(dto => new LineItem(dto.Description, dto.Price, dto.Quantity)).ToList(), invoiceDto.BillFrom, invoiceDto.BillTo, invoiceDto.IssuedAt, invoiceDto.DueDate, invoiceDto.Recurring, invoiceDto.TaxRate, invoiceDto.Tax, invoiceDto.SubTotal, invoiceDto.AdditionalNotes, invoiceDto.ProjectId, invoiceDto.CreatedAt, invoiceDto.ModifiedAt, invoiceDto.Status, invoiceDto.InvoicedTo, invoiceDto.InvoicedFrom);
        
        public static InvoiceDto AsDto(this Invoice invoice)
            =>  new(invoice.Id, invoice.Items?.Select(item => new LineItemDto(item.Description, item.Price, item.Quantity)).ToList(), invoice.BillFrom, invoice.BillTo, invoice.IssuedAt, invoice.DueDate, invoice.Recurring, invoice.TaxRate, invoice.Tax, invoice.SubTotal, invoice.AdditionalNotes, invoice.ProjectId, invoice.CreatedAt, invoice.ModifiedAt, invoice.Status, invoice.InvoicedTo, invoice.InvoicedFrom);
        
    }
}