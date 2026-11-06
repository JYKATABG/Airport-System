using System;
using backend.Models;

namespace backend.DTOs;

public class MaintenanceLogResponseDto
{
    public int Id { get; set; }
    public int AircraftId { get; set; }
    public required string TechnicianName { get; set; } = string.Empty;
    public required string WorkDescription { get; set; } = string.Empty;
    public double CostUsd { get; set; }
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    public bool IsCriticalRepair { get; set; } = false;
}
