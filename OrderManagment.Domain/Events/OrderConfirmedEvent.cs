using RabbitMQ.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Events;

public class OrderConfirmedEvent : IOrderConfirmedEvent
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string SupplierName { get; set; }
    public string UserName { get; set; }

    public ICollection<Product> Products { get; set; }
    public Dictionary<int, int> ProductsWithQuanitity { get; set; }
}
