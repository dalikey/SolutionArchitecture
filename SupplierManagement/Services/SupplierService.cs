using MassTransit;
using RabbitMQ.domain;
using SupplierManagement.Domain;
using SupplierManagement.Domain.Events;
using SupplierManagement.Infrastructure;

namespace SupplierManagement.Services;

public class SupplierService
{
    private readonly SupplierRepository _supplierRepository;
    private readonly IBus _bus;

    public SupplierService(SupplierRepository supplierRepository, IBus bus)
    {
        _supplierRepository = supplierRepository;
        _bus = bus;
    }

    public async Task RegisterSupplierAsync(Supplier supplier)
    {
        await _supplierRepository.AddSupplierAsync(supplier);
    }

    public async Task InsertProduct(Product product, int supplierId)
    {

        // Insert into supplier database only the product id and name

        var productWithId = await _supplierRepository.AddProductOfSupplierAsync(product, supplierId);
        Console.WriteLine(productWithId.ProductId);
        // Publish the event so that the product management service can insert the product into its database
        IInsertedEvent @event = new ProductInsertedEvent
        {
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            Price = (int)product.Price,
            StockQuantity = (int)product.StockQuantity,
            Category = product.Category,
            SupplierId = supplierId,
            ProductId = productWithId.ProductId
        };
        await _bus.Publish(@event);

    }
}
