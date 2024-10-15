using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain;

public class Support
{
    [Key]
    public int SupportId { get; set; }
    public string SupportTicketNumber { get; set; }
    public string UserEmail { get; set; }
    public DateTime IssueDate { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }
}
