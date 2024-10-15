namespace OrderManagement.Domain;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
}
