using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Account;

public class UpdateUserRoleDto
{
    [Required(ErrorMessage = "New role is required.")]
    [RegularExpression("^(User|Admin)$", ErrorMessage = "Only allowed roles are 'User' and 'Admin'.")]
    public string NewRole { get; set; } = string.Empty;
}
