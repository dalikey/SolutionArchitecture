﻿namespace RabbitMQ.domain.OrderEvents;

public interface IOrderUpdatedEvent
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
    public string SupplierName { get; set; }
}