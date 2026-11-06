using System;
using backend.Data;
using backend.Models;

namespace backend.Interfaces;

public interface IMaintenanceLogRepository
{
    Task<IEnumerable<MaintenanceLog>> GetAllAsync();
    Task<MaintenanceLog?> GetByIdAsync(int id);
    Task CreateAsync(MaintenanceLog maintenanceLog);
    Task<bool> ExistByTechnicianName(string technicianName);

    Task<bool> SaveChangesAsync();
    void Remove(MaintenanceLog maintenanceLog);
}
