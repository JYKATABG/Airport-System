using backend.DTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class FlightScheduleController : ControllerBase
{
    private readonly IFlightScheduleRepository _flightScheduleRepo;
    private readonly IAircraftRepository _aircraftRepo;

    public FlightScheduleController(IFlightScheduleRepository flightScheduleRepo, IAircraftRepository aircraftRepo)
    {
        _flightScheduleRepo = flightScheduleRepo;
        _aircraftRepo = aircraftRepo;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightScheduleResponseDto>>> GetAll([FromQuery] FlightScheduleQueryObject query)
    {
        var flightSchedules = await _flightScheduleRepo.GetAllAsync(query);

        var flightSchedulesDTO = flightSchedules.Select(flight => flight.ToDto());

        return Ok(flightSchedulesDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FlightScheduleResponseDto>> GetById(int id)
    {
        var flightSchedule = await _flightScheduleRepo.GetByIdAsync(id);
        if (flightSchedule == null)
        {
            return NotFound(new { message = $"Flight schedule with Id {id} not found." });
        }

        return Ok(flightSchedule.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<FlightScheduleResponseDto>> Create([FromBody] FlightScheduleCreateDto dto)
    {
        var aircraft = await _aircraftRepo.GetByIdAsync(dto.AircraftId);
        if (aircraft == null)
        {
            return NotFound(new { message = $"Aircraft with Id {dto.AircraftId} not found." });
        }

        var existingFlightSchedule = await _flightScheduleRepo.ExistByFlightNumber(dto.FlightNumber);
        if (existingFlightSchedule)
        {
            return Conflict(new { message = $"Flight schedule with flight number {dto.FlightNumber} already exists." });
        }

        var flightSchedule = dto.ToFlightScheduleToCreateDto();

        await _flightScheduleRepo.CreateAsync(flightSchedule);
        await _flightScheduleRepo.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { Id = flightSchedule.Id }, flightSchedule.ToDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var flightSchedule = await _flightScheduleRepo.GetByIdAsync(id);
        if (flightSchedule == null)
        {
            return NotFound(new { message = $"Flight schedule with Id {id} not found." });
        }

        _flightScheduleRepo.Remove(flightSchedule);
        await _flightScheduleRepo.SaveChangesAsync();

        return NoContent();
    }
}
