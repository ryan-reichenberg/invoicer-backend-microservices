using System;
using InvoicingService.DTO;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace InvoicingService.Repositories
{
    public class InvoiceDbContext : DbContext
    {
        public DbSet<InvoiceDto> Invoices { get; set; }

        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InvoiceDto>().ToTable("Invoice");
            base.OnModelCreating(builder);
        }

        public void MigrateDB()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }
            
    }
}