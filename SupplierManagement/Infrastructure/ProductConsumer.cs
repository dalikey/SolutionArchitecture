using MassTransit;
using RabbitMQ.domain;
using SupplierManagement.Domain.Events;

namespace SupplierManagement.Infrastructure;

public class ProductConsumer : IConsumer<IInsertedEvent>
{
    public Task Consume(ConsumeContext<IInsertedEvent> context)
    {
        Console.WriteLine("Product received on SupplierManagement: " + context.Message.ToString());
        return Task.CompletedTask;
    }
}
