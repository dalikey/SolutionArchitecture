using MassTransit;
using RabbitMQ.domain;

namespace OrderManagement.Infrastructure.Product;

public class ProductInsertedConsumer : IConsumer<IInsertedEvent>
{
    private readonly ProductRepository _productRepository;
    public ProductInsertedConsumer(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<IInsertedEvent> context)
    {

        var product = new Domain.Product()
        {
            ProductId = context.Message.ProductId,
            ProductName = context.Message.ProductName,
            ProductDescription = "Not used",
            Price = 0,
            Quantity = 0,
            Category = "Not used",            
        };

        await _productRepository.AddProductAsync(product);
    }
}
