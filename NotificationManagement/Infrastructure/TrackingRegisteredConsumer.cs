using MassTransit;
using NotificationManagement.Service;
using RabbitMQ.domain;

namespace NotificationManagement.Infrastructure;

public class TrackingRegisteredConsumer : IConsumer<ITrackingStartedEvent>, IConsumer<ITrackingUpdatedEvent>
{
    private readonly NotificationService _notificationService;

    public TrackingRegisteredConsumer(NotificationService service)
    {
        _notificationService = service;
    }

    public async Task Consume(ConsumeContext<ITrackingStartedEvent> context)
    {
        ITrackingStartedEvent order = context.Message;

        //TODO: Convert event to invoice
        _notificationService.SendNotification("Your product with TrackinID: " + order.TrackingId + " from: " + order.ProductId + " has been orderd, the estimated delivery time is: " + order.EstimatedDelivery);
    }

    public async Task Consume(ConsumeContext<ITrackingUpdatedEvent> context)
    {
        ITrackingUpdatedEvent order = context.Message;

        //TODO: Convert event to invoice
        _notificationService.SendNotification("Your product with TrackinID: " + order.TrackingId + " from: " + order.ProductId + " has been updated, the estimated delivery time is: " + order.EstimatedDelivery);
    }
}
