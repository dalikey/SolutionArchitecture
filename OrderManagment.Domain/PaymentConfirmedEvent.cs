using RabbitMQ.domain;

namespace OrderManagement.Infrastructure;

public class PaymentConfirmedEvent : IPaymentConfirmedEvent
{
    public string PaymentId { get; set; }
    public string OrderNumber { get; set; }
    public Dictionary<int, int> ProductIdQuantity { get; set; } = new Dictionary<int, int>();
    public string UserName { get; set; }
    public string SupplierName { get; set; }
    public bool IsForwardPaid { get; set; }
}
