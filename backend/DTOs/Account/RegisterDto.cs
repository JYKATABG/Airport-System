using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Account;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9]+(?:[._%+-][a-zA-Z0-9]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}$",
    ErrorMessage = "Please enter valid email.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public string? Password { get; set; }
}
