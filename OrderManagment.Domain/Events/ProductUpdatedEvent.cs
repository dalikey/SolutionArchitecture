namespace OrderManagement.Domain.Events;

public class ProductUpdatedEvent
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public float Price { get; set; }
}