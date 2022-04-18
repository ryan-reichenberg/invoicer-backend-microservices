using System;
using System.Collections.Generic;
using System.Linq;
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
        

        public async Task<List<Invoice>> GetAllInvoicesForUserAsync(string id)
        {
            var invoices = await _dbContext.Invoices.Where(x => x.InvoicedFrom.ToString() == id).ToListAsync();
            return invoices.Select(dto => dto.AsEntity()).ToList();
        }

        public async Task<List<Invoice>> GetInvoicesBy(InvoiceStatus? status, Guid? invoicedTo, DateTime? issuedAfter, DateTime? issuedBefore,
            Guid? projectId)
        {
            var invoiceDbSet = _dbContext.Invoices.AsQueryable();
            
            if (status != null)
            {
                invoiceDbSet = invoiceDbSet.Where(x => x.Status == status);
            }
            if (invoicedTo != null)
            {
                invoiceDbSet = invoiceDbSet.Where(x => x.InvoicedTo == invoicedTo);
            }
            if (issuedAfter != null)
            {
                invoiceDbSet = invoiceDbSet.Where(x => x.IssuedAt > issuedAfter);
            }
            if (issuedBefore != null)
            {
                invoiceDbSet = invoiceDbSet.Where(x => x.IssuedAt < issuedBefore);
            }

            var invoices = await invoiceDbSet.ToListAsync();
                    

            return invoices.Select(dto => dto.AsEntity()).ToList();
        }

        public async Task<Invoice> GetByIdAsync(string id)
        {
            var invoice = await _dbContext.Invoices.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            return invoice.AsEntity();
        }
    }
}