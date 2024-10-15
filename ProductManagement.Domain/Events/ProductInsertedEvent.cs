using MassTransit;
using RabbitMQ.domain;

namespace ProductManagement.Domain.Events;


public class ProductInsertedEvent : IInsertedEvent
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Price { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; }
    public int SupplierId { get; set; }

    public int ProductId { get; set; }
}
