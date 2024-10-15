using MassTransit;
using OrderManagement.Services;
using RabbitMQ.domain;

namespace OrderManagement.Infrastructure;

public class PaymentConfirmedConsumer : IConsumer<IPaymentConfirmedEvent>, IConsumer<IAfterPaymentConfirmedEvent>
{
    private readonly OrderService _orderService;
    public PaymentConfirmedConsumer(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task Consume(ConsumeContext<IPaymentConfirmedEvent> context)
    {
        Console.WriteLine("PaymentConfirmedConsumer");
        // Publish OrderConfirmedEvent
        IPaymentConfirmedEvent @event = context.Message;

        Domain.Order order = new() { User = new Domain.User()};

        order.OrderNumber = @event.OrderNumber;
        order.OrderDate = DateTime.Now;
        order.Status = "Paying after";
        order.User.Email = @event.UserName;
        order.SupplierName = @event.SupplierName;
        order.ProductIdQuantity = @event.ProductIdQuantity;

        order.Products = new List<Domain.Product>();

        foreach (var item in @event.ProductIdQuantity)
        {
            order.Products.Add(new Domain.Product() { ProductId = item.Key, Quantity = item.Value });
        }


        await _orderService.AddOrder(order);
    }

    public async Task Consume(ConsumeContext<IAfterPaymentConfirmedEvent> context)
    {
        Console.WriteLine("AfterPaymentConfirmedConsumer");
        IAfterPaymentConfirmedEvent @event = context.Message;
        var result = await _orderService.GetOrderByOrdernumber(@event.OrderNumber);
        if(result == null)
        {
            Console.WriteLine("Order not found.");
            return;
        }
        if(result.Status == "Paid")
        {
            Console.WriteLine("Order already paid.");
            return;
        }
        result.Status = "Paid";
        await _orderService.UpdateOrder(result);
    }
}
