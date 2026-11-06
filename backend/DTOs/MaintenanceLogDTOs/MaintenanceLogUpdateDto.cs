using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class MaintenanceLogUpdateDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Aircraft ID must be a positive integer.")]
    public int? AircraftId { get; set; }

    [StringLength(100, MinimumLength = 2, ErrorMessage = "Technician name must be between 2 and 100 characters.")]
    public string? TechnicianName { get; set; }

    [StringLength(500, MinimumLength = 5, ErrorMessage = "Work description must be between 5 and 500 characters.")]
    public string? WorkDescription { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Cost must be greater than zero.")]
    public double? CostUsd { get; set; }

    public DateTime? LoggedAt { get; set; }

    public bool? IsCriticalRepair { get; set; }
}
