using System;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.TicketDTOs;

public class TicketCreateDto
{
    [Required(ErrorMessage = "Flight schedule id is required.")]
    public int FlightScheduleId { get; set; }

    [Required(ErrorMessage = "Seat number is required.")]
    [RegularExpression(@"^[0-9]{1,2}[A-Za-z]$", ErrorMessage = "Seat number must look like 12A (e.g. 14B).")]
    public string SeatNumber { get; set; } = string.Empty;
}
