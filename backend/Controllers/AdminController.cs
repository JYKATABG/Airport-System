using System;
using backend.Data;
using backend.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _context.Users
        .Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Role = u.Role,
            FullName = u.FullName,
            Address = u.Address
        })
        .ToListAsync();

        return Ok(users);
    }

    [HttpPut("users/{id}/role")]
    public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound("User not found");

        user.Role = dto.NewRole;
        await _context.SaveChangesAsync();

        return Ok(new { message = $"{user.Username}'s role was successfully changed to {user.Role}" });
    }
}
