using RabbitMQ.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement.Domain;

public class AfterPaymentConfirmedEvent : IAfterPaymentConfirmedEvent
{
    public string PaymentId { get; set; }
    public string OrderNumber { get; set; }
}

