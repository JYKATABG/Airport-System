using System;
using backend.DTOs.Aircraft;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<FlightSchedule> FlightSchedules { get; set; }
    public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

        modelBuilder.Entity<User>()
        .HasIndex(u => u.Username)
        .IsUnique();

        modelBuilder.Entity<Ticket>()
        .HasIndex(t => new { t.FlightScheduleId, t.SeatNumber })
        .IsUnique();
        modelBuilder.Entity<Ticket>()
        .Property(t => t.Status)
        .HasConversion<string>();
    }
}
