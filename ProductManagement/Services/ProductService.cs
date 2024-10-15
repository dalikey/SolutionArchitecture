using MassTransit;
using ProductManagement.Domain;
using ProductManagement.Domain.Events;
using ProductManagement.Infrastructure;
using RabbitMQ.domain;

namespace ProductManagement.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly IBus _bus;


    public ProductService(ProductRepository productRepository, IBus bus)
    {
        _productRepository = productRepository;
        _bus = bus;
    }

    public async Task RegisterProductAsync(Product product)
    {
        // Add to database
        var insertResult = await _productRepository.AddProductAsync(product);
        if (insertResult == null)
            return;
        // Publish event
        IInsertedEvent @event = new ProductInsertedEvent
        {
            ProductName = insertResult.ProductName,
            ProductDescription = insertResult.ProductDescription,
            Price = insertResult.Price,
            StockQuantity = insertResult.StockQuantity,
            Category = insertResult.Category
        };

        await _bus.Publish(@event);
    }

}
