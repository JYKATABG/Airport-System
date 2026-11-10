using System;
using backend.Data;
using backend.DTOs.Account;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    public AccountController(ITokenService tokenService, ApplicationDbContext context)
    {
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var usernameExist = await _context.Users.AnyAsync(u => u.Username == dto.Username);
        if (usernameExist)
        {
            return Conflict("Username already exist");
        }

        var user = new User
        {
            Username = dto.Username!,
            Email = dto.Email!,
            Role = "User"
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password!);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _tokenService.CreateToken(user.Username, user.Role);

        return Ok(new
        {
            username = dto.Username,
            email = dto.Email,
            token = token
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password!);

        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Invalid credentials");
        }



        var token = _tokenService.CreateToken(user.Username, user.Role);

        return Ok(new
        {
            username = user.Username,
            token = token
        });
    }
}
