using System;

namespace backend.Models;

public class Aircraft
{
    public int Id { get; set; }
    public required string RegistrationNumber { get; set; } = string.Empty;
    public required string ModelName { get; set; } = string.Empty;
    public double TotalFlightHours { get; set; }
    public int ManufactureYear { get; set; }
    public DateTime LastInspectionDate { get; set; } = DateTime.UtcNow;
    public bool IsAirWorthy { get; set; } = true;

    public ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
    public ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
}
