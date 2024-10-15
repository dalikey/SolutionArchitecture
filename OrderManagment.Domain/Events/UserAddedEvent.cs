namespace OrderManagement.Domain.Events;

public class UserAddedEvent
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}