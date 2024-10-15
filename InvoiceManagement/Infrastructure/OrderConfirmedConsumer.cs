using InvoiceManagement.Domain;
using InvoiceManagement.Services;
using MassTransit;
using RabbitMQ.domain;

namespace InvoiceManagement.Infrastructure;

public class OrderConfirmedConsumer : IConsumer<IOrderConfirmedEvent>
{

    private readonly InvoiceService _invoiceService;

    public OrderConfirmedConsumer(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }
    public async Task Consume(ConsumeContext<IOrderConfirmedEvent> context)
    {
        Console.WriteLine("OrderConfirmedConsumer");
        IOrderConfirmedEvent order = context.Message;

        _invoiceService.createInvoice(new Invoice() { OrderNumber = order.OrderNumber, SupplierName = order.SupplierName, InvoiceDate = order.OrderDate, InvoiceNumber = Guid.NewGuid().ToString(), UserName = order.UserName, Products= order.ProductsWithQuanitity });


    }
}
