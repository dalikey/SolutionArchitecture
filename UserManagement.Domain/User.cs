﻿namespace UserManagement.Domain;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public List<Support>? Supports { get; set; } = new List<Support>();
}