using RabbitMQ.domain;

namespace OrderManagement.Domain.Events;

public class OrderAddedEvent : IOrderAddedEvent
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
    public string SupplierName { get; set; }
    public string UserName { get; set; }

    public ICollection<Product> Products { get; set; } 
}
