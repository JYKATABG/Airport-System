using System;

namespace backend.Helpers;

public class FlightScheduleQueryObject : BaseQueryObject
{
    public string? DepartureAirport { get; set; }
    public string? ArrivalAirport { get; set; }
}
