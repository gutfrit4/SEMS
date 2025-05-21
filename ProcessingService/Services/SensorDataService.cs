using Microsoft.EntityFrameworkCore;
using ProcessingService.Data;
using ProcessingService.Interfaces;
using ProcessingService.Models;

namespace ProcessingService.Services;

public class SensorDataService(AppDbContext context) : ISensorDataService
{
    public async Task<IEnumerable<SensorData>> GetAllAsync()
    {
        return await context.SensorData
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();
    }

    public async Task<SensorData> CreateAsync(SensorData data)
    {
        try
        {
            data.Timestamp = DateTime.UtcNow;
            context.SensorData.Add(data);
            await context.SaveChangesAsync();
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("DB Save Failed", ex);
        }
    }
}