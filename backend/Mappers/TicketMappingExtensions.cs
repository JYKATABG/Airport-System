using System;
using backend.DTOs.TicketDTOs;
using backend.Models;

namespace backend.Mappers;

public static class TicketMappingExtensions
{

    public static Ticket ToTicketFromCreateDto(this Ticket dto) => new()
    {
        FlightSchedule = dto.FlightSchedule,
        SeatNumber = dto.SeatNumber,
        Price = dto.Price,
        Status = dto.Status,
        PurchasedAt = dto.PurchasedAt
    };

    public static TicketResponseDto ToDto(this Ticket ticket) => new()
    {
        Id = ticket.Id,
        FlightScheduleId = ticket.FlightScheduleId,
        SeatNumber = ticket.SeatNumber,
        Price = ticket.Price,
        Status = ticket.Status,
        PurchasedAt = ticket.PurchasedAt
    };

    public static void ApplyPartialUpdate(this Ticket ticket, TicketUpdateDto dto)
    {
        if (dto.SeatNumber != null)
        {
            ticket.SeatNumber = dto.SeatNumber;
        }

        if (dto.Status != null)
        {
            ticket.Status = dto.Status.Value;
        }
    }
}
