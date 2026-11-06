using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class MaintenanceLogCreateDto : IValidatableObject
{
    [Required(ErrorMessage = "Aircraft ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Aircraft ID must be a positive integer.")]
    public int AircraftId { get; set; }

    [Required(ErrorMessage = "Technician name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Technician name must be between 2 and 100 characters.")]
    public required string TechnicianName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Work description is required.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Work description must be between 5 and 500 characters.")]
    public required string WorkDescription { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Cost must be greater than zero.")]
    public double CostUsd { get; set; }

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public bool IsCriticalRepair { get; set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LoggedAt > DateTime.UtcNow)
        {
            yield return new ValidationResult(
                "Maintenance log date cannot be in the future.",
                new[] { nameof(LoggedAt) }
            );
        }
    }
}
