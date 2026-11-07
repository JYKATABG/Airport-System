using System;
using backend.Data;
using backend.Helpers;
using backend.Models;

namespace backend.Interfaces;

public interface IMaintenanceLogRepository
{
    Task<IEnumerable<MaintenanceLog>> GetAllAsync(MaintenanceLogQueryObject query);
    Task<MaintenanceLog?> GetByIdAsync(int id);
    Task CreateAsync(MaintenanceLog maintenanceLog);
    Task<bool> ExistByTechnicianName(string technicianName);

    Task<bool> SaveChangesAsync();
    void Remove(MaintenanceLog maintenanceLog);
}
