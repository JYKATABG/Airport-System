using System;
using backend.Enums;

namespace backend.DTOs.TicketDTOs;

public class TicketResponseDto
{
    public int Id { get; set; }
    public int FlightScheduleId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime PurchasedAt { get; set; }
}
