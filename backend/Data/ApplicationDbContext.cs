using System;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<FlightSchedule> FlightSchedules { get; set; }
    public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
}
