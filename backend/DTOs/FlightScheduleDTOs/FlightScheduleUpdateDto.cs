using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class FlightScheduleUpdateDto
{
    [RegularExpression(@"^[A-Z0-9/-]+$", ErrorMessage = "Flight mumber must include only uppercase letters, numbers and dashes (exp. LZ-ABC).")]
    public string? FlightNumber { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Aircraft ID must be a positive integer.")]
    public int? AircraftId { get; set; }
    public string? DepartureAirport { get; set; }
    public string? ArrivalAirport { get; set; }
    public DateTime? DepartureTime { get; set; }
    [Range(15, 1440, ErrorMessage = "Estimated duration Minutes must be between 15 and 1440")]
    public int? EstimatedDurationMinutes { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Ticket price cannot be zero or negative")]
    public double? TicketPrice { get; set; }
}
