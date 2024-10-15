namespace OrderManagement.Domain.Events;

public class OrderEvent
{
    public long EventId { get; set; }
    public string AggregateId { get; set; }
    public string EventType { get; set; }
    public string EventData { get; set; }
    public DateTime EventTime { get; set; }
}
