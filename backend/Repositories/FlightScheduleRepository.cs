using System;
using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class FlightScheduleRepository : IFlightScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public FlightScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FlightSchedule>> GetAllAsync()
    {
        return await _context.FlightSchedules.ToListAsync();
    }
    public async Task<FlightSchedule?> GetByIdAsync(int id)
    {
        return await _context.FlightSchedules.FindAsync(id);
    }

    public async Task CreateAsync(FlightSchedule flightSchedule)
    {
        await _context.FlightSchedules.AddAsync(flightSchedule);
    }

    public void Remove(FlightSchedule flightSchedule)
    {
        _context.FlightSchedules.Remove(flightSchedule);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ExistByFlightNumber(string flightNumber)
    {
        return await _context.FlightSchedules.AnyAsync(flight =>
         flight.FlightNumber.ToLower() == flightNumber.ToLower());
    }
}
