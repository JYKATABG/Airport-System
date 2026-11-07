using System;

namespace backend.Helpers;

public class MaintenanceLogQueryObject : BaseQueryObject
{
    public string? TechnicianName { get; set; }
    public bool? IsCriticalRepair { get; set; }
}
