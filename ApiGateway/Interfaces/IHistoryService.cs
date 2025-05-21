using ApiGateway.Models;

namespace ApiGateway.Interfaces;

public interface IHistoryService
{
    Task<IEnumerable<SensorData>> GetHistoryAsync();
}