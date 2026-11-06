using System;

namespace backend.Dtos;

public class AircraftResponseDto
{
    public int Id { get; set; }
    public required string RegistrationNumber { get; set; } = string.Empty;
    public required string ModelName { get; set; } = string.Empty;
    public double TotalFlightHours { get; set; }
    public int ManufactureYear { get; set; }
    public DateTime LastInspectionDate { get; set; }
    public bool IsAirWorthy { get; set; } = true;
}
