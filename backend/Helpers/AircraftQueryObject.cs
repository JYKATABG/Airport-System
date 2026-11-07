using System;

namespace backend.Helpers;

public class AircraftQueryObject : BaseQueryObject
{
    public string? ModelName { get; set; }
    public bool? IsAirWorthy { get; set; }
}
