using System.Collections.Generic;
using System.Threading.Tasks;
using InvoicingService.Domain;
using InvoicingService.DTO;
using Microsoft.EntityFrameworkCore;

namespace InvoicingService.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private InvoiceDbContext _dbContext;
        public InvoiceRepository(InvoiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invoice> SaveAsync(InvoiceDto invoice)
        {
            _dbContext.Invoices.Add(invoice);
            await _dbContext.SaveChangesAsync();
            return invoice.AsEntity();
        }

        public Task<Invoice> DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Invoice> UpdateAsync(InvoiceDto invoice)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Invoice>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Invoice> GetByIdAsync(string id)
        {
            var invoice = await _dbContext.Invoices.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            return invoice.AsEntity();
        }
    }
}