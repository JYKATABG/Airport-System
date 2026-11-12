using System;

namespace backend.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User"; // | Admin
    public string? FullName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}
