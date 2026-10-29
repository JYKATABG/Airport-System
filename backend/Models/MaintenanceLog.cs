using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class MaintenanceLog : IValidatableObject
{
    public int Id { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "AircraftId is required and must be a valid positive number.")]
    public int AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
    [Required(ErrorMessage = "Technician name is required.")]
    [MinLength(2, ErrorMessage = "Technician name must be at least 2 characters.")]
    public required string TechnicianName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Work description is required.")]
    [MinLength(15, ErrorMessage = "Work description must be at least 15 characters.")]
    public required string WorkDescription { get; set; } = string.Empty;
    [Range(0, int.MaxValue, ErrorMessage = "Cost cannot be negative.")]
    public double CostUsd { get; set; }
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    public bool IsCriticalRepair { get; set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LoggedAt > DateTime.UtcNow)
        {
            yield return new ValidationResult(
                "Maintenance cannot be logged in future",
                new[] { nameof(LoggedAt) }
            );
        }
    }
}
