using System;
using backend.Data;
using backend.DTOs.Models;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class AircraftRepository : IAircraftRepository
{
    private readonly ApplicationDbContext _context;

    public AircraftRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Aircraft>> GetAllAsync()
    {
        return await _context.Aircrafts.ToListAsync();
    }

    public async Task<Aircraft?> GetByIdAsync(int id)
    {
        return await _context.Aircrafts.FindAsync(id);
    }

    public async Task CreateAsync(Aircraft aircraft)
    {
        await _context.Aircrafts.AddAsync(aircraft);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExistByRegistrationNumberAsync(string registrationNumber)
    {
        return await _context.Aircrafts.AnyAsync(a =>
         a.RegistrationNumber.ToLower() == registrationNumber.ToLower());
    }

    public void Remove(Aircraft aircraft)
    {
        _context.Aircrafts.Remove(aircraft);
    }
}
