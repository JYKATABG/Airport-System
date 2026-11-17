using System;
using System.ComponentModel.DataAnnotations;
using backend.Enums;

namespace backend.DTOs.TicketDTOs;

public class TicketUpdateDto
{
    [RegularExpression("^[0-9]{1,3}[A-Za-z]$", ErrorMessage = "Seat number must look like 12A.")]
    public string? SeatNumber { get; set; }
    [EnumDataType(typeof(TicketStatus), ErrorMessage = "Invalid ticket status value.")]
    public TicketStatus? Status { get; set; }
}
