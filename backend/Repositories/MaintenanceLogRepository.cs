using System;
using backend.Data;
using backend.Helpers;
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

    public async Task<IEnumerable<MaintenanceLog>> GetAllAsync(MaintenanceLogQueryObject query)
    {
        var queryable = _context.MaintenanceLogs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.TechnicianName))
        {
            var lowerTechnicianName = query.TechnicianName.ToLower();
            queryable = queryable.Where(log => log.TechnicianName.ToLower().Contains(lowerTechnicianName));
        }

        if (query.IsCriticalRepair.HasValue)
        {
            queryable = queryable.Where(log => log.IsCriticalRepair == query.IsCriticalRepair.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Technician", StringComparison.OrdinalIgnoreCase))
            {
                queryable = query.IsDescending ?
                queryable.OrderByDescending(log => log.TechnicianName)
                : queryable.OrderBy(log => log.TechnicianName);
            }

            if (query.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
            {
                queryable = query.IsDescending ?
                queryable.OrderByDescending(log => log.LoggedAt)
                : queryable.OrderBy(log => log.LoggedAt);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        return await queryable.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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
