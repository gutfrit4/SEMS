using ProcessingService.Models;

namespace ProcessingService.Interfaces;

public interface ISensorDataService
{
    Task<IEnumerable<SensorData>> GetAllAsync();
    Task<SensorData> CreateAsync(SensorData data);
}