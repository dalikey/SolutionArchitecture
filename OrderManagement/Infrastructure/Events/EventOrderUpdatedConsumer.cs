using MassTransit;
using MongoDB.Bson;
using OrderManagement.Infrastructure.Order;
using RabbitMQ.domain;
using RabbitMQ.domain.OrderEvents;

namespace OrderManagement.Infrastructure.Events;

public class EventOrderUpdatedConsumer : IConsumer<IOrderUpdatedEvent>, IConsumer<IOrderAddedEvent>
{
    private readonly EventsRepository _eventsRepository;
    private readonly OrderRepository _orderRepository;

    public EventOrderUpdatedConsumer(EventsRepository eventsRepository, OrderRepository orderRepository)
    {
        _eventsRepository = eventsRepository;
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<IOrderUpdatedEvent> context)
    {
        Console.WriteLine("Order received on Events: " + context.Message.ToString());
        
        IOrderUpdatedEvent @event = context.Message;
        await _orderRepository.UpdateOrderAsync(new Domain.Order()
        {
            OrderNumber = @event.OrderNumber,
            OrderDate = @event.OrderDate,
            Status = @event.Status,
            UserId = @event.UserId,
            SupplierName = @event.SupplierName
        });
    }

    public async Task Consume(ConsumeContext<IOrderAddedEvent> context)
    {
        Console.WriteLine("Order received on Events: " + context.Message.ToString());

        IOrderAddedEvent @event = context.Message;

        await _orderRepository.AddOrderAsync(new Domain.Order()
        {
            OrderNumber = @event.OrderNumber,
            OrderDate = @event.OrderDate,
            Status = @event.Status,
            UserId = 1,
            SupplierName = @event.SupplierName
        });
    }
}
