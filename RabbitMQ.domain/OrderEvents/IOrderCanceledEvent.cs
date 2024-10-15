﻿namespace RabbitMQ.domain.OrderEvents;

public interface IOrderCanceledEvent
{
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
}