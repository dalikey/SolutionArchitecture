using MassTransit;
using NotificationManagement.Service;
using RabbitMQ.domain;

namespace NotificationManagement.Infrastructure;

public class OrderConfirmedConsumer : IConsumer<IOrderConfirmedEvent>
{

    private readonly NotificationService _notificationService;
    public OrderConfirmedConsumer(NotificationService service)
    {
        _notificationService = service;

    }

    public async Task Consume(ConsumeContext<IOrderConfirmedEvent> context)
    {
        IOrderConfirmedEvent order = context.Message;

        //TODO: Convert event to invoice
        _notificationService.SendNotification("Notification sent to "+ order.UserName +": Your package from " + order.SupplierName + " will be deliverd in the coming 24 hours :)");
    }
}

