using System;

namespace backend.Models;

public class FlightSchedule
{
    public int Id { get; set; }
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    public required string FlightNumber { get; set; } = string.Empty;
    public required string DepartureAirport { get; set; } = string.Empty;
    public required string ArrivalAirport { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; } = DateTime.UtcNow;
    public int EstimatedDurationMinutes { get; set; }
    public double TicketPrice { get; set; }
}
