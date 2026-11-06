using System;
using backend.DTOs;
using backend.Interfaces;
using backend.Mappers;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceLogController : ControllerBase
{

    private readonly IMaintenanceLogRepository _maintenanceLogRepo;
    private readonly IAircraftRepository _aircraftRepo;

    public MaintenanceLogController(IMaintenanceLogRepository maintenanceLogRepo, IAircraftRepository aircraftRepo)
    {
        _maintenanceLogRepo = maintenanceLogRepo;
        _aircraftRepo = aircraftRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaintenanceLogResponseDto>>> GetAll()
    {
        var maintenanceLogs = await _maintenanceLogRepo.GetAllAsync();
        var maintenanceLogDTOs = maintenanceLogs.Select(log => log.ToDto());
        return Ok(maintenanceLogDTOs);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MaintenanceLogResponseDto>> GetById(int id)
    {
        var existingMaintenanceLog = await _maintenanceLogRepo.GetByIdAsync(id);
        if (existingMaintenanceLog == null)
        {
            return NotFound(new { message = $"Maintenance log with Id {id} not found." });
        }

        return Ok(existingMaintenanceLog.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<MaintenanceLogResponseDto>> Create([FromBody] MaintenanceLogCreateDto dto)
    {
        var aircraft = await _aircraftRepo.GetByIdAsync(dto.AircraftId);
        if (aircraft == null)
        {
            return NotFound(new { message = $"Aircraft with Id {dto.AircraftId} not found." });
        }

        var existingMaintenanceLog = await _maintenanceLogRepo.ExistByTechnicianName(dto.TechnicianName);
        if (existingMaintenanceLog)
        {
            return Conflict(new { message = $"Maintenance log with technician name {dto.TechnicianName} already exists." });
        }

        var maintenanceLog = dto.ToMaintenanceLogToCreateDto();

        await _maintenanceLogRepo.CreateAsync(maintenanceLog);
        await _maintenanceLogRepo.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { Id = maintenanceLog.Id }, maintenanceLog.ToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        var maintenanceLog = await _maintenanceLogRepo.GetByIdAsync(id);
        if (maintenanceLog == null)
        {
            return NotFound(new { message = $"Maintenance log with Id {id} not found." });
        }

        _maintenanceLogRepo.Remove(maintenanceLog);
        await _maintenanceLogRepo.SaveChangesAsync();

        return NoContent();
    }
}
