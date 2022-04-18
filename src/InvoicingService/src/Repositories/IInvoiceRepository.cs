using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoicingService.Domain;
using InvoicingService.DTO;

namespace InvoicingService.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> SaveAsync(InvoiceDto invoice);
        Task<Invoice> DeleteAsync(string id);

        Task<Invoice> UpdateAsync(InvoiceDto invoice);
        
        Task<List<Invoice>> GetAllInvoicesForUserAsync(string id);
        
        Task<List<Invoice>> GetInvoicesBy(InvoiceStatus? status, Guid? invoicedTo, DateTime? issuedAfter, DateTime? issuedBefore, Guid? projectId);

        Task<Invoice> GetByIdAsync(string id);
    }
}