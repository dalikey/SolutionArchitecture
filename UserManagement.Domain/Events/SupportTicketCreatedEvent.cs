using RabbitMQ.domain;

namespace UserManagement.Domain.Events;

public class SupportTicketCreatedEvent : ISupportTicketCreatedEvent
{
    public string SupportTicketNumber { get; set; }
    public string UserEmail { get; set; }
    public DateTime IssueDate { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public int SupportId { get; set; }
    public int UserId { get; set; }
}
