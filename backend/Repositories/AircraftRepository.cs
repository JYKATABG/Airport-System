using System;
using backend.Data;
using backend.Models;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.DTOs.Aircraft;
using backend.Helpers;

namespace backend.Repositories;

public class AircraftRepository : IAircraftRepository
{
    private readonly ApplicationDbContext _context;

    public AircraftRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Aircraft>> GetAllAsync(AircraftQueryObject query)
    {

        var queryable = _context.Aircrafts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.ModelName))
        {
            var lowerModel = query.ModelName.ToLower();
            queryable = queryable.Where(aircraft => aircraft.ModelName.ToLower().Contains(lowerModel));
        }

        if (query.IsAirWorthy.HasValue)
        {
            queryable = queryable.Where(aircraft => aircraft.IsAirWorthy == query.IsAirWorthy.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Model", StringComparison.OrdinalIgnoreCase))
            {
                queryable = query.IsDescending ? queryable.OrderByDescending(f => f.ModelName) : queryable.OrderBy(f => f.ModelName);
            }

            if (query.SortBy.Equals("Hours", StringComparison.OrdinalIgnoreCase))
            {
                queryable = query.IsDescending ? queryable.OrderByDescending(f => f.TotalFlightHours) : queryable.OrderBy(f => f.TotalFlightHours);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await queryable.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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
