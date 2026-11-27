using System;
using System.Security.Claims;
using backend.DTOs.TicketDTOs;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketRepository _ticketRepo;

    public TicketController(ITicketRepository ticketRepository)
    {
        _ticketRepo = ticketRepository;
    }

    private bool IsAdmin => User.IsInRole("Admin");

    [HttpPost]
    public async Task<ActionResult<TicketResponseDto>> Buy([FromBody] TicketCreateDto dto)
    {
        var result = await _ticketRepo.BuyAsync(User.GetUserId(), dto);

        return result.Error switch
        {
            TicketError.None => CreatedAtAction(nameof(GetById), new { id = result.Ticket!.Id }, result.Ticket.ToDto()),
            TicketError.FlightNotFound => NotFound(new { message = $"Flight with id {dto.FlightScheduleId} not found." }),
            TicketError.FlightDeparted => BadRequest(new { message = "This flight has already departed" }),
            TicketError.SeatTaken => Conflict(new { message = $"Seat {dto.SeatNumber} is already taken." }),
            _ => BadRequest()
        };
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketResponseDto>>> GetMine()
    {
        var tickets = await _ticketRepo.GetByUserAsync(User.GetUserId());
        return Ok(tickets.Select(t => t.ToDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketResponseDto>> GetById(int id)
    {
        var result = await _ticketRepo.GetByIdAsync(id, User.GetUserId(), IsAdmin);

        return result.Error switch
        {
            TicketError.None => Ok(result.Ticket!.ToDto()),
            TicketError.Forbidden => Forbid(),
            _ => NotFound(new { message = $"Ticket with Id {id} not found." })
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Cancel(int id)
    {
        var error = await _ticketRepo.CancelTicket(id, User.GetUserId(), IsAdmin);

        return error switch
        {
            TicketError.None => NoContent(),
            TicketError.Forbidden => Forbid(),
            TicketError.FlightDeparted => BadRequest(new { message = "Cannot cancel a ticket for a departed flight." }),
            _ => NotFound(new { message = $"Ticket with Id {id} not found." })
        };
    }
}
