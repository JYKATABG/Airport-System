using System;
using backend.Data;
using backend.DTOs.TicketDTOs;
using backend.Enums;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TicketService : ITicketRepository
{

    private readonly ApplicationDbContext _context;

    public TicketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TicketResult> BuyAsync(int userId, TicketCreateDto dto)
    {
        var flight = await _context.FlightSchedules.FindAsync(dto.FlightScheduleId);
        if (flight == null)
        {
            return new(null, TicketError.FlightNotFound);
        }

        if (flight.DepartureTime <= DateTime.UtcNow)
        {
            return new(null, TicketError.FlightDeparted);
        }

        var seat = dto.SeatNumber.ToUpper();

        var taken = await _context.Tickets.AnyAsync(t =>
        t.FlightScheduleId == flight.Id &&
        t.SeatNumber == seat &&
        t.Status != TicketStatus.Cancelled);
        if (taken)
        {
            return new(null, TicketError.SeatTaken);
        }

        var ticket = new Ticket
        {
            UserId = userId,
            FlightScheduleId = flight.Id,
            SeatNumber = seat,
            Price = flight.TicketPrice,
            Status = TicketStatus.Booked
        };

        _context.Tickets.Add(ticket);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(null, TicketError.SeatTaken);
        }

        return new(ticket);
    }

    public async Task<TicketError> CancelTicket(int id, int userId, bool isAdmin)
    {
        var ticket = await _context.Tickets
        .Include(t => t.FlightSchedule)
        .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return TicketError.NotFound;
        }

        if (!isAdmin && ticket.UserId != userId)
        {
            return TicketError.Forbbiden;
        }

        if (ticket.FlightSchedule!.DepartureTime <= DateTime.UtcNow)
        {
            return TicketError.FlightDeparted;
        }

        ticket.Status = TicketStatus.Cancelled;
        await _context.SaveChangesAsync();

        return TicketError.None;
    }

    public async Task<TicketResult> GetByIdAsync(int id, int userId, bool isAdmin)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return new(null, TicketError.NotFound);
        }

        if (!isAdmin && ticket.UserId != userId)
        {
            return new(null, TicketError.Forbbiden);
        }

        return new(ticket);
    }

    public async Task<IEnumerable<Ticket>> GetByUserAsync(int userId)
    {
        return await _context.Tickets
        .Where(t => t.UserId == userId)
        .OrderByDescending(t => t.PurchasedAt)
        .ToListAsync();
    }
}
