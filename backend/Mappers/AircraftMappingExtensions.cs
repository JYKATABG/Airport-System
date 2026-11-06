using System;
using backend.Dtos;
using backend.DTOs.Aircraft;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Mappers;

public static class AircraftMappingExtensions
{
    public static Aircraft ToAircraftFromCreateDto(this AircraftCreateDto dto)
    {
        return new Aircraft
        {
            RegistrationNumber = dto.RegistrationNumber,
            ModelName = dto.ModelName,
            TotalFlightHours = dto.TotalFlightHours,
            ManufactureYear = dto.ManufactureYear,
            LastInspectionDate = dto.LastInspectionDate,
            IsAirWorthy = dto.IsAirWorthy
        };
    }

    public static AircraftResponseDto ToDto(this Aircraft aircraft)
    {
        return new AircraftResponseDto
        {
            Id = aircraft.Id,
            RegistrationNumber = aircraft.RegistrationNumber,
            ModelName = aircraft.ModelName,
            TotalFlightHours = aircraft.TotalFlightHours,
            ManufactureYear = aircraft.ManufactureYear,
            LastInspectionDate = aircraft.LastInspectionDate,
            IsAirWorthy = aircraft.IsAirWorthy
        };
    }

    public static void ApplyPartialUpdate(this Aircraft aircraft, AircraftUpdateDto dto)
    {
        if (dto.ModelName != null)
        {
            aircraft.ModelName = dto.ModelName;
        }

        if (dto.TotalFlightHours.HasValue)
        {
            aircraft.TotalFlightHours = dto.TotalFlightHours.Value;
        }

        if (dto.ManufactureYear.HasValue)
        {
            aircraft.ManufactureYear = dto.ManufactureYear.Value;
        }

        if (dto.IsAirWorthy.HasValue)
        {
            aircraft.IsAirWorthy = dto.IsAirWorthy.Value;
        }
    }
}
