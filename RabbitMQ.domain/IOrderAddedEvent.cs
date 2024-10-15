using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.domain;

public interface IOrderAddedEvent
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
    public string SupplierName { get; set; }
    public string UserName { get; set; }
}
