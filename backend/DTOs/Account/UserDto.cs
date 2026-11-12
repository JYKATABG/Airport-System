using System;

namespace backend.DTOs.Account;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}
