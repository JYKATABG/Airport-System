using System;
using backend.DTOs;
using backend.Models;

namespace backend.Mappers;

public static class FlightScheduleMappingExtensions
{
    public static FlightScheduleResponseDto ToDto(this FlightSchedule dto)
    {
        return new FlightScheduleResponseDto
        {
            Id = dto.Id,
            FlightNumber = dto.FlightNumber,
            AircraftId = dto.AircraftId,
            DepartureAirport = dto.DepartureAirport,
            ArrivalAirport = dto.ArrivalAirport,
            DepartureTime = dto.DepartureTime,
            EstimatedDurationMinutes = dto.EstimatedDurationMinutes,
            TicketPrice = dto.TicketPrice
        };
    }

    public static FlightSchedule ToFlightScheduleToCreateDto(this FlightScheduleCreateDto dto)
    {
        return new FlightSchedule
        {
            FlightNumber = dto.FlightNumber,
            AircraftId = dto.AircraftId,
            DepartureAirport = dto.DepartureAirport,
            ArrivalAirport = dto.ArrivalAirport,
            DepartureTime = dto.DepartureTime,
            EstimatedDurationMinutes = dto.EstimatedDurationMinutes,
            TicketPrice = dto.TicketPrice
        };
    }

    public static void ApplyPartialUpdate(this FlightSchedule schedule, FlightScheduleUpdateDto dto)
    {
        if (dto.FlightNumber != null) schedule.FlightNumber = dto.FlightNumber;
        if (dto.AircraftId.HasValue) schedule.AircraftId = dto.AircraftId.Value;
        if (dto.DepartureAirport != null) schedule.DepartureAirport = dto.DepartureAirport;
        if (dto.ArrivalAirport != null) schedule.ArrivalAirport = dto.ArrivalAirport;
        if (dto.DepartureTime.HasValue) schedule.DepartureTime = dto.DepartureTime.Value;
        if (dto.EstimatedDurationMinutes.HasValue) schedule.EstimatedDurationMinutes = dto.EstimatedDurationMinutes.Value;
        if (dto.TicketPrice.HasValue) schedule.TicketPrice = dto.TicketPrice.Value;
    }
}
