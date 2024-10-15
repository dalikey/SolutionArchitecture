using OrderManagement.Infrastructure;
using OrderManagement.Domain;
using OrderManagement.Infrastructure.Order;
using OrderManagement.Domain.Events;
using MassTransit;
using RabbitMQ.domain;
using RabbitMQ.domain.OrderEvents;
using OrderManagement.Infrastructure.Events;
using MassTransit.Transports;

namespace OrderManagement.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly EventsRepository _eventRepository;
    private readonly IBus _bus;

    public OrderService(OrderRepository orderRepository, EventsRepository eventsRepository, IBus bus)
    {
        _orderRepository = orderRepository;
        _eventRepository = eventsRepository;
        _bus = bus;
    }

    public async Task<bool> AddOrder(Order order)
    {





        var result = await _eventRepository.SaveEventAsync(order.OrderNumber, "OrderAdded", new OrderAddedEvent()
        {
            OrderDate = order.OrderDate,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            UserId = order.UserId,
            Products = order.Products,
        });

        if (!result)
            return false;

        IOrderConfirmedEvent @event = new OrderConfirmedEvent() { OrderDate = order.OrderDate, OrderNumber = order.OrderNumber, SupplierName = order.SupplierName, UserName = order.User.Email, ProductsWithQuanitity = order.ProductIdQuantity };
        await _bus.Publish(@event, x =>
        {
            x.SetRoutingKey("order-confirmed-routingkey");
        });


        IOrderAddedEvent @addEvent = new OrderAddedEvent()
        {
            OrderDate = order.OrderDate,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            UserId = order.UserId,
            SupplierName = order.SupplierName,
            Products = order.Products
        };
        await _bus.Publish(@addEvent);


        return true;
    }

    public async Task<bool> UpdateOrder(Order order)
    {

        var result = await _eventRepository.SaveEventAsync(order.OrderNumber, "OrderUpdated", new OrderUpdatedEvent()
        {
            OrderDate = order.OrderDate,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            UserId = order.UserId
        });

        if (result == null)
            return false;

        IOrderUpdatedEvent @event = new OrderUpdatedEvent()
        {
            OrderDate = order.OrderDate,
            OrderNumber = order.OrderNumber,
            Status = order.Status,
            UserId = order.UserId,
            SupplierName = order.SupplierName
        };

        await _bus.Publish(@event);
        return true;
    }

    public async Task<bool> CancelOrder(string OrderNumber)
    {
        var result = await _orderRepository.CancelOrderAsync(OrderNumber);

        if (result == null)
            return false;

        IOrderCanceledEvent @event = new OrderCanceledEvent()
        {
            OrderDate = result.OrderDate,
            OrderNumber = result.OrderNumber,
            Status = result.Status,
            UserId = result.UserId,
        };

        await _bus.Publish(@event);
        return true;
    }

    public async Task<Order> GetOrderByOrdernumber(string orderNumber)
    {
        var order = await _orderRepository.GetOrderAsync(orderNumber);
        return order;
    }
}
