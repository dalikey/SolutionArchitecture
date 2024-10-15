using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Infrastructure.Events;

public class EventsRepository
{
    private readonly EventsMySQLContext _sqlContext;

    public EventsRepository(EventsMySQLContext context)
    {
        _sqlContext = context;
    }

    public async Task<bool> SaveEventAsync(string aggregateId, string eventType, object eventData)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

            var orderEvent = new Domain.Events.OrderEvent
            {
                AggregateId = aggregateId,
                EventType = eventType,
                EventData = JsonSerializer.Serialize(eventData, options),
                EventTime = DateTime.Now
            };

            await _sqlContext.Events.AddAsync(orderEvent);
            await _sqlContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }

    }

    public async Task<IEnumerable<Domain.Events.OrderEvent>> GetEventsAsync(string aggregateId)
    {
        return await _sqlContext.Events
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.EventTime)
            .ToListAsync();
    }
}
