using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.domain;

public interface IAfterPaymentConfirmedEvent
{
    public string PaymentId { get; set; }
    public string OrderNumber { get; set; }
}
