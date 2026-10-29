using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Aircraft : IValidatableObject
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Registration number is required.")]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Registration number must be between 3 and 15 characters.")]
    [RegularExpression(@"^[A-Z0-9\-]+$", ErrorMessage = "Registration number must include only uppercase letters, numbers and dashes (exp. LZ-ABC).")]
    public required string RegistrationNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Model name is required.")]
    [StringLength(100, ErrorMessage = "Model name must have max 100 characters.")]
    public required string ModelName { get; set; } = string.Empty;
    [Range(1, int.MaxValue, ErrorMessage = "Flight hours must be greater than 0")]
    public double TotalFlightHours { get; set; }
    [Range(1900, 2026, ErrorMessage = "Manufacture year must be between 1990 and 2026.")]
    public int ManufactureYear { get; set; }
    public DateTime LastInspectionDate { get; set; } = DateTime.UtcNow;
    public bool IsAirWorthy { get; set; } = true;

    public ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
    public ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LastInspectionDate > DateTime.UtcNow)
        {
            yield return new ValidationResult(
                "Date on last inspection cannot be in future.",
                new[] { nameof(LastInspectionDate) }
            );
        }
    }
}
