using MassTransit;
using TrackingManagement.Domain;
using TrackingManagement.Domain.Events;
using TrackingManagement.Infrastructure;
using RabbitMQ.domain;

namespace TrackingManagement.Services;

public class TrackingService
{
    private readonly TrackingRepository _trackingRepository;
    private readonly IBus _bus;


    public TrackingService(TrackingRepository trackingRepository, IBus bus)
    {
        _trackingRepository = trackingRepository;
        _bus = bus;
    }


    private string FindCheapestCarrier()
    {

        //Randomly return a carrier
        string[] carriers = new string[] { "DHL", "DPD", "PostNL" };

        Random random = new Random();
        int index = random.Next(carriers.Length);
        return carriers[index];
    }

    public async Task RegisterTrackingAsync(TrackingData tracking)
    {
        // Add to database
        var insertResult = await _trackingRepository.AddTrackingAsync(tracking);
        if (insertResult == null)
            return;
        // Publish event
        ITrackingStartedEvent @event = new TrackingStartedEvent()
        {
            TrackingId = tracking.TrackingId,
            SupplierId = tracking.SupplierId,
            ProductId = tracking.ProductId,
            Carrier = FindCheapestCarrier(),
            Status = tracking.Status,
            EstimatedDelivery = tracking.EstimatedDelivery
        };

        await _bus.Publish(@event,  x=>
        {
            x.SetRoutingKey("tracking-registered-routingkey");
        });
    }

    public async Task UpdateTrackingAsync(TrackingData tracking)
    {
        // Add to database
        var insertResult = await _trackingRepository.UpdateOrderAsync(tracking);
        if (insertResult == null)
            return;
        // Publish event
        ITrackingUpdatedEvent @event = new TrackingUpdatedEvent()
        {
            TrackingId = tracking.TrackingId,
            SupplierId = tracking.SupplierId,
            ProductId = tracking.ProductId,
            Carrier = tracking.Carrier,
            Status = tracking.Status,
            EstimatedDelivery = tracking.EstimatedDelivery
        };

        await _bus.Publish(@event);
    }
    public async Task GetTrackingAsync(int trackingId)
    {
        var insertResult = await _trackingRepository.GetTrackingAsync(trackingId);
        if (insertResult == null)
            return;
        await _trackingRepository.GetTrackingAsync(trackingId);
    }

}
