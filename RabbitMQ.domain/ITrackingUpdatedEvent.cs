using MassTransit;

namespace RabbitMQ.domain;


public interface ITrackingUpdatedEvent
{
    public int TrackingId { get; set; }
    public string Carrier { get; set; }
    public string Status { get; set; }
    public int SupplierId { get; set; }
    public int ProductId { get; set; }
    public DateTime EstimatedDelivery { get; set; }


}
