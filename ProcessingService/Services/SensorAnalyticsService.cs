using Microsoft.EntityFrameworkCore;
using ProcessingService.Data;
using ProcessingService.Interfaces;
using ProcessingService.Models;

namespace ProcessingService.Services;

public class SensorAnalyticsService(AppDbContext context) : ISensorAnalyticsService
{
    public async Task<IEnumerable<SensorDataAnalytics>> GetAnalyticsAsync()
    {
        return await context.SensorData
            .GroupBy(d => d.DeviceId)
            .Select(g => new SensorDataAnalytics
            {
                DeviceId = g.Key,
                Count = g.Count(),
                AvgTemperature = Math.Round(g.Average(x => x.Temperature), 2),
                MaxTemperature = g.Max(x => x.Temperature),
                MinTemperature = g.Min(x => x.Temperature),
            })
            .ToListAsync();
    }
}