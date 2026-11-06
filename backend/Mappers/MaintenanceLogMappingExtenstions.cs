using System;
using backend.DTOs;
using backend.Models;

namespace backend.Mappers;

public static class MaintenanceLogMappingExtenstions
{
    public static MaintenanceLogResponseDto ToDto(this MaintenanceLog dto)
    {
        return new MaintenanceLogResponseDto
        {
            Id = dto.Id,
            AircraftId = dto.AircraftId,
            TechnicianName = dto.TechnicianName,
            WorkDescription = dto.WorkDescription,
            CostUsd = dto.CostUsd,
            LoggedAt = dto.LoggedAt,
            IsCriticalRepair = dto.IsCriticalRepair
        };
    }

    public static MaintenanceLog ToMaintenanceLogToCreateDto(this MaintenanceLogCreateDto dto)
    {
        return new MaintenanceLog
        {
            AircraftId = dto.AircraftId,
            TechnicianName = dto.TechnicianName,
            WorkDescription = dto.WorkDescription,
            CostUsd = dto.CostUsd,
            LoggedAt = dto.LoggedAt,
            IsCriticalRepair = dto.IsCriticalRepair
        };
    }

    public static void ApplyPartialUpdate(this MaintenanceLog log, MaintenanceLogUpdateDto dto)
    {
        if (dto.TechnicianName != null) log.TechnicianName = dto.TechnicianName;
        if (dto.AircraftId.HasValue) log.AircraftId = dto.AircraftId.Value;
        if (dto.WorkDescription != null) log.WorkDescription = dto.WorkDescription;
        if (dto.CostUsd.HasValue) log.CostUsd = dto.CostUsd.Value;
        if (dto.LoggedAt.HasValue) log.LoggedAt = dto.LoggedAt.Value;
        if (dto.IsCriticalRepair.HasValue) log.IsCriticalRepair = dto.IsCriticalRepair.Value;

    }
}
