using System;
using backend.Data;
using backend.Dtos;
using backend.DTOs.Aircraft;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AircraftController : ControllerBase
{
    private readonly IAircraftRepository _aircraftRepo;

    public AircraftController(IAircraftRepository aircraftRepo)
    {
        _aircraftRepo = aircraftRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AircraftResponseDto>>> GetAll([FromQuery] AircraftQueryObject query)
    {
        var aircrafts = await _aircraftRepo.GetAllAsync(query);

        var aircraftDTOs = aircrafts.Select(aircraft => aircraft.ToDto());

        return Ok(aircraftDTOs);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AircraftResponseDto>> GetById(int id)
    {
        var aircraft = await _aircraftRepo.GetByIdAsync(id);

        if (aircraft == null)
        {
            return NotFound(new { message = $"Aircraft with Id {id} not found" });
        }

        return Ok(aircraft.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<AircraftResponseDto>> Create([FromBody] AircraftCreateDto dto)
    {
        var existingAircraft = await _aircraftRepo.ExistByRegistrationNumberAsync(dto.RegistrationNumber);
        if (existingAircraft)
        {
            return Conflict(new { message = $"Aircraft with registration number '{dto.RegistrationNumber}' already exists." });
        }

        var aircraft = dto.ToAircraftFromCreateDto();

        await _aircraftRepo.CreateAsync(aircraft);
        await _aircraftRepo.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { Id = aircraft.Id }, aircraft.ToDto());
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<AircraftResponseDto>> Update(int id, [FromBody] AircraftUpdateDto dto)
    {
        var aircraft = await _aircraftRepo.GetByIdAsync(id);
        if (aircraft == null)
        {
            return NotFound(new { message = $"Aircraft with Id {id} not found" });
        }

        if (!string.IsNullOrEmpty(dto.RegistrationNumber) && dto.RegistrationNumber != aircraft.RegistrationNumber)
        {
            var exists = await _aircraftRepo.ExistByRegistrationNumberAsync(dto.RegistrationNumber);
            if (exists)
            {
                return Conflict(new { message = $"Aircraft with registration number {dto.RegistrationNumber} already exists." });
            }
            aircraft.RegistrationNumber = dto.RegistrationNumber;
        }

        if (dto.LastInspectionDate.HasValue)
        {
            if (dto.LastInspectionDate.Value > DateTime.UtcNow)
            {
                return BadRequest(new { message = "Last inspection date cannot be in the future." });
            }
            aircraft.LastInspectionDate = dto.LastInspectionDate.Value;
        }

        aircraft.ApplyPartialUpdate(dto);
        await _aircraftRepo.SaveChangesAsync();

        return Ok(aircraft.ToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        var aircraft = await _aircraftRepo.GetByIdAsync(id);
        if (aircraft == null)
        {
            return NotFound(new { message = $"Aircraft with Id {id} was not found." });
        }

        _aircraftRepo.Remove(aircraft);
        await _aircraftRepo.SaveChangesAsync();

        return NoContent();
    }

}
