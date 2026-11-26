using System;
using backend.DTOs.TicketDTOs;
using backend.Models;

namespace backend.Interfaces;

public enum TicketError { None, NotFound, FlightNotFound, FlightDeparted, SeatTaken, Forbidden }

public record TicketResult(Ticket? Ticket, TicketError Error = TicketError.None);
public interface ITicketRepository
{
    public Task<TicketResult> BuyAsync(int userId, TicketCreateDto dto);
    public Task<IEnumerable<Ticket>> GetByUserAsync(int userId);
    public Task<TicketResult> GetByIdAsync(int id, int userId, bool isAdmin);
    public Task<TicketError> CancelTicket(int id, int userId, bool isAdmin);
}
