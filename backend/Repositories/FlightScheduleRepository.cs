using System;
using backend.Data;
using backend.Helpers;
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

    public async Task<IEnumerable<FlightSchedule>> GetAllAsync(FlightScheduleQueryObject query)
    {
        var queryable = _context.FlightSchedules.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.ArrivalAirport))
        {
            queryable = queryable.Where(flight => flight.ArrivalAirport.ToUpper() == query.ArrivalAirport.ToUpper());
        }

        if (!string.IsNullOrWhiteSpace(query.DepartureAirport))
        {
            queryable = queryable.Where(flight => flight.DepartureAirport.ToUpper() == query.DepartureAirport.ToUpper());
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
            {
                queryable = query.IsDescending ? queryable.OrderByDescending(f => f.TicketPrice) : queryable.OrderBy(f => f.TicketPrice);
            }

            if (query.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
            {
                queryable = query.IsDescending ? queryable.OrderByDescending(f => f.DepartureTime) : queryable.OrderBy(f => f.DepartureAirport);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await queryable.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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
