using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace backend.Models;

public class FlightSchedule : IValidatableObject
{
    public int Id { get; set; }
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    [Required(ErrorMessage = "Flight number is required")]
    [RegularExpression(@"^[A-Z0-9/-]+$", ErrorMessage = "Flight mumber must include only uppercase letters, numbers and dashes (exp. LZ-ABC).")]

    public required string FlightNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Departure airport is required.")]
    public required string DepartureAirport { get; set; } = string.Empty;
    [Required(ErrorMessage = "Arrival airport is required.")]
    public required string ArrivalAirport { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; } = DateTime.UtcNow;

    [Range(15, 1440, ErrorMessage = "Estimated duration Minutes must be between 15 and 1440")]
    public int EstimatedDurationMinutes { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Ticket price cannot be zero or negative")]
    public double TicketPrice { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DepartureTime < DateTime.UtcNow)
        {
            yield return new ValidationResult(
                "Departure time must be in future",
                new[] { nameof(DepartureTime) }
            );
        }

        if (!IsValidAirportCode(DepartureAirport))
        {
            yield return new ValidationResult(
                "Departure airport must be a valid 3-letter IATA code.",
                new[] { nameof(DepartureAirport) }
            );
        }

        if (!IsValidAirportCode(ArrivalAirport))
        {
            yield return new ValidationResult(
                "Arrival airport must be a valid 3-letter IATA code.",
                new[] { nameof(ArrivalAirport) }
            );
        }

        if (string.Equals(DepartureAirport, ArrivalAirport, StringComparison.OrdinalIgnoreCase))
        {
            yield return new ValidationResult(
                "Departure and arrival airports cannot be the same",
                new[] { nameof(DepartureAirport), nameof(ArrivalAirport) }
            );
        }
    }

    private static bool IsValidAirportCode(string aiportCode)
    {
        if (string.IsNullOrWhiteSpace(aiportCode)) return false;

        return Regex.IsMatch(aiportCode, @"^[A-Z]{3}$");
    }
}
