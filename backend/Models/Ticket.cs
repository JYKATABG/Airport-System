using System;
using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.Models;

public class Ticket
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public int FlightScheduleId { get; set; }
    public FlightSchedule? FlightSchedule { get; set; }

    public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    [Required, MaxLength(5)]
    public string SeatNumber { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Booked;
}
