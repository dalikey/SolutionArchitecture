using InvoiceManagement.Domain;

namespace InvoiceManagement.Infrastructure
{
    public class InvoiceRepository
    {
        private readonly InvoiceMySQLContext _dbContext;

        public InvoiceRepository(InvoiceMySQLContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async void createInvoice(Invoice invoice)
        {
            await _dbContext.Invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}
