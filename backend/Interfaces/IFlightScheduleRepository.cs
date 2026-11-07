using System;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces;

public interface IFlightScheduleRepository
{
    Task<IEnumerable<FlightSchedule>> GetAllAsync(FlightScheduleQueryObject query);
    Task<FlightSchedule?> GetByIdAsync(int id);
    Task CreateAsync(FlightSchedule flightSchedule);
    Task<bool> ExistByFlightNumber(string flightNumber);
    Task<bool> SaveChangesAsync();
    void Remove(FlightSchedule flightSchedule);
}
