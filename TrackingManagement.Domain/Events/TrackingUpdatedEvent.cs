using MassTransit;
using RabbitMQ.domain;

namespace TrackingManagement.Domain.Events;
public class TrackingUpdatedEvent : ITrackingUpdatedEvent
{
    public int TrackingId { get; set; }
    public int SupplierId { get; set; }
    public int ProductId { get; set; }
    public string Carrier { get; set; }
    public string Status { get; set; }
    public DateTime EstimatedDelivery { get; set; }
}
