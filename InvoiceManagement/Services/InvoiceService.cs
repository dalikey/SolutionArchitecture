using InvoiceManagement.Domain;
using InvoiceManagement.Infrastructure;

namespace InvoiceManagement.Services;

public class InvoiceService
{
    private readonly InvoiceRepository _invoiceRepository;
    public InvoiceService(InvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async void createInvoice(Invoice invoice)
    {
        _invoiceRepository.createInvoice(invoice);

        Console.WriteLine("Sending invoice to user by email");

        Console.WriteLine("Invoice sent to: " + invoice.UserName);
    }
}
