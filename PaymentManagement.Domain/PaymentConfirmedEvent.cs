using RabbitMQ.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement.Domain;

public class PaymentConfirmedEvent : IPaymentConfirmedEvent
{
    public string PaymentId { get; set; }
    public string OrderNumber { get; set; }
    public Dictionary<int, int> ProductIdQuantity { get; set; }
    public string UserName { get; set; }
    public string SupplierName { get; set; }
    public bool IsForwardPaid { get; set; }
}
