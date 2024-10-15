using RabbitMQ.domain;

namespace InvoiceManagement.Domain;

public class OrderConfirmedEvent : IOrderConfirmedEvent
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string SupplierName { get; set; }
    public string UserName { get; set; }
    public Dictionary<int, int> ProductsWithQuanitity { get; set; }
}
