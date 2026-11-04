using System;

namespace backend.DTOs;

public class FlightScheduleResponseDto
{
    public int Id { get; set; }
    public required string FlightNumber { get; set; } = string.Empty;
    public int AircraftId { get; set; }
    public required string DepartureAirport { get; set; } = string.Empty;
    public required string ArrivalAirport { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; } = DateTime.UtcNow;
    public int EstimatedDurationMinutes { get; set; }
    public double TicketPrice { get; set; }
}
