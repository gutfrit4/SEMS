using ProcessingService.Models;

namespace ProcessingService.Interfaces;

public interface ISensorAnalyticsService
{
    Task<IEnumerable<SensorDataAnalytics>> GetAnalyticsAsync();
}