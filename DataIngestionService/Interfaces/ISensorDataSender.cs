using DataIngestionService.Models;

namespace DataIngestionService.Interfaces;

public interface ISensorDataSender
{
    Task<bool> SendAsync(SensorData data);
}