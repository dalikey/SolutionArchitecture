using RabbitMQ.domain.UserEvents;

namespace UserManagement.Domain.Events;

public class UserUpdatedEvent : IUserUpdatedEvent
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}
