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

        Task<List<Invoice>> GetAllAsync();

        Task<Invoice> GetByIdAsync(string id);
    }
}