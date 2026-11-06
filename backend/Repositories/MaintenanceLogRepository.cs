using System;
using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class MaintenanceLogRepository : IMaintenanceLogRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceLogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(MaintenanceLog maintenanceLog)
    {
        await _context.AddAsync(maintenanceLog);
    }

    public Task<bool> ExistByTechnicianName(string technicianName)
    {
        return _context.MaintenanceLogs.AnyAsync(log => log.TechnicianName.ToLower() == technicianName.ToLower());
    }

    public async Task<IEnumerable<MaintenanceLog>> GetAllAsync()
    {
        return await _context.MaintenanceLogs.ToListAsync();
    }

    public async Task<MaintenanceLog?> GetByIdAsync(int id)
    {
        return await _context.MaintenanceLogs.FindAsync(id);
    }

    public void Remove(MaintenanceLog maintenanceLog)
    {
        _context.MaintenanceLogs.Remove(maintenanceLog);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
