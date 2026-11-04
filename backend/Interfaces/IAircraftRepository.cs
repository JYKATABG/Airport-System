using System;
using backend.DTOs.Aircraft;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Interfaces;

public interface IAircraftRepository
{
    Task<IEnumerable<Aircraft>> GetAllAsync();
    Task<Aircraft?> GetByIdAsync(int id);
    Task<bool> ExistByRegistrationNumberAsync(string registrationNumber);
    Task CreateAsync(Aircraft aircraft);
    Task<bool> SaveChangesAsync();
    void Remove(Aircraft aircraft);
}
